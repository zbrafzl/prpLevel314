using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class SubjectAdminController : BaseAdminController
	{
		public SubjectAdminController()
		{
		}

		[ValidateInput(false)]
		public ActionResult SaveSubjectData(SpecialityModelAdmin ModelSave, HttpPostedFileBase files)
		{
			Speciality modelSave = ModelSave.speciality;
			modelSave.name = modelSave.name.TooString("");
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.sortOrder = modelSave.sortOrder.TooInt();
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			(new SpecialityDAL()).AddUpdate(modelSave);
			return this.Redirect("/admin/speciality-manage");
		}

		public ActionResult SubjectManage()
		{
			SpecialityModelAdmin specialityModelAdmin = new SpecialityModelAdmin();
			Request.QueryString["inductionId"].TooString("");
			specialityModelAdmin.inductionId = Request.QueryString["inductionId"].TooInt();
			specialityModelAdmin.listInduction = Prp.Sln.DDLInduction.GetAll("GetAllActive");
			if (specialityModelAdmin.inductionId == 0)
			{
				if ((specialityModelAdmin.listInduction == null ? false : specialityModelAdmin.listInduction.Count == 1))
				{
					specialityModelAdmin.inductionId = specialityModelAdmin.listInduction.FirstOrDefault<EntityDDL>().id.TooInt();
				}
			}
			if (specialityModelAdmin.inductionId <= 0)
			{
				specialityModelAdmin.list = (new SpecialityDAL()).GetAll();
			}
			else
			{
				specialityModelAdmin.list = (new SpecialityDAL()).GetWithJobByInduction(specialityModelAdmin.inductionId);
			}
			return View(specialityModelAdmin);
		}

		public ActionResult SubjectSetup()
		{
			SpecialityModelAdmin specialityModelAdmin = new SpecialityModelAdmin();
			int num = Request.QueryString["id"].TooInt();
			if (num > 0)
			{
				specialityModelAdmin.speciality = (new SpecialityDAL()).GetById(num);
			}
			return View(specialityModelAdmin);
		}
	}
}