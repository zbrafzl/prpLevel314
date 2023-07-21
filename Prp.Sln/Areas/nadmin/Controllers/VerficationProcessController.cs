using Prp.Data;
using Prp.Model;
using Prp.Sln;
using Prp.Sln.Areas.nadmin;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class VerficationProcessController : BaseAdminController
	{
		public VerficationProcessController()
		{
		}

		[HttpPost]
		public JsonResult AddUpdateVerficationAmmenmentStatus(VerificationEntity obj)
		{
			obj.adminId = base.loggedInUser.userId;
			obj.comments = obj.comments.TooString("");
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			Message message = (new VerificationDAL()).AddUpdateVerficationStatus(obj);
			int num = 0;
			//Blocking Email,SMS
			//try
			//{
			//	Applicant applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, obj.applicantId);
			//	string str = "";
			//	int num1 = 0;
			//	if (obj.approvalStatusId == 1)
			//	{
			//		num = ProjConstant.EmailTemplateType.ammendmentAccepted;
			//		try
			//		{
			//			SMS byTypeForApplicant = (new SMSDAL()).GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentApproved);
			//			str = byTypeForApplicant.detail;
			//			num1 = byTypeForApplicant.smsId;
			//		}
			//		catch (Exception exception)
			//		{
			//			str = "";
			//		}
			//		if (string.IsNullOrWhiteSpace(str))
			//		{
			//			num1 = 0;
			//			str = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
			//		}
			//	}
			//	else if (obj.approvalStatusId == 2)
			//	{
			//		num = ProjConstant.EmailTemplateType.ammendmentRejected;
			//		try
			//		{
			//			try
			//			{
			//				SMS sM = (new SMSDAL()).GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentReject);
			//				str = sM.detail;
			//				num1 = sM.smsId;
			//			}
			//			catch (Exception exception1)
			//			{
			//				num1 = 0;
			//				str = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
			//			}
			//		}
			//		catch (Exception exception2)
			//		{
			//		}
			//	}
			//	try
			//	{
			//		EmailProcess emailProcess = new EmailProcess()
			//		{
			//			applicantId = obj.applicantId,
			//			keyword = "",
			//			typeId = num,
			//			adminId = base.loggedInUser.adminId
			//		};
			//		(new EmailDAL()).EmailProcessAdd(emailProcess);
			//	}
			//	catch (Exception exception3)
			//	{
			//	}
			//	try
			//	{
			//		Message message1 = new Message();
			//		try
			//		{
			//			message1 = FunctionUI.SendSms(applicant.contactNumber, str);
			//		}
			//		catch (Exception exception4)
			//		{
			//		}
			//		try
			//		{
			//			SmsProcess smsProcess = message1.status.SmsProcessMakeDefaultObject(obj.applicantId, num1);
			//			(new SMSDAL()).AddUpdateSmsProcess(smsProcess);
			//		}
			//		catch (Exception exception5)
			//		{
			//		}
			//	}
			//	catch (Exception exception6)
			//	{
			//	}
			//	EmailProcess emailProcess1 = (new EmailDAL()).EmailProcessGetByApplicantAndType(obj.applicantId, num);
			//	Message message2 = new Message();
			//	try
			//	{
			//		emailProcess1.emailId = applicant.emailId;
			//		emailProcess1.isProcess = 1;
			//		message2 = emailProcess1.SendEmail();
			//	}
			//	catch (Exception exception8)
			//	{
			//		Exception exception7 = exception8;
			//		message2.status = false;
			//		message2.msg = exception7.Message;
			//	}
			//	try
			//	{
			//		emailProcess1.isSent = 0;
			//		if (message2.status)
			//		{
			//			emailProcess1.isSent = 1;
			//		}
			//		(new EmailDAL()).EmailStatusAddUpdate(emailProcess1);
			//	}
			//	catch (Exception exception9)
			//	{
			//	}
			//}
			//catch (Exception exception10)
			//{
			//}
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult AddUpdateVerficationStatus(VerificationEntity obj)
		{
			bool flag;
			obj.adminId = base.loggedInUser.userId;
			obj.comments = obj.comments.TooString("");
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			bool flag1 = true;
			try
			{
				ApplicationStatus applicationStatu = (new ApplicantDAL()).GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId, obj.applicantId, ProjConstant.Constant.ApplicationStatusType.voucherPhf).FirstOrDefault<ApplicationStatus>();
				if (applicationStatu == null)
				{
					flag = false;
				}
				else
				{
					flag = (applicationStatu.statusId == 6 ? true : applicationStatu.statusId == 9);
				}
				if (flag)
				{
					flag1 = false;
				}
			}
			catch (Exception exception)
			{
			}
			if (!flag1)
			{
				obj.approvalStatusId = 2;
				if (!obj.comments.Contains("Voucher/Payment"))
				{
					if (!string.IsNullOrWhiteSpace(obj.comments))
					{
						obj.comments = string.Concat(obj.comments, ", Voucher/Payment Unverified");
					}
					else
					{
						obj.comments = " Voucher/Payment Unverified";
					}
				}
			}
			Message message = (new VerificationDAL()).AddUpdateVerficationStatus(obj);
			Applicant applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, obj.applicantId);
			(new ApplicantDAL()).GetApplicantInfoDetail(ProjConstant.inductionId, ProjConstant.phaseId, obj.applicantId);
			string str = "";
			int num = 0;
			if (obj.approvalStatusId == 1)
			{
				num = ProjConstant.EmailTemplateType.varificationAccepted;
			}
			else if (obj.approvalStatusId == 2)
			{
				num = ProjConstant.EmailTemplateType.varificationRejected;
				try
				{
					str = (new VerificationDAL()).GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, obj.applicantId).FirstOrDefault<ApplicantApprovalStatus>((ApplicantApprovalStatus x) => x.approvalStatusTypeId == 131).dateMessage;
				}
				catch (Exception exception1)
				{
					str = "";
				}
			}
			try
			{
				EmailProcess emailProcess = new EmailProcess()
				{
					applicantId = obj.applicantId,
					keyword = "",
					typeId = num,
					adminId = base.loggedInUser.adminId
				};
				(new EmailDAL()).EmailProcessAdd(emailProcess);
			}
			catch (Exception exception2)
			{
			}
			int num1 = 0;
			if (obj.approvalStatusId == 1)
			{
				num1 = ProjConstant.SMSType.applicationApproved;
			}
			else if (obj.approvalStatusId == 2)
			{
				num1 = ProjConstant.SMSType.applicationReject;
			}
			SMS byTypeForApplicant = (new SMSDAL()).GetByTypeForApplicant(applicant.applicantId, num1);
			byTypeForApplicant.detail = byTypeForApplicant.detail.Replace("{date}", str);
			Message message1 = new Message();
			try
			{
				message1 = FunctionUI.SendSms(applicant.contactNumber, byTypeForApplicant.detail);
			}
			catch (Exception exception3)
			{
			}
			try
			{
				SmsProcess smsProcess = message1.status.SmsProcessMakeDefaultObject(obj.applicantId, byTypeForApplicant.smsId);
				(new SMSDAL()).AddUpdateSmsProcess(smsProcess);
			}
			catch (Exception exception4)
			{
			}
			EmailProcess emailProcess1 = (new EmailDAL()).EmailProcessGetByApplicantAndType(obj.applicantId, num);
			Message message2 = new Message();
			try
			{
				emailProcess1.emailId = applicant.emailId;
				emailProcess1.isProcess = 1;
				message2 = emailProcess1.SendEmail();
			}
			catch (Exception exception6)
			{
				Exception exception5 = exception6;
				message2.status = false;
				message2.msg = exception5.Message;
			}
			try
			{
				emailProcess1.isSent = 0;
				if (message2.status)
				{
					emailProcess1.isSent = 1;
				}
				(new EmailDAL()).EmailStatusAddUpdate(emailProcess1);
			}
			catch (Exception exception7)
			{
			}
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult ApplicantListForVerification()
		{
			if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.applicant)
			{
			}
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				int phaseId = AdminHelper.GetPhaseId();
				proofReadingAdminModel.applicantId = Request.QueryString["applicantid"].TooInt();
				if (proofReadingAdminModel.applicantId > 0)
				{
					proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, proofReadingAdminModel.applicantId);
				}
			}
			catch (Exception exception)
			{
				proofReadingAdminModel.applicantId = 0;
			}
			return View(proofReadingAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantListForVerificationWithDownload()
		{
			return View(new VerificationModel());
		}

		[ValidateInput(false)]
		public ActionResult ExportDataToExcelAndDownload(VerificationModel ModelSave)
		{
			Message message = new Message();
			VerificationEntity modelSave = ModelSave.verification;
			try
			{
				modelSave.statusId = modelSave.statusId.TooInt();
				string str = "ApplcantList.xlsx";
				if (modelSave.statusId != 1)
				{
					str = (modelSave.statusId != 2 ? string.Concat("All", str) : string.Concat("Rejected", str));
				}
				else
				{
					str = string.Concat("Approved", str);
				}
				modelSave.inductionId = ProjConstant.inductionId;
				modelSave.phaseId = ProjConstant.phaseId;
				string str1 = str.GenerateFilePath(base.loggedInUser);
				if (string.IsNullOrWhiteSpace(str1))
				{
					message.status = false;
					message.msg = "Error : File path and name creating.";
				}
				else
				{
					DataTable dataTable = new DataTable();
					dataTable = (new VerificationDAL()).ApplicantListVerifyExport(modelSave);
					if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
					{
						message.status = false;
						message.msg = "";
					}
					else
					{
						message = str1.ExcelFileWrite(dataTable, "Sheet1", "A1");
						str1.FileDownload();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = string.Concat("Error in exported : ", exception.Message);
			}
			if (string.IsNullOrWhiteSpace(modelSave.pageUrl))
			{
				modelSave.pageUrl = "/admin/voucher-list";
			}
			return this.Redirect(modelSave.pageUrl);
		}

		[CheckHasRight]
		public ActionResult FinanceTeam()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				int phaseId = AdminHelper.GetPhaseId();
				proofReadingAdminModel.applicantId = Request.QueryString["applicantid"].TooInt();
				if (proofReadingAdminModel.applicantId > 0)
				{
					proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, proofReadingAdminModel.applicantId);
				}
			}
			catch (Exception exception)
			{
				proofReadingAdminModel.applicantId = 0;
			}
			return View(proofReadingAdminModel);
		}

		[CheckHasRight]
		public ActionResult VerificationTeam()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
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
					Message applicantIdBySearchVerification = (new VerificationDAL()).GetApplicantIdBySearchVerification(str1, str);
					int num = applicantIdBySearchVerification.id.TooInt();
					if (num > 0)
					{
						int inductionId = AdminHelper.GetInductionId();
						proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, AdminHelper.GetPhaseId(), num);
						proofReadingAdminModel.search.key = str;
						proofReadingAdminModel.search.@value = str1;
					}
					proofReadingAdminModel.statusId = applicantIdBySearchVerification.statusId;
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

		[CheckHasRight]
		public ActionResult VerificationView()
		{
			ProofReadingAdminModel proofReadingAdminModel = new ProofReadingAdminModel();
			try
			{
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
					Message applicantIdBySearchVerification = (new VerificationDAL()).GetApplicantIdBySearchVerification(str1, str);
					int num = applicantIdBySearchVerification.id.TooInt();
					if (num > 0)
					{
						int inductionId = AdminHelper.GetInductionId();
						proofReadingAdminModel = AdminFunctions.GenerateModelProofReading(inductionId, AdminHelper.GetPhaseId(), num);
						proofReadingAdminModel.search.key = str;
						proofReadingAdminModel.search.@value = str1;
					}
					proofReadingAdminModel.statusId = applicantIdBySearchVerification.statusId;
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
	}
}