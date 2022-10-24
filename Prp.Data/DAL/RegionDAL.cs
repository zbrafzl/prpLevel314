using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class RegionDAL : PrpDBConnect
    {
        public List<Region> RegionGetByCondition(int typeId, int parentId, string condition="")
        {
            List<Region> list = new List<Region>();
            try
            {
                var listt = db.spRegionGetByCondition(typeId, parentId, condition).ToList();
                list = MapRegion.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<Region>();
            }
            return list;
        }
    }
}
