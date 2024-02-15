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

        public EmailResp EmailProcessGetInfoByType(EmailProcess objs)
        {
            EmailResp obj = new EmailResp();
            try
            {
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spEmailProcessGetInfoByType]"
                };
                sqlCommand.Parameters.AddWithValue("@inductionId", objs.inductionId);
                sqlCommand.Parameters.AddWithValue("@typeId", objs.typeId);
                sqlCommand.Parameters.AddWithValue("@applicantId", objs.applicantId);
                sqlCommand.Parameters.AddWithValue("@search", objs.search.TooString());
                sqlCommand.Parameters.AddWithValue("@reffIds1", objs.reffIds1.TooString());
                sqlCommand.Parameters.AddWithValue("@reffIds2", objs.reffIds2.TooString());
                sqlCommand.Parameters.AddWithValue("@reffIds3", objs.reffIds3.TooString());
                sqlCommand.Parameters.AddWithValue("@reffIds4", objs.reffIds4.TooString());
                sqlCommand.Parameters.AddWithValue("@reffIds5", objs.reffIds5.TooString());


                DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
                if ((dataTable == null ? false : dataTable.Rows.Count > 0))
                {
                    DataRow item = dataTable.Rows[0];
                    obj.detailId = item["detailId"].TooInt();
                    obj.applicantId = item["applicantId"].TooInt();
                    obj.typeId = item["typeId"].TooInt();
                    obj.emailId = item["emailId"].TooString();
                    obj.status = item["status"].TooBoolean();

                    obj.reffIds1 = item["reffIds1"].TooString();
                    obj.reffIds2 = item["reffIds2"].TooString();
                    obj.reffIds3 = item["reffIds3"].TooString();
                    obj.reffIds4 = item["reffIds4"].TooString();
                    obj.reffIds5 = item["reffIds5"].TooString();
                    obj.body = item["body"].TooString();
                    obj.title = item["title"].TooString();
                    obj.subject = item["subject"].TooString();

                    obj.emailFrom = item["emailFrom"].TooString();
                    obj.emailPassword = item["emailPassword"].TooString();

                }
            }
            catch (Exception ex)
            {
                obj = new EmailResp();
                obj.status = false;
                obj.body = ex.Message;
            }
            return obj;
        }

        public Message EmailProcessAddUpdate(EmailResp objs)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessUpdateStatus]"
            };
            sqlCommand.Parameters.AddWithValue("@emailProcessId", objs.detailId);
            sqlCommand.Parameters.AddWithValue("@typeId", objs.typeId);
            sqlCommand.Parameters.AddWithValue("@applicantId", objs.applicantId);
            sqlCommand.Parameters.AddWithValue("@emailId", objs.emailId.TooString());
            sqlCommand.Parameters.AddWithValue("@body", objs.body.TooString());
            sqlCommand.Parameters.AddWithValue("@resp", objs.resp.TooString());
            sqlCommand.Parameters.AddWithValue("@isProcess", objs.isProcess);
            sqlCommand.Parameters.AddWithValue("@isSent", objs.isSent);
            return PrpDbADO.FillDataTableMessage(sqlCommand);
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