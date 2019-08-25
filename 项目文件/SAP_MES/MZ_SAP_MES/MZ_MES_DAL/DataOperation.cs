using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MZ_MES_DAL
{
    public class DataOperation
    {
        public static bool AddMetralInfo(DataTable dt)
        {

            string connString = ConfigurationManager.ConnectionStrings["MES_Conn"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(connString);
            if (sqlConn.State == ConnectionState.Closed)
            {
                sqlConn.Open();
            }

                try
                {
                        using (SqlCommand cmd = sqlConn.CreateCommand())
                        {
                            SqlParameter[] spara = new SqlParameter[1];
                            spara[0] = new SqlParameter("@Product", SqlDbType.Structured);
                            spara[0].Value = dt;
                            cmd.CommandText = "Txn_Win_IntoMESProduct";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddRange(spara);
                           int dresult= cmd.ExecuteNonQuery();
                        }

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sqlConn != null)
                    {
                        sqlConn.Close();
                        sqlConn.Dispose();
                    }
                }
            
        }
    }
}
