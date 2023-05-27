using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class MenusAdminController : BaseAdminController
	{
		public MenusAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult AccessUser()
		{
			MenuAccessModel menuAccessModel = new MenuAccessModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.userType
			};
			menuAccessModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(menuAccessModel);
		}

		[CheckHasRight]
		public ActionResult AccessUserType()
		{
			MenuAccessModel menuAccessModel = new MenuAccessModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.userType
			};
			menuAccessModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			return View(menuAccessModel);
		}

		[HttpGet]
		public JsonResult GetMenuAccessListForUser(int userId)
		{
			List<Menu> list = (
				from x in (new MenuDAL()).GetMenuListForUserId(userId)
				orderby x.nameDisplay
				select x).ToList<Menu>();
			return base.Json(list, 0);
		}

		[HttpGet]
		public JsonResult GetMenuAccessListForUserType(int typeId)
		{
			List<Menu> list = (
				from x in (new MenuDAL()).GetMenuListForUserType(typeId)
				orderby x.nameDisplay
				select x).ToList<Menu>();
			return base.Json(list, 0);
		}

		[CheckHasRight]
		public ActionResult Manage()
		{
			MenuManageModel menuManageModel = new MenuManageModel()
			{
				appId = Request.QueryString["appId"].TooInt(),
				parentId = Request.QueryString["parentId"].TooInt()
			};
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.appType
			};
			menuManageModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			if (menuManageModel.appId == 0)
			{
				menuManageModel.appId = 2;
			}
			menuManageModel.listParent = (new MenuDAL()).GetParent(menuManageModel.appId);
			menuManageModel.listMenu = (new MenuDAL()).Search(menuManageModel.appId, menuManageModel.parentId);
			return View(menuManageModel);
		}

		[ValidateInput(false)]
		public ActionResult SaveMenuData(MenuSetupModel ModelSave, HttpPostedFileBase files)
		{
			Menu modelSave = ModelSave.menu;
			modelSave.menuId = modelSave.menuId.TooInt();
			modelSave.name = modelSave.name.TooString("");
			modelSave.nameDisplay = modelSave.nameDisplay.TooString("");
			modelSave.url = modelSave.url.TooString("");
			modelSave.iconClass = modelSave.iconClass.TooString("");
			modelSave.isMenu = modelSave.isMenu.TooBoolean(false);
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.typeId = modelSave.typeId.TooInt();
			modelSave.appId = modelSave.appId.TooInt();
			modelSave.parentId = modelSave.parentId.TooInt();
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			(new MenuDAL()).AddUpdate(modelSave);
			int num = modelSave.appId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/menu-manage?typeId=", num.ToString()));
			return actionResult;
		}

		[CheckHasRight]
		public ActionResult Setup()
		{
			MenuSetupModel menuSetupModel = new MenuSetupModel();
			DDLConstants dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.menuType
			};
			menuSetupModel.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				typeId = ProjConstant.Constant.appType
			};
			menuSetupModel.listApp = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			DDLMenu dDLMenu = new DDLMenu()
			{
				condition = "GetParent"
			};
			menuSetupModel.listParent = (new MenuDAL()).GetMenuForDDL(dDLMenu);
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				menuSetupModel.menu = (new MenuDAL()).GetById(num);
			}
			return View(menuSetupModel);
		}

		[HttpGet]
		public JsonResult UserAccessAddUpdate(int userId, string menuIds)
		{
			menuIds = menuIds.TooString("").TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' });
			Message message = (new MenuDAL()).AddUpdateUserAccess(userId, menuIds);
			return base.Json(message, 0);
		}

		[HttpGet]
		public JsonResult UserTypeAccessAddUpdate(int typeId, string menuIds)
		{
			menuIds = menuIds.TooString("").TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' });
			Message message = (new MenuDAL()).AddUpdateUserTypeAccess(typeId, menuIds);
			return base.Json(message, 0);
		}
	}
}