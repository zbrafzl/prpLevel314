using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class FrontDeskController : BaseAdminController
    {
        // GET: nadmin/FrontDesk
        public ActionResult FrontDeskManage()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        public ActionResult FrontDeskSetup()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        
    }
}