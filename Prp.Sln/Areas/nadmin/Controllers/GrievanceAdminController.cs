using Newtonsoft.Json;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class GrievanceAdminController : BaseAdminController
	{
		public GrievanceAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult GrievanceAttendence()
		{
			GrievanceAdminModel grievanceAdminModel = new GrievanceAdminModel()
			{
				searchTxt = Request.QueryString["search"].TooString("")
			};
			return View(grievanceAdminModel);
		}

		public ActionResult GrievanceAttendenceMark()
		{
			GrievanceActionModel grievanceActionModel = new GrievanceActionModel();
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				grievanceActionModel.grievance = (new GrievanceDAL()).GetById(num);
				grievanceActionModel.action = (new GrievanceDAL()).ActionGetById(num);
				grievanceActionModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, grievanceActionModel.grievance.applicantId);
			}
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.guardianType
			};
			grievanceActionModel.listRelation = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(grievanceActionModel);
		}

		public ActionResult GrievanceAttendenceRemarks()
		{
			GrievanceRemarkModel grievanceRemarkModel = new GrievanceRemarkModel();
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				grievanceRemarkModel.action = (new GrievanceDAL()).ActionGetById(num);
				grievanceRemarkModel.grievance = (new GrievanceDAL()).GetById(num);
				grievanceRemarkModel.remark = (new GrievanceDAL()).RemarksGetById(num);
				grievanceRemarkModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, grievanceRemarkModel.grievance.applicantId);
			}
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.guardianType
			};
			grievanceRemarkModel.listRelation = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.grievanceStatusType
			};
			grievanceRemarkModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(grievanceRemarkModel);
		}

		public ActionResult GrievanceGazetteMarksDetail()
		{
			GrievanceAdminModel grievanceAdminModel = new GrievanceAdminModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.appearanceType
			};
			grievanceAdminModel.listAppearanceType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.guardianType
			};
			grievanceAdminModel.listGuardian = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.grievanceActionType
			};
			grievanceAdminModel.listActionType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			grievanceAdminModel.grievanceId = Request.QueryString["grievanceId"].TooInt();
			grievanceAdminModel.appearanceId = Request.QueryString["appearanceId"].TooInt();
			try
			{
				grievanceAdminModel.grievance = (new GrievanceDAL()).GetById(grievanceAdminModel.grievanceId);
				grievanceAdminModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, grievanceAdminModel.grievance.applicantId);
			}
			catch (Exception exception)
			{
				grievanceAdminModel = new GrievanceAdminModel();
			}
			return View(grievanceAdminModel);
		}

		[CheckHasRight]
		public ActionResult GrievanceManage()
		{
			return View(new GrievanceAdminModel());
		}

		[CheckHasRight]
		public ActionResult GrievancePrint()
		{
			GrievanceAdminModel grievanceAdminModel = new GrievanceAdminModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.grievanceType
			};
			grievanceAdminModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(grievanceAdminModel);
		}

		[HttpPost]
		public ActionResult GrievanceSearch(Grievance obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new GrievanceDAL()).GrievanceSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult GrievanceSearchAttendence(Grievance obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new GrievanceDAL()).GrievanceSearchAttendence(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpPost]
		public ActionResult GrievanceSearchPrint(Grievance obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			obj.dated = obj.datedStr.TooDate();
			DataTable dataTable = (new GrievanceDAL()).GrievancePrint(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[ValidateInput(false)]
		public ActionResult SaveGrievanceAppearanceData(GrievanceAdminModel ModelSave, HttpPostedFileBase files)
		{
			Grievance modelSave = ModelSave.appearance;
			modelSave.grievanceId = modelSave.grievanceId.TooInt();
			modelSave.attendanceNo = modelSave.attendanceNo.TooInt();
			modelSave.appearanceTypeId = modelSave.appearanceTypeId.TooInt();
			modelSave.guardianId = modelSave.guardianId.TooInt();
			modelSave.guardianName = modelSave.guardianName.TooString("");
			modelSave.guardianContactNumber = modelSave.guardianContactNumber.TooString("");
			modelSave.actionTypeId = modelSave.actionTypeId.TooInt();
			modelSave.actionComments = modelSave.actionComments.TooString("");
			modelSave.datedAppearance = modelSave.appearanceDate.TooDate('/');
			modelSave.adminIdAppearance = base.loggedInUser.userId;
			modelSave.adminIdStatus = 0;
			modelSave.statusId = 0;
			modelSave.datedStatus = DateTime.Now;
			int num = modelSave.grievanceId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/grievance-marks-detail?grievanceId=", num.ToString()));
			return actionResult;
		}

		[HttpPost]
		public JsonResult SaveGrievanceAttendence(GrievanceAction obj)
		{
			Message message = new Message();
			try
			{
				obj.grievanceId = obj.grievanceId.TooInt();
				obj.grievanceActionId = obj.grievanceActionId.TooInt();
				obj.isSelf = obj.isSelf.TooInt();
				obj.relationId = obj.relationId.TooInt();
				obj.adminIdAttendece = base.loggedInUser.userId;
				message = (new GrievanceDAL()).ActionAddUpdate(obj);
			}
			catch (Exception exception)
			{
				message.status = false;
			}
			return base.Json(message, 0);
		}

		[ValidateInput(false)]
		public ActionResult SaveGrievanceAttendenceData(GrievanceActionModel ModelSave, HttpPostedFileBase files)
		{
			GrievanceAction modelSave = ModelSave.action;
			modelSave.grievanceId = modelSave.grievanceId.TooInt();
			modelSave.grievanceActionId = modelSave.grievanceActionId.TooInt();
			modelSave.isSelf = modelSave.isSelf.TooInt();
			modelSave.relationId = modelSave.relationId.TooInt();
			modelSave.adminIdAttendece = base.loggedInUser.userId;
			(new GrievanceDAL()).ActionAddUpdate(modelSave);
			int num = ModelSave.applicant.applicantId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/grievance-attendence?search=", num.ToString()));
			return actionResult;
		}

		[HttpPost]
		public JsonResult SaveGrievanceRemarks(GrievanceRemark obj)
		{
			Message message = new Message();
			try
			{
				obj.grievanceId = obj.grievanceId.TooInt();
				obj.grievanceRemarksId = obj.grievanceRemarksId.TooInt();
				obj.typeId = obj.typeId.TooInt();
				obj.remarks = obj.remarks.TooString("");
				obj.title = obj.title.TooString("");
				obj.adminId = base.loggedInUser.userId;
				message = (new GrievanceDAL()).RemarksAddUpdate(obj);
			}
			catch (Exception exception)
			{
				message.status = false;
			}
			return base.Json(message, 0);
		}
	}
}