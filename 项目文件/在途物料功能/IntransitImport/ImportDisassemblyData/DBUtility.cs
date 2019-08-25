using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;

namespace IntransitImport
{
    public class DBUtility
    {
        public static string ConnectionString;

        public static void ExecStoredProcedure(string procedure, SqlParameter[] paras)
        {
            DataSet ds = new DataSet();
            string strConn = ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = procedure;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandTimeout = conn.ConnectionTimeout;
                    comm.Parameters.AddRange(paras);
                    //comm.ExecuteReader();
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        System.Windows.Forms.MessageBox.Show("导入成功");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("导入失败");
                    }
                    conn.Close();
                }
                catch(Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
                finally
                {

                }
                
            }
        }

        // 执行sql脚本或存储过程，为确保完整性增加了事务
        public static void ExecStoredProcedureWithTrans(SqlCommandForBatch[] cmds)
        {
            string strConn = ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.Transaction = trans;
                    comm.CommandTimeout = conn.ConnectionTimeout;

                    bool hasError = false;
                    foreach (SqlCommandForBatch item in cmds)
                    {
                        comm.CommandType = item.CmdType;
                        comm.CommandText = item.CmdText;

                        comm.Parameters.Clear();
                        comm.Parameters.AddRange(item.Paras);
                        try
                        {
                            comm.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            hasError = true;
                            break;
                            throw ex;
                        }
                    }

                    if (hasError)
                        trans.Rollback();
                    else
                        trans.Commit();
                }
                conn.Close();
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTableWithSQLString(string sql)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            string strConn = ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;

                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = conn.ConnectionTimeout;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(ds);

                conn.Close();
            }
            if (ds.Tables.Count > 0) dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTableWithSQLString(string sql, string strConn)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = sql;

                comm.CommandType = CommandType.Text;
                comm.CommandTimeout = conn.ConnectionTimeout;

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comm;
                adapter.Fill(ds);

                conn.Close();
            }
            if (ds.Tables.Count > 0) dt = ds.Tables[0];

            return dt;
        }

        public static int ExcuteSql(string strsql)
        {
            string strConn = ConnectionString;
            return ExcuteSql(strsql, strConn);
        }

        public static int ExcuteSql(string strsql, string strConn)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                try
                {
                    SqlCommand cmd = new SqlCommand(strsql, conn);
                    result = cmd.ExecuteNonQuery();
                }
                catch
                {
                    result = 0;
                }
                finally
                {
                    conn.Close();
                }
            }

            return result;
        }
    }

    public class SqlCommandForBatch
    {
        public CommandType CmdType { get; set; }
        public string CmdText { get; set; }
        public SqlParameter[] Paras { get; set; }

        public SqlCommandForBatch()
        {
            CmdType = CommandType.Text;
            CmdText = string.Empty;
            Paras = new SqlParameter[] { };
        }

        public SqlCommandForBatch(CommandType cmdType, string cmdText, SqlParameter[] paras)
        {
            CmdType = cmdType;
            CmdText = cmdText;
            Paras = paras;
        } 
    }


   

}
