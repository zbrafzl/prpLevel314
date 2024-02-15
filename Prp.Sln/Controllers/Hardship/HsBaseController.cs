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
using System.Web.Routing;
using static Prp.Sln.ProjConstant;

namespace Prp.Sln.Controllers.Hardship
{
    [CheckLoginHsAction]
    public class HsBaseController : Controller
    {
        public Applicant loggedInUser { get; set; }

        public HsBaseController() {
            loggedInUser = ProjFunctions.CookieApplicantGetHs();

        }
    }

    public class CheckLoginHsActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                Applicant loggedInUser = ProjFunctions.CookieApplicantGetHs();

                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedIn",
                        action = "LogoutHs"
                    }));
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedIn",
                    action = "LogoutHs"
                }));
            }
        }
    }
}