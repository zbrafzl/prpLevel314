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
	public class PrintAdminController : BaseAdminController
	{
		public PrintAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult PrintApplicantAttachedHospital()
		{
			ReportApplicantModel reportApplicantModel = new ReportApplicantModel()
			{
				hospitalId = Request.QueryString["hospitalId"].TooInt(),
				attachStatusId = Request.QueryString["attachStatusId"].TooInt(),
				inductionId = Request.QueryString["inductionId"].TooInt(),
				specialityId = Request.QueryString["specialityId"].TooInt(),
				typeId = Request.QueryString["typeId"].TooInt()
			};
			return View(reportApplicantModel);
		}

		public ActionResult PrintJoiningApplicantInstitute()
		{
			ApplicantJoiningAdminModel applicantJoiningAdminModel = new ApplicantJoiningAdminModel();
			List<ApplicantJoined> allByInstiteUser = (new JoiningDAL()).GetAllByInstiteUser(ProjConstant.inductionId, base.loggedInUser.userId, 0, "");
			applicantJoiningAdminModel.listApplicant = (
				from x in allByInstiteUser
				where x.joiningId > 0
				select x).ToList<ApplicantJoined>();
			return View(applicantJoiningAdminModel);
		}
	}
}