using Prp.Data;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Prp.Data.DAL
{
	public class VoucherDAL : PrpDBConnect
	{
		public VoucherDAL()
		{
		}

		public DataTable GetApplicantThoseWasInServiceHospitalSepeciality()
		{
			return PrpDbADO.FillDataTable(new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGetApplicantThoseWasInServiceHospitalSepeciality]"
			}, "");
		}

		public List<VoucherInfo> GetVoucherByCondtion(int applicationStatusId, int countryType, string condition)
		{
			List<VoucherInfo> voucherInfos = new List<VoucherInfo>();
			try
			{
				List<spVoucherGetList_Result> list = this.db.spVoucherGetList(new int?(applicationStatusId), new int?(countryType), condition).ToList<spVoucherGetList_Result>();
				voucherInfos = MapVoucher.ToEntityList(list);
			}
			catch (Exception exception)
			{
				voucherInfos = new List<VoucherInfo>();
			}
			return voucherInfos;
		}

		public List<VoucherInfo> GetVoucherList(int countryId = 1)
		{
			List<VoucherInfo> voucherInfos = new List<VoucherInfo>();
			try
			{
				if (countryId == 1)
				{
					List<tvwVoucherInfo> list = (
						from x in this.db.tvwVoucherInfoes
						where x.countryId == 132
						select x).ToList<tvwVoucherInfo>();
					voucherInfos = MapVoucher.ToEntityList(list);
				}
			}
			catch (Exception exception)
			{
				voucherInfos = new List<VoucherInfo>();
			}
			return voucherInfos;
		}

		public DataTable VoucherExport(VoucherSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spVoucherExport]"
			};
			sqlCommand.Parameters.AddWithValue("@countryTypeId", obj.countryTypeId);
			sqlCommand.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
			sqlCommand.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
			sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
			sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
			sqlCommand.Parameters.AddWithValue("@applicationStatusId", obj.applicationStatusId);
			sqlCommand.Parameters.AddWithValue("@voucherStatusId", obj.voucherStatusId);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable VoucherExportBank(VoucherSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spVoucherExportBank]"
			};
			sqlCommand.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
			sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
			sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable VoucherSearch(VoucherSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spVoucherSearch]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@countryTypeId", obj.countryTypeId);
			sqlCommand.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
			sqlCommand.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
			sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
			sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
			sqlCommand.Parameters.AddWithValue("@applicationStatusId", obj.applicationStatusId);
			sqlCommand.Parameters.AddWithValue("@voucherStatusId", obj.voucherStatusId);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable VoucherSearchBank(VoucherSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spVoucherSearchBank]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
			sqlCommand.Parameters.AddWithValue("@startDate", obj.startDate);
			sqlCommand.Parameters.AddWithValue("@endDate", obj.endDate);
			sqlCommand.Parameters.AddWithValue("@condition", obj.condition);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable VoucherSearchWithBank(VoucherSearch obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spVoucherSearchWithBank]"
			};
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@statusId", obj.statusId);
			sqlCommand.Parameters.AddWithValue("@statusIdBank", obj.statusIdBank);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}