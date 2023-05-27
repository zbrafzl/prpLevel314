using Newtonsoft.Json;
using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
	public class JournalAdminController : BaseAdminController
	{
		public JournalAdminController()
		{
		}

		[HttpPost]
		public ActionResult ApplicantResearchSearch(ResearchJournalSearch obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new MasterSetupDAL()).ApplicantResearchSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[CheckHasRight]
		public ActionResult JournalApplicants()
		{
			ResearchJournalModel researchJournalModel = new ResearchJournalModel()
			{
				researchJournalId = Request.QueryString["id"].TooInt()
			};
			return View(researchJournalModel);
		}

		[CheckHasRight]
		public ActionResult JournalManage()
		{
			ResearchJournalModel researchJournalModel = new ResearchJournalModel()
			{
				listType = (
					from x in (new ConstantDAL()).GetAll(ProjConstant.Constant.journalDispline)
					orderby x.id
					select x).ToList<Constant>()
			};
			return View(researchJournalModel);
		}

		[HttpPost]
		public ActionResult JournalSearch(ResearchJournalSearch obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.phaseId = ProjConstant.phaseId;
			DataTable dataTable = (new MasterSetupDAL()).ResearchJournalSearch(obj);
			string str = JsonConvert.SerializeObject(dataTable);
			return base.Content(str, "application/json");
		}

		[CheckHasRight]
		public ActionResult JournalSetup()
		{
			JournalModelAdmin journalModelAdmin = new JournalModelAdmin();
			int num = Request.QueryString["id"].TooInt();
			journalModelAdmin.journal = (new MasterSetupDAL()).JournalGetById(num);
			DDLConstants dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.metalTiers
			};
			journalModelAdmin.listBatch = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.journalDispline
			};
			journalModelAdmin.listDiscipline = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			dDLConstant = new DDLConstants()
			{
				condition = "",
				typeId = ProjConstant.Constant.journalImpactFactorType
			};
			journalModelAdmin.listType = (new ConstantDAL()).GetConstantDDL(dDLConstant);
			DDLRegions dDLRegion = new DDLRegions()
			{
				condition = "ByType",
				typeId = ProjConstant.Constant.Region.country
			};
			journalModelAdmin.listRegion = (new MasterSetupDAL()).GetRegionDDL(dDLRegion);
			return View(journalModelAdmin);
		}

		[ValidateInput(false)]
		public ActionResult SaveJournalData(JournalModelAdmin ModelSave, HttpPostedFileBase files)
		{
			ResearchJournal modelSave = ModelSave.journal;
			modelSave.researchJournalId = modelSave.researchJournalId.TooInt();
			modelSave.typeId = modelSave.typeId.TooInt();
			modelSave.name = modelSave.name.TooString("");
			modelSave.code = modelSave.code.TooString("");
			modelSave.url = modelSave.url.TooString("");
			modelSave.regionId = modelSave.regionId.TooInt();
			modelSave.batchId = modelSave.batchId.TooInt();
			modelSave.displineId = modelSave.displineId.TooInt();
			modelSave.isActive = modelSave.isActive.TooBoolean(false);
			modelSave.dated = DateTime.Now;
			modelSave.adminId = base.loggedInUser.userId;
			(new MasterSetupDAL()).JournalAddUpdate(modelSave);
			return this.Redirect("/admin/research-journal-manage");
		}
	}
}