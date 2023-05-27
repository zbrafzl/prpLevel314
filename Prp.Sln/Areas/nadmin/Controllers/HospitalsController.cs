using Newtonsoft.Json;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class HospitalsController : BaseAdminController
	{
		public HospitalsController()
		{
		}

		[HttpPost]
		public JsonResult BedDelete(Bed obj)
		{
			Message message = (new CommonDAL()).BedDelete(obj);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult BedsAddUpdate(Bed obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.adminId = base.loggedInUser.userId;
			obj.remarksN = obj.remarksN.TooString("");
			obj.remarksDep = obj.remarksDep.TooString("");
			obj.imageN = obj.imageN.TooString("");
			obj.imageDep = obj.imageDep.TooString("");
			Message message = (new CommonDAL()).BedAddUpdate(obj);
			return base.Json(message, 0);
		}

		public ActionResult BedsManagement()
		{
			BedModelAdmin bedModelAdmin = new BedModelAdmin()
			{
				hospitalId = base.loggedInUser.reffId
			};
			try
			{
				if (bedModelAdmin.hospitalId == 0)
				{
					bedModelAdmin.listHospital = DDLHospital.GetAll(25, "HasEmployee");
					bedModelAdmin.hospitalId = Request.QueryString["hospitalId"].TooInt();
					if (bedModelAdmin.hospitalId == 0)
					{
						bedModelAdmin.hospitalId = bedModelAdmin.listHospital.FirstOrDefault<EntityDDL>().@value.TooInt();
					}
				}
			}
			catch (Exception exception)
			{
			}
			bedModelAdmin.departmentId = Request.QueryString["departmentId"].TooInt();
			bedModelAdmin.unitId = Request.QueryString["unitId"].TooInt();
			bedModelAdmin.listDepartment = (
				from x in DDLDepartment.GetAll(bedModelAdmin.hospitalId, "ByHospitalWithBed")
				orderby x.key
				select x).ToList<EntityDDL>();
			bedModelAdmin.listDiscipline = DDLDiscipline.GetAll(bedModelAdmin.hospitalId, "ForBed").ToList<EntityDDL>();
			bedModelAdmin.listSpeciality = DDLSpeciality.GetAll("GetAll").ToList<EntityDDL>();
			return View(bedModelAdmin);
		}

		[HttpPost]
		public ActionResult BedsSearch(Bed obj)
		{
			if (obj.inductionId == 0)
			{
				obj.inductionId = ProjConstant.inductionId;
			}
			DataTable dataTable = (new CommonDAL()).BedsSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[CheckHasRight]
		public ActionResult DepartmentAssociation()
		{
			HospitalModelAdmin hospitalModelAdmin = new HospitalModelAdmin()
			{
				hospitalId = Request.QueryString["id"].TooInt(),
				listHospital = DDLHospital.GetAll(25, "GetTeaching")
			};
			return View(hospitalModelAdmin);
		}

		[HttpGet]
		public JsonResult DepartmentHospitalAddUpdate(int hospitalId, string ids)
		{
			ids = ids.TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' });
			Message message = (new CommonDAL()).DepartmentHospitalAddUpdate(hospitalId, ids, base.loggedInUser.userId);
			return base.Json(message, 0);
		}

		public ActionResult DisciplineAssociation()
		{
			HospitalDisciplineModelAdmin hospitalDisciplineModelAdmin = new HospitalDisciplineModelAdmin()
			{
				listType = DDLConstant.GetAll(ProjConstant.Constant.bedsApprovalType)
			};
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				hospitalDisciplineModelAdmin.discipline = (new HospitalDAL()).HospitalDisciplineGetById(num);
				string str = string.Concat("select isnull(certificateImage,'') from tblHospitalDiscipline where id  = ", num.ToString());
				SqlConnection sqlConnection = new SqlConnection();
				Message message = new Message();
				SqlCommand sqlCommand = new SqlCommand(str);
				try
				{
					try
					{
						sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
						sqlConnection.Open();
						sqlCommand.Connection = sqlConnection;
						hospitalDisciplineModelAdmin.certificateImage = sqlCommand.ExecuteScalar().ToString();
					}
					catch (Exception exception)
					{
						hospitalDisciplineModelAdmin.certificateImage = "";
					}
				}
				finally
				{
					sqlConnection.Close();
				}
			}
			if (base.loggedInUser.typeId != ProjConstant.Constant.UserType.hospital)
			{
				hospitalDisciplineModelAdmin.hospitalId = Request.QueryString["hospitalId"].TooInt();
				hospitalDisciplineModelAdmin.listHospital = DDLHospital.GetAll(25, "GetTeaching");
				if (hospitalDisciplineModelAdmin.hospitalId == 0)
				{
					hospitalDisciplineModelAdmin.hospitalId = hospitalDisciplineModelAdmin.listHospital.FirstOrDefault<EntityDDL>().@value.TooInt();
				}
			}
			else
			{
				hospitalDisciplineModelAdmin.hospitalId = base.loggedInUser.reffId;
			}
			hospitalDisciplineModelAdmin.listDiscipline = DDLDiscipline.GetAll(hospitalDisciplineModelAdmin.hospitalId, hospitalDisciplineModelAdmin.discipline.disciplineId, "HospitalAccess");
			hospitalDisciplineModelAdmin.list = (new HospitalDAL()).HospitalDisciplineSearch(hospitalDisciplineModelAdmin.hospitalId);
			return View(hospitalDisciplineModelAdmin);
		}

		[HttpGet]
		public ActionResult GetDepartmentForHospital(int hospitalId)
		{
			DataTable departmentForHospital = (new CommonDAL()).GetDepartmentForHospital(hospitalId);
			string str = JsonConvert.SerializeObject(departmentForHospital);
			return base.Content(str, "application/json");
		}

		[HttpGet]
		public JsonResult GetHospitalById(int hospitalId)
		{
			Hospital hospital = new Hospital();
			try
			{
				hospital = (new HospitalDAL()).GetById(hospitalId);
			}
			catch (Exception exception)
			{
				hospital = new Hospital();
			}
			return base.Json(hospital, 0);
		}

		[HttpPost]
		public JsonResult GetHospitalDDL(DDLHospitals obj)
		{
			List<EntityDDL> hospitalDDL = (new HospitalDAL()).GetHospitalDDL(obj);
			return base.Json(hospitalDDL, 0);
		}

		[HttpPost]
		public JsonResult HospitalDisciplineAddUpdate(HospitalDiscipline obj)
		{
			obj.adminId = base.loggedInUser.adminId;
			obj.remarks = obj.remarks.TooString("");
			obj.dateStart = DateTime.Now;
			obj.dateEnd = DateTime.Now;
			obj.adminId = base.loggedInUser.userId;
			obj.certificateImage = obj.certificateImage.TooString("");
			obj.startDate = obj.startDate.TooString("");
			obj.endDate = obj.endDate.TooString("");
			if (!string.IsNullOrWhiteSpace(obj.startDate))
			{
				obj.dateStart = obj.startDate.TooDate();
			}
			if (!string.IsNullOrWhiteSpace(obj.endDate))
			{
				obj.dateEnd = obj.endDate.TooDate();
			}
			Message message = (new HospitalDAL()).HospitalDisciplineAddUpdate(obj);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult HospitalDisciplineDelete(HospitalDiscipline obj)
		{
			Message message = (new HospitalDAL()).HospitalDisciplineDelete(obj);
			return base.Json(message, 0);
		}

		public ActionResult HospitalManage()
		{
			HospitalModelAdmin hospitalModelAdmin = new HospitalModelAdmin();
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.instituteLevel
			};
			hospitalModelAdmin.listLevel = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			DDLRegions dDLRegion = new DDLRegions()
			{
				condition = "ByType",
				typeId = ProjConstant.Constant.Region.distric
			};
			hospitalModelAdmin.listRegion = (new MasterSetupDAL()).GetRegionDDL(dDLRegion);
			return View(hospitalModelAdmin);
		}

		[HttpPost]
		public ActionResult HospitalSearch(HospitalSearch obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new HospitalDAL()).HospitalSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		public ActionResult HospitalSetup()
		{
			HospitalModelAdmin hospitalModelAdmin = new HospitalModelAdmin();
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				hospitalModelAdmin.hospital = (new HospitalDAL()).GetById(num);
			}
			hospitalModelAdmin.listDistrict = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.distric, 0, "GetDistrictAll");
			DDLConstants dDLConstant = new DDLConstants()
			{
				reffIds = "23,24,25",
				condition = "ReffIdsParent"
			};
			hospitalModelAdmin.listLevel = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				reffIds = "23,24,25",
				condition = "ReffIdsChild"
			};
			hospitalModelAdmin.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(hospitalModelAdmin);
		}

		[ValidateInput(false)]
		public ActionResult SaveHospitalData(HospitalModelAdmin ModelSave, HttpPostedFileBase files)
		{
			Hospital modelSave = ModelSave.hospital;
			modelSave.name = modelSave.name.TooString("");
			modelSave.code = modelSave.code.TooString("");
			modelSave.nameDisplay = modelSave.nameDisplay.TooString("");
			modelSave.abbr = modelSave.abbr.TooString("");
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.address = modelSave.address.TooString("");
			modelSave.regionId = modelSave.regionId.TooInt();
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			modelSave.instituteIds = modelSave.instituteIds.TooString("");
			(new HospitalDAL()).AddUpdate(modelSave);
			return this.Redirect("/admin/hospital-manage");
		}

		[HttpPost]
		public JsonResult UnitAddUpdate(Unit obj)
		{
			obj.adminId = base.loggedInUser.userId;
			Message message = (new CommonDAL()).UnitAddUpdate(obj);
			return base.Json(message, 0);
		}
	}
}