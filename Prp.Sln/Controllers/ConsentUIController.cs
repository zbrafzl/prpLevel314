using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class ConsentUIController : BaseController
	{
		public ConsentUIController()
		{
		}

		public ActionResult ApplicantConsent()
		{
			ConsentModel consentModel = new ConsentModel();
			int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
			consentModel.roundId = num;
			consentModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, base.loggedInUser.applicantId);
			consentModel.consentId = Request.QueryString["consentId"].TooInt();
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "ByType",
				typeId = ProjConstant.Constant.consentType
			};
			consentModel.listConsentStatus = (
				from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
				orderby x.id
				select x).ToList<EntityDDL>();
			consentModel.validStatus = 1;
			if (consentModel.validStatus == 1)
			{
				if (consentModel.consentId > 0)
				{
					consentModel.consent = (new ConsentDAL()).GetById(consentModel.consentId);
					if (consentModel.consent.applicantId != base.loggedInUser.applicantId)
					{
						consentModel.validStatus = 0;
					}
				}
				if (consentModel.validStatus == 1)
				{
					consentModel.listType = (new ConsentDAL()).GetTypeApplicantHasMerit(base.loggedInUser.applicantId, num);
					consentModel.listConsent = (new ConsentDAL()).GetAllByApplicant(base.loggedInUser.applicantId);
				}
			}
			return View(consentModel);
		}

		[HttpPost]
		public ActionResult ApplicantContents()
		{
			ConsentModel consentModel = new ConsentModel()
			{
				listConsent = (new ConsentDAL()).GetAllByApplicant(base.loggedInUser.applicantId)
			};
			string str = JsonConvert.SerializeObject(consentModel.listConsent);
			return base.Content(str, "application/json");
		}

		public ActionResult ApplicantJoining()
		{
			ApplicantJoiningModel applicantJoiningModel = new ApplicantJoiningModel()
			{
				applicantFinal = (new JoiningDAL()).GetByApplicantDetailById(base.loggedInUser.applicantId),
				info = (new ApplicantDAL()).GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, base.loggedInUser.applicantId)
			};
			return View(applicantJoiningModel);
		}

		[HttpPost]
		public ActionResult ApplicantMerits()
		{
			int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
			ConsentModel consentModel = new ConsentModel()
			{
				roundId = num,
				listType = (new ConsentDAL()).GetTypeApplicantHasMerit(base.loggedInUser.applicantId, num)
			};
			string str = JsonConvert.SerializeObject(consentModel.listType);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public JsonResult ConsentSave(ConsentPushed consentsFinal)
		{
			Message message = new Message();
			int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
			int num1 = ProjConstant.inductionId;
			int num2 = ProjConstant.phaseId;
			for (int i = 0; i < consentsFinal.consentsFinal.Count; i++)
			{
				Consent consent = new Consent()
				{
					applicantId = base.loggedInUser.applicantId,
					typeId = consentsFinal.consentsFinal[i].typeId.TooInt(),
					consentTypeId = consentsFinal.consentsFinal[i].consentTypeId.TooInt(),
					roundId = num,
					inductionId = num1,
					phaseId = num2
				};
				if (base.loggedInUser.adminId == 0)
				{
				}
			}
			return base.Json(message, 0);
		}

		[ValidateInput(false)]
		public ActionResult SaveConsentData(ConsentModel ModelSave, HttpPostedFileBase files)
		{
			int num = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
			Consent modelSave = ModelSave.consent;
			modelSave.inductionId = ProjConstant.inductionId;
			modelSave.phaseId = ProjConstant.phaseId;
			modelSave.consentId = modelSave.consentId.TooInt();
			modelSave.applicantId = base.loggedInUser.applicantId;
			modelSave.typeId = modelSave.typeId.TooInt();
			modelSave.consentTypeId = modelSave.consentTypeId.TooInt();
			modelSave.roundId = num;
			(new ConsentDAL()).AddUpdate(modelSave);
			ActionResult actionResult = this.Redirect(string.Concat("/", ProjConstant.preUrl, "/applicant/consent"));
			return actionResult;
		}
	}
}