using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {

        public ActionResult HomePageAdmin()
        
        {
            HomeModelAdmin model = new HomeModelAdmin();
            model.typeId = loggedInUser.typeId;
            model.inductionId = ProjConstant.inductionId;

            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
            {
                //model.listInduction = DDLInduction.GetAll("AllCompleted");

                //int inductionId = 0;
                //try
                //{
                //    inductionId = model.listInduction.OrderBy(x => x.value).FirstOrDefault().value.TooInt();
                //}
                //catch (Exception)
                //{
                //    inductionId = 0;
                //}

                //if (inductionId > 0 && model.inductionId != inductionId)
                //    model.inductionId = inductionId;

                model.listDashBoard = new CommonDAL().GetDashboardCountInstituteHospital(0, loggedInUser.userId, "");
                //model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("InstituteHospitalBoard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.institute)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCountInstituteHospital(loggedInUser.userId, 0, "");
                return View("InstituteDashBoard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.bank)
            {
                return Redirect("/admin/voucher-list-bank");
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keJournalTeam)
            {
                return Redirect("/admin/research-journal-manage");
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("KeSeniorDashboard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("KeVerifyDashboard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.phfAccountant)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("DashboardAccountPHF", model);
            }
            else
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("DashboardInductionWise", model);
            }
        }

        [CheckHasRight]
        public ActionResult DashboardInductionWise()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            model.inductionId = Request.QueryString["inductionId"].TooInt();
            if (model.inductionId == 0) model.inductionId = ProjConstant.inductionId;
            model.listDashBoard = new CommonDAL().GetDashboardCount(model.inductionId, ProjConstant.phaseId);

            return View(model);
        }

        public ActionResult Index()
        {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }


        public ActionResult StatusView()
        {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }


        public ActionResult DashboardApplicantion()
        {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }

        public ActionResult Welcome()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult InstituteDashBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult HospitalDashBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult InstituteHospitalBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult KeSeniorDashboard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }


        public ActionResult KeVerifyDashboard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult DashboardAccountPHF()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }


        public ActionResult DsbJoiningInstitute()
        {
            ApplicantJoiningDsbModel model = new ApplicantJoiningDsbModel();
            model.listHospInst = new JoiningDAL().GetCountInstituteHospitalWise();

            return View(model);
        }

        [HttpGet]
        public JsonResult GetCountInstituteWise()
        {
            List<ApplicantJoiningDsb> list = new JoiningDAL().GetCountInstituteHospitalWise();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DsbJoiningHospital()
        {
            int instituteId = Request.QueryString["instituteId"].TooInt();

            ApplicantJoiningDsbModel model = new ApplicantJoiningDsbModel();

            if (instituteId > 0)
                model.listHospInst = new JoiningDAL().GetCountInstituteHospitalWise(instituteId);
            return View(model);
        }


    }
}