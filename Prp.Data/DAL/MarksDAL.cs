using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prp.Data
{
	public class MarksDAL : PrpDBConnect
	{
		public MarksDAL()
		{
		}

		public List<Marks> GetMarksAccumulativeByApplicant(int applicantId)
		{
			List<Marks> marks = new List<Marks>();
			try
			{
				List<spMarksGetAccumulativeByApplicant_Result> list = this.db.spMarksGetAccumulativeByApplicant(new int?(applicantId), 0).ToList<spMarksGetAccumulativeByApplicant_Result>();
					marks = MapMark.ToEntityList(list);
			}
			catch (Exception ex)
			{
			}
			return marks;
		}

		public List<Marks> GetMarksDetaikByApplicant(int inductionId, int applicantId)
		{
			List<Marks> marks = new List<Marks>();
			try
			{
				List<spMarksDetailByApplicant_Result> list = this.db.spMarksDetailByApplicant(new int?(inductionId), new int?(applicantId)).ToList<spMarksDetailByApplicant_Result>();
				marks = MapMark.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return marks;
		}
	}
}