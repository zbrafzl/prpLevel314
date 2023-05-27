using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class InstitueAdminController : BaseAdminController
	{
		public InstitueAdminController()
		{
		}

		public ActionResult InstituteManage()
		{
			InstituteModelAdmin instituteModelAdmin = new InstituteModelAdmin();
			instituteModelAdmin.institute.instituteTypeId = Request.QueryString["typeId"].TooInt();
			instituteModelAdmin.institute.provinceId = Request.QueryString["provinceId"].TooInt();
			instituteModelAdmin.listType = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteType);
			instituteModelAdmin.listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, "");
			if (instituteModelAdmin.institute.instituteTypeId == 0)
			{
				instituteModelAdmin.institute.instituteTypeId = instituteModelAdmin.listType.FirstOrDefault<Constant>().id.TooInt();
			}
			if (instituteModelAdmin.institute.provinceId == 0)
			{
				instituteModelAdmin.institute.provinceId = 196;
			}
			instituteModelAdmin.list = (
				from x in (new InstitueDAL()).GetAll(instituteModelAdmin.institute.instituteTypeId, instituteModelAdmin.institute.provinceId)
				orderby x.name
				select x).ToList<Institute>();
			return View(instituteModelAdmin);
		}

		public ActionResult InstituteSetup()
		{
			InstituteModelAdmin instituteModelAdmin = new InstituteModelAdmin();
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				instituteModelAdmin.institute = (new InstitueDAL()).GetById(num);
			}
			instituteModelAdmin.listHospital = (new HospitalDAL()).GetHospitalForInstitute(num);
			instituteModelAdmin.listType = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteType);
			instituteModelAdmin.listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, "");
			return View(instituteModelAdmin);
		}

		[ValidateInput(false)]
		public ActionResult SaveInstituteData(InstituteModelAdmin ModelSave, HttpPostedFileBase files)
		{
			Institute modelSave = ModelSave.institute;
			modelSave.name = modelSave.name.TooString("");
			modelSave.code = "0000";
			modelSave.instituteTypeId = modelSave.instituteTypeId.TooInt();
			modelSave.districtId = modelSave.districtId.TooInt();
			modelSave.provinceId = modelSave.provinceId.TooInt();
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.sortOrder = modelSave.sortOrder.TooInt();
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			modelSave.hospitalIds = modelSave.hospitalIds.TooString("").TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' });
			(new InstitueDAL()).AddUpdate(modelSave);
			int num = modelSave.instituteTypeId;
			ActionResult actionResult = this.Redirect(string.Concat("/admin/institute-manage?typeId=", num.ToString()));
			return actionResult;
		}
	}
}