using Prp.Data;
using Prp.Sln;
using System;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class HomeController : BaseController
	{
		public HomeController()
		{
		}

		public ActionResult ChangePassword()
		{
			return View(new HomeModel());
		}

		[HttpPost]
		public JsonResult ChangePasswordEvent(ChangePassword obj)
		{
			obj.id = base.loggedInUser.applicantId;
			Message message = (new ApplicantDAL()).ChangePassword(obj);
			return base.Json(message, 0);
		}

		public ActionResult Index()
		{
			return View(new HomeModel());
		}

		public ActionResult NoRights()
		{
			return View(new HomeModel());
		}

		[CheckHasRight]
		public ActionResult TestRighsPage()
		{
			return View(new HomeModel());
		}
	}
}