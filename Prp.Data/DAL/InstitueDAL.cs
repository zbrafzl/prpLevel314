using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class InstitueDAL : PrpDBConnect
	{
		public InstitueDAL()
		{
		}

		public Message AddUpdate(Institute obj)
		{
			Message message = new Message();
			try
			{
				tblInstitute _tblInstitute = new tblInstitute();
				if (obj.instituteId != 0)
				{
					_tblInstitute = this.db.tblInstitutes.FirstOrDefault<tblInstitute>((tblInstitute x) => x.instituteId == obj.instituteId);
					_tblInstitute.name = obj.name;
					_tblInstitute.code = obj.code;
					_tblInstitute.instituteTypeId = obj.instituteTypeId;
					_tblInstitute.provinceId = obj.provinceId;
					_tblInstitute.districtId = obj.districtId;
					_tblInstitute.isActive = obj.isActive;
					_tblInstitute.sortOrder = obj.sortOrder;
					_tblInstitute.dated = obj.dated;
					_tblInstitute.adminId = obj.adminId;
				}
				else
				{
					_tblInstitute = MapInstitue.ToTable(obj);
					this.db.tblInstitutes.Add(_tblInstitute);
				}
				this.db.SaveChanges();
				message.id = _tblInstitute.instituteId;
				try
				{
					this.db.spHospitalInstituteAddUpdate(new int?(_tblInstitute.instituteId), new int?(obj.adminId), obj.hospitalIds);
				}
				catch (Exception exception)
				{
				}
			}
			catch (Exception exception1)
			{
				message.msg = exception1.Message;
				message.id = 0;
			}
			return message;
		}

		public List<Institute> GetAll()
		{
			List<Institute> institutes = new List<Institute>();
			try
			{
				List<tvwInstitute> list = this.db.tvwInstitutes.ToList<tvwInstitute>();
				institutes = MapInstitue.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return institutes;
		}

		public List<Institute> GetAll(int instituteTypeId)
		{
			List<Institute> institutes = new List<Institute>();
			try
			{
				List<tvwInstitute> list = (
					from x in this.db.tvwInstitutes
					where x.instituteTypeId == instituteTypeId
					select x).ToList<tvwInstitute>();
				institutes = MapInstitue.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return institutes;
		}

		public List<Institute> GetAll(int instituteTypeId, int provinceId)
		{
			List<Institute> institutes = new List<Institute>();
			try
			{
				List<tvwInstitute> list = (
					from x in this.db.tvwInstitutes
					where x.instituteTypeId == instituteTypeId && x.provinceId == provinceId
					select x).ToList<tvwInstitute>();
				institutes = MapInstitue.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return institutes;
		}

		public Institute GetById(int instituteId)
		{
			Institute institute = new Institute();
			try
			{
				tblInstitute _tblInstitute = this.db.tblInstitutes.FirstOrDefault<tblInstitute>((tblInstitute x) => x.instituteId == instituteId);
				institute = MapInstitue.ToEntity(_tblInstitute);
			}
			catch (Exception exception)
			{
			}
			return institute;
		}

		public Institute GetByUserId(int userId)
		{
			Institute institute = new Institute();
			try
			{
				tblInstituteUser _tblInstituteUser = this.db.tblInstituteUsers.FirstOrDefault<tblInstituteUser>((tblInstituteUser x) => x.userId == userId);
				tblInstitute _tblInstitute = this.db.tblInstitutes.FirstOrDefault<tblInstitute>((tblInstitute x) => x.instituteId == _tblInstituteUser.instituteId);
				institute = MapInstitue.ToEntity(_tblInstitute);
			}
			catch (Exception exception)
			{
				institute = new Institute();
			}
			return institute;
		}

		public List<EntityDDL> GetInstituteDDL(DDLInstitutes obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spInstituteForDDL_Result> list = this.db.spInstituteForDDL(new int?(obj.hospitalId), new int?(obj.regionId), new int?(obj.typeId), new int?(obj.userId), obj.reffIds, obj.condition).ToList<spInstituteForDDL_Result>();
				entityDDLs = MapInstitue.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}
	}
}