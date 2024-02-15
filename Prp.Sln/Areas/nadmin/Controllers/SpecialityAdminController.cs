using Newtonsoft.Json;
using prp.fn;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Message = Prp.Data.Message;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class SpecialityAdminController : BaseAdminController
	{
		public SpecialityAdminController()
		{
		}

		[HttpPost]
		public ActionResult ApplicantSearchList(ApplicantSearch obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new ApplicantAdminDAL()).ApplicantSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpGet]
		public JsonResult GetSpecialityJobById(int specialityJobId)
		{
			SpecialityJob specialityJobById = (new SpecialityDAL()).GetSpecialityJobById(specialityJobId);
			return base.Json(specialityJobById, 0);
		}

		[CheckHasRight]
		public ActionResult JobApplicationSearch()
		{
			SpecialityJobModelAdmin specialityJobModelAdmin = new SpecialityJobModelAdmin()
			{
				inductionId = Request.QueryString["instituteId"].TooInt()
			};
			if (specialityJobModelAdmin.inductionId == 0)
			{
				specialityJobModelAdmin.inductionId = ProjConstant.inductionId;
			}
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.degreeType
			};
			specialityJobModelAdmin.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(specialityJobModelAdmin);
		}

		public ActionResult JobHospitalWise()
		{
			if (Request.QueryString["hospitalId"].TooInt() > 0)
			{
			}
			return View();
		}

		public ActionResult JobInstituteWise()
		{
			SpecialityJobModelAdmin specialityJobModelAdmin = new SpecialityJobModelAdmin();
			specialityJobModelAdmin.job.specialityId = Request.QueryString["specialityId"].TooInt();
			specialityJobModelAdmin.job.instituteId = Request.QueryString["instituteId"].TooInt();
			specialityJobModelAdmin.job.isActive = true;
			specialityJobModelAdmin.listInstitute = DDLInstitute.GetAll(base.loggedInUser.userId, ProjConstant.DDL.Institute.hasHospitalByUserId);
			DDLSpecialitys dDLSpeciality = new DDLSpecialitys()
			{
				condition = "GetAll"
			};
			specialityJobModelAdmin.listSpeciality = (new SpecialityDAL()).GetSpecialityDDL(dDLSpeciality);
			specialityJobModelAdmin.listDegreeType = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.degreeType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listQouta = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.qoutaType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listInduction = Prp.Sln.DDLInduction.GetAll("GetAllActive");
			if ((specialityJobModelAdmin.listInduction == null ? false : specialityJobModelAdmin.listInduction.Count == 1))
			{
				specialityJobModelAdmin.job.inductionId = specialityJobModelAdmin.listInduction.FirstOrDefault<EntityDDL>().id.TooInt();
			}
			if ((specialityJobModelAdmin.job.inductionId <= 0 ? false : specialityJobModelAdmin.job.specialityId > 0))
			{
				specialityJobModelAdmin.listSpecialityJob = (new SpecialityDAL()).GetSpecialityJobByParameters(specialityJobModelAdmin.job.inductionId, specialityJobModelAdmin.job.specialityId);
			}
			return View(specialityJobModelAdmin);
		}

		public ActionResult JobListing()
		{
			SpecialityJobModelAdmin specialityJobModelAdmin = new SpecialityJobModelAdmin();
			int num = Request.QueryString["typeId"].TooInt();
			specialityJobModelAdmin.listDegreeType = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.degreeType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listQouta = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.qoutaType)
				orderby x.id
				select x).ToList<Constant>();
			if (num == 0)
			{
				num = specialityJobModelAdmin.listDegreeType.FirstOrDefault<Constant>().id.TooInt();
			}
			specialityJobModelAdmin.typeId = num;
			specialityJobModelAdmin.listSpecialityJob = (new SpecialityDAL()).GetSpecialityJobByTypeId(ProjConstant.inductionId, num);
			return View(specialityJobModelAdmin);
		}

		[ValidateInput(false)]
		public ActionResult SaveSpecialityJobData(SpecialityJobModelAdmin ModelSave, HttpPostedFileBase files)
		{
			SpecialityJob modelSave = ModelSave.job;
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			(new SpecialityDAL()).AddUpdate(modelSave);
			int num = modelSave.specialityId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/speciality-job?specialityId=", num.ToString()));
			return actionResult;
		}

		public ActionResult SeatsStatusList()
		{
			SpecialityJobModelAdmin specialityJobModelAdmin = new SpecialityJobModelAdmin();
			int num = Request.QueryString["typeId"].TooInt();
			specialityJobModelAdmin.listDegreeType = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.degreeType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listQouta = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.qoutaType)
				orderby x.id
				select x).ToList<Constant>();
			if (num == 0)
			{
				num = specialityJobModelAdmin.listDegreeType.FirstOrDefault<Constant>().id.TooInt();
			}
			specialityJobModelAdmin.typeId = num;
			specialityJobModelAdmin.listSpecialityJob = (new SpecialityDAL()).GetSpecialityJobByTypeId(ProjConstant.inductionId, num);
			return View(specialityJobModelAdmin);
		}

		[HttpPost]
		public ActionResult SpecialityJobPrefferenceSearch(SpecialityJobSearch obj)
		{
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new SpecialityDAL()).SpecialityJobPrefferenceSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		public ActionResult SpecialityJobSetup()
		{
			SpecialityJobModelAdmin specialityJobModelAdmin = new SpecialityJobModelAdmin();
			specialityJobModelAdmin.job.specialityId = Request.QueryString["specialityId"].TooInt();
			specialityJobModelAdmin.job.instituteId = Request.QueryString["instituteId"].TooInt();
			specialityJobModelAdmin.job.isActive = true;
			specialityJobModelAdmin.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasHospital);
			DDLSpecialitys dDLSpeciality = new DDLSpecialitys()
			{
				condition = "GetAll"
			};
			specialityJobModelAdmin.listSpeciality = (new SpecialityDAL()).GetSpecialityDDL(dDLSpeciality);
			specialityJobModelAdmin.listDegreeType = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.degreeType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listQouta = (
				from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.qoutaType)
				orderby x.id
				select x).ToList<Constant>();
			specialityJobModelAdmin.listInduction = Prp.Sln.DDLInduction.GetAll("GetAllActive");
			if ((specialityJobModelAdmin.listInduction == null ? false : specialityJobModelAdmin.listInduction.Count == 1))
			{
				specialityJobModelAdmin.job.inductionId = specialityJobModelAdmin.listInduction.FirstOrDefault<EntityDDL>().id.TooInt();
			}
			specialityJobModelAdmin.listSpecialityJob = (new SpecialityDAL()).GetSpecialityJobByParameters(specialityJobModelAdmin.job.inductionId, specialityJobModelAdmin.job.specialityId);
			return View(specialityJobModelAdmin);
		}


        public ActionResult SpecialityJobSetupMultiple()
        {
            return View(new EmptyModelAdmin() { });
        }

        [HttpPost]
        public ActionResult SpecialityJobGetDataByParam(SpecialityJobSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            DataSet dataTable = (new SpecialityDAL()).SpecialityJobGetDataByParam(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public ActionResult SpecialityJobAddUpdateParam(SpecialityJob obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.adminId = loggedInUser.adminId;
            obj.dataTable = MyFunctions.ConvertDataTable(obj.listDataTb);

            DataSet dataTable = (new SpecialityDAL()).SpecialityJobAddUpdateParam(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }
    }
}