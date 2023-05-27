using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Prp.Data
{
	public class InductionDAL : PrpDBConnect
	{
		public InductionDAL()
		{
		}

		public List<EntityDDL> GetInductionDDL(DDLInduction obj)
		{
			List<EntityDDL> entityDDLs = new List<EntityDDL>();
			try
			{
				List<spInductionForDDL_Result> list = (
					from x in this.db.spInductionForDDL(new int?(obj.userId), obj.reffIds, obj.condition)
					orderby x.id descending
					select x).ToList<spInductionForDDL_Result>();
				entityDDLs = MapInduction.ToEntityList(list);
			}
			catch (Exception exception)
			{
			}
			return entityDDLs;
		}
	}
}