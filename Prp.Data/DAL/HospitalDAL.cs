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
	public class HospitalDAL : PrpDBConnect
	{
		public HospitalDAL()
		{
		}

		public Message AddUpdate(Hospital obj)
		{
			Message message = new Message();
			try
			{
				tblHospital _tblHospital = new tblHospital();
				if (obj.hospitalId != 0)
				{
					_tblHospital = this.db.tblHospitals.FirstOrDefault<tblHospital>((tblHospital x) => x.hospitalId == obj.hospitalId);
					_tblHospital.name = obj.name;
					_tblHospital.code = obj.code;
					_tblHospital.abbr = obj.abbr;
					_tblHospital.levelId = obj.levelId;
					_tblHospital.typeId = obj.typeId;
					_tblHospital.regionId = obj.regionId;
					_tblHospital.nameDisplay = obj.nameDisplay;
					_tblHospital.isActive = obj.isActive;
					_tblHospital.address = obj.address;
					_tblHospital.dated = obj.dated;
					_tblHospital.adminId = obj.adminId;
				}
				else
				{
					_tblHospital = MapHospital.ToTable(obj);
					this.db.tblHospitals.Add(_tblHospital);
				}
				this.db.SaveChanges();
				message.id = _tblHospital.hospitalId;
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public List<Hospital> GetAll()
		{
			List<Hospital> hospitals = new List<Hospital>();
			try
			{
				List<tvwHospital> list = this.db.tvwHospitals.ToList<tvwHospital>();
				hospitals = MapHospital.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return hospitals;
		}

		public List<Hospital> GetAll(int levelId, int typeId)
		{
			List<Hospital> hospitals = new List<Hospital>();
			try
			{
				List<tvwHospital> list = (
					from x in this.db.tvwHospitals
					where x.levelId == levelId && x.typeId == typeId
					select x).ToList<tvwHospital>();
				hospitals = MapHospital.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return hospitals;
		}

		public Hospital GetById(int hospitalId)
		{
			Hospital hospital = new Hospital();
			try
			{
				tblHospital _tblHospital = this.db.tblHospitals.FirstOrDefault<tblHospital>((tblHospital x) => x.hospitalId == hospitalId);
				hospital = MapHospital.ToEntity(_tblHospital);
			}
			catch (Exception exception)
			{
			}
			return hospital;
		}

		public Hospital GetByUserId(int userId)
		{
			Hospital hospital = new Hospital();
			try
			{
				tvwHospitalUser _tvwHospitalUser = this.db.tvwHospitalUsers.FirstOrDefault<tvwHospitalUser>((tvwHospitalUser x) => x.userId == userId);
				hospital = MapHospital.ToEntity(_tvwHospitalUser);
			}
			catch (Exception exception)
			{
			}
			return hospital;
		}

		public List<EntityDDL> GetHospitalDDL(DDLHospitals obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spHospitalForDDL_Result> list = this.db.spHospitalForDDL(new int?(obj.instituteId), new int?(obj.typeId), new int?(obj.userId), new int?(obj.reffId), obj.reffIds, obj.condition).ToList<spHospitalForDDL_Result>();
				entityDDLs = MapHospital.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public List<Hospital> GetHospitalForInstitute(int instituteId)
		{
			List<Hospital> hospitals = new List<Hospital>();
			try
			{
				List<spHospitalForInstitute_Result> list = this.db.spHospitalForInstitute(new int?(instituteId)).ToList<spHospitalForInstitute_Result>();
				hospitals = MapHospital.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return hospitals;
		}

		public Message HospitalDisciplineAddUpdate(HospitalDiscipline obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spHospitalDisciplineAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@isApproved", obj.isApproved);
			sqlCommand.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@remarks", obj.remarks);
			sqlCommand.Parameters.AddWithValue("@certificateImage", obj.certificateImage);
			sqlCommand.Parameters.AddWithValue("@dateStart", obj.dateStart);
			sqlCommand.Parameters.AddWithValue("@dateEnd", obj.dateEnd);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message HospitalDisciplineDelete(HospitalDiscipline obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spHospitalDisciplineDelete]"
			};
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public HospitalDiscipline HospitalDisciplineGetById(int id)
		{
			HospitalDiscipline hospitalDiscipline = new HospitalDiscipline();
			try
			{
				tblHospitalDiscipline _tblHospitalDiscipline = this.db.tblHospitalDisciplines.FirstOrDefault<tblHospitalDiscipline>((tblHospitalDiscipline x) => x.id == id);
				hospitalDiscipline = ((_tblHospitalDiscipline == null ? true : _tblHospitalDiscipline.id <= 0) ? new HospitalDiscipline() : MapHospital.ToEntity(_tblHospitalDiscipline));
			}
			catch (Exception exception)
			{
				hospitalDiscipline = new HospitalDiscipline();
			}
			return hospitalDiscipline;
		}

		public List<HospitalDiscipline> HospitalDisciplineSearch(int hospitalId)
		{
			List<HospitalDiscipline> hospitalDisciplines = new List<HospitalDiscipline>();
			try
			{
				List<spHospitalDisciplineSearch_Result> list = this.db.spHospitalDisciplineSearch(new int?(hospitalId)).ToList<spHospitalDisciplineSearch_Result>();
				hospitalDisciplines = MapHospital.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return hospitalDisciplines;
		}

		public DataTable HospitalSearch(HospitalSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spHospitalSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@levelId", obj.levelId);
			sqlCommand.Parameters.AddWithValue("@regionId", obj.regionId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}