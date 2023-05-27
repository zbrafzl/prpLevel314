using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class ApplicationUpdateController : BaseAdminController
	{
		public ApplicationUpdateController()
		{
		}

		[HttpPost]
		public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
		{
			ApplicantExperience applicantExperience = new ApplicantExperience()
			{
				applicantExperienceId = objExperience.applicantExperienceId,
				applicantId = objExperience.applicantId,
				levelId = objExperience.levelId,
				levelTypeId = objExperience.levelTypeId,
				instituteId = objExperience.instituteId,
				provinceId = objExperience.provinceId,
				typeId = objExperience.typeId,
				instituteName = objExperience.instituteName,
				imageExperience = objExperience.imageExperience,
				isCurrent = objExperience.isCurrent,
				startDated = objExperience.startDated.TooDate()
			};
			if (!applicantExperience.isCurrent)
			{
				applicantExperience.endDate = objExperience.endDate.TooDate();
			}
			else
			{
				applicantExperience.endDate = DateTime.Now;
			}
			applicantExperience.adminId = base.loggedInUser.userId;
			Message message = (new ApplicantAdminDAL()).ApplicantExperienceAddUpdate(applicantExperience);
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult ExperienceAdmin()
		{
			ExperienceModelAdmin experienceModelAdmin = new ExperienceModelAdmin();
			try
			{
				int num = Request.QueryString["applicantid"].TooInt();
				if (num > 0)
				{
					experienceModelAdmin.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
					experienceModelAdmin.listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, "");
					experienceModelAdmin.listExperience = (new ApplicantDAL()).GetApplicantExperienceListByApplicant(experienceModelAdmin.inductionId, experienceModelAdmin.phaseId, num);
					experienceModelAdmin.listMarks = (new MarksDAL()).GetMarksAccumulativeByApplicant(num);
				}
			}
			catch (Exception exception)
			{
				experienceModelAdmin = new ExperienceModelAdmin();
			}
			return View(experienceModelAdmin);
		}

		[HttpGet]
		public JsonResult GetApplicantExperienceDataSingleRow(int applicantId, int applicantExperienceId)
		{
			ApplicantExperience applicantExperience = new ApplicantExperience();
			try
			{
				List<ApplicantExperience> applicantExperienceListByApplicant = (new ApplicantDAL()).GetApplicantExperienceListByApplicant(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
				if ((applicantExperienceListByApplicant == null ? false : applicantExperienceListByApplicant.Count > 0))
				{
					applicantExperience = applicantExperienceListByApplicant.FirstOrDefault<ApplicantExperience>((ApplicantExperience x) => x.applicantExperienceId == applicantExperienceId);
				}
			}
			catch (Exception exception)
			{
				applicantExperience = new ApplicantExperience();
			}
			return base.Json(applicantExperience, 0);
		}
	}
}