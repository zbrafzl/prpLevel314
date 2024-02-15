using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class SpecialityDAL : PrpDBConnect
	{
		public SpecialityDAL()
		{
		}

		public Message AddUpdate(Speciality obj)
		{
			Message message = new Message();
			try
			{
				tblSpeciality _tblSpeciality = new tblSpeciality();
				if (obj.specialityId != 0)
				{
					_tblSpeciality = this.db.tblSpecialities.FirstOrDefault<tblSpeciality>((tblSpeciality x) => x.specialityId == obj.specialityId);
					_tblSpeciality.name = obj.name;
					_tblSpeciality.isActive = obj.isActive;
					_tblSpeciality.sortOrder = obj.sortOrder;
					_tblSpeciality.dated = obj.dated;
					_tblSpeciality.adminId = obj.adminId;
				}
				else
				{
					_tblSpeciality = MapSpeciality.ToTable(obj);
					this.db.tblSpecialities.Add(_tblSpeciality);
				}
				this.db.SaveChanges();
				message.id = _tblSpeciality.specialityId;
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public Message AddUpdate(SpecialityJob obj)
		{
			Message message = new Message();
			try
			{
				tblSpecialityJob _tblSpecialityJob = new tblSpecialityJob();
				if (obj.specialityJobId != 0)
				{
					_tblSpecialityJob = this.db.tblSpecialityJobs.FirstOrDefault<tblSpecialityJob>((tblSpecialityJob x) => x.specialityJobId == obj.specialityJobId);
					_tblSpecialityJob.inductionId = _tblSpecialityJob.inductionId;
					_tblSpecialityJob.specialityId = obj.specialityId;
					_tblSpecialityJob.instituteId = obj.instituteId;
					_tblSpecialityJob.hospitalId = obj.hospitalId;
					_tblSpecialityJob.typeId = obj.typeId;
					_tblSpecialityJob.quotaId = obj.quotaId;
					_tblSpecialityJob.jobs = obj.jobs;
					_tblSpecialityJob.isActive = obj.isActive;
					_tblSpecialityJob.dated = obj.dated;
					_tblSpecialityJob.adminId = obj.adminId;
				}
				else
				{
					_tblSpecialityJob = MapSpecialityJob.ToTable(obj);
					this.db.tblSpecialityJobs.Add(_tblSpecialityJob);
				}
				this.db.SaveChanges();
				try
				{
					this.db.spSynchSpecialityJob(new int?(_tblSpecialityJob.specialityJobId));
				}
				catch (Exception exception)
				{
				}
				message.id = _tblSpecialityJob.specialityJobId;
			}
			catch (Exception exception1)
			{
				message.msg = exception1.Message;
				message.id = 0;
			}
			return message;
		}

		public List<Speciality> GetAll()
		{
			List<Speciality> specialities = new List<Speciality>();
			try
			{
				List<tblSpeciality> list = (
					from x in this.db.tblSpecialities
					orderby x.name
					select x).ToList<tblSpeciality>();
				specialities = MapSpeciality.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return specialities;
		}

		public Speciality GetById(int subjectId)
		{
			Speciality speciality = new Speciality();
			try
			{
				tblSpeciality _tblSpeciality = this.db.tblSpecialities.FirstOrDefault<tblSpeciality>((tblSpeciality x) => x.specialityId == subjectId);
				speciality = MapSpeciality.ToEntity(_tblSpeciality);
			}
			catch (Exception exception)
			{
			}
			return speciality;
		}

		public List<EntityDDL> GetSpecialityDDL(DDLSpecialitys obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spSpecialityForDDL_Result> list = this.db.spSpecialityForDDL(new int?(obj.typeId), new int?(obj.userId), new int?(obj.reffId), obj.reffIds, obj.condition).ToList<spSpecialityForDDL_Result>();
				entityDDLs = MapSpeciality.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public SpecialityJob GetSpecialityJobById(int specialityJobId)
		{
			SpecialityJob specialityJob = new SpecialityJob();
			try
			{
				tblSpecialityJob _tblSpecialityJob = this.db.tblSpecialityJobs.FirstOrDefault<tblSpecialityJob>((tblSpecialityJob x) => x.specialityJobId == specialityJobId);
				specialityJob = MapSpeciality.ToEntity(_tblSpecialityJob);
			}
			catch (Exception exception)
			{
				specialityJob = new SpecialityJob();
			}
			return specialityJob;
		}

		public List<SpecialityJob> GetSpecialityJobByParameters(int inductionId, int specialityId)
		{
			List<SpecialityJob> specialityJobs = new List<SpecialityJob>();
			try
			{
				List<spSpecialityJobByParameters_Result> list = this.db.spSpecialityJobByParameters(new int?(inductionId), new int?(specialityId)).ToList<spSpecialityJobByParameters_Result>();
				specialityJobs = MapSpeciality.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return specialityJobs;
		}

		public List<SpecialityJob> GetSpecialityJobByTypeId(int inductionId, int typeId)
		{
			List<SpecialityJob> specialityJobs = new List<SpecialityJob>();
			try
			{
				List<spSpecialityJobByTypeId_Result> list = this.db.spSpecialityJobByTypeId(new int?(inductionId), new int?(typeId)).ToList<spSpecialityJobByTypeId_Result>();
				specialityJobs = MapSpeciality.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return specialityJobs;
		}

		public List<Speciality> GetWithJobByInduction(int inductionId)
		{
			List<Speciality> specialities = new List<Speciality>();
			try
			{
				List<spSpecialityJobByInduction_Result> list = this.db.spSpecialityJobByInduction(new int?(inductionId)).ToList<spSpecialityJobByInduction_Result>();
				specialities = MapSpeciality.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return specialities;
		}

		public DataTable SpecialityJobPrefferenceSearch(SpecialityJobSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spSpecialityJobPrefferenceSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@quotaId", obj.quotaId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}


        public DataSet SpecialityJobGetDataByParam(SpecialityJobSearch obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSpecialityJobGetDataByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@reffId", obj.reffId);
            sqlCommand.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

       

        public DataSet SpecialityJobAddUpdateParam(SpecialityJob obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.spSpecialityJobAddUpdateParam"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@baseId", obj.baseId);
            cmd.Parameters.AddWithValue("@reffId", obj.reffId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            SqlParameter param = new SqlParameter("@tbList", SqlDbType.Structured)
            {
                TypeName = SqlDataTypes.tbType,
                Value = obj.dataTable
            };
            cmd.Parameters.Add(param);
            return PrpDbADO.FillDataSet(cmd);
        }

        public DataSet HospitalSpecialityGetByParam(HospitalDiscipline obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHospitalSpecialityGetByParam]"
            };
            sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);

            sqlCommand.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataSet(sqlCommand);
        }
    }
}