using Prp.Data;
using Prp.Sln;
using System;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class BaseLoginController : Controller
	{
		public Applicant loggedInUser
		{
			get;
			set;
		}

		public BaseLoginController()
		{
			this.loggedInUser = ProjFunctions.CookieApplicantGet();
		}
	}
}