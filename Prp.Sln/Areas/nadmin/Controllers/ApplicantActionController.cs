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
    public class ApplicantActionController : BaseAdminController
    {
        public ActionResult ActionSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            if (url.Contains("reg-term") )
            {
                model.typeId = 0;
                model.heading = "Resignation/Termination";
            }
            else if (url.Contains("freez"))
            {
                model.typeId = ProjConstant.Constant.ActionStatusType.freez;
                model.heading = "Freez";
            }
            model.applicantId = Request.QueryString["applicantId"].TooInt();

            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);

                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                }
            }
            return View(model);
        }

        public ActionResult LeaveSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            model.typeId = 0;
            model.heading = "Leave Application";
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);
                model.leaveDataList = new ActionDAL().getLeaveDataList(model.applicantId);
                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                }
            }
            model.typeId = 0;
            return View(model);
        }

        public ActionResult ExtensionSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            model.typeId = 0;
            model.heading = "Extension Application";
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);
                model.extensionDataList = new ActionDAL().getExtensionDataList(model.applicantId);
                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                }
            }
            model.typeId = 0;
            return View(model);
        }

        public ActionResult LeaveApprovalSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            model.typeId = 0;
            model.heading = "Leave Approval";
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            model.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);

                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                    ApplicantLeaveAction data = new ActionDAL().getLeaveData(model.applicantId, model.applicantLeaveId);
                    List<ApplicantLeaveAction> ldList = new ActionDAL().getLeaveDataList(model.applicantId);
                    //ApplicantLeaveAction ld = new ApplicantLeaveAction();
                    //ld.typeId = data.typeId;
                    //ld.startDate = data.startDate;
                    //ld.endDate = data.endDate;
                    //ld.requestedBy = data.requestedBy;
                    //ld.remarksRequested = data.remarksRequested;
                    //ld.image = data.image;
                    //ld.imageAffidavit = data.imageAffidavit;
                    //ldList.Add(data);
                    model.leaveDataList = ldList;
                    model.leaveData = data;
                    //ApplicantLeaveAction ld = new ActionDAL.getLeaveData(model.applicantId, model.leaveTypeId);
                }
            }
            model.typeId = 0;
            return View(model);
        }

        public ActionResult LeavePrintSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            model.typeId = 0;
            model.heading = "Leave Approval";
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            model.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);

                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                    ApplicantLeaveAction data = new ActionDAL().getLeaveData(model.applicantId, model.applicantLeaveId);
                    List<ApplicantLeaveAction> ldList = new ActionDAL().getLeaveDataList(model.applicantId);
                    //ApplicantLeaveAction ld = new ApplicantLeaveAction();
                    //ld.typeId = data.typeId;
                    //ld.startDate = data.startDate;
                    //ld.endDate = data.endDate;
                    //ld.requestedBy = data.requestedBy;
                    //ld.remarksRequested = data.remarksRequested;
                    //ld.image = data.image;
                    //ld.imageAffidavit = data.imageAffidavit;
                    //ldList.Add(data);
                    model.leaveDataList = ldList;
                    model.leaveData = data;
                    //ApplicantLeaveAction ld = new ActionDAL.getLeaveData(model.applicantId, model.leaveTypeId);
                }
            }
            model.typeId = 0;
            return View(model);
        }

        public ActionResult ActionLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            if (url.Contains("reg-term"))
            {
                model.typeId = 0;
            }
            else if (url.Contains("freez"))
            {
                model.typeId = ProjConstant.Constant.ActionStatusType.freez;
                model.heading = "Freez";
            }
            return View( model);
        }

        public ActionResult LeavesLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            model.typeId = 0;
            model.instituteId = loggedInUser.reffId;
            return View(model);
        }

        public ActionResult ExtenstionLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            model.typeId = 0;
            model.instituteId = loggedInUser.reffId;
            return View(model);
        }

        public ActionResult ExtensionApprovalLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            model.typeId = 0;
            model.instituteId = loggedInUser.reffId;
            return View(model);
        }


        public ActionResult LeavesApprovalLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            model.typeId = 0;
            model.instituteId = loggedInUser.reffId;
            return View(model);
        }

        [HttpPost]
        public ActionResult ActionSearch(SearchReport obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            DataTable dataTable = new ReportDAL().ActionSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult LeaveSearch(SearchReport obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            DataTable dataTable = new ReportDAL().LeavesSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult LeaveApprovalSearch(SearchReport obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            DataTable dataTable = new ReportDAL().LeavesApprovalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        public ActionResult ExtensionApprovalSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            model.typeId = 0;
            model.heading = "Extension Approval";
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            model.applicantLeaveId = Request.QueryString["applicantLeaveId"].TooInt();
            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);

                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                    ApplicantExtensionAction data = new ActionDAL().getExtensionData(model.applicantId, model.applicantLeaveId);
                    List<ApplicantExtensionAction> ldList = new ActionDAL().getExtensionDataList(model.applicantId);
                    //ApplicantLeaveAction ld = new ApplicantLeaveAction();
                    //ld.typeId = data.typeId;
                    //ld.startDate = data.startDate;
                    //ld.endDate = data.endDate;
                    //ld.requestedBy = data.requestedBy;
                    //ld.remarksRequested = data.remarksRequested;
                    //ld.image = data.image;
                    //ld.imageAffidavit = data.imageAffidavit;
                    //ldList.Add(data);
                    model.extensionDataList = ldList;
                    model.extensionData = data;
                    //ApplicantLeaveAction ld = new ActionDAL.getLeaveData(model.applicantId, model.leaveTypeId);
                }
            }
            model.typeId = 0;
            return View(model);
        }
        [HttpPost]
        public JsonResult ExtensionApprovalSetupSave(ApplicantLeaveAction ac)
        {
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateExtensionApproval(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExtensionApprovalSearch(SearchReport obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            DataTable dataTable = new ReportDAL().ExtensionApprovalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public JsonResult LeaveSetupSave(ApplicantLeaveAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!String.IsNullOrWhiteSpace(ac.dateStart))
                ac.startDate = ac.dateStart.TooDate();
            if (!String.IsNullOrWhiteSpace(ac.dateEnd))
                ac.endDate = ac.dateEnd.TooDate();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateLeave(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ExtensionSetupSave(ApplicantExtensionAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!String.IsNullOrWhiteSpace(ac.dateStart))
                ac.startDate = ac.dateStart.TooDate();
            if (!String.IsNullOrWhiteSpace(ac.dateEnd))
                ac.endDate = ac.dateEnd.TooDate();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateExtension(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LeaveApprovalSetupSave(ApplicantLeaveAction ac)
        {
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateLeaveApproval(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActionSetupSave(ApplicantAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!String.IsNullOrWhiteSpace(ac.dateStart))
                ac.startDate = ac.dateStart.TooDate();
            if (!String.IsNullOrWhiteSpace(ac.dateEnd))
                ac.endDate = ac.dateEnd.TooDate();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdate(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActionUpdateStatus(ApplicantAction ac)
        {
            ac.image = ac.image.TooString();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateStatus(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}