using Prp.Data;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class FeedbackAdminController : BaseAdminController
	{
		public FeedbackAdminController()
		{
		}

		public ActionResult FeebBackList()
		{
			FeedbackAdminModel feedbackAdminModel = new FeedbackAdminModel();
			FeedbackSearch feedbackSearch = new FeedbackSearch();
			int num = Request.QueryString["top"].TooInt();
			int num1 = Request.QueryString["page"].TooInt();
			string str = Request.QueryString["search"].TooString("");
			string str1 = Request.QueryString["start"].TooString("");
			string str2 = Request.QueryString["end"].TooString("");
			if (string.IsNullOrWhiteSpace(str1))
			{
				str1 = "21-09-2020";
			}
			if (string.IsNullOrWhiteSpace(str2))
			{
				str2 = str1;
			}
			feedbackSearch.search = str;
			feedbackSearch.startDate = str1.TooDate('-');
			feedbackSearch.endDate = str2.TooDate('-');
			feedbackSearch.top = num;
			feedbackSearch.pageNum = num1;
			if (num == 0)
			{
				feedbackSearch.top = 5000;
			}
			if (num1 == 0)
			{
				feedbackSearch.pageNum = 1;
			}
			feedbackAdminModel.listFeedback = (new FeedBackDAL()).GetBySearch(feedbackSearch);
			return View(feedbackAdminModel);
		}
	}
}