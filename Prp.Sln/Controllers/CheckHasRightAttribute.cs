using Prp.Data;
using Prp.Sln;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Controllers
{
	public class CheckHasRightAttribute : ActionFilterAttribute
	{
		public CheckHasRightAttribute()
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			try
			{
				Applicant applicant = ProjFunctions.CookieApplicantGet();
				if (applicant != null)
				{
					try
					{
						string str = HttpContext.Current.Request.Url.AbsolutePath.ToLower().TrimStart(new char[] { '/' });
						if (!(new MenuDAL()).CheckPageHasRight(1, applicant.applicantId, str))
						{
							filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "NoRights" })));
						}
					}
					catch (Exception exception)
					{
						filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "NoRights" })));
					}
				}
				else
				{
					filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedIn", action = "Logout" })));
				}
			}
			catch (Exception exception1)
			{
				filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedIn", action = "Logout" })));
			}
		}
	}
}