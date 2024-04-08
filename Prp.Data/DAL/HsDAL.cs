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
                obj = this.db.tblHs.Where(x => x.isCompleted == false).OrderByDescending(x=>x.hsId).FirstOrDefault();
                if(obj== null)
                    obj = new tblH();
            }
            catch (Exception ex)
            {
                obj = new tblH();
            }
            return obj;
        }

        public List<tblHsStepCalendar> GetHsCalendarStep(int hsId)
        {
            List<tblHsStepCalendar> list = new List<tblHsStepCalendar>();
            try
            {
              list = this.db.tblHsStepCalendars.Where(x => x.hsId == hsId).ToList();

            }
            catch (Exception exception)
            {
                list = new List<tblHsStepCalendar>();
            }
            return list;
        }


        public DataSet HsApplicationAddUpdate(HsApplication obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHsApplicationAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@hsId", obj.hsId);
            sqlCommand.Parameters.AddWithValue("@hsAppId", obj.hsAppId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            sqlCommand.Parameters.AddWithValue("@typeId", obj.typeId);
            sqlCommand.Parameters.AddWithValue("@detail", obj.detail);
            SqlParameter paramPreference = new SqlParameter("@tblPreference", SqlDbType.Structured)
            {
                TypeName = SqlDataTypes.tbType,
                Value = obj.dtPreference
            };
            sqlCommand.Parameters.Add(paramPreference);
            return PrpDbADO.FillDataSet(sqlCommand);
        }

        public Message HsApplicationDocsAddUpdate(HsApplication obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHsApplicationDocsAddUpdate]"
            };
            sqlCommand.Parameters.AddWithValue("@hsId", obj.hsId);
            sqlCommand.Parameters.AddWithValue("@hsAppId", obj.hsAppId);
            sqlCommand.Parameters.AddWithValue("@applicantId", obj.applicantId);
            SqlParameter paramDoc = new SqlParameter("@tblDocs", SqlDbType.Structured)
            {
                TypeName = SqlDataTypes.tbType,
                Value = obj.dtDocs
            };
            sqlCommand.Parameters.Add(paramDoc);
            return PrpDbADO. FillDataTableMessage(sqlCommand);
        }
    }
}
