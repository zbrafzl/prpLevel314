using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;

namespace Prp.Sln.Controllers
{
    public class AjaxMainController : Controller
    {
        [HttpGet]
        public JsonResult HtmlFileRead(string filePath)
        {

            HtmlSearch obj = new HtmlSearch();
            try
            {
                obj.html = filePath.ReadHtmlFile();
            }
            catch (Exception ex)
            {
                obj.html = "";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}