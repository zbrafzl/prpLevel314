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

		public Message AddUpdate(SMS obj)
		{
			Message message = new Message();
			try
			{
				tblSM _tblSM = new tblSM();
				if (obj.smsId != 0)
				{
					_tblSM = this.db.tblSMS.FirstOrDefault<tblSM>((tblSM x) => x.smsId == obj.smsId);
					_tblSM.inductionId = obj.inductionId;
					_tblSM.phaseId = obj.phaseId;
					_tblSM.name = obj.name;
					_tblSM.detail = obj.detail;
					_tblSM.preDetail = obj.preDetail;
					_tblSM.postDetail = obj.postDetail;
					_tblSM.isActive = obj.isActive;
					_tblSM.typeId = obj.typeId;
					_tblSM.dated = obj.dated;
					_tblSM.adminId = obj.adminId;
				}
				else
				{
					_tblSM = MapSMS.ToTable(obj);
					this.db.tblSMS.Add(_tblSM);
				}
				this.db.SaveChanges();
				message.id = _tblSM.smsId;
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public Message AddUpdateSmsProcess(SmsProcess obj)
		{
			Message message = new Message();
			try
			{
				this.db.spSMSProcessAddUpdate(new int?(obj.smsProcessId), new int?(obj.smsId), new int?(obj.applicantId), new int?(obj.isProcess), new int?(obj.isSent));
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public List<SMS> GetAll()
		{
			List<SMS> sMs = new List<SMS>();
			try
			{
				List<tvwSM> list = (
					from x in this.db.tvwSMS
					orderby x.name
					select x).ToList<tvwSM>();
				sMs = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return sMs;
		}

		public List<SMS> GetAll(int inductionId)
		{
			List<SMS> sMs = new List<SMS>();
			try
			{
				List<tvwSM> list = (
					from x in this.db.tvwSMS
					where x.inductionId == inductionId
					orderby x.name
					select x).ToList<tvwSM>();
				sMs = MapSMS.ToEntityList(list);
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
				List<tvwSM> list = (
					from x in this.db.tvwSMS
					where x.inductionId == inductionId && x.typeId == typeId
					orderby x.name
					select x).ToList<tvwSM>();
				sMs = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return sMs;
		}

		public SMS GetById(int smsId)
		{
			SMS sM = new SMS();
			try
			{
				tblSM _tblSM = this.db.tblSMS.FirstOrDefault<tblSM>((tblSM x) => x.smsId == smsId);
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
				spSMSGetByTypeForApplicant_Result spSMSGetByTypeForApplicantResult = this.db.spSMSGetByTypeForApplicant(new int?(applicantId), new int?(typeId)).FirstOrDefault<spSMSGetByTypeForApplicant_Result>();
				sM = MapSMS.ToEntity(spSMSGetByTypeForApplicantResult);
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
				List<spSMSProcessGetBySmsId_Result> list = this.db.spSMSProcessGetBySmsId(new int?(smsId), new int?(isProcess)).ToList<spSMSProcessGetBySmsId_Result>();
				smsProcesses = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return smsProcesses;
		}

		public List<SmsProcess> SMSProcessGetByType(int typeId, int isProcess)
		{
			List<SmsProcess> smsProcesses = new List<SmsProcess>();
			try
			{
				List<spSMSProcessGetByType_Result> list = this.db.spSMSProcessGetByType(new int?(typeId), new int?(isProcess)).ToList<spSMSProcessGetByType_Result>();
				smsProcesses = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return smsProcesses;
		}

		public List<SmsProcess> SMSProcessGetRemaning()
		{
			List<SmsProcess> smsProcesses = new List<SmsProcess>();
			try
			{
				List<spSMSProcessGetRemaning_Result> list = this.db.spSMSProcessGetRemaning().ToList<spSMSProcessGetRemaning_Result>();
				smsProcesses = MapSMS.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return smsProcesses;
		}
	}
}