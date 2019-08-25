using Production_Order_Receiving.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Production_Order_Receiving
{
    public static class Production_Order_Receiving
    {
        public static String Return(string Id)
        {
            try
            {
                Id = "PROD000000GK";
                //销售订单出库回传服务对象
                ServiceReference1.ZWS_MZClient Client = null;
                //传入的字符串对象
                ZFUN_MZ OBJ_Str = null;
                //SAP输出对象
                ZFUN_MZResponse ObjResponse = null;
                SqlHelper Helper = new SqlHelper();
                DataSet Set = Helper.Production_Order_Receiving(Id);
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
                    OBJ_Str.FCODE = "7";
                    OBJ_Str.INPUT = Str;
                    using (Client = new ServiceReference1.ZWS_MZClient("binding_SOAP12"))
                    {
                        ObjResponse = Client.ZFUN_MZ(OBJ_Str);
                        if (String.IsNullOrEmpty(ObjResponse.OUTPUT))
                        {
                            return "无效的SAP返回数据!";
                        }
                        try
                        {
                            return ObjResponse.ToString();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    return "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Response><SIGN>Y</SIGN><MESSAGE>成功</MESSAGE></Response>";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
