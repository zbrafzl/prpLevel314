using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class AppProfileController : BaseAdminController
    {
        // GET: nadmin/AppProfile
        public ActionResult ProfileView()
        {
            ProfileModelEmpty model = new ProfileModelEmpty();
            return View(model);
        }
    }
}