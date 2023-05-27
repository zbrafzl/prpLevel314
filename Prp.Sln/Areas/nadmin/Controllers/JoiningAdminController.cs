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
	public class JoiningAdminController : BaseAdminController
	{
		public JoiningAdminController()
		{
		}

		public ActionResult ApplicantJoined()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.degreeType,
				condition = "ByType"
			};
			applicantJoiningAdminModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(applicantJoiningAdminModel);
		}

		[HttpPost]
		public JsonResult ApplicantJoiningAddUpdate(ApplicantJoined objJoin)
		{
			Message message = new Message();
			objJoin.adminId = base.loggedInUser.userId;
			try
			{
				objJoin.joiningDate = objJoin.dateJoining.TooDate();
				message = (new JoiningDAL()).AddUpdate(objJoin);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			return base.Json(message, 0);
		}

		[CheckHasRight]
		public ActionResult ApplicantJoiningFullList()
		{
			return View(new ApplicantJoiningAdminModel());
		}

		public ActionResult ApplicantJoiningListHospInst()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel()
			{
				instituteId = Request.QueryString["instituteId"].TooInt(),
				hospitalId = Request.QueryString["hospitalId"].TooInt()
			};
			if (applicantJoiningAdminModel.instituteId > 0)
			{
				applicantJoiningAdminModel.listApplicant = (new JoiningDAL()).GetAllByInstiteUser(ProjConstant.inductionId, base.loggedInUser.userId, applicantJoiningAdminModel.instituteId, "");
			}
			else if (applicantJoiningAdminModel.hospitalId > 0)
			{
				applicantJoiningAdminModel.listApplicant = (new JoiningDAL()).GetAllByHospitalUser(base.loggedInUser.userId, applicantJoiningAdminModel.hospitalId);
			}
			return View(applicantJoiningAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantListFinal()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel();
			try
			{
				applicantJoiningAdminModel.listApplicant = (new JoiningDAL()).GetAllByHospitalUser(base.loggedInUser.userId, 0);
			}
			catch (Exception exception)
			{
			}
			return View(applicantJoiningAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantTakeJoining()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel();
			int num = Request.QueryString["applicantId"].TooInt();
			if (num > 0)
			{
				ApplicantJoined byApplicantById = (new JoiningDAL()).GetByApplicantById(num, ProjConstant.inductionId);
				applicantJoiningAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
				applicantJoiningAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, ProjConstant.phaseId, num);
				if ((byApplicantById == null ? true : byApplicantById.joiningId <= 0))
				{
					applicantJoiningAdminModel.isExist = false;
					applicantJoiningAdminModel.joinApplicant = (new JoiningDAL()).GetApplicantByInstiteUser(ProjConstant.inductionId, base.loggedInUser.userId, num);
				}
				else
				{
					applicantJoiningAdminModel.isExist = true;
				}
			}
			return View(applicantJoiningAdminModel);
		}

		public ActionResult ApplicantTransfer()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.degreeType,
				condition = "ByType"
			};
			applicantJoiningAdminModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(applicantJoiningAdminModel);
		}

		[HttpPost]
		public ActionResult ExportDataToExcelAndDownload(JoiningApplicantSearch obj)
		{
			Message message = new Message();
			if (obj.inductionId.TooInt() == 0)
			{
				obj.inductionId = ProjConstant.inductionId;
			}
			obj.typeId = obj.typeId.TooInt();
			obj.search = obj.search.TooString("");
			int num = obj.inductionId;
			string str = string.Concat("JoiningList", num.ToString(), ".xlsx");
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
					DataTable dataTable = (new JoiningDAL()).JoiningSearch(obj);
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

		[HttpGet]
		public ActionResult GetAllByInstiteUser(string search)
		{
			JoiningApplicantSearch joiningApplicantSearch = new JoiningApplicantSearch()
			{
				inductionId = ProjConstant.inductionId,
				instituteId = 0,
				userId = base.loggedInUser.userId,
				search = search.TooString("")
			};
			DataTable dataTable = (new JoiningDAL()).JoiningSearchGetByInstitute(joiningApplicantSearch);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[HttpGet]
		public JsonResult GetJoiningAll(int top, int pageNo, int joinStatus, string search)
		{
			List<ApplicantJoined> joiningAll = (new JoiningDAL()).GetJoiningAll(top, pageNo, joinStatus, search);
			return base.Json(joiningAll, 0);
		}

		[HttpPost]
		public ActionResult JoiningSearch(JoiningApplicantSearch obj)
		{
			bool flag;
			if (obj.inductionId.TooInt() != 0)
			{
				flag = false;
			}
			else
			{
				flag = (base.loggedInUser.typeId == 81 ? true : base.loggedInUser.typeId == 86);
			}
			if (flag)
			{
				obj.inductionId = ProjConstant.inductionId;
			}
			obj.typeId = obj.typeId.TooInt();
			obj.search = obj.search.TooString("");
			DataTable dataTable = (new JoiningDAL()).JoiningSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}
	}
}