using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using MZ_MES_MAIN;
using log4net.Config;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;
using MZ_MES_Service.ServiceReference1;

namespace MZ_MES_Service
{
    /// <summary>
    /// QueryServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class QueryServices : System.Web.Services.WebService
    {
        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation(string queryString)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                DataTable dt = new DataTable();

                DataColumn DC = null;
                DataRow Dr = dt.NewRow();
                //根据XML结构创建列
                for (int i = 0; i < Node.ChildNodes.Count; i++)
                {
                    XmlNode SingleChildNode = Node.ChildNodes[i];
                    DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                    //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                }
                dt.Columns.Add("Resource_Id");
                //读取数据
                XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                for (int j = 0; j < List.Count; j++)
                {
                    XmlNode LineNode = List[j];
                    for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                    {
                        XmlNode ChildNode = LineNode.ChildNodes[i];
                        if (ChildNode.Name == "ITEMS")
                        {
                            continue;
                        }
                        Dr[ChildNode.Name] = ChildNode.InnerText;
                        Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                    }
                    dt.Rows.Add(Dr.ItemArray);
                }

                //DataSet ds = util.GetDataByXML(obj.Data);
                //if (ds.Tables.Count > 2)
                //{
                //    ds.Tables[2].Columns.Remove("BATCH");
                //}
                //将SAP返回的信息写入到日志
                logger.Info(obj.Data + "\r\n\r\n\r\n");

                SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                con.Open();
                using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                {
                    try
                    {
                        Copy.DestinationTableName = "Inventory_Query";
                        Copy.BatchSize = dt.Rows.Count;
                        Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                        Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                        Copy.ColumnMappings.Add("PLANT", "PLANT");
                        Copy.ColumnMappings.Add("LGORT", "LGORT");
                        Copy.ColumnMappings.Add("BATCH", "BATCH");
                        Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                        Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                        Copy.ColumnMappings.Add("QUANT", "QUANT");
                        Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                        Copy.ColumnMappings.Add("MATKL", "MATKL");
                        Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                        Copy.WriteToServer(dt);

                        //SqlBulkCopy COPy = new SqlBulkCopy(con);
                        //COPy.DestinationTableName = "Inventory_Query1";
                        //COPy.BatchSize = dt.Rows.Count;
                        //COPy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                        //COPy.ColumnMappings.Add("MAKTX", "MAKTX");
                        //COPy.ColumnMappings.Add("PLANT", "PLANT");
                        //COPy.ColumnMappings.Add("LGORT", "LGORT");
                        //COPy.ColumnMappings.Add("BATCH", "BATCH");
                        //COPy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                        //COPy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                        //COPy.ColumnMappings.Add("QUANT", "QUANT");
                        //COPy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                        //COPy.ColumnMappings.Add("MATKL", "MATKL");
                        //COPy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                        //COPy.WriteToServer(dt);

                        con.Close();
                        if (dt.Rows.Count > 0)
                        {
                            return "1";
                        }
                        else
                        {
                            return "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }

        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation1(string queryString)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //将SAP返回的信息写入到日志
                    logger.Info(obj.Data + "\r\n\r\n\r\n");

                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query1";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }

        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation2(string queryString)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //将SAP返回的信息写入到日志
                    logger.Info(obj.Data + "\r\n\r\n\r\n");

                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            Copy.DestinationTableName = "Inventory_Query2";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            //SqlBulkCopy COPy = new SqlBulkCopy(con);
                            //COPy.DestinationTableName = "Inventory_Query1";
                            //COPy.BatchSize = dt.Rows.Count;
                            //COPy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //COPy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //COPy.ColumnMappings.Add("PLANT", "PLANT");
                            //COPy.ColumnMappings.Add("LGORT", "LGORT");
                            //COPy.ColumnMappings.Add("BATCH", "BATCH");
                            //COPy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //COPy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //COPy.ColumnMappings.Add("QUANT", "QUANT");
                            //COPy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //COPy.ColumnMappings.Add("MATKL", "MATKL");
                            //COPy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //COPy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }

        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation3(string queryString)
        {

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);



                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                            //Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //将SAP返回的信息写入到日志
                    logger.Info(obj.Data + "\r\n\r\n\r\n");

                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query3";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }

        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation4(string queryString)
        {

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            //logger.Info("\r\n\r\n"+queryString+"报错1"+"\r\n\r\n");
            logger.Info(queryString);
            try
            {
                ////测试库
                //Utils util = new Utils();
                //StockQuery query = util.GetStockQueryByString(queryString);

                //StockQuerySender sender = new StockQuerySender("");
                ////ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);
                ////ServiceReference1.ZWS_MZClient sap_ws = new ServiceReference1.ZWS_MZClient();
                //string str = util.GetSAPRequestData(query);
                //ServiceReference1.ZWS_MZClient Client = null;
                ////传入的字符串对象
                ////ZFUN_MZ OBJ_Str = null;
                //ZFUN_MZ objstr = null;
                ////SAP输出对象
                //ZFUN_MZResponse ObjResponse = null;
                //objstr = new ZFUN_MZ();
                //objstr.FCODE = EnumFCODE.ZFUN_KC_MES_MZ;
                //objstr.INPUT = str;
                ////logger.Info("\r\n\r\n" + str + "报错2" + "\r\n\r\n");
                //using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                //{
                //    ObjResponse = Client.ZFUN_MZ(objstr);
                //}
                ////logger.Info("\r\n\r\n" + ObjResponse.OUTPUT + "报错3" + "\r\n\r\n");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Replace(" ","");

                //int EndIndex = ObjResponse.OUTPUT.LastIndexOf("?");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Substring(EndIndex+2);
                //string XMLHead = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                //ObjResponse.OUTPUT = XMLHead + ObjResponse.OUTPUT;
                ////logger.Info("\r\n\r\n报错4"+ObjResponse.OUTPUT+"\r\n\r\n");
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(ObjResponse.OUTPUT);

                //正式库
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    //正式库
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    //测试库
                    //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + ObjResponse.OUTPUT + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                            //Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //正式库将SAP返回的信息写入到日志
                    logger.Info(obj.Data + "\r\n\r\n\r\n");
                    ////测试库写日志把SAP返回的值写入到日志
                    //logger.Info(ObjResponse.OUTPUT + "\r\n\r\n\r\n");
                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query4";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }

        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation5(string queryString)
        {

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                ////测试库
                //Utils util = new Utils();
                //StockQuery query = util.GetStockQueryByString(queryString);

                //StockQuerySender sender = new StockQuerySender("");
                ////ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);
                ////ServiceReference1.ZWS_MZClient sap_ws = new ServiceReference1.ZWS_MZClient();
                //string str = util.GetSAPRequestData(query);
                //ServiceReference1.ZWS_MZClient Client = null;
                ////传入的字符串对象
                ////ZFUN_MZ OBJ_Str = null;
                //ZFUN_MZ objstr = null;
                ////SAP输出对象
                //ZFUN_MZResponse ObjResponse = null;
                //objstr = new ZFUN_MZ();
                //objstr.FCODE = EnumFCODE.ZFUN_KC_MES_MZ;
                //objstr.INPUT = str;
                ////logger.Info("\r\n\r\n" + str + "报错2" + "\r\n\r\n");
                //using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                //{
                //    ObjResponse = Client.ZFUN_MZ(objstr);
                //}
                ////logger.Info("\r\n\r\n" + ObjResponse.OUTPUT + "报错3" + "\r\n\r\n");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Replace(" ", "");

                //int EndIndex = ObjResponse.OUTPUT.LastIndexOf("?");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Substring(EndIndex + 2);
                //string XMLHead = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                //ObjResponse.OUTPUT = XMLHead + ObjResponse.OUTPUT;
                ////logger.Info("\r\n\r\n报错4"+ObjResponse.OUTPUT+"\r\n\r\n");
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(ObjResponse.OUTPUT);

                //正式库
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    //正式库
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    //测试库
                    //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + ObjResponse.OUTPUT + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                            //Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //正式库
                    logger.Info(obj.Data);
                    //测试库写日志把SAP返回的值写入到日志
                    //logger.Info(ObjResponse.OUTPUT+"\r\n\r\n\r\n");
                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query5";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }


        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation6(string queryString)
        {

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                ////测试库
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);

                StockQuerySender sender = new StockQuerySender("");
                //ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);
                //ServiceReference1.ZWS_MZClient sap_ws = new ServiceReference1.ZWS_MZClient();
                string str = util.GetSAPRequestData(query);
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                //ZFUN_MZ OBJ_Str = null;
                ZFUN_MZ objstr = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                objstr = new ZFUN_MZ();
                objstr.FCODE = EnumFCODE.ZFUN_KC_MES_MZ;
                objstr.INPUT = str;
                //logger.Info("\r\n\r\n" + str + "报错2" + "\r\n\r\n");
                using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                {
                    ObjResponse = Client.ZFUN_MZ(objstr);
                }
                //logger.Info("\r\n\r\n" + ObjResponse.OUTPUT + "报错3" + "\r\n\r\n");
                ObjResponse.OUTPUT = ObjResponse.OUTPUT.Replace(" ", "");

                int EndIndex = ObjResponse.OUTPUT.LastIndexOf("?");
                ObjResponse.OUTPUT = ObjResponse.OUTPUT.Substring(EndIndex + 2);
                string XMLHead = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                ObjResponse.OUTPUT = XMLHead + ObjResponse.OUTPUT;
                //logger.Info("\r\n\r\n报错4"+ObjResponse.OUTPUT+"\r\n\r\n");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ObjResponse.OUTPUT);

                //正式库
                //Utils util = new Utils();
                //StockQuery query = util.GetStockQueryByString(queryString);
                //StockQuerySender sender = new StockQuerySender("");
                //ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    //正式库
                    //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    //测试库
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + ObjResponse.OUTPUT + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                            //Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //正式库
                    //logger.Info(obj.Data);
                    //测试库写日志把SAP返回的值写入到日志
                    logger.Info(ObjResponse.OUTPUT+"\r\n\r\n\r\n");
                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query6";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }


        [WebMethod(Description = "根据MES传过来的字符串分割构造查询数组")]
        public string GetQueryInfoMation7(string queryString)
        {

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));
            logger.Info(queryString);
            try
            {
                ////测试库
                //Utils util = new Utils();
                //StockQuery query = util.GetStockQueryByString(queryString);

                //StockQuerySender sender = new StockQuerySender("");
                ////ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);
                ////ServiceReference1.ZWS_MZClient sap_ws = new ServiceReference1.ZWS_MZClient();
                //string str = util.GetSAPRequestData(query);
                //ServiceReference1.ZWS_MZClient Client = null;
                ////传入的字符串对象
                ////ZFUN_MZ OBJ_Str = null;
                //ZFUN_MZ objstr = null;
                ////SAP输出对象
                //ZFUN_MZResponse ObjResponse = null;
                //objstr = new ZFUN_MZ();
                //objstr.FCODE = EnumFCODE.ZFUN_KC_MES_MZ;
                //objstr.INPUT = str;
                ////logger.Info("\r\n\r\n" + str + "报错2" + "\r\n\r\n");
                //using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                //{
                //    ObjResponse = Client.ZFUN_MZ(objstr);
                //}
                ////logger.Info("\r\n\r\n" + ObjResponse.OUTPUT + "报错3" + "\r\n\r\n");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Replace(" ", "");

                //int EndIndex = ObjResponse.OUTPUT.LastIndexOf("?");
                //ObjResponse.OUTPUT = ObjResponse.OUTPUT.Substring(EndIndex + 2);
                //string XMLHead = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                //ObjResponse.OUTPUT = XMLHead + ObjResponse.OUTPUT;
                ////logger.Info("\r\n\r\n报错4"+ObjResponse.OUTPUT+"\r\n\r\n");
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(ObjResponse.OUTPUT);

                //正式库
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(obj.Data);

                XmlNode Node = doc.SelectSingleNode(@"ROOT/DATALINES/DATALINE");
                if (Node == null)
                {
                    //正式库
                    logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + obj.Data + "\r\n\r\n");
                    //测试库
                    //logger.Info(System.Reflection.MethodBase.GetCurrentMethod().Name + "SAP返回的消息为：" + ObjResponse.OUTPUT + "\r\n\r\n");
                    return "0";
                }
                else
                {
                    DataTable dt = new DataTable();

                    DataColumn DC = null;
                    DataRow Dr = dt.NewRow();
                    //根据XML结构创建列
                    for (int i = 0; i < Node.ChildNodes.Count; i++)
                    {
                        XmlNode SingleChildNode = Node.ChildNodes[i];
                        DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                    }
                    dt.Columns.Add("Resource_Id");
                    //读取数据
                    XmlNodeList List = doc.SelectNodes(@"ROOT/DATALINES/DATALINE");
                    for (int j = 0; j < List.Count; j++)
                    {
                        XmlNode LineNode = List[j];
                        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                        {
                            XmlNode ChildNode = LineNode.ChildNodes[i];
                            if (ChildNode.Name == "ITEMS")
                            {
                                continue;
                            }
                            Dr[ChildNode.Name] = ChildNode.InnerText;
                            Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                            //Dr["Resource_Id"] = System.Net.Dns.GetHostName();
                        }
                        dt.Rows.Add(Dr.ItemArray);
                    }

                    //DataSet ds = util.GetDataByXML(obj.Data);
                    //if (ds.Tables.Count > 2)
                    //{
                    //    ds.Tables[2].Columns.Remove("BATCH");
                    //}
                    //正式库
                    logger.Info(obj.Data);
                    //测试库写日志把SAP返回的值写入到日志
                    //logger.Info(ObjResponse.OUTPUT+"\r\n\r\n\r\n");
                    SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017");
                    con.Open();
                    using (SqlBulkCopy Copy = new SqlBulkCopy(con))
                    {
                        try
                        {
                            //Copy.DestinationTableName = "Inventory_Query";
                            //Copy.BatchSize = dt.Rows.Count;
                            //Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            //Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            //Copy.ColumnMappings.Add("PLANT", "PLANT");
                            //Copy.ColumnMappings.Add("LGORT", "LGORT");
                            //Copy.ColumnMappings.Add("BATCH", "BATCH");
                            //Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            //Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            //Copy.ColumnMappings.Add("QUANT", "QUANT");
                            //Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            //Copy.ColumnMappings.Add("MATKL", "MATKL");
                            //Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            //Copy.WriteToServer(dt);

                            Copy.DestinationTableName = "Inventory_Query7";
                            Copy.BatchSize = dt.Rows.Count;
                            Copy.ColumnMappings.Add("MATERIAL", "MATERIAL");
                            Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                            Copy.ColumnMappings.Add("PLANT", "PLANT");
                            Copy.ColumnMappings.Add("LGORT", "LGORT");
                            Copy.ColumnMappings.Add("BATCH", "BATCH");
                            Copy.ColumnMappings.Add("SPEC_STOCK", "SPEC_STOCK");
                            Copy.ColumnMappings.Add("SP_STCK_NO", "SP_STCK_NO");
                            Copy.ColumnMappings.Add("QUANT", "QUANT");
                            Copy.ColumnMappings.Add("BASE_UOM", "BASE_UOM");
                            Copy.ColumnMappings.Add("MATKL", "MATKL");
                            Copy.ColumnMappings.Add("Resource_Id", "Resource_Id");
                            Copy.WriteToServer(dt);

                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                return "1";
                            }
                            else
                            {
                                return "0";
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + "\r\n\r\n\r\n");
                return ex.Message.ToString();
            }
        }




        [WebMethod(Description = "库内转移（库存地、状态、批次，物料）回传")]
        public DataSet Inventory_Query(string queryString)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(QueryServices));

            try
            {
                Utils util = new Utils();
                StockQuery query = util.GetStockQueryByString(queryString);
                StockQuerySender sender = new StockQuerySender("");
                ResultWrap obj = (ResultWrap)sender.SendDataToSAP(query);
                DataSet ds = util.GetDataByXML(obj.Data);
                //if (ds.Tables.Count > 2)
                //{
                //    ds.Tables[2].Columns.Remove("BATCH");
                //}
                logger.Info(obj);
                return ds;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new DataSet();
            }
        }
    }
}
