using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop.Access.Dao;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using System.Collections;
using OfficeOpenXml;
using IntransitImport;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace IntransitImport
{
    public partial class UserControl1 : UserControl
    {
        #region ////以下定义OrBit插件与浏览器进行交互的接口变量

        //基本接口变量
        public string PlugInCommand;  //插件的命令ID
        public string PlugInName; //插件的名字(能随语言ID改变)
        public string LanguageId; //语言ID(0-英,1-简,2-繁...8)
        public string ParameterString; //参数字串
        public string RightString; //权限字串(1-Z)
        public string OrBitUserId; //OrBit用户ID
        public string OrBitUserName; //用户名
        public string ApplicationName; //应用系统
        public string ResourceId; //资源(电脑)ID
        public string ResourceName; //资源名
        public bool IsExitQuery; //在插件窗体退出是询问是否要退出，用于提醒数据状态已改变。
        public string UserTicket; //经浏览器认证后的加密票，调用某些WCF服务时需要使用

        //单独调试数据库应用时需用到参数
        public string DatabaseServer; //数据库服务器
        public string DatabaseName;//数据库名
        public string DatabaseUser;//数据库用户
        public string DatabasePassword; //密码

        //各服务器的地址
        public string WcfServerUrl; // WCF或Webservice服务的路径
        public string DocumentServerURL; //文档服务器URL
        public string PluginServerURL;//插件服务器URL
        public string RptReportServerURL; //水晶报表服务器URL

        //回传给浏览器的元对象信息
        public string MetaDataName = "No metadata"; //元对象名
        public string MetaDataVersion = "0.00"; //元对象版本
        public string UIMappingEngineVersion = "0.00"; //UIMapping版本号

        //事件日志类型枚举--1.普通事件，2警告，3错误，4严重错误 ,5表字段变更 ,6其它
        public enum EventLogType { Normal = 1, Warning = 2, Error = 3, FatalError = 4, TableChanged = 5, Other = 6 };

        #endregion

        #region ////以下定义OrBit插件与浏览器交互的接口函数

        /// <summary>
        /// 朗读文本
        /// </summary>
        /// <param name="text">语音内容</param>

        public void SpeechText(string text)
        { //朗读文本
            if (text.Trim() == "") return;
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("SpeechText", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { text });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 接收Winsock传过来的消息指令
        /// </summary>
        /// <param name="commandString">命令字串</param>
        public void WinsockMessageReceive(string msgString)
        {
            // this.ParentForm.Activate(); 
            MessageBox.Show(msgString);
        }

        /// <summary>
        /// 执行浏览器的命令
        /// </summary>
        /// <param name="command">命令ID</param>
        public void RunCommand(string command)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("RunCommand", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { command });
            }
            catch
            {

            }
        }
        /// <summary>
        /// 将键值写入OrBit的注册表
        /// </summary>
        /// <param name="ApplicationName">应用系统名</param>
        /// <param name="KeyName">键名</param>
        /// <param name="KeyValue">键值</param>
        public void WriteRegistryKey(string ApplicationName, string KeyName, string KeyValue)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("GetSetRegistryInfomation", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { ApplicationName + "." + KeyName, KeyValue, false });
            }
            catch
            {

            }
        }
        /// <summary>
        /// 从OrBit的注册表读取键值
        /// </summary>
        /// <param name="ApplicationName"></param>
        /// <param name="KeyName"></param>
        /// <returns></returns>
        public string ReadRegistryKey(string ApplicationName, string KeyName)
        {  //读取注册表

            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;

                return (string)type.InvokeMember("GetSetRegistryInfomation", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { ApplicationName + "." + KeyName, "", true });
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 采用OrBit-MES的WCF服务器上传文件
        public string UploadFile(string Filename, string SafeFileName, string UploadFilePath)
        {

            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;

                return (string)type.InvokeMember("UploadFile", BindingFlags.InvokeMethod | BindingFlags.Public |
                    BindingFlags.Instance, null, obj, new object[] { Filename, SafeFileName, UploadFilePath });
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        /// 在内置浏览器中打开一个WebURL
        /// </summary>
        /// <param name="URL"></param>
        public void OpenWebUrl(string URL)
        {
            try
            {
                //用反射的方法直接调用浏览器来浏览文件                
                Type type = this.ParentForm.GetType();
                Object obj = this.ParentForm;
                type.InvokeMember("OpenIEURL", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { URL });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将信息送往浏览器状态条
        /// </summary>
        /// <param name="Message">信息</param>
        public void SendToStatusBar(string Message)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("SendToStatusBar", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { Message });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 更改浏览器进度条
        /// </summary>
        /// <param name="Maximum">最大值</param>
        /// <param name="Value">当前值</param>        
        public void ChangeProgressBar(int Maximum, int Value)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("ChangeProgressBar", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, null, obj, new object[] { Maximum, Value });
            }
            catch
            {

            }
        }

        /// <summary>
        /// 写入事件日志-重载1
        /// </summary>
        /// <param name="eventLogType">1.普通事件，2警告，3错误，4严重错误 ,5表字段变更 ,6其它</param>
        /// <param name="eventLog">事件具体内容</param>
        public void WriteToEventLog(string eventLogType, string eventLog)
        {
            if (eventLogType == "1") WriteToEventLog(EventLogType.Normal, eventLog);
            if (eventLogType == "2") WriteToEventLog(EventLogType.Warning, eventLog);
            if (eventLogType == "3") WriteToEventLog(EventLogType.Error, eventLog);
            if (eventLogType == "4") WriteToEventLog(EventLogType.FatalError, eventLog);
            if (eventLogType == "5") WriteToEventLog(EventLogType.TableChanged, eventLog);
            if (eventLogType == "6") WriteToEventLog(EventLogType.Other, eventLog);
        }
        /// <summary>
        /// 写入事件日志-原型
        /// </summary>
        /// <param name="eventLogType">1.普通事件，2警告，3错误，4严重错误 ,5表字段变更 ,6其它</param>
        /// <param name="eventLog">事件具体内容</param>
        public void WriteToEventLog(EventLogType eventLogType, string eventLog)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                type.InvokeMember("WriteToEventLog", BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Instance, null, obj, new object[] { (int)eventLogType, this.ParentForm.Text, eventLog });
            }
            catch
            {

            }
        }

        //更新一个由客户端传回的记录集
        public bool UpdateDataSetBySQL(DataSet DataSetWithSQL, string SQLString)
        {
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                            ";Initial Catalog=" + DatabaseName +
                            ";password=" + DatabasePassword +
                            ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {

                        conn.ConnectionString = ConnectionString;
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(SQLString, conn);
                        SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(da);
                        da.Update(DataSetWithSQL.Tables[0]);
                        conn.Close();
                        return true;
                    }
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    bool resultR = true;
                    Object[] myArgs = new object[] { DataSetWithSQL, SQLString };
                    resultR = (bool)type.InvokeMember("UpdateDataSetBySQL", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    return resultR;

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //创建一个用于执行存储过程的结构
        public DataSet CreateIOParameterDataSet()
        {
            DataSet ds = new DataSet();//声明一个DataSet对象
            ds.Tables.Add(new DataTable()); //添加一个表
            ds.Tables[0].Columns.Add(new DataColumn("FieldName"));
            ds.Tables[0].Columns.Add(new DataColumn("FieldType"));
            ds.Tables[0].Columns.Add(new DataColumn("IsOutput"));
            ds.Tables[0].Columns.Add(new DataColumn("FieldValue"));

            return ds;
        }

        //创建执行存储过程的结构记录集的输入输出参数
        public void CreateIOParameter(DataSet IOParameterDataSet, string FieldName, string FieldType, bool IsOutput, string FieldValue)
        {

            if (FieldType.ToUpper() != "Bit".ToUpper() &&
                FieldType.ToUpper() != "DateTime".ToUpper() &&
                FieldType.ToUpper() != "Decimal".ToUpper() &&
                FieldType.ToUpper() != "Int".ToUpper() &&
                FieldType.ToUpper() != "NVarChar".ToUpper())
                FieldType = "NVarChar";

            bool isUpdate = false;
            for (int i = 0; i < IOParameterDataSet.Tables[0].Rows.Count; i++)
            {
                if (IOParameterDataSet.Tables[0].Rows[i]["FieldName"].ToString().Trim().ToUpper() == FieldName.Trim().ToUpper())
                {
                    IOParameterDataSet.Tables[0].Rows[i]["FieldType"] = FieldType.ToUpper();
                    IOParameterDataSet.Tables[0].Rows[i]["IsOutput"] = IsOutput;
                    IOParameterDataSet.Tables[0].Rows[i]["FieldValue"] = FieldValue;
                    isUpdate = true;
                    break;
                }

            }
            if (isUpdate == false)
            {
                DataRow dr = IOParameterDataSet.Tables[0].NewRow();//添加一个新行对象
                dr["FieldName"] = FieldName;
                dr["FieldType"] = FieldType;
                dr["IsOutput"] = IsOutput;
                dr["FieldValue"] = FieldValue;
                IOParameterDataSet.Tables[0].Rows.Add(dr);  //将新添加的行添加到表中            
            }
            IOParameterDataSet.AcceptChanges();
        }

        //取得输入输出参数的值
        public string GetIOParameterValue(DataSet IOParameterDataSet, string FieldName)
        {
            for (int i = 0; i < IOParameterDataSet.Tables[0].Rows.Count; i++)
            {
                if (IOParameterDataSet.Tables[0].Rows[i]["FieldName"].ToString().Trim().ToUpper() == FieldName.Trim().ToUpper())
                {
                    return IOParameterDataSet.Tables[0].Rows[i]["FieldValue"].ToString();
                }

            }
            return "";
        }

        //更新一个由客户端传回的记录集
        public DataSet ExecSP(string SpName, DataSet IOParameterDataSet, out int Return_value, out DataSet IOParameterDataSetReturn)
        {
            Return_value = -1;
            IOParameterDataSetReturn = null;
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    DataSet ds = execSPInner(SpName, IOParameterDataSet, out Return_value);
                    IOParameterDataSetReturn = IOParameterDataSet;
                    return ds;
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    DataSet resultR = new DataSet();
                    Object[] myArgs = new object[] { SpName, IOParameterDataSet, Return_value, IOParameterDataSetReturn };
                    resultR = (DataSet)type.InvokeMember("ExecSP", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    IOParameterDataSetReturn = (DataSet)myArgs[3];
                    Return_value = (int)myArgs[2];
                    return resultR;

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        //纯DataSet版本的存储过程执行接口
        private DataSet execSPInner(string SpName, DataSet IOParameterDataSet, out int Return_value)
        {
            SqlCommand cmdDs = new SqlCommand();
            DataSet ReturnDataSet = new DataSet();
            Return_value = 0;
            //将DataSet转化为SqlCommand
            if (IOParameterDataSet != null && IOParameterDataSet.Tables.Count > 0 && IOParameterDataSet.Tables[0].Columns.Count > 0)
            {
                cmdDs.CommandText = SpName;
                for (int i = 0; i < IOParameterDataSet.Tables[0].Rows.Count; i++)
                {
                    bool isOutput = false;
                    string paraName = IOParameterDataSet.Tables[0].Rows[i]["FieldName"].ToString();

                    if (IOParameterDataSet.Tables[0].Rows[i]["IsOutput"].ToString().ToUpper().Trim() == "TRUE")
                    {
                        isOutput = true;
                    }

                    string dataType = IOParameterDataSet.Tables[0].Rows[i]["FieldType"].ToString();

                    if (dataType.ToUpper() == "Bit".ToUpper())
                        cmdDs.Parameters.Add(new SqlParameter(paraName, SqlDbType.Bit));
                    if (dataType.ToUpper() == "DateTime".ToUpper())
                        cmdDs.Parameters.Add(new SqlParameter(paraName, SqlDbType.DateTime));
                    if (dataType.ToUpper() == "Decimal".ToUpper())
                        cmdDs.Parameters.Add(new SqlParameter(paraName, SqlDbType.Decimal));
                    if (dataType.ToUpper() == "Int".ToUpper())
                        cmdDs.Parameters.Add(new SqlParameter(paraName, SqlDbType.Int));
                    if (dataType.ToUpper() == "NVarChar".ToUpper())
                        cmdDs.Parameters.Add(new SqlParameter(paraName, SqlDbType.NVarChar, 1000));


                    cmdDs.Parameters[paraName].Value = IOParameterDataSet.Tables[0].Rows[i]["FieldValue"];
                    if (isOutput == true) cmdDs.Parameters[paraName].Direction = ParameterDirection.Output;


                }

                cmdDs.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                cmdDs.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;


                // 执行SqlCommand

                string ConnectionString = "Data Source=" + DatabaseServer +
                    ";Initial Catalog=" + DatabaseName +
                    ";password=" + DatabasePassword +
                    ";Persist Security Info=True;User ID=" + DatabaseUser;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                conn.Open();

                cmdDs.CommandTimeout = conn.ConnectionTimeout;
                cmdDs.Connection = conn;
                cmdDs.CommandType = CommandType.StoredProcedure;


                DataSet ds = new DataSet("WCFSQLDataSet");
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmdDs;
                adapter.Fill(ds, "WCFSQLDataSet");
                ReturnDataSet = ds;
                conn.Close();

                //将返回值类型的参数送回到DataSet
                for (int i = 0; i < cmdDs.Parameters.Count; i++)
                {
                    if (cmdDs.Parameters[i].Direction.ToString() == "Output")
                    {
                        for (int ii = 0; ii < IOParameterDataSet.Tables[0].Rows.Count; ii++)
                        {

                            if (IOParameterDataSet.Tables[0].Rows[ii]["IsOutput"].ToString().ToUpper().Trim() == "TRUE" &&
                                IOParameterDataSet.Tables[0].Rows[ii]["FieldName"].ToString().ToUpper() == cmdDs.Parameters[i].ToString().ToUpper())
                            {
                                IOParameterDataSet.Tables[0].Rows[ii]["FieldValue"] = cmdDs.Parameters[i].Value;
                            }
                        }

                    }

                    if (cmdDs.Parameters[i].Direction.ToString() == "ReturnValue")
                    {
                        Return_value = (int)cmdDs.Parameters["RETURN_VALUE"].Value;
                    }
                }
                return ReturnDataSet;
            }

            return null;
        }


        /// <summary>
        /// 通用执行存储过程程序.
        /// </summary>
        /// <param name="SQLCmd">传入的SqlCommand对象</param>
        /// <param name="ReturnDataSet">执行存储过程后返回的数据集</param>
        /// <param name="ReturnValue">执行存储过程的返回值</param>
        /// <returns>将SQLCmd执行后的参数刷新并传回，主要返回存储过程中的out参数</returns>
        public SqlCommand RunStoredProcedure(SqlCommand SQLCmd, out DataSet ReturnDataSet, out int ReturnValue)
        {
            ReturnValue = 0;
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                            ";Initial Catalog=" + DatabaseName +
                            ";password=" + DatabasePassword +
                            ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SQLCmd.Connection = conn;
                        SQLCmd.CommandType = CommandType.StoredProcedure;
                        SQLCmd.CommandTimeout = conn.ConnectionTimeout;
                        SQLCmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                        SQLCmd.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = SQLCmd;

                        DataSet ds = new DataSet("WCFSQLDataSet");
                        adapter.Fill(ds, "WCFSQLDataSet");

                        ReturnDataSet = ds;
                        conn.Close();
                        ReturnValue = (int)SQLCmd.Parameters["RETURN_VALUE"].Value;
                        return SQLCmd;
                    }
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;

                    SqlCommand cmd = new SqlCommand();
                    DataSet rds = new DataSet();

                    ReturnDataSet = null;

                    Object[] myArgs = new object[] { SQLCmd, ReturnDataSet, ReturnValue };
                    cmd = (SqlCommand)type.InvokeMember("RunStoredProcedure", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    ReturnValue = (int)myArgs[2];
                    ReturnDataSet = (DataSet)myArgs[1];
                    return cmd;

                }
            }
            catch (Exception er)
            {
                ReturnDataSet = null;
                MessageBox.Show(er.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /// <summary>
        /// 通用执行存储过程程序，并支侍向存储过程上传数据集,需SQL-Server 2008对表值类型传入参数的支持
        /// </summary>
        /// <param name="SQLCmd">传入的SqlCommand对象</param>
        /// <param name="UserDataSet">用于上传数据的UserDataSet</param>
        /// <param name="ReturnDataSet">执行存储过程后返回的数据集</param>
        /// <param name="ReturnValue">执行存储过程的返回值</param>
        /// <returns>将SQLCmd执行后的参数刷新并传回，主要返回存储过程中的out参数</returns>
        public SqlCommand RunSPUploadDataSet(SqlCommand SQLCmd, DataSet UserDataSet, out DataSet ReturnDataSet, out int ReturnValue)
        {
            ReturnValue = 0;
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                            ";Initial Catalog=" + DatabaseName +
                            ";password=" + DatabasePassword +
                            ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SQLCmd.Connection = conn;
                        SQLCmd.CommandType = CommandType.StoredProcedure;
                        SQLCmd.CommandTimeout = conn.ConnectionTimeout;
                        SQLCmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
                        SQLCmd.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;

                        //处理一次性从客户端传过来的DataSet，并将它的Table名作为参数名使用
                        if (UserDataSet != null && UserDataSet.Tables.Count > 0)
                        {
                            for (int k = 0; k < UserDataSet.Tables.Count; k++)
                            {
                                string tableName = UserDataSet.Tables[k].TableName.ToString().Trim();
                                if (tableName != "")
                                {
                                    SQLCmd.Parameters.AddWithValue("@" + tableName, UserDataSet.Tables[k]);
                                }
                            }
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = SQLCmd;

                        DataSet ds = new DataSet("WCFSQLDataSet");
                        adapter.Fill(ds, "WCFSQLDataSet");

                        ReturnDataSet = ds;
                        conn.Close();
                        ReturnValue = (int)SQLCmd.Parameters["RETURN_VALUE"].Value;
                        return SQLCmd;
                    }
                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;

                    SqlCommand cmd = new SqlCommand();
                    DataSet rds = new DataSet();

                    ReturnDataSet = null;

                    Object[] myArgs = new object[] { SQLCmd, UserDataSet, ReturnDataSet, ReturnValue };
                    cmd = (SqlCommand)type.InvokeMember("RunSPUploadDataSet", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    ReturnValue = (int)myArgs[3];
                    ReturnDataSet = (DataSet)myArgs[2];
                    return cmd;

                }
            }
            catch (Exception er)
            {
                ReturnDataSet = null;
                MessageBox.Show(er.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /// <summary>
        /// //为插件提供可以任意一个WCF的地址上直接调用任意存储过程的接口,并支持上传数据集UserDataSet。
        /// </summary>
        /// <param name="WCFUrl">一个任意的WCFUrl地址，需要完整的写出*.svc</param>
        /// <param name="SQLCmd">传入的SqlCommand对象</param>
        /// <param name="UserDataSet">用于上传数据的UserDataSet</param>
        /// <param name="ReturnDataSet">执行存储过程后返回的数据集</param>
        /// <param name="ReturnValue">执行存储过程的返回值</param>
        /// <returns>将SQLCmd执行后的参数刷新并传回，主要返回存储过程中的out参数</returns>
        public SqlCommand RunSPUploadDataSetByWCFUrl(string WCFUrl, SqlCommand SQLCmd, DataSet UserDataSet, out DataSet ReturnDataSet, out int ReturnValue)
        {
            ReturnValue = 0;
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    MessageBox.Show("本函数必须在OrBit浏览器环境下执行！");
                    ReturnDataSet = null;
                    return null;

                }
                else
                {  //在浏览器下运行时，直接调用浏览器的RunStoredProcedure,

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;

                    SqlCommand cmd = new SqlCommand();
                    DataSet rds = new DataSet();

                    ReturnDataSet = null;

                    Object[] myArgs = new object[] { WCFUrl, SQLCmd, UserDataSet, ReturnDataSet, ReturnValue };
                    cmd = (SqlCommand)type.InvokeMember("RunSPUploadDataSetByWCFUrl", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, myArgs);
                    ReturnValue = (int)myArgs[4];
                    ReturnDataSet = (DataSet)myArgs[3];
                    return cmd;

                }
            }
            catch (Exception er)
            {
                ReturnDataSet = null;
                MessageBox.Show(er.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        /// <summary>
        /// 执行一个指定的SQL字串，并返回一个记录集
        /// 在浏览器下执行时，直接调用浏览器的WCF服务器来传送记录集
        /// </summary>
        /// <param name="SQLString">SQL字串</param>
        /// <returns>返回的记录集</returns>
        public DataSet GetDataSetWithSQLString(string SQLString)
        {
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                                        ";Initial Catalog=" + DatabaseName +
                                        ";password=" + DatabasePassword +
                                        ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = SQLString;

                        comm.CommandType = CommandType.Text;
                        comm.CommandTimeout = conn.ConnectionTimeout;

                        DataSet ds = new DataSet("SQLDataSet");
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = comm;
                        adapter.Fill(ds, "SQLDataSet");

                        conn.Close();
                        return ds;
                    }
                }
                else
                {　//在浏览器下运行时，直接调用浏览器的GetDataSetWithSQLString,
                    //通过WCF服务器返回记录集

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    DataSet ds = new DataSet("SQLDataSet");
                    ds = (DataSet)type.InvokeMember("GetDataSetWithSQLString", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, new object[] { SQLString });
                    return ds;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// 执行一个指定的SQL字串，并从OLAP服务器上返回一个记录集
        /// 在浏览器下执行时，直接调用浏览器的WCF服务器来传送记录集
        /// </summary>
        /// <param name="SQLString">SQL字串</param>
        /// <returns>返回的记录集</returns>
        public DataSet GetDataSetWithSQLStringFromOLAP(string SQLString)
        {
            try
            {
                if (this.Parent.Name.ToString() != "FormPlugIn")
                {　//在插件调试环境下运行时，用ADO.NET直连 
                    string ConnectionString = "Data Source=" + DatabaseServer +
                                        ";Initial Catalog=" + DatabaseName +
                                        ";password=" + DatabasePassword +
                                        ";Persist Security Info=True;User ID=" + DatabaseUser;
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = SQLString;

                        comm.CommandType = CommandType.Text;
                        comm.CommandTimeout = conn.ConnectionTimeout;

                        DataSet ds = new DataSet("SQLDataSet");
                        SqlDataAdapter adapter = new SqlDataAdapter();


                        adapter.SelectCommand = comm;
                        adapter.Fill(ds, "SQLDataSet");



                        conn.Close();
                        return ds;
                    }
                }
                else
                {　//在浏览器下运行时，直接调用浏览器的GetDataSetWithSQLString,
                    //通过WCF服务器返回记录集

                    Type type = this.ParentForm.GetType();
                    Object obj = this.ParentForm;
                    DataSet ds = new DataSet("SQLDataSet");
                    ds = (DataSet)type.InvokeMember("GetDataSetWithSQLStringFromOLAP", BindingFlags.InvokeMethod | BindingFlags.Public |
                                    BindingFlags.Instance, null, obj, new object[] { SQLString });
                    return ds;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), this.ParentForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        #region // GetUIText为浏览器提供的语言切换函数，可以用它返回控件或提示信息不同语种的内容。
        // GetUIText提供了几种形式的重载，以方便使用

        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载4
        /// </summary>
        /// <param name="module">模块名</param>
        /// <returns></returns>
        public String GetUIText(string module)
        {
            string s = GetUIText("[Public Text]", module, "", "").Trim();
            if (s == string.Empty)
            {
                return module;
            }
            else
            {
                return s;
            }

        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载3
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string module, string defaultText)
        {
            string s = GetUIText("[Public Text]", module, "", defaultText).Trim();
            if (s == string.Empty)
            {
                return defaultText;
            }
            else
            {
                return s;
            }
        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载2
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="control">控件对象</param>
        /// <returns></returns>
        public string GetUIText(string module, Control control)
        {
            string s = GetUIText(this.ParentForm.Text, module, control.Name, control.Text);
            if (s == string.Empty)
            {
                return control.Text;
            }
            else
            {
                return s;
            }

        }
        /// <summary>
        /// 返回控件或提示信息不同语种的内容-重载1
        /// </summary>
        /// <param name="module">模块名</param>
        /// <param name="controlName">控件名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string module, string controlName, string defaultText)
        {
            string s = GetUIText(this.ParentForm.Text, controlName, defaultText);
            if (s == string.Empty)
            {
                return defaultText;
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// 返回控件或提示信息不同语种的内容-基本型式
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <param name="module">模块名</param>
        /// <param name="controlName">控件名</param>
        /// <param name="defaultText">默认文本,必须是英文</param>
        /// <returns></returns>
        public string GetUIText(string owner, string module, string controlName, string defaultText)
        {
            try
            {
                Type type = this.ParentForm.GetType();
                //调用没有返回值的方法
                Object obj = this.ParentForm;
                string s = (string)type.InvokeMember("GetUIText", BindingFlags.InvokeMethod | BindingFlags.Public |
                                BindingFlags.Instance, null, obj, new object[] { owner, module, controlName, defaultText });
                if (s != null)
                {
                    return s;
                }
                else
                {
                    return defaultText;
                }
            }
            catch
            {
                return defaultText;
            }

        }
        #endregion

        #endregion

        /// <summary>
        /// 本私有函数对插件各接口变量进行初始化，赋予默认值
        /// 调试环境下这些值不变，通过浏览器执行时，
        /// 这些变量将会根据系统环境被重新赋值。
        /// </summary>
        private void initializeVariable()
        {
            PlugInCommand = "TestAndonProblem";
            PlugInName = "Andon故障上报插件";
            LanguageId = "0";  //(0-英,1-简,2-繁...8)
            ParameterString = "";
            RightString = "(0)";

            OrBitUserId = "DEVUSER";
            OrBitUserName = "调试者";
            ApplicationName = "DEBUG";
            ResourceId = "RES0000XXX";
            ResourceName = "YourPC";

            //这里需要根据实际的数据库环境进行改写
            DatabaseServer = "192.168.124.72";
            DatabaseName = "OrBitXI";
            DatabaseUser = "sa";
            DatabasePassword = "admin@2017";

            WcfServerUrl = "http://localhost/BrowserWCFService";
            DocumentServerURL = ""; //文档服务器URL
            PluginServerURL = "http://henryx61/Plug-in/";//插件服务器URL
            RptReportServerURL = "http://henryx61/RptExamples/"; //水晶报表服务器URL

            UserTicket = "";
            IsExitQuery = false;
        }

        public void SwitchUI()
        {

        }
        public UserControl1()
        {
            InitializeComponent();
            initializeVariable();
        }



        /// <summary>
        /// 下载选中订单的Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void 下载Excel文件_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //timer1.Enabled = false;
        //        string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        //        string Name = null;
        //        string ProductNo = null;
        //        Name = this.dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
        //        ProductNo = this.dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
        //        string ProductOrderId = this.dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
        //        if (Name.Length > 3)
        //        {
        //            Name = Name.Substring(0, 3);
        //        }
        //        //获取Excel文件上传日期
        //        SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;Persist Security Info=True;User ID=sa;Password=admin@2017");
        //        con.Open();
        //        SqlCommand comm = new SqlCommand(string.Format("select Ctime from IssueMaterialBills where ProductOrderId='{0}'", ProductOrderId), con);
        //        object Ctime = comm.ExecuteScalar();
        //        if (Ctime == null)
        //        {
        //            MessageBox.Show("未查询到相关订单的导入信息！");
        //        }
        //        else
        //        {
        //            string YearPath = Convert.ToDateTime(Ctime).ToString("yyyy") + "年";
        //            string MonthPath = Convert.ToDateTime(Ctime).ToString("MM") + "月";

        //            string DatePath = Convert.ToDateTime(Ctime).ToString("yyyy-MM-dd");
        //            string ExcelName = Name + "*" + ProductNo + "*.xlsx";
        //            string FilePathOnServer = "////192.168.124.66//D$//FBExcel//" + YearPath + "//" + MonthPath + "//" + DatePath + "//";
        //            DirectoryInfo FileName1 = new DirectoryInfo(@FilePathOnServer);
        //            if (FileName1.Exists)
        //            {
        //                string[] files = Directory.GetFiles(@FilePathOnServer, ExcelName);
        //                if (files.Length > 0)
        //                {
        //                    DirectoryInfo FileName = new DirectoryInfo(@dir);
        //                    if (!FileName.Exists)
        //                    {
        //                        FileName.Create();
        //                    }
        //                    files.ToList().ForEach(x => File.Copy(x, Path.Combine(FileName.ToString(), Path.GetFileName(x)), true));
        //                    for (int i = 0; i < files.ToList().Count; i++)
        //                    {
        //                        string DelPath = Path.GetFullPath(files[i].ToString());
        //                        if (DelPath.Contains(ProductNo))
        //                        {
        //                            File.Delete(DelPath);
        //                        }
        //                    }
        //                    MessageBox.Show("下载成功，文件保存路径为：" + dir);
        //                }
        //                else
        //                {
        //                    MessageBox.Show("未找到此订单的Excel文件");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("未找到此订单的Excel文件");
        //            }
        //        }
        //        //timer1.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        private void btnImport_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

            //获取订单ID
            //string ParentId = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();


            //return;
            string Path = this.txtFilePath.Text.Trim();
            if (string.IsNullOrEmpty(Path) || Path == "")
            {
                MessageBox.Show("导入的文件路径为空，请选择正确的文件路径！");
                return;
            }
            if (System.IO.File.Exists(Path) == true)
            {


                string fileSuffix = System.IO.Path.GetExtension(Path);
                if (string.IsNullOrEmpty(fileSuffix))
                    return;
                FileStream FS = new FileStream(Path, FileMode.Open, FileAccess.Read);
                using (ExcelPackage pck = new ExcelPackage(FS))
                {
                    //柜体Sheet页
                    try
                    {
                        ExcelWorksheet sheet = null;
                        sheet = pck.Workbook.Worksheets["Sheet1"];
                        if (sheet.Hidden.ToString() == "Visible")
                        {
                            string hidden = sheet.Hidden.ToString();
                            int maxColumnNum = sheet.Dimension.End.Column;//最大列
                            int minColumnNum = sheet.Dimension.Start.Column;//最小列
                            int minRowNum = sheet.Dimension.Start.Row;//最小行
                            int maxRowNum = sheet.Dimension.End.Row;//最大行
                            int sRowNum = 3;//开始行
                            int eRowNum = 0;//结束行
                            if (Convert.ToString(sheet.Hidden) != "Hidden")
                            {
                                for (int i = sRowNum; i <= maxRowNum+1; i++)
                                {
                                    string EndRow = Convert.ToString(sheet.Cells[i, 1].Value);
                                    if (EndRow.Length == 0)
                                    {
                                        eRowNum = i;
                                        break;
                                    }
                                }
                                //string Con = "Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017";
                                DataTable Table1 = new DataTable();
                                Table1.Columns.Add("PlanDate",Type.GetType("System.Datetime"));//计划日期
                                Table1.Columns.Add("VendorDescription");//供应商
                                Table1.Columns.Add("ProductName");//物料编码
                                Table1.Columns.Add("ProductDescription");//材料名称
                                Table1.Columns.Add("PurchaseUnit");//单位
                                Table1.Columns.Add("PurchaseCycle");//采购周期
                                Table1.Columns.Add("PurchaseQty");//订购数量
                                Table1.Columns.Add("PurchaseApplyId");//客户名称与单号
                                Table1.Columns.Add("DeliveryAddress");//送货地址
                                Table1.Columns.Add("ExamineGoods");//下单+检验
                                Table1.Columns.Add("RequestArriveDate", Type.GetType("System.Datetime"));//应到货日期
                                Table1.Columns.Add("PredictArrivalDate", Type.GetType("System.Datetime"));////预计到货日期
                                Table1.Columns.Add("Agent");//承办采购员
                                Table1.Columns.Add("ActualArrivalDate", Type.GetType("System.Datetime"));//实际到货日期
                                Table1.Columns.Add("InboundDate", Type.GetType("System.Datetime"));//入库日期
                                Table1.Columns.Add("TotalQuantity");//入库数量
                                Table1.Columns.Add("SalesReturnNumber");//未到货数量
                                Table1.Columns.Add("Remark");//异常情况（备注）
                                for (int i = sRowNum; i < eRowNum; i++)
                                {
                                    DataRow GuitiIssueMaterialBillsDataTableRow = Table1.NewRow();
                                    object IsNull = sheet.GetValue(i, 1);//单元格数据
                                    if (IsNull == null || IsNull.ToString() == "")
                                    {
                                        continue;
                                    }
                                    for (int j = 1; j < 28; j++)
                                    {
                                        ExcelRange Rang = sheet.Cells[i, j];
                                        object objectCellValue = sheet.GetValue(i, j);//单元格数据
                                        string CellValue = objectCellValue != null ? objectCellValue.ToString().Trim() : "";
                                        if (Rang.Merge)
                                        {
                                            CellValue = GetMergeValue(sheet, i, j);
                                        }
                                        switch (j)
                                        {
                                            case 1:
                                                GuitiIssueMaterialBillsDataTableRow["PlanDate"] = CellValue;
                                                break;
                                            case 2:
                                                GuitiIssueMaterialBillsDataTableRow["VendorDescription"] = CellValue;
                                                break;
                                            case 3:
                                                GuitiIssueMaterialBillsDataTableRow["ProductName"] = CellValue;

                                                break;
                                            case 4:
                                                GuitiIssueMaterialBillsDataTableRow["ProductDescription"] = CellValue;
                                                break;
                                            case 5:
                                                GuitiIssueMaterialBillsDataTableRow["PurchaseUnit"] = CellValue;
                                                break;
                                            case 6:
                                                GuitiIssueMaterialBillsDataTableRow["PurchaseCycle"] = CellValue;
                                                break;
                                            case 7:
                                                GuitiIssueMaterialBillsDataTableRow["PurchaseQty"] = CellValue;
                                                break;
                                            case 8:
                                                GuitiIssueMaterialBillsDataTableRow["PurchaseApplyId"] = CellValue;
                                                break;
                                            case 9:
                                                GuitiIssueMaterialBillsDataTableRow["DeliveryAddress"] = CellValue;
                                                break;
                                            case 10:
                                                GuitiIssueMaterialBillsDataTableRow["ExamineGoods"] = CellValue;
                                                break;
                                            case 11:
                                                GuitiIssueMaterialBillsDataTableRow["RequestArriveDate"] = CellValue;
                                                break;
                                            case 12:
                                                GuitiIssueMaterialBillsDataTableRow["PredictArrivalDate"] = CellValue;
                                                break;
                                            case 13:
                                                GuitiIssueMaterialBillsDataTableRow["Agent"] = CellValue;
                                                break;
                                            case 14:
                                                GuitiIssueMaterialBillsDataTableRow["ActualArrivalDate"] = CellValue;
                                                break;
                                            case 15:
                                                GuitiIssueMaterialBillsDataTableRow["ReportedToDate"] = CellValue;
                                                break;
                                            case 16:
                                                GuitiIssueMaterialBillsDataTableRow["ReportedDescription"] = CellValue;
                                                break;
                                            case 17:
                                                GuitiIssueMaterialBillsDataTableRow["ProcessingCycle"] = CellValue;
                                                break;
                                            case 18:
                                                GuitiIssueMaterialBillsDataTableRow["DueDate"] = CellValue;
                                                break;
                                            case 19:
                                                GuitiIssueMaterialBillsDataTableRow["VendorDescription1"] = CellValue;
                                                break;
                                            case 20:
                                                GuitiIssueMaterialBillsDataTableRow["TheDateOfDelivery"] = CellValue;
                                                break;
                                             case 21:
                                                GuitiIssueMaterialBillsDataTableRow["ThePromoter"] = CellValue;
                                                break;
                                            case 22:
                                                GuitiIssueMaterialBillsDataTableRow["ActualCompletionDate"] = CellValue;
                                                break;
                                            case 23:
                                                GuitiIssueMaterialBillsDataTableRow["ActualCompletionCycle"] = CellValue;
                                                break;
                                            case 24:
                                                GuitiIssueMaterialBillsDataTableRow["InboundDate"] = CellValue;
                                                break;
                                            case 25:
                                                GuitiIssueMaterialBillsDataTableRow["TotalQuantity"] = CellValue;
                                                break;
                                            case 26:
                                                GuitiIssueMaterialBillsDataTableRow["SalesReturnNumber"] = CellValue;
                                                break;
                                            case 27:
                                                GuitiIssueMaterialBillsDataTableRow["Remark"] = CellValue;
                                                break;
                                            //case 19:
                                            //    GuitiIssueMaterialBillsDataTableRow["Remark"] = CellValue;
                                            //    break;
                                        }
                                    }
                                    Table1.Rows.Add(GuitiIssueMaterialBillsDataTableRow);

                                }
                                DialogResult ResultYes = MessageBox.Show("是否导入？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                if (ResultYes == DialogResult.OK)
                                {
                                    SqlConnection Con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                                    Con.Open();
                                    SqlBulkCopy TableCopy = new SqlBulkCopy(Con);
                                    TableCopy.DestinationTableName = "PurchaseDetails";
                                    TableCopy.BatchSize = Table1.Rows.Count;
                                    for (int j = 0; j < Table1.Columns.Count; j++)
                                    {
                                        ////						MessageBox.Show(Table1.Columns[j].ColumnName.ToString());
                                        TableCopy.ColumnMappings.Add(Table1.Columns[j].ColumnName, Table1.Columns[j].ColumnName);
                                    }
                                    ////				}

                                    TableCopy.WriteToServer(Table1);
                                    Con.Close();
                                    MessageBox.Show("导入成功！！！");
                                    //Scripter.SetParameterDataSet("DeliveryPlanShow",DS);
                                }
                                this.dataGridView1.DataSource = Table1;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                //导入之前检查之前是否导入过，有就删除，重新导入
                //SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;Persist Security Info=True;User ID=sa;Password=admin@2017");
                //con.Open();
                //SqlCommand com = new SqlCommand("Del_FBData", con);
                //com.CommandType = CommandType.StoredProcedure;
                //com.Parameters.Add("@ParentId", SqlDbType.NVarChar).Value = ParentId;
                //com.ExecuteNonQuery();


                ////C:\Users\91307\Desktop\刁燕杰S300160693 - 副本.xlsx
                ////验证订单与Excel单号是否是同一个单子
                //string ProductNo = Path.Substring(Path.LastIndexOf("\\") + 1);
                //ProductNo = Regex.Replace(ProductNo, @"[^0-9|a-z|A-Z]+", "");
                //ProductNo = ProductNo.Substring(0, ProductNo.Length - 4);

                //SqlCommand comm = new SqlCommand();
                //string SQL = string.Format("select ProductOrderId from ProductOrder where ProductOrderNO='{0}'", ProductNo);
                //comm.Connection = con;
                //comm.CommandText = SQL;
                //object ProductOrderNO = comm.ExecuteScalar();
                //if (ProductOrderNO != null)
                //{
                //    if (ProductOrderNO.ToString() == ParentId)
                //    {
                //        ImportExcelData(Path, ParentId);
                //    }
                //    else
                //    {
                //        MessageBox.Show("所选订单单号与Excel单号不匹配，请重新检查", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                //    }
                //}

                //UpdateCheckSkirtingBoard(Path, ParentId);
                //GetDataFromExcelToSQL(Path, ParentId);
                //LingLiaoDanToSQL(Path, ParentId);
                //NoPaintDoorToSQL(Path,ParentId);
                //WenqiOrderMaterialToSQL(Path, ParentId);
                //SuctionPlasticSheetToSQL(Path, ParentId);
                //无毒柜体数据导入数据库
                //Non_toxicSeriesOfWorkOrders(Path, ParentId);
                //AluminiumSheetGlass(Path, ParentId);
                //QuickBeautyFreePaintDoorSheet(Path,ParentId);
                //QuickBeautyFreePaintSheet(Path,ParentId);
                //PackagingToSQL(Path, ParentId);
                //SpeedThePackingToSQL(Path, ParentId);
            }
            else
            {
                MessageBox.Show("导入的文件不存在，请选择正确的文件！");
                return;
            }
        }


        //   //判断单元格是否是合并单元格并获取值
        //                                  ExcelRange Rang = Sheet2.Cells[i, j];
        //                                  object objectCellValue = Sheet2.GetValue(i, j);//单元格数据
        //                                  string CellValue = objectCellValue != null ? objectCellValue.ToString().Trim() : "";
        //                                  if (Rang.Merge)
        //                                  {
        //                                      CellValue = GetMergeValue(Sheet2, i, j);
        //                                  }
        /// <summary>
        /// 判断合并单元格获取值
        /// </summary>
        /// <param name="worksheet"></param>
        /// <param name="row">单元格行</param>
        /// <param name="column">单元格列</param>
        /// <returns></returns>
        public static string GetMergeValue(ExcelWorksheet worksheet, int row, int column)
        {
            string MergeValue = "";
            ExcelWorksheet.MergeCellsCollection<string> MergeCells = worksheet.MergedCells;
            for (int i = 0; i < MergeCells.Count; i++)
            {
                string range = MergeCells[i];
                ExcelAddress add = new ExcelAddress(range);
                int srow = add.Start.Row;//合并单元格开始行
                int scol = add.Start.Column;//合并单元格开始行
                int erow = add.End.Row;//合并单元格结束行
                int ecol = add.End.Column;//合并单元格结束行
                object value = worksheet.Cells[(new ExcelAddress(range)).Start.Row, (new ExcelAddress(range)).Start.Column].Value;
                if (row >= srow && row <= erow && column >= scol && column <= ecol)
                {
                    if (value == null || value.ToString() == "")
                    {
                        MergeValue = "";
                    }
                    else
                    {
                        MergeValue = value.ToString();
                    }
                    break;
                }
            }
            return MergeValue;
        }


        /// <summary>
        /// 修改踢脚板数据
        /// </summary>
        public void UpdateCheckSkirtingBoard(string path, string ParentId)
        {
            FileStream FS = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (ExcelPackage pk = new ExcelPackage(FS))
            {
                ExcelWorksheet sheet = null;
                sheet = pk.Workbook.Worksheets["Sheet1"];
                int MaxsheetRow = sheet.Dimension.End.Row;
                int StartsheetRow = sheet.Dimension.Start.Row;
                int MaxsheetColum = sheet.Dimension.End.Column;
                int StartsheetColum = sheet.Dimension.Start.Column;

                int MaxRow = 0;
                int StartRow = 8;
                int MaxColum = 13;
                int StartColum = 1;

                ParentId = sheet.Cells[5, 1].Value.ToString().Substring(7, 10);

                for (int i = StartRow; i <= 50; i++)
                {
                    object EndRow = sheet.Cells[i, 1].Value;
                    if (EndRow == null)
                    {
                        continue;
                    }
                    if (EndRow.ToString() == "结束")
                    {
                        MaxRow = i;
                        break;
                    }
                }



                //Excel数据存放表
                DataTable GlassTable = new DataTable();
                GlassTable.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("OrdId"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("OlnId"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("ItmId"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
                GlassTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
                GlassTable.Columns.Add("Qty", typeof(float));
                GlassTable.Columns.Add("Ctime", typeof(DateTime));
                SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                con.Open();
                SqlCommand com = new SqlCommand();
                com.CommandText = "GetGlassTable";
                com.Connection = con;
                //com.Parameters.Add("@OrderDetailId",ParentId);
                com.CommandType = CommandType.StoredProcedure;
                SqlParameter paremeters = new SqlParameter("@OrderDetailId", SqlDbType.NVarChar);
                paremeters.Value = ParentId;
                com.Parameters.Add(paremeters);
                //获取到存储过程返回的参数
                com.Parameters.Add(new SqlParameter("@return", SqlDbType.Int));
                com.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
                SqlDataReader reader = com.ExecuteReader();

                //数据库读取数据存放表
                DataTable GlassIssTable = new DataTable();
                GlassIssTable.Load(reader);
                reader.Close();
                //string Word_Number = "0123456789";
                //string Word_S = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                //string Word_s = Word_S.ToLower();
                int a = 1;
                for (int i = StartRow; i < MaxRow; i++)
                {
                    if (sheet.Cells[i, 3].Value == null)
                    {
                        continue;
                    }
                    string str = Guid.NewGuid().ToString().Substring(0, 12);

                    if (sheet.Cells[i, 3].Value.ToString().Contains("铝踢脚板"))
                    {
                        DataRow GlassTableRow = GlassTable.NewRow();
                        if (a > GlassIssTable.Rows.Count)
                        {

                            //string GlassIssueMaterialBillsId=  GlassIssTable.Rows[a-2]["GlassIssueMaterialBillsId"].ToString();
                            //string GlassIssueMaterialBillsId1= GlassIssueMaterialBillsId.Substring(11, 1);
                            //if (Word_Number.Contains(GlassIssueMaterialBillsId1))
                            //{
                            //    GlassIssueMaterialBillsId1 =(Convert.ToInt32( GlassIssueMaterialBillsId1) + 1).ToString();
                            //}
                            //else
                            //{
                            //    int s = Word_S.IndexOf(GlassIssueMaterialBillsId1);
                            //    string NewGlassIssueMaterialBillsId1 = Word_S.Substring(s + 1, 1);
                            //    GlassIssueMaterialBillsId1 = GlassIssueMaterialBillsId.Substring(0, 11) + NewGlassIssueMaterialBillsId1;
                            //}

                            GlassTableRow["GlassIssueMaterialBillsId"] = str.ToUpper();
                            GlassTableRow["ProductOrderId"] = GlassIssTable.Rows[a - 2]["ProductOrderId"].ToString();
                            GlassTableRow["OrdId"] = GlassIssTable.Rows[a - 2]["OrdId"].ToString();
                            GlassTableRow["OlnId"] = null;
                            GlassTableRow["ItmId"] = null;
                            GlassTableRow["Ctime"] = DateTime.Now.ToString();
                        }
                        else
                        {
                            a = a - 1;
                            GlassTableRow["GlassIssueMaterialBillsId"] = GlassIssTable.Rows[a]["GlassIssueMaterialBillsId"].ToString();
                            GlassTableRow["ProductOrderId"] = GlassIssTable.Rows[a]["ProductOrderId"].ToString();
                            GlassTableRow["OrdId"] = GlassIssTable.Rows[a]["OrdId"].ToString();
                            GlassTableRow["OlnId"] = GlassIssTable.Rows[a]["OlnId"].ToString();
                            GlassTableRow["ItmId"] = GlassIssTable.Rows[a]["ItmId"].ToString();
                            GlassTableRow["Ctime"] = DateTime.Now.ToString();
                            a = a + 1;
                        }
                        for (int j = StartColum; j < MaxColum; j++)
                        {
                            switch (j)
                            {
                                case 3:
                                    GlassTableRow["MaterialName"] = sheet.Cells[i, j].Value;
                                    break;
                                case 7:
                                    GlassTableRow["StandardsName"] = sheet.Cells[i, j].Value.ToString().Trim();
                                    break;
                                case 10:
                                    GlassTableRow["Unit"] = sheet.Cells[i, j].Value;
                                    break;
                                case 11:
                                    GlassTableRow["Qty"] = sheet.Cells[i, j].Value;
                                    break;
                            }
                        }
                        GlassTable.Rows.Add(GlassTableRow);
                        a++;
                    }
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UpdateGlassTable";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] para = new SqlParameter[]{
                        new SqlParameter("@ProductOrderNO",SqlDbType.Char)
                                {
                                    Value=ParentId
                                },
                                new SqlParameter("@Pro_UpdateGlassIssueMaterialBills",SqlDbType.Structured)
                                {
                                    Value=GlassTable
                                }
                                
                             };
                cmd.Parameters.AddRange(para);
                cmd.ExecuteNonQuery();
                ////获取到存储过程返回的参数
                cmd.Parameters.Add(new SqlParameter("@return", SqlDbType.Int));
                cmd.Parameters["@return"].Direction = ParameterDirection.ReturnValue;
                con.Close();
                if (Convert.ToInt32(cmd.Parameters["@return"].Value) == 0)
                {
                    MessageBox.Show("修改成功");
                }
                else
                {
                    MessageBox.Show("失败");
                }

            }
        }



        void ImportExcelData(string Path, string ParentId)
        {
            ExcelData excelDataObj = null;
            //无毒平板门板模板
            Non_toxicSeriesOfMaterialsData Non_toxicSeriesOfMaterialsDataObj = null;

            IntransitImport.QuickBeautyFreePaintDoorSheetExcelData QuickBeautyFreePaintDoorSheetObj = null;

            IntransitImport.NoPaintDoorSheetExcelData NoPaintDoorSheetExcelDataObj = null;
            //吸塑模板
            BlisterTemplateMES BlisterTemplateMESObj = null;
            //无毒柜体数据导入到SQL数据库
            Non_ToxicCabinetTable Non_ToxicCabinetTableDataObj = null;
            //ImportDisassemblyData.NorthPointCabinet.Non_ToxicCabinetTableExcelData Non_ToxicCabinetTableDataObj = null;
            //混油+油漆数据
            The_PaintToSQL The_PaintToSQLObj = null;


            Packing_List PackingObj = null;

            Packing_List_ToSQL Packing_List_ToSQLObj = null;

            CheckTicketDoorSheet_ToSQL CheckTicketDoorSheet_ToSQLObj = null;
            //NontoxicPlateDoorTemplateExcelData NontoxicPlateDoorTemplateExcelDataObj = null;
            //GetMaterialBillsDataToSQL GetMaterialBillsDataToSQLObj = null;
            IntransitImport.ClothesClosetMaterialListToSQL ClothesClosetMaterialObj = null;

            CheckTicket_ToSQL CheckTicket_ToSQLObj = null;

            BlisterDoorPlankMaterialRequisitionMES BlisterDoorPlankMaterialRequisitionMESObj = null;

            //盛可居
            NorthPointExperienceMuseumData NorthPointExperienceMuseumDataObj = null;
            NorthPointCabinetData NorthPointCabinetDataObj = null;

            //盛可居全部表单
            ShengCanJu ShengCanJuObj = null;


            //免漆柜体数据导入数据库2018.6.26
            //实例化无毒柜体对象
            MianqiCabinetToSQL MianqiCabinetToSQLObj = null;
            //免漆衣壁柜数据导入SQL2018.6.27
            MianQiYiBiGui_ToSQL MianQiYiBiGui_ToSQLObj = null;





            //switch (this.comboBox1.SelectedItem.ToString())
            //{
            //    case "柜体":
            //        excelDataObj = new CabinetExcelData();
            //        excelDataObj.ReadExcelData(Path, ParentId);
            //        excelDataObj.ImportExcelDataToDB();
            //        break;


            //    case "混油+油漆":
            //        The_PaintToSQLObj = new The_PaintDataToSQL();
            //        The_PaintToSQLObj.ReadThe_PaintToSQLData(Path, ParentId);
            //        break;



            //    case "免漆厨浴柜体数据导入数据库":
            //        //实例化无毒柜体方法
            //        MianqiCabinetToSQLObj = new MianQiGuiTi();
            //        //调用读取Excel方法
            //        MianqiCabinetToSQLObj.ReadMianqiCabinetData(Path, ParentId);
            //        break;
            //    case "免漆衣壁柜数据导入数据库":
            //        //实例化无毒柜体方法
            //        MianQiYiBiGui_ToSQLObj = new ImportDisassemblyData.MianQiYiBiGuiListToSQL();
            //        //调用读取Excel方法
            //        MianQiYiBiGui_ToSQLObj.ReadMianQiYiBiGuiData(Path, ParentId);
            //        break;






            //    case "无毒平板门板模板":
            //        Non_toxicSeriesOfMaterialsDataObj = new ImportDisassemblyData.Non_toxicSeriesOfMaterialsExcelData();
            //        Non_toxicSeriesOfMaterialsDataObj.ReadNon_toxicSeriesOfMaterialsData(Path, ParentId);
            //        //Non_toxicSeriesOfMaterialsDataObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;
            //    case "无毒厨浴柜数据导入数据库":
            //        //Non_ToxicCabinet
            //        Non_ToxicCabinetTableDataObj = new ImportDisassemblyData.Non_ToxicCabinetTableExcelData();
            //        Non_ToxicCabinetTableDataObj.ReadNon_ToxicCabinetTableData(Path, ParentId);
            //        //Non_ToxicCabinetTableDataObj.ImportExcelDataToDB();
            //        break;
            //    case "速美免漆门板单，免漆料单":
            //        QuickBeautyFreePaintDoorSheetObj = new ImportDisassemblyData.QuickBeautyFreePaintDoorSheetExcelData();
            //        QuickBeautyFreePaintDoorSheetObj.ReadQuickBeautyFreePaintDoorSheetData(Path, ParentId);
            //        //QuickBeautyFreePaintDoorSheetObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;
            //    case "免漆门板单，免漆料单":
            //        NoPaintDoorSheetExcelDataObj = new ImportDisassemblyData.NoPaintDoorSheetExcelData();
            //        NoPaintDoorSheetExcelDataObj.ReadNoPaintDoorSheetData(Path, ParentId);
            //        //NoPaintDoorSheetExcelDataObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;
            //    case "吸塑模板MES":
            //        BlisterTemplateMESObj = new ImportDisassemblyData.BlisterTemplateMESExcelData();
            //        BlisterTemplateMESObj.ReadBlisterTemplateMESData(Path, ParentId);
            //        //BlisterTemplateMESObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;





            //    case "包装":
            //        PackingObj = new ImportDisassemblyData.ReadPacking_ListTableTableExcelData();
            //        PackingObj.ReadPacking_ListTableTableData(Path, ParentId);
            //        PackingObj.Packing_ListTableTableDataToSQL();
            //        break;
            //    case "A6包装":
            //        Packing_List_ToSQLObj = new ImportDisassemblyData.ReadPacking_ListTableTableExcelDataToSQL();
            //        Packing_List_ToSQLObj.ReadPacking_ListTableTableData(Path, ParentId);
            //        Packing_List_ToSQLObj.Packing_ListTableTableDataToSQL();
            //        break;
            //    //case "无毒平板门板模板":
            //    //    NontoxicPlateDoorTemplateExcelDataObj = new NontoxicPlateDoorTemplateExcelData();
            //    //    NontoxicPlateDoorTemplateExcelDataObj.ReadNontoxicPlateDoorTemplateData(Path,ParentId);
            //    //    NontoxicPlateDoorTemplateExcelDataObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //    //    break;
            //    case "衣壁柜":
            //        ClothesClosetMaterialObj = new ImportDisassemblyData.ClothesClosetMaterialListToSQL();
            //        ClothesClosetMaterialObj.ReadClothesClosetMaterialListData(Path, ParentId);
            //        ClothesClosetMaterialObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;
            //    case "CheckTicketCabinet":
            //        CheckTicket_ToSQLObj = new ImportDisassemblyData.CheckTicketData1();
            //        CheckTicket_ToSQLObj.CheckTicketData(Path, ParentId, this.dataGridView2);
            //        //CheckTicket_ToSQLObj.CheckTicketDataToDB();
            //        break;

            //    case "CheckTicketDoorSheet":
            //        CheckTicketDoorSheet_ToSQLObj = new ImportDisassemblyData.CheckTicketData2();
            //        CheckTicketDoorSheet_ToSQLObj.CheckTicketDoorSheetData(Path, ParentId, this.dataGridView2);
            //        break;


            //    case "吸塑门板领料单":
            //        BlisterDoorPlankMaterialRequisitionMESObj = new ImportDisassemblyData.BlisterDoorPlankMaterialRequisition();
            //        BlisterDoorPlankMaterialRequisitionMESObj.ReadBlisterDoorPlankMaterialRequisitionMESData(Path, ParentId);
            //        BlisterDoorPlankMaterialRequisitionMESObj.ImportNon_toxicSeriesOfMaterialsDataToDB();
            //        break;

            //    case "北分体验馆粉末喷涂":
            //        NorthPointExperienceMuseum NorthPointExperienceMuseumOBJ = new NorthPointExperienceMuseum();

            //        NorthPointExperienceMuseumOBJ = new NorthPointExperienceMuseum();
            //        NorthPointExperienceMuseumOBJ.ReadNorthPointExperienceMuseumData(Path, ParentId);
            //        NorthPointExperienceMuseumOBJ.NorthPointExperienceMuseumDataToDB();

            //        break;

            //    case "北分体验馆柜体":
            //        //NorthPointExperienceMuseumDataObj = new NorthPointExperienceMuseum();
            //        NorthPointCabinetDataObj = new NorthPointCabinet();
            //        //NorthPointExperienceMuseumDataObj.ReadNorthPointExperienceMuseumData(Path, ParentId);
            //        //NorthPointExperienceMuseumDataObj.NorthPointExperienceMuseumDataToDB();
            //        NorthPointCabinetDataObj.ReadNorthPointCabinetData(Path, ParentId);
            //        NorthPointCabinetDataObj.ReadNorthPointCabinetDataToDB();
            //        break;

            //    case "盛可居":
            //        ShengCanJuObj = new ShengCanJu();
            //        ShengCanJuObj.ReadShengCanJuData(Path, ParentId);
            //        ShengCanJuObj.ShengCanJuDataToDB();
            //        break;


            //}
            ////刷新订单列表
            //DBUtility.ConnectionString = this.GetConnString();
            //DataSet ds = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus<>'未推送' order by Ctime Desc ");
            //this.dataGridView1.DataSource = ds.Tables[0];
            //this.dataGridView1.Columns[0].Visible = true;
            //this.dataGridView1.Columns[3].Width = 220;
            //this.dataGridView1.Columns[4].Width = 220;

            ////刷新已推送订单列表
            //DataSet set = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc ");

            //this.dataGridView2.DataSource = set.Tables[0];
            //this.dataGridView2.Columns[0].Visible = true;
            //this.dataGridView2.Columns[3].Width = 220;
            //this.dataGridView2.Columns[4].Width = 220;
            //DBUtility.ConnectionString = GetConnString();
        }





        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.openFileDialog1.ShowDialog())
            {
                this.txtFilePath.Text = this.openFileDialog1.FileName;
            }
        }
        //柜体



        //无毒平板门板模板

        //速美免漆门板单，免漆料单

        //免漆门板单，免漆料单

        //吸塑模板MES

        //混油+油漆










        //包装
        //A6包装
        //衣壁柜

        //CheckTicketCabinet

        //CheckTicketDoorSheet


        //吸塑门板领料单

        //北分体验馆粉末喷涂

        //北分体验馆柜体

        //盛可居
        //导入库位
        private void UserControl1_Load(object sender, EventArgs e)
        {
            //this.progressBar1.Show();
            //左侧gridview绑定数据
            //DBUtility.ConnectionString = this.GetConnString();
            //DataSet ds = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus<>'未推送' order by Ctime Desc ");
            //this.dataGridView1.DataSource = ds.Tables[0];
            //this.dataGridView1.Columns[0].Visible = true;
            //this.dataGridView1.Columns[3].Width = 220;
            //this.dataGridView1.Columns[4].Width = 220;

            ////右侧gridview绑定数据
            //DataSet set = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc ");

            //this.dataGridView2.DataSource = set.Tables[0];
            //this.dataGridView2.Columns[0].Visible = true;
            //this.dataGridView2.Columns[3].Width = 220;
            //this.dataGridView2.Columns[4].Width = 220;
            //DBUtility.ConnectionString = GetConnString();
            ////DBUtility.ConnectionString = GetConnString();

            //DataSet ds = this.GetDataSetWithSQLStringFromOLAP("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder");
            //this.dataGridView1.DataSource = ds.Tables[0];
            //this.dataGridView1.Columns[0].Visible = true;
            //this.dataGridView1.Columns[3].Width = 220;
            //this.dataGridView1.Columns[4].Width = 220;
            //DBUtility.ConnectionString = GetConnString();
            //listView1.

            ////测试gridview绑定数据
            //DataSet CSset = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc ");
            //this.dataGridView3.DataSource = CSset.Tables[0];
            //this.dataGridView3.Columns[0].Visible = true;
            //this.dataGridView3.Columns[3].Width = 220;
            //this.dataGridView3.Columns[4].Width = 220;
            //DBUtility.ConnectionString = GetConnString();



        }
        /// <summary>
        /// 获取数据连接字符串
        /// </summary>
        /// <returns></returns>
        private string GetConnString()
        {
            return string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};password={3}",
            DatabaseServer, DatabaseName, DatabaseUser, DatabasePassword);
        }







        protected string CStr(object o) { return o != null ? o.ToString() : ""; }
        /// <summary>
        /// 根据订单号模糊查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //string ProductOrderNO = this.ProductOrderNO.Text;
        //    //DataSet ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where OrderType <>'正单'  and ProductOrderNO like '%"+ProductOrderNO+"%' order by Ctime Desc "));


        //    //this.dataGridView1.DataSource = ds.Tables[0];
        //    //this.dataGridView1.Columns[0].Visible = true;
        //    //this.dataGridView1.Columns[3].Width = 220;
        //    //this.dataGridView1.Columns[4].Width = 220;
        //    //DBUtility.ConnectionString = GetConnString();

        //    if (textBox1.Text == null)
        //    {
        //        timer1.Enabled = true;
        //    }
        //    else
        //    {
        //        timer1.Enabled = false;
        //    }

        //    string ProductOrderNO = this.ProductOrderNO.Text;
        //    DataSet ds = new DataSet();
        //    if (ProductOrderNO == null || ProductOrderNO == "")
        //    {
        //        ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus<>'未推送' order by Ctime Desc "));
        //    }
        //    else
        //    {
        //        ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder   where DisassemblyWay=2 and CurrentStatus<>'未推送'  and  ProductOrderNO like '%" + ProductOrderNO + "%' order by Ctime Desc "));
        //    }



        //    this.dataGridView1.DataSource = ds.Tables[0];
        //    this.dataGridView1.Columns[0].Visible = true;
        //    this.dataGridView1.Columns[3].Width = 220;
        //    this.dataGridView1.Columns[4].Width = 220;
        //    DBUtility.ConnectionString = GetConnString();
        //}
        /// <summary>
        /// 刷新数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    //if(this.progressBar1.Value<progressBar1.Maximum)
        //    //{
        //    //    this.progressBar1.Value++;
        //    //}

        //    //string ProductOrderNO = this.ProductOrderNO.Text;
        //    //DataSet ds = new DataSet();
        //    //if (ProductOrderNO == null || ProductOrderNO == "")
        //    //{
        //    //    ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc "));
        //    //}
        //    //else
        //    //{
        //    //    ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder   where DisassemblyWay=2 and CurrentStatus='未推送'  and  ProductOrderNO like '%" + ProductOrderNO + "%' order by Ctime Desc "));
        //    //}
        //    string Id = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        //    //左侧gridview绑定数据
        //    DBUtility.ConnectionString = this.GetConnString();
        //    DataSet ds = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus<>'未推送' order by Ctime Desc ");
        //    this.dataGridView1.DataSource = ds.Tables[0];
        //    this.dataGridView1.Columns[0].Visible = true;
        //    this.dataGridView1.Columns[3].Width = 220;
        //    this.dataGridView1.Columns[4].Width = 220;

        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        string Id2 = ds.Tables[0].Rows[i][0].ToString();
        //        if (Id2 == Id)
        //        {
        //            this.dataGridView1.ClearSelection();
        //            this.dataGridView1.Rows[i].Selected = true;
        //            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[i].Cells[0];
        //            //string id = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        //            //MessageBox.Show(id);
        //            break;
        //        }
        //    }
        //    int RightRowCount = this.dataGridView2.Rows.Count;
        //    if (RightRowCount >= 1)
        //    {
        //        object RightValue = this.dataGridView2.SelectedRows[0].Cells[0].Value;
        //        if (RightValue != null || RightValue.ToString() != "")
        //        {
        //            string IdRight = RightValue.ToString();
        //            DataSet dss = this.GetDataSetWithSQLStringFromOLAP("select top 20 productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc ");

        //            this.dataGridView2.DataSource = dss.Tables[0];
        //            this.dataGridView2.Columns[0].Visible = true;
        //            this.dataGridView2.Columns[3].Width = 220;
        //            this.dataGridView2.Columns[4].Width = 220;
        //            //DBUtility.ConnectionString = GetConnString();
        //            for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
        //            {
        //                string Id2 = dss.Tables[0].Rows[i][0].ToString();
        //                if (Id2 == IdRight)
        //                {
        //                    this.dataGridView2.ClearSelection();
        //                    this.dataGridView2.Rows[i].Selected = true;
        //                    this.dataGridView2.CurrentCell = this.dataGridView2.Rows[i].Cells[0];
        //                    //string id = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        //                    //MessageBox.Show(id);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    //label4.Text = DateTime.Now.ToString();
        //}
        /// <summary>
        /// 查询要下载的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    string ProductOrderNO = this.textBox1.Text;
        //    DataSet ds = new DataSet();
        //    if (ProductOrderNO == null || ProductOrderNO == "")
        //    {
        //        ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc "));
        //        // timer1.Enabled = true;
        //    }
        //    else
        //    {
        //        ds = this.GetDataSetWithSQLStringFromOLAP(string.Format("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder   where DisassemblyWay=2 and CurrentStatus='未推送'  and  ProductOrderNO like '%" + ProductOrderNO + "%' order by Ctime Desc "));
        //        //timer1.Enabled = false;
        //    }

        //    //DataSet ds = this.GetDataSetWithSQLStringFromOLAP("select productOrderId,ProductOrderNO as '订单号',CustomerName as '客户姓名',ProductDescription as '产品描述',ProductName as '产品名称',ProductLine as '产品系列',OrderType as '订单类型' from ProductOrder where DisassemblyWay=2 and CurrentStatus='未推送' order by Ctime Desc ");



        //    this.dataGridView2.DataSource = ds.Tables[0];
        //    this.dataGridView2.Columns[0].Visible = true;
        //    this.dataGridView2.Columns[3].Width = 220;
        //    this.dataGridView2.Columns[4].Width = 220;
        //    DBUtility.ConnectionString = GetConnString();

        //}

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }















    }
}