using Prp.Data;
using Prp.Sln;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Controllers
{
	public class CheckLoginActionAttribute : ActionFilterAttribute
	{
		public CheckLoginActionAttribute()
		{
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
			try
			{
				if (ProjFunctions.CookieApplicantGet() == null)
				{
					filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedIn", action = "Logout" })));
				}
			}
			catch (Exception exception)
			{
				filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedIn", action = "Logout" })));
			}
		}
	}
}