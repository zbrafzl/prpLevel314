using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nHardship.Controllers
{
    public class LoggedInHsController : Controller
    {
        public ActionResult LoggedIn()

        {
            ActionResult actionResult;
            LoginHsModel loginModel = new LoginHsModel();
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

        [HttpPost]
        public JsonResult LoggedInUserHs(LoginUser login)
        {
            Message message = new Message();
            Applicant applicant = ProjFunctions.CookieApplicantGetHs();
            if (applicant == null)
            {
                try
                {
                    applicant = (new ApplicantDAL()).LoginHs(login.emailId, login.password);
                    if (applicant.applicantId > 0)
                    {
                        ProjFunctions.CookieApplicantSetHs(applicant);
                    }

                }
                catch (Exception ex)
                {
                    applicant = new Applicant();
                    applicant.status = ex.Message;
                    applicant.statusId = 0;
                    applicant.url = "";
                }
            }
            return base.Json(applicant, 0);
        }

        public ActionResult Logout()
        {
            try
            {
                ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicantHs);
            }
            catch (Exception)
            {
            }
            return this.Redirect("/hs");
        }
    }
}