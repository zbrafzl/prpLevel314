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
    public class APIDataDAL
    {
        public DataTable APIInductionGetAll(int projId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "api.spAPIInductionGetAll"
            };
            sqlCommand.Parameters.AddWithValue("@projId", projId);
            return PrpDbADO.FillDataTable(sqlCommand);
        }

        public DataTable APIInstituteGetAll(int projId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "api.spAPIInstituteGetAll"
            };
            sqlCommand.Parameters.AddWithValue("@projId", projId);
            return PrpDbADO.FillDataTable(sqlCommand);
        }

        public DataTable APIHospitalGetAll(int projId)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "api.spAPIHospitalGetAll"
            };
            sqlCommand.Parameters.AddWithValue("@projId", projId);
            return PrpDbADO.FillDataTable(sqlCommand);
        }

        public DataTable APIHospitalGetByInstitute(HospitalReq obj)
        {
            SqlCommand sqlCommand = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "api.spAPIHospitalGetAll"
            };
            sqlCommand.Parameters.AddWithValue("@projId", obj.projId);
            sqlCommand.Parameters.AddWithValue("@instituteId", obj.instituteId);
            return PrpDbADO.FillDataTable(sqlCommand);
        }
    }
}
