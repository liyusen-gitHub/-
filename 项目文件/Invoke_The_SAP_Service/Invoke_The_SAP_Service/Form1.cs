using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAP_DataTo_SQL;
using System.Xml;
using Invoke_The_SAP_Service.Util;
using Invoke_The_SAP_Service.ServiceReference4;
using System.IO;
using log4net;
using log4net.Config;
namespace Invoke_The_SAP_Service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private String endPointName = "binding_SOAP12";
        private String user = String.Empty;
        private String _syncName = String.Empty;
        private string XMLHead = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        /// <summary>
        /// 采购订单出库回传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string Str = "B7244F14-8CE";
            //采购订单出库回传服务对象
            ServiceReference4.ZWS_MZClient Client = null;
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
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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

                    Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00005;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
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
        /// 其他出库回传接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string Str = "5CE08322-32F";
            //其他出库回传服务对象
            ServiceReference4.ZWS_MZClient Client = null;
            //传入的字符串对象
            ZFUN_MZ OBJ_Str = null;
            //SAP输出对象
            ZFUN_MZResponse ObjResponse = null;

            try
            {
                SqlHelper helper = new SqlHelper();
                DataSet set = helper.Other_Outbound_Returns(Str);
                String[] StrList = new String[1];
                StrList = new String[] { "Z61", "Z62", "Z63", "Z64", "Z65", "Z66", "Z67", "Z68", "Z69", "Z70", "Z71", "Z72", "Z73", "Z74", "Z75", "Z76", "Z77", "Z78", "Z79", "Z80", "Z83", "Z84", "Z87", "Z88", "Z91", "Z92" };
                string BWART = set.Tables[0].Rows[0][3].ToString();
                if (StrList.Contains(BWART))
                {
                    if (set == null)
                    {
                        MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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
                                if (ChildName.Name == "BUDAT")
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
                                MessageBox.Show("无效的SAP返回数据!");
                            }
                            try
                            {
                                MessageBox.Show(ObjResponse.ToString());
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");
                    }
                }
                else
                {
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>其他出库回传不允许'" + BWART + "'类型</MESSAGE></Response>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 销售出库回传接口（成品出库）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void button2_Click(object sender, EventArgs e)
        {
            string Str = "48B87E75-922";
            //销售订单出库回传服务对象
            ServiceReference4.ZWS_MZClient Client = null;
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
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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

                    Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00007;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");
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
        private void button4_Click(object sender, EventArgs e)
        {
            string Str = "PURC000000TM";
            //销售订单出库回传服务对象
            ServiceReference4.ZWS_MZClient Client = null;
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
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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
                        for (int j = 0; j < set.Tables[0].Columns.Count; j++)
                        {

                            XmlElement ChildName = XMLDoc.CreateElement(set.Tables[0].Columns[j].ColumnName);
                            if (ChildName.Name == "BSART" || ChildName.Name == "ITEXT1")
                            {
                                continue;
                            }
                            if (ChildName.Name == "LFDAT")
                            {
                                DateTime Time =Convert.ToDateTime( set.Tables[0].Rows[a][j].ToString());
                                ChildName.InnerText = Time.ToString("yyyyMMdd");
                                ITEM.AppendChild(ChildName);
                            }
                            else
                            {
                                ChildName.InnerText = set.Tables[0].Rows[a][j].ToString().TrimEnd().TrimStart();
                                ITEM.AppendChild(ChildName);

                            }
                        }
                        //}

                        Str = XMLDoc.InnerXml;
                        OBJ_Str = new ZFUN_MZ();
                        OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00012;
                        OBJ_Str.INPUT = Str;
                        using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                        {
                            ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                            if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                            {
                                MessageBox.Show("无效的SAP返回数据!");
                            }
                            try
                            {
                                MessageBox.Show(ObjResponse.ToString());
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        //MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 订单冲销接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = "0080015356";

                //销售订单出库回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Orders_For_Sterilisation(Id);
                if (Set == null)
                {
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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
                                string Time = Set.Tables[0].Rows[i][J].ToString();
                                ChildNode.InnerText = Convert.ToDateTime(Time).ToString("yyyyMMdd");
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
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 转储订单出库回传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string Str = "";
                SqlHelper Helper = new SqlHelper();
                DataTable Dump_The_Order_Outbound_Return_Table = new DataTable();





            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生产订单发货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            //获取日志配置文件
            var logCfg = new FileInfo("C:\\Users\\李玉森\\Desktop\\Invoke_The_SAP_Service\\Invoke_The_SAP_Service\\log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(Form1));
            try
            {
                string Id = "S300134170";
                //销售订单出库回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Order_Delivery(Id);
                if (Set == null)
                {
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
                    logger.Info("无数据");
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
                    string XMLMESSAGE = "";
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        XmlDocument Xdoc = new XmlDocument();
                        string OutPutXML = ObjResponse.OUTPUT.Replace(XMLHead, "");
                        Xdoc.LoadXml(ObjResponse.OUTPUT);

                        XmlNode SIGN = Xdoc.SelectSingleNode("//RESULT//ITEM//SIGN");
                        string XMLSIGN = SIGN.InnerText;
                        //把生成的凭证保存到数据库
                        if (XMLSIGN == "Y")
                        {
                            XmlNode MESSAGE = Xdoc.SelectSingleNode("//RESULT//ITEM//SIGN");
                            XMLMESSAGE = MESSAGE.InnerText;

                            string RXML = "<?xml version=\"1.0\" encoding=\"utf-8\"?><RESULT><ITEM><SIGN>Y</SIGN><MESSAGE>生产订单S300134170发货成功,生成凭证:";
                            XMLMESSAGE = XMLMESSAGE.Replace(RXML, "");
                            XMLMESSAGE = XMLMESSAGE.Substring(0, 10);
                            Helper.ReturnNum(Id, XMLMESSAGE);
                        }

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                            logger.Info("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");
                    logger.Info(XMLMESSAGE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生产订单收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = "PROD000002SW";
                //销售订单出库回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Production_Order_Receiving(Id);
                if (Set == null)
                {
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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

                            ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();
                            //}

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
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_PO_GOODS_IN_MZ;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 库内转移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = "80E9FB61-B36";
                //销售订单出库回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Transfer_The_Rolls(Id);
                if (Set == null)
                {
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
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

                            ChildNode.InnerText = Set.Tables[0].Rows[i][J].ToString();
                            //}

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
                            //if (ChildNode.Name == "EBELP")
                            //{
                            //    ChildNode.InnerText = EBELP.ToString();
                            //    EBELP = EBELP + 1;
                            //}
                            //else
                            //{
                            ChildNode.InnerText = Set.Tables[1].Rows[i][j].ToString();
                            //}

                            KNZYLN.AppendChild(ChildNode);
                        }
                    }
                    string Str = XMLDoc.InnerXml;
                    OBJ_Str = new ZFUN_MZ();
                    OBJ_Str.FCODE = EnumFCODE.ZFUN_WM00014;
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 取消下发订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                //销售订单出库取消回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                string VBELN = "0080015361";
                SqlHelper Helper = new SqlHelper();
                int A = 0;
                // A= Helper.Cancellation_Issued(VBELN);
                A = 1;
                if (A > 0)
                {
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
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        Helper.Cancellation_Issued(VBELN);
                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                else
                {
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
                    ZYYZT.InnerText = "N";
                    ChildNode.AppendChild(ZYYZT);

                    XmlElement WERKS = XmlDoc.CreateElement("WERKS");
                    WERKS.InnerText = "02";
                    ChildNode.AppendChild(WERKS);

                    XmlElement ZYYTM = XmlDoc.CreateElement("ZYYTM");
                    ZYYTM.InnerText = "不同意";
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
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
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
        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                //销售订单出库取消回传服务对象
                ServiceReference4.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                string VBELN = "4500588750";
                SqlHelper Helper = new SqlHelper();
                int A = Helper.Cancel_The_Purchase_Order(VBELN);
                if (A > 0)
                {
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
                    WERKS.InnerText = "03";
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
                    using (Client = new ServiceReference4.ZWS_MZClient(this.endPointName))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);

                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            MessageBox.Show("无效的SAP返回数据!");
                        }
                        try
                        {
                            MessageBox.Show(ObjResponse.ToString());
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


        ///// <summary>
        ///// 库内转移（库存地、状态、批次、物料）回传
        ///// </summary>
        ///// <returns></returns>
        //private void button12_Click(object sender, EventArgs e)
        //{
        //    //[WebMethod(Description = "库内转移（库存地、状态、批次、物料）回传")]

        //    try
        //    {
        //        String Input = "80E9FB61-B36";
        //        SqlHelper Helper = new SqlHelper();
        //        DataSet SET = Helper.Transfer_The_Rolls(Input);
        //        if (SET == null)
        //        {
        //            MessageBox.Show("<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>N</SIGN><MESSAGE>无数据</MESSAGE></Response>");
        //        }
        //        else
        //        {
        //            //创建XML文档
        //            XmlDocument XmlDoc = new XmlDocument();
        //            //创建根节点
        //            XmlNode Node = XmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        //            XmlDoc.AppendChild(Node);
        //            //创建子节点
        //            XmlElement ROOT = XmlDoc.CreateElement("ROOT");
        //            XmlDoc.AppendChild(ROOT);

        //            XmlElement SOURCESYSTEMID = XmlDoc.CreateElement("SOURCESYSTEMID");
        //            SOURCESYSTEMID.InnerText = "MES";
        //            ROOT.AppendChild(SOURCESYSTEMID);

        //            XmlElement SOURCESYSTEMNAME = XmlDoc.CreateElement("SOURCESYSTEMNAME");
        //            SOURCESYSTEMNAME.InnerText = "MES";
        //            ROOT.AppendChild(SOURCESYSTEMNAME);

        //            XmlElement USERID = XmlDoc.CreateElement("USERID");
        //            ROOT.AppendChild(USERID);

        //            XmlElement USERNAME = XmlDoc.CreateElement("USERNAME");
        //            ROOT.AppendChild(USERNAME);

        //            XmlElement SUBMITDATE = XmlDoc.CreateElement("SUBMITDATE");
        //            ROOT.AppendChild(SUBMITDATE);

        //            XmlElement SUBMITTIME = XmlDoc.CreateElement("SUBMITTIME");
        //            ROOT.AppendChild(SUBMITTIME);

        //            XmlElement ZFUNCTION = XmlDoc.CreateElement("ZFUNCTION");
        //            ZFUNCTION.InnerText = "ZFUN_WMS00012";
        //            ROOT.AppendChild(ZFUNCTION);

        //            XmlElement CURRENT_PAGE = XmlDoc.CreateElement("CURRENT_PAGE");
        //            CURRENT_PAGE.InnerText = "1";
        //            ROOT.AppendChild(CURRENT_PAGE);

        //            XmlElement TOTAL_RECORD = XmlDoc.CreateElement("TOTAL_RECORD");
        //            TOTAL_RECORD.InnerText = "1";
        //            ROOT.AppendChild(TOTAL_RECORD);

        //            XmlElement KNZY = XmlDoc.CreateElement("KNZY");
        //            ROOT.AppendChild(KNZY);

        //            for (int i = 0; i < SET.Tables[0].Rows.Count; i++)
        //            {
        //                for (int j = 0; j < SET.Tables[0].Columns.Count; j++)
        //                {
        //                    XmlElement CHILDNODE = XmlDoc.CreateElement(SET.Tables[0].Columns[j].ColumnName);
        //                    CHILDNODE.InnerText = SET.Tables[0].Rows[i][j].ToString();
        //                    KNZY.AppendChild(CHILDNODE);
        //                }
        //            }
        //            XmlElement KNZYLN = XmlDoc.CreateElement("KNZYLN");

        //            KNZY.AppendChild(KNZYLN);
        //            for (int i = 0; i < SET.Tables[1].Rows.Count; i++)
        //            {
        //                for (int j = 0; j < SET.Tables[1].Columns.Count; j++)
        //                {
        //                    XmlElement CHILDNODE = XmlDoc.CreateElement(SET.Tables[1].Columns[j].ColumnName);
        //                    CHILDNODE.InnerText = SET.Tables[1].Rows[i][j].ToString();
        //                    KNZYLN.AppendChild(CHILDNODE);
        //                }
        //            }
        //            string XMLStr = XmlDoc.InnerXml;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {

        //    }
        //}


    }
}
