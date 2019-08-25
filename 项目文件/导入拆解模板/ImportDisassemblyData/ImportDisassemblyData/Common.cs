using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ImportDisassemblyData
{
    public static class Common
    {
        public static string GetPKID(string table)
        {
            SqlParameter[] parameters = {
                        new SqlParameter("@ObjectId",SqlDbType.Char),
                        new SqlParameter("@ObjectName",SqlDbType.NVarChar),
                        new SqlParameter("@PKID",SqlDbType.Char,12)};

            parameters[0].Value = "";
            parameters[1].Value = table;
            parameters[2].Value = "";
            parameters[2].Direction = ParameterDirection.Output;

            DBUtility.ExecStoredProcedure("SysGetObjectPKid", parameters);
            return parameters[2].Value.ToString();
        }

    }
}
