using Newtonsoft.Json;
using Prp.Data;
using prp.fn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nHardship.Controllers
{
    public class ApplicantHsController : BaseHsController
    {
        public ActionResult Application()
        {
            EmptyHsModel model = new EmptyHsModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult HsApplicationAddUpdate(HsApplication ac)
        {
            ac.applicantId = base.loggedInUser.applicantId;
            ac.dtPreference = MyFunctions.ConvertDataTable(ac.listTbPreference);
            DataSet ds = (new HsDAL()).HsApplicationAddUpdate(ac);
            string str = JsonConvert.SerializeObject(ds);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public JsonResult HsApplicationDocsAddUpdate(HsApplication ac)
        {
            ac.applicantId = base.loggedInUser.applicantId;
            ac.dtDocs = MyFunctions.ConvertDataTable(ac.listTbDocs);
            Message message = (new HsDAL()).HsApplicationDocsAddUpdate(ac);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}