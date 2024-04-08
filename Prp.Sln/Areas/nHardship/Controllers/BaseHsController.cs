using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Areas.nHardship.Controllers
{
    [CheckLoginHsAction]
    public class BaseHsController : Controller
    {
        public Applicant loggedInUser { get; set; }

        public BaseHsController()
        {
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
                        controller = "LoggedInHs",
                        action = "Logout"
                    }));
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedInHs",
                    action = "Logout"
                }));
            }
        }
    }
}