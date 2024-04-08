using Newtonsoft.Json;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static Prp.Sln.ProjConstant;

namespace Prp.Sln.Controllers
{
	public class LoggedInController : BaseLoginController
	{
		public LoggedInController()
		{
		}

		[HttpPost]
		public JsonResult ApplicantForgotPassword(Applicant obj)
		{
			Message message = new Message();
			try
			{
				obj.emailId = obj.emailId.TooString("");
				Applicant applicantByEmail = (new ApplicantDAL()).GetApplicantByEmail(obj.emailId);
				if ((applicantByEmail == null ? true : applicantByEmail.applicantId <= 0))
				{
					message.id = 0;
					message.status = false;
					message.message = "Information not exists against this email. Plese sign up";
				}
				else if (applicantByEmail.statusId != 0)
				{
					string str = ProjConstant.Email.Path.forgotPassword;
					string str1 = Path.Combine(Server.MapPath(str));
					str1 = str1.ReadFile();
					str1 = str1.Replace("{#name#}", applicantByEmail.name).Replace("{#password#}", applicantByEmail.passwordDecrypt);
					applicantByEmail.emailId.SendEmail(ProjConstant.Email.Subject.forgotPassword, ProjConstant.Email.Title.forgotPassword, str1);
					message.id = 0;
					message.status = true;
					message.message = "Password  sent to your email.";
				}
				else
				{
					string str2 = ProjConstant.Email.Path.registration;
					string str3 = Path.Combine(Server.MapPath(str2));
					str3 = str3.ReadFile();
					int num = applicantByEmail.applicantId + 10011;
					string str4 = num.TooString("").Encrypt();
					str3 = str3.Replace("{#name#}", applicantByEmail.name).Replace("{#key#}", str4);
					applicantByEmail.emailId.SendEmail(ProjConstant.Email.Subject.registration, ProjConstant.Email.Title.registration, str3);
					message.id = 0;
					message.status = true;
					message.message = "Account is unverified. Please check your email and verify account.";
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.id = 0;
				message.status = false;
				message.msg = exception.Message;
			}
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantIsExist(IsExists obj)
		{
			Message message = (new ApplicantDAL()).ApplicantIsExist(obj);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantRegistration(Applicant obj)
		{
			Message message = new Message();
			int num = 0;
			try
			{
				obj.name = obj.name.TooString("");
				obj.pmdcNo = obj.pmdcNo.TooString("");
				obj.emailId = obj.emailId.TooString("");
				obj.password = obj.password.TooString("");
				obj.contactNumber = obj.contactNumber.TooString("");
				obj.pic = obj.pic.TooString("");
				message = (new ApplicantDAL()).Registration(obj);
				if (message.status)
				{
					num = message.id;

                    #region SMS
      //              SMSResp objSms = new SMSResp();
      //              try
      //              {
      //                  SmsProcess objs = new SmsProcess();
      //                  objs.applicantId = message.id;
      //                  objs.smsId = SMSType.registration;
      //                  objSms = new SMSDAL().SMSProcessGetInfoByType(objs);
						//if (objSms.status)
						//{
						//	Message msgSms = FunctionUI.SendSms(objSms.contactNumber, objSms.body);
						//	objSms.resp = msgSms.msg;
						//	objSms.isProcess = 1;
						//	objSms.isSent = 0;
						//	if (msgSms.status == true)
						//		objSms.isSent = 1;
						//	new SMSDAL().SMSProcessAddUpdate(objSms);
						//}
      //              }
      //              catch (Exception ex)
      //              {
      //              }
                    #endregion

                    #region Email
                    FunctionUI.SendActivationEmail(message.id);
                    #endregion

                }
			}
			catch (Exception exception5)
			{
				Exception exception4 = exception5;
				message.id = 0;
				message.status = false;
				message.msg = exception4.Message;
			}
			return base.Json(message, 0);
		}

		public ActionResult CommingSoon()
		{
			ActionResult actionResult;
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if ((applicant == null ? true : applicant.applicantId <= 0))
			{
				actionResult = base.View(new RegistrationModel());
			}
			else
			{
				actionResult = this.Redirect("/index");
			}
			return actionResult;
		}

		public ActionResult ConactUs()
		{
			ContactModel contactModel = new ContactModel();
			contactModel.contact.typeId = 1;
            contactModel.projId= Request.QueryString["pid"].TooInt();
            contactModel.contact.projId = Request.QueryString["pid"].TooInt();

            contactModel.listProj = new ConstantDAL().GetAll(1001);

            ViewBag.message = TempData["Message"];
			return View(contactModel);
		}

		[HttpPost]
		public ActionResult ContactUsSave(ContactModel model, IEnumerable<HttpPostedFileBase> flImages)
		{
			string absoluteUri = Request.Url.AbsoluteUri;
			Message message = new Message();
			Message message1 = new Message();
			int num = 0;
			try
			{
				num = base.loggedInUser.applicantId;
			}
			catch (Exception exception)
			{
				num = 0;
			}
			try
			{
				Contact contact = model.contact;
				contact.applicantId = num;
				message = (new ContactDAL()).AddUpdate(contact);
				string str = "";
				try
				{
					if ((flImages == null || flImages.Count<HttpPostedFileBase>() <= 0 ? false : flImages.ToList<HttpPostedFileBase>()[0] != null))
					{
						str = Files.SaveMultipleFiles(flImages, "/images/contact/");
					}
				}
				catch (Exception exception1)
				{
					str = "";
				}
				str = str.TrimEnd(new char[] { ',' });
				if (!string.IsNullOrWhiteSpace(str))
				{
					message1 = (new ContactDAL()).AddUpdateDocs(message.id, str, 1);
				}
				//if (message.id > 0)
				//{
				//	EmailFunctions.SendEmailQuestion(contact);
				//}
			}
			catch (Exception exception3)
			{
				Exception exception2 = exception3;
				message.id = 0;
				message.status = false;
				message.msg = exception2.Message;
			}
			TempData["Message"] = message;
			return this.Redirect("/contactus");
		}

		public ActionResult ForgotPassword()
		{
			ActionResult actionResult;
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if ((applicant == null ? true : applicant.applicantId <= 0))
			{
				actionResult = base.View(new RegistrationModel());
			}
			else
			{
				actionResult = this.Redirect("/index");
			}
			return actionResult;
		}

		public ActionResult GalleryLayout()
		{
			return View(new LoginModel());
		}

		public ActionResult Index()
		{
			ActionResult actionResult;
			HomeModel homeModel = new HomeModel();
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if ((applicant == null ? true : applicant.applicantId <= 0))
			{
				actionResult = base.View(homeModel);
			}
			else
			{
				actionResult = this.Redirect("/home");
			}
			return actionResult;
		}

		[HttpPost]
		public JsonResult LoggedInUser(LoginUser login)
		{
			Message message = new Message();
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if (applicant != null)
			{
				message.id = applicant.applicantId;
				message.status = true;
			}
			else
			{
				try
				{
					applicant = (new ApplicantDAL()).Login(login.emailId, login.password);
					if ((applicant == null ? true : applicant.applicantId <= 0))
					{
						message.id = applicant.applicantId;
						message.status = false;
						message.message = applicant.emailId;
					}
					else
					{
						ProjFunctions.CookieApplicantSet(applicant);
						message.id = applicant.applicantId;
						message.status = true;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					message.id = 0;
					message.status = false;
					message.msg = exception.Message;
				}
			}
			return base.Json(message, 0);
		}

		public ActionResult Login()
		{
			ActionResult actionResult;
			LoginModel loginModel = new LoginModel();
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if ((applicant == null ? true : applicant.applicantId <= 0))
			{
				actionResult = base.View(loginModel);
			}
			else
			{
				actionResult = this.Redirect("/home");
			}
			return actionResult;
		}

		public ActionResult Logout()
		{
			try
			{
				ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
			}
			catch (Exception exception)
			{
			}
			return this.Redirect("/");
		}

		public ActionResult Register()
		{
			ActionResult actionResult;
			Applicant applicant = ProjFunctions.CookieApplicantGet();
			if ((applicant == null ? true : applicant.applicantId <= 0))
			{
				actionResult = base.View(new RegistrationModel());
			}
			else
			{
				actionResult = this.Redirect("/index");
			}
			return actionResult;
		}

		public ActionResult RegisterComplete()
		{
			return View(new LoginModel());
		}

		public ActionResult RegisterVerify()
		{
			LoginModel loginModel = new LoginModel();
			try
			{
				loginModel.applicationId = 0;
				string str = Request.QueryString["key"].TooString("");
				int num = str.Decrypt().TooInt();
				int num1 = 0;
				if (num > 0)
				{
					num1 = num - 10011;
				}
				if (num1 > 0)
				{
					Applicant applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, num1);
					if ((applicant == null ? true : applicant.applicantId <= 0))
					{
						loginModel.applicationId = 0;
					}
					else if (!(new ApplicantDAL()).AccountStatusUpdate(num1, 1, 0).status)
					{
						loginModel.applicationId = 0;
					}
					else
					{
						loginModel.applicationId = num1;
					}
				}
				loginModel.msg.message = "Registration completed";
				loginModel.msg.status = true;
			}
			catch (Exception exception)
			{
				loginModel.msg.message = exception.Message;
				loginModel.msg.status = false;
			}
			return View(loginModel);
		}

		public static CaptchaResponse ValidateCaptcha(string response)
		{
			string item = WebConfigurationManager.AppSettings["recaptchaKeySecret"];
			WebClient webClient = new WebClient();
			string str = webClient.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", item, response));
			return JsonConvert.DeserializeObject<CaptchaResponse>(str.ToString());
		}



        #region MyRegion

       

        public ActionResult LoginHs()
        {
            ActionResult actionResult;
            LoginModel loginModel = new LoginModel();
            Applicant applicant = ProjFunctions.CookieApplicantGetHs();
            if ((applicant == null ? true : applicant.applicantId <= 0))
            {
                actionResult = base.View(loginModel);
            }
            else
            {
                actionResult = this.Redirect("/hs/apply");
            }
            return actionResult;
        }

        

        public ActionResult LogoutHs()
        {
            try
            {
                ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicantHs);
            }
            catch (Exception exception)
            {
            }
            return this.Redirect("/hs");
        }
        #endregion
    }
}