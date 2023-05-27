using Prp.Data;
using Prp.Model;
using Prp.Sln;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
	public class ApplicationAdminController : BaseController
	{
		public ApplicationAdminController()
		{
		}

		[HttpPost]
		public JsonResult ApplicantDistinctionAddUpdate(ApplicantDistinctionParam objExperience)
		{
			ApplicantDistinction applicantDistinction = new ApplicantDistinction()
			{
				applicantDistinctionId = objExperience.applicantDistinctionId,
				applicantId = base.loggedInUser.applicantId,
				subject = objExperience.subject,
				university = objExperience.university,
				year = objExperience.year,
				position = objExperience.position,
				inductionId = ProjConstant.inductionId,
				phaseId = ProjConstant.phaseId,
				imageDistinction = objExperience.imageDistinction,
				dated = DateTime.Now,
				adminId = base.loggedInUser.adminId
			};
			Message message = (new ApplicantAdminDAL()).ApplicantDistinctionAddUpdate(applicantDistinction);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantEducationAddUpdate(ApplicantEducationParam objEducation)
		{
			ApplicantDegree applicantDegree = objEducation.applicantDegree;
			applicantDegree.typeIds = applicantDegree.typeIds.TooString("").TrimStart(new char[] { ',' }).TrimEnd(new char[] { ',' });
			applicantDegree.adminId = base.loggedInUser.adminId;
			applicantDegree.inductionId = ProjConstant.inductionId;
			applicantDegree.phaseId = ProjConstant.phaseId;
			List<ApplicantDegreeMark> applicantDegreeMarks = objEducation.listDegreeMarks;
			Message message = (new ApplicantAdminDAL()).ApplicantDegreeAddUpdate(applicantDegree);
			if (message.status)
			{
				if ((applicantDegreeMarks == null ? false : applicantDegreeMarks.Count > 0))
				{
					message = (new ApplicantAdminDAL()).ApplicantDegreeMarksAddUpdate(applicantDegreeMarks, ProjConstant.inductionId, ProjConstant.phaseId, base.loggedInUser.adminId);
				}
				if ((objEducation.listCertificate == null ? false : objEducation.listCertificate.Count > 0))
				{
					foreach (ApplicantCertificate applicantCertificate in objEducation.listCertificate)
					{
						applicantCertificate.inductionId = ProjConstant.inductionId;
						applicantCertificate.phaseId = ProjConstant.phaseId;
						applicantCertificate.passingDate = applicantCertificate.datePassing.TooDate();
						applicantCertificate.adminId = base.loggedInUser.adminId;
					}
					message = (new ApplicantAdminDAL()).ApplicantCertificateAddUpdate(objEducation.listCertificate);
				}
			}
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
		{
			ApplicantExperience applicantExperience = new ApplicantExperience()
			{
				inductionId = ProjConstant.inductionId,
				phaseId = ProjConstant.phaseId,
				applicantExperienceId = objExperience.applicantExperienceId,
				applicantId = base.loggedInUser.applicantId,
				levelId = objExperience.levelId,
				levelTypeId = objExperience.levelTypeId,
				instituteId = objExperience.instituteId,
				provinceId = objExperience.provinceId,
				typeId = objExperience.typeId,
				instituteName = objExperience.instituteName,
				imageExperience = objExperience.imageExperience,
				isCurrent = objExperience.isCurrent,
				startDated = objExperience.startDated.TooDate()
			};
			if (!applicantExperience.isCurrent)
			{
				applicantExperience.endDate = objExperience.endDate.TooDate();
			}
			else
			{
				applicantExperience.endDate = DateTime.Now;
			}
			applicantExperience.dated = DateTime.Now;
			applicantExperience.adminId = base.loggedInUser.adminId;
			Message message = (new ApplicantAdminDAL()).ApplicantExperienceAddUpdate(applicantExperience);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
		{
			obj.inductionId = ProjConstant.inductionId;
			obj.startDate = obj.startDateStr.TooDate();
			obj.endDate = obj.endDateStr.TooDate();
			obj.dated = DateTime.Now;
			obj.hospitalId = obj.hospitalId.TooInt();
			obj.hospital = obj.hospital.TooString("");
			obj.adminId = base.loggedInUser.adminId;
			obj.isSame = obj.isSame.TooBoolean(false);
			Message message = (new ApplicantAdminDAL()).ApplicantHouseJobAddUpdate(obj);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantInfoAddUpdate(ApplicantInfoParam objApplicantInfo)
		{
			Message message = new Message();
			int num = 1;
			try
			{
				ApplicantInfo applicantInfo = new ApplicantInfo()
				{
					applicantId = base.loggedInUser.applicantId,
					fatherName = objApplicantInfo.fatherName.TooString(""),
					genderId = objApplicantInfo.genderId,
					disableId = objApplicantInfo.disableId,
					countryIdDegreePassing = objApplicantInfo.countryIdDegreePassing,
					countryId = objApplicantInfo.countryId,
					districtId = objApplicantInfo.districtId,
					districtName = objApplicantInfo.districtName,
					domicileProvinceId = objApplicantInfo.domicileProvinceId,
					domicileDistrictId = objApplicantInfo.domicileDistrictId,
					address = objApplicantInfo.address,
					cnicNo = objApplicantInfo.cnicNo,
					cnicPicFront = objApplicantInfo.cnicPicFront,
					cnicPicBack = objApplicantInfo.cnicPicBack,
					domicilePicFront = objApplicantInfo.domicilePicFront,
					domicilePicBack = objApplicantInfo.domicilePicBack,
					pic = objApplicantInfo.pic,
					disableImage = objApplicantInfo.disableImage,
					dob = objApplicantInfo.dob.TooDate(),
					pmdcExpiryDate = objApplicantInfo.pmdcExpiryDate.TooDate(),
					mbbsPassingDate = objApplicantInfo.mbbsPassingDate.TooDate(),
					cnicExpiryDate = objApplicantInfo.cnicExpiryDate.TooDate(),
					adminId = base.loggedInUser.adminId,
					inductionId = ProjConstant.inductionId,
					phaseId = ProjConstant.phaseId
				};
				message = (new ApplicantAdminDAL()).ApplicantInfoAddUpdate(applicantInfo);
				message.id = num;
			}
			catch (Exception exception)
			{
				message.id = num;
			}
			return base.Json(message, 0);
		}

		public JsonResult ApplicantResearchPaperAddUpdate(ApplicantResearchPaperParam objExperience)
		{
			ApplicantResearchPaper applicantResearchPaper = new ApplicantResearchPaper()
			{
				applicantResearchId = objExperience.applicantResearchId,
				applicantId = base.loggedInUser.applicantId,
				name = objExperience.name,
				authorId = objExperience.authorId,
				publishStatusId = objExperience.publishStatusId,
				link = objExperience.link,
				webLink = objExperience.webLink,
				imageLetter = objExperience.imageLetter,
				dated = DateTime.Now,
				adminId = base.loggedInUser.adminId,
				inductionId = ProjConstant.inductionId,
				phaseId = ProjConstant.phaseId,
				researchJournalId = objExperience.researchJournalId,
				isActive = objExperience.isActive
			};
			Message message = (new ApplicantAdminDAL()).ApplicantResearchPaperAddUpdate(applicantResearchPaper);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantSpecilityAddUpdate(ApplicantSpecility objSpecility)
		{
			objSpecility.applicantId = base.loggedInUser.applicantId;
			objSpecility.dated = DateTime.Now;
			objSpecility.adminId = base.loggedInUser.adminId;
			objSpecility.inductionId = ProjConstant.inductionId;
			objSpecility.phaseId = ProjConstant.phaseId;
			Message message = (new ApplicantAdminDAL()).ApplicantSpecilityAddUpdate(objSpecility);
			return base.Json(message, 0);
		}

		[HttpPost]
		public JsonResult ApplicantVoucherAddUpdate(ApplicantVoucherParam objParam)
		{
			ApplicantVoucher applicantVoucher = new ApplicantVoucher()
			{
				applicantId = base.loggedInUser.applicantId,
				applicantVoucherId = objParam.applicantVoucherId.TooInt(),
				branchCode = objParam.branchCode.TooString(""),
				voucherImage = objParam.voucherImage.TooString(""),
				ibn = objParam.ibn.TooString(""),
				accountNo = objParam.accountNo.TooString(""),
				accountTitle = objParam.accountTitle.TooString(""),
				submittedDate = objParam.dateSubmitted.TooDate(),
				adminId = base.loggedInUser.adminId,
				inductionId = ProjConstant.inductionId,
				phaseId = ProjConstant.phaseId
			};
			Message message = (new ApplicantAdminDAL()).ApplicantVoucherAddUpdate(applicantVoucher);
			return base.Json(message, 0);
		}

		public ActionResult ApplicationUpdate()
		{
			ActionResult actionResult;
			HomeModel homeModel = new HomeModel();
			if (base.loggedInUser.adminId != 0)
			{
				actionResult = base.View(homeModel);
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult ApplicationVoucher()
		{
			ActionResult actionResult;
			if (base.loggedInUser.adminId != 0)
			{
				actionResult = base.View(new ApplicantVoucherModel());
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult EducationProcess()
		{
			ActionResult actionResult;
			ProfileModel profileModel = new ProfileModel();
			if (base.loggedInUser.adminId != 0)
			{
				try
				{
					profileModel.listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, "");
					profileModel.listDegree = (new ConstantDAL()).GetAll(ProjConstant.Constant.degree);
					profileModel.listInstituteType = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteType);
					profileModel.listInstitute = (new InstitueDAL()).GetAll(ProjConstant.Constant.InstituteType.govt);
					profileModel.listDiscipline = (new CommonDAL()).DisciplineGetAll();
					actionResult = base.View(profileModel);
				}
				catch (Exception exception)
				{
					actionResult = base.View(profileModel);
				}
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult ExprienceProcess()
		{
			ActionResult actionResult;
			ProfileModel profileModel = new ProfileModel()
			{
				listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, ""),
				listCountry = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.country, ProjConstant.Constant.pakistan, "GetAllCountry"),
				listInstituteType = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteType)
			};
			if (base.loggedInUser.adminId != 0)
			{
				actionResult = base.View(profileModel);
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult ProfileProcess()
		{
			ActionResult actionResult;
			ProfileModel profileModel = new ProfileModel();
			if (base.loggedInUser.adminId != 0)
			{
				try
				{
					profileModel.listProvince = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan, "");
					profileModel.listCountry = (new RegionDAL()).RegionGetByCondition(ProjConstant.Constant.Region.country, 0, ProjConstant.Constant.Condition.GetAllCountry);
					profileModel.listDegree = (new ConstantDAL()).GetAll(ProjConstant.Constant.degree);
					profileModel.listInstituteType = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteType);
					profileModel.listInstituteLevel = (new ConstantDAL()).GetAll(ProjConstant.Constant.instituteLevel);
					profileModel.listInstitute = (new InstitueDAL()).GetAll(ProjConstant.Constant.InstituteType.govt);
					profileModel.listSpeciality = (new SpecialityDAL()).GetAll();
					actionResult = base.View(profileModel);
				}
				catch (Exception exception)
				{
					actionResult = base.View(profileModel);
				}
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult ResearchPaperProcess()
		{
			ActionResult actionResult;
			ProfileModel profileModel = new ProfileModel();
			DDLRegions dDLRegion = new DDLRegions()
			{
				condition = "ByType",
				typeId = ProjConstant.Constant.Region.country
			};
			profileModel.listRegion = (new MasterSetupDAL()).GetRegionDDL(dDLRegion);
			if (base.loggedInUser.adminId != 0)
			{
				actionResult = base.View(profileModel);
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}

		public ActionResult SpecialityProcess()
		{
			ActionResult actionResult;
			ProfileModel profileModel = new ProfileModel();
			if (base.loggedInUser.adminId != 0)
			{
				actionResult = base.View(profileModel);
			}
			else
			{
				actionResult = this.Redirect("/profile-view-print");
			}
			return actionResult;
		}
	}
}