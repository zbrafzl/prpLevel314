using Prp.Data;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class MeritAdminController : BaseAdminController
	{
		public MeritAdminController()
		{
		}

		[CheckHasRight]
		public ActionResult ApplicantMeritRound2()
		{
			MeritApplicantAdminModel meritApplicantAdminModel = new MeritApplicantAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				meritApplicantAdminModel.search.key = str;
				meritApplicantAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					meritApplicantAdminModel.applicantId = 0;
					meritApplicantAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 2;
						int num2 = ProjConstant.inductionId;
						int num3 = ProjConstant.phaseId;
						try
						{
							if (RouteData.Values["induction"].TooInt() > 0)
							{
								num2 = RouteData.Values["induction"].TooInt();
							}
						}
						catch (Exception exception)
						{
							num2 = ProjConstant.inductionId;
						}
						meritApplicantAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
						meritApplicantAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, 0, num);
						meritApplicantAdminModel.degree = (new ApplicantDAL()).GetApplicantDegreeDetail(0, 0, num);
						meritApplicantAdminModel.listSpecilityMerit = (new MeritDAL()).GetApplicantSpecialityWithMeritMarks(num2, num, num1);
						meritApplicantAdminModel.search.key = str;
						meritApplicantAdminModel.search.@value = str1;
						DDLConstants dDLConstant = new DDLConstants()
						{
							condition = "ByType",
							typeId = ProjConstant.Constant.degreeType
						};
						meritApplicantAdminModel.listType = (
							from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
							orderby x.id
							select x).ToList<EntityDDL>();
					}
					meritApplicantAdminModel.applicantId = num;
					meritApplicantAdminModel.requestType = 1;
				}
			}
			catch (Exception exception1)
			{
				meritApplicantAdminModel.applicantId = 0;
				meritApplicantAdminModel.requestType = 3;
			}
			return View(meritApplicantAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantMeritRound3()
		{
			MeritApplicantAdminModel meritApplicantAdminModel = new MeritApplicantAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				meritApplicantAdminModel.search.key = str;
				meritApplicantAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					meritApplicantAdminModel.applicantId = 0;
					meritApplicantAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 3;
						int num2 = ProjConstant.inductionId;
						int num3 = ProjConstant.phaseId;
						try
						{
							if (RouteData.Values["induction"].TooInt() > 0)
							{
								num2 = RouteData.Values["induction"].TooInt();
							}
						}
						catch (Exception exception)
						{
							num2 = ProjConstant.inductionId;
						}
						if (num2 == 0)
						{
							num2 = ProjConstant.inductionId;
						}
						meritApplicantAdminModel.inductionId = num2;
						meritApplicantAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
						meritApplicantAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, 0, num);
						meritApplicantAdminModel.degree = (new ApplicantDAL()).GetApplicantDegreeDetail(0, 0, num);
						meritApplicantAdminModel.listSpecilityMerit = (new MeritDAL()).GetApplicantSpecialityWithMeritMarks(num2, num, num1);
						meritApplicantAdminModel.search.key = str;
						meritApplicantAdminModel.search.@value = str1;
						DDLConstants dDLConstant = new DDLConstants()
						{
							condition = "ByType",
							typeId = ProjConstant.Constant.degreeType
						};
						meritApplicantAdminModel.listType = (
							from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
							orderby x.id
							select x).ToList<EntityDDL>();
					}
					meritApplicantAdminModel.applicantId = num;
					meritApplicantAdminModel.requestType = 1;
				}
			}
			catch (Exception exception1)
			{
				meritApplicantAdminModel.applicantId = 0;
				meritApplicantAdminModel.requestType = 3;
			}
			return View(meritApplicantAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantMeritRound4()
		{
			MeritApplicantAdminModel meritApplicantAdminModel = new MeritApplicantAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				meritApplicantAdminModel.search.key = str;
				meritApplicantAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					meritApplicantAdminModel.applicantId = 0;
					meritApplicantAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 4;
						int num2 = ProjConstant.inductionId;
						int num3 = ProjConstant.phaseId;
						try
						{
							if (RouteData.Values["induction"].TooInt() > 0)
							{
								num2 = RouteData.Values["induction"].TooInt();
							}
						}
						catch (Exception exception)
						{
							num2 = ProjConstant.inductionId;
						}
						if (num2 == 0)
						{
							num2 = ProjConstant.inductionId;
						}
						meritApplicantAdminModel.inductionId = num2;
						meritApplicantAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
						meritApplicantAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, 0, num);
						meritApplicantAdminModel.degree = (new ApplicantDAL()).GetApplicantDegreeDetail(0, 0, num);
						meritApplicantAdminModel.listSpecilityMerit = (new MeritDAL()).GetApplicantSpecialityWithMeritMarks(num2, num, num1);
						meritApplicantAdminModel.search.key = str;
						meritApplicantAdminModel.search.@value = str1;
						DDLConstants dDLConstant = new DDLConstants()
						{
							condition = "ByType",
							typeId = ProjConstant.Constant.degreeType
						};
						meritApplicantAdminModel.listType = (
							from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
							orderby x.id
							select x).ToList<EntityDDL>();
					}
					meritApplicantAdminModel.applicantId = num;
					meritApplicantAdminModel.requestType = 1;
				}
			}
			catch (Exception exception1)
			{
				meritApplicantAdminModel.applicantId = 0;
				meritApplicantAdminModel.requestType = 3;
			}
			return View(meritApplicantAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantMeritRound5()
		{
			MeritApplicantAdminModel meritApplicantAdminModel = new MeritApplicantAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				meritApplicantAdminModel.search.key = str;
				meritApplicantAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					meritApplicantAdminModel.applicantId = 0;
					meritApplicantAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 5;
						int num2 = ProjConstant.inductionId;
						int num3 = ProjConstant.phaseId;
						try
						{
							if (RouteData.Values["induction"].TooInt() > 0)
							{
								num2 = RouteData.Values["induction"].TooInt();
							}
						}
						catch (Exception exception)
						{
							num2 = ProjConstant.inductionId;
						}
						if (num2 == 0)
						{
							num2 = ProjConstant.inductionId;
						}
						meritApplicantAdminModel.inductionId = num2;
						meritApplicantAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
						meritApplicantAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, 0, num);
						meritApplicantAdminModel.degree = (new ApplicantDAL()).GetApplicantDegreeDetail(0, 0, num);
						meritApplicantAdminModel.listSpecilityMerit = (new MeritDAL()).GetApplicantSpecialityWithMeritMarks(num2, num, num1);
						meritApplicantAdminModel.search.key = str;
						meritApplicantAdminModel.search.@value = str1;
						DDLConstants dDLConstant = new DDLConstants()
						{
							condition = "ByType",
							typeId = ProjConstant.Constant.degreeType
						};
						meritApplicantAdminModel.listType = (
							from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
							orderby x.id
							select x).ToList<EntityDDL>();
					}
					meritApplicantAdminModel.applicantId = num;
					meritApplicantAdminModel.requestType = 1;
				}
			}
			catch (Exception exception1)
			{
				meritApplicantAdminModel.applicantId = 0;
				meritApplicantAdminModel.requestType = 3;
			}
			return View(meritApplicantAdminModel);
		}

		[CheckHasRight]
		public ActionResult ApplicantPreference()
		{
			MeritApplicantAdminModel meritApplicantAdminModel = new MeritApplicantAdminModel();
			try
			{
				string str = Request.QueryString["key"].TooString("");
				string str1 = Request.QueryString["value"].TooString("");
				meritApplicantAdminModel.search.key = str;
				meritApplicantAdminModel.search.@value = str1;
				if ((string.IsNullOrEmpty(str) ? true : string.IsNullOrWhiteSpace(str1)))
				{
					meritApplicantAdminModel.applicantId = 0;
					meritApplicantAdminModel.requestType = 2;
				}
				else
				{
					Message applicantIdBySearch = (new ApplicantDAL()).GetApplicantIdBySearch(str1, str);
					int num = applicantIdBySearch.id.TooInt();
					if (num > 0)
					{
						int num1 = 1;
						int num2 = ProjConstant.inductionId;
						int num3 = ProjConstant.phaseId;
						try
						{
							if (RouteData.Values["induction"].TooInt() > 0)
							{
								num2 = RouteData.Values["induction"].TooInt();
							}
						}
						catch (Exception exception)
						{
							num2 = ProjConstant.inductionId;
						}
						meritApplicantAdminModel.applicant = (new ApplicantDAL()).GetApplicant(0, num);
						meritApplicantAdminModel.applicantInfo = (new ApplicantDAL()).GetApplicantInfoDetail(0, num3, num);
						meritApplicantAdminModel.degree = (new ApplicantDAL()).GetApplicantDegreeDetail(0, num3, num);
						meritApplicantAdminModel.listSpecilityMerit = (new MeritDAL()).GetApplicantSpecialityWithMeritMarks(num2, num, num1);
						meritApplicantAdminModel.search.key = str;
						meritApplicantAdminModel.search.@value = str1;
						DDLConstants dDLConstant = new DDLConstants()
						{
							condition = "ByType",
							typeId = ProjConstant.Constant.degreeType
						};
						meritApplicantAdminModel.listType = (
							from x in (new ConstantDAL()).GetConstantDDL(dDLConstant)
							orderby x.id
							select x).ToList<EntityDDL>();
					}
					meritApplicantAdminModel.applicantId = num;
					meritApplicantAdminModel.requestType = 1;
				}
			}
			catch (Exception exception1)
			{
				meritApplicantAdminModel.applicantId = 0;
				meritApplicantAdminModel.requestType = 3;
			}
			return View(meritApplicantAdminModel);
		}
	}
}