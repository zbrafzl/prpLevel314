using Prp.Sln;
using System;
using System.Web;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public static class AdminHelper
	{
		public static int GetInductionId()
		{
			int num = 0;
			try
			{
				if (HttpContext.Current.Request.RequestContext.RouteData.Values["induction"].TooInt() > 0)
				{
					num = HttpContext.Current.Request.RequestContext.RouteData.Values["induction"].TooInt();
				}
			}
			catch (Exception exception)
			{
				num = ProjConstant.inductionId;
			}
			if (num == 0)
			{
				num = ProjConstant.inductionId;
			}
			return num;
		}

		public static int GetPhaseId()
		{
			int num = 0;
			try
			{
				if (HttpContext.Current.Request.RequestContext.RouteData.Values["phase"].TooInt() > 0)
				{
					num = HttpContext.Current.Request.RequestContext.RouteData.Values["phase"].TooInt();
				}
			}
			catch (Exception exception)
			{
				num = ProjConstant.phaseId;
			}
			if (num == 0)
			{
				num = ProjConstant.phaseId;
			}
			return num;
		}
	}
}