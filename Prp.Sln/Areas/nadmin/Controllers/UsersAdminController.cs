using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class UsersAdminController : BaseAdminController
	{
		public UsersAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult ChangePassword()
		{
			return View(new UserSetupModel());
		}

		[ValidateInput(false)]
		public ActionResult ChangePasswordData(UserSetupModel ModelSave, HttpPostedFileBase files)
		{
			ActionResult actionResult;
			User modelSave = ModelSave.user;
			modelSave.userId = base.loggedInUser.userId;
			modelSave.password = modelSave.password.TooString("");
			modelSave.passwordNew = modelSave.passwordNew.TooString("");
			actionResult = (!(new UserDAL()).UpdatePassword(modelSave).status ? this.Redirect("/admin/change-password") : this.Redirect("/admin/logout"));
			return actionResult;
		}

		[CheckHasRight]
		public ActionResult Manage()
		{
			UserManageModel userManageModel = new UserManageModel()
			{
				typeId = Request.QueryString["typeId"].TooInt()
			};
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.userType
			};
			userManageModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			if (userManageModel.typeId == 0)
			{
				userManageModel.typeId = userManageModel.listType.FirstOrDefault<EntityDDL>().id.TooInt();
			}
			userManageModel.listUser = (new UserDAL()).GetByTypeId(userManageModel.typeId);
			return View(userManageModel);
		}

		[ValidateInput(false)]
		public ActionResult SaveUserData(UserSetupModel ModelSave, HttpPostedFileBase files)
		{
			User modelSave = ModelSave.user;
			modelSave.userId = modelSave.userId.TooInt();
			modelSave.typeId = modelSave.typeId.TooInt();
			modelSave.parentId = new int?(modelSave.parentId.TooInt());
			modelSave.firstName = modelSave.firstName.TooString("");
			modelSave.lastName = modelSave.lastName.TooString("");
			modelSave.emailId = modelSave.emailId.TooString("");
			modelSave.password = modelSave.password.TooString("");
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			(new UserDAL()).AddUpdate(modelSave);
			int num = modelSave.typeId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/user-manage?typeId=", num.ToString()));
			return actionResult;
		}

		[CheckHasRight]
		public ActionResult Setup()
		{
			UserSetupModel userSetupModel = new UserSetupModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.userType
			};
			userSetupModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.department
			};
			userSetupModel.listDepartment = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.designantion
			};
			userSetupModel.listDesgnation = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				userSetupModel.user = (new UserDAL()).GetById(num);
			}
			return View(userSetupModel);
		}
	}
}