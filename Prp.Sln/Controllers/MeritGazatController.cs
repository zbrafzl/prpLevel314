using Newtonsoft.Json;
using Prp.Data;
using Prp.Sln;
using System;
using System.Data;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class MeritGazatController : Controller
	{
		public MeritGazatController()
		{
		}

		[ValidateInput(false)]
		public ActionResult ExportDataToExcelAndDownload(MeritGazatModel ModelSave)
		{
			Message message = new Message();
			GazatMerit gazatMerit = new GazatMerit()
			{
				typeId = ModelSave.gazatMerit.typeId
			};
			try
			{
				string str = string.Concat(ModelSave.exportExcel.fileName, ".xlsx");
				string str1 = str.GenerateFilePath("/ExcelFiles/Gazat/");
				if (string.IsNullOrWhiteSpace(str1))
				{
					message.status = false;
					message.msg = "Error : File path and name creating.";
				}
				else
				{
					DataTable dataTable = new DataTable();
					dataTable = (new MeritDAL()).GazatGetAllByTypeExport(gazatMerit);
					if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
					{
						message.status = false;
						message.msg = "";
					}
					else
					{
						message = str1.ExcelFileWrite(dataTable, "Sheet1", "A1");
						str1.FileDownload();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = string.Concat("Error in exported : ", exception.Message);
			}
			if (string.IsNullOrWhiteSpace(ModelSave.exportExcel.url))
			{
				ModelSave.exportExcel.url = "/";
			}
			return this.Redirect(ModelSave.exportExcel.url);
		}

		[HttpGet]
		[ValidateInput(false)]
		public ActionResult ExportDataToExcelAndDownloadGazzat(GazatMerit obj)
		{
			Message message = new Message();
			obj.search = obj.search.TooString("");
			try
			{
				string str = "Gazzet-January-2023.xlsx".GenerateFilePath("/ExcelFiles/Gazat/");
				if (string.IsNullOrWhiteSpace(str))
				{
					message.status = false;
					message.msg = "Error : File path and name creating.";
				}
				else
				{
					DataTable dataTable = new DataTable();
					dataTable = (new MeritDAL()).GazatGetAllByTypeViewExport(obj);
					if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
					{
						message.status = false;
						message.msg = "";
					}
					else
					{
						message = str.ExcelFileWrite(dataTable, "Sheet1", "A1");
						str.FileDownload();
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = string.Concat("Error in exported : ", exception.Message);
			}
			return this.Redirect("/ExcelFiles/Gazat/Gazzet-January-2023.xlsx");
		}

		public ActionResult GazatFCPS()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult GazatFCPSD()
		{
			return View(new MeritGazatModel());
		}

		[HttpPost]
		public ActionResult GazatGetAllByTypeView(GazatMerit obj)
		{
			obj.search = obj.search.TooString("");
			DataTable dataTable = (new MeritDAL()).GazatGetAllByTypeView(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult GazatGetAllByTypeViewExport(GazatMerit obj)
		{
			obj.search = obj.search.TooString("");
			DataTable dataTable = (new MeritDAL()).GazatGetAllByTypeViewExport(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		public ActionResult GazatMD()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult GazatMDS()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult GazatMS()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult JobListing()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult MeritFCPS()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult MeritFCPSD()
		{
			return View(new MeritGazatModel());
		}

		[HttpPost]
		public ActionResult MeritGetAllByTypeView(GazatMerit obj)
		{
			obj.search = obj.search.TooString("");
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			obj.roundNo = ProjConstant.consentRound;
			DataTable dataTable = (new MeritDAL()).MeritGetAllByTypeView(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		public ActionResult MeritMD()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult MeritMDS()
		{
			return View(new MeritGazatModel());
		}

		public ActionResult MeritMS()
		{
			return View(new MeritGazatModel());
		}
	}
}