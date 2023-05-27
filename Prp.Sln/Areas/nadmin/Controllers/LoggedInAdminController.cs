using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class LoggedInAdminController : Controller
	{
		public LoggedInAdminController()
		{
		}

		[HttpPost]
		public JsonResult LoggedInUser(LoginUser login)
		{
			Message message = new Message();
			User user = ProjFunctions.CookiesAdminGet();
			if (user != null)
			{
				message.id = user.userId;
				message.status = true;
			}
			else
			{
				try
				{
					user = (new UserDAL()).Login(login.userName, login.password);
					if ((user == null ? false : user.userId > 0))
					{
						if (user.typeId == ProjConstant.Constant.UserType.institute)
						{
							Institute byUserId = (new InstitueDAL()).GetByUserId(user.userId);
							user.reffId = byUserId.instituteId;
							user.displayName = byUserId.name;
						}
						else if (user.typeId != ProjConstant.Constant.UserType.hospital)
						{
							user.reffId = 0;
							user.displayName = string.Concat(new string[] { user.firstName, " ", user.lastName, " - ", user.typeName });
						}
						else
						{
							Hospital hospital = (new HospitalDAL()).GetByUserId(user.userId);
							user.reffId = hospital.hospitalId;
							user.displayName = hospital.name;
						}
						user.displayName = user.displayName.TooString("");
						ProjFunctions.CookiesAdminSet(user);
						message.id = user.userId;
						message.status = true;
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					message.id = 0;
					message.status = false;
					message.msg = exception.Message;
				}
			}
			return base.Json(message, 0);
		}

		public ActionResult Login()
		{
			ActionResult actionResult;
			User user = ProjFunctions.CookiesAdminGet();
			if ((user == null ? true : user.userId <= 0))
			{
				actionResult = base.View();
			}
			else
			{
				actionResult = (user.typeId != ProjConstant.Constant.UserType.bank ? this.Redirect("/admin/index") : this.Redirect("/admin/voucher-list-bank"));
			}
			return actionResult;
		}

		public ActionResult Logout()
		{
			try
			{
				ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInAdmin);
			}
			catch (Exception exception)
			{
			}
			return this.Redirect("/admin");
		}

		public ActionResult NoRights()
		{
			return View();
		}
	}
}