using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ApplicantProfileController : BaseController
    {
        #region Views

        public ActionResult ProfileBuilder()
        {
            #region Profile Model


            ProfileModel model = new ProfileModel();
            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                , ProjConstant.Constant.pakistan);

            model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
              , 0, ProjConstant.Constant.Condition.GetAllCountry);
            model.countryJson = JsonConvert.SerializeObject(model.listCountry);

            model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

            model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
            model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
            model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
            model.listSpeciality = new SpecialityDAL().GetAll();
            model.listDiscipline = new CommonDAL().DisciplineGetAll();

            #endregion

            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                      , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);
                if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.profile)
                {
                    return View("ProfileProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.education)
                {
                    return View("EducationProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.experience)
                {
                    return View("ExprienceProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.researchPaper)
                {
                    return View("ResearchPaperProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.specility)
                {
                    return View("SpecialityProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.paymentDone)
                {
                    ApplicantVoucherModel voucher = new ApplicantVoucherModel();
                    return View("ApplicationVoucher", voucher);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.proofReading)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ProofReadingProcess", modelProof);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View("ProfileProcess", model);
                }
            }
            catch (Exception)
            {
                return View("ProfileProcess", model);
            }
        }

        public ActionResult ProfileProcess()
        {
            ProfileModel model = new ProfileModel();
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                        , ProjConstant.Constant.pakistan);

                    model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
                      , 0, ProjConstant.Constant.Condition.GetAllCountry);

                    model.countryJson = JsonConvert.SerializeObject(model.listCountry);

                    model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                    model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                    model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
                    model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                    model.listSpeciality = new SpecialityDAL().GetAll();

                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult EducationProcess()
        {
            ProfileModel model = new ProfileModel();


            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                   , ProjConstant.Constant.pakistan);

                    model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                    model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                    model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                    model.listDiscipline = new CommonDAL().DisciplineGetAll();
                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(model);
            }

        }

        public ActionResult ExprienceProcess()
        {
            ProfileModel model = new ProfileModel();
            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
              , ProjConstant.Constant.pakistan);
            model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
              , ProjConstant.Constant.pakistan, "GetAllCountry");
            model.loggedInUser = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, loggedInUser.applicantId);
            model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }
        }

        public ActionResult ResearchPaperProcess()
        {
            ProfileModel model = new ProfileModel();

            try
            {

                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }
        }

        public ActionResult SpecialityProcess()
        {
            ProfileModel model = new ProfileModel();


            try
            {

                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
               , ProjConstant.Constant.pakistan);

                    model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
                      , 0, ProjConstant.Constant.Condition.GetAllCountry);

                    model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                    model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                    model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
                    model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                    model.listSpeciality = new SpecialityDAL().GetAll();
                    string query = "select top(1) imageCertificate from tblApplicantDegree where applicantId = " + loggedInUser.applicantId + " and fcpsExemptionStatus = 1";
                    SqlConnection con = new SqlConnection();
                    SqlCommand cmd = new SqlCommand(query);
                    int ExemptedDisicpline = 0;
                    int onlyBasicFlag = 0;
                    string exemptedDisciplineName = "";
                    try
                    {
                        con = new SqlConnection(PrpDbConnectADO.Conn);
                        con.Open();
                        cmd.Connection = con;
                        ExemptedDisicpline = cmd.ExecuteScalar().TooInt();
                        string queryCerts = "select count(*) from tblApplicantCertificate where applicantId = " + loggedInUser.applicantId + "";
                        SqlCommand cmdCerts = new SqlCommand(queryCerts);
                        cmdCerts.Connection = con;
                        onlyBasicFlag = cmdCerts.ExecuteScalar().TooInt();
                        string queryNameDiscipline = "select top(1) name from tblSpeciality where specialityId = " + ExemptedDisicpline + " and isActive = 1";
                        SqlCommand cmdNameDiscipline = new SqlCommand(queryNameDiscipline);
                        cmdNameDiscipline.Connection = con;
                        exemptedDisciplineName = cmdNameDiscipline.ExecuteScalar().TooString();
                        ViewBag.ExemptedSpecialtyId = ExemptedDisicpline;
                        ViewBag.BasicOnly = onlyBasicFlag;
                        ViewBag.ExemptedSpecialtyName = exemptedDisciplineName;
                    }
                    catch (Exception ex)
                    {
                        ExemptedDisicpline = 0;
                    }
                    finally
                    {
                        con.Close();
                    }
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }



        }

        public ActionResult ProofReadingProcess()
        {
            ProofReadingModel model = GenerateModelProofReading();

            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                                    , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }

        }

        public ActionResult ProofReadingView()
        {
            ProofReadingModel model = GenerateModelProofReading();
            return View(model);
        }

        public ActionResult ApplicationViewAnPrint()
        {
            ProofReadingModel model = GenerateModelProofReading();

            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

            if (objStatus.statusId < ProjConstant.Constant.ApplicationStatus.completed)
            {
                return Redirect("/applicant-profile-create");
            }

            return View(model);
        }

        public ActionResult ApplicantMerit()
        {
            ProofReadingModel model = GenerateModelProofReading();
            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();

            int inductionId = ProjConstant.inductionId;

            model.listSpecilityMerit = new MeritDAL().GetApplicantSpecialityWithMeritMarks(inductionId, loggedInUser.applicantId, roundId);


            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);


            if (objStatus.statusId < ProjConstant.Constant.ApplicationStatus.completed)
            {
                return Redirect("/applicant-profile-create");
            }

            return View(model);
        }

        public ActionResult ApplicationProcessComplete()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        public ActionResult ApplicationListView()
        {
            ApplicantApplicationModel model = GenerateModelApplicantApplication();

            return View(model);
        }

        public ActionResult ApplicationVoucher()
        {
            ApplicantVoucherModel model = new ApplicantVoucherModel();

            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

            if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.completed)
            {
                ProofReadingModel modelProof = GenerateModelProofReading();
                return View("ApplicationViewAnPrint", modelProof);
            }
            else
            {
                return View(model);
            }
        }

        public ApplicantApplicationModel GenerateModelApplicantApplication()
        {
            ApplicantApplicationModel model = new ApplicantApplicationModel();
            try
            {
                model.applicant = new ApplicantDAL().GetApplicant(0, loggedInUser.applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, ProjConstant.phaseId, loggedInUser.applicantId);
                model.status = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();
            }
            catch (Exception)
            {

            }

            return model;
        }

        public ProofReadingModel GenerateModelProofReading()
        {
            ProofReadingModel model = new ProofReadingModel();
            try
            {
                DDLConstants dDLConstant = new DDLConstants();
                dDLConstant.condition = "ByType";
                dDLConstant.typeId = ProjConstant.Constant.degreeType;
                model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();

                int inductionId = ProjConstant.inductionId;
                int phaseId = ProjConstant.phaseId;

                model.applicant = new ApplicantDAL().GetApplicant(0, loggedInUser.applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, loggedInUser.applicantId);
                model.degree = new ApplicantDAL().GetApplicantDegreeDetail(0, 0, loggedInUser.applicantId);
                model.listJob = new ApplicantDAL().GetApplicantHouseJobList(0, 0, loggedInUser.applicantId);
                model.listDegreeMark = new ApplicantDAL().GetApplicantDegreeMarkList(0, 0, loggedInUser.applicantId);
                model.listCertificate = new ApplicantDAL().GetApplicantCertificateListDetail(0, 0, loggedInUser.applicantId);
                model.listExprince = new ApplicantDAL().GetApplicantExperienceListDetail(0, 0, loggedInUser.applicantId);
                model.listDistinction = new ApplicantDAL().GetApplicantDistinctionList(0, 0, loggedInUser.applicantId);
                model.listResearchPaper = new ApplicantDAL().GetApplicantResearchPaperListDetail(0, 0, loggedInUser.applicantId);
                model.listSpecility = new ApplicantDAL().GetApplicantSpecilityListWithMarks(0, 0, loggedInUser.applicantId);
                model.voucher = new ApplicantDAL().GetApplicantVoucher(0, 0, loggedInUser.applicantId);
                model.listMarks = new MarksDAL().GetMarksAccumulativeByApplicant(loggedInUser.applicantId);

            }
            catch (Exception)
            {
            }
            return model;
        }

        public ActionResult ApplicationStatusView()
        {

            ApplicationStatusModel model = new ApplicationStatusModel();
            model.listStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            model.listStatusApproval = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            model.info = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            try
            {
                model.final = new JoiningDAL().GetFinalApplicantById(ProjConstant.inductionId, loggedInUser.applicantId);
            }
            catch (Exception)
            {
                model.final = new ApplicantSelected();
            }
            return View(model);
        }


        public ActionResult ApplicationApprovalStatusView()
        {

            ApplicantApprovalStatusModel model = new ApplicantApprovalStatusModel();
            model.listStatus = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            return View(model);
        }

        #endregion

        #region Personal Information

        [HttpGet]
        public JsonResult GetApplicantInfo(int applicantId)
        {
            Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
            return Json(applicant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantProfileData(int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantInfo();

            try
            {
                applicantInfo = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
                applicantInfo.bod = applicantInfo.dob.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
            }



            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetApplicantInfoDualNational(int applicantId)
        {
            ApplicantInfoDualNational applicantInfo = new ApplicantDAL().GetApplicantInfoDualNational(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantInfoDetail(int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, applicantId);
            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantInfoAddUpdate(ApplicantInfoParam objApplicantInfo)
        {
            Message msg = new Message();

            int applicationStatus = 1;

            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                  , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                if (objStatus.statusId < 8)
                {

                    ApplicantInfo obj = new ApplicantInfo();
                    obj.inductionId = ProjConstant.inductionId;
                    obj.phaseId = ProjConstant.phaseId;
                    obj.applicantId = loggedInUser.applicantId;
                    obj.fatherName = objApplicantInfo.fatherName.TooString();
                    obj.genderId = objApplicantInfo.genderId;
                    obj.disableId = objApplicantInfo.disableId;
                    obj.countryIdDegreePassing = objApplicantInfo.countryIdDegreePassing;
                    obj.dualNationalityType = objApplicantInfo.dualNationalityType;
                    obj.countryId = objApplicantInfo.countryId;
                    obj.districtId = objApplicantInfo.districtId;
                    obj.districtName = objApplicantInfo.districtName;
                    obj.domicileProvinceId = objApplicantInfo.domicileProvinceId;
                    obj.domicileDistrictId = objApplicantInfo.domicileDistrictId;
                    obj.address = objApplicantInfo.address;
                    obj.cnicNo = objApplicantInfo.cnicNo;
                    obj.cnicPicFront = objApplicantInfo.cnicPicFront;
                    obj.cnicPicBack = objApplicantInfo.cnicPicBack;
                    obj.domicilePicFront = objApplicantInfo.domicilePicFront;
                    obj.domicilePicBack = objApplicantInfo.domicilePicBack;
                    obj.pic = objApplicantInfo.pic;
                    obj.disableImage = objApplicantInfo.disableImage;
                    obj.dob = objApplicantInfo.dob.TooDate();
                    obj.pmdcExpiryDate = objApplicantInfo.pmdcExpiryDate.TooDate();
                    obj.mbbsPassingDate = objApplicantInfo.mbbsPassingDate.TooDate();
                    obj.cnicExpiryDate = objApplicantInfo.cnicExpiryDate.TooDate();
                    msg = new ApplicantDAL().ApplicantInfoAddUpdate(obj);
                    msg.id = applicationStatus;

                    if (msg.status)
                        ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.education);
                }
                else
                {
                    msg.id = applicationStatus;
                }
            }
            catch (Exception)
            {
                msg.id = applicationStatus;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantInfoAddUpdateDualNational(ApplicantInfoDualNational obj)
        {
            Message msg = new Message();



            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                  , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                if (objStatus.statusId < 8)
                {
                    obj.applicantId = loggedInUser.applicantId;
                    obj.countryId = obj.countryId;
                    obj.embassyCertificate = obj.embassyCertificate.TooString();
                    obj.languageCertificate = obj.languageCertificate.TooString();
                    obj.policeCertificate = obj.policeCertificate.TooString();
                    obj.affidavitCertificate = obj.affidavitCertificate.TooString();
                    msg = new ApplicantDAL().ApplicantInfoDualNationalAddUpdate(obj);
                }

            }
            catch (Exception)
            {
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        #endregion

        #region Education/Degree

        [HttpPost]
        public JsonResult ApplicantEducationAddUpdate(ApplicantEducationParam objEducation)
        {


            ApplicantDegree degree = objEducation.applicantDegree;

            degree.typeIds = degree.typeIds.TooString().TrimStart(',').TrimEnd(',');
            degree.inductionId = ProjConstant.inductionId;
            degree.phaseId = ProjConstant.phaseId;

            List<ApplicantDegreeMark> listMarks = objEducation.listDegreeMarks;
            Message msg = new ApplicantDAL().ApplicantDegreeAddUpdate(degree);
            if (msg.status == true)
            {
                if (listMarks != null && listMarks.Count > 0)
                    msg = new ApplicantDAL().ApplicantDegreeMarksAddUpdate(ProjConstant.inductionId, ProjConstant.phaseId, listMarks);

                if (objEducation.listCertificate != null && objEducation.listCertificate.Count > 0)
                {
                    foreach (var item in objEducation.listCertificate)
                    {
                        item.inductionId = ProjConstant.inductionId;
                        item.phaseId = ProjConstant.phaseId;
                        item.passingDate = item.datePassing.TooDate();
                    }
                    msg = new ApplicantDAL().ApplicantCertificateAddUpdate(objEducation.listCertificate);
                }
            }

            if (msg.status)
                ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.experience);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantEducationData(int applicantId)
        {
            try
            {
                new ApplicantDAL().ApplicantDegreeMarksMakeAccurate(0, 0, applicantId);
            }
            catch (Exception)
            {
            }

            ApplicantEducation objEducation = new ApplicantEducation();
            try
            {
                try
                {
                    objEducation.applicantDegree = new ApplicantDAL().GetApplicantDegree(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

                try
                {
                    objEducation.listDegreeMarks = new ApplicantDAL().GetApplicantDegreeMarkList(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

                try
                {
                    objEducation.listCertificate = new ApplicantDAL().GetApplicantCertificateList(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

            }
            catch (Exception)
            {
            }
            return Json(objEducation, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantCertificateList(int applicantId)
        {
            List<ApplicantCertificate> list = new ApplicantDAL().GetApplicantCertificateList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region House Job

        [HttpPost]
        public JsonResult ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {

            obj.inductionId = ProjConstant.inductionId;
            obj.startDate = obj.startDateStr.TooDate();
            obj.endDate = obj.endDateStr.TooDate();
            obj.dated = DateTime.Now;
            obj.hospitalId = obj.hospitalId.TooInt();
            obj.hospital = obj.hospital.TooString();
            obj.isSame = obj.isSame.TooBoolean();

            Message msg = new ApplicantDAL().ApplicantHouseJobAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantHouseJobDeleteSingle(int houseJodId)
        {
            Message msg = new ApplicantDAL().ApplicantHouseJobDeleteSingle(houseJodId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantHouseJobByApplicant(int applicantId)
        {
            List<ApplicantHouseJob> list = new ApplicantDAL().GetApplicantHouseJobList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Exprience

        [HttpPost]
        public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
        {
            ApplicantExperience obj = new ApplicantExperience();
            obj.applicantExperienceId = objExperience.applicantExperienceId;
            obj.applicantId = loggedInUser.applicantId;
            obj.levelId = objExperience.levelId;
            obj.levelTypeId = objExperience.levelTypeId;
            obj.instituteId = objExperience.instituteId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.provinceId = objExperience.provinceId;
            obj.typeId = objExperience.typeId;
            obj.instituteName = objExperience.instituteName;
            obj.imageExperience = objExperience.imageExperience;
            obj.isCurrent = objExperience.isCurrent;
            obj.startDated = objExperience.startDated.TooDate();
            if (obj.isCurrent)
                obj.endDate = DateTime.Now;
            else
                obj.endDate = objExperience.endDate.TooDate();
            obj.dated = DateTime.Now;

            Message msg = new ApplicantDAL().ApplicantExperienceAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantExperienceDeleteSingle(int applicantExperienceId)
        {
            Message msg = new ApplicantDAL().ApplicantExperienceDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantExperienceId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantExperienceData(int applicantId)
        {
            List<ApplicantExperience> list = new ApplicantDAL().GetApplicantExperienceListByApplicant(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Distinction

        [HttpPost]
        public JsonResult ApplicantDistinctionAddUpdate(ApplicantDistinctionParam objExperience)
        {
            //imageDistinction
            ApplicantDistinction obj = new ApplicantDistinction();
            obj.applicantDistinctionId = objExperience.applicantDistinctionId;
            obj.applicantId = loggedInUser.applicantId;
            obj.subject = objExperience.subject;
            obj.university = objExperience.university;
            obj.year = objExperience.year;
            obj.position = objExperience.position;

            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;

            obj.imageDistinction = objExperience.imageDistinction;
            obj.dated = DateTime.Now;

            Message msg = new ApplicantDAL().ApplicantDistinctionAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantDistinctionData(int applicantId)
        {
            List<ApplicantDistinction> list = new ApplicantDAL().GetApplicantDistinctionList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ApplicantDistinctionDeleteSingle(int applicantDistinctionId)
        {
            Message msg = new ApplicantDAL().ApplicantDistinctionDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantDistinctionId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ResearchPaper

        [HttpPost]
        public JsonResult ApplicantResearchPaperAddUpdate(ApplicantResearchPaperParam objExperience)
        {
            ApplicantResearchPaper obj = new ApplicantResearchPaper();
            obj.applicantResearchId = objExperience.applicantResearchId;
            obj.researchJournalId = objExperience.researchJournalId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.applicantId = loggedInUser.applicantId;
            obj.name = objExperience.name;
            obj.authorId = objExperience.authorId;
            obj.publishStatusId = objExperience.publishStatusId;
            obj.link = objExperience.link;
            obj.webLink = objExperience.webLink;
            obj.imageLetter = objExperience.imageLetter;
            obj.dated = DateTime.Now;
            Message msg = new ApplicantDAL().ApplicantResearchPaperAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantResearchPaperDeleteSingle(int applicantResearchId)
        {
            Message msg = new ApplicantDAL().ApplicantResearchPaperDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantResearchId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantResearchPaperData(int applicantId)
        {
            List<ApplicantResearchPaper> list = new ApplicantDAL().GetApplicantResearchPaperList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Specility

        [HttpPost]
        public JsonResult ApplicantSpecilityCheckPreferenceNo(ApplicantSpecility objSpecility)
        {
            objSpecility.dated = DateTime.Now;
            objSpecility.inductionId = ProjConstant.inductionId;
            objSpecility.phaseId = ProjConstant.phaseId;
            Message msg = new ApplicantDAL().ApplicantSpecilityCheckPreferenceNo(objSpecility);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantSpecilityAddUpdate(ApplicantSpecility objSpecility)
        {
            objSpecility.dated = DateTime.Now;

            objSpecility.inductionId = ProjConstant.inductionId;
            objSpecility.phaseId = ProjConstant.phaseId;

            Message msg = new ApplicantDAL().ApplicantSpecilityAddUpdate(objSpecility);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantSpecilityDeleteSingle(int applicantSpecilityId)
        {
            Message msg = new ApplicantDAL().ApplicantSpecilityDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantSpecilityId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantSpecilityData(int applicantId)
        {
            List<ApplicantSpecility> list = new ApplicantDAL().GetApplicantSpecilityList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Voucher

        [HttpGet]
        public JsonResult GetApplicantVoucher(int applicantId)
        {
            ApplicantVoucher applicant = new ApplicantDAL().GetApplicantVoucher(0, 0, applicantId);
            return Json(applicant, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantVoucherAddUpdate(ApplicantVoucherParam objParam)
        {

            ApplicantVoucher obj = new ApplicantVoucher();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.applicantId = loggedInUser.applicantId;
            obj.applicantVoucherId = objParam.applicantVoucherId.TooInt();
            obj.branchCode = objParam.branchCode.TooString();
            obj.voucherImage = objParam.voucherImage.TooString();
            obj.ibn = objParam.ibn.TooString();
            obj.accountNo = objParam.accountNo.TooString();
            obj.accountTitle = objParam.accountTitle.TooString();
            obj.submittedDate = objParam.dateSubmitted.TooDate();
            Message msg = new ApplicantDAL().ApplicantVoucherAddUpdate(obj);

            if (msg.status)
                ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.paymentDone);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Common

        ///
        ///

        [HttpGet]
        public JsonResult ReopenApplication()
        {
            Message msg = new Message();
            if (loggedInUser.applicantId > 0)
            {
                msg = new ApplicantDAL().ReopenApplication(loggedInUser.applicantId, ProjConstant.Constant.ApplicationStatus.proofReading);
            }
            else
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateApplicantStatus(int applicantId, int applicationStatus)
        {
            Message msg = ApplicantStatusUpdate(applicantId, ProjConstant.Constant.statusApplicantApplication, applicationStatus);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public Message ApplicantStatusUpdate(int applicantId, int statusTypeId, int statusId)
        {
            Message msg = new Message();
            try
            {
                msg = new ApplicantDAL().ApplicantStatusUpdate(applicantId, statusTypeId, statusId);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        [HttpGet]
        public JsonResult GetApplicationDetailData(int applicantId)
        {
            Application obj = new Application();

            Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
            if (applicant != null && applicant.applicantId > 0)
                obj.applicant = applicant;

            int inductionId = ProjConstant.inductionId;
            int phaseId = ProjConstant.phaseId;

            ApplicantInfo applicantInfo = new ApplicantDAL().GetApplicantInfo(inductionId, phaseId, applicantId);
            if (applicantInfo != null && applicantInfo.applicantDetailId > 0)
                obj.applicantInfo = applicantInfo;

            obj.applicantDegree = new ApplicantDAL().GetApplicantDegree(inductionId, phaseId, applicantId);

            obj.listDegreeMarks = new ApplicantDAL().GetApplicantDegreeMarkList(inductionId, phaseId, applicantId);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}