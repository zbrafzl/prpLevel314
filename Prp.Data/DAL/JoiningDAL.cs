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
	public class JoiningDAL : PrpDBConnect
	{
		public JoiningDAL()
		{
		}

		public Message AddUpdate(ApplicantJoined obj)
		{

            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoiningAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@specialityJobId", obj.specialityJobId);
            cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);

		}

		public List<ApplicantJoined> GetAllByHospitalUser(int userId, int hospitalId = 0)
		{
			List<ApplicantJoined> applicantJoineds = new List<ApplicantJoined>();
			try
			{
				List<spJoiningGetByHospital_Result> list = this.db.spJoiningGetByHospital(new int?(userId), new int?(hospitalId)).ToList<spJoiningGetByHospital_Result>();
				applicantJoineds = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoineds = new List<ApplicantJoined>();
			}
			return applicantJoineds;
		}

		public List<ApplicantJoined> GetAllByInstiteUser(int inductionId, int userId, int instituteId, string search)
		{
			List<ApplicantJoined> applicantJoineds = new List<ApplicantJoined>();
			try
			{
				List<spJoiningGetByInstitute_Result> list = this.db.spJoiningGetByInstitute(new int?(inductionId), new int?(userId), new int?(instituteId), search).ToList<spJoiningGetByInstitute_Result>();
				applicantJoineds = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoineds = new List<ApplicantJoined>();
			}
			return applicantJoineds;
		}

		public ApplicantJoined GetApplicantByInstiteUser(int inductionId, int userId, int applicantId)
		{
			ApplicantJoined applicantJoined = new ApplicantJoined();
			try
			{
				List<spJoiningGetByInstitute_Result> list = this.db.spJoiningGetByInstitute(new int?(inductionId), new int?(userId), new int?(0), "").ToList<spJoiningGetByInstitute_Result>();
				List<ApplicantJoined> applicantJoineds = new List<ApplicantJoined>();
				applicantJoineds = MapApplicantJoining.ToEntityList(list);
				if ((applicantJoineds == null ? false : applicantJoineds.Count > 0))
				{
					applicantJoined = applicantJoineds.FirstOrDefault<ApplicantJoined>((ApplicantJoined x) => x.applicantId == applicantId);
				}
			}
			catch (Exception exception)
			{
				applicantJoined = new ApplicantJoined();
			}
			return applicantJoined;
		}

		public ApplicantJoined GetByApplicant(int userId, int applicantId)
		{
			ApplicantJoined applicantJoined = new ApplicantJoined();
			try
			{
				List<spJoiningGetByHospital_Result> list = this.db.spJoiningGetByHospital(new int?(userId), new int?(0)).ToList<spJoiningGetByHospital_Result>();
				List<ApplicantJoined> applicantJoineds = new List<ApplicantJoined>();
				applicantJoineds = MapApplicantJoining.ToEntityList(list);
				if ((applicantJoineds == null ? false : applicantJoineds.Count > 0))
				{
					applicantJoined = applicantJoineds.FirstOrDefault<ApplicantJoined>((ApplicantJoined x) => x.applicantId == applicantId);
				}
			}
			catch (Exception exception)
			{
				applicantJoined = new ApplicantJoined();
			}
			return applicantJoined;
		}

		public ApplicantJoined GetByApplicantById(int applicantId, int inductionId = 0)
		{
			ApplicantJoined applicantJoined = new ApplicantJoined();
			try
			{
				tblApplicantJoined _tblApplicantJoined = new tblApplicantJoined();
				_tblApplicantJoined = (inductionId != 0 ? this.db.tblApplicantJoineds.FirstOrDefault<tblApplicantJoined>((tblApplicantJoined x) => x.applicantId == applicantId && x.inductionId == inductionId) : this.db.tblApplicantJoineds.FirstOrDefault<tblApplicantJoined>((tblApplicantJoined x) => x.applicantId == applicantId));
				applicantJoined = ((_tblApplicantJoined == null ? true : _tblApplicantJoined.applicantId <= 0) ? new ApplicantJoined() : MapApplicantJoining.ToEntity(_tblApplicantJoined));
			}
			catch (Exception exception)
			{
				applicantJoined = new ApplicantJoined();
			}
			return applicantJoined;
		}

		public ApplicantSelected GetByApplicantDetailById(int applicantId)
		{
			ApplicantSelected applicantSelected = new ApplicantSelected();
			try
			{
				tvwApplicantSelected _tvwApplicantSelected = this.db.tvwApplicantSelecteds.FirstOrDefault<tvwApplicantSelected>((tvwApplicantSelected x) => x.applicantId == applicantId);
				applicantSelected = MapApplicantJoining.ToEntity(_tvwApplicantSelected);
			}
			catch (Exception exception)
			{
				applicantSelected = new ApplicantSelected();
			}
			return applicantSelected;
		}

		public List<ApplicantJoiningDsb> GetCountHospitalWise(int instituteId)
		{
			List<ApplicantJoiningDsb> applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			try
			{
				List<spJoiningCountByInstituteHospitalWise_Result> list = this.db.spJoiningCountByInstituteHospitalWise(new int?(instituteId)).ToList<spJoiningCountByInstituteHospitalWise_Result>();
				applicantJoiningDsbs = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			}
			return applicantJoiningDsbs;
		}

		public List<ApplicantJoiningDsb> GetCountInstituteHospitalWise(int instituteId = 0)
		{
			List<ApplicantJoiningDsb> applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			try
			{
				List<spJoiningCountInstituteHospitalWise_Result> list = this.db.spJoiningCountInstituteHospitalWise(new int?(instituteId)).ToList<spJoiningCountInstituteHospitalWise_Result>();
				applicantJoiningDsbs = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			}
			return applicantJoiningDsbs;
		}

		public List<ApplicantJoiningDsb> GetCountInstituteWise()
		{
			List<ApplicantJoiningDsb> applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			try
			{
				List<spJoiningCountInstituteWiseNew_Result> list = this.db.spJoiningCountInstituteWiseNew().ToList<spJoiningCountInstituteWiseNew_Result>();
				applicantJoiningDsbs = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoiningDsbs = new List<ApplicantJoiningDsb>();
			}
			return applicantJoiningDsbs;
		}

		public ApplicantSelected GetFinalApplicantById(int inductionId, int applicantId)
		{
			ApplicantSelected applicantSelected = new ApplicantSelected();
			try
			{
				tvwApplicantSelected _tvwApplicantSelected = this.db.tvwApplicantSelecteds.FirstOrDefault<tvwApplicantSelected>((tvwApplicantSelected x) => x.inductionId == inductionId && x.applicantId == applicantId);
				applicantSelected = ((_tvwApplicantSelected == null ? true : _tvwApplicantSelected.applicantId <= 0) ? new ApplicantSelected() : MapApplicantFinal.ToEntity(_tvwApplicantSelected));
			}
			catch (Exception exception)
			{
				applicantSelected = new ApplicantSelected();
			}
			return applicantSelected;
		}

		public List<SpecialityJob> GetHardshipSeatsStatusByApplicant(int applicantId)
		{
			List<SpecialityJob> specialityJobs = new List<SpecialityJob>();
			try
			{
				List<spHardshipSeatsStatusByApplicant_Result> list = this.db.spHardshipSeatsStatusByApplicant(new int?(applicantId)).ToList<spHardshipSeatsStatusByApplicant_Result>();
				specialityJobs = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				specialityJobs = new List<SpecialityJob>();
			}
			return specialityJobs;
		}

		public ApplicantJoined GetJoinedApplicantDetailById(int applicantId)
		{
			ApplicantJoined applicantJoined = new ApplicantJoined();
			try
			{
				tvwApplicantJoining _tvwApplicantJoining = this.db.tvwApplicantJoinings.FirstOrDefault<tvwApplicantJoining>((tvwApplicantJoining x) => x.applicantId == applicantId);
				applicantJoined = ((_tvwApplicantJoining == null ? true : _tvwApplicantJoining.applicantId <= 0) ? new ApplicantJoined() : MapApplicantJoining.ToEntity(_tvwApplicantJoining));
			}
			catch (Exception exception)
			{
				applicantJoined = new ApplicantJoined();
			}
			return applicantJoined;
		}

        public tblApplicantJoinedPreviou GetJoinedApplicantDetailByIdPrevious(int applicantId)
        {
            tblApplicantJoinedPreviou applicantJoined = new tblApplicantJoinedPreviou();
            try
            {
                 applicantJoined = this.db.tblApplicantJoinedPrevious.FirstOrDefault(x => x.applicantId == applicantId);
            }
            catch (Exception exception)
            {
                applicantJoined = new tblApplicantJoinedPreviou();
            }
            return applicantJoined;
        }

        public List<ApplicantJoined> GetJoiningAll(int top, int pageNo, int joinStatus, string search)
		{
			List<ApplicantJoined> applicantJoineds = new List<ApplicantJoined>();
			try
			{
				List<spJoiningGetAll_Result> list = this.db.spJoiningGetAll(new int?(top), new int?(pageNo), new int?(joinStatus), search).ToList<spJoiningGetAll_Result>();
				applicantJoineds = MapApplicantJoining.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantJoineds = new List<ApplicantJoined>();
			}
			return applicantJoineds;
		}

		public DataTable JoiningSearch(JoiningApplicantSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spJoiningSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable JoiningSearchGetByInstitute(JoiningApplicantSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spJoiningSearchGetByInstitute]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@userId", obj.userId);
			sqlCommand.Parameters.AddWithValue("@instituteId", obj.instituteId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}