using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Data;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class MasterSetupsController : Controller
	{
		public MasterSetupsController()
		{
		}

		[CheckHasRight]
		public ActionResult RegionManage()
		{
			RegionModelModel regionModelModel = new RegionModelModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.regionType
			};
			regionModelModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(regionModelModel);
		}

		[HttpPost]
		public ActionResult RegionSearch(RegionSearch obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new MasterSetupDAL()).RegionSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[CheckHasRight]
		public ActionResult RegionSetup()
		{
			return View();
		}
	}
}