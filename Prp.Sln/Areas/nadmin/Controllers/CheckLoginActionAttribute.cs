using Prp.Data;
using Prp.Sln;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
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
				if (ProjFunctions.CookiesAdminGet() == null)
				{
					filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "Logout" })));
				}
			}
			catch (Exception exception)
			{
				filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "Logout" })));
			}
		}
	}
}