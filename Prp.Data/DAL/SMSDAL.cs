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
	public class SMSDAL : PrpDBConnect
	{
		public SMSDAL()
		{
		}
        ////new SMSDAL().AddUpdateSmsProcess(objProcess);
        public SMSResp SMSProcessGetInfoByType(SmsProcess objs)
        {
			SMSResp obj = new SMSResp();
			try
			{
                SqlCommand sqlCommand = new SqlCommand()
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spSMSProcessGetInfoByType]"
                };
                sqlCommand.Parameters.AddWithValue("@inductionid", objs.inductionid);
                sqlCommand.Parameters.AddWithValue("@smsId", objs.smsId);
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
                    obj.smsId = item["smsId"].TooInt();
                    obj.contactNumber = item["contactNumber"].TooString();
                    obj.reffIds1 = item["reffIds1"].TooString();
                    obj.reffIds2 = item["reffIds2"].TooString();
                    obj.reffIds3 = item["reffIds3"].TooString();
                    obj.reffIds4 = item["reffIds4"].TooString();
                    obj.reffIds5 = item["reffIds5"].TooString();
                    obj.body = item["body"].TooString();
                    obj.status = item["status"].TooBoolean();
                }
            }
			catch (Exception ex)
			{
                obj = new SMSResp();
				obj.status = false;
				obj.body = ex.Message;
            }
			return obj;
        }

        public Message SMSProcessAddUpdate(SMSResp objs)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSProcessAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@smsProcessId", objs.detailId);
            sqlCommand.Parameters.AddWithValue("@smsId", objs.smsId);
            sqlCommand.Parameters.AddWithValue("@applicantId", objs.applicantId);
            sqlCommand.Parameters.AddWithValue("@body", objs.body.TooString());
            sqlCommand.Parameters.AddWithValue("@resp", objs.resp.TooString());
            sqlCommand.Parameters.AddWithValue("@contactNumber", objs.contactNumber.TooString());
            sqlCommand.Parameters.AddWithValue("@isProcess", objs.isProcess);
            sqlCommand.Parameters.AddWithValue("@isSent", objs.isSent);
             return PrpDbADO.FillDataTableMessage(sqlCommand);
        }

		public List<SMS> GetAll()
		{
			List<SMS> sMs = new List<SMS>();
			try
			{
				//List<tvwSM> list = (
				//	from x in this.db.tvwSMS
				//	orderby x.name
				//	select x).ToList<tvwSM>();
				//sMs = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return sMs;
		}

        public DataSet SMSSearch(SMS obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSSearch]"
            };
            sqlCommand.Parameters.AddWithValue("@projId", obj.projId);
            sqlCommand.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public DataSet SMSGetById(SMS obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSGetById]"
            };
            //sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@id", obj.id);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public Message SMSAddUpdate(SMS obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@id", obj.id);
            sqlCommand.Parameters.AddWithValue("@projId", obj.projId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
            sqlCommand.Parameters.AddWithValue("@detail", obj.detail);
            sqlCommand.Parameters.AddWithValue("@query", obj.query.TooString());
            sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
            sqlCommand.Parameters.AddWithValue("@isQuery", obj.isQuery);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public Message SMSAddUpdateCampaign(SmsCampaign obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSCampaignAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@campaignId", obj.campaignId);
            sqlCommand.Parameters.AddWithValue("@projId", obj.projId);
            sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
            sqlCommand.Parameters.AddWithValue("@isSchedule", obj.isSchedule);
            sqlCommand.Parameters.AddWithValue("@startTime", obj.startTime);
            sqlCommand.Parameters.AddWithValue("@name", obj.name);
            sqlCommand.Parameters.AddWithValue("@detail", obj.detail);
            sqlCommand.Parameters.AddWithValue("@numbers", obj.numbers);
            sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
        }

        public List<SMS> GetAll(int inductionId)
		{
			List<SMS> sMs = new List<SMS>();
			try
			{
				//List<tvwSM> list = (
				//	from x in this.db.tvwSMS
				//	where x.inductionId == inductionId
				//	orderby x.name
				//	select x).ToList<tvwSM>();
				//sMs = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return sMs;
		}

		public List<SMS> GetAllByType(int inductionId, int typeId)
		{
			List<SMS> sMs = new List<SMS>();
			try
			{
				//List<tvwSM> list = (
				//	from x in this.db.tvwSMS
				//	where x.inductionId == inductionId && x.typeId == typeId
				//	orderby x.name
				//	select x).ToList<tvwSM>();
				//sMs = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return sMs;
		}

		public SMS GetById(int id)
		{
			SMS sM = new SMS();
			try
			{
				tblSM _tblSM = this.db.tblSMS.Where(x => x.id == id).FirstOrDefault();
				sM = MapSMS.ToEntity(_tblSM);
			}
			catch (Exception exception)
			{
			}
			return sM;
		}

		public SMS GetByTypeForApplicant(int applicantId, int typeId)
		{
			SMS sM = new SMS();
			try
			{
				//spSMSGetByTypeForApplicant_Result spSMSGetByTypeForApplicantResult = this.db.spSMSGetByTypeForApplicant(new int?(applicantId), new int?(typeId)).FirstOrDefault<spSMSGetByTypeForApplicant_Result>();
				//sM = MapSMS.ToEntity(spSMSGetByTypeForApplicantResult);
			}
			catch (Exception exception)
			{
			}
			return sM;
		}

		public DataTable SearchSMSByApplicant(SmsEntity obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spSMSProcessSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message SMSProcessCreateListBySmsId(int typeId, int smsId)
		{
			Message message = new Message();
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spSMSProcessCreateListBySmsId]"
			};
			sqlCommand.Parameters.AddWithValue("@typeId", typeId);
			sqlCommand.Parameters.AddWithValue("@smsId", smsId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public List<SmsProcess> SMSProcessGetBySmsId(int smsId, int isProcess = 0)
		{
			List<SmsProcess> smsProcesses = new List<SmsProcess>();
			try
			{
				//List<spSMSProcessGetBySmsId_Result> list = this.db.spSMSProcessGetBySmsId(new int?(smsId), new int?(isProcess)).ToList<spSMSProcessGetBySmsId_Result>();
				//smsProcesses = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return smsProcesses;
		}

		public List<SmsProcess> SMSProcessGetByType(int typeId, int isProcess)
		{
			List<SmsProcess> smsProcesses = new List<SmsProcess>();
			//try
			//{
			//	List<spSMSProcessGetByType_Result> list = this.db.spSMSProcessGetByType(new int?(typeId), new int?(isProcess)).ToList<spSMSProcessGetByType_Result>();
			//	smsProcesses = MapSMS.ToEntityList(list);
			//}
			//catch (Exception exception)
			//{
			//}
			return smsProcesses;
		}

		public List<spSMSProcessGetRemaning_Result> SMSProcessGetRemaning()
		{
            return this.db.spSMSProcessGetRemaning().ToList<spSMSProcessGetRemaning_Result>();
        }
	}
}