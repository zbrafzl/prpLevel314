using System;
using System.Data;
using System.Data.SqlClient;

namespace Prp.Data
{
	public class EmailSMSDAL : PrpDBConnect
	{
		public EmailSMSDAL()
		{
		}

		public DataTable GetEmailStatusRemaningRecords(string condition)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spEmailStatusRemaningRecords]"
			};
			sqlCommand.Parameters.AddWithValue("@condition", condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}