using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class PrintsController : BaseController
	{
		public PrintsController()
		{
		}

		public ActionResult Application()
		{
			return View();
		}

		public ActionResult ApplicationTest()
		{
			return View();
		}

		public ActionResult GrievanceGazzettePrint()
		{
			ContactModel contactModel = new ContactModel();
			string absolutePath = HttpContext.Request.Url.AbsolutePath;
			if (absolutePath == "/print/grievance-verification")
			{
				contactModel.typeId = 11;
			}
			else if (absolutePath == "/print/grievance-gazzette")
			{
				contactModel.typeId = 21;
			}
			contactModel.listQuestion = (new ContactDAL()).GetQuestionByApplicant(contactModel.typeId, ProjConstant.inductionId, base.loggedInUser.applicantId);
			contactModel.contact = (
				from x in contactModel.listQuestion
				where (x.typeId != contactModel.typeId ? false : x.totalReply == 0)
				select x).FirstOrDefault<Contact>();
			contactModel.applicant = (new ApplicantDAL()).GetApplicant(ProjConstant.inductionId, base.loggedInUser.applicantId);
			try
			{
				contactModel.listStatusApproval = (new VerificationDAL()).GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, base.loggedInUser.applicantId);
			}
			catch (Exception exception)
			{
			}
			return View(contactModel);
		}

		public ActionResult Vouchers()
		{
			return View(new VoucherModel());
		}
	}
}