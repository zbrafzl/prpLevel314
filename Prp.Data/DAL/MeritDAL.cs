using Prp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Prp.Data
{
	public class MeritDAL : PrpDBConnect
	{
		public MeritDAL()
		{
		}

		public DataTable GazatGetAllByTypeExport(GazatMerit obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGazatGetAllByTypeExport]"
			};
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable GazatGetAllByTypeView(GazatMerit obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGazatGetAllByTypeView]"
			};
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public DataTable GazatGetAllByTypeViewExport(GazatMerit obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spGazatGetAllByTypeViewExport]"
			};
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}

		public List<ApplicantSpecilityMerit> GetApplicantSpecialityWithMeritMarks(int inductionId, int applicantId, int roundNo)
		{
			List<ApplicantSpecilityMerit> applicantSpecilityMerits = new List<ApplicantSpecilityMerit>();
			try
			{
				SqlCommand sqlCommand = new SqlCommand()
				{
					CommandType = CommandType.StoredProcedure,
					CommandText = "[dbo].[spApplicantSpecialityWithMeritMarks]"
				};
				sqlCommand.Parameters.AddWithValue("@inductionId", inductionId);
				sqlCommand.Parameters.AddWithValue("@applicantId", applicantId);
				sqlCommand.Parameters.AddWithValue("@roundNo", roundNo);
				DataTable dataTable = PrpDbADO.FillDataTable(sqlCommand, "");
				if ((dataTable == null ? false : dataTable.Rows.Count > 0))
				{
					foreach (DataRow row in dataTable.Rows)
					{
						ApplicantSpecilityMerit applicantSpecilityMerit = new ApplicantSpecilityMerit()
						{
							typeId = row["typeId"].TooInt(),
							applicantId = row["applicantId"].TooInt(),
							specialityJobId = row["specialityJobId"].TooInt(),
							specialityName = row["specialityName"].TooString(""),
							hospitalName = row["hospitalName"].TooString(""),
							preferenceNo = row["preferenceNo"].TooInt(),
							marks = row["marks"].TooDecimal(),
							minMerit = row["minMerit"].TooDecimal(),
							differnceMarks = new decimal?(row["differnceMarks"].TooDecimal()),
							specialityJobIdMerit = row["specialityJobIdMerit"].TooInt(),
							preferenceNoMerit = row["preferenceNoMerit"].TooInt(),
							isFilled = row["isFilled"].TooInt()
						};
						applicantSpecilityMerits.Add(applicantSpecilityMerit);
					}
				}
			}
			catch (Exception exception)
			{
				applicantSpecilityMerits = new List<ApplicantSpecilityMerit>();
			}
			return applicantSpecilityMerits;
		}

		public List<ApplicantSpecilityMerit> GetApplicantSpecialityWithMeritMarksFCPS(int applicantId)
		{
			List<ApplicantSpecilityMerit> applicantSpecilityMerits = new List<ApplicantSpecilityMerit>();
			try
			{
				List<spApplicantSpecialityWithMeritMarksFCPS_Result> list = this.db.spApplicantSpecialityWithMeritMarksFCPS(new int?(applicantId)).ToList<spApplicantSpecialityWithMeritMarksFCPS_Result>();
				applicantSpecilityMerits = MapMerit.ToEntityList(list);
			}
			catch (Exception exception)
			{
				applicantSpecilityMerits = new List<ApplicantSpecilityMerit>();
			}
			return applicantSpecilityMerits;
		}

		public DataTable MeritGetAllByTypeView(GazatMerit obj)
		{
			SqlCommand sqlCommand = new SqlCommand()
			{
				CommandType = CommandType.StoredProcedure,
				CommandText = "[dbo].[spMeritGetAllByTypeView]"
			};
			sqlCommand.Parameters.AddWithValue("@top", obj.top);
			sqlCommand.Parameters.AddWithValue("@pageNum", obj.pageNum);
			sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
			sqlCommand.Parameters.AddWithValue("@quotaId", obj.quotaId);
			sqlCommand.Parameters.AddWithValue("@roundNo", obj.roundNo);
			sqlCommand.Parameters.AddWithValue("@search", obj.search);
			return PrpDbADO.FillDataTable(sqlCommand, "");
		}
	}
}