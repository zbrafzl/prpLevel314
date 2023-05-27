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
	public class CommonDAL : PrpDBConnect
	{
		public CommonDAL()
		{
		}

		public ApplicantInfoAPI ApplicantGetInfo(string applicantNo, int transactionType)
		{
			ApplicantInfoAPI applicantInfoAPI = new ApplicantInfoAPI();
			try
			{
				SqlCommand sqlCommand = new SqlCommand()
				{
					CommandType = CommandType.StoredProcedure,
					CommandText = "[dbo].[spApplicantGetByApplicantNo]"
				};
				sqlCommand.Parameters.AddWithValue("@applicantNo", applicantNo);
				sqlCommand.Parameters.AddWithValue("@transactionType", transactionType);
				DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
				if ((dataTable == null ? false : dataTable.Rows.Count > 0))
				{
					DataRow item = dataTable.Rows[0];
					applicantInfoAPI.applicantId = item["applicantId"].TooInt();
					applicantInfoAPI.applicantNo = item["applicantNo"].TooString("");
					applicantInfoAPI.name = item["name"].TooString("");
					applicantInfoAPI.pmdcNo = item["pmdcNo"].TooString("");
					applicantInfoAPI.cnicNo = item["cnicNo"].TooString("");
					applicantInfoAPI.amount = item["amount"].TooInt();
					applicantInfoAPI.message = item["message"].TooString("");
					applicantInfoAPI.status = item["status"].TooBoolean(false);
					applicantInfoAPI.dueDate = Convert.ToDateTime(item["dueDate"]);
				}
			}
			catch (Exception exception)
			{
				applicantInfoAPI = new ApplicantInfoAPI();
			}
			return applicantInfoAPI;
		}

		public Message BedAddUpdate(Bed obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spBedsAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@bedId", obj.bedId);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@departmentId", obj.departmentId);
			sqlCommand.Parameters.AddWithValue("@unitId", obj.unitId);
			sqlCommand.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@bedsER", obj.bedsER);
			sqlCommand.Parameters.AddWithValue("@bedsICU", obj.bedsICU);
			sqlCommand.Parameters.AddWithValue("@bedsWards", obj.bedsWards);
			sqlCommand.Parameters.AddWithValue("@bedsOther", obj.bedsOther);
			sqlCommand.Parameters.AddWithValue("@remarksN", obj.remarksN);
			sqlCommand.Parameters.AddWithValue("@imageN", obj.imageN);
			sqlCommand.Parameters.AddWithValue("@bedsDep", obj.bedsDep);
			sqlCommand.Parameters.AddWithValue("@remarksDep", obj.remarksDep);
			sqlCommand.Parameters.AddWithValue("@imageDep", obj.imageDep);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message BedDelete(Bed obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spBedsDelete]"
			};
			sqlCommand.Parameters.AddWithValue("@bedId", obj.bedId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public DataTable BedsSearch(Bed obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[BedsSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@departmentId", obj.departmentId);
			sqlCommand.Parameters.AddWithValue("@unitId", obj.unitId);
			sqlCommand.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message DepartmentAddUpdate(Department obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spDepartmentAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@departmentId", obj.departmentId);
			sqlCommand.Parameters.AddWithValue("@name", obj.name);
			sqlCommand.Parameters.AddWithValue("@code", obj.code);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<Department> DepartmentGetAll(int typeId)
		{
			List<Department> departments = new List<Department>();
			try
			{
				List<tvwDepartment> list = (
					from x in this.db.tvwDepartments
					where x.typeId == typeId
					select x).ToList<tvwDepartment>();
				departments = MapDepartment.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return departments;
		}

		public Department DepartmentGetById(int departmentId)
		{
			Department department = new Department();
			try
			{
				tblDepartment _tblDepartment = this.db.tblDepartments.FirstOrDefault<tblDepartment>((tblDepartment x) => x.departmentId == departmentId);
				department = MapDepartment.ToEntity(_tblDepartment);
			}
			catch (Exception exception)
			{
			}
			return department;
		}

		public Message DepartmentHospitalAddUpdate(int hospitalId, string ids, int adminId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spDepartmentHospitalAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", hospitalId);
			sqlCommand.Parameters.AddWithValue("@ids", ids);
			sqlCommand.Parameters.AddWithValue("@adminId", adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<Discipline> DisciplineGetAll()
		{
			List<Discipline> disciplines = new List<Discipline>();
			try
			{
				List<tblDiscipline> list = this.db.tblDisciplines.ToList<tblDiscipline>();
				disciplines = MapDiscipline.ToEntityList(list);
			}
			catch (Exception exception)
			{
				disciplines = new List<Discipline>();
			}
			return disciplines;
		}

		public List<EntityCount> GetCountApplicantApprovalStatus(CountApplicantStatusEntity obj)
		{
			List<EntityCount> entityCounts = new List<EntityCount>();
			try
			{
			}
			catch (Exception exception)
			{
			}
			return entityCounts;
		}

		public List<EntityCount> GetCountJobs(CountJobsEntity obj)
		{
			List<EntityCount> entityCounts = new List<EntityCount>();
			try
			{
				List<spJobGetCount_Result> list = this.db.spJobGetCount(new int?(obj.typeId), new int?(obj.hospitalId), new int?(obj.specialityId), obj.condition).ToList<spJobGetCount_Result>();
				entityCounts = MapCommon.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityCounts;
		}

		public List<EntityCount> GetDashboardCount(int inductionId, int phaseId)
		{
			List<EntityCount> entityCounts = new List<EntityCount>();
			try
			{
				List<spApplicantStatusGetAll_Result> list = this.db.spApplicantStatusGetAll(new int?(inductionId), new int?(phaseId)).ToList<spApplicantStatusGetAll_Result>();
				entityCounts = MapCommon.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityCounts;
		}

		public List<EntityCount> GetDashboardCountInstituteHospital(int instituteId, int hospitalId, string condition)
		{
			List<EntityCount> entityCounts = new List<EntityCount>();
			try
			{
				List<spInstituteHospitalGetDashBoardData_Result> list = this.db.spInstituteHospitalGetDashBoardData(new int?(instituteId), new int?(hospitalId), condition).ToList<spInstituteHospitalGetDashBoardData_Result>();
				entityCounts = MapCommon.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityCounts;
		}

		public DataTable GetDepartmentForHospital(int hospitalId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spDepartmentForHospital]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", hospitalId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public List<EntityDDL> SearchDDL(DDLSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spDDL]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@parentId", obj.parentId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@reffId", obj.reffId);
			sqlCommand.Parameters.AddWithValue("@reffIds", obj.reffIds);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			sqlCommand.Parameters.AddWithValue("@section", obj.section);
			return PrpDbADO.FillDataTableEntityDDL(sqlCommand, 0);
		}

		public Message UnitAddUpdate(Unit obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spUnitAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@unitId", obj.unitId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@departmentId", obj.departmentId);
			sqlCommand.Parameters.AddWithValue("@name", obj.name);
			sqlCommand.Parameters.AddWithValue("@code", obj.code);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}
	}
}