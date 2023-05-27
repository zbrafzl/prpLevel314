using Prp.Data;
using Prp.Sln;
using Prp.Sln.Areas.nadmin;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class HardshipController : BaseAdminController
	{
		public HardshipController()
		{
		}

		[CheckHasRight]
		public ActionResult ApplicantDetail()
		{
			HardshipAdminModel hardshipAdminModel = new HardshipAdminModel();
			try
			{
				int inductionId = AdminHelper.GetInductionId();
				AdminHelper.GetPhaseId();
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					hardshipAdminModel.applicantId = 0;
					hardshipAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						hardshipAdminModel = AdminFunctions.GenerateModelHardship(num);
						hardshipAdminModel.listInduction = Prp.Sln.DDLInduction.GetAll("All");
						hardshipAdminModel.applicantId = num;
						hardshipAdminModel.search.key = str;
						hardshipAdminModel.search.@value = str1;
					}
					hardshipAdminModel.applicantId = num;
					hardshipAdminModel.requestType = 1;
				}
				hardshipAdminModel.inductionId = inductionId;
				hardshipAdminModel.search.key = str;
				hardshipAdminModel.search.@value = str1;
			}
			catch (Exception exception)
			{
				hardshipAdminModel.applicantId = 0;
				hardshipAdminModel.requestType = 3;
			}
			return View(hardshipAdminModel);
		}
	}
}