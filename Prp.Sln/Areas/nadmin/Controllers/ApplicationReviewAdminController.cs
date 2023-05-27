using Prp.Data;
using Prp.Model;
using Prp.Sln;
using Prp.Sln.Areas.nadmin;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class ApplicationReviewAdminController : BaseAdminController
	{
		public ApplicationReviewAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult ApplicantDebar()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				int phaseId = AdminHelper.GetPhaseId();
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					proofReadingAdminModel.applicantId = 0;
					proofReadingAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					try
					{
						Message applicantDebar = (new VerificationDAL()).getApplicantDebar(num);
						ViewData["debarStatus"] = applicantDebar.status;
					}
					catch (Exception exception)
					{
						ViewData["debarStatus"] = 0;
					}
					if (num > 0)
					{
						int num1 = 0;
						try
						{
							ApplicationStatus applicationStatu = (new ApplicantDAL()).GetApplicationStatus(inductionId, ProjConstant.phaseId, num, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault<ApplicationStatus>();
							num1 = applicationStatu.statusId;
						}
						catch (Exception exception1)
						{
							num1 = 0;
						}
						if ((base.loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification ? false : base.loggedInUser.typeId != ProjConstant.Constant.UserType.keSenior))
						{
							proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, num);
							proofReadingAdminModel.applicantId = num;
						}
						else if (num1 != 8)
						{
							proofReadingAdminModel.applicantId = 0;
							proofReadingAdminModel.requestType = 2;
						}
						else
						{
							proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, num);
							proofReadingAdminModel.applicantId = num;
						}
						if (num1 == 8)
						{
							try
							{
								proofReadingAdminModel.statusApproval = (new VerificationDAL()).GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId, ProjConstant.phaseId, 131, proofReadingAdminModel.applicant.applicantId);
							}
							catch (Exception exception2)
							{
								proofReadingAdminModel.statusApproval = new ApplicantApprovalStatus();
							}
							try
							{
								proofReadingAdminModel.statusAmendment = (new VerificationDAL()).GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId, ProjConstant.phaseId, 132, proofReadingAdminModel.applicant.applicantId);
							}
							catch (Exception exception3)
							{
								proofReadingAdminModel.statusAmendment = new ApplicantApprovalStatus();
							}
							try
							{
								Message message = (new VerificationDAL()).getApplicantDebar(num);
								ViewData["debarStatus"] = message.status;
							}
							catch (Exception exception4)
							{
								ViewData["debarStatus"] = 0;
							}
						}
						proofReadingAdminModel.search.key = str;
						proofReadingAdminModel.search.@value = str1;
					}
					proofReadingAdminModel.applicantId = num;
					proofReadingAdminModel.requestType = 1;
				}
				proofReadingAdminModel.inductionId = inductionId;
				proofReadingAdminModel.search.key = str;
				proofReadingAdminModel.search.@value = str1;
			}
			catch (Exception exception5)
			{
				proofReadingAdminModel.applicantId = 0;
				proofReadingAdminModel.requestType = 3;
			}
			return View(proofReadingAdminModel);
		}

		[HttpPost]
		public JsonResult ApplicantDebarStatusUpdate(ApplicantDebarData obj)
		{
			Message message = (new ApplicantDAL()).ApplicantDebarStatusUpdate(obj.applicantId, obj.typeId, obj.image, base.loggedInUser.userId);
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult ApplicantDetail()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				int phaseId = AdminHelper.GetPhaseId();
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					proofReadingAdminModel.applicantId = 0;
					proofReadingAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 0;
						try
						{
							ApplicationStatus applicationStatu = (new ApplicantDAL()).GetApplicationStatus(inductionId, ProjConstant.phaseId, num, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault<ApplicationStatus>();
							num1 = applicationStatu.statusId;
						}
						catch (Exception exception)
						{
							num1 = 0;
						}
						if ((base.loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification ? false : base.loggedInUser.typeId != ProjConstant.Constant.UserType.keSenior))
						{
							proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, num);
							proofReadingAdminModel.applicantId = num;
						}
						else if (num1 != 8)
						{
							proofReadingAdminModel.applicantId = 0;
							proofReadingAdminModel.requestType = 2;
						}
						else
						{
							proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, num);
							proofReadingAdminModel.applicantId = num;
						}
						if (num1 == 8)
						{
							try
							{
								proofReadingAdminModel.statusApproval = (new VerificationDAL()).GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId, ProjConstant.phaseId, 131, proofReadingAdminModel.applicant.applicantId);
							}
							catch (Exception exception1)
							{
								proofReadingAdminModel.statusApproval = new ApplicantApprovalStatus();
							}
							try
							{
								proofReadingAdminModel.statusAmendment = (new VerificationDAL()).GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId, ProjConstant.phaseId, 132, proofReadingAdminModel.applicant.applicantId);
							}
							catch (Exception exception2)
							{
								proofReadingAdminModel.statusAmendment = new ApplicantApprovalStatus();
							}
						}
						proofReadingAdminModel.search.key = str;
						proofReadingAdminModel.search.@value = str1;
					}
					proofReadingAdminModel.applicantId = num;
					proofReadingAdminModel.requestType = 1;
				}
				proofReadingAdminModel.inductionId = inductionId;
				proofReadingAdminModel.search.key = str;
				proofReadingAdminModel.search.@value = str1;
			}
			catch (Exception exception3)
			{
				proofReadingAdminModel.applicantId = 0;
				proofReadingAdminModel.requestType = 3;
			}
			return View(proofReadingAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantList()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			applicantStatusModel.search.applicationStatusId = ProjConstant.Constant.ApplicationStatus.completed;
			applicantStatusModel.search.condition = "GetByAccountApplicationStatusId";
			applicantStatusModel.listApplicant = (new ApplicantDAL()).GetApplicantList(applicantStatusModel.search);
			return View(applicantStatusModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantView()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				int phaseId = AdminHelper.GetPhaseId();
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				proofReadingAdminModel.search.key = str;
				proofReadingAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					proofReadingAdminModel.applicantId = 0;
					proofReadingAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, num);
						proofReadingAdminModel.search.key = str;
						proofReadingAdminModel.search.@value = str1;
					}
					proofReadingAdminModel.applicantId = num;
					proofReadingAdminModel.requestType = 1;
				}
			}
			catch (Exception exception)
			{
				proofReadingAdminModel.applicantId = 0;
				proofReadingAdminModel.requestType = 3;
			}
			return View(proofReadingAdminModel);
		}

		public ActionResult ApplicantViewDetail()
		{
			int num = Request.QueryString["applicantId"].TooInt();
			int inductionId = AdminHelper.GetInductionId();
			ProofReadingAdminModel proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, AdminHelper.GetPhaseId(), num);
			return View(proofReadingAdminModel);
		}

		public ActionResult ApplicantViewDetailView()
		{
			int num = Request.QueryString["applicantId"].TooInt();
			int inductionId = AdminHelper.GetInductionId();
			ProofReadingAdminModel proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, AdminHelper.GetPhaseId(), num);
			return View(proofReadingAdminModel);
		}
	}
}