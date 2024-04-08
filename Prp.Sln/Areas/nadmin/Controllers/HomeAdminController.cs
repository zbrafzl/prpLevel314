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
	public class HomeAdminController : BaseAdminController
	{
		public HomeAdminController()
		{
		}

		public ActionResult DashboardAccountPHF()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult DashboardApplicantion()
		{
			return View(new HomeModelAdmin());
		}

		[CheckHasRight]
		public ActionResult DashboardInductionWise()
		{
			HomeModelAdmin homeModelAdmin = new HomeModelAdmin()
			{
				inductionId = Request.QueryString["inductionId"].TooInt()
			};
			if (homeModelAdmin.inductionId == 0)
			{
				homeModelAdmin.inductionId = ProjConstant.inductionId;
			}
			homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCount(homeModelAdmin.inductionId, ProjConstant.phaseId);
			return View(homeModelAdmin);
		}

		public ActionResult DsbJoiningHospital()
		{
			int num = Request.QueryString["instituteId"].TooInt();
			ApplicantJoiningDsbModel applicantJoiningDsbModel = new ApplicantJoiningDsbModel();
			if (num > 0)
			{
				applicantJoiningDsbModel.listHospInst = (new JoiningDAL()).GetCountInstituteHospitalWise(num);
			}
			return View(applicantJoiningDsbModel);
		}

		public ActionResult DsbJoiningInstitute()
		{
			ApplicantJoiningDsbModel applicantJoiningDsbModel = new ApplicantJoiningDsbModel()
			{
				listHospInst = (new JoiningDAL()).GetCountInstituteHospitalWise(0)
			};
			return View(applicantJoiningDsbModel);
		}

		[HttpGet]
		public JsonResult GetCountInstituteWise()
		{
			List<ApplicantJoiningDsb> countInstituteHospitalWise = (new JoiningDAL()).GetCountInstituteHospitalWise(0);
			return base.Json(countInstituteHospitalWise, 0);
		}

		public ActionResult HomePageAdmin()
		{
			ActionResult actionResult;
			HomeModelAdmin homeModelAdmin = new HomeModelAdmin()
			{
				typeId = base.loggedInUser.typeId,
				inductionId = ProjConstant.inductionId
			};
			if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCountInstituteHospital(0, base.loggedInUser.userId, "");
				actionResult = base.View("InstituteHospitalBoard", homeModelAdmin);
			}
			else if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.institute)
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCountInstituteHospital(base.loggedInUser.userId, 0, "");
				actionResult = base.View("InstituteDashBoard", homeModelAdmin);
			}
			else if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.bank)
			{
				actionResult = this.Redirect("/admin/voucher-list-bank");
			}
			else if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.keJournalTeam)
			{
				actionResult = this.Redirect("/admin/research-journal-manage");
			}
			else if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior)
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
				actionResult = base.View("KeSeniorDashboard", homeModelAdmin);
			}
			else if (base.loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification || base.loggedInUser.typeId == 41)
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
				actionResult = base.View("KeVerifyDashboard", homeModelAdmin);
			}
			else if (base.loggedInUser.typeId != ProjConstant.Constant.UserType.phfAccountant)
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
				actionResult = base.View("DashboardInductionWise", homeModelAdmin);
			}
			else
			{
				homeModelAdmin.listDashBoard = (new CommonDAL()).GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
				actionResult = base.View("DashboardAccountPHF", homeModelAdmin);
			}
			return actionResult;
		}

        [HttpPost]
        public ActionResult GetDashboardCount(ApplicationStatus obj)
        {
            DataSet dataSet = (new CommonDAL()).GetDashboardCount(obj);
            string json = JsonConvert.SerializeObject(dataSet);
            return base.Content(json, "application/json");
        }

        public ActionResult HospitalDashBoard()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult Index()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult InstituteDashBoard()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult InstituteHospitalBoard()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult KeSeniorDashboard()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult KeVerifyDashboard()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult StatusView()
		{
			return View(new HomeModelAdmin());
		}

		public ActionResult Welcome()
		{
			return View(new HomeModelAdmin());
		}

        public ActionResult ApplicantSupervisorCount()
        {
            return View(new EmptyModelAdmin());
        }
    }
}