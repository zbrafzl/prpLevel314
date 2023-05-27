using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prp.Data
{
	public class RegionDAL : PrpDBConnect
	{
		public RegionDAL()
		{
		}

		public List<Region> RegionGetByCondition(int typeId, int parentId, string condition = "")
		{
			List<Region> regions = new List<Region>();
			try
			{
				List<spRegionGetByCondition_Result> list = this.db.spRegionGetByCondition(new int?(typeId), new int?(parentId), condition).ToList<spRegionGetByCondition_Result>();
				regions = MapRegion.ToEntityList(list);
			}
			catch (Exception exception)
			{
				regions = new List<Region>();
			}
			return regions;
		}
	}
}