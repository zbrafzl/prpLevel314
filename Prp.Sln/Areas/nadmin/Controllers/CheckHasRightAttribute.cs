using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
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
				User user = ProjFunctions.CookiesAdminGet();
				if (user != null)
				{
					try
					{
						string str = HttpContext.Current.Request.Url.AbsolutePath.ToLower().TrimStart(new char[] { '/' });
						if (str.Contains("-induction"))
						{
							int num = str.IndexOf("-induction");
							if (num > 0)
							{
								str = str.Substring(0, num);
							}
						}
						bool flag = (new MenuDAL()).CheckPageHasRight(2, user.userId, str);
						if ((user.userId <= 1 ? false : !flag))
						{
							filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "NoRights" })));
						}
					}
					catch (Exception exception)
					{
						filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "NoRights" })));
					}
				}
				else
				{
					filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "Logout" })));
				}
			}
			catch (Exception exception1)
			{
				filterContext.set_Result(new RedirectToRouteResult(new RouteValueDictionary(new { controller = "LoggedInAdmin", action = "Logout" })));
			}
		}
	}
}