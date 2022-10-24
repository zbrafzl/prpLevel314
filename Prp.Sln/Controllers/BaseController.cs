using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Prp.Sln.Controllers
{
    [CheckLoginAction]
    public class BaseController : Controller
    {
       
        public Applicant loggedInUser { get; set; }
        public BaseController()
        {
            loggedInUser = ProjFunctions.CookieApplicantGet();

            
        }
    }


    public class BaseLoginController : Controller
    {
        public Applicant loggedInUser { get; set; }
        public BaseLoginController()
        {
            loggedInUser = ProjFunctions.CookieApplicantGet();
            

        }
    }

    public class CheckLoginActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                Applicant loggedInUser = ProjFunctions.CookieApplicantGet();

                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedIn",
                        action = "Logout"
                    }));
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedIn",
                    action = "Logout"
                }));
            }
        }
    }

    public class CheckHasRightAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                Applicant loggedInUser = ProjFunctions.CookieApplicantGet();
                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedIn",
                        action = "Logout"
                    }));
                }
                else
                {
                    #region CheckRights

                    try
                    {
                        string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower().TrimStart('/');
                        bool rightHas = new MenuDAL().CheckPageHasRight(1, loggedInUser.applicantId, url);

                        if (rightHas == false)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "Home",
                                action = "NoRights"
                            }));
                        }

                    }
                    catch (Exception)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
                            action = "NoRights"
                        }));
                    }

                    #endregion
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedIn",
                    action = "Logout"
                }));
            }

        }
    }
}