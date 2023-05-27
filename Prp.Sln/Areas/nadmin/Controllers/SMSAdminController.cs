using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class SMSAdminController : Controller
	{
		public SMSAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult SearchSms()
		{
			SMSModelAdmin sMSModelAdmin = new SMSModelAdmin();
			sMSModelAdmin.search.applicantId = Request.QueryString["applicantId"].TooInt();
			return View(sMSModelAdmin);
		}

		[HttpPost]
		public ActionResult SearchSMSByApplicant(SmsEntity obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new SMSDAL()).SearchSMSByApplicant(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}
	}
}