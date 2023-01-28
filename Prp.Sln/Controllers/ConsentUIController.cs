using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ConsentUIController : BaseController
    {
        // GET: ConsentUI
        public ActionResult ApplicantConsent()
        {
            ConsentModel model = new ConsentModel();


            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            model.roundId = roundId;

            model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, loggedInUser.applicantId);
            model.consentId = Request.QueryString["consentId"].TooInt();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "ByType";
            dDLConstant.typeId = ProjConstant.Constant.consentType;
            model.listConsentStatus = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();
            model.validStatus = 1;

            if (model.validStatus == 1)
            {

                if (model.consentId > 0)
                {
                    model.consent = new ConsentDAL().GetById(model.consentId);

                    if (model.consent.applicantId != loggedInUser.applicantId)
                        model.validStatus = 0;
                }
                if (model.validStatus == 1)
                {

                    model.listType = new ConsentDAL().GetTypeApplicantHasMerit(loggedInUser.applicantId, roundId);
                    //if (model.listType != null && model.listType.Count > 0)
                    model.listConsent = new ConsentDAL().GetAllByApplicant(loggedInUser.applicantId);
                }


            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ApplicantContents()
        {
            ConsentModel model = new ConsentModel();
            model.listConsent = new ConsentDAL().GetAllByApplicant(loggedInUser.applicantId);
            string json = JsonConvert.SerializeObject(model.listConsent);
            return Content(json, "application/json");
        }

        [HttpPost]
        public JsonResult ConsentSave(ConsentPushed consentsFinal)
        {
            Message msg = new Message();
            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            int inductionId = ProjConstant.inductionId;
            int phaseId = ProjConstant.phaseId;
            for (int i = 0; i < consentsFinal.consentsFinal.Count; i++)
            {
                Consent obj = new Consent();
                obj.applicantId = loggedInUser.applicantId;
                obj.typeId = consentsFinal.consentsFinal[i].typeId.TooInt();
                obj.consentTypeId = consentsFinal.consentsFinal[i].consentTypeId.TooInt();
                obj.roundId = roundId;
                obj.inductionId = inductionId;
                obj.phaseId = phaseId;
                //msg = new ConsentDAL().AddUpdateConsent(obj);
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ApplicantMerits()
        {
            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            
            ConsentModel model = new ConsentModel();
            model.roundId = roundId;
            model.listType = new ConsentDAL().GetTypeApplicantHasMerit(loggedInUser.applicantId, roundId);
            string json = JsonConvert.SerializeObject(model.listType);
            return Content(json, "application/json");
        }

        public ActionResult ApplicantJoining()
        {
            ApplicantJoiningModel model = new ApplicantJoiningModel();
            model.applicantFinal = new JoiningDAL().GetByApplicantDetailById(loggedInUser.applicantId);
            model.info = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            return View(model);
        }



        [ValidateInput(false)]
        public ActionResult SaveConsentData(ConsentModel ModelSave, HttpPostedFileBase files)
        {

            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            Consent obj = ModelSave.consent;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.consentId = obj.consentId.TooInt();
            obj.applicantId = loggedInUser.applicantId;
            obj.typeId = obj.typeId.TooInt();
            obj.consentTypeId = obj.consentTypeId.TooInt();
            obj.roundId = roundId;
            Message m = new ConsentDAL().AddUpdate(obj);
            return Redirect("/"+ProjConstant.preUrl+"/applicant/consent");
        }
    }
}