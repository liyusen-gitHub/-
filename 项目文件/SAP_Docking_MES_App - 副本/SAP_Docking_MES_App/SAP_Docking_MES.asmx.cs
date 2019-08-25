using log4net.Config;
using MZ_MES_MAIN.Receiver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using SAP_DataTo_SQL;
using SAP_Docking_MES_App.ServiceReference1;
namespace SAP_Docking_MES_App
{
    /// <summary>
    /// SAP_Docking_MES_2 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SAP_Docking_MES_2 : System.Web.Services.WebService
    {
        private string ConStr = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString;
        private String InputReplace = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        //Log4net.log4Helper log = new Log4net.log4Helper();
        private String endPointName = "binding_SOAP12";
        private String user = String.Empty;
        private String _syncName = String.Empty;
        /// <summary>
        /// 物料主数据接口  MaterialMasterData表
        /// </summary>
        /// <param name="Input">SAP传送过来的XML字符串</param>
        /// <returns>返回MES接收是否成功</returns>
        [WebMethod(Description = "物料主数据接口")]
        public string MaterialMasterData(string Input)
        {
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            //Input = @"<DocItems><DocItem><MATNR>000000JBC-BH030001</MATNR><MAKTX>红樱桃双贴三聚氰胺爱丽丝E1级刨花板3*1220*2440（中海紫御4号楼）</MAKTX><MEINS>张</MEINS><SPART/><MEINH/><UMREZ/><UMREN/><XCHPF/><MATKL>030101</MATKL><WGBEZ>原材料-板材-刨花板</WGBEZ><EKGRP>J01 </EKGRP><DISMM>PD</DISMM><DISPO>J01</DISPO><DISLS>EX</DISLS><MABST/><BESKZ>F</BESKZ><PLIFZ>1</PLIFZ><DZEIT/><EISBE/><MTVFP>02</MTVFP><PRCTR>PC60060001</PRCTR><BRGEW>1</BRGEW><NTGEW>1</NTGEW><GEWEI>KG</GEWEI><BKLAS>3510</BKLAS><STPRS>0</STPRS><VERPR>47.01</VERPR><CHANGE_IND>I</CHANGE_IND><ZXTCHBS>MES</ZXTCHBS><WERKS>6006</WERKS><AUSME/><ZMEMO1/><ZMEMO2/><ZMEMO3/></DocItem></DocItems>";
            try
            {
                //Move_Type_ToMesReceiver rec = new Move_Type_ToMesReceiver("", "");
                //string result = rec.ReceiveDataFromSAP(Input);
                if (Input.Length == 0)
                {
                    logger.Error("物料主数据接口传入的字符串为空");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>字符串为空</MESSAGE></Response>";
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input);
                    if (doc == null)
                    {
                        logger.Info("物料主数据接口传入的字符串非XML格式");
                        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>字符串非XML格式</MESSAGE></Response>";
                    }
                    else
                    {
                        int a = 0;
                        DataTable dt = new DataTable();
                        XmlNode Node = doc.SelectSingleNode(@"DocItems/DocItem");
                        dt.Columns.Add("MaterialMasterDataId", Type.GetType("System.String"));
                        DataColumn DC = null;
                        for (int i = 0; i < Node.ChildNodes.Count; i++)
                        {
                            XmlNode SingleChildNode = Node.ChildNodes[i];
                            DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        }
                        DataRow Dr = dt.NewRow();
                        XmlNodeList List = doc.SelectNodes(@"DocItems/DocItem");
                        for (int j = 0; j < List.Count; j++)
                        {
                            XmlNode LineNode = List[j];
                            for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                            {
                                XmlNode ChildNode = LineNode.ChildNodes[i];
                                Dr[ChildNode.Name] = ChildNode.InnerText;
                                string Id = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                Dr["MaterialMasterDataId"] = Id;
                            }
                            dt.Rows.Add(Dr.ItemArray);
                        }
                        SAP_DataTo_SQL.SqlHelper SAPToMES = new SAP_DataTo_SQL.SqlHelper();
                        //调用导入数据库方法
                        a = SAPToMES.MaterialMasterData(dt);
                        string STRXML = null;
                        if (a == 1)
                        {
                            dt.Dispose();
                            //return XmlDoc.ToString();
                            logger.Info(Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                        }
                        else
                        {
                            dt.Dispose();
                            //return XmlDoc.ToString();
                            logger.Info(Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                        }
                        dt.Dispose();

                        return STRXML;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>物料主数据接口" + ex.Message + "</MESSAGE></Response>";
            }
        }

        /// <summary>
        /// 移动类型下发
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "移动类型下发")]
        public string Move_Type_ToMES(string Input)
        {
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));

            try
            {
                //Move_Type_ToMesReceiver rec = new Move_Type_ToMesReceiver("", "");
                //string result = rec.ReceiveDataFromSAP(Input);
                if (Input.Length == 0)
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlNode HeaderNode = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlDoc.AppendChild(HeaderNode);
                    //创建根节点
                    XmlElement ResponeseNode = XmlDoc.CreateElement("Response");
                    XmlDoc.AppendChild(ResponeseNode);

                    //创建子节点
                    XmlElement ChildNode = XmlDoc.CreateElement("SIGN");
                    ChildNode.InnerText = "N";
                    ResponeseNode.AppendChild(ChildNode);

                    XmlElement ChildNode2 = XmlDoc.CreateElement("MESSAGE");
                    ChildNode2.InnerText = "失败";
                    ResponeseNode.AppendChild(ChildNode2);
                    string STRXML = XmlDoc.InnerText.ToString();
                    logger.Error("移动类型下发传入的字符串为空");
                    return XmlDoc.ToString();
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input);
                    if (doc == null)
                    {
                        XmlNode HeaderNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        doc.AppendChild(HeaderNode);
                        //创建根节点
                        XmlElement ResponeseNode = doc.CreateElement("Response");
                        doc.AppendChild(ResponeseNode);

                        //创建子节点
                        XmlElement ChildNode = doc.CreateElement("SIGN");
                        ChildNode.InnerText = "！！！！！！！！！！";
                        ResponeseNode.AppendChild(ChildNode);

                        XmlElement ChildNode2 = doc.CreateElement("MESSAGE");
                        ChildNode2.InnerText = "注意这不是XML字符串";
                        ResponeseNode.AppendChild(ChildNode2);
                        string STRXML = doc.InnerText.ToString();
                        logger.Error("移动类型下发传入的字符串非XML格式");
                        return doc.ToString();
                    }
                    else
                    {
                        int a = 0;
                        DataTable dt = new DataTable();
                        XmlNode Node = doc.SelectSingleNode(@"root/line");
                        dt.Columns.Add("Id", Type.GetType("System.String"));
                        DataColumn DC = null;
                        for (int i = 0; i < Node.ChildNodes.Count; i++)
                        {
                            XmlNode SingleChildNode = Node.ChildNodes[i];
                            DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        }
                        DataRow Dr = dt.NewRow();
                        XmlNodeList List = doc.SelectNodes(@"root/line");
                        for (int j = 0; j < List.Count; j++)
                        {
                            XmlNode LineNode = List[j];
                            for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                            {
                                XmlNode ChildNode = LineNode.ChildNodes[i];
                                Dr[ChildNode.Name] = ChildNode.InnerText;
                                string Id = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                Dr["Id"] = Id;
                            }
                            dt.Rows.Add(Dr.ItemArray);
                        }
                        SAP_DataTo_SQL.SqlHelper SAPToMES = new SAP_DataTo_SQL.SqlHelper();
                        //调用导入数据库方法
                        a = SAPToMES.SAP_TableTo_SQL(dt);
                        string STRXML = null;
                        XmlDocument XmlDoc = new XmlDocument();
                        if (a == 1)
                        {
                            dt.Dispose();
                            //return XmlDoc.ToString();
                            logger.Info(Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                        }
                        else
                        {
                            dt.Dispose();
                            //return XmlDoc.ToString();
                            logger.Info(Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                        }
                        dt.Dispose();
                        logger.Info("移动类型下发" + STRXML);
                        return STRXML;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>移动类型下发" + ex.Message + "</MESSAGE></Response>";
            }
        }
        /// <summary>
        /// 采购订单数据同步
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "采购订单数据同步")]
        public string Purchase_OrderData_ToMES(String Input)
        {
            //Input = "<ROOT><SOURCESYSTEMID>MES</SOURCESYSTEMID><SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME><USERID>常月MM009</USERID><USERNAME>常月MM009</USERNAME><SUBMITDATE>20170629</SUBMITDATE><SUBMITTIME>112843</SUBMITTIME><ZFUNCTION>ZFUN_WMS00004</ZFUNCTION><CURRENT_PAGE>000</CURRENT_PAGE><TOTAL_RECORD>000</TOTAL_RECORD><HEAD><VBELN>4500588752</VBELN><BSART>UB</BSART><BUKRS>6007</BUKRS><WERKS>6007</WERKS><LIFNR>3005</LIFNR><NAME1>速美集家装饰有限责任公司-销售</NAME1><ZLGORT>DBJ1</ZLGORT><BWART>351</BWART><SOBKZ/><BEDAT>2017-06-29</BEDAT><ZHSSUM>2</ZHSSUM><ZCHANGE_IND>I</ZCHANGE_IND><ZGIPLANT>北京/通州区/张家湾西定福庄393号（老粮库院内）</ZGIPLANT><ZGRPLANT/><ZKHDZ>沈阳市沈北新区秋月湖街60号沈阳美安物流园</ZKHDZ><ZNAME>孙霄汉</ZNAME><ZTEL>15330818658</ZTEL><KHXM/><KHDZ/><ZKHTEL/><ZGRGIMARK>2</ZGRGIMARK><ZYJCHSJ>0000-00-00</ZYJCHSJ><POST1/><ZFSHENG>210000</ZFSHENG><ZFSHI>210100</ZFSHI><ZFQU>210184</ZFQU><ZFHDZ>沈阳市沈北新区秋月湖街60号沈阳美安物流园</ZFHDZ><ZSSHENG>210000</ZSSHENG><ZSSHI>210100</ZSSHI><ZSQU>210184</ZSQU><ZXTCHBS>MES</ZXTCHBS><ZMEMO1/><ZMEMO2/><ZYSFS/><IHREZ/><ITEMS><ITEM><EBELP>00010</EBELP><MATNR>000000002000017421</MATNR><TXZ01>LD+瓷砖+莫钛+LSI8019+800*800</TXZ01><LGORT>DB01</LGORT><CHARG/><MENGE>640.0</MENGE><MEINS>M2</MEINS><LOEKZ/><PSPID/><RETPO>4</RETPO><ZDBFS/><BKLAS>4000</BKLAS><ZMEMO3/><ZMEMO4/><MATKL>010101</MATKL><ZZHONGL>25.000</ZZHONGL><ZVBELN/></ITEM><ITEM><EBELP>00020</EBELP><MATNR>000000002000017456</MATNR><TXZ01>科进石膏线+阴角线ZB0001+2400*100*80mm</TXZ01><LGORT>DB01</LGORT><CHARG/><MENGE>250.0</MENGE><MEINS>支</MEINS><LOEKZ/><PSPID/><RETPO>4</RETPO><ZDBFS/><BKLAS>4000</BKLAS><ZMEMO3/><ZMEMO4/><MATKL>010518</MATKL><ZZHONGL>4.400</ZZHONGL><ZVBELN/></ITEM></ITEMS></HEAD></ROOT>";
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            string PurchaseOrderSynchronizationVBELN = "";
            Input = Input.Replace(InputReplace, "");
            try
            {
                SqlHelper Helper = new SqlHelper();
                //Move_Type_ToMesReceiver rec = new Move_Type_ToMesReceiver("", "");
                //string result = rec.ReceiveDataFromSAP(Input);
                if (Input.Length == 0)
                {
                    logger.Error("传入的字符串为空");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>字符串为空</MESSAGE></Response>";
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input);
                    if (doc == null)
                    {
                        logger.Error("传入的字符串非XML格式");
                        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>字符串非XML格式</MESSAGE></Response>";
                    }
                    else
                    {
                        DataTable Dt = new DataTable("root");
                        DataTable Itemdt = new DataTable("items");
                        doc.LoadXml(Input);
                        string ZCHANGE_IND = doc.SelectSingleNode(@"ROOT/HEAD/ZCHANGE_IND").InnerText;
                        string VBELN1 = doc.SelectSingleNode(@"ROOT/HEAD/VBELN").InnerText;
                        //Q取消，I下发，D删除，U修改
                        if (ZCHANGE_IND == "Q")
                        {
                            int ReturnVBELN1 = Helper.ReturnVBELN(VBELN1);
                            if (ReturnVBELN1 > 0)
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                        }
                        else if (ZCHANGE_IND == "D")
                        {
                            int ReturnVBELN1 = Helper.DelVBELN(VBELN1);
                            if (ReturnVBELN1 > 0)
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                        }
                        else
                        {
                            if (ZCHANGE_IND == "U")
                            {
                                //先删除原来的数据，再把新传过来的保存到数据库
                                Helper.DelVBELN(VBELN1);
                            }
                            //SqlHelper Helper1 = new SqlHelper();
                            //int A = Convert.ToInt32(Helper1.VbelnExistsThe_Purchase_Order(VBELN1));
                            //if (A > 0)
                            //{
                            //    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>该订单已下发</MESSAGE></Response>";
                            //}
                            //else
                            //{
                            XmlNode SingleLineNode = doc.SelectSingleNode(@"ROOT/HEAD");
                            PurchaseOrderSynchronizationVBELN = doc.SelectSingleNode(@"ROOT/HEAD/VBELN").InnerText;
                            //SellOutOfTheWarehouse表
                            DataColumn Dc = null;
                            for (int i = 0; i < SingleLineNode.ChildNodes.Count; i++)
                            {
                                XmlNode SingleChildNode = SingleLineNode.ChildNodes[i];
                                if (SingleChildNode.Name == "ITEMS")
                                {
                                    break;
                                }
                                Dc = Dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                            }
                            DataRow SellOutOfTheWarehouseRow;
                            SellOutOfTheWarehouseRow = Dt.NewRow();
                            Dt.Columns.Add("PurchaseOrderSynchronizationId", Type.GetType("System.String"));
                            string SellOutOfTheWarehouseId = System.Guid.NewGuid().ToString();
                            SellOutOfTheWarehouseRow["PurchaseOrderSynchronizationId"] = SellOutOfTheWarehouseId.Substring(0, 12).ToUpper();
                            XmlNodeList NodeList = doc.SelectNodes(@"ROOT/HEAD");
                            //获取当前时间
                            DateTime NowTime = DateTime.Now;
                            //获取本月27日
                            DateTime DateTime27 = new DateTime(NowTime.Year, NowTime.Month, 27);
                            //定义变量保存过账日期
                            DateTime BEDAT;
                            //判断当前日期是否大于等于27日，过账
                            if (NowTime >= DateTime27)
                            {
                                BEDAT = new DateTime(NowTime.Year, NowTime.Month + 1, 1);
                            }
                            else
                            {
                                BEDAT = NowTime;
                            }

                            for (int i = 0; i < NodeList.Count; i++)
                            {
                                XmlNode LineNode = NodeList[i];
                                for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                                {
                                    XmlNode ChildNode = LineNode.ChildNodes[j];
                                    if (ChildNode.Name == "ITEMS")
                                    {
                                        break;
                                    }
                                    SellOutOfTheWarehouseRow[ChildNode.Name] = ChildNode.InnerText;
                                }
                                Dt.Rows.Add(SellOutOfTheWarehouseRow);
                            }
                            //Items表
                            XmlNode ItemNode = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM");

                            DataColumn DC = null;
                            Itemdt.Columns.Add("PurchaseOrderSynchronizationId", Type.GetType("System.String"));
                            Itemdt.Columns.Add("PurchaseOrderSynchronizationItemId", Type.GetType("System.String"));

                            Itemdt.Columns.Add("BS", Type.GetType("System.String"));
                            Itemdt.Columns.Add("DATA", Type.GetType("System.String"));
                            Itemdt.Columns.Add("MBLNR", Type.GetType("System.String"));
                            Itemdt.Columns.Add("MJAHR", Type.GetType("System.String"));
                            Itemdt.Columns.Add("VBELN", Type.GetType("System.String"));
                            Itemdt.Columns.Add("VSTEL", Type.GetType("System.String"));
                            for (int i = 0; i < ItemNode.ChildNodes.Count; i++)
                            {
                                XmlNode ItemChildNode = ItemNode.ChildNodes[i];
                                DC = Itemdt.Columns.Add(ItemChildNode.Name.ToUpper(), Type.GetType("System.String"));
                            }
                            XmlNodeList ItemNodeList = doc.SelectNodes(@"ROOT/HEAD/ITEMS/ITEM");
                            DataRow ItemsRow;
                            ItemsRow = Itemdt.NewRow();
                            for (int i = 0; i < ItemNodeList.Count; i++)
                            {
                                XmlNode LineNode = ItemNodeList[i];
                                ItemsRow["PurchaseOrderSynchronizationId"] = SellOutOfTheWarehouseId.Substring(0, 12).ToUpper();
                                string ItemsId = System.Guid.NewGuid().ToString();
                                ItemsRow["PurchaseOrderSynchronizationItemId"] = ItemsId.Substring(0, 12).ToUpper();
                                for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                                {
                                    XmlNode ChildNode = LineNode.ChildNodes[j];
                                    ItemsRow[ChildNode.Name] = ChildNode.InnerText;
                                }
                                //表示0为普通冲销物料凭证，1为销售订单冲销
                                ItemsRow["BS"] = "0";
                                DateTime DATA = DateTime.Now;
                                DateTime DATA27 = new DateTime(DATA.Year, DATA.Month, 27);
                                if (DATA > DATA27)
                                {
                                    DATA = new DateTime(DATA.Year, DATA.Month + 1, 1);
                                }
                                //过账日期
                                ItemsRow["DATA"] = DATA.ToString("yyyyMMdd");
                                XmlNode MBLNR = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM/MATNR");
                                //物料凭证
                                ItemsRow["MBLNR"] = MBLNR.InnerText;
                                //物料凭证年度
                                ItemsRow["MJAHR"] = DATA.Year;
                                XmlNode VBELN2 = doc.SelectSingleNode(@"ROOT/HEAD/VBELN");
                                //销售出库单号
                                ItemsRow["VBELN"] = PurchaseOrderSynchronizationVBELN;
                                //装运地点
                                ItemsRow["VSTEL"] = "";

                                Itemdt.Rows.Add(ItemsRow.ItemArray);
                            }

                            XmlNode BSART = doc.SelectSingleNode("//BSART");
                            //int b = 0;

                            #region
                            ////BSART.InnerText = "NB";
                            //if (BSART.InnerText == "UB")
                            //{
                            //    //生成351出库单
                            //    DataTable Dump_The_Order_Outbound_ReturnTable = new DataTable();
                            //    //获取当前时间
                            //    DateTime now = DateTime.Now;
                            //    //DateTime NextTime = new DateTime(now.Year,now.Month,28);
                            //    //获取第27天的日期
                            //    DateTime Time27 = new DateTime(now.Year, now.Month, 27);
                            //    XmlNode EBELN = doc.SelectSingleNode("//VBELN");
                            //    XmlNode BWART = doc.SelectSingleNode("//BWART");
                            //    XmlNode SOBKZ = doc.SelectSingleNode("//SOBKZ");
                            //    //XmlNode ZWLGRNUM = doc.SelectSingleNode("//BSART");
                            //    DateTime BUDAT;
                            //    //判断日期是否大于27日记录过账日期和凭证日期
                            //    if (now >= Time27)
                            //    {
                            //        BUDAT = new DateTime(now.Year, now.Month + 1, 1);
                            //    }
                            //    else
                            //    {
                            //        BUDAT = now;
                            //    }
                            //    DateTime BLDAT = now;
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("Dump_The_Order_Outbound_ReturnTableId", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("EBELN", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("BWART", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("SOBKZ", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("ZWLGRNUM", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("BUDAT", Type.GetType("System.String"));
                            //    Dump_The_Order_Outbound_ReturnTable.Columns.Add("BLDAT", Type.GetType("System.String"));
                            //    DataRow Dump_The_Order_Outbound_ReturnTableRow = Dump_The_Order_Outbound_ReturnTable.NewRow();
                            //    string Id = System.Guid.NewGuid().ToString().ToUpper();
                            //    string Dump_The_Order_Outbound_ReturnTableId = Id.Substring(0, 12);
                            //    Dump_The_Order_Outbound_ReturnTableRow["Dump_The_Order_Outbound_ReturnTableId"] = Dump_The_Order_Outbound_ReturnTableId;
                            //    Dump_The_Order_Outbound_ReturnTableRow["EBELN"] = EBELN.InnerText;
                            //    Dump_The_Order_Outbound_ReturnTableRow["BWART"] = "351";
                            //    Dump_The_Order_Outbound_ReturnTableRow["SOBKZ"] = SOBKZ.InnerText;
                            //    Dump_The_Order_Outbound_ReturnTableRow["ZWLGRNUM"] = "CK1706290027";
                            //    Dump_The_Order_Outbound_ReturnTableRow["BUDAT"] = BUDAT.ToString("yyyyMMdd");
                            //    Dump_The_Order_Outbound_ReturnTableRow["BLDAT"] = BLDAT.ToString("yyyyMMdd");
                            //    Dump_The_Order_Outbound_ReturnTable.Rows.Add(Dump_The_Order_Outbound_ReturnTableRow);

                            //    DataTable Dump_The_Order_Outbound_ReturnekpoTable = new DataTable();
                            //    DataColumn Dump_The_Order_Outbound_ReturnekpoTableColum = null;
                            //    DataRow Dump_The_Order_Outbound_ReturnekpoTableRow = Dump_The_Order_Outbound_ReturnekpoTable.NewRow();

                            //    XmlNode Node = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM");
                            //    Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("Dump_The_Order_Outbound_ReturnekpoTableId", Type.GetType("System.String"));
                            //    //订单号
                            //    Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("VBELN", Type.GetType("System.String"));

                            //    Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("Dump_The_Order_Outbound_ReturnTableId", Type.GetType("System.String"));
                            //    for (int i = 0; i < Node.ChildNodes.Count; i++)
                            //    {
                            //        XmlNode SingleChildNode = Node.ChildNodes[i];
                            //        if (SingleChildNode.Name == "EBELP" || SingleChildNode.Name == "MATNR" || SingleChildNode.Name == "CHARG" || SingleChildNode.Name == "LGORT" || SingleChildNode.Name == "MENGE" || SingleChildNode.Name == "ZMENGE" || SingleChildNode.Name == "WERKS" || SingleChildNode.Name == "MEINS")
                            //        {
                            //            Dump_The_Order_Outbound_ReturnekpoTableColum = Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));

                            //        }
                            //    }
                            //    XmlNode Node1 = doc.SelectSingleNode(@"ROOT/HEAD");

                            //    for (int i = 0; i < Node1.ChildNodes.Count; i++)
                            //    {
                            //        XmlNode SingleChildNode = Node1.ChildNodes[i];
                            //        if (SingleChildNode.Name == "ZMENGE" || SingleChildNode.Name == "WERKS")
                            //        {
                            //            Dump_The_Order_Outbound_ReturnekpoTableColum = Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));

                            //        }
                            //    }
                            //    Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("ZMENGE", Type.GetType("System.String"));
                            //    //Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("Dump_The_Order_Outbound_ReturnekpoTableId", Type.GetType("System.String"));

                            //    XmlNodeList List = doc.SelectNodes(@"ROOT/HEAD/ITEMS/ITEM");
                            //    for (int j = 0; j < List.Count; j++)
                            //    {
                            //        XmlNode LineNode = List[j];
                            //        for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                            //        {
                            //            XmlNode ChildNode = LineNode.ChildNodes[i];
                            //            if (Dump_The_Order_Outbound_ReturnekpoTable.Columns.Contains(ChildNode.Name))
                            //            {
                            //                Dump_The_Order_Outbound_ReturnekpoTableRow[ChildNode.Name] = ChildNode.InnerText;
                            //            }
                            //            XmlNode WERKS = doc.SelectSingleNode("//WERKS");
                            //            Dump_The_Order_Outbound_ReturnekpoTableRow["WERKS"] = WERKS.InnerText;
                            //            string Dump_The_Order_Outbound_ReturnekpoTableId = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                            //            Dump_The_Order_Outbound_ReturnekpoTableRow["Dump_The_Order_Outbound_ReturnekpoTableId"] = Dump_The_Order_Outbound_ReturnekpoTableId;
                            //            Dump_The_Order_Outbound_ReturnekpoTableRow["Dump_The_Order_Outbound_ReturnTableId"] = Dump_The_Order_Outbound_ReturnTableId;

                            //        }
                            //        Dump_The_Order_Outbound_ReturnekpoTableRow["VBELN"] = PurchaseOrderSynchronizationVBELN;
                            //        Dump_The_Order_Outbound_ReturnekpoTable.Rows.Add(Dump_The_Order_Outbound_ReturnekpoTableRow.ItemArray);
                            //    }


                            //    b = Helper.Dump_The_Order_Outbound_ReturnTable(Dump_The_Order_Outbound_ReturnTable, Dump_The_Order_Outbound_ReturnekpoTable);

                            //}
                            #endregion
                            #region
                            ////生成101采购入库单EKKO信息
                            //DataTable Purchase_RequisitionTable = new DataTable();
                            ////获取当前时间
                            //DateTime nowtime = DateTime.Now;
                            ////DateTime NextTime = new DateTime(now.Year,now.Month,28);
                            ////获取第27天的日期
                            //DateTime Day27 = new DateTime(nowtime.Year, nowtime.Month, 27);
                            //XmlNode VBELN = doc.SelectSingleNode("//VBELN");
                            //XmlNode BWART1 = doc.SelectSingleNode("//BWART");
                            //XmlNode SOBKZ1 = doc.SelectSingleNode("//SOBKZ");
                            ////XmlNode ZWLGRNUM = doc.SelectSingleNode("//BSART");
                            //DateTime BUDAT1;
                            ////判断日期是否大于27日记录过账日期和凭证日期
                            //if (nowtime >= Day27)
                            //{
                            //    BUDAT1 = new DateTime(nowtime.Year, nowtime.Month + 1, 1);
                            //}
                            //else
                            //{
                            //    BUDAT1 = nowtime;
                            //}
                            //DateTime BLDAT1 = nowtime;
                            //Purchase_RequisitionTable.Columns.Add("Purchase_RequisitionTableId", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("EBELN", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("BWART", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("SOBKZ", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("ZWLGRNUM", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("BUDAT", Type.GetType("System.String"));
                            //Purchase_RequisitionTable.Columns.Add("BLDAT", Type.GetType("System.String"));
                            //DataRow Purchase_RequisitionTableRow = Purchase_RequisitionTable.NewRow();
                            //string Id1 = System.Guid.NewGuid().ToString().ToUpper();
                            //string Purchase_RequisitionTableId = Id1.Substring(0, 12);
                            //Purchase_RequisitionTableRow["Purchase_RequisitionTableId"] = Purchase_RequisitionTableId;
                            //Purchase_RequisitionTableRow["EBELN"] = VBELN.InnerText;
                            //Purchase_RequisitionTableRow["BWART"] = "101";
                            //Purchase_RequisitionTableRow["SOBKZ"] = SOBKZ1.InnerText;
                            //Purchase_RequisitionTableRow["ZWLGRNUM"] = "CK1706290027";
                            //Purchase_RequisitionTableRow["BUDAT"] = BUDAT1.ToString("yyyyMMdd");
                            //Purchase_RequisitionTableRow["BLDAT"] = BLDAT1.ToString("yyyyMMdd");
                            //Purchase_RequisitionTable.Rows.Add(Purchase_RequisitionTableRow);

                            ////101采购入库单EKPO表
                            //DataTable Purchase_RequisitionTablekpoTable = new DataTable();
                            //DataColumn Purchase_RequisitionTablekpoTableColum = null;
                            //DataRow Purchase_RequisitionTablekpoTableRow = Purchase_RequisitionTablekpoTable.NewRow();

                            //XmlNode NodeITEM = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM");
                            //Purchase_RequisitionTablekpoTable.Columns.Add("Purchase_RequisitionTablekpoTableId", Type.GetType("System.String"));
                            ////订单号
                            //Purchase_RequisitionTablekpoTable.Columns.Add("VBELN", Type.GetType("System.String"));
                            //Purchase_RequisitionTablekpoTable.Columns.Add("Purchase_RequisitionTableId", Type.GetType("System.String"));
                            //for (int i = 0; i < NodeITEM.ChildNodes.Count; i++)
                            //{
                            //    XmlNode SingleChildNode = NodeITEM.ChildNodes[i];
                            //    if (SingleChildNode.Name == "EBELP" || SingleChildNode.Name == "MATNR" || SingleChildNode.Name == "CHARG" || SingleChildNode.Name == "LGORT" || SingleChildNode.Name == "MENGE" || SingleChildNode.Name == "ZMENGE" || SingleChildNode.Name == "WERKS" || SingleChildNode.Name == "MEINS")
                            //    {
                            //        Purchase_RequisitionTablekpoTableColum = Purchase_RequisitionTablekpoTable.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));

                            //    }
                            //}
                            //XmlNode Node2 = doc.SelectSingleNode(@"ROOT/HEAD");

                            //for (int i = 0; i < Node2.ChildNodes.Count; i++)
                            //{
                            //    XmlNode SingleChildNode = Node2.ChildNodes[i];
                            //    if (SingleChildNode.Name == "ZMENGE" || SingleChildNode.Name == "WERKS")
                            //    {
                            //        Purchase_RequisitionTablekpoTableColum = Purchase_RequisitionTablekpoTable.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));

                            //    }
                            //}
                            //Purchase_RequisitionTablekpoTable.Columns.Add("ZMENGE", Type.GetType("System.String"));
                            ////Dump_The_Order_Outbound_ReturnekpoTable.Columns.Add("Dump_The_Order_Outbound_ReturnekpoTableId", Type.GetType("System.String"));

                            //XmlNodeList List1 = doc.SelectNodes(@"ROOT/HEAD/ITEMS/ITEM");
                            //for (int j = 0; j < List1.Count; j++)
                            //{
                            //    XmlNode LineNode = List1[j];
                            //    for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                            //    {
                            //        XmlNode ChildNode = LineNode.ChildNodes[i];
                            //        if (Purchase_RequisitionTablekpoTable.Columns.Contains(ChildNode.Name))
                            //        {
                            //            Purchase_RequisitionTablekpoTableRow[ChildNode.Name] = ChildNode.InnerText;
                            //        }
                            //        XmlNode WERKS = doc.SelectSingleNode("//WERKS");
                            //        Purchase_RequisitionTablekpoTableRow["WERKS"] = WERKS.InnerText;
                            //        string Purchase_RequisitionTablekpoTableId = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                            //        Purchase_RequisitionTablekpoTableRow["Purchase_RequisitionTablekpoTableId"] = Purchase_RequisitionTablekpoTableId;
                            //        Purchase_RequisitionTablekpoTableRow["Purchase_RequisitionTableId"] = Purchase_RequisitionTableId;

                            //    }
                            //    Purchase_RequisitionTablekpoTableRow["VBELN"] = PurchaseOrderSynchronizationVBELN;
                            //    Purchase_RequisitionTablekpoTable.Rows.Add(Purchase_RequisitionTablekpoTableRow.ItemArray);
                            //}
                            //SqlHelper Purchase_RequisitionTableHelper = new SqlHelper();
                            //int c = Purchase_RequisitionTableHelper.Purchase_RequisitionTable(Purchase_RequisitionTable, Purchase_RequisitionTablekpoTable);
                            #endregion

                            int a = Helper.Purchase_OrderData_ToMES(Dt, Itemdt);
                            //int b = Helper.DumpTheOrderOutboundReturn(SellOutOfTheWarehouseId.Substring(0, 12).ToUpper());
                            string STRXML;
                            if (a == 1)
                            {
                                Dt.Dispose();
                                Itemdt.Dispose();
                                //return XmlDoc.ToString();
                                logger.Info(Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                Dt.Dispose();
                                Itemdt.Dispose();
                                //return XmlDoc.ToString();
                                logger.Info(Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                            Dt.Dispose();
                            Itemdt.Dispose();
                            logger.Info("采购订单数据同步" + STRXML);
                            return STRXML;
                            //}
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>采购订单数据同步" + ex.Message + "</MESSAGE></Response>";
            }
        }
        /// <summary>
        /// 销售出库下发MES接口  (外向交货单)
        /// SellOutOfTheWarehouse表，Items表
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "销售出库下发MES接口")]
        public string Sell_Out_Of_The_Warehouse_ToMES(String Input)
        {
            // Input = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ROOT><SOURCESYSTEMID>ERP</SOURCESYSTEMID><SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME><USERID>MM_HHM</USERID><USERNAME>MM_HHM</USERNAME><SUBMITDATE>20180508</SUBMITDATE><SUBMITTIME>095017</SUBMITTIME><ZFUNCTION>ZFUN_MES00006</ZFUNCTION><CURRENT_PAGE>000</CURRENT_PAGE><TOTAL_RECORD>000</TOTAL_RECORD><HEAD><VBELN>0080015361</VBELN><AUART>ZS08</AUART><WERKS>6006</WERKS><BUKRS/><BWART>601</BWART><ZCHANGE_IND>Q</ZCHANGE_IND><SOBKZ>E</SOBKZ><ZHSSUM>  1</ZHSSUM><KUNNR>0000002001</KUNNR><NAME1>北京分公司</NAME1><STRAS>北京市廊坊嘿嘿嘿街道</STRAS><ZLXNM>SONGJINGL</ZLXNM><TELF1>1212323231</TELF1><PSTLZ/><ZYSFS>物流</ZYSFS><ZYJCHSJ>2018-04-28</ZYJCHSJ><ZXSVBELN>S300134170</ZXSVBELN><ZXSHDTXT/><ZPSPID>1160116110002</ZPSPID><EBELN/><ZSJS>常晓庆BJ0879</ZSJS><ZYFCD/><ZSTO/><ZPRCTR>CBD工作室</ZPRCTR><IHREZ>A20180428000</IHREZ><ZMEMO1/><ZMEMO2>测试立项</ZMEMO2><ZFSHENG/><ZFSHI/><ZFQU/><ZFHDZ>廊坊市安次区龙河高新技术产业区富康道145号</ZFHDZ><ZSSHENG>110000</ZSSHENG><ZSSHI>110100</ZSSHI><ZSQU>110105</ZSQU><ZXTCHBS>MES</ZXTCHBS><ZMMI001/><ZMMI002/><ZMMI003/><ITEMS><ITEM><POSNR>000010</POSNR><MATNR>JC-C-X-XCTK2000</MATNR><MAKTX>壁柜-壁柜-吸塑-香草天空II</MAKTX><LGORT>3001</LGORT><CHARG/><LFIMG>1.0</LFIMG><MEINS>套</MEINS><ENWRT>0.0</ENWRT><BKLAS>7904</BKLAS><ZSYWZ/><MATKL>060504</MATKL><ZZHONGL>         1.000</ZZHONGL><ZVBELN>S300134170/000010</ZVBELN><ZMEMO3/><ZMEMO4/></ITEM></ITEMS></HEAD></ROOT>";

            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                if (Input.Length == 0)
                {
                    logger.Error("销售出库下发MES接口传入的字符串为空");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    Input = Input.Replace(InputReplace, "");
                    doc.LoadXml(Input.ToUpper());
                    if (doc == null)
                    {
                        logger.Error("销售出库下发MES接口传入的字符串非XML格式");
                        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                    }
                    else
                    {
                        SqlHelper Helper = new SqlHelper();

                        //XmlNode Node = doc.SelectSingleNode("//ROOT//HEAD//VBELN");
                        //string VBELN = Node.InnerText;
                        //int IsExits = Convert.ToInt32(Helper.VbelnExists(VBELN));
                        //if (IsExits > 0)
                        //{
                        //    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>该订单已下发</MESSAGE></Response>";
                        //}
                        //else
                        //{
                        XmlNode ZCHANGE_INDNode = doc.SelectSingleNode("//ROOT//HEAD//ZCHANGE_IND");
                        string ZCHANGE_IND = ZCHANGE_INDNode.InnerText;
                        string QVBELN = "";
                        //判断下发，取消，修改下发Q取消，I下发，U修改
                        if (ZCHANGE_IND == "Q")
                        {
                            XmlNode VBELNNode = doc.SelectSingleNode(@"ROOT/HEAD/VBELN");
                            QVBELN = VBELNNode.InnerText;

                            int UpdZCHANGE_IND = Helper.UpdZCHANGE_IND(ZCHANGE_IND, QVBELN);
                            //修改下发订单变更属性
                            if (UpdZCHANGE_IND > 0)
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                        }
                        else if (ZCHANGE_IND == "D")
                        {
                            int i = Helper.Cancellation_Issued(QVBELN);
                            if (i > 0)
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                        }
                        else
                        {
                            if (ZCHANGE_IND == "U")
                            {
                                int i = Helper.Cancellation_Issued(QVBELN);
                            }
                            XmlNode SingleLineNode = doc.SelectSingleNode(@"ROOT/HEAD");
                            //SellOutOfTheWarehouse表
                            DataTable Dt = new DataTable("root");
                            DataColumn Dc = null;
                            for (int i = 0; i < SingleLineNode.ChildNodes.Count; i++)
                            {
                                XmlNode SingleChildNode = SingleLineNode.ChildNodes[i];
                                Dc = Dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                            }
                            DataRow SellOutOfTheWarehouseRow;
                            SellOutOfTheWarehouseRow = Dt.NewRow();
                            Dt.Columns.Add("SellOutOfTheWarehouseId", Type.GetType("System.String"));
                            string SellOutOfTheWarehouseId = System.Guid.NewGuid().ToString();
                            SellOutOfTheWarehouseRow["SellOutOfTheWarehouseId"] = SellOutOfTheWarehouseId.Substring(0, 12).ToUpper();
                            XmlNodeList NodeList = doc.SelectNodes(@"ROOT/HEAD");
                            for (int i = 0; i < NodeList.Count; i++)
                            {
                                XmlNode LineNode = NodeList[i];
                                for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                                {
                                    XmlNode ChildNode = LineNode.ChildNodes[j];
                                    if (ChildNode.Name == "items")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        SellOutOfTheWarehouseRow[ChildNode.Name] = ChildNode.InnerText;
                                    }
                                }
                                Dt.Rows.Add(SellOutOfTheWarehouseRow);
                            }
                            //Items表
                            XmlNode ItemNode = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM");
                            DataTable Itemdt = new DataTable("items");
                            DataColumn DC = null;
                            Itemdt.Columns.Add("SellOutOfTheWarehouseId", Type.GetType("System.String"));
                            Itemdt.Columns.Add("BS", Type.GetType("System.String"));
                            Itemdt.Columns.Add("DATA", Type.GetType("System.String"));
                            Itemdt.Columns.Add("MBLNR", Type.GetType("System.String"));
                            Itemdt.Columns.Add("MJAHR", Type.GetType("System.String"));
                            Itemdt.Columns.Add("VBELN", Type.GetType("System.String"));
                            Itemdt.Columns.Add("VSTEL", Type.GetType("System.String"));
                            Itemdt.Columns.Add("ItemsId", Type.GetType("System.String"));
                            for (int i = 0; i < ItemNode.ChildNodes.Count; i++)
                            {
                                XmlNode ItemChildNode = ItemNode.ChildNodes[i];
                                DC = Itemdt.Columns.Add(ItemChildNode.Name.ToUpper(), Type.GetType("System.String"));
                            }
                            XmlNodeList ItemNodeList = doc.SelectNodes(@"ROOT/HEAD/ITEMS/ITEM");
                            DataRow ItemsRow;
                            ItemsRow = Itemdt.NewRow();
                            for (int i = 0; i < ItemNodeList.Count; i++)
                            {
                                XmlNode LineNode = ItemNodeList[i];
                                ItemsRow["SellOutOfTheWarehouseId"] = SellOutOfTheWarehouseId.Substring(0, 12).ToUpper();
                                string ItemsId = System.Guid.NewGuid().ToString();
                                ItemsRow["ItemsId"] = ItemsId.Substring(0, 12).ToUpper();
                                for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                                {
                                    XmlNode ChildNode = LineNode.ChildNodes[j];
                                    ItemsRow[ChildNode.Name] = ChildNode.InnerText;

                                }
                                //表示0为普通冲销物料凭证，1为销售订单冲销
                                //ItemsRow["BS"] = "0";
                                DateTime DATA = DateTime.Now;
                                DateTime DATA27 = new DateTime(DATA.Year, DATA.Month, 27);
                                if (DATA > DATA27)
                                {
                                    DATA = new DateTime(DATA.Year, DATA.Month + 1, 1);
                                }
                                //过账日期
                                ItemsRow["DATA"] = DATA.ToString("yyyyMMdd");
                                XmlNode MBLNR = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM/MATNR");
                                //物料凭证
                                ItemsRow["MBLNR"] = MBLNR.InnerText;
                                //物料凭证年度
                                ItemsRow["MJAHR"] = DATA.Year;
                                XmlNode VBELN1 = doc.SelectSingleNode(@"ROOT/HEAD/VBELN");
                                //销售出库单号
                                ItemsRow["VBELN"] = VBELN1.InnerText;
                                //装运地点
                                ItemsRow["VSTEL"] = "6006";
                                Itemdt.Rows.Add(ItemsRow.ItemArray);
                            }

                            SAP_DataTo_SQL.SqlHelper SellOutOfTheWarehouse = new SAP_DataTo_SQL.SqlHelper();
                            int a = SellOutOfTheWarehouse.SAP_SellOutOfTheWarehouseTo_SQL(Dt, Itemdt);
                            string STRXML;
                            if (a > 0)
                            {
                                logger.Info(Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                logger.Info(Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                            Itemdt.Dispose();
                            Dt.Dispose();
                            logger.Info("销售出库下发MES接口：" + STRXML);
                            return STRXML;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex + Input);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
            }
        }

        /// <summary>
        /// 工厂库存地下发
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "工厂库存地下发")]
        public string Plant_Stock_ToMes(String Input)
        {
            //Input = "  <?xml version=\"1.0\" encoding=\"utf-8\" ?> <root>  <SOURCESYSTEMID>ERP</SOURCESYSTEMID>   <SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME>   <USERID>DYRS_YUNWEI</USERID>   <USERNAME>DYRS_YUNWEI</USERNAME>   <SUBMITDATE>2018-06-08</SUBMITDATE>   <SUBMITTIME>14:27:21</SUBMITTIME>   <ZFUNCTION>ZFUN_MES00002</ZFUNCTION>   <CURRENT_PAGE>000</CURRENT_PAGE>   <TOTAL_RECORD>000</TOTAL_RECORD><line>  <WERKS>6006</WERKS>   <NAME1>东易日盛智能家居科技有限公司</NAME1>   <BUKRS>6006</BUKRS>   <BUTXT>东易日盛智能家居科技有限公司</BUTXT>   <ZLGORT>1001</ZLGORT>   <ZLGOBE>板材库</ZLGOBE>   <SOURCESYSTEMID>MES</SOURCESYSTEMID>   <TMSSOURCESYSTEMID />   <WLBUKRS />   <WLNAME />   <WLWERKS />   <WLNAME1 />   <WLLGORT />   <WLADD />   <CHANGE_IND>U</CHANGE_IND>   <VERSION>1</VERSION>   <ZUPDDATE>0000-00-00</ZUPDDATE>   <ZXTCHBS>MES</ZXTCHBS>   <ZMEMO1 />   <ZMEMO2 />   <ZMEMO3 />   </line></root>";
            SqlHelper helper = new SqlHelper();
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                if (Input.Length == 0)
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlNode HeaderNode = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlDoc.AppendChild(HeaderNode);
                    //创建根节点
                    XmlElement ResponeseNode = XmlDoc.CreateElement("Response");
                    XmlDoc.AppendChild(ResponeseNode);

                    //创建子节点
                    XmlElement ChildNode = XmlDoc.CreateElement("SIGN");
                    ChildNode.InnerText = "N";
                    ResponeseNode.AppendChild(ChildNode);

                    XmlElement ChildNode2 = XmlDoc.CreateElement("MESSAGE");
                    ChildNode2.InnerText = "失败";
                    ResponeseNode.AppendChild(ChildNode2);
                    string STRXML = XmlDoc.InnerText.ToString();
                    logger.Info("工厂库存地下发接口传入的字符串为空");
                    return XmlDoc.ToString();
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input.Trim());
                    if (doc == null)
                    {
                        XmlNode HeaderNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        doc.AppendChild(HeaderNode);
                        //创建根节点
                        XmlElement ResponeseNode = doc.CreateElement("Response");
                        doc.AppendChild(ResponeseNode);

                        //创建子节点
                        XmlElement ChildNode = doc.CreateElement("SIGN");
                        ChildNode.InnerText = "！！！！！！！！！！";
                        ResponeseNode.AppendChild(ChildNode);

                        XmlElement ChildNode2 = doc.CreateElement("MESSAGE");
                        ChildNode2.InnerText = "注意这不是XML字符串";
                        ResponeseNode.AppendChild(ChildNode2);
                        string STRXML = doc.InnerText.ToString();
                        logger.Info("工厂库存地下发接口传入的字符串非XML格式");
                        return doc.ToString();
                    }
                    else
                    {
                        XmlNode CHANGE_IND = doc.SelectSingleNode(@"root/line/CHANGE_IND");
                        if(CHANGE_IND.InnerText=="U")
                        {
                            XmlNode ZLGORT = doc.SelectSingleNode(@"root/line/ZLGORT");
                            int aaa= helper.UpdatePlant_Stock_Table(ZLGORT.InnerText);
                            string RXML = "";
                            if(aaa>0)
                            {
                                XmlNode Node = doc.SelectSingleNode(@"root/line");
                                DataTable dt = new DataTable();
                                dt.Columns.Add("Id", Type.GetType("System.String"));
                                DataColumn DC = null;
                                for (int i = 0; i < Node.ChildNodes.Count; i++)
                                {
                                    XmlNode SingleChildNode = Node.ChildNodes[i];
                                    DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                                }
                                DataRow Dr = dt.NewRow();
                                XmlNodeList List = doc.SelectNodes(@"root/line");
                                for (int j = 0; j < List.Count; j++)
                                {
                                    XmlNode LineNode = List[j];
                                    for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                                    {
                                        XmlNode ChildNode = LineNode.ChildNodes[i];
                                        Dr[ChildNode.Name] = ChildNode.InnerText;
                                        string Id = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                        Dr["Id"] = Id;
                                    }
                                    dt.Rows.Add(Dr.ItemArray);
                                }
                                //调用导入数据库方法
                                int a = helper.Plant_Stock_ToSQL(dt);
                                if(a>0)
                                {
                                    logger.Info("修改成功");
                                    RXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                                }
                                else
                                {
                                    logger.Info("修改失败");
                                    RXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                                }
                            }
                            return RXML;
                        }
                        else if(CHANGE_IND.InnerText=="D")
                        {
                            XmlNode ZLGORT = doc.SelectSingleNode(@"root/line/ZLGORT");
                            int aaa = helper.UpdatePlant_Stock_Table(ZLGORT.InnerText);
                            string ReturnXML = "";
                            if(aaa>0)
                            {
                                logger.Info("删除成功");
                                ReturnXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                logger.Info("删除失败");
                                ReturnXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                            return ReturnXML;
                        }
                        else
                        {
                            XmlNode Node = doc.SelectSingleNode(@"root/line");
                            DataTable dt = new DataTable();
                            dt.Columns.Add("Id", Type.GetType("System.String"));
                            DataColumn DC = null;
                            for (int i = 0; i < Node.ChildNodes.Count; i++)
                            {
                                XmlNode SingleChildNode = Node.ChildNodes[i];
                                DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                            }
                            DataRow Dr = dt.NewRow();
                            XmlNodeList List = doc.SelectNodes(@"root/line");
                            for (int j = 0; j < List.Count; j++)
                            {
                                XmlNode LineNode = List[j];
                                for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                                {
                                    XmlNode ChildNode = LineNode.ChildNodes[i];
                                    Dr[ChildNode.Name] = ChildNode.InnerText;
                                    string Id = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                    Dr["Id"] = Id;
                                }
                                dt.Rows.Add(Dr.ItemArray);
                            }
                            SAP_DataTo_SQL.SqlHelper SAPToMES = new SAP_DataTo_SQL.SqlHelper();
                            //调用导入数据库方法
                            int a = SAPToMES.Plant_Stock_ToSQL(dt);
                            string STRXML = null;
                            if (a == 1)
                            {
                                logger.Info("工厂库存地下发接口" + Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                            }
                            else
                            {
                                logger.Info("工厂库存地下发接口" + Input);
                                STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                            }
                            dt.Dispose();

                            return STRXML;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>工厂库存地下发接口：" + ex.Message + "</MESSAGE></Response>";
            }
        }



        /// <summary>
        /// 其它出库下发接口
        /// </summary>
        /// <returns></returns>
        //[WebMethod(Description = "其它出库下发接口")]
        public string Other_Outbound_Shipments(String Input)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                if (Input.Length == 0)
                {
                    XmlDocument XmlDoc = new XmlDocument();
                    XmlNode HeaderNode = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlDoc.AppendChild(HeaderNode);
                    //创建根节点
                    XmlElement ResponeseNode = XmlDoc.CreateElement("Response");
                    XmlDoc.AppendChild(ResponeseNode);

                    //创建子节点
                    XmlElement ChildNode = XmlDoc.CreateElement("SIGN");
                    ChildNode.InnerText = "N";
                    ResponeseNode.AppendChild(ChildNode);

                    XmlElement ChildNode2 = XmlDoc.CreateElement("MESSAGE");
                    ChildNode2.InnerText = "失败";
                    ResponeseNode.AppendChild(ChildNode2);
                    string STRXML = XmlDoc.InnerText.ToString();
                    logger.Info("其它出库下发接口传入的字符串为空");
                    return XmlDoc.ToString();
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input);
                    if (doc == null)
                    {
                        XmlNode HeaderNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        doc.AppendChild(HeaderNode);
                        //创建根节点
                        XmlElement ResponeseNode = doc.CreateElement("Response");
                        doc.AppendChild(ResponeseNode);

                        //创建子节点
                        XmlElement ChildNode = doc.CreateElement("SIGN");
                        ChildNode.InnerText = "！！！！！！！！！！";
                        ResponeseNode.AppendChild(ChildNode);

                        XmlElement ChildNode2 = doc.CreateElement("MESSAGE");
                        ChildNode2.InnerText = "失败";
                        ResponeseNode.AppendChild(ChildNode2);
                        string STRXML = doc.InnerText.ToString();
                        logger.Info("其它出库下发接口传入的字符串非XML格式");
                        return doc.ToString();
                    }
                    else
                    {
                        XmlNode Node = doc.SelectSingleNode(@"ROOT/HEAD");
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Other_Outbound_Shipments_TableId", Type.GetType("System.String"));
                        DataColumn DC = null;
                        DataRow Dr = dt.NewRow();
                        for (int i = 0; i < Node.ChildNodes.Count; i++)
                        {
                            XmlNode SingleChildNode = Node.ChildNodes[i];
                            DC = dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                            //Dr[SingleChildNode.Name] = SingleChildNode.InnerText;
                        }
                        string Id = null;
                        XmlNodeList List = doc.SelectNodes(@"ROOT/HEAD");
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
                                Id = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                Dr["Other_Outbound_Shipments_TableId"] = Id;
                            }
                            dt.Rows.Add(Dr.ItemArray);
                        }
                        XmlNode DTNode = doc.SelectSingleNode(@"ROOT/HEAD/ITEMS/ITEM");
                        DataTable DT = new DataTable();
                        DT.Columns.Add("Other_Outbound_Shipments_ItemId", Type.GetType("System.String"));
                        DT.Columns.Add("Other_Outbound_Shipments_TableId", Type.GetType("System.String"));
                        DataColumn DTDC = null;
                        for (int i = 0; i < DTNode.ChildNodes.Count; i++)
                        {
                            XmlNode SingleChildNode = DTNode.ChildNodes[i];
                            DTDC = DT.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
                        }
                        DataRow DTDr = DT.NewRow();
                        XmlNodeList DTList = doc.SelectNodes(@"ROOT/HEAD/ITEMS/ITEM");
                        for (int j = 0; j < DTList.Count; j++)
                        {
                            XmlNode LineNode = DTList[j];
                            for (int i = 0; i < LineNode.ChildNodes.Count; i++)
                            {
                                XmlNode ChildNode = LineNode.ChildNodes[i];
                                DTDr[ChildNode.Name] = ChildNode.InnerText;
                                DTDr["Other_Outbound_Shipments_TableId"] = Id;
                                string Other_Outbound_Shipments_ItemId = System.Guid.NewGuid().ToString().Substring(0, 12).ToUpper();
                                DTDr["Other_Outbound_Shipments_ItemId"] = Other_Outbound_Shipments_ItemId;
                            }
                            DT.Rows.Add(DTDr.ItemArray);
                        }



                        SAP_DataTo_SQL.SqlHelper SAPToMES = new SAP_DataTo_SQL.SqlHelper();
                        //调用导入数据库方法
                        int a = SAPToMES.Other_Outbound_Shipments(dt, DT);
                        string STRXML = null;
                        if (a == 1)
                        {
                            logger.Info(Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>下发成功</MESSAGE></Response>";
                        }
                        else
                        {
                            logger.Info("其它出库下发接口" + Input);
                            STRXML = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>下发失败</MESSAGE></Response>";
                        }
                        dt.Dispose();
                        return STRXML;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>其它出库下发接口：" + ex.Message + "</MESSAGE></Response>";
            }
        }


        /// <summary>
        /// 销售出库取消下发接口
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        [WebMethod(Description = "销售出库取消下发接口（修改移动类型）")]
        public string Cancellation_Issued(String Input)
        {
            //Input = "<ROOT><SOURCESYSTEMID>MES</SOURCESYSTEMID><SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME><USERID>路颖MM009</USERID><USERNAME>路颖MM009</USERNAME><SUBMITDATE>20170626</SUBMITDATE><SUBMITTIME>172413</SUBMITTIME><ZFUNCTION>ZFUN_WMS00006</ZFUNCTION><CURRENT_PAGE>001</CURRENT_PAGE><TOTAL_RECORD>001</TOTAL_RECORD><HEAD><VBELN>0080015357</VBELN><AUART>ZS02</AUART><WERKS>6006</WERKS><BUKRS>6006</BUKRS><BWART>601</BWART><ZCHANGE_IND>Q</ZCHANGE_IND><SOBKZ/><ZHSSUM>2</ZHSSUM><KUNNR>6200000109</KUNNR><NAME1>河南速美超级家装饰工程有限公司</NAME1><STRAS>商丘市梁园区胜利东路与睢阳大道交叉口鼎雄新里程</STRAS><ZLXNM>李战胜</ZLXNM><TELF1>13903700426</TELF1><PSTLZ/><ZYSFS>物流</ZYSFS><ZYJCHSJ>2017-03-28</ZYJCHSJ><ZXSVBELN>0000057925</ZXSVBELN><ZXSHDTXT>河南省郑州市花园路农科路西200米万达中心1605-1608室李战胜13071071517</ZXSHDTXT><ZPSPID/><EBELN>6421703280</EBELN><ZSJS>张绿花023555</ZSJS><ZYFCD/><ZSTO/><ZPRCTR>速美电商公共</ZPRCTR><IHREZ>642170328050</IHREZ><ZMEMO1/><ZMEMO2>河南速美超级家装饰工程有限公司</ZMEMO2><ZFSHENG>410000</ZFSHENG><ZFSHI>410100</ZFSHI><ZFQU>410122</ZFQU><ZFHDZ>测试数据111111111111111111111</ZFHDZ><ZSSHENG>410000</ZSSHENG><ZSSHI>411400</ZSSHI><ZSQU>411403</ZSQU><ZXTCHBS>MES</ZXTCHBS><ZMMI001/><ZMMI002/><ZMMI003/><ITEMS><ITEM><POSNR>000010</POSNR><MATNR>000000002000017814</MATNR><MAKTX>美标坐厕305+CP2033.004.04+CCAS2033-2200400C0</MAKTX><LGORT>ZZJ1</LGORT><CHARG/><LFIMG>2.0</LFIMG><MEINS>套</MEINS><ENWRT>0.0</ENWRT><BKLAS>3005</BKLAS><ZSYWZ>补货328</ZSYWZ><MATKL>010201</MATKL><ZZHONGL>1.000</ZZHONGL><ZVBELN>0000057925/000010</ZVBELN><ZMEMO3/><ZMEMO4/></ITEM><ITEM><POSNR>000020</POSNR><MATNR>000000002000017814</MATNR><MAKTX>美标坐厕305+CP2033.004.04+CCAS2033-2200400C0</MAKTX><LGORT>ZZJ1</LGORT><CHARG/><LFIMG>2.0</LFIMG><MEINS>套</MEINS><ENWRT>0.0</ENWRT><BKLAS>3005</BKLAS><ZSYWZ>补货328</ZSYWZ><MATKL>010201</MATKL><ZZHONGL>1.000</ZZHONGL><ZVBELN>0000057925/000010</ZVBELN><ZMEMO3/><ZMEMO4/></ITEM></ITEMS></HEAD></ROOT>";

            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                if (Input.Length == 0)
                {
                    logger.Info("销售出库取消下发XML为空");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(Input);
                    if (doc == null)
                    {
                        logger.Info("销售出库取消下发失败");
                        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                    }
                    else
                    {
                        SqlHelper Helper = new SqlHelper();
                        XmlNode Node = doc.SelectSingleNode(@"ROOT/HEAD/VBELN");
                        string VBELN = Node.InnerText;

                        string GetVBELN = VBELN;
                        string ZCHANGE_IND = doc.SelectSingleNode("//ROOT//HEAD//ZCHANGE_IND").InnerText;
                        int UpdZCHANGE_IND = Helper.UpdZCHANGE_IND(ZCHANGE_IND, GetVBELN);
                        //修改下发订单变更属性
                        if (UpdZCHANGE_IND > 0)
                        {
                            logger.Info("销售出库取消下发成功");
                            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";
                        }
                        else
                        {
                            logger.Info("销售出库取消下发失败");
                            return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
                return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>销售出库取消下发接口：" + ex.Message + "</MESSAGE></Response>";
            }
        }



        //调用SAP服务

        /// <summary>
        /// 生产订单收货接口 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [WebMethod(Description = "生产订单收货接口")]
        public string Production_Order_Receiving(string Id)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //Id = "PROD000002SW";
                //生产订单收货回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;

                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Production_Order_Receiving(Id);
                if (Set == null)
                {
                    logger.Error("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    //创建XML文档对象
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ROOT = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ROOT);

                    XmlElement HEAD = XMLDoc.CreateElement("HEAD");
                    ROOT.AppendChild(HEAD);

                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int J = 0; J < Set.Tables[0].Columns.Count; J++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[J].ColumnName);
                            if (ChildNode.Name == "BUDAT" || ChildNode.Name == "BLDAT")
                            {
                                DateTime ThisTime = Convert.ToDateTime(Set.Tables[0].Rows[i][J].ToString());
                                ChildNode.InnerText = ThisTime.ToString("yyyyMMdd");
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();
                            }
                            HEAD.AppendChild(ChildNode);
                        }
                    }
                    XmlElement ITEMS = XMLDoc.CreateElement("ITEMS");
                    HEAD.AppendChild(ITEMS);
                    int EBELP = 1;
                    for (int i = 0; i < Set.Tables[1].Rows.Count; i++)
                    {
                        XmlElement ITEM = XMLDoc.CreateElement("ITEM");
                        ITEMS.AppendChild(ITEM);
                        for (int j = 0; j < Set.Tables[1].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[1].Columns[j].ColumnName);
                            if (ChildNode.Name == "EBELP")
                            {
                                ChildNode.InnerText = EBELP.ToString();
                                EBELP = EBELP + 1;
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[1].Rows[i][j].ToString();
                            }

                            ITEM.AppendChild(ChildNode);
                        }
                    }
                    string Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = "7";
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Error("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            string aaa = ObjResponse.OUTPUT.ToString();
                            if (aaa.Contains("生成凭证"))
                            {
                                int bbb = aaa.IndexOf("生成凭证");
                                aaa = aaa.Substring(bbb);
                                aaa = System.Text.RegularExpressions.Regex.Replace(aaa, @"[^0-9]+", "");
                                Helper.Save_Production_Order_Receiving_Inbound_Credentials(Id, aaa);
                            }
                            logger.Error(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.ToString());
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 生产订单发货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "生产订单发货接口")]
        public string Order_Delivery_ToSAP(string Id)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\Users\\李玉森\\Desktop\\Invoke_The_SAP_Service\\Invoke_The_SAP_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //Id = "S300134170";
                //销售订单出库回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Order_Delivery(Id);
                if (Set == null)
                {
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>";

                }
                else
                {
                    //创建XML文档对象
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ROOT = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ROOT);

                    XmlElement HEAD = XMLDoc.CreateElement("HEAD");
                    ROOT.AppendChild(HEAD);
                    //获取当前时间
                    DateTime ThisDate = DateTime.Now;
                    //获取本月第二十七天日期
                    DateTime ThisDate27 = new DateTime(ThisDate.Year, ThisDate.Month, 27);
                    if (ThisDate > ThisDate27)
                    {
                        //判断当前月份是否为12月
                        if (ThisDate.Month == 12)
                        {
                            ThisDate = new DateTime(ThisDate.Year + 1, 1, 1);
                        }
                        ThisDate = new DateTime(ThisDate.Year, ThisDate.Month + 1, 1);
                    }
                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < Set.Tables[0].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[j].ColumnName);

                            if (ChildNode.Name == "BUDAT" || ChildNode.Name == "BLDAT")
                            {
                                ChildNode.InnerText = ThisDate.ToString("yyyyMMdd");
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][j].ToString();
                            }
                            HEAD.AppendChild(ChildNode);
                        }
                    }
                    XmlElement ITEMS = XMLDoc.CreateElement("ITEMS");
                    HEAD.AppendChild(ITEMS);

                    int EBELP = 1;
                    for (int i = 0; i < Set.Tables[1].Rows.Count; i++)
                    {
                        XmlElement ITEM = XMLDoc.CreateElement("ITEM");
                        ITEMS.AppendChild(ITEM);
                        for (int j = 0; j < Set.Tables[1].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[1].Columns[j].ColumnName); if (ChildNode.Name == "EBELP")
                            {
                                ChildNode.InnerText = EBELP.ToString();
                                EBELP = EBELP + 1;
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[1].Rows[i][j].ToString();
                            }
                            ITEM.AppendChild(ChildNode);
                        }

                    }

                    string str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_PO_GOODS_OUT_MZ;
                    OBJ_Str.INPUT = str;


                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        string aaa = ObjResponse.OUTPUT.ToString();
                        if (aaa.Contains("生成凭证"))
                        {
                            int bbb = aaa.IndexOf("生成凭证");
                            aaa = aaa.Substring(bbb);
                            aaa = System.Text.RegularExpressions.Regex.Replace(aaa, @"[^0-9]+", "");
                        }
                        Helper.ReturnNum(Id, aaa);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex);
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 采购申请创建接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "采购申请创建接口")]
        public string Purchase_Application_Creation_ToSAP(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            //Str = "PURC000000TM";
            //销售订单出库回传服务对象
            ServiceReference1.ZWS_MZClient Client = null;
            //传入的字符串对象
            ZFUN_MZ OBJ_Str = null;
            //SAP输出对象
            ZFUN_MZResponse ObjResponse = null;
            try
            {
                SqlHelper helper = new SqlHelper();
                DataSet set = helper.The_Purchasing_Requisition(Str);
                if (set == null)
                {
                    logger.Info("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    for (int a = 0; a < set.Tables["PurchaseApply"].Rows.Count; a++)
                    {
                       

                        //创建XML文档
                        XmlDocument XMLDoc = new XmlDocument();
                        //创建根节点
                        XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XMLDoc.AppendChild(Node);
                        //创建子节点
                        XmlElement ResPose = XMLDoc.CreateElement("HEAD");
                        XMLDoc.AppendChild(ResPose);
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            XmlElement ChildName = null;
                            for (int j = 0; j < set.Tables[0].Columns.Count; j++)
                            {
                                ChildName = XMLDoc.CreateElement(set.Tables[0].Columns[j].ColumnName);
                                ChildName.InnerText = set.Tables[0].Rows[i][j].ToString().TrimEnd().TrimStart();
                                ResPose.AppendChild(ChildName);
                                if (ChildName.Name != "BSART" || ChildName.Name != "ITEXT1")
                                {
                                    break;
                                }
                            }
                            if (ChildName.Name != "BSART" || ChildName.Name != "ITEXT1")
                            {
                                break;
                            }
                        }

                        XmlElement ITEMS = XMLDoc.CreateElement("ITEMS");
                        ResPose.AppendChild(ITEMS);
                        //for (int i = 0; i < set.Tables[1].Rows.Count; i++)
                        //{
                        XmlElement ITEM = XMLDoc.CreateElement("ITEM");
                        ITEMS.AppendChild(ITEM);
                        string PurchaseDetailsId = "";
                        for (int j = 0; j < set.Tables[0].Columns.Count; j++)
                        {

                            XmlElement ChildName = XMLDoc.CreateElement(set.Tables[0].Columns[j].ColumnName);
                            if (ChildName.Name == "BSART" || ChildName.Name == "ITEXT1" || ChildName.Name == "PurchaseDetailsId")
                            {
                                PurchaseDetailsId = set.Tables[0].Rows[a][j].ToString();
                                continue;
                            }
                            if (ChildName.Name == "LFDAT")
                            {
                                DateTime Time = Convert.ToDateTime(set.Tables[0].Rows[a][j].ToString());
                                ChildName.InnerText = Time.ToString("yyyyMMdd");
                                ITEM.AppendChild(ChildName);
                            }
                            else
                            {
                                ChildName.InnerText = set.Tables[0].Rows[a][j].ToString().TrimEnd().TrimStart();
                                ITEM.AppendChild(ChildName);

                            }
                        }
                        Str = XMLDoc.InnerXml;
                        OBJ_Str = new ZFUN_MZ();
                        OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00012;
                        OBJ_Str.INPUT = Str;
                        using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                        {
                            ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(ObjResponse.OUTPUT);
                            XmlNode Data = doc.SelectSingleNode(@"RESULT/DATA");
                            if (helper.SaveThe_Purchasing_Requisition(PurchaseDetailsId, Data.InnerText) > 0)
                            {
                                if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                                {
                                    logger.Info("无效的SAP返回数据!");
                                    return "无效的SAP返回数据!";
                                }
                                try
                                {
                                    logger.Info(ObjResponse.OUTPUT.ToString());
                                    return ObjResponse.OUTPUT.ToString();
                                }
                                catch (Exception ex)
                                {
                                    logger.Info(ex.ToString());
                                    throw ex;
                                }
                            }
                        }
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 销售出库回传接口（成品出库）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "销售出库回传接口（成品出库）")]
        public string Sell_The_Outbound_Return_ToSAP(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            //Str = "48B87E75-922";
            //销售订单出库回传服务对象
            ServiceReference1.ZWS_MZClient Client = null;
            //传入的字符串对象
            ZFUN_MZ OBJ_Str = null;
            //SAP输出对象
            ZFUN_MZResponse ObjResponse = null;

            try
            {
                SqlHelper helper = new SqlHelper();
                DataSet set = helper.Sell_The_Outbound_Return(Str);
                if (set == null)
                {
                    logger.Info("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    //创建XML文档
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ResPose = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ResPose);

                    XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                    SOURCESYSTEMID.InnerText = "MES";
                    ResPose.AppendChild(SOURCESYSTEMID);

                    XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                    SOURCESYSTEMNAME.InnerText = "MES";
                    ResPose.AppendChild(SOURCESYSTEMNAME);

                    XmlElement USERID = XMLDoc.CreateElement("USERID");
                    ResPose.AppendChild(USERID);

                    XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                    ResPose.AppendChild(USERNAME);

                    XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                    ResPose.AppendChild(SUBMITDATE);

                    XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                    ResPose.AppendChild(SUBMITTIME);

                    XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                    ZFUNCTION.InnerText = "ZFUN_WM00007";
                    ResPose.AppendChild(ZFUNCTION);

                    XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                    CURRENT_PAGE.InnerText = "1";
                    ResPose.AppendChild(CURRENT_PAGE);

                    XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                    TOTAL_RECORD.InnerText = "1";
                    ResPose.AppendChild(TOTAL_RECORD);

                    XmlElement XSCKD = XMLDoc.CreateElement("XSCKD");
                    ResPose.AppendChild(XSCKD);


                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < set.Tables[0].Columns.Count; j++)
                        {
                            XmlElement ChildName = XMLDoc.CreateElement(set.Tables[0].Columns[j].ColumnName);
                            if (ChildName.Name == "BUDAT")
                            {
                                DateTime Date = DateTime.Now;
                                ChildName.InnerText = Date.ToString("yyyyMMdd");
                            }
                            else if (ChildName.Name == "ZWLGINUM")
                            {
                                ChildName.InnerText = Str;
                            }
                            else
                            {
                                ChildName.InnerText = set.Tables[0].Rows[i][j].ToString().TrimEnd().TrimStart();
                            }
                            XSCKD.AppendChild(ChildName);
                        }
                    }
                    for (int i = 0; i < set.Tables[1].Rows.Count; i++)
                    {
                        XmlElement XSCKDLN = XMLDoc.CreateElement("XSCKDLN");
                        XSCKD.AppendChild(XSCKDLN);
                        for (int j = 0; j < set.Tables[1].Columns.Count; j++)
                        {
                            XmlElement ChildName = XMLDoc.CreateElement(set.Tables[1].Columns[j].ColumnName);
                            if (ChildName.Name == "MATNR")
                            {
                                ChildName.InnerText = set.Tables[1].Rows[i][j].ToString().TrimEnd().TrimStart().ToUpper();
                            }
                            else
                            {
                                ChildName.InnerText = set.Tables[1].Rows[i][j].ToString().TrimEnd().TrimStart();
                            }
                            XSCKDLN.AppendChild(ChildName);
                        }
                    }

                    string Str1 = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00007;
                    OBJ_Str.INPUT = Str1;
                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        string Response = ObjResponse.OUTPUT;
                        string Num = System.Text.RegularExpressions.Regex.Replace(Response, @"[^0-9]+", "");
                        Num = Num.Substring(3);
                        helper.SaveSell_The_Outbound_Return_Credentials(Str, Num);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            logger.Info(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.ToString());
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 销售订单冲销
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "销售订单冲销")]
        public string Sell_The_Outbound_Return_Sterilisation(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //Str = "0B390587-FAD";
                //销售订单冲销接口回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.GetSell_The_Outbound_Return_Credentials(Str);
                if (Set == null)
                {
                    logger.Info("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    //创建XML对象
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ROOT = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ROOT);

                    XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                    SOURCESYSTEMID.InnerText = "MES";
                    ROOT.AppendChild(SOURCESYSTEMID);

                    XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                    SOURCESYSTEMNAME.InnerText = "MES";
                    ROOT.AppendChild(SOURCESYSTEMNAME);

                    XmlElement USERID = XMLDoc.CreateElement("USERID");
                    ROOT.AppendChild(USERID);

                    XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                    ROOT.AppendChild(USERNAME);

                    XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                    ROOT.AppendChild(SUBMITDATE);


                    XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                    ROOT.AppendChild(SUBMITTIME);

                    XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                    ZFUNCTION.InnerText = "ZFUN_WM000**";
                    ROOT.AppendChild(ZFUNCTION);

                    XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                    CURRENT_PAGE.InnerText = "1";
                    ROOT.AppendChild(CURRENT_PAGE);

                    XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                    TOTAL_RECORD.InnerText = "1";
                    ROOT.AppendChild(TOTAL_RECORD);

                    XmlElement QTCRK = XMLDoc.CreateElement("QTCRK");
                    ROOT.AppendChild(QTCRK);

                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int J = 0; J < Set.Tables[0].Columns.Count; J++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[J].ColumnName);
                            if (ChildNode.Name == "DATA")
                            {
                                DateTime ThisTime = System.DateTime.Now;
                                if (ThisTime.Day >= 27)
                                {
                                    ThisTime = new DateTime(ThisTime.Year, ThisTime.Month + 1, 1);
                                }
                                ChildNode.InnerText = ThisTime.ToString("yyyyMMdd");
                            }
                            else if (ChildNode.Name == "MJAHR")
                            {
                                DateTime ThisTime = System.DateTime.Now;
                                ChildNode.InnerText = ThisTime.Year.ToString();
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();
                            }
                            QTCRK.AppendChild(ChildNode);
                        }
                    }
                    string Str1 = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00013;
                    OBJ_Str.INPUT = Str1;
                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        string aaa = ObjResponse.OUTPUT.ToString();
                        if (aaa.Contains("生成凭证"))
                        {
                            int bbb = aaa.IndexOf("生成凭证");
                            aaa = aaa.Substring(bbb);
                            aaa = System.Text.RegularExpressions.Regex.Replace(aaa, @"[^0-9]+", "");
                            Helper.UpdSell_The_Outbound_Return_Credentials(Str, aaa);
                        }
                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            logger.Info(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.ToString());
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 取消销售出库下发订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "取消销售出库下发订单(删除之前下发的数据)")]
        public string Cancellation_Issued_ToSAP(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //销售订单出库取消回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                //VBELN = "0080015365";
                SqlHelper Helper = new SqlHelper();

                XmlDocument XmlDoc = new XmlDocument();
                XmlNode HeaderNode = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlDoc.AppendChild(HeaderNode);
                //创建根节点
                XmlElement ResponeseNode = XmlDoc.CreateElement("ROOT");
                XmlDoc.AppendChild(ResponeseNode);

                //创建子节点
                XmlElement ChildNode = XmlDoc.CreateElement("ITEM");
                ChildNode.InnerText = "";
                ResponeseNode.AppendChild(ChildNode);

                XmlElement ChildNode2 = XmlDoc.CreateElement("EBELN");
                ChildNode2.InnerText = Str;
                ChildNode.AppendChild(ChildNode2);

                XmlElement ZYYZT = XmlDoc.CreateElement("ZYYZT");
                ZYYZT.InnerText = "Y";
                ChildNode.AppendChild(ZYYZT);

                XmlElement WERKS = XmlDoc.CreateElement("WERKS");
                WERKS.InnerText = "02";
                ChildNode.AppendChild(WERKS);

                XmlElement ZYYTM = XmlDoc.CreateElement("ZYYTM");
                ZYYTM.InnerText = "同意";
                ChildNode.AppendChild(ZYYTM);

                XmlElement ZYYRY = XmlDoc.CreateElement("ZYYRY");
                ZYYRY.InnerText = "曹忠良";
                ChildNode.AppendChild(ZYYRY);

                XmlElement AUDAT = XmlDoc.CreateElement("AUDAT");
                AUDAT.InnerText = DateTime.Now.ToString("yyyyMMdd");
                ChildNode.AppendChild(AUDAT);

                XmlElement ZMMI001 = XmlDoc.CreateElement("ZMMI001");
                ZMMI001.InnerText = "";
                ChildNode.AppendChild(ZMMI001);

                XmlElement ZMMI002 = XmlDoc.CreateElement("ZMMI002");
                ZMMI002.InnerText = "";
                ChildNode.AppendChild(ZMMI002);

                string STRXML = XmlDoc.InnerXml;
                OBJ_Str = new ZFUN_MZ();
                OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00015;
                OBJ_Str.INPUT = STRXML;
                using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                {
                    ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                    Helper.Cancellation_Issued(Str);
                    if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                    {
                        logger.Info("无效的SAP返回数据!");
                        return "无效的SAP返回数据!";
                    }
                    try
                    {
                        logger.Info(ObjResponse.OUTPUT.ToString());
                        return ObjResponse.OUTPUT.ToString();
                    }
                    catch (Exception ex)
                    {
                        logger.Info(ex.ToString());
                        throw ex;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 采购订单出库回传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "采购订单出库回传")]
        public string Return_Of_Purchase_Order_ToSAP(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            //采购订单出库回传服务对象
            ServiceReference1.ZWS_MZClient Client = null;
            //传入的字符串对象
            ZFUN_MZ OBJ_Str = null;
            //SAP输出对象
            ZFUN_MZResponse ObjResponse = null;
            try
            {
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Return_Of_Purchase_Order(Str);

                if (Set == null)
                {
                    logger.Info("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    //创建XML文档
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ResPose = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ResPose);

                    XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                    SOURCESYSTEMID.InnerText = "MES";
                    ResPose.AppendChild(SOURCESYSTEMID);

                    XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                    SOURCESYSTEMNAME.InnerText = "MES";
                    ResPose.AppendChild(SOURCESYSTEMNAME);

                    XmlElement USERID = XMLDoc.CreateElement("USERID");
                    ResPose.AppendChild(USERID);

                    XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                    ResPose.AppendChild(USERNAME);

                    XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                    ResPose.AppendChild(SUBMITDATE);

                    XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                    ResPose.AppendChild(SUBMITTIME);

                    XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                    ZFUNCTION.InnerText = "ZFUN_WM00005";
                    ResPose.AppendChild(ZFUNCTION);

                    XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                    CURRENT_PAGE.InnerText = "1";
                    ResPose.AppendChild(CURRENT_PAGE);

                    XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                    TOTAL_RECORD.InnerText = "1";
                    ResPose.AppendChild(TOTAL_RECORD);

                    XmlElement EKKO = XMLDoc.CreateElement("EKKO");
                    ResPose.AppendChild(EKKO);

                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < Set.Tables[0].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[j].ColumnName);
                            if (ChildNode.Name == "BUDAT" || ChildNode.Name == "BLDAT")
                            {
                                DateTime Date = Convert.ToDateTime(Set.Tables[0].Rows[i][j].ToString());
                                ChildNode.InnerText = Date.ToString("yyyyMMdd");
                            }
                            else if (ChildNode.Name == "ZWLGRNUM")
                            {
                                ChildNode.InnerText = Str;
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][j].ToString().TrimStart().TrimEnd();
                            }

                            EKKO.AppendChild(ChildNode);
                        }
                    }

                    for (int i = 0; i < Set.Tables[1].Rows.Count; i++)
                    {
                        XmlElement EKPO = XMLDoc.CreateElement("EKPO");
                        EKKO.AppendChild(EKPO);
                        for (int j = 0; j < Set.Tables[1].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[1].Columns[j].ColumnName);
                            ChildNode.InnerText = Set.Tables[1].Rows[i][j].ToString().TrimStart().TrimEnd();
                            EKPO.AppendChild(ChildNode);
                        }
                    }

                    string Input = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00005;
                    OBJ_Str.INPUT = Input;
                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        string GetCredentials = ObjResponse.OUTPUT;
                        string Num = System.Text.RegularExpressions.Regex.Replace(GetCredentials, @"[^0-9]+", "");
                        Num = Num.Substring(13);
                        Helper.SaveCredentials(Str, Num);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            logger.Info(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.ToString());
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }


        }

        /// <summary>
        /// 采购订单冲销接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "采购订单冲销接口")]
        public string Orders_For_Sterilisation_ToSAP(string Id)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //Id = "71072C84-CC4";
                //采购订单冲销接口回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Orders_For_Sterilisation(Id);
                if (Set == null)
                {
                    logger.Info("无数据");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
                else
                {
                    //创建XML对象
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ROOT = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ROOT);

                    XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                    SOURCESYSTEMID.InnerText = "MES";
                    ROOT.AppendChild(SOURCESYSTEMID);

                    XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                    SOURCESYSTEMNAME.InnerText = "MES";
                    ROOT.AppendChild(SOURCESYSTEMNAME);

                    XmlElement USERID = XMLDoc.CreateElement("USERID");
                    ROOT.AppendChild(USERID);

                    XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                    ROOT.AppendChild(USERNAME);

                    XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                    ROOT.AppendChild(SUBMITDATE);


                    XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                    ROOT.AppendChild(SUBMITTIME);

                    XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                    ZFUNCTION.InnerText = "ZFUN_WM000**";
                    ROOT.AppendChild(ZFUNCTION);

                    XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                    CURRENT_PAGE.InnerText = "1";
                    ROOT.AppendChild(CURRENT_PAGE);

                    XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                    TOTAL_RECORD.InnerText = "1";
                    ROOT.AppendChild(TOTAL_RECORD);

                    XmlElement QTCRK = XMLDoc.CreateElement("QTCRK");
                    ROOT.AppendChild(QTCRK);

                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int J = 0; J < Set.Tables[0].Columns.Count; J++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[J].ColumnName);
                            if (ChildNode.Name == "DATA")
                            {
                                DateTime ThisTime = System.DateTime.Now;
                                if (ThisTime.Day >= 27)
                                {
                                    ThisTime = new DateTime(ThisTime.Year, ThisTime.Month + 1, 1);
                                }
                                ChildNode.InnerText = ThisTime.ToString("yyyyMMdd");
                            }
                            else if (ChildNode.Name == "MJAHR")
                            {
                                DateTime ThisTime = System.DateTime.Now;
                                ChildNode.InnerText = ThisTime.Year.ToString();
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();
                            }
                            QTCRK.AppendChild(ChildNode);
                        }
                    }
                    string Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00013;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        string Response = ObjResponse.OUTPUT;
                        string Num = System.Text.RegularExpressions.Regex.Replace(Response, @"[^0-9]+", "");
                        Num = Num.Substring(13);
                        Helper.UpdCredentials(Id, Num);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            logger.Info(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.ToString());
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 其他出库回传接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "其他出库回传接口")]
        public string Other_Outbound_Returns_ToSAP(string Str)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            //其他出库回传服务对象
            ServiceReference1.ZWS_MZClient Client = null;
            //传入的字符串对象
            ZFUN_MZ OBJ_Str = null;
            //SAP输出对象
            ZFUN_MZResponse ObjResponse = null;
            try
            {
                SqlHelper helper = new SqlHelper();
                DataSet set = helper.Other_Outbound_Returns(Str);
                DataTable TB = set.Tables["The_Movement_Type"];
                String BWART1 = set.Tables["The_Movement_Type"].Rows[0][0].ToString();
                DataView Dv = TB.DefaultView;
                Dv.RowFilter = "BWART LIKE '%" + BWART1 + "%'";
                if (Dv.Count > 0)
                {
                    if (set == null)
                    {
                        logger.Info("无数据");
                        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                    }
                    else
                    {
                        //创建XML文档
                        XmlDocument XMLDoc = new XmlDocument();
                        //创建根节点
                        XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XMLDoc.AppendChild(Node);
                        //创建子节点
                        XmlElement ResPose = XMLDoc.CreateElement("ROOT");
                        XMLDoc.AppendChild(ResPose);

                        XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                        SOURCESYSTEMID.InnerText = "MES";
                        ResPose.AppendChild(SOURCESYSTEMID);

                        XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                        SOURCESYSTEMNAME.InnerText = "MES";
                        ResPose.AppendChild(SOURCESYSTEMNAME);

                        XmlElement USERID = XMLDoc.CreateElement("USERID");
                        ResPose.AppendChild(USERID);

                        XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                        ResPose.AppendChild(USERNAME);

                        XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                        ResPose.AppendChild(SUBMITDATE);

                        XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                        ResPose.AppendChild(SUBMITTIME);

                        XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                        ZFUNCTION.InnerText = "ZFUN_WM00011";
                        ResPose.AppendChild(ZFUNCTION);

                        XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                        CURRENT_PAGE.InnerText = "1";
                        ResPose.AppendChild(CURRENT_PAGE);

                        XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                        TOTAL_RECORD.InnerText = "1";
                        ResPose.AppendChild(TOTAL_RECORD);

                        XmlElement QTCRK = XMLDoc.CreateElement("QTCRK");
                        ResPose.AppendChild(QTCRK);


                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < set.Tables[0].Columns.Count; j++)
                            {
                                XmlElement ChildName = XMLDoc.CreateElement(set.Tables[0].Columns[j].ColumnName);
                                if (ChildName.Name == "BUDAT" || ChildName.Name == "BLDAT")
                                {
                                    DateTime date = Convert.ToDateTime(set.Tables[0].Rows[i][j].ToString().TrimEnd().TrimStart());
                                    ChildName.InnerText = date.ToString("yyyyMMdd");
                                }
                                else
                                {
                                    ChildName.InnerText = set.Tables[0].Rows[i][j].ToString().TrimEnd().TrimStart();
                                }
                                QTCRK.AppendChild(ChildName);
                            }
                        }
                        for (int i = 0; i < set.Tables[1].Rows.Count; i++)
                        {
                            XmlElement QTCRKLN = XMLDoc.CreateElement("QTCRKLN");
                            QTCRK.AppendChild(QTCRKLN);
                            for (int j = 0; j < set.Tables[1].Columns.Count; j++)
                            {
                                XmlElement ChildName = XMLDoc.CreateElement(set.Tables[1].Columns[j].ColumnName);
                                ChildName.InnerText = set.Tables[1].Rows[i][j].ToString().TrimEnd().TrimStart();
                                QTCRKLN.AppendChild(ChildName);
                            }
                        }

                        Str = XMLDoc.InnerXml;
                        OBJ_Str = new ZFUN_MZ();
                        OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00011;
                        OBJ_Str.INPUT = Str;
                        using (Client = new ZWS_MZClient(this.endPointName))
                        {
                            ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                            if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                            {
                                logger.Info("无效的SAP返回数据!");
                                return "无效的SAP返回数据!";
                            }
                            try
                            {
                                string aaa = ObjResponse.OUTPUT.ToString();
                                if (aaa.Contains("生成凭证"))
                                {
                                    int bbb = aaa.IndexOf("生成凭证");
                                    aaa = aaa.Substring(bbb);
                                    aaa = System.Text.RegularExpressions.Regex.Replace(aaa, @"[^0-9]+", "");
                                    helper.Save_Other_Outbound_Returns_CompanyProof1(Str, aaa);
                                }
                                logger.Info(ObjResponse.OUTPUT.ToString());
                                return ObjResponse.OUTPUT.ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Info(ex.ToString());
                                throw ex;
                            }
                        }
                    }
                }
                else
                {
                    logger.Info("失败");
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>失败</MESSAGE></Response>";
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }


        /// <summary>
        /// 库内转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "库内转移")]
        public string Transfer_The_Rolls_ToSAP(string Id)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //Id = "80E9FB61-B36";
                //销售订单出库回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Transfer_The_Rolls(Id);
                if (Set == null)
                {
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>";
                }
                else
                {
                    //创建XML文档对象
                    XmlDocument XMLDoc = new XmlDocument();
                    //创建根节点
                    XmlNode Node = XMLDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                    XMLDoc.AppendChild(Node);
                    //创建子节点
                    XmlElement ROOT = XMLDoc.CreateElement("ROOT");
                    XMLDoc.AppendChild(ROOT);

                    XmlElement SOURCESYSTEMID = XMLDoc.CreateElement("SOURCESYSTEMID");
                    SOURCESYSTEMID.InnerText = "WMS";
                    ROOT.AppendChild(SOURCESYSTEMID);

                    XmlElement SOURCESYSTEMNAME = XMLDoc.CreateElement("SOURCESYSTEMNAME");
                    SOURCESYSTEMNAME.InnerText = "WMSLMS";
                    ROOT.AppendChild(SOURCESYSTEMNAME);

                    XmlElement USERID = XMLDoc.CreateElement("USERID");
                    ROOT.AppendChild(USERID);

                    XmlElement USERNAME = XMLDoc.CreateElement("USERNAME");
                    ROOT.AppendChild(USERNAME);

                    XmlElement SUBMITDATE = XMLDoc.CreateElement("SUBMITDATE");
                    ROOT.AppendChild(SUBMITDATE);

                    XmlElement SUBMITTIME = XMLDoc.CreateElement("SUBMITTIME");
                    ROOT.AppendChild(SUBMITTIME);

                    XmlElement ZFUNCTION = XMLDoc.CreateElement("ZFUNCTION");
                    ZFUNCTION.InnerText = "ZFUN_WMS00012";
                    ROOT.AppendChild(ZFUNCTION);


                    XmlElement CURRENT_PAGE = XMLDoc.CreateElement("CURRENT_PAGE");
                    CURRENT_PAGE.InnerText = "1";
                    ROOT.AppendChild(CURRENT_PAGE);

                    XmlElement TOTAL_RECORD = XMLDoc.CreateElement("TOTAL_RECORD");
                    TOTAL_RECORD.InnerText = "1";
                    ROOT.AppendChild(TOTAL_RECORD);

                    XmlElement KNZY = XMLDoc.CreateElement("KNZY");
                    ROOT.AppendChild(KNZY);

                    for (int i = 0; i < Set.Tables[0].Rows.Count; i++)
                    {
                        for (int J = 0; J < Set.Tables[0].Columns.Count; J++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[0].Columns[J].ColumnName);
                            if (ChildNode.Name == "BUDAT")
                            {
                                DateTime Time = Convert.ToDateTime(Set.Tables[0].Rows[i][J].ToString());
                                ChildNode.InnerText = Time.ToString("yyyyMMdd");
                            }
                            else
                            {
                                ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();

                            }

                            KNZY.AppendChild(ChildNode);
                        }
                    }
                    XmlElement KNZYLN = XMLDoc.CreateElement("KNZYLN");
                    KNZY.AppendChild(KNZYLN);
                    //int EBELP = 1;
                    for (int i = 0; i < Set.Tables[1].Rows.Count; i++)
                    {

                        for (int j = 0; j < Set.Tables[1].Columns.Count; j++)
                        {
                            XmlElement ChildNode = XMLDoc.CreateElement(Set.Tables[1].Columns[j].ColumnName);

                            ChildNode.InnerText = Set.Tables[1].Rows[i][j].ToString();

                            KNZYLN.AppendChild(ChildNode);
                        }
                    }


                    string Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00014;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            string aaa = ObjResponse.OUTPUT.ToString();
                            if (aaa.Contains("生成凭证"))
                            {
                                int bbb = aaa.IndexOf("生成凭证");
                                aaa = aaa.Substring(bbb);
                                aaa = System.Text.RegularExpressions.Regex.Replace(aaa, @"[^0-9]+", "");
                                Helper.Save_Transfer_The_Rolls_CompanyProof1(Id, aaa);
                            }
                            return ObjResponse.ToString();
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
                throw ex;
            }
        }


        /// <summary>
        /// 取消采购订单下发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [WebMethod(Description = "取消采购订单下发")]
        public string Cancel_The_Purchase_Order_ToSAP(string VBELN)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\inetpub\\wwwroot\\MES_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(SAP_Docking_MES_2));
            try
            {
                //销售订单出库取消回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();

                XmlDocument XmlDoc = new XmlDocument();
                XmlNode HeaderNode = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlDoc.AppendChild(HeaderNode);
                //创建根节点
                XmlElement ResponeseNode = XmlDoc.CreateElement("ROOT");
                XmlDoc.AppendChild(ResponeseNode);

                //创建子节点
                XmlElement ChildNode = XmlDoc.CreateElement("ITEM");
                ChildNode.InnerText = "";
                ResponeseNode.AppendChild(ChildNode);

                XmlElement ChildNode2 = XmlDoc.CreateElement("EBELN");
                ChildNode2.InnerText = VBELN;
                ChildNode.AppendChild(ChildNode2);

                XmlElement ZYYZT = XmlDoc.CreateElement("ZYYZT");
                ZYYZT.InnerText = "Y";
                ChildNode.AppendChild(ZYYZT);

                XmlElement WERKS = XmlDoc.CreateElement("WERKS");
                WERKS.InnerText = "01";
                ChildNode.AppendChild(WERKS);

                XmlElement ZYYTM = XmlDoc.CreateElement("ZYYTM");
                ZYYTM.InnerText = "同意";
                ChildNode.AppendChild(ZYYTM);

                XmlElement ZYYRY = XmlDoc.CreateElement("ZYYRY");
                ZYYRY.InnerText = "曹忠良";
                ChildNode.AppendChild(ZYYRY);

                XmlElement AUDAT = XmlDoc.CreateElement("AUDAT");
                AUDAT.InnerText = "20180402";
                ChildNode.AppendChild(AUDAT);

                XmlElement ZMMI001 = XmlDoc.CreateElement("ZMMI001");
                ZMMI001.InnerText = "";
                ChildNode.AppendChild(ZMMI001);

                XmlElement ZMMI002 = XmlDoc.CreateElement("ZMMI002");
                ZMMI002.InnerText = "";
                ChildNode.AppendChild(ZMMI002);

                string STRXML = XmlDoc.InnerXml;
                OBJ_Str = new ZFUN_MZ();
                OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00015;
                OBJ_Str.INPUT = STRXML;
                using (Client = new ServiceReference1.ZWS_MZClient(this.endPointName))
                {
                    ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                    int A = Helper.Cancel_The_Purchase_Order(VBELN);
                    if (A > 0)
                    {

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            logger.Info("无效的SAP返回数据!");
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            logger.Info(ObjResponse.OUTPUT.ToString());
                            return ObjResponse.OUTPUT.ToString();
                        }
                        catch (Exception ex)
                        {
                            logger.Info(ex.ToString());
                            throw ex;
                        }
                    }
                    else
                    {
                        return "";
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Info(ex.ToString());
                throw ex;
            }
        }

    }
}
