using Prp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Prp.Data.DAL;
using Newtonsoft.Json;

namespace Prp.Sln.Controllers
{
    public class LoggedInController : BaseLoginController
    {
        #region Index/Landing Page

        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app != null && app.applicantId > 0)
            {
                return Redirect("/home");
            }
            else
            {
            }

            return View(model);
        }

        #endregion

        #region Login

        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app != null && app.applicantId > 0)
            {
                return Redirect("/home");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult LoggedInUser(LoginUser login)
        {
            Message msg = new Message();


            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app == null)
            {
                try
                {
                    app = new ApplicantDAL().Login(login.emailId, login.password);

                    if (app != null && app.applicantId > 0)
                    {
                        ProjFunctions.CookieApplicantSet(app);
                        msg.id = app.applicantId;
                        msg.status = true;
                    }
                    else
                    {
                        msg.id = app.applicantId;
                        msg.status = false;
                        msg.message = app.emailId;
                    }
                }
                catch (Exception ex)
                {
                    msg.id = 0;
                    msg.status = false;
                    msg.msg = ex.Message;
                }
            }
            else
            {
                msg.id = app.applicantId;
                msg.status = true;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Registration


        public ActionResult CommingSoon()
        {
            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app != null && app.applicantId > 0)
            {
                return Redirect("/index");
            }

            RegistrationModel model = new RegistrationModel();
            return View(model);
        }


        public ActionResult Register()
        {
            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app != null && app.applicantId > 0)
            {
                return Redirect("/index");
            }

            RegistrationModel model = new RegistrationModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult ApplicantIsExist(IsExists obj)
        {
            Message msg = new ApplicantDAL().ApplicantIsExist(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ApplicantRegistration(Applicant obj)
        {
            Message msg = new Message();
            int applicantId = 0;
            try
            {

                obj.name = obj.name.TooString();
                obj.pmdcNo = obj.pmdcNo.TooString();
                obj.emailId = obj.emailId.TooString();
                obj.password = obj.password.TooString();
                obj.contactNumber = obj.contactNumber.TooString();
                obj.pic = obj.pic.TooString();
                msg = new ApplicantDAL().Registration(obj);
                if (msg.status)
                {
                    applicantId = msg.id;
                    string path = ProjConstant.Email.Path.registration;

                    string filePath = Path.Combine(Server.MapPath(path));
                    string body = filePath.ReadFile();

                    msg.id = msg.id + 10011;

                    string key = msg.id.TooString().Encrypt();

                    body = body.Replace("{#name#}", obj.name).Replace("{#key#}", key);

                    try
                    {

                        obj.emailId.SendEmail(ProjConstant.Email.Subject.registration
                      , ProjConstant.Email.Title.registration, body);
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        string smsBody = "";
                        int smsId = 0;
                        try
                        {
                            SMS sms = new SMSDAL().GetByTypeForApplicant(applicantId, ProjConstant.SMSType.registration);
                            smsBody = sms.detail;
                            smsId = sms.smsId;
                        }
                        catch (Exception)
                        {
                            smsBody = "";
                        }
                        if (String.IsNullOrWhiteSpace(smsBody))
                        {
                            smsId = 0;
                            smsBody = "Dear Candidate, You have successfully registered in Induction January 2023(phase 1). For detail please check your email.";
                        }


                        Message msgSms = FunctionUI.SendSms(obj.contactNumber, smsBody);

                        try
                        {
                            SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(applicantId, smsId);
                            new SMSDAL().AddUpdateSmsProcess(objProcess);
                        }
                        catch (Exception)
                        {
                        }


                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterComplete()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        public ActionResult RegisterVerify()
        {
            LoginModel model = new LoginModel();
            try
            {
                model.applicationId = 0;
                string key = Request.QueryString["key"].TooString();
                string decrypt = key.Decrypt();
                int decryptInt = decrypt.TooInt();

                int applicantId = 0;
                if (decryptInt > 0)
                {
                    applicantId = decryptInt - 10011;
                }
                if (applicantId > 0)
                {
                    Applicant app = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                    if (app != null && app.applicantId > 0)
                    {
                        Message msg = new ApplicantDAL().AccountStatusUpdate(applicantId, 1, 0);
                        if (msg.status)
                        {
                            model.applicationId = applicantId;
                        }
                        else
                            model.applicationId = 0;
                    }
                    else
                        model.applicationId = 0;
                }
                model.msg.message = "Registration completed";
                model.msg.status = true;
            }
            catch (Exception ex)
            {
                model.msg.message = ex.Message;
                model.msg.status = false;
            }

            return View(model);
        }


        #endregion

        public ActionResult GalleryLayout()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        #region Forgot Password

        public ActionResult ForgotPassword()
        {
            Applicant app = ProjFunctions.CookieApplicantGet();
            if (app != null && app.applicantId > 0)
            {
                return Redirect("/index");
            }

            RegistrationModel model = new RegistrationModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult ApplicantForgotPassword(Applicant obj)
        {
            Message msg = new Message();
            try
            {

                obj.emailId = obj.emailId.TooString();
                Applicant applicant = new ApplicantDAL().GetApplicantByEmail(obj.emailId);
                if (applicant != null && applicant.applicantId > 0)
                {

                    if (applicant.statusId == 0)
                    {
                        string path = ProjConstant.Email.Path.registration;
                        string filePath = Path.Combine(Server.MapPath(path));
                        string body = filePath.ReadFile();

                        var appId = applicant.applicantId + 10011;

                        string key = appId.TooString().Encrypt();

                        body = body.Replace("{#name#}", applicant.name).Replace("{#key#}", key);

                        applicant.emailId.SendEmail(ProjConstant.Email.Subject.registration
                            , ProjConstant.Email.Title.registration, body);

                        msg.id = 0;
                        msg.status = true;
                        msg.message = "Account is unverified. Please check your email and verify account.";
                    }
                    else
                    {

                        #region Email

                        string path = ProjConstant.Email.Path.forgotPassword;
                        string filePath = Path.Combine(Server.MapPath(path));
                        string body = filePath.ReadFile();
                        body = body.Replace("{#name#}", applicant.name).Replace("{#password#}", applicant.passwordDecrypt);

                        applicant.emailId.SendEmail(ProjConstant.Email.Subject.forgotPassword
                            , ProjConstant.Email.Title.forgotPassword, body);

                        msg.id = 0;
                        msg.status = true;
                        msg.message = "Password  sent to your email.";

                        #endregion
                    }

                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                    msg.message = "Information not exists against this email. Plese sign up";
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Logout

        public ActionResult Logout()
        {
            try
            {
                ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
            }
            catch (Exception)
            {
            }

            return Redirect("/");

        }

        #endregion


        #region ConactUs
        public ActionResult ConactUs()
        {
            ContactModel model = new ContactModel();
            model.contact.typeId = 1;

            ViewBag.message = TempData["Message"];

            return View(model);
        }

        [HttpPost]
        public ActionResult ContactUsSave(ContactModel model, IEnumerable<HttpPostedFileBase> flImages)
        {
            string url = Request.Url.AbsoluteUri;

            Message mssgg = new Message();
            Message msg = new Message();
            int applicantId = 0;
            try
            {
                applicantId = loggedInUser.applicantId;
            }
            catch (Exception)
            {

                applicantId = 0;
            }

            try
            {
                Contact contact = model.contact;
                contact.applicantId = applicantId;
                mssgg = new ContactDAL().AddUpdate(contact);


                string images = "";
                try
                {
                    if (flImages != null && flImages.Count() > 0 && flImages.ToList()[0] != null)
                    {
                        images = Files.SaveMultipleFiles(flImages, "/images/contact/");
                    }
                }
                catch (Exception ex)
                {
                    images = "";
                }

                images = images.TrimEnd(',');

                if (!String.IsNullOrWhiteSpace(images))
                {
                    msg = new ContactDAL().AddUpdateDocs(mssgg.id, images, 1);
                }

                if (mssgg.id > 0)
                    EmailFunctions.SendEmailQuestion(contact);

            }
            catch (Exception ex)
            {
                mssgg.id = 0;
                mssgg.status = false;
                mssgg.msg = ex.Message;
            }

            TempData["Message"] = mssgg;

            return Redirect("/contactus");
        }

        //https://www.c-sharpcorner.com/article/integrate-google-recaptcha-and-validate-it-with-asp-net-mvc5-and-entity-framewor/

        public static CaptchaResponse ValidateCaptcha(string response)
        {
            string secret = System.Web.Configuration.WebConfigurationManager.AppSettings["recaptchaKeySecret"];
            var client = new WebClient();
            var jsonResult = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            return JsonConvert.DeserializeObject<CaptchaResponse>(jsonResult.ToString());
        }

        #endregion

    }
}