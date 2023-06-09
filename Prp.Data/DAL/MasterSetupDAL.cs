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
	public class MasterSetupDAL : PrpDBConnect
	{
		public MasterSetupDAL()
		{
		}

		public DataTable ApplicantResearchSearch(ResearchJournalSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spApplicantResearchPaperSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@phaseId", obj.phaseId);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@researchJournalId", obj.researchJournalId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public List<EntityDDL> GetJournalForDDL(DDLJournal obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spResearchJournalForDDL_Result> list = this.db.spResearchJournalForDDL(new int?(obj.typeId), new int?(obj.parentId), new int?(obj.reffId), new int?(obj.userId), obj.reffIds, obj.condition).ToList<spResearchJournalForDDL_Result>();
				entityDDLs = MapResearchJournal.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public List<EntityDDL> GetRegionDDL(DDLRegions obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spRegionForDDL_Result> list = this.db.spRegionForDDL(new int?(obj.typeId), new int?(obj.parentId), new int?(obj.reffId), new int?(obj.userId), obj.reffIds, obj.condition).ToList<spRegionForDDL_Result>();
				entityDDLs = MapRegion.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}

		public Message JournalAddUpdate(ResearchJournal obj)
		{
			Message message = new Message();
			try
			{
				this.db.spResearchJournalAddUpdate(new int?(obj.researchJournalId), obj.name, obj.code, obj.url, new int?(obj.typeId), new int?(obj.batchId), new int?(obj.regionId), new int?(obj.displineId), new bool?(obj.isActive), new int?(obj.adminId));
			}
			catch (Exception exception)
			{
				message.msg = exception.Message;
				message.id = 0;
			}
			return message;
		}

		public ResearchJournal JournalGetById(int researchJournalId)
		{
			ResearchJournal researchJournal = new ResearchJournal();
			try
			{
				tblResearchJournal _tblResearchJournal = this.db.tblResearchJournals.FirstOrDefault<tblResearchJournal>((tblResearchJournal x) => x.researchJournalId == researchJournalId);
				researchJournal = MapResearchJournal.ToEntity(_tblResearchJournal);
			}
			catch (Exception exception)
			{
				researchJournal = new ResearchJournal();
			}
			return researchJournal;
		}

		public DataTable RegionSearch(RegionSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spRegionSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable ResearchJournalSearch(ResearchJournalSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spResearchJournalSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@displineId", obj.displineId);
			sqlCommand.Parameters.AddWithValue("@regionId", obj.regionId);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public Message TickerAddUpdate(Ticker obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTickerAddUpdate]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", obj.inductionId);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@tickerId", obj.tickerId);
			sqlCommand.Parameters.AddWithValue("@name", obj.name);
			sqlCommand.Parameters.AddWithValue("@detail", obj.detail);
			sqlCommand.Parameters.AddWithValue("@sortOrder", obj.sortOrder);
			sqlCommand.Parameters.AddWithValue("@isActive", obj.isActive);
			sqlCommand.Parameters.AddWithValue("@adminId", obj.adminId);
			return PrpDbADO.FillDataTableMessage(sqlCommand, 0);
		}

		public Ticker TickerGetById(int tickerId)
		{
			Ticker ticker = new Ticker();
			try
			{
				tblTicker _tblTicker = this.db.tblTickers.FirstOrDefault<tblTicker>((tblTicker x) => x.tickerId == tickerId);
				ticker = MapTicker.ToEntity(_tblTicker);
			}
			catch (Exception exception)
			{
				ticker = new Ticker();
			}
			return ticker;
		}

		public List<Ticker> TickerGetByInduction(int inductionId, int typeId)
		{
			List<Ticker> tickers = new List<Ticker>();
			try
			{
				List<tvwTicker> list = (
					from x in this.db.tvwTickers
					where x.inductionId == inductionId && x.typeId == typeId
					select x).ToList<tvwTicker>();
				tickers = MapTicker.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return tickers;
		}

		public List<Ticker> TickerGetByInduction(int inductionId)
		{
			List<Ticker> tickers = new List<Ticker>();
			try
			{
				List<tvwTicker> list = (
					from x in this.db.tvwTickers
					where x.inductionId == inductionId
					select x).ToList<tvwTicker>();
				tickers = MapTicker.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return tickers;
		}

		public DataTable TickerSearch(int inductionId)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spTickerSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@inductionId", inductionId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}