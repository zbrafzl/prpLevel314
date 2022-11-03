using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class ApplicantDAL : PrpDBConnect
    {
        #region Applicant

        public Applicant Login(string userName, string password)
        {
            Applicant obj = new Applicant();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantLogin]"
                };
                cmd.Parameters.AddWithValue("@emailId", userName);
                cmd.Parameters.AddWithValue("@password", password);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    obj.inductionId = dr["inductionId"].TooInt();
                    obj.applicantId = dr["applicantId"].TooInt();
                    obj.network = dr["network"].TooInt();
                    obj.facultyId = dr["facultyId"].TooInt();
                    obj.statusId = dr["statusId"].TooInt();

                    obj.name = dr["name"].TooString();
                    obj.pmdcNo = dr["pmdcNo"].TooString();
                    obj.emailId = dr["emailId"].TooString();
                    obj.password = dr["password"].TooString();
                    obj.passwordDecrypt = dr["passwordDecrypt"].TooString();
                    obj.contactNumber = dr["contactNumber"].TooString();
                    obj.pic = dr["pic"].TooString();
                    obj.date = dr["date"].TooString();
                    obj.facultyName = dr["facultyName"].TooString();
                    obj.status = dr["status"].TooString();
                }

                //var dbt = db.spApplicantLogin(userName, password).FirstOrDefault();
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception ex)
            {
                obj = new Applicant();
            }
            return obj;
        }


        public Applicant LoginByPhfDeveloper(string userName, int typeId)
        {
            Applicant obj = new Applicant();
            try
            {
                //var dbt = db.spApplicantLoginByPhf(userName, typeId).FirstOrDefault();
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception ex)
            {
                obj = new Applicant();
            }
            return obj;
        }

        public Message ApplicantIsExist(IsExists obj)
        {
            Message msg = new Message();
            try
            {
                var item = db.spApplicantIsExist(obj.id, obj.search, obj.condition).FirstOrDefault();
                if (item != null && item.applicantId > 0)
                {
                    msg.id = item.applicantId;
                }
                else
                {
                    msg.id = 0;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message Registration(Applicant obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantRegister]"
                };
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo);
                cmd.Parameters.AddWithValue("@emailId", obj.emailId);
                cmd.Parameters.AddWithValue("@password", obj.password);
                cmd.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
                cmd.Parameters.AddWithValue("@network", obj.network);
                cmd.Parameters.AddWithValue("@levelId", obj.levelId);
                cmd.Parameters.AddWithValue("@facultyId", obj.facultyId);
                cmd.Parameters.AddWithValue("@pic", obj.pic);

                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@adminId", obj.adminId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
                //var item = db.spApplicantRegister(obj.name, obj.pmdcNo, obj.emailId, obj.password, obj.contactNumber, obj.network
                //       , obj.levelId, obj.facultyId, obj.pic).FirstOrDefault();
                //msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantUpdate(Applicant obj)
        {
            Message msg = new Message();
            try
            {

                var item = db.spApplicantUpdate(obj.applicantId, obj.name, obj.pmdcNo, obj.emailId
                    , obj.contactNumber, obj.adminId).FirstOrDefault();
                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantUpdateByAdmin(Applicant obj)
        {



            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantUpdateAdmin]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo);
            cmd.Parameters.AddWithValue("@emailId", obj.emailId);
            cmd.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
            cmd.Parameters.AddWithValue("@facultyId", obj.facultyId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);

        }

        public Message ChangePassword(ChangePassword obj)
        {
            Message msg = new Message();
            try
            {

                var item = db.spApplicantChangePassword(obj.id, obj.passwordOld, obj.passwordNew).FirstOrDefault();
                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Applicant GetApplicant(int inductionId, int applicantId)
        {
            Applicant obj = new Applicant();
            try
            {
                var dbt = db.tvwApplicants.FirstOrDefault(x => x.applicantId == applicantId);
                obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                obj = new Applicant();
            }
            return obj;
        }

        public DataTable GetApplicantByIdAdmin(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetByIdAdmin]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable GetApplicantById(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetById]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message GetApplicantIdBySearch(string search, string condition)
        {
            Message msg = new Message();
            try
            {

                var item = db.spApplicantGetBySearch(search, condition).FirstOrDefault();
                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Applicant GetApplicantByEmail(string emailId)
        {
            Applicant obj = new Applicant();
            try
            {
                var dbt = db.tvwApplicants.FirstOrDefault(x => x.emailId == emailId);
                obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                obj = new Applicant();
            }
            return obj;
        }

        //public ApplicantStatus GetApplicantStatus(int applicantId)
        //{
        //    ApplicantStatus obj = new ApplicantStatus();
        //    try
        //    {
        //        var dbt = db.spApplicantStatusGet(applicantId).FirstOrDefault();
        //        obj = MapApplicant.ToEntity(dbt);
        //    }
        //    catch (Exception)
        //    {
        //        obj = new ApplicantStatus();
        //    }
        //    return obj;
        //}

        public List<ApplicationStatus> GetApplicationStatus(int inductionId, int phaseId, int applicantId, int statusTypeId = 0)
        {
            List<ApplicationStatus> list = new List<ApplicationStatus>();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationStatusGet]"
                };
                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@phaseId", phaseId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        ApplicationStatus obj = new ApplicationStatus();

                        obj.inductionId = dr["inductionId"].TooInt();
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.statusTypeId = dr["statusTypeId"].TooInt();
                        obj.statusId = dr["statusId"].TooInt();

                        obj.statusType = dr["statusType"].TooString();
                        obj.status = dr["status"].TooString();
                        list.Add(obj);
                    }
                }


                //var dbt = db.spApplicationStatusGet(inductionId, phaseId, applicantId, statusTypeId);
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                list = new List<ApplicationStatus>();
            }
            return list;
        }


        public Message ApplicantStatusUpdate(int applicantId, int statusTypeId, int status)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantStatusUpdate(applicantId, statusTypeId, status);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }


        public Message AccountStatusUpdate(int applicantId, int statusId, int adminId)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spAccountStatusUpdate]"
                };

                cmd.Parameters.AddWithValue("@adminId", adminId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusId", statusId);

                msg = PrpDbADO.FillDataTableMessage(cmd);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDebarStatusUpdate(int applicantId, int statusId, string image, int adminId)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantDebarStatusUpdate]"
                };

                cmd.Parameters.AddWithValue("@adminId", adminId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusId", statusId);
                cmd.Parameters.AddWithValue("@image", image);

                msg = PrpDbADO.FillDataTableMessage(cmd);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        public Message ReopenApplication(int applicantId, int status)
        {
            Message msg = new Message();
            try
            {
                db.spApplicationReopen(applicantId, status);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        public List<Applicant> GetApplicantList(int accountStatusId = 1)
        {
            List<Applicant> list = new List<Applicant>();
            try
            {
                //var dbt = db.tvwApplicants.Where(x => x.accountStatusId == accountStatusId).ToList();
                //list = MapApplicant.ToEntityList(dbt);
            }
            catch (Exception)
            {
                list = new List<Applicant>();
            }
            return list;
        }

        #endregion

        #region ApplicantInfo

        public Message ApplicantInfoAddUpdate(ApplicantInfo obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantAddUpdateInfo(obj.inductionId, obj.phaseId, obj.applicantId, obj.fatherName, obj.genderId, obj.disableId, obj.dob
                    , obj.pmdcExpiryDate, obj.mbbsPassingDate, obj.countryIdDegreePassing, obj.dualNationalityType
                   , obj.countryId, obj.districtId, obj.districtName, obj.domicileProvinceId, obj.domicileDistrictId
                    , obj.address, obj.cnicNo, obj.cnicExpiryDate, obj.cnicPicFront, obj.cnicPicBack
                    , obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantInfoDualNationalAddUpdate(ApplicantInfoDualNational obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantAddUpdateInfoDualNational(0, 0, obj.applicantId, obj.countryId, obj.embassyCertificate, obj.languageCertificate
                    , obj.policeCertificate, obj.affidavitCertificate);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public ApplicantInfo GetApplicantInfo(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfo obj = new ApplicantInfo();
            try
            {
                var dbt = db.tblApplicantInfoes.FirstOrDefault(x => x.applicantId == applicantId);
                if (dbt != null && dbt.applicantDetailId > 0)
                    obj = MapApplicantInfo.ToEntity(dbt);
                else obj = new ApplicantInfo();
            }
            catch (Exception)
            {
                obj = new ApplicantInfo();
            }
            return obj;
        }

        public ApplicantInfoDualNational GetApplicantInfoDualNational(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfoDualNational obj = new ApplicantInfoDualNational();
            try
            {
                var dbt = db.tvwApplicantInfoDualNationals.FirstOrDefault(x => x.applicantId == applicantId);
                if (dbt != null && dbt.dualInfoId > 0)
                    obj = MapApplicantInfo.ToEntity(dbt);
                else obj = new ApplicantInfoDualNational();
            }
            catch (Exception)
            {
                obj = new ApplicantInfoDualNational();
            }
            return obj;
        }

        public ApplicantInfo GetApplicantInfoDetail(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfo obj = new ApplicantInfo();

            try
            {
                if (inductionId == 0)
                {
                    var dbt = db.tvwApplicantInfoes.FirstOrDefault(x => x.applicantId == applicantId);
                    if (dbt != null && dbt.applicantDetailId > 0)
                        obj = MapApplicantInfo.ToEntity(dbt);
                    else obj = new ApplicantInfo();
                }
                else
                {
                    var dbt = db.tvwfApplicantInfoes.FirstOrDefault(x => x.applicantId == applicantId && x.inductionId == inductionId);
                    if (dbt != null && dbt.applicantDetailId > 0)
                        obj = MapApplicantInfo.ToEntity(dbt);
                    else obj = new ApplicantInfo();
                }
            }
            catch (Exception ex)
            {
                obj = new ApplicantInfo();
            }
            return obj;
        }

        #endregion

        #region Education


        public Message ApplicantDegreeMarksMakeAccurate(int inductionId, int phaseId, int applicantId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDegreeMarksMakeAccurate(applicantId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantDegreeAddUpdate(ApplicantDegree obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDegreeAddUpdate(obj.applicantDegreeDetailId, obj.inductionId, obj.phaseId, obj.applicantId, obj.graduateTypeId
                    , obj.degreeTypeId, obj.degreeYear, obj.provinceId, obj.instituteTypeId, obj.instituteId, obj.instituteName
                    , obj.totalMarks, obj.obtainMarks, obj.imageDegree, obj.imageDegreeForeignFront, obj.imageDegreeForeignBack
                    , obj.imageDegreeMatric, obj.imageCertificate, obj.typeIds, obj.fcpsExemptionStatus, obj.fcpsExemptionCertificate);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDegreeMarksAddUpdate(int inductionId, int phaseId, List<ApplicantDegreeMark> listMarks)
        {
            int applicantId = 0;
            Message msg = new Message();
            try
            {
                foreach (ApplicantDegreeMark objMark in listMarks)
                {
                    try
                    {
                        applicantId = objMark.applicantId;
                        db.spApplicantDegreeMarksAddUpdate(objMark.degreeMarksId, inductionId, phaseId, objMark.applicantId, objMark.graduateTypeId
                            , objMark.year
                            , objMark.totalMarks, objMark.obtainMarks, objMark.attempt, objMark.imageDMC);
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                }
                msg.status = true;


            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public ApplicantDegree GetApplicantDegree(int inductionId, int phaseId, int applicantId)
        {
            ApplicantDegree obj = new ApplicantDegree();
            try
            {
                var dbt = db.tvwApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId);
                obj = MapApplicantDegree.ToEntity(dbt);
            }
            catch (Exception)
            {
                obj = new ApplicantDegree();
            }
            return obj;
        }

        public ApplicantDegree GetApplicantDegreeDetail(int inductionId, int phaseId, int applicantId)
        {


            ApplicantDegree obj = new ApplicantDegree();
            try
            {
                if (inductionId == 0)
                {
                    try
                    {
                        ApplicantDegreeMarksMakeAccurate(inductionId, phaseId, applicantId);
                    }
                    catch (Exception)
                    {
                    }
                    var dbt = db.tvwApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId);
                    obj = MapApplicantDegree.ToEntity(dbt);
                }
                else
                {
                    var dbt = db.tvwfApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId && x.inductionId == inductionId);
                    obj = MapApplicantDegree.ToEntity(dbt);
                }
            }
            catch (Exception)
            {
                obj = new ApplicantDegree();
            }
            return obj;
        }

        public List<ApplicantDegreeMark> GetApplicantDegreeMarkList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantDegreeMark> list = new List<ApplicantDegreeMark>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tblApplicantDegreeMarks.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantDegreeMark.ToEntityList(listt);
                }
                else
                {

                    var listt = db.tblApplicantDegreeAttemptFinals.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantDegreeMark.ToEntityList(listt);

                }

            }
            catch (Exception)
            {
                list = new List<ApplicantDegreeMark>();
            }
            return list;
        }

        public Message ApplicantCertificateAddUpdate(List<ApplicantCertificate> listCertificate)
        {
            Message msg = new Message();
            try
            {
                foreach (ApplicantCertificate obj in listCertificate)
                {
                    try
                    {
                        db.spApplicantCertificateAddUpdate(obj.applicantCertificateTypeId, obj.inductionId, obj.phaseId
                            , obj.applicantId, obj.typeId
                           , obj.disciplineId, obj.obtainMarks, obj.totalMarks, obj.passingDate, obj.imageCertificate);
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                }
                msg.status = true;


            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public List<ApplicantCertificate> GetApplicantCertificateList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            try
            {
                var listt = db.tblApplicantCertificates.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantCertificate.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantCertificate>();
            }
            return list;
        }


        public List<ApplicantCertificate> GetApplicantCertificateListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tvwApplicantCertificates.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantCertificate.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tvwfApplicantCertificates.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantCertificate.ToEntityList(listt);

                }
            }
            catch (Exception)
            {
                list = new List<ApplicantCertificate>();
            }
            return list;
        }
        #endregion

        #region HouseJob

        public List<ApplicantHouseJob> GetApplicantHouseJobList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            try
            {
                var listt = db.tvwApplicantHouseJobs.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantHouseJob.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantHouseJob>();
            }
            return list;
        }

        public List<ApplicantHouseJob> GetApplicantHouseJobListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            try
            {
                var listt = db.tvwApplicantHouseJobs.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantHouseJob.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantHouseJob>();
            }
            return list;
        }



        public Message ApplicantHouseJobDeleteSingle(int houseJodId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantHouseJobDeleteByApplicant(houseJodId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantHouseJobAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@houseJodId", obj.houseJodId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@provinceId", obj.provinceId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isSame", obj.isSame);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@hospital", obj.hospital);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@image", obj.image);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        #endregion

        #region Exprience

        public List<ApplicantExperience> GetApplicantExperienceList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                var listt = db.tblApplicantExperiences.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantExperience.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public List<ApplicantExperience> GetApplicantExperienceListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tvwApplicantExperiences.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantExperience.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tvwfApplicantExperiences.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantExperience.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public List<ApplicantExperience> GetApplicantExperienceListByApplicant(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                var listt = db.spApplicantExperienceByApplicant(inductionId, phaseId, applicantId).ToList();
                list = MapApplicantExperience.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public Message ApplicantExperienceDeleteSingle(int inductionId, int phaseId, int applicantExperienceId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantExperienceDeleteByApplicant(applicantExperienceId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantExperienceAddUpdate(ApplicantExperience obj)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spApplicantExperienceAddUpdate(obj.applicantExperienceId, obj.inductionId, obj.phaseId, obj.applicantId, obj.levelId
                      , obj.levelTypeId, obj.instituteName, obj.instituteId, obj.provinceId, obj.typeId
                      , obj.startDated, obj.endDate
                      , obj.isCurrent, obj.imageExperience).FirstOrDefault();
                msg = MapApplicantExperience.ToEntity(objt);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        #endregion

        #region Distinction

        public List<ApplicantDistinction> GetApplicantDistinctionList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantDistinction> list = new List<ApplicantDistinction>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tblApplicantDistinctions.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantDistinction.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tblApplicantDistinctionFinals.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantDistinction.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantDistinction>();
            }
            return list;
        }

        public Message ApplicantDistinctionDeleteSingle(int inductionId, int phaseId, int applicantDistinctionId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDistinctionDeleteByApplicant(applicantDistinctionId, inductionId, phaseId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDistinctionAddUpdate(ApplicantDistinction obj)
        {


            Message msg = new Message();
            try
            {

                var objt = db.spApplicantDistinctionAddUpdate(obj.applicantDistinctionId, obj.inductionId, obj.phaseId
                    , obj.applicantId, obj.subject, obj.year, obj.imageDistinction).FirstOrDefault();
                msg = MapApplicantDistinction.ToEntity(objt);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message GetApplicantVerifyDistictionCountByApplicant(int inductionId, int phaseId, int applicantId)
        {
            Message msg = new Message();
            try
            {
                //var objt = db.spApplicantVerifyDistictionCountByApplicant(applicantId).FirstOrDefault();
                //if (objt == null)
                //{
                //    msg.id = 0;
                //    msg.status = false;
                //}
                //else if (objt != null && objt.id > 0)
                //{
                //    msg.id = objt.id.TooInt();
                //    msg.status = true;
                //}
                //else
                //{
                //    msg.id = 0;
                //    msg.status = false;
                //}
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
            }
            return msg;
        }

        #endregion

        #region Research Paper

        public List<ApplicantResearchPaper> GetApplicantResearchPaperList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            try
            {
                var listt = db.spApplicantResearchPaperByApplicant(inductionId, phaseId, applicantId).ToList();
                list = MapApplicantResearchPaper.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantResearchPaper>();
            }
            return list;
        }

        public List<ApplicantResearchPaper> GetApplicantResearchPaperListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.spApplicantResearchPaperByApplicant(inductionId, phaseId, applicantId).ToList();
                    list = MapApplicantResearchPaper.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tblApplicantResearchPaperFinals.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantResearchPaper.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantResearchPaper>();
            }
            return list;
        }

        public Message ApplicantResearchPaperDeleteSingle(int inductionId, int phaseId, int applicantResearchId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantResearchPaperDeleteByApplicant(applicantResearchId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantResearchPaperAddUpdate(ApplicantResearchPaper obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantResearchPaperAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantResearchId", obj.applicantResearchId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@authorId", obj.authorId);
            cmd.Parameters.AddWithValue("@publishStatusId", obj.publishStatusId);
            cmd.Parameters.AddWithValue("@link", obj.link);
            cmd.Parameters.AddWithValue("@webLink", obj.webLink);
            cmd.Parameters.AddWithValue("@imageLetter", obj.imageLetter);
            cmd.Parameters.AddWithValue("@researchJournalId", obj.researchJournalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }


        #endregion

        #region Applicant Specility

        public List<ApplicantSpecility> GetApplicantSpecilityList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {
                var listt = db.spApplicantSpecilityByApplicant(inductionId, phaseId, applicantId).ToList();
                list = MapApplicantSpecility.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantSpecility>();
            }
            return list;
        }


        public List<ApplicantSpecility> GetApplicantSpecilityListWithMarks(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.spApplicantSpecilityWithMarksByApplicant(applicantId, inductionId, phaseId).ToList();
                    list = MapApplicantSpecility.ToEntityList(listt);
                }
                else
                {
                    var listt = db.spApplicantSpecilityWithMarksByApplicantFinal(applicantId, inductionId, phaseId).ToList();
                    list = MapApplicantSpecility.ToEntityList(listt);
                }
            }
            catch (Exception ex)
            {
                list = new List<ApplicantSpecility>();
            }
            return list;
        }

        public List<ApplicantSpecility> GetApplicantSpecilityListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {
                var listt = db.tvwApplicantSpecilities.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantSpecility.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantSpecility>();
            }
            return list;
        }


        public Message ApplicantSpecilityDeleteSingle(int inductionId, int phaseId, int applicantSpecilityId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantSpecilityDeleteByApplicant(applicantSpecilityId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantSpecilityCheckPreferenceNo(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                var item = db.spApplicantSpecilityCheckPreferenceNo(obj.applicantSpecilityId, obj.inductionId, obj.phaseId, obj.applicantId, obj.preferenceNo
                     , obj.typeId, obj.specialityId).FirstOrDefault();

                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantSpecilityAddUpdate(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                var item = db.spApplicantSpecilityAddUpdate(obj.applicantSpecilityId, obj.inductionId, obj.phaseId, obj.applicantId, obj.preferenceNo
                     , obj.typeId, obj.specialityId, obj.hospitalId).FirstOrDefault();

                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }
        #endregion

        #region Applicant Voucher


        public ApplicantVoucher GetApplicantVoucher(int inductionId, int phaseId, int applicantId)
        {
            ApplicantVoucher obj = new ApplicantVoucher();
            try
            {
                if (inductionId == 0)
                {
                    var dbt = db.tblApplicantVouchers.FirstOrDefault(x => x.applicantId == applicantId);
                    if (dbt != null && dbt.applicantVoucherId > 0)
                        obj = MapVoucher.ToEntity(dbt);
                    else obj = new ApplicantVoucher();
                }
                else
                {
                    var dbt = db.tblApplicantVoucherFinals.FirstOrDefault(x => x.inductionId == inductionId && x.applicantId == applicantId);
                    if (dbt != null && dbt.applicantVoucherId > 0)
                        obj = MapVoucher.ToEntity(dbt);
                    else obj = new ApplicantVoucher();

                }
            }
            catch (Exception ex)
            {
                obj = new ApplicantVoucher();
            }
            return obj;
        }


        public Message ApplicantVoucherAddUpdate(ApplicantVoucher obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantVoucherAddUpdate(obj.applicantId, obj.inductionId, obj.phaseId
                    , obj.amount, obj.branchCode, obj.voucherImage
                    , obj.ibn, obj.accountNo, obj.accountTitle, obj.submittedDate);

                msg.status = true;
                msg.message = "Transaction saved successfully";
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = "System error.";
            }
            return msg;
        }

        public MessageAPI ApplicantVoucherBankAddUpdate(ApplicantVoucherBank obj)
        {
            MessageAPI msg = new MessageAPI();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantVoucherAddUpdateBank]"
                };
                cmd.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
                cmd.Parameters.AddWithValue("@amount", obj.amount);
                cmd.Parameters.AddWithValue("@transactionIdBank", obj.transactionIdBank);
                cmd.Parameters.AddWithValue("@statusBank", obj.statusBank);
                cmd.Parameters.AddWithValue("@transactionType", obj.transactionType);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg.message = dr["message"].TooString();
                    msg.status = dr["status"].TooBoolean();
                }
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;

            }

            return msg;
        }

        //public Message ApplicantVoucherBankAddUpdate(ApplicantVoucherBank obj)
        //{
        //    Message msg = new Message();
        //    try
        //    {
        //        db.spApplicantVoucherAddUpdateBank(obj.applicantNo, obj.amount, obj.transactionIdBank, obj.statusBank, obj.transactionType);

        //        msg.status = true;
        //        msg.message = "Transaction saved successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.status = false;
        //        msg.msg = "System error.";
        //    }
        //    return msg;
        //}


        public ApplicantVoucherBank ApplicantVoucherGetByApplicantNo(string applicantNo, int transactionType)
        {
            ApplicantVoucherBank obj = new ApplicantVoucherBank();


            try
            {
                var ss = db.spApplicantVoucherGetByApplicantNo(applicantNo, transactionType).FirstOrDefault();

                obj = MapVoucher.ToEntity(ss);
            }
            catch (Exception ex)
            {
                obj = new ApplicantVoucherBank();
                obj.status = "System Error.";
            }

            return obj;
        }
        #endregion


        #region Admin

        #region Applicant Status

        public List<Applicant> GetApplicantList(ApplicantEntity obj)
        {
            List<Applicant> list = new List<Applicant>();
            try
            {
                var listt = db.spApplicantGetList(obj.accountStatusId, obj.applicationStatusId, obj.facultyId, obj.condition).ToList();
                list = MapApplicant.ToEntityList(listt);
            }
            catch (Exception ex)
            {
                list = new List<Applicant>();
            }
            return list;
        }

        #endregion

        #endregion




    }
}
