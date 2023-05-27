using Prp.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Prp.Data
{
	public class TraineeDAL : PrpDBConnect
	{
		public TraineeDAL()
		{
		}

		public DataTable ApplicantForTraineeYear(TraineeSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantForTraineeYear]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message EmployeeTraineeAddUpdate(EmployeeTrainee obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmployeeTraineeAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Message TraineeAddUpdate(TraineeInfo obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTraineeInfoAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@specialityId", obj.specialityId);
			sqlCommand.Parameters.AddWithValue("@marks", obj.marks);
			sqlCommand.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public DataTable TraineeAttachedGetByEmployee(TraineeSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTraineeAttachedGetByEmployee]"
			};
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@employeeId", obj.employeeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable TraineeGetById(int applicantId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTraineeInfoGetById]"
			};
			sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message TraineeInfoStatusUpdate(TraineeInfo obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTraineeInfoStatusUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			sqlCommand.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
			sqlCommand.Parameters.AddWithValue("@hospitalIdTo", obj.hospitalIdTo);
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}
	}
}