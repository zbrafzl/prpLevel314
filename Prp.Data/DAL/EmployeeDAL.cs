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
	public class EmployeeDAL : PrpDBConnect
	{
		public EmployeeDAL()
		{
		}

		public Message AddUpdate(Employee obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@name", obj.name);
			sqlCommand.Parameters.AddWithValue("@relationId", obj.relationId);
			sqlCommand.Parameters.AddWithValue("@relationName", obj.relationName);
			sqlCommand.Parameters.AddWithValue("@genderId", obj.genderId);
			sqlCommand.Parameters.AddWithValue("@martialStatusId", obj.martialStatusId);
			sqlCommand.Parameters.AddWithValue("@cellNo", obj.cellNo);
			sqlCommand.Parameters.AddWithValue("@cnic", obj.cnic);
			sqlCommand.Parameters.AddWithValue("@districtId", obj.districtId);
			sqlCommand.Parameters.AddWithValue("@address", obj.address);
			sqlCommand.Parameters.AddWithValue("@designationId", obj.designationId);
			sqlCommand.Parameters.AddWithValue("@degreeId", obj.degreeId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
			sqlCommand.Parameters.AddWithValue("@image", obj.image);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@programIds", obj.programIds);
			sqlCommand.Parameters.AddWithValue("@yearExerience", obj.yearExerience);
			sqlCommand.Parameters.AddWithValue("@rtmcNumber", obj.rtmcNumber);
			sqlCommand.Parameters.AddWithValue("@imageRTMC", obj.imageRTMC);
			sqlCommand.Parameters.AddWithValue("@statusApproval", obj.statusApproval);
			sqlCommand.Parameters.AddWithValue("@uhsNumber", obj.uhsNumber);
			sqlCommand.Parameters.AddWithValue("@imageUHS", obj.imageUHS);
			sqlCommand.Parameters.AddWithValue("@statusApprovalUHS", obj.statusApprovalUHS);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message AddUpdateEmployeeSpeciality(EmployeeSpeciality obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeSpecialityAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message AddUpdateExperience(EmployeeExperience obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeExperienceAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@id", obj.id);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@hospitalName", obj.hospitalName);
			sqlCommand.Parameters.AddWithValue("@dateStart", obj.dateStart);
			sqlCommand.Parameters.AddWithValue("@dateEnd", obj.dateEnd);
			sqlCommand.Parameters.AddWithValue("@isCurrent", obj.isCurrent);
			sqlCommand.Parameters.AddWithValue("@executionType", obj.executionType);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message EmployeeDelete(Employee obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeDelete]"
			};
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<EmployeeExperience> EmployeeExperienceGet(EmployeeExperience obj)
		{
			List<EmployeeExperience> employeeExperiences = new List<EmployeeExperience>();
			try
			{
				List<spEmployeeExperienceGet_Result> list = this.db.spEmployeeExperienceGet(new int?(obj.employeeId), new int?(obj.hospitalId), new int?(obj.adminId)).ToList<spEmployeeExperienceGet_Result>();
				employeeExperiences = MapEmployee.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return employeeExperiences;
		}

		public EmployeeExperience EmployeeExperienceGetById(int id)
		{
			EmployeeExperience employeeExperience = new EmployeeExperience();
			try
			{
				tblEmployeeExperience _tblEmployeeExperience = this.db.tblEmployeeExperiences.FirstOrDefault<tblEmployeeExperience>((tblEmployeeExperience x) => x.id == id);
				employeeExperience = ((_tblEmployeeExperience == null ? true : _tblEmployeeExperience.id <= 0) ? new EmployeeExperience() : MapEmployee.ToEntity(_tblEmployeeExperience));
			}
			catch (Exception exception)
			{
				employeeExperience = new EmployeeExperience();
			}
			return employeeExperience;
		}

		public Message EmployeeIsExistsCellNo(Employee obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeIsExistsCellNo]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@cellNo", obj.cellNo);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message EmployeeIsExistsCNIC(Employee obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeIsExistsCNIC]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@cnic", obj.cnic);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		//public List<spEmployeeSearch_Result> EmployeeSearch(EmployeeSearch obj)
		//{
		//	List<spEmployeeSearch_Result> spEmployeeSearchResults = new List<spEmployeeSearch_Result>();
		//	try
		//	{
		//		spEmployeeSearchResults = this.db.spEmployeeSearch(new int?(10000),new int?(1),  new int?(obj.hospitalId), new int?(obj.adminId), obj.search).ToList<spEmployeeSearch_Result>();
		//	}
		//	catch (Exception exception)
		//	{
		//		spEmployeeSearchResults = new List<spEmployeeSearch_Result>();
		//	}
		//	return spEmployeeSearchResults;
		//}

		public DataTable EmployeeSearchReport(EmployeeSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeSearchReport]"
			};
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@genderId", obj.genderId);
			sqlCommand.Parameters.AddWithValue("@degreeId", obj.degreeId);
			sqlCommand.Parameters.AddWithValue("@designationId", obj.designationId);
			sqlCommand.Parameters.AddWithValue("@reportTypeId", obj.reportTypeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Employee GetById(int employeeId)
		{
			Employee employee = new Employee();
			try
			{
				tblEmployee _tblEmployee = this.db.tblEmployees.FirstOrDefault<tblEmployee>((tblEmployee x) => x.employeeId == employeeId);
				employee = ((_tblEmployee == null ? true : _tblEmployee.employeeId <= 0) ? new Employee() : MapEmployee.ToEntity(_tblEmployee));
			}
			catch (Exception exception)
			{
				employee = new Employee();
			}
			return employee;
		}

		public Employee GetDetailById(int employeeId)
		{
			Employee employee = new Employee();
			try
			{
				tvwEmployee _tvwEmployee = this.db.tvwEmployees.FirstOrDefault<tvwEmployee>((tvwEmployee x) => x.employeeId == employeeId);
				employee = ((_tvwEmployee == null ? true : _tvwEmployee.employeeId <= 0) ? new Employee() : MapEmployee.ToEntity(_tvwEmployee));
			}
			catch (Exception exception)
			{
				employee = new Employee();
			}
			return employee;
		}

		public List<EmployeeSpeciality> GetEmployeeSpecialities(int employeeId)
		{
			List<EmployeeSpeciality> employeeSpecialities = new List<EmployeeSpeciality>();
			try
			{
				List<tvwEmployeeSpeciality> list = (
					from x in this.db.tvwEmployeeSpecialities
					where x.employeeId == employeeId
					select x).ToList<tvwEmployeeSpeciality>();
				employeeSpecialities = MapEmployee.ToEntityList(list);
			}
			catch (Exception exception)
			{
				employeeSpecialities = new List<EmployeeSpeciality>();
			}
			return employeeSpecialities;
		}

		public EmployeeSpeciality GetEmployeeSpeciality(int id)
		{
			EmployeeSpeciality employeeSpeciality = new EmployeeSpeciality();
			try
			{
				tblEmployeeSpeciality _tblEmployeeSpeciality = this.db.tblEmployeeSpecialities.FirstOrDefault<tblEmployeeSpeciality>((tblEmployeeSpeciality x) => x.id == id);
				employeeSpeciality = ((_tblEmployeeSpeciality == null ? true : _tblEmployeeSpeciality.id <= 0) ? new EmployeeSpeciality() : MapEmployee.ToEntity(_tblEmployeeSpeciality));
			}
			catch (Exception exception)
			{
				employeeSpeciality = new EmployeeSpeciality();
			}
			return employeeSpeciality;
		}

		public EmployeeSpeciality GetEmployeeSpecialitySingleByEmployee(int employeeId)
		{
			EmployeeSpeciality employeeSpeciality = new EmployeeSpeciality();
			try
			{
				tblEmployeeSpeciality _tblEmployeeSpeciality = this.db.tblEmployeeSpecialities.FirstOrDefault<tblEmployeeSpeciality>((tblEmployeeSpeciality x) => x.employeeId == employeeId);
				employeeSpeciality = ((_tblEmployeeSpeciality == null ? true : _tblEmployeeSpeciality.id <= 0) ? new EmployeeSpeciality() : MapEmployee.ToEntity(_tblEmployeeSpeciality));
			}
			catch (Exception exception)
			{
				employeeSpeciality = new EmployeeSpeciality();
			}
			return employeeSpeciality;
		}

		public List<Employee> Search(EmployeeSearch obj)
		{
			List<Employee> employees = new List<Employee>();
			try
			{
				//List<spEmployeeSearch_Result> list = this.db.spEmployeeSearch(new int?(10000), new int?(1), new int?(obj.hospitalId), new int?(obj.adminId), obj.search).ToList<spEmployeeSearch_Result>();
				//employees = MapEmployee.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return employees;
		}
	}
}