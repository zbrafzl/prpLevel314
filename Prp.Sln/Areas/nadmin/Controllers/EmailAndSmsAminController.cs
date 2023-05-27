using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class EmailAndSmsAminController : BaseAdminController
	{
		public EmailAndSmsAminController()
		{
		}

		[CheckHasRight]
		public ActionResult ApplicationAmendmentEmailSMS()
		{
			SmsEmailAdminModel smsEmailAdminModel = new SmsEmailAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				smsEmailAdminModel.search.key = str;
				smsEmailAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					smsEmailAdminModel.applicantId = 0;
					smsEmailAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						smsEmailAdminModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
						smsEmailAdminModel.search.key = str;
						smsEmailAdminModel.search.@value = str1;
					}
					smsEmailAdminModel.applicantId = num;
					smsEmailAdminModel.requestType = 1;
				}
			}
			catch (Exception exception)
			{
				smsEmailAdminModel.applicantId = 0;
				smsEmailAdminModel.requestType = 3;
			}
			return View(smsEmailAdminModel);
		}

		[CheckHasRight]
		public ActionResult EmailProcess()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			applicantStatusModel.search.applicationStatusId = Request.QueryString["statusId"].TooInt();
			if (applicantStatusModel.search.applicationStatusId == 0)
			{
				applicantStatusModel.search.applicationStatusId = 8;
			}
			applicantStatusModel.search.condition = "GetByAccountApplicationStatusId";
			return View(applicantStatusModel);
		}

		[CheckHasRight]
		public ActionResult EmailProcessCustomeSingle()
		{
			SmsEmailAdminModel smsEmailAdminModel = new SmsEmailAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				smsEmailAdminModel.search.key = str;
				smsEmailAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					smsEmailAdminModel.applicantId = 0;
					smsEmailAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						smsEmailAdminModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
						smsEmailAdminModel.search.key = str;
						smsEmailAdminModel.search.@value = str1;
					}
					smsEmailAdminModel.applicantId = num;
					smsEmailAdminModel.requestType = 1;
				}
			}
			catch (Exception exception)
			{
				smsEmailAdminModel.applicantId = 0;
				smsEmailAdminModel.requestType = 3;
			}
			return View(smsEmailAdminModel);
		}

		[CheckHasRight]
		public ActionResult EmailProcessSingle()
		{
			SmsEmailAdminModel smsEmailAdminModel = new SmsEmailAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				smsEmailAdminModel.search.key = str;
				smsEmailAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					smsEmailAdminModel.applicantId = 0;
					smsEmailAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						smsEmailAdminModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
						smsEmailAdminModel.search.key = str;
						smsEmailAdminModel.search.@value = str1;
					}
					smsEmailAdminModel.applicantId = num;
					smsEmailAdminModel.requestType = 1;
				}
			}
			catch (Exception exception)
			{
				smsEmailAdminModel.applicantId = 0;
				smsEmailAdminModel.requestType = 3;
			}
			return View(smsEmailAdminModel);
		}

		public ActionResult EmailProcessStart()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin();
			try
			{
				string str = "";
				List<EmailProcess> list = (new EmailDAL()).EmailProcessGetAllRemaninig().ToList<EmailProcess>();
				if ((list == null ? false : list.Count > 0))
				{
					foreach (EmailProcess emailProcess in list)
					{
						try
						{
							if (emailProcess.SendEmail().status)
							{
								sMSModelAdmin.totalSent = sMSModelAdmin.totalSent + 1;
								int num = emailProcess.emailProcessId;
								str = string.Concat(str, num.ToString(), ",");
							}
						}
						catch (Exception exception)
						{
						}
					}
					str = str.TrimEnd(new char[] { ',' });
					(new EmailDAL()).EmailStatusUpdateByProcessIds(str);
				}
			}
			catch (Exception exception1)
			{
			}
			return View(sMSModelAdmin);
		}

		[CheckHasRight]
		public ActionResult EmailProcessView()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.emailTemplate)
					orderby x.id
					select x).ToList<Constant>()
			};
			return View(sMSModelAdmin);
		}

		public ActionResult EmailSMSAmendmentAlert()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			DataTable applicationHasAmedmentAndNotSentEmail = (new VerificationDAL()).GetApplicationHasAmedmentAndNotSentEmail();
			int num = 0;
			if ((applicationHasAmedmentAndNotSentEmail == null ? false : applicationHasAmedmentAndNotSentEmail.Rows.Count > 0))
			{
				foreach (DataRow row in applicationHasAmedmentAndNotSentEmail.Rows)
				{
					Message message = new Message();
					try
					{
						string str = "";
						string str1 = "";
						num = row["applicantId"].TooInt();
						Applicant applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
						if ((applicant == null || applicant.applicantId <= 0 ? true : applicant.applicationStatusId != 8))
						{
							message.status = false;
							message.msg = "Applicant not exist";
						}
						else
						{
							ApplicationVerificationStatus applicationAmendmentsStatus = (new VerificationDAL()).GetApplicationAmendmentsStatus(num);
							if (applicationAmendmentsStatus.statusId == 1)
							{
								str = string.Concat(new string[] { "Dear ", applicant.name, "( PMDC No: ", applicant.pmdcNo, ", EmailId: ", applicant.emailId, " ). It is hereby informed that your Form for the PG Induction Aug-2020 has been approved by the Grievances Committee after due scrutiny. Regards PRP Support Team." });
								FunctionUI.SendSms(applicant.contactNumber, str);
								try
								{
									string str2 = ProjConstant.Email.Path.amendmentApprove.GetServerPathFolder().ReadFile();
									str1 = str2.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo).Replace("{#emailId#}", applicant.emailId);
									Message message1 = this.SendAmendmentEmail(applicant, str1, 41);
									message.status = message1.status;
									message.msg = message1.msg;
								}
								catch (Exception exception)
								{
									str1 = "";
								}
							}
							else if (applicationAmendmentsStatus.statusId != 2)
							{
								message.status = false;
								message.msg = "No Amendment.";
							}
							else
							{
								str = string.Concat(new string[] { "Dear ", applicant.name, "( PMDC No: ", applicant.pmdcNo, ", EmailId: ", applicant.emailId, " ). It is hereby informed that your Form for the PG Induction Aug-2020 has been rejected by the Grievances Committee after due scrutiny.  Regards PRP Support Team." });
								FunctionUI.SendSms(applicant.contactNumber, str);
								try
								{
									string str3 = ProjConstant.Email.Path.amendmentReject.GetServerPathFolder().ReadFile();
									str1 = str3.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo).Replace("{#emailId#}", applicant.emailId);
									Message message2 = this.SendAmendmentEmail(applicant, str1, 46);
									message.status = message2.status;
									message.msg = message2.msg;
								}
								catch (Exception exception1)
								{
									str1 = "";
								}
							}
						}
					}
					catch (Exception exception2)
					{
						message.status = false;
					}
				}
			}
			return View(applicantStatusModel);
		}

		public ActionResult EmailSMSBulkSendingToRemaning()
		{
			return View(new ApplicantStatusModel());
		}

		public ActionResult EmailSMSGazetteMarksAlert()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			DataTable emailStatusRemaningRecords = (new EmailSMSDAL()).GetEmailStatusRemaningRecords("GazetteRemaningApplicant");
			int num = 0;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			if ((emailStatusRemaningRecords == null ? false : emailStatusRemaningRecords.Rows.Count > 0))
			{
				string str4 = "";
				string str5 = ProjConstant.Email.Path.grievancesGazette.GetServerPathFolder().ReadFile();
				foreach (DataRow row in emailStatusRemaningRecords.Rows)
				{
					Message message = new Message();
					try
					{
						num = row["applicantId"].TooInt();
						str = row["name"].TooString("");
						str1 = row["pmdcNo"].TooString("");
						str2 = row["emailId"].TooString("");
						str3 = row["contactNumber"].TooString("");
						FunctionUI.SendSms(str3, string.Concat(new string[] { "Dear ", str, "( PMDC No: ", str1, ", EmailId: ", str2, " ). Grievances Committee Meeting will held: 28to30/09/2020 at KEMU. If any issue please appear. Check prp.phf.gop.pk for details.. Regards PRP Support Team." }));
						try
						{
							str4 = str5.Replace("{#name#}", str).Replace("{#pmdcNo#}", str1).Replace("{#emailId#}", str2);
							Message message1 = this.SendAndSaveEmailStatus(num, str2, str4, ProjConstant.Email.Subject.grievancesGazette, 111);
							message.status = message1.status;
							message.msg = message1.msg;
						}
						catch (Exception exception)
						{
							str4 = "";
						}
					}
					catch (Exception exception1)
					{
						message.status = false;
					}
				}
			}
			return View(applicantStatusModel);
		}

		public ActionResult EmailSMSJoiningAlert()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			DataTable emailStatusRemaningRecords = (new EmailSMSDAL()).GetEmailStatusRemaningRecords("JoningRemaningApplicant");
			int num = 0;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			if ((emailStatusRemaningRecords == null ? false : emailStatusRemaningRecords.Rows.Count > 0))
			{
				string str4 = "";
				string str5 = "";
				string str6 = ProjConstant.Email.Path.joiningApplicant.GetServerPathFolder().ReadFile();
				foreach (DataRow row in emailStatusRemaningRecords.Rows)
				{
					Message message = new Message();
					try
					{
						string str7 = "/html/sms/joining.html".GetServerPathFolder().ReadFile();
						num = row["applicantId"].TooInt();
						str = row["name"].TooString("");
						str1 = row["pmdcNo"].TooString("");
						str2 = row["emailId"].TooString("");
						str3 = row["contactNumber"].TooString("");
						str4 = string.Concat("Dear,PMDC No:", str1, ".", str7);
						FunctionUI.SendSms(str3, str4);
						try
						{
							string byApplicantDetailById = (new JoiningDAL()).GetByApplicantDetailById(num).instituteName;
							str5 = str6.Replace("{#name#}", str).Replace("{#pmdcNo#}", str1).Replace("{#emailId#}", str2).Replace("{#hospitalName#}", byApplicantDetailById);
							Message message1 = this.SendAndSaveEmailStatus(num, str2, str5, ProjConstant.Email.Subject.joining, 131);
							message.status = message1.status;
							message.msg = message1.msg;
						}
						catch (Exception exception)
						{
							str5 = "";
						}
					}
					catch (Exception exception1)
					{
						message.status = false;
					}
				}
			}
			return View(applicantStatusModel);
		}

		public ActionResult EmailSMSMeritMarksAlert()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			DataTable emailStatusRemaningRecords = (new EmailSMSDAL()).GetEmailStatusRemaningRecords("MeritListRemaningApplicantRound03");
			int num = 0;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			if ((emailStatusRemaningRecords == null ? false : emailStatusRemaningRecords.Rows.Count > 0))
			{
				string str4 = "";
				string str5 = "";
				string str6 = ProjConstant.Email.Path.meritListConsent.GetServerPathFolder().ReadFile();
				foreach (DataRow row in emailStatusRemaningRecords.Rows)
				{
					Message message = new Message();
					try
					{
						num = row["applicantId"].TooInt();
						str = row["name"].TooString("");
						str1 = row["pmdcNo"].TooString("");
						str2 = row["emailId"].TooString("");
						str3 = row["contactNumber"].TooString("");
						str4 = string.Concat("Dear,PMDC No:", str1, ". Please ensure consent before closing hours (23-10-2020 at 08PM).");
						FunctionUI.SendSms(str3, str4);
						try
						{
							str5 = str6.Replace("{#name#}", str).Replace("{#pmdcNo#}", str1).Replace("{#emailId#}", str2);
							Message message1 = this.SendAndSaveEmailStatus(num, str2, str5, ProjConstant.Email.Subject.meritList, 123);
							message.status = message1.status;
							message.msg = message1.msg;
						}
						catch (Exception exception)
						{
							str5 = "";
						}
					}
					catch (Exception exception1)
					{
						message.status = false;
					}
				}
			}
			return View(applicantStatusModel);
		}

		public ActionResult EmailSMSSelectedCongratulationAlert()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			DataTable emailStatusRemaningRecords = (new EmailSMSDAL()).GetEmailStatusRemaningRecords("CongratulationRemaningApplicant");
			int num = 0;
			string str = "";
			string str1 = "";
			string str2 = "";
			string str3 = "";
			if ((emailStatusRemaningRecords == null ? false : emailStatusRemaningRecords.Rows.Count > 0))
			{
				string str4 = "";
				string str5 = "";
				string str6 = ProjConstant.Email.Path.congratulationApplicant.GetServerPathFolder().ReadFile();
				foreach (DataRow row in emailStatusRemaningRecords.Rows)
				{
					Message message = new Message();
					try
					{
						string str7 = "/html/sms/congratulationSMS.html".GetServerPathFolder().ReadFile();
						num = row["applicantId"].TooInt();
						str = row["name"].TooString("");
						str1 = row["pmdcNo"].TooString("");
						str2 = row["emailId"].TooString("");
						str3 = row["contactNumber"].TooString("");
						str4 = string.Concat("Dear,PMDC No:", str1, ".", str7);
						FunctionUI.SendSms(str3, str4);
						try
						{
							string byApplicantDetailById = (new JoiningDAL()).GetByApplicantDetailById(num).instituteName;
							str5 = str6.Replace("{#name#}", str).Replace("{#pmdcNo#}", str1).Replace("{#emailId#}", str2).Replace("{#hospitalName#}", byApplicantDetailById);
							Message message1 = this.SendAndSaveEmailStatus(num, str2, str5, ProjConstant.Email.Subject.congratulation, 151);
							message.status = message1.status;
							message.msg = message1.msg;
						}
						catch (Exception exception)
						{
							str5 = "";
						}
					}
					catch (Exception exception1)
					{
						message.status = false;
					}
				}
			}
			return View(applicantStatusModel);
		}

		[HttpGet]
		public JsonResult GetSMSAllByType(int typeId)
		{
			List<SMS> allByType = (new SMSDAL()).GetAllByType(ProjConstant.inductionId, typeId);
			return base.Json(allByType, 0);
		}

		[ValidateInput(false)]
		public ActionResult SaveEmailTemplateData(EmailTemplateModel ModelSave, HttpPostedFileBase files)
		{
			EmailTemplate modelSave = ModelSave.template;
			modelSave.typeId = modelSave.typeId.TooInt();
			modelSave.body = modelSave.body.TooString("");
			modelSave.adminId = base.loggedInUser.userId;
			modelSave.inductionId = ProjConstant.inductionId;
			(new EmailDAL()).EmailTemplateAddUpdate(modelSave);
			return this.Redirect(ModelSave.redirectUrl);
		}

		private Message SendAmendmentEmail(Applicant applicant, string emailBody, int emailTypeId)
		{
			Message message = new Message();
			try
			{
				message = applicant.emailId.SendEmail(ProjConstant.Email.Subject.verification, ProjConstant.Email.Title.verification, emailBody);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			try
			{
				StatusEmail statusEmail = new StatusEmail()
				{
					emailStatusId = 0,
					applicantId = applicant.applicantId,
					typeId = emailTypeId,
					body = emailBody,
					sechduleDate = DateTime.Now,
					adminId = base.loggedInUser.userId
				};
				if (!message.status)
				{
					statusEmail.statusMessage = message.msg;
					statusEmail.statusId = 2;
				}
				else
				{
					statusEmail.statusId = 1;
					statusEmail.statusMessage = message.msg;
				}
				statusEmail.statusMessage = statusEmail.statusMessage.TooString("");
				(new EmailDAL()).EmailStatusAddUpdate(statusEmail);
			}
			catch (Exception exception2)
			{
			}
			return message;
		}

		private Message SendAndSaveEmailStatus(int applicantId, string emailId, string emailBody, string emailSubject, int emailTypeId)
		{
			Message message = new Message();
			try
			{
				message = emailId.SendEmail(emailSubject, ProjConstant.Email.Title.verification, emailBody);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			try
			{
				StatusEmail statusEmail = new StatusEmail()
				{
					emailStatusId = 0,
					applicantId = applicantId,
					typeId = emailTypeId,
					body = emailBody,
					sechduleDate = DateTime.Now,
					adminId = base.loggedInUser.userId
				};
				if (!message.status)
				{
					statusEmail.statusMessage = message.msg;
					statusEmail.statusId = 2;
				}
				else
				{
					statusEmail.statusId = 1;
					statusEmail.statusMessage = message.msg;
				}
				statusEmail.statusMessage = statusEmail.statusMessage.TooString("");
				(new EmailDAL()).EmailStatusAddUpdate(statusEmail);
			}
			catch (Exception exception2)
			{
			}
			return message;
		}

		[HttpGet]
		public JsonResult SendEmailByType(int typeId)
		{
			Message message = new Message();
			int count = 0;
			int num = 0;
			int num1 = 0;
			try
			{
				List<EmailProcess> list = (new EmailDAL()).EmailProcessGetByType(typeId).ToList<EmailProcess>();
				if ((list == null ? false : list.Count > 0))
				{
					count = list.Count;
					foreach (EmailProcess emailProcess in list)
					{
						try
						{
							emailProcess.isProcess = 1;
							Message message1 = emailProcess.SendEmail();
							emailProcess.isSent = 0;
							if (!message1.status)
							{
								num1++;
							}
							else
							{
								emailProcess.isSent = 1;
								num++;
								(new EmailDAL()).EmailStatusAddUpdate(emailProcess);
							}
						}
						catch (Exception exception)
						{
						}
					}
					message.id = list.Count;
				}
			}
			catch (Exception exception1)
			{
				message.id = 0;
			}
			message.message = string.Concat(new string[] { "Total Fetch : ", count.ToString(), " Email Sent : ", num.ToString(), " Email Failed: ", num1.ToString() });
			return base.Json(message, 0);
		}

		[HttpPost]
		public void SendEmailToApplicantSingle(int applicantId)
		{
			Message message = new Message();
			string str = ProjConstant.Email.Path.registration;
			string str1 = Path.Combine(Server.MapPath(str));
			message.id = message.id + 10011;
			string str2 = message.id.TooString("").Encrypt();
			str1 = str1.Replace("{#name#}", "").Replace("{#key#}", str2);
			try
			{
			}
			catch (Exception exception)
			{
			}
		}

		[HttpGet]
		public JsonResult SendSMSByTypeAndSms(int typeId, int smsId)
		{
			Message message = new Message();
			int count = 0;
			int num = 0;
			int num1 = 0;
			try
			{
				List<SmsProcess> list = (new SMSDAL()).SMSProcessGetBySmsId(smsId, 0).Take<SmsProcess>(50).ToList<SmsProcess>();
				if ((list == null ? false : list.Count > 0))
				{
					count = list.Count;
					foreach (SmsProcess smsProcess in list)
					{
						Message message1 = FunctionUI.SendSms(smsProcess.contactNumber, smsProcess.detail);
						try
						{
							SmsProcess smsProcess1 = message1.status.SmsProcessMakeDefaultObject(smsProcess.applicantId, smsProcess.smsId);
							smsProcess1.smsProcessId = smsProcess.smsProcessId;
							(new SMSDAL()).AddUpdateSmsProcess(smsProcess1);
							if (!message1.status)
							{
								num1++;
							}
							else
							{
								num++;
							}
						}
						catch (Exception exception)
						{
						}
					}
					message.id = list.Count;
				}
			}
			catch (Exception exception1)
			{
				message.id = 0;
			}
			message.message = string.Concat(new string[] { "Total Fetch : ", count.ToString(), " SMS Sent : ", num.ToString(), " SMS Failed: ", num1.ToString() });
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult SMSProcess()
		{
			ApplicantStatusModel applicantStatusModel = new ApplicantStatusModel();
			applicantStatusModel.search.applicationStatusId = Request.QueryString["statusId"].TooInt();
			if (applicantStatusModel.search.applicationStatusId == 0)
			{
				applicantStatusModel.search.applicationStatusId = 8;
			}
			applicantStatusModel.search.condition = "GetByAccountApplicationStatusId";
			return View(applicantStatusModel);
		}

		[HttpGet]
		public JsonResult SMSProcessCreateListBySmsId(int typeId, int smsId)
		{
			Message message = (new SMSDAL()).SMSProcessCreateListBySmsId(typeId, smsId);
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult SMSProcessQuery()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.smsType)
					orderby x.id
					select x).ToList<Constant>()
			};
			return View(sMSModelAdmin);
		}

		[CheckHasRight]
		public ActionResult SMSProcessSingle()
		{
			SmsEmailAdminModel smsEmailAdminModel = new SmsEmailAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				smsEmailAdminModel.search.key = str;
				smsEmailAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					smsEmailAdminModel.applicantId = 0;
					smsEmailAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						smsEmailAdminModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num);
						smsEmailAdminModel.search.key = str;
						smsEmailAdminModel.search.@value = str1;
					}
					smsEmailAdminModel.applicantId = num;
					smsEmailAdminModel.requestType = 1;
				}
			}
			catch (Exception exception)
			{
				smsEmailAdminModel.applicantId = 0;
				smsEmailAdminModel.requestType = 3;
			}
			return View(smsEmailAdminModel);
		}

		[CheckHasRight]
		public ActionResult SMSProcessView()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.smsType)
					orderby x.id
					select x).ToList<Constant>()
			};
			return View(sMSModelAdmin);
		}

		[CheckHasRight]
		public ActionResult TemplateManageEmail()
		{
			EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
			emailTemplateModel.template.inductionId = ProjConstant.inductionId;
			emailTemplateModel.template.search = "";
			emailTemplateModel.listTemplate = (new EmailDAL()).EmailTemplateSearch(emailTemplateModel.template);
			return View(emailTemplateModel);
		}

		[CheckHasRight]
		public ActionResult TemplateManageSMS()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.smsType)
					orderby x.id
					select x).ToList<Constant>()
			};
			return View(sMSModelAdmin);
		}

		[CheckHasRight]
		public ActionResult TemplateSetupEmail()
		{
			bool flag;
			EmailTemplateModel emailTemplateModel = new EmailTemplateModel()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.emailTemplate)
					orderby x.id
					select x).ToList<Constant>(),
				typeId = Request.QueryString["typeId"].TooInt()
			};
			if (emailTemplateModel.typeId != 0)
			{
				flag = false;
			}
			else
			{
				flag = (emailTemplateModel.listType == null ? false : emailTemplateModel.listType.Count > 0);
			}
			if (flag)
			{
				emailTemplateModel.typeId = emailTemplateModel.listType.FirstOrDefault<Constant>().id.TooInt();
			}
			if (emailTemplateModel.typeId <= 0)
			{
				emailTemplateModel.template.isActive = true;
			}
			else
			{
				emailTemplateModel.template = (new EmailDAL()).EmailTemplateByTypeId(emailTemplateModel.typeId, 0);
			}
			return View(emailTemplateModel);
		}
	}
}