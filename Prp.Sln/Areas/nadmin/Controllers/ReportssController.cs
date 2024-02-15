using Newtonsoft.Json;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class ReportssController : BaseAdminController
	{
		public ReportssController()
		{
		}

		[HttpPost]
		public ActionResult HospitalTraineeCountReport(SpecialityJobSearch obj)
		{
			obj.search = obj.search.TooString("");
			DataTable dataTable = (new ReportDAL()).HospitalTraineeCountReport(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult JoinedApplicantByHospitalSearch(SpecialityJobSearch obj)
		{
			obj.phaseId = ProjConstant.phaseId;
			if (base.loggedInUser.reffId > 0)
			{
				obj.hospitalId = base.loggedInUser.reffId;
			}
			obj.userId = base.loggedInUser.userId;
			DataTable dataTable = (new ReportDAL()).JoinedApplicantByHospitalSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[CheckHasRight]
		public ActionResult JoinedApplicantHospital()
		{
			ReportApplicantModel reportApplicantModel = new ReportApplicantModel()
			{
				listHospital = DDLHospital.GetAll("GetTeaching"),
				listAttachStatus = DDLConstant.GetAll(901),
				listSpeciality = new List<EntityDDL>(),
				listType = DDLConstant.GetAll(12),
				hospitalId = 0
			};
			if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
			{
				reportApplicantModel.hospitalId = base.loggedInUser.reffId;
			}
			return View(reportApplicantModel);
		}

		[CheckHasRight]
		public ActionResult JoinedApplicantHospitalStatus()
		{
			ReportApplicantModel reportApplicantModel = new ReportApplicantModel()
			{
				listHospital = DDLHospital.GetAll("GetTeaching"),
				listAttachStatus = DDLConstant.GetAll(901),
				listSpeciality = new List<EntityDDL>(),
				listType = DDLConstant.GetAll(12),
				hospitalId = 0
			};
			if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
			{
				reportApplicantModel.hospitalId = base.loggedInUser.reffId;
			}
			return View(reportApplicantModel);
		}

		[CheckHasRight]
		public ActionResult JoinedReport()
		{
			return View(new ReportApplicantModel());
		}

		[HttpPost]
		public ActionResult JoiningApplicantByHospitalSearch(SpecialityJobSearch obj)
		{
			obj.phaseId = ProjConstant.phaseId;
			if (base.loggedInUser.reffId > 0)
			{
				obj.hospitalId = base.loggedInUser.reffId;
			}
			obj.userId = base.loggedInUser.userId;
			DataTable dataTable = (new ReportDAL()).JoiningApplicantByHospitalSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult RptSeatsStatus(SpecialityJobSearch obj)
		{
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new ReportDAL()).RptSeatsStatus(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult RptSeatsStatusExport(SpecialityJobSearch obj)
		{
			Message message = new Message();
			if (obj.inductionId.TooInt() == 0)
			{
				obj.inductionId = ProjConstant.inductionId;
			}
			obj.typeId = obj.typeId.TooInt();
			obj.search = obj.search.TooString("");
			int num = obj.inductionId;
			string str = string.Concat("SeatsStatus", num.ToString(), ".xlsx");
			try
			{
				string str1 = str.GenerateFilePath(base.loggedInUser);
				if (string.IsNullOrWhiteSpace(str1))
				{
					message.status = false;
					message.msg = "Error : File path and name creating.";
				}
				else
				{
					DataTable dataTable = (new ReportDAL()).RptSeatsStatus(obj);
					if ((dataTable == null ? true : dataTable.Rows.Count <= 0))
					{
						message.status = false;
						message.msg = "Some thing went wrong!";
					}
					else
					{
						message = str1.ExcelFileWrite(dataTable, "Sheet1", "A1");
						message.status = true;
						message.msg = str;
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = string.Concat("Error in exported : ", exception.Message);
			}
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult SeatsReport()
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

		[CheckHasRight]
		public ActionResult TraineeAttachReportHospitalWise()
		{
			ReportApplicantModel reportApplicantModel = new ReportApplicantModel()
			{
				listHospital = DDLHospital.GetAll("HasTrainee"),
				hospitalId = 0
			};
			if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
			{
				reportApplicantModel.hospitalId = base.loggedInUser.reffId;
			}
			return View(reportApplicantModel);
		}


        #region Seats Status

        public ActionResult RptInductionGeneral()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        public ActionResult RptExperice()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        public ActionResult SeatsStatus()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        public ActionResult ApplicantFinalStatus()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }


        #endregion
    }
}