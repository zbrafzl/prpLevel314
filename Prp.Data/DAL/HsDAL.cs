using Prp.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Prp.Data
{
    public class HsDAL : PrpDBConnect
    {
        public tblH GetHsCurrent()
        {
            tblH obj = new tblH();
            try
            {
                obj = this.db.tblHs.FirstOrDefault(x => x.isCompleted == false);
                if(obj== null)
                    obj = new tblH();
            }
            catch (Exception exception)
            {
                obj = new tblH();
            }
            return obj;
        }

        public List<tblHsCalendarStep> GetHsCalendarStep(int hsId)
        {
            List<tblHsCalendarStep> list = new List<tblHsCalendarStep>();
            try
            {
              list = this.db.tblHsCalendarSteps.Where(x => x.hsId == hsId).ToList();

            }
            catch (Exception exception)
            {
                list = new List<tblHsCalendarStep>();
            }
            return list;
        }
    }
}
