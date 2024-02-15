using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Prp.Data
{
	public class ApplicantAdminDAL : PrpDBConnect
	{
		public ApplicantAdminDAL()
		{
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
						this.db.spApplicantCertificateAddUpdateAdmin(new int?(applicantCertificate.applicantCertificateTypeId), new int?(applicantCertificate.applicantId), new int?(applicantCertificate.typeId), new int?(0), new int?(applicantCertificate.disciplineId), new int?(applicantCertificate.obtainMarks), new int?(applicantCertificate.totalMarks), new DateTime?(applicantCertificate.passingDate), applicantCertificate.imageCertificate, new int?(applicantCertificate.adminId));
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

		public Message ApplicantDegreeAddUpdate(ApplicantDegree obj)
		{
			Message message = new Message();
			try
			{
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantDegreeAddUpdateAdmin]"
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
                message.status = true;

            }
			catch (Exception ex)
			{
				message.status = false;
				message.msg = ex.Message;
			}
			return message;
		}

		public Message ApplicantDegreeMarksAddUpdate(List<ApplicantDegreeMark> listMarks, int inductionId, int phaseId, int adminId)
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
                            CommandText = "[dbo].[spApplicantDegreeMarksAddUpdateAdmin]"
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
                        sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
						message = PrpDbADO.FillDataTableMessage(sqlCommand);

       //                 this.db.spApplicantDegreeMarksAddUpdateAdmin(new int?(listMark.degreeMarksId), new int?(inductionId), new int?(phaseId)
       //, new int?(listMark.applicantId), new int?(listMark.graduateTypeId), new int?(listMark.year)
       //, new int?(listMark.totalMarks), new int?(listMark.obtainMarks), new int?(listMark.attempt)
       //, listMark.imageDMC, new int?(adminId));
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
			catch (Exception ex)
			{
				message.status = false;
				message.msg = ex.Message;
			}
			return message;
		}

		public Message ApplicantDistinctionAddUpdate(ApplicantDistinction obj)
		{
			Message message = new Message();
			try
			{
				//this.db.spApplicantDistinctionAddUpdateAdmin(new int?(obj.applicantDistinctionId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), obj.subject, new int?(obj.year), obj.imageDistinction, new int?(obj.position), obj.university, new int?(obj.adminId)).FirstOrDefault<spApplicantDistinctionAddUpdateAdmin_Result>();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			return message;
		}

		public Message ApplicantExperienceAddUpdate(ApplicantExperience obj)
		{
			Message message = new Message();
			try
			{
				spApplicantExperienceAddUpdateAdmin_Result spApplicantExperienceAddUpdateAdminResult = this.db.spApplicantExperienceAddUpdateAdmin(new int?(obj.applicantExperienceId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), new int?(obj.levelId), new int?(obj.levelTypeId), obj.instituteName, new int?(obj.instituteId), new int?(obj.provinceId), new int?(obj.typeId), new DateTime?(obj.startDated), new DateTime?(obj.endDate), new bool?(obj.isCurrent), obj.imageExperience, new int?(obj.adminId)).FirstOrDefault<spApplicantExperienceAddUpdateAdmin_Result>();
				message = MapApplicantExperience.ToEntity(spApplicantExperienceAddUpdateAdminResult);
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
				CommandText = "[dbo].[spApplicantHouseJobAddUpdateAdmin]"
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
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message ApplicantInfoAddUpdate(ApplicantInfo obj)
		{
			Message message = new Message();
			try
			{
				this.db.spApplicantAddUpdateInfoAdmin(new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), obj.fatherName, new int?(obj.genderId), new int?(obj.disableId), new DateTime?(obj.dob), new DateTime?(obj.pmdcExpiryDate), new DateTime?(obj.mbbsPassingDate), new int?(obj.countryIdDegreePassing), new int?(obj.countryId), new int?(obj.districtId), obj.districtName, new int?(obj.domicileProvinceId), new int?(obj.domicileDistrictId), obj.address, obj.cnicNo, new DateTime?(obj.cnicExpiryDate), obj.cnicPicFront, obj.cnicPicBack, obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage, new int?(obj.adminId));
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

		public Message ApplicantResearchPaperAddUpdate(ApplicantResearchPaper obj)
		{
			Message message = new Message();
			try
			{
				this.db.spApplicantResearchPaperAddUpdateAdmin(new int?(obj.applicantResearchId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), obj.name, new int?(obj.authorId), new int?(obj.publishStatusId), obj.link, obj.webLink, obj.imageLetter, new int?(obj.researchJournalId), new bool?(obj.isActive), new int?(obj.adminId));
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

		public DataTable ApplicantSearch(ApplicantSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ApplicantSearchByAdmin(ApplicantSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantSearchByAdmin]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@userId", obj.userId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataSet ApplicantSearchSimple(ApplicantSearch obj)
		{
			SqlCommand cmd = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantSearchSimple]"
			};
			cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
			cmd.Parameters.AddWithValue("@top", obj.top);
			cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
			cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
			cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
			cmd.Parameters.AddWithValue("@statusId", obj.statusId);
			cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            cmd.Parameters.AddWithValue("@name", obj.name.TooString());
            cmd.Parameters.AddWithValue("@pmdcNo", obj.pmdcNo.TooString());
            cmd.Parameters.AddWithValue("@contactNumber", obj.contactNumber.TooString());
            cmd.Parameters.AddWithValue("@emailId", obj.emailId.TooString());
            return PrpDbADO.FillDataSet(cmd);
		}

		public Message ApplicantSpecilityAddUpdate(ApplicantSpecility obj)
		{
			Message message = new Message();
			try
			{
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

		public DataTable ApplicantStatusHistory(int applicantId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spHistoryApplicantViewByInduction]"
			};
			sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

        public DataTable ApplicantStatusByParam(int applicantId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantStatusByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(sqlCommand, "");
        }

        public Message ApplicantVoucherAddUpdate(ApplicantVoucher obj)
		{
			Message message = new Message();
			try
			{
				this.db.spApplicantVoucherAddUpdateAdmin(new int?(obj.applicantId), new int?(obj.amount), obj.branchCode, obj.voucherImage, obj.ibn, obj.accountNo, obj.accountTitle, new DateTime?(obj.submittedDate), new int?(obj.adminId));
				message.status = true;
				message.message = "Transaction saved successfully";
			}
			catch (Exception exception)
			{
				message.status = false;
				message.msg = "System error.";
			}
			return message;
		}
	}
}