using Prp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;

namespace Prp.Data
{
    public class ApplicantDAL : PrpDBConnect
    {
        public ApplicantDAL()
        {
        }

        public Message AccountStatusUpdate(int applicantId, int statusId, int adminId)
        {
            Message message = new Message();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spAccountStatusUpdate]"
                };
                sqlCommand.Parameters.AddWithValue("@adminId", adminId);
                sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
                sqlCommand.Parameters.AddWithValue("@statusId", statusId);
                message = PrpDbADO.FillDataTableMessage(sqlCommand, 0);
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.message = exception.Message;
            }
            return message;
        }

        public Message ApplicantCertificateAddUpdate(List<ApplicantCertificate> listCertificate)
        {
            Message message = new Message();
            try
            {
                foreach (ApplicantCertificate applicantCertificate in listCertificate)
                {
                    try
                    {
                        this.db.spApplicantCertificateAddUpdate(new int?(applicantCertificate.applicantCertificateTypeId), new int?(applicantCertificate.inductionId), new int?(applicantCertificate.phaseId), new int?(applicantCertificate.applicantId), new int?(applicantCertificate.typeId), new int?(applicantCertificate.disciplineId), new int?(applicantCertificate.obtainMarks), new int?(applicantCertificate.totalMarks), new DateTime?(applicantCertificate.passingDate), applicantCertificate.imageCertificate);
                    }
                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                message.status = true;
            }
            catch (Exception exception3)
            {
                Exception exception2 = exception3;
                message.status = false;
                message.msg = exception2.Message;
            }
            return message;
        }

        public Message ApplicantDebarStatusUpdate(int applicantId, int statusId, string image, int adminId)
        {
            Message message = new Message();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantDebarStatusUpdate]"
                };
                sqlCommand.Parameters.AddWithValue("@adminId", adminId);
                sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
                sqlCommand.Parameters.AddWithValue("@statusId", statusId);
                sqlCommand.Parameters.AddWithValue("@image", image);
                message = PrpDbADO.FillDataTableMessage(sqlCommand, 0);
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.message = exception.Message;
            }
            return message;
        }

        public Message ApplicantDegreeAddUpdate(ApplicantDegree obj)
        {
            Message message = new Message();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantDegreeAddUpdate]"
                };
                sqlCommand.Parameters.AddWithValue("@applicantDegreeDetailId", obj.applicantDegreeDetailId);
                sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
                sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
                sqlCommand.Parameters.AddWithValue("@graduateTypeId", obj.graduateTypeId);
                sqlCommand.Parameters.AddWithValue("@degreeTypeId", obj.degreeTypeId);
                sqlCommand.Parameters.AddWithValue("@degreeYear", obj.degreeYear);
                sqlCommand.Parameters.AddWithValue("@provinceId", obj.provinceId);
                sqlCommand.Parameters.AddWithValue("@instituteTypeId", obj.instituteTypeId);
                sqlCommand.Parameters.AddWithValue("@instituteId", obj.instituteId);
                sqlCommand.Parameters.AddWithValue("@instituteName", obj.instituteName);
                sqlCommand.Parameters.AddWithValue("@totalMarks", obj.totalMarks);
                sqlCommand.Parameters.AddWithValue("@obtainMarks", obj.obtainMarks);
                sqlCommand.Parameters.AddWithValue("@imageDegree", obj.imageDegree);
                sqlCommand.Parameters.AddWithValue("@imageDegreeForeignFront", obj.imageDegreeForeignFront);

                sqlCommand.Parameters.AddWithValue("@imageDegreeForeignBack", obj.imageDegreeForeignBack);
                sqlCommand.Parameters.AddWithValue("@imageDegreeMatric", obj.imageDegreeMatric);
                sqlCommand.Parameters.AddWithValue("@imageCertificate", obj.imageCertificate);
                sqlCommand.Parameters.AddWithValue("@fcpsExemptionStatus", obj.fcpsExemptionStatus);

                sqlCommand.Parameters.AddWithValue("@fcpsExemptionCertificate", obj.fcpsExemptionCertificate);
                sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
                message = PrpDbADO.FillDataTableMessage(sqlCommand);
                //this.db.spApplicantDegreeAddUpdate(new int?(obj.applicantDegreeDetailId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), new int?(obj.graduateTypeId), new int?(obj.degreeTypeId), new int?(obj.degreeYear), new int?(obj.provinceId), new int?(obj.instituteTypeId), new int?(obj.instituteId), obj.instituteName, new int?(obj.totalMarks), new int?(obj.obtainMarks), obj.imageDegree, obj.imageDegreeForeignFront, obj.imageDegreeForeignBack, obj.imageDegreeMatric, obj.imageCertificate, obj.typeIds, new bool?(obj.fcpsExemptionStatus), obj.fcpsExemptionCertificate);
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantDegreeMarksAddUpdate(int inductionId, int phaseId, List<ApplicantDegreeMark> listMarks)
        {
            int num = 0;
            Message message = new Message();
            try
            {
                foreach (ApplicantDegreeMark obj in listMarks)
                {
                    try
                    {
                        num = obj.applicantId;

                        SqlCommand sqlCommand = new SqlCommand()
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandText = "[dbo].[spApplicantDegreeMarksAddUpdate]"
                        };
                        sqlCommand.Parameters.AddWithValue("@degreeMarksId", obj.degreeMarksId);
                        sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
                        sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
                        sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
                        sqlCommand.Parameters.AddWithValue("@graduateTypeId", obj.graduateTypeId);
                        sqlCommand.Parameters.AddWithValue("@year", obj.year);
                        sqlCommand.Parameters.AddWithValue("@obtainMarks", obj.obtainMarks);
                        sqlCommand.Parameters.AddWithValue("@totalMarks", obj.totalMarks);
                        sqlCommand.Parameters.AddWithValue("@attempt", obj.attempt);
                        sqlCommand.Parameters.AddWithValue("@imageDMC", obj.imageDMC);
                        sqlCommand.Parameters.AddWithValue("@position", obj.position);
                        message = PrpDbADO.FillDataTableMessage(sqlCommand);
                        //this.db.spApplicantDegreeMarksAddUpdate(new int?(listMark.degreeMarksId), new int?(inductionId)
                        //    , new int?(phaseId), new int?(listMark.applicantId), new int?(listMark.graduateTypeId)
                        //    , new int?(listMark.year), new int?(listMark.totalMarks), new int?(listMark.obtainMarks)
                        //    , new int?(listMark.attempt), listMark.imageDMC, new int?(listMark.position)
                        //    , listMark.imgPosition);
                    }


                    catch (Exception exception1)
                    {
                        Exception exception = exception1;
                        message.status = false;
                        message.msg = exception.Message;
                    }
                }
                message.status = true;
            }
            catch (Exception exception3)
            {
                Exception exception2 = exception3;
                message.status = false;
                message.msg = exception2.Message;
            }
            return message;
        }

        public Message ApplicantDegreeMarksMakeAccurate(int inductionId, int phaseId, int applicantId)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantDegreeMarksMakeAccurate(new int?(applicantId));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public DataSet ApplicantDistinctionAddUpdate(ApplicantDistinction obj)
        {

            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantDistinctionAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantDistinctionId", obj.applicantDistinctionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@subject", obj.subject);
            sqlCommand.Parameters.AddWithValue("@position", obj.position);
            sqlCommand.Parameters.AddWithValue("@year", obj.year);
            sqlCommand.Parameters.AddWithValue("@imageDistinction", obj.imageDistinction);
            sqlCommand.Parameters.AddWithValue("@instituteId", obj.instituteId);
            return PrpDbADO.FillDataSet(sqlCommand);

            
        }

        public DataSet ApplicantDistinctionDeleteSingle(ApplicantDistinction obj)
        {

            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantDistinctionDeleteByApplicant]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantDistinctionId", obj.applicantDistinctionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
         
            return PrpDbADO.FillDataSet(sqlCommand);

           
        }

        public Message ApplicantExperienceAddUpdate(ApplicantExperience obj)
        {
            Message message = new Message();
            try
            {
                spApplicantExperienceAddUpdate_Result spApplicantExperienceAddUpdateResult = this.db.spApplicantExperienceAddUpdate(new int?(obj.applicantExperienceId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), new int?(obj.levelId), new int?(obj.levelTypeId), obj.instituteName, new int?(obj.instituteId), new int?(obj.provinceId), new int?(obj.typeId), new DateTime?(obj.startDated), new DateTime?(obj.endDate), new bool?(obj.isCurrent), obj.imageExperience).FirstOrDefault<spApplicantExperienceAddUpdate_Result>();
                message = MapApplicantExperience.ToEntity(spApplicantExperienceAddUpdateResult);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantExperienceDeleteSingle(int inductionId, int phaseId, int applicantExperienceId)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantExperienceDeleteByApplicant(new int?(applicantExperienceId));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantHouseJobAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@houseJodId", obj.houseJodId);
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@provinceId", obj.provinceId);
            sqlCommand.Parameters.AddWithValue("@countryId", obj.countryId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@isSame", obj.isSame);
            sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            sqlCommand.Parameters.AddWithValue("@hospital", obj.hospital);
            sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
            sqlCommand.Parameters.AddWithValue("@image", obj.image);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message ApplicantHouseJobDeleteSingle(int houseJodId)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantHouseJobDeleteByApplicant(new int?(houseJodId));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantInfoAddUpdate(ApplicantInfo obj)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantAddUpdateInfo(new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), obj.fatherName, new int?(obj.genderId), new int?(obj.disableId), new DateTime?(obj.dob), new DateTime?(obj.pmdcExpiryDate), new DateTime?(obj.mbbsPassingDate), new int?(obj.countryIdDegreePassing), new int?(obj.dualNationalityType), new int?(obj.countryId), new int?(obj.districtId), obj.districtName, new int?(obj.domicileProvinceId), new int?(obj.domicileDistrictId), obj.address, obj.cnicNo, new DateTime?(obj.cnicExpiryDate), obj.cnicPicFront, obj.cnicPicBack, obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage);
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantInfoDualNationalAddUpdate(ApplicantInfoDualNational obj)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantAddUpdateInfoDualNational(new int?(0), new int?(0), new int?(obj.applicantId), new int?(obj.countryId), obj.embassyCertificate, obj.languageCertificate, obj.policeCertificate, obj.affidavitCertificate);
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantIsExist(IsExists obj)
        {
            Message message = new Message();
            try
            {
                spApplicantIsExist_Result spApplicantIsExistResult = this.db.spApplicantIsExist(new int?(obj.id), obj.search, obj.condition).FirstOrDefault<spApplicantIsExist_Result>();
                if ((spApplicantIsExistResult == null ? true : spApplicantIsExistResult.applicantId <= 0))
                {
                    message.id = 0;
                }
                else
                {
                    message.id = spApplicantIsExistResult.applicantId;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.id = 0;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantResearchPaperAddUpdate(ApplicantResearchPaper obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantResearchPaperAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantResearchId", obj.applicantResearchId);
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@name", obj.name);
            sqlCommand.Parameters.AddWithValue("@authorId", obj.authorId);
            sqlCommand.Parameters.AddWithValue("@publishStatusId", obj.publishStatusId);
            sqlCommand.Parameters.AddWithValue("@link", obj.link);
            sqlCommand.Parameters.AddWithValue("@webLink", obj.webLink);
            sqlCommand.Parameters.AddWithValue("@imageLetter", obj.imageLetter);
            sqlCommand.Parameters.AddWithValue("@researchJournalId", obj.researchJournalId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message ApplicantResearchPaperDeleteSingle(int inductionId, int phaseId, int applicantResearchId)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantResearchPaperDeleteByApplicant(new int?(applicantResearchId));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public DataSet ApplicantSpecilityAddUpdate(ApplicantSpecility obj)
        {

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSpecilityAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@applicantSpecilityId", obj.applicantSpecilityId);
            cmd.Parameters.AddWithValue("@specialityJobId", obj.specialityJobId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@preferenceNo", obj.preferenceNo);
            return PrpDbADO.FillDataSet(cmd);
        }

        public DataSet ApplicantSpecilityDeleteSingle(ApplicantSpecility obj)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSpecilityDeleteByApplicant]"
            };
            cmd.Parameters.AddWithValue("@applicantSpecilityId", obj.applicantSpecilityId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            return PrpDbADO.FillDataSet(cmd);
        }

       
        public Message ApplicantSpecilityCheckPreferenceNo(ApplicantSpecility obj)
        {
            Message message = new Message();
            try
            {
                spApplicantSpecilityCheckPreferenceNo_Result spApplicantSpecilityCheckPreferenceNoResult = this.db.spApplicantSpecilityCheckPreferenceNo(new int?(obj.applicantSpecilityId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), new int?(obj.preferenceNo), new int?(obj.typeId), new int?(obj.specialityId)).FirstOrDefault<spApplicantSpecilityCheckPreferenceNo_Result>();
                message = MapApplicant.ToEntity(spApplicantSpecilityCheckPreferenceNoResult);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantStatusUpdate(ApplicationStatus obj)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantStatusUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message ApplicantStatusUpdate(int applicantId, int statusTypeId, int status)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicantStatusUpdate(new int?(applicantId), new int?(statusTypeId), new int?(status));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.message = exception.Message;
            }
            return message;
        }

        public Message ApplicantUpdate(Applicant obj)
        {
            Message message = new Message();
            try
            {
                spApplicantUpdate_Result spApplicantUpdateResult = this.db.spApplicantUpdate(new int?(obj.applicantId), obj.name, obj.pmdcNo, obj.emailId, obj.contactNumber, new int?(obj.adminId)).FirstOrDefault<spApplicantUpdate_Result>();
                message = MapApplicant.ToEntity(spApplicantUpdateResult);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Message ApplicantUpdateByAdmin(Applicant obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantUpdateAdmin]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@name", obj.name);
            sqlCommand.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo);
            sqlCommand.Parameters.AddWithValue("@emailId", obj.emailId);
            sqlCommand.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
            sqlCommand.Parameters.AddWithValue("@facultyId", obj.facultyId);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message ApplicantVoucherAddUpdate(ApplicantVoucher obj)
        {

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantVoucherAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@branchCode", obj.branchCode.TooString());
            cmd.Parameters.AddWithValue("@voucherImage", obj.voucherImage.TooString());
            cmd.Parameters.AddWithValue("@accountNo", obj.accountNo.TooString());
            cmd.Parameters.AddWithValue("@ibn", obj.ibn.TooString());
            cmd.Parameters.AddWithValue("@accountTitle", obj.accountTitle.TooString());
            cmd.Parameters.AddWithValue("@submittedDate", obj.submittedDate);
            return PrpDbADO.FillDataTableMessage(cmd, 0);

        }

        public MessageAPI ApplicantVoucherBankAddUpdate(ApplicantVoucherBank obj)
        {
            MessageAPI messageAPI = new MessageAPI();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantVoucherAddUpdateBank]"
                };
                sqlCommand.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
                sqlCommand.Parameters.AddWithValue("@amount", obj.amount);
                sqlCommand.Parameters.AddWithValue("@transactionIdBank", obj.transactionIdBank);
                sqlCommand.Parameters.AddWithValue("@statusBank", obj.statusBank);
                sqlCommand.Parameters.AddWithValue("@transactionType", obj.transactionType);
                DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow item = dataTable.Rows[0];
                    messageAPI.message = item["message"].TooString("");
                    messageAPI.status = item["status"].TooBoolean(false);
                }
            }
            catch (Exception exception)
            {
                messageAPI.message = "System Error";
                messageAPI.status = false;
            }
            return messageAPI;
        }

        public ApplicantVoucherBank ApplicantVoucherGetByApplicantNo(string applicantNo, int transactionType)
        {
            ApplicantVoucherBank applicantVoucherBank = new ApplicantVoucherBank();
            //try
            //{
            //    spApplicantVoucherGetByApplicantNo_Result spApplicantVoucherGetByApplicantNoResult = this.db.spApplicantVoucherGetByApplicantNo(applicantNo, new int?(transactionType)).FirstOrDefault<spApplicantVoucherGetByApplicantNo_Result>();
            //    applicantVoucherBank = MapVoucher.ToEntity(spApplicantVoucherGetByApplicantNoResult);
            //}
            //catch (Exception exception)
            //{
            //    applicantVoucherBank = new ApplicantVoucherBank()
            //    {
            //        status = "System Error."
            //    };
            //}
            return applicantVoucherBank;
        }

        public Message ChangePassword(ChangePassword obj)
        {
            Message message = new Message();
            try
            {
                spApplicantChangePassword_Result spApplicantChangePasswordResult = this.db.spApplicantChangePassword(new int?(obj.id), obj.passwordOld, obj.passwordNew).FirstOrDefault<spApplicantChangePassword_Result>();
                message = MapApplicant.ToEntity(spApplicantChangePasswordResult);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public Applicant GetApplicant(int inductionId, int applicantId)
        {
            Applicant applicant = new Applicant();
            try
            {
                tvwApplicant _tvwApplicant = this.db.tvwApplicants.FirstOrDefault<tvwApplicant>((tvwApplicant x) => x.applicantId == applicantId);
                applicant = MapApplicant.ToEntity(_tvwApplicant);
            }
            catch (Exception exception)
            {
                applicant = new Applicant();
            }
            return applicant;
        }

        public Applicant GetApplicantByEmail(string emailId)
        {
            Applicant applicant = new Applicant();
            try
            {
                tvwApplicant _tvwApplicant = this.db.tvwApplicants.FirstOrDefault<tvwApplicant>((tvwApplicant x) => x.emailId == emailId);
                applicant = MapApplicant.ToEntity(_tvwApplicant);
            }
            catch (Exception exception)
            {
                applicant = new Applicant();
            }
            return applicant;
        }

        public DataTable GetApplicantById(int applicantId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetById]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(sqlCommand, "");
        }

        public DataTable GetApplicantByIdAdmin(int applicantId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetByIdAdmin]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(sqlCommand, "");
        }

        public List<ApplicantCertificate> GetApplicantCertificateList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> applicantCertificates = new List<ApplicantCertificate>();
            try
            {
                List<tblApplicantCertificate> list = (
                    from x in this.db.tblApplicantCertificates
                    where x.applicantId == applicantId
                    select x).ToList<tblApplicantCertificate>();
                applicantCertificates = MapApplicantCertificate.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantCertificates = new List<ApplicantCertificate>();
            }
            return applicantCertificates;
        }

        public List<ApplicantCertificate> GetApplicantCertificateListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> applicantCertificates = new List<ApplicantCertificate>();
            try
            {
                if (inductionId != 0)
                {
                    List<tvwfApplicantCertificate> list = (
                        from x in this.db.tvwfApplicantCertificates
                        where x.applicantId == applicantId && x.inductionId == inductionId
                        select x).ToList<tvwfApplicantCertificate>();
                    applicantCertificates = MapApplicantCertificate.ToEntityList(list);
                }
                else
                {
                    List<tvwApplicantCertificate> tvwApplicantCertificates = (
                        from x in this.db.tvwApplicantCertificates
                        where x.applicantId == applicantId
                        select x).ToList<tvwApplicantCertificate>();
                    applicantCertificates = MapApplicantCertificate.ToEntityList(tvwApplicantCertificates);
                }
            }
            catch (Exception exception)
            {
                applicantCertificates = new List<ApplicantCertificate>();
            }
            return applicantCertificates;
        }

        public ApplicantDegree GetApplicantDegree(int inductionId, int phaseId, int applicantId)
        {
            ApplicantDegree applicantDegree = new ApplicantDegree();
            try
            {
                tvwApplicantDegree _tvwApplicantDegree = this.db.tvwApplicantDegrees.FirstOrDefault<tvwApplicantDegree>((tvwApplicantDegree x) => x.applicantId == applicantId);
                applicantDegree = MapApplicantDegree.ToEntity(_tvwApplicantDegree);
            }
            catch (Exception exception)
            {
                applicantDegree = new ApplicantDegree();
            }
            return applicantDegree;
        }

        public ApplicantDegree GetApplicantDegreeDetail(int inductionId, int phaseId, int applicantId)
        {
            ApplicantDegree applicantDegree = new ApplicantDegree();
            try
            {
                if (inductionId != 0)
                {
                    tvwfApplicantDegree _tvwfApplicantDegree = this.db.tvwfApplicantDegrees.FirstOrDefault<tvwfApplicantDegree>((tvwfApplicantDegree x) => x.applicantId == applicantId && x.inductionId == inductionId);
                    applicantDegree = MapApplicantDegree.ToEntity(_tvwfApplicantDegree);
                }
                else
                {
                    try
                    {
                        this.ApplicantDegreeMarksMakeAccurate(inductionId, phaseId, applicantId);
                    }
                    catch (Exception exception)
                    {
                    }
                    tvwApplicantDegree _tvwApplicantDegree = this.db.tvwApplicantDegrees.FirstOrDefault<tvwApplicantDegree>((tvwApplicantDegree x) => x.applicantId == applicantId);
                    applicantDegree = MapApplicantDegree.ToEntity(_tvwApplicantDegree);
                }
            }
            catch (Exception exception1)
            {
                applicantDegree = new ApplicantDegree();
            }
            return applicantDegree;
        }

        public List<ApplicantDegreeMark> GetApplicantDegreeMarkList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantDegreeMark> applicantDegreeMarks = new List<ApplicantDegreeMark>();
            try
            {
                if (inductionId != 0)
                {
                    List<tblApplicantDegreeAttemptFinal> list = (
                        from x in this.db.tblApplicantDegreeAttemptFinals
                        where x.applicantId == applicantId && x.inductionId == inductionId
                        select x).ToList<tblApplicantDegreeAttemptFinal>();
                    applicantDegreeMarks = MapApplicantDegreeMark.ToEntityList(list);
                }
                else
                {
                    List<tblApplicantDegreeMark> tblApplicantDegreeMarks = (
                        from x in this.db.tblApplicantDegreeMarks
                        where x.applicantId == applicantId
                        select x).ToList<tblApplicantDegreeMark>();
                    applicantDegreeMarks = MapApplicantDegreeMark.ToEntityList(tblApplicantDegreeMarks);
                    foreach (ApplicantDegreeMark item in applicantDegreeMarks)
                    {
                        SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
                        string query = "Select Top 1 position, imagePosition from tblApplicantDegreeMarks where degreeMarksId = " + item.degreeMarksId + " ";
                        SqlCommand cmd = new SqlCommand(query, con);
                        DataTable dt = new DataTable();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        con.Open();
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            item.position = dt.Rows[0][0].TooInt();
                            item.imgPosition = dt.Rows[0][1].TooString();
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception exception)
            {
                applicantDegreeMarks = new List<ApplicantDegreeMark>();
            }
            return applicantDegreeMarks;
        }

        public List<ApplicantDistinction> GetApplicantDistinctionList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantDistinction> applicantDistinctions = new List<ApplicantDistinction>();
            try
            {
                if (inductionId != 0)
                {
                    List<tblApplicantDistinctionFinal> list = (
                        from x in this.db.tblApplicantDistinctionFinals
                        where x.applicantId == applicantId && x.inductionId == inductionId
                        select x).ToList<tblApplicantDistinctionFinal>();
                    applicantDistinctions = MapApplicantDistinction.ToEntityList(list);
                }
                else
                {
                    List<tblApplicantDistinction> tblApplicantDistinctions = (
                        from x in this.db.tblApplicantDistinctions
                        where x.applicantId == applicantId
                        select x).ToList<tblApplicantDistinction>();
                    applicantDistinctions = MapApplicantDistinction.ToEntityList(tblApplicantDistinctions);
                }
            }
            catch (Exception exception)
            {
                applicantDistinctions = new List<ApplicantDistinction>();
            }
            //foreach (ApplicantDistinction applicantDistinction in applicantDistinctions)
            //{
            //    int num = applicantDistinction.applicantDistinctionId;
            //    string str = string.Concat("select top(1) position, university from tblApplicantDistinction where applicantDistinctionId = ", num.ToString());
            //    SqlConnection sqlConnection = new SqlConnection();
            //    DataTable dataTable = new DataTable();
            //    Message message = new Message();
            //    SqlCommand sqlCommand = new SqlCommand(str);
            //    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            //    try
            //    {
            //        try
            //        {
            //            sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
            //            sqlConnection.Open();
            //            sqlCommand.Connection = sqlConnection;
            //            sqlDataAdapter.Fill(dataTable);
            //            if (dataTable.Rows.Count > 0)
            //            {
            //                int num1 = Convert.ToInt32(dataTable.Rows[0][0]);
            //                applicantDistinction.position = num1;
            //                string str1 = dataTable.Rows[0][1].TooString("");
            //                applicantDistinction.institute = str1;
            //            }
            //        }
            //        catch (Exception exception1)
            //        {
            //            applicantDistinction.position = 0;
            //        }
            //    }
            //    finally
            //    {
            //        sqlConnection.Close();
            //    }
            //}
            return applicantDistinctions;
        }

        public List<ApplicantExperience> GetApplicantExperienceList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> applicantExperiences = new List<ApplicantExperience>();
            try
            {
                List<tblApplicantExperience> list = (
                    from x in this.db.tblApplicantExperiences
                    where x.applicantId == applicantId
                    select x).ToList<tblApplicantExperience>();
                applicantExperiences = MapApplicantExperience.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantExperiences = new List<ApplicantExperience>();
            }
            return applicantExperiences;
        }

        public List<ApplicantExperience> GetApplicantExperienceListByApplicant(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> applicantExperiences = new List<ApplicantExperience>();
            try
            {
                List<spApplicantExperienceByApplicant_Result> list = this.db.spApplicantExperienceByApplicant(new int?(inductionId), new int?(phaseId), new int?(applicantId)).ToList<spApplicantExperienceByApplicant_Result>();
                applicantExperiences = MapApplicantExperience.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantExperiences = new List<ApplicantExperience>();
            }
            return applicantExperiences;
        }

        public List<ApplicantExperience> GetApplicantExperienceListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> applicantExperiences = new List<ApplicantExperience>();
            try
            {
                if (inductionId != 0)
                {
                    List<tvwfApplicantExperience> list = (
                        from x in this.db.tvwfApplicantExperiences
                        where x.applicantId == applicantId && x.inductionId == inductionId
                        select x).ToList<tvwfApplicantExperience>();
                    applicantExperiences = MapApplicantExperience.ToEntityList(list);
                }
                else
                {
                    List<tvwApplicantExperience> tvwApplicantExperiences = (
                        from x in this.db.tvwApplicantExperiences
                        where x.applicantId == applicantId
                        select x).ToList<tvwApplicantExperience>();
                    applicantExperiences = MapApplicantExperience.ToEntityList(tvwApplicantExperiences);
                }
            }
            catch (Exception exception)
            {
                applicantExperiences = new List<ApplicantExperience>();
            }
            return applicantExperiences;
        }

        public List<ApplicantHouseJob> GetApplicantHouseJobList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> applicantHouseJobs = new List<ApplicantHouseJob>();
            try
            {
                List<tvwApplicantHouseJob> list = (
                    from x in this.db.tvwApplicantHouseJobs
                    where x.applicantId == applicantId
                    select x).ToList<tvwApplicantHouseJob>();
                applicantHouseJobs = MapApplicantHouseJob.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantHouseJobs = new List<ApplicantHouseJob>();
            }
            foreach (ApplicantHouseJob applicantHouseJob in applicantHouseJobs)
            {
                int num = applicantHouseJob.houseJodId;
                string str = string.Concat("select top(1) countryId from tblApplicantHouseJob where houseJodId = ", num.ToString());
                SqlConnection sqlConnection = new SqlConnection();
                Message message = new Message();
                SqlCommand sqlCommand = new SqlCommand(str);
                try
                {
                    try
                    {
                        sqlConnection = new SqlConnection(PrpDbConnectADO.Conn);
                        sqlConnection.Open();
                        sqlCommand.Connection = sqlConnection;
                        applicantHouseJob.countryId = sqlCommand.ExecuteScalar().TooInt();
                    }
                    catch (Exception exception1)
                    {
                        applicantHouseJob.countryId = 132;
                    }
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
            return applicantHouseJobs;
        }

        public List<ApplicantHouseJob> GetApplicantHouseJobListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> applicantHouseJobs = new List<ApplicantHouseJob>();
            try
            {
                List<tvwApplicantHouseJob> list = (
                    from x in this.db.tvwApplicantHouseJobs
                    where x.applicantId == applicantId
                    select x).ToList<tvwApplicantHouseJob>();
                applicantHouseJobs = MapApplicantHouseJob.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantHouseJobs = new List<ApplicantHouseJob>();
            }
            return applicantHouseJobs;
        }

        public Message GetApplicantIdBySearch(string search, string condition)
        {
            Message message = new Message();
            try
            {
                spApplicantGetBySearch_Result spApplicantGetBySearchResult = this.db.spApplicantGetBySearch(search, condition).FirstOrDefault<spApplicantGetBySearch_Result>();
                message = MapApplicant.ToEntity(spApplicantGetBySearchResult);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.msg = exception.Message;
            }
            return message;
        }

        public ApplicantInfo GetApplicantInfo(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantInfo();
            try
            {
                tblApplicantInfo _tblApplicantInfo = this.db.tblApplicantInfoes.FirstOrDefault<tblApplicantInfo>((tblApplicantInfo x) => x.applicantId == applicantId);
                applicantInfo = ((_tblApplicantInfo == null ? true : _tblApplicantInfo.applicantDetailId <= 0) ? new ApplicantInfo() : MapApplicantInfo.ToEntity(_tblApplicantInfo));
            }
            catch (Exception exception)
            {
                applicantInfo = new ApplicantInfo();
            }
            return applicantInfo;
        }

        public ApplicantInfo GetApplicantInfoDetail(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantInfo();
            try
            {
                if (inductionId != 0)
                {
                    tvwfApplicantInfo _tvwfApplicantInfo = this.db.tvwfApplicantInfoes.FirstOrDefault<tvwfApplicantInfo>((tvwfApplicantInfo x) => x.applicantId == applicantId && x.inductionId == inductionId);
                    applicantInfo = ((_tvwfApplicantInfo == null ? true : _tvwfApplicantInfo.applicantDetailId <= 0) ? new ApplicantInfo() : MapApplicantInfo.ToEntity(_tvwfApplicantInfo));
                }
                else
                {
                    tvwApplicantInfo _tvwApplicantInfo = this.db.tvwApplicantInfoes.FirstOrDefault<tvwApplicantInfo>((tvwApplicantInfo x) => x.applicantId == applicantId);
                    applicantInfo = ((_tvwApplicantInfo == null ? true : _tvwApplicantInfo.applicantDetailId <= 0) ? new ApplicantInfo() : MapApplicantInfo.ToEntity(_tvwApplicantInfo));
                }
            }
            catch (Exception exception)
            {
                applicantInfo = new ApplicantInfo();
            }
            return applicantInfo;
        }

        public ApplicantInfoDualNational GetApplicantInfoDualNational(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfoDualNational applicantInfoDualNational = new ApplicantInfoDualNational();
            try
            {
                tvwApplicantInfoDualNational _tvwApplicantInfoDualNational = this.db.tvwApplicantInfoDualNationals.FirstOrDefault<tvwApplicantInfoDualNational>((tvwApplicantInfoDualNational x) => x.applicantId == applicantId);
                applicantInfoDualNational = ((_tvwApplicantInfoDualNational == null ? true : _tvwApplicantInfoDualNational.dualInfoId <= 0) ? new ApplicantInfoDualNational() : MapApplicantInfo.ToEntity(_tvwApplicantInfoDualNational));
            }
            catch (Exception exception)
            {
                applicantInfoDualNational = new ApplicantInfoDualNational();
            }
            return applicantInfoDualNational;
        }

        public List<Applicant> GetApplicantList(int accountStatusId = 1)
        {
            List<Applicant> applicants = new List<Applicant>();
            try
            {
            }
            catch (Exception exception)
            {
                applicants = new List<Applicant>();
            }
            return applicants;
        }

        public List<Applicant> GetApplicantList(ApplicantEntity obj)
        {
            List<Applicant> applicants = new List<Applicant>();
            try
            {
                List<spApplicantGetList_Result> list = this.db.spApplicantGetList(new int?(obj.accountStatusId), new int?(obj.applicationStatusId), new int?(obj.facultyId), obj.condition).ToList<spApplicantGetList_Result>();
                applicants = MapApplicant.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicants = new List<Applicant>();
            }
            return applicants;
        }

        public List<ApplicantResearchPaper> GetApplicantResearchPaperList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> applicantResearchPapers = new List<ApplicantResearchPaper>();
            try
            {
                List<spApplicantResearchPaperByApplicant_Result> list = this.db.spApplicantResearchPaperByApplicant(new int?(inductionId), new int?(phaseId), new int?(applicantId)).ToList<spApplicantResearchPaperByApplicant_Result>();
                applicantResearchPapers = MapApplicantResearchPaper.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantResearchPapers = new List<ApplicantResearchPaper>();
            }
            return applicantResearchPapers;
        }

        public List<ApplicantResearchPaper> GetApplicantResearchPaperListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> applicantResearchPapers = new List<ApplicantResearchPaper>();
            try
            {
                if (inductionId != 0)
                {
                    List<tblApplicantResearchPaperFinal> list = (
                        from x in this.db.tblApplicantResearchPaperFinals
                        where x.applicantId == applicantId && x.inductionId == inductionId
                        select x).ToList<tblApplicantResearchPaperFinal>();
                    applicantResearchPapers = MapApplicantResearchPaper.ToEntityList(list);
                }
                else
                {
                    List<spApplicantResearchPaperByApplicant_Result> spApplicantResearchPaperByApplicantResults = this.db.spApplicantResearchPaperByApplicant(new int?(inductionId), new int?(phaseId), new int?(applicantId)).ToList<spApplicantResearchPaperByApplicant_Result>();
                    applicantResearchPapers = MapApplicantResearchPaper.ToEntityList(spApplicantResearchPaperByApplicantResults);
                }
            }
            catch (Exception exception)
            {
                applicantResearchPapers = new List<ApplicantResearchPaper>();
            }
            return applicantResearchPapers;
        }

        public List<ApplicantSpecility> GetApplicantSpecilityList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> applicantSpecilities = new List<ApplicantSpecility>();
            try
            {
                List<spApplicantSpecilityByApplicant_Result> list = this.db.spApplicantSpecilityByApplicant(new int?(inductionId), new int?(phaseId), new int?(applicantId)).ToList<spApplicantSpecilityByApplicant_Result>();
                applicantSpecilities = MapApplicantSpecility.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantSpecilities = new List<ApplicantSpecility>();
            }
            return applicantSpecilities;
        }

        public List<ApplicantSpecility> GetApplicantSpecilityListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> applicantSpecilities = new List<ApplicantSpecility>();
            try
            {
                List<tvwApplicantSpecility> list = (
                    from x in this.db.tvwApplicantSpecilities
                    where x.applicantId == applicantId
                    select x).ToList<tvwApplicantSpecility>();
                applicantSpecilities = MapApplicantSpecility.ToEntityList(list);
            }
            catch (Exception exception)
            {
                applicantSpecilities = new List<ApplicantSpecility>();
            }
            return applicantSpecilities;
        }

        public List<ApplicantSpecility> GetApplicantSpecilityListWithMarks(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> applicantSpecilities = new List<ApplicantSpecility>();
            try
            {
                if (inductionId != 0)
                {
                    List<spApplicantSpecilityWithMarksByApplicantFinal_Result> list = this.db.spApplicantSpecilityWithMarksByApplicantFinal(new int?(applicantId), new int?(inductionId), new int?(phaseId)).ToList<spApplicantSpecilityWithMarksByApplicantFinal_Result>();
                    applicantSpecilities = MapApplicantSpecility.ToEntityList(list);
                }
                else
                {
                    List<spApplicantSpecilityWithMarksByApplicant_Result> spApplicantSpecilityWithMarksByApplicantResults = this.db.spApplicantSpecilityWithMarksByApplicant(new int?(applicantId), new int?(inductionId), new int?(phaseId)).ToList<spApplicantSpecilityWithMarksByApplicant_Result>();
                    applicantSpecilities = MapApplicantSpecility.ToEntityList(spApplicantSpecilityWithMarksByApplicantResults);
                }
            }
            catch (Exception exception)
            {
                applicantSpecilities = new List<ApplicantSpecility>();
            }
            return applicantSpecilities;
        }

        public Message GetApplicantVerifyDistictionCountByApplicant(int inductionId, int phaseId, int applicantId)
        {
            Message message = new Message();
            try
            {
            }
            catch (Exception exception)
            {
                message.id = 0;
                message.status = false;
            }
            return message;
        }

        public ApplicantVoucher GetApplicantVoucher(int inductionId, int phaseId, int applicantId)
        {
            ApplicantVoucher applicantVoucher = new ApplicantVoucher();
            try
            {
                if (inductionId != 0)
                {
                    tblApplicantVoucherFinal _tblApplicantVoucherFinal = this.db.tblApplicantVoucherFinals.FirstOrDefault<tblApplicantVoucherFinal>((tblApplicantVoucherFinal x) => x.inductionId == inductionId && x.applicantId == applicantId);
                    applicantVoucher = ((_tblApplicantVoucherFinal == null ? true : _tblApplicantVoucherFinal.applicantVoucherId <= 0) ? new ApplicantVoucher() : MapVoucher.ToEntity(_tblApplicantVoucherFinal));
                }
                else
                {
                    tblApplicantVoucher _tblApplicantVoucher = this.db.tblApplicantVouchers.FirstOrDefault<tblApplicantVoucher>((tblApplicantVoucher x) => x.applicantId == applicantId);
                    applicantVoucher = ((_tblApplicantVoucher == null ? true : _tblApplicantVoucher.applicantVoucherId <= 0) ? new ApplicantVoucher() : MapVoucher.ToEntity(_tblApplicantVoucher));
                }
            }
            catch (Exception exception)
            {
                applicantVoucher = new ApplicantVoucher();
            }
            return applicantVoucher;
        }

        public ApplicationStatus GetApplicationStatusSingle(int inductionId, int phaseId, int applicantId, int statusTypeId = 0)
        {
            ApplicationStatus resp = new ApplicationStatus();
            try
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationStatusGet]"
                };
                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@phaseId", phaseId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);
                DataTable dataTable = PrpDbADO.FillDataTable(cmd, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow row = dataTable.Rows[0];
                    resp.inductionId = row["inductionId"].TooInt();
                    resp.applicantId = row["applicantId"].TooInt();
                    resp.statusTypeId = row["statusTypeId"].TooInt();
                    resp.statusId = row["statusId"].TooInt();
                    resp.statusType = row["statusType"].TooString("");
                    resp.status = row["status"].TooString("");
                    resp.stepStatusId = row["stepStatusId"].TooInt();
                }
            }
            catch (Exception exception)
            {
                resp = new ApplicationStatus();
            }
            return resp;
        }

        public List<ApplicationStatus> GetApplicationStatus(int inductionId, int phaseId, int applicantId, int statusTypeId = 0)
        {
            List<ApplicationStatus> resp = new List<ApplicationStatus>();
            try
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationStatusGet]"
                };
                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@phaseId", phaseId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);
                DataTable dataTable = PrpDbADO.FillDataTable(cmd, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ApplicationStatus applicationStatu = new ApplicationStatus()
                        {
                            inductionId = row["inductionId"].TooInt(),
                            applicantId = row["applicantId"].TooInt(),
                            statusTypeId = row["statusTypeId"].TooInt(),
                            statusId = row["statusId"].TooInt(),
                            statusType = row["statusType"].TooString(""),
                            status = row["status"].TooString("")
                        };
                        resp.Add(applicationStatu);
                    }
                }
            }
            catch (Exception exception)
            {
                resp = new List<ApplicationStatus>();
            }
            return resp;
        }

        public ApplicationStatus GetApplicationCurrentStatus( int applicantId)
        {
            ApplicationStatus resp = new ApplicationStatus();
            try
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationGetCurrentStatus]"
                };
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                DataTable dataTable = PrpDbADO.FillDataTable(cmd, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow row = dataTable.Rows[0];
                    resp.inductionId = row["inductionId"].TooInt();
                    resp.applicantId = row["applicantId"].TooInt();
                    resp.statusTypeId = row["statusTypeId"].TooInt();
                    resp.statusId = row["statusId"].TooInt();
                    resp.statusType = row["statusType"].TooString("");
                    resp.status = row["status"].TooString("");
                    resp.stepStatusId = row["stepStatusId"].TooInt();
                }
            }
            catch (Exception exception)
            {
                resp = new ApplicationStatus();
            }
            return resp;
        }
        public List<ApplicationStatus> GetApplicantAllStatus( int applicantId)
        {
            List<ApplicationStatus> resp = new List<ApplicationStatus>();
            try
            {
                SqlCommand cmd = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantStatusGetAll]"
                };
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                DataTable dataTable = PrpDbADO.FillDataTable(cmd, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        ApplicationStatus applicationStatu = new ApplicationStatus()
                        {
                            inductionId = row["inductionId"].TooInt(),
                            applicantId = row["applicantId"].TooInt(),
                            statusTypeId = row["statusTypeId"].TooInt(),
                            statusId = row["statusId"].TooInt(),
                            statusType = row["statusType"].TooString(""),
                            status = row["status"].TooString("")
                        };
                        resp.Add(applicationStatu);
                    }
                }
            }
            catch (Exception exception)
            {
                resp = new List<ApplicationStatus>();
            }
            return resp;
        }

        public Applicant Login(string userName, string password)
        {
            Applicant applicant = new Applicant();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantLogin]"
                };
                sqlCommand.Parameters.AddWithValue("@emailId", userName);
                sqlCommand.Parameters.AddWithValue("@password", password);
                DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow item = dataTable.Rows[0];
                    applicant.inductionId = item["inductionId"].TooInt();
                    applicant.applicantId = item["applicantId"].TooInt();
                    applicant.network = item["network"].TooInt();
                    applicant.facultyId = item["facultyId"].TooInt();
                    applicant.statusId = item["statusId"].TooInt();
                    applicant.name = item["name"].TooString("");
                    applicant.pmdcNo = item["pmdcNo"].TooString("");
                    applicant.emailId = item["emailId"].TooString("");
                    applicant.password = item["password"].TooString("");
                    applicant.passwordDecrypt = item["passwordDecrypt"].TooString("");
                    applicant.contactNumber = item["contactNumber"].TooString("");
                    applicant.pic = item["pic"].TooString("");
                    applicant.date = item["date"].TooString("");
                    applicant.facultyName = item["facultyName"].TooString("");
                    applicant.status = item["status"].TooString("");
                }
            }
            catch (Exception exception)
            {
                applicant = new Applicant();
            }
            return applicant;
        }

        public Applicant LoginHs(string userName, string password)
        {
            Applicant applicant = new Applicant();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantLoginHs]"
                };
                sqlCommand.Parameters.AddWithValue("@emailId", userName);
                sqlCommand.Parameters.AddWithValue("@password", password);
                DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow item = dataTable.Rows[0];
                    applicant.inductionId = item["inductionId"].TooInt();
                    applicant.applicantId = item["applicantId"].TooInt();
                    applicant.specialityJobId = item["specialityJobId"].TooInt();
                    applicant.name = item["name"].TooString("");
                    applicant.pic = item["pic"].TooString("");
                    applicant.statusId = item["statusId"].TooInt();
                    applicant.status = item["status"].TooString("");
                    applicant.url = item["url"].TooString("");
                }
            }
            catch (Exception ex)
            {
                applicant = new Applicant();
                applicant.status = ex.Message;
                applicant.statusId = 0;
                applicant.url = "";

            }
            return applicant;
        }

        public Applicant LoginByPhfDeveloper(string userName, int typeId)
        {
            Applicant applicant = new Applicant();
            try
            {
            }
            catch (Exception exception)
            {
                applicant = new Applicant();
            }
            return applicant;
        }


        public Message Registration(Applicant obj)
        {
            Message message = new Message();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantRegister]"
                };
                sqlCommand.Parameters.AddWithValue("@name", obj.name);
                sqlCommand.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo);
                sqlCommand.Parameters.AddWithValue("@emailId", obj.emailId);
                sqlCommand.Parameters.AddWithValue("@password", obj.password);
                sqlCommand.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
                sqlCommand.Parameters.AddWithValue("@network", obj.network);
                sqlCommand.Parameters.AddWithValue("@levelId", obj.levelId);
                sqlCommand.Parameters.AddWithValue("@facultyId", obj.facultyId);
                sqlCommand.Parameters.AddWithValue("@pic", obj.pic);
                sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
                sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
                message = PrpDbADO.FillDataTable(sqlCommand, "").ConvertToEnitityMessage();
            }
            catch (Exception ex)
            {
                message.status = false;
                message.msg = ex.Message;
            }
            return message;
        }

        public Message ReopenApplication(int applicantId, int status)
        {
            Message message = new Message();
            try
            {
                this.db.spApplicationReopen(new int?(applicantId), new int?(status));
                message.status = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                message.status = false;
                message.message = exception.Message;
            }
            return message;
        }


        public DataSet ProfileProcessGetInfoByParam(ProfileSearch obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spProfileProcessGetInfoByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@stepId", obj.stepId);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public DataSet ApplicationStatusGetAll(ApplicationStatus obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicationStatusGetAll]"
            };
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

    }
}