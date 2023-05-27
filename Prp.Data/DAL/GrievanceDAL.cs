using Prp.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class GrievanceDAL : PrpDBConnect
	{
		public GrievanceDAL()
		{
		}

		public Message ActionAddUpdate(GrievanceAction obj)
		{
			Message message = new Message();
			try
			{
				this.db.spGrievanceAttendenceAddUpdate(new int?(obj.grievanceActionId), new int?(obj.grievanceId), new int?(obj.isSelf), new int?(obj.relationId), new int?(obj.attendenceNo), new int?(obj.adminIdAttendece));
				message.status = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public GrievanceAction ActionGetById(int grievanceId)
		{
			GrievanceAction grievanceAction = new GrievanceAction();
			try
			{
				tblGrievanceAction _tblGrievanceAction = this.db.tblGrievanceActions.FirstOrDefault<tblGrievanceAction>((tblGrievanceAction x) => x.grievanceId == grievanceId);
				grievanceAction = MapGrievanceAction.ToEntity(_tblGrievanceAction);
			}
			catch (Exception exception)
			{
				grievanceAction = new GrievanceAction();
			}
			if (grievanceAction == null)
			{
				grievanceAction = new GrievanceAction();
			}
			return grievanceAction;
		}

		public Message AddUpdate(Grievance obj)
		{
			Message message = new Message();
			try
			{
				this.db.spGrievanceAddUpdate(new int?(obj.grievanceId), new int?(obj.inductionId), new int?(obj.phaseId), new int?(obj.applicantId), new int?(obj.typeId), new int?(obj.verficationTypeId), obj.checkListIds, obj.title, obj.detail);
				message.status = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public Grievance GetById(int grievanceId)
		{
			Grievance grievance = new Grievance();
			try
			{
				tblGrievance _tblGrievance = this.db.tblGrievances.FirstOrDefault<tblGrievance>((tblGrievance x) => x.grievanceId == grievanceId);
				grievance = MapGrievance.ToEntity(_tblGrievance);
			}
			catch (Exception exception)
			{
				grievance = new Grievance();
			}
			if (grievance == null)
			{
				grievance = new Grievance();
			}
			return grievance;
		}

		public Grievance GetByTypeAndApplicant(int typeId, int applicantId)
		{
			Grievance grievance = new Grievance();
			try
			{
				tblGrievance _tblGrievance = this.db.tblGrievances.FirstOrDefault<tblGrievance>((tblGrievance x) => x.typeId == typeId && x.applicantId == applicantId);
				grievance = MapGrievance.ToEntity(_tblGrievance);
			}
			catch (Exception exception)
			{
				grievance = new Grievance();
			}
			if (grievance == null)
			{
				grievance = new Grievance();
			}
			return grievance;
		}

		public DataTable GrievancePrint(Grievance obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGrievancePrint]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@dated", obj.dated);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable GrievanceSearch(Grievance obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGrievanceSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@verficationTypeId", obj.verficationTypeId);
			sqlCommand.Parameters.AddWithValue("@checkListIds", obj.checkListIds);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable GrievanceSearchAttendence(Grievance obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGrievanceSearchAttendece]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@verficationTypeId", obj.verficationTypeId);
			sqlCommand.Parameters.AddWithValue("@checkListIds", obj.checkListIds);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message RemarksAddUpdate(GrievanceRemark obj)
		{
			Message message = new Message();
			try
			{
				this.db.spGrievanceRemarksAddUpdate(new int?(obj.grievanceRemarksId), new int?(obj.grievanceId), obj.title, new int?(obj.typeId), obj.remarks, new int?(obj.adminId));
				message.status = true;
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				message.status = false;
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public GrievanceRemark RemarksGetById(int grievanceId)
		{
			GrievanceRemark grievanceRemark = new GrievanceRemark();
			try
			{
				tblGrievanceRemark _tblGrievanceRemark = this.db.tblGrievanceRemarks.FirstOrDefault<tblGrievanceRemark>((tblGrievanceRemark x) => x.grievanceId == grievanceId);
				grievanceRemark = MapGrievanceRemark.ToEntity(_tblGrievanceRemark);
			}
			catch (Exception exception)
			{
				grievanceRemark = new GrievanceRemark();
			}
			if (grievanceRemark == null)
			{
				grievanceRemark = new GrievanceRemark();
			}
			return grievanceRemark;
		}
	}
}