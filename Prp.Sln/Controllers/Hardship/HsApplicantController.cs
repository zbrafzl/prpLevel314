using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers.Hardship
{
    public class HsApplicantController : HsBaseController
    {
        // GET: ApplicantHs
        public ActionResult Application()
        {
            HsEmptyModel model = new HsEmptyModel();
            return View(model);
        }
    }
}