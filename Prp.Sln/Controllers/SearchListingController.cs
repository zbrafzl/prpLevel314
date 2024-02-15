using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class SearchListingController : Controller
    {
        // GET: SearchListing
        public ActionResult ApplicantList()
        {
            SearhListModel model = new SearhListModel();
            string param = Request.RequestContext.RouteData.Values["param"].TooString();
            string key = "";// param.Decrypt();
            string[] arrStr = key.Split(',');
            model.inductionId = arrStr[0].TooInt();
            model.statusId = arrStr[1].TooInt();
            model.urlPram = param;
            model.isValid = true;
            return View(model);
        }
    }
}