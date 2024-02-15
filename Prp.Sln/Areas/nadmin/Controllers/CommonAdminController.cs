using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class CommonAdminController : BaseAdminController
    {
        #region Constatnt

        public ActionResult InductionManage()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        [HttpPost]
        public ActionResult InductionCalendarGetById(InductionCalendar obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new CommonDAL().InductionCalendarGetById(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public JsonResult InductionCalendarUpdate(InductionCalendar obj)
        {
            obj.adminId = base.loggedInUser.userId;
            obj.startDate = obj.dateStart.TooDate();
            obj.endDate = obj.dateEnd.TooDate();
            Message message = new CommonDAL().InductionCalendarUpdate(obj);
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        [CheckHasRight]
        public ActionResult ConstantManage()
        {
            ConstantModelAdmin model = new ConstantModelAdmin();
            model.typeId = Request.QueryString["typeId"].TooInt();
            model.listType = new ConstantDAL().GetAll(0).OrderBy(x => x.id).ToList();
            //model.list = new ConstantDAL().GetAll(model.typeId).OrderBy(x => x.id).ToList();
            return View(model);
        }


        [HttpPost]
        public ActionResult ConstantSearch(Constant obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new ConstantDAL().ConstantSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        [HttpPost]
        public ActionResult ConstantGetByParam(Constant obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataTable dataTable = new ConstantDAL().ConstantGetByParam(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        [CheckHasRight]
        public ActionResult ConstantSetup()
        {
            ConstantModelAdmin model = new ConstantModelAdmin();
            int constantId = Request.QueryString["id"].TooInt();
            if (constantId > 0)
                model.constant = new ConstantDAL().GetById(constantId);

            model.listType = new ConstantDAL().GetAll(0);

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult SaveConstantData(ConstantModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Constant obj = ModelSave.constant;

            obj.id = obj.id.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.parentId = obj.parentId.TooInt();
            obj.detail = obj.detail.TooString();
            obj.shortDesc = obj.shortDesc.TooString();
            if (String.IsNullOrWhiteSpace(obj.shortDesc))
                obj.shortDesc = obj.name.TooString();
            obj.nameDisplay = obj.nameDisplay.TooString();
            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.value = obj.value.TooInt();
            obj.isActive = obj.isActive.TooBoolean();
            obj.isDeleted = obj.isDeleted.TooBoolean();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;


            Message m = new ConstantDAL().AddUpdate(obj);

            return Redirect("/admin/constant-manage?typeId=" + obj.typeId);
        }

        #endregion


        [CheckHasRight]
        public ActionResult DisciplineSetup()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        [HttpPost]
        public ActionResult DisciplineGetById(Discipline obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new CommonDAL().DisciplineGetById(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public JsonResult DisciplineAddUpdate(Discipline obj)
        {
            obj.adminId = base.loggedInUser.userId;
            Message message = new CommonDAL().DisciplineAddUpdate(obj);
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        [CheckHasRight]
        public ActionResult DisciplineManage()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        [HttpPost]
        public ActionResult DisciplineSearch(Discipline obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new CommonDAL().DisciplineSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        #region SMS

        [CheckHasRight]
        public ActionResult SMSManage()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }




        [HttpPost]
        public ActionResult SMSSearch(SMS obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new SMSDAL().SMSSearch(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }






        [CheckHasRight]
        public ActionResult SMSSetup()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            return View(model);
        }


        [HttpPost]
        public ActionResult SMSGetById(SMS obj)
        {
            obj.adminId = loggedInUser.adminId;
            DataSet dataTable = new SMSDAL().SMSGetById(obj);
            string str = JsonConvert.SerializeObject(dataTable);
            return base.Content(str, "application/json");
        }

        public JsonResult SMSAddUpdate(SMS obj)
        {
            obj.adminId = base.loggedInUser.userId;
            Message message = new SMSDAL().SMSAddUpdate(obj);
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SMSCampaignManage()
        {
            EmptyModelAdmin model = new EmptyModelAdmin();
            return View(model);
        }

        public ActionResult SMSCampaignSetup()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            return View(model);
        }

        public JsonResult SMSAddUpdateCampaign(SmsCampaign obj)
        {
            obj.adminId = base.loggedInUser.userId;
            if (obj.isSchedule == false)
                obj.startTime = DateTime.Now;
            else
                obj.startTime = obj.timeStart.TooDate();

            Message message = new SMSDAL().SMSAddUpdateCampaign(obj);
            return Json(message, JsonRequestBehavior.AllowGet);
        }


        #endregion

        [CheckHasRight]
        public ActionResult ResearchJournalManage()
        {
            ResearchJournalModel model = new ResearchJournalModel();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.journalDispline).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult ResearchJournalSearch(ResearchJournalSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new MasterSetupDAL().ResearchJournalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }



        #region Count


        [HttpPost]
        public JsonResult GetCountJobs(CountJobsEntity obj)
        {
            List<EntityCount> list = new CommonDAL().GetCountJobs(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }






        #endregion

        #region Applicant

        //LoginByPhfDeveloper

        [HttpGet]
        public JsonResult LoginByPhfDeveloper(string emailId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().LoginByPhfDeveloper(emailId, loggedInUser.typeId);
                if (app != null && app.applicantId > 0)
                {
                    app.adminId = loggedInUser.userId;
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoginByPhfDeveloperByApplicantId(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                if (app != null && app.applicantId > 0)
                {
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoginByPhfByApplicantId(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                if (app != null && app.applicantId > 0)
                {
                    app.adminId = loggedInUser.userId;
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantStatusUpdate(int applicantId)
        {
            Message msg = new ApplicantDAL().AccountStatusUpdate(applicantId, 1, loggedInUser.userId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantDebarStatusUpdate(ApplicantDebarData obj)
        {
            Message msg = new ApplicantDAL().ApplicantDebarStatusUpdate(obj.applicantId, obj.typeId, obj.image, loggedInUser.userId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SendAccountActivationEmail(int applicantId)
        {
            Message msg = FunctionUI.SendActivationEmail(applicantId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantUpdate(Applicant obj)
        {
            obj.adminId = loggedInUser.userId;

            Message msg = new ApplicantDAL().ApplicantUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ApplicantSendMessage(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

                string smsBody = "";
                int smsId = 0;
                try
                {
                    SMS sms = new SMSDAL().GetByTypeForApplicant(applicantId, ProjConstant.SMSType.sendPasswordAdmin);
                    smsBody = sms.detail;
                    smsId = sms.smsId;
                }
                catch (Exception)
                {
                    smsBody = "";
                }
                if (String.IsNullOrWhiteSpace(smsBody))
                {
                    smsId = 0;
                    smsBody = "Dear Applicant, Your current password  for PRP Induction January (2021) against emailId(" + applicant.emailId + ") is : " + applicant.passwordDecrypt + "";
                }
                msg = FunctionUI.SendSms(applicant.contactNumber, smsBody);

                try
                {
                    SmsProcess objProcess = msg.status.SmsProcessMakeDefaultObject(applicantId, smsId);
                    //new SMSDAL().AddUpdateSmsProcess(objProcess);
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ticker
        [CheckHasRight]
        public ActionResult TickerManage()
        {
            TickerModelAdmin model = new TickerModelAdmin();
            model.inductionId = Request.QueryString["inductionId"].TooInt();
            model.typeId = Request.QueryString["typeId"].TooInt();
            if (model.inductionId == 0)
                model.inductionId = ProjConstant.inductionId;


            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.tickerType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            if (model.typeId == 0)
                model.typeId = model.listType.FirstOrDefault().id;
            model.list = new MasterSetupDAL().TickerGetByInduction(model.inductionId, model.typeId);
            return View(model);
        }

        [CheckHasRight]
        public ActionResult TickerSetup()
        {
            TickerModelAdmin model = new TickerModelAdmin();
            int tickerId = Request.QueryString["id"].TooInt();
            if (tickerId > 0)
            {
                model.ticker = new MasterSetupDAL().TickerGetById(tickerId);
                model.ticker.detailShort = model.ticker.detail;
                model.ticker.detailLong = model.ticker.detail;
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.tickerType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TickerSaveData(TickerModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Ticker obj = ModelSave.ticker;
            obj.name = obj.name.TooString();
            if (obj.typeId == 1)
                obj.detail = obj.detailShort.TooString();
            else obj.detail = obj.detailLong.TooString();
            obj.adminId = loggedInUser.userId;
            if (obj.inductionId == 0) obj.inductionId = ProjConstant.inductionId;
            Message m = new MasterSetupDAL().TickerAddUpdate(obj);
            return Redirect("/admin/ticker-manage?inductionId=" + obj.inductionId + "&typeId=" + obj.typeId);
        }

        #endregion

        #region Department

        [CheckHasRight]
        public ActionResult DepartmentManage()
        {
            DepartmentModelAdmin model = new DepartmentModelAdmin();
            model.typeId = Request.QueryString["typeId"].TooInt();
            model.listType = new ConstantDAL().GetAll(0).OrderBy(x => x.id).ToList();
            model.list = new CommonDAL().DepartmentGetAll(model.typeId).OrderBy(x => x.name).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult DepartmentSetup()
        {
            DepartmentModelAdmin model = new DepartmentModelAdmin();
            int departmentId = Request.QueryString["id"].TooInt();
            if (departmentId > 0)
                model.department = new CommonDAL().DepartmentGetById(departmentId);

            model.listType = new ConstantDAL().GetAll(0);

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult SaveDepartmentData(DepartmentModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Department obj = ModelSave.department;
            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new CommonDAL().DepartmentAddUpdate(obj);

            return Redirect(ModelSave.redirectUrl);
        }



        #endregion

    }
}