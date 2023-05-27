using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class VoucherAdminController : BaseAdminController
	{
		public VoucherAdminController()
		{
		}

		[ValidateInput(false)]
		public ActionResult ExportDataToExcelAndDownload(VoucherAdminModel ModelSave)
		{
			Message message = new Message();
			VoucherSearch modelSave = ModelSave.search;
			modelSave.startDate = modelSave.dateStart.TooDate();
			modelSave.endDate = modelSave.dateEnd.TooDate();
			modelSave.cnicNo = modelSave.cnicNo.TooString("");
			modelSave.applicantNo = modelSave.applicantNo.TooString("");
			try
			{
				string str = "VoucherList.xlsx";
				if (modelSave.reportType != "Voucher")
				{
					str = "VoucherBankList.xlsx";
				}
				else
				{
					str = "VoucherList.xlsx";
					if (modelSave.countryTypeId == 1)
					{
						str = string.Concat("National", str);
					}
					else if (modelSave.countryTypeId == 2)
					{
						str = string.Concat("Foreigner", str);
					}
				}
				string str1 = str.GenerateFilePath(base.loggedInUser);
				if (string.IsNullOrWhiteSpace(str1))
				{
					message.status = false;
					message.msg = "Error : File path and name creating.";
				}
				else
				{
					DataTable dataTable = new DataTable();
					dataTable = (modelSave.reportType != "Voucher" ? (new VoucherDAL()).VoucherExportBank(modelSave) : (new VoucherDAL()).VoucherExport(modelSave));
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
			if (string.IsNullOrWhiteSpace(modelSave.pageUrl))
			{
				modelSave.pageUrl = "/admin/voucher-list";
			}
			return this.Redirect(modelSave.pageUrl);
		}

		[HttpPost]
		public JsonResult UpdateStatus(ApplicationStatus obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			Message message = (new ApplicantDAL()).ApplicantStatusUpdate(obj.applicantId, ProjConstant.Constant.ApplicationStatusType.voucherPhf, obj.statusId);
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult VoucherBankList()
		{
			return View(new VoucherAdminModel());
		}

		public ActionResult VoucherDetail()
		{
			VoucherAdminModel voucherAdminModel = new VoucherAdminModel();
			int num = Request.QueryString["applicantId"].TooInt();
			int num1 = ProjConstant.inductionId;
			int num2 = ProjConstant.phaseId;
			voucherAdminModel.applicant = (new ApplicantDAL()).GetApplicant(num1, num);
			voucherAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(num1, num2, num);
			voucherAdminModel.voucher = (new ApplicantDAL()).GetApplicantVoucher(num1, ProjConstant.phaseId, num);
			voucherAdminModel.voucherStatus = (new ApplicantDAL()).GetApplicationStatus(num1, ProjConstant.phaseId, num, ProjConstant.Constant.ApplicationStatusType.voucherPhf).FirstOrDefault<ApplicationStatus>();
			return View(voucherAdminModel);
		}

		[CheckHasRight]
		public ActionResult VoucherList()
		{
			return View(new VoucherAdminModel());
		}

		public ActionResult VoucherListAll()
		{
			VoucherAdminModel voucherAdminModel = new VoucherAdminModel();
			string str = "GetByStatus";
			voucherAdminModel.countryType = Request.QueryString["id"].TooInt();
			if (voucherAdminModel.countryType == 0)
			{
				voucherAdminModel.countryType = 1;
			}
			voucherAdminModel.listVoucher = (new VoucherDAL()).GetVoucherByCondtion(ProjConstant.Constant.ApplicationStatus.completed, voucherAdminModel.countryType, str);
			return View(voucherAdminModel);
		}

		public ActionResult VoucherListNotVerified()
		{
			return View(new VoucherAdminModel());
		}

		[HttpPost]
		public ActionResult VoucherListSearch(VoucherSearch obj)
		{
			obj.startDate = obj.dateStart.TooDate();
			obj.endDate = obj.dateEnd.TooDate();
			DataTable dataTable = (new VoucherDAL()).VoucherSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult VoucherListSearchBank(VoucherSearch obj)
		{
			obj.startDate = obj.dateStart.TooDate();
			obj.endDate = obj.dateEnd.TooDate();
			DataTable dataTable = (new VoucherDAL()).VoucherSearchBank(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult VoucherSearchWithBank(VoucherSearch obj)
		{
			obj.search = obj.search.TooString("");
			DataTable dataTable = (new VoucherDAL()).VoucherSearchWithBank(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}
	}
}