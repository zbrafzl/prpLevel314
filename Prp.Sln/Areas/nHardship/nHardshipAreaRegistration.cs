using System.Web.Mvc;

namespace Prp.Sln.Areas.nHardship
{
    public class nHardshipAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "nHardship";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(name: "Hs", url: "hs", defaults: new { controller = "LoggedInHs", action = "LoggedIn", id = UrlParameter.Optional });
            context.MapRoute(name: "HsLogin", url: "hs/login", defaults: new { controller = "LoggedInHs", action = "LoggedIn", id = UrlParameter.Optional });
            context.MapRoute(name: "HsLogout", url: "hs/logout", defaults: new { controller = "LoggedInHs", action = "Logout", id = UrlParameter.Optional });
            context.MapRoute(name: "HsApply", url: "hs/apply", defaults: new { controller = "ApplicantHs", action = "Application", id = UrlParameter.Optional });

            context.MapRoute(
                "nHardship_default",
                "nHardship/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}