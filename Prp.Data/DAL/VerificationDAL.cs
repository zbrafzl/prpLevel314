using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class VerificationDAL : PrpDBConnect
	{
		public VerificationDAL()
		{
		}

		public Message AddUpdateVerficationStatus(VerificationEntity obj)
		{
			Message message = new Message();
			try
			{
				SqlCommand sqlCommand = new SqlCommand()
				{
					CommandType = CommandType.StoredProcedure,
					CommandText = "[dbo].[spApplicantApprovalStatusAddUpdate]"
				};
				sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
				sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
				sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
				sqlCommand.Parameters.AddWithValue("@approvalStatusTypeId", obj.approvalStatusTypeId);
				sqlCommand.Parameters.AddWithValue("@approvalStatusId", obj.approvalStatusId);
				sqlCommand.Parameters.AddWithValue("@comments", obj.comments);
				sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
				message = PrpDbADO.FillDataTable(sqlCommand, "").ConvertToEnitityMessage();
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			return message;
		}

		public DataTable ApplicantListVerifyExport(VerificationEntity obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantListVerifyExport]"
			};
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ApplicantListVerifyView(VerificationEntity obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantListVerify]"
			};
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message getApplicantDebar(int applicantId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spCheckDebarApplicant]"
			};
			sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message GetApplicantIdBySearchVerification(string search, string condition)
		{
			Message message = new Message();
			try
			{
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
			}
			return message;
		}

		public ApplicationVerificationStatus GetApplicationAmendmentsStatus(int applicantId)
		{
			ApplicationVerificationStatus applicationVerificationStatu = new ApplicationVerificationStatus();
			try
			{
				tvwApplicationAmendment _tvwApplicationAmendment = this.db.tvwApplicationAmendments.FirstOrDefault<tvwApplicationAmendment>((tvwApplicationAmendment x) => x.applicantId == applicantId);
				applicationVerificationStatu = MapVerification.ToEntity(_tvwApplicationAmendment);
			}
			catch (Exception exception)
			{
				applicationVerificationStatu = new ApplicationVerificationStatus();
			}
			return applicationVerificationStatu;
		}

		public List<ApplicantApprovalStatus> GetApplicationApprovalStatusGetById(int inductionId, int phaseId, int applicantId)
		{
			List<ApplicantApprovalStatus> applicantApprovalStatuses = new List<ApplicantApprovalStatus>();
			try
			{
				List<spApplicationApprovalStatusGetById_Result> list = this.db.spApplicationApprovalStatusGetById(new int?(inductionId), new int?(phaseId), new int?(applicantId)).ToList<spApplicationApprovalStatusGetById_Result>();
				applicantApprovalStatuses = MapVerification.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantApprovalStatuses = new List<ApplicantApprovalStatus>();
			}
			return applicantApprovalStatuses;
		}

		public ApplicantApprovalStatus GetApplicationApprovalStatusGetByTypeAndId(int inductionId, int phaseId, int statusTypeId, int applicantId)
		{
			ApplicantApprovalStatus applicantApprovalStatu = new ApplicantApprovalStatus();
			try
			{
				spApplicationApprovalStatusGetByTypeAndId_Result spApplicationApprovalStatusGetByTypeAndIdResult = this.db.spApplicationApprovalStatusGetByTypeAndId(new int?(inductionId), new int?(phaseId), new int?(statusTypeId), new int?(applicantId)).FirstOrDefault<spApplicationApprovalStatusGetByTypeAndId_Result>();
				if ((spApplicationApprovalStatusGetByTypeAndIdResult == null ? false : spApplicationApprovalStatusGetByTypeAndIdResult.applicationApprovalStatusId > 0))
				{
					applicantApprovalStatu = MapVerification.ToEntity(spApplicationApprovalStatusGetByTypeAndIdResult);
				}
			}
			catch (Exception exception)
			{
				applicantApprovalStatu = new ApplicantApprovalStatus();
			}
			return applicantApprovalStatu;
		}

		public DataTable GetApplicationHasAmedmentAndNotSentEmail()
		{
			return PrpDbADO.FillDataTable(new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGetApplicationHasAmedmentAndNotSentEmail]"
			}, "");
		}
	}
}