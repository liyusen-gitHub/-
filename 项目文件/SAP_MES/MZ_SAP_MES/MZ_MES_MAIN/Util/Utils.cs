using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace MZ_MES_MAIN
{
    public class Utils
    {

        private XmlUtil xmlUtil = new XmlUtil();
        /// <summary>
        /// 获取推给SAP数据
        /// </summary>
        /// <param name="obj">业务实体对象</param>
        /// <returns>包含业务实体对象数据的XML</returns>
        public  String GetSAPRequestData(Object obj)
        {
            String result = String.Empty;
            MemoryStream ms = null;
            XmlSerializer mySerializer = null;
            ResultWrap rw = null;
            XmlSerializerNamespaces xmlSerializerNamespaces = null;
            XmlWriterSettings setting = null;
            try
            {
                rw = xmlUtil.VerificationRequestData(obj);
                if (rw.SIGN != EnumResultFlagType.Y)
                {
                    throw new ArgumentException("obj参数校验失败: " + rw.MESSAGE);
                }
                xmlSerializerNamespaces = new XmlSerializerNamespaces();
                xmlSerializerNamespaces.Add("", "");
                obj = xmlUtil.FormatRequestObjData(obj);
                setting = new XmlWriterSettings();
                setting.Encoding = new UTF8Encoding(false);
                setting.Indent = false;
                mySerializer = new XmlSerializer(obj.GetType());
                using (ms = new MemoryStream())
                {
                    using (XmlWriter writer = XmlWriter.Create(ms, setting))
                    {
                        mySerializer.Serialize(writer, obj, xmlSerializerNamespaces);
                    }
                    result = Encoding.UTF8.GetString(ms.ToArray());
                    //result = formatRequestStrData(result);    
                    ms.Close();
                }
            }
            catch (Exception exc)
            {
                result = String.Empty;
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
                throw exc;
            }
            return result;
        }

        public DataSet GetDataByXML(string xmlString)
        {
            byte[] array = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream stream = new MemoryStream(array);
            DataSet ds = new DataSet();
            ds.ReadXml(stream, XmlReadMode.Auto);
            return ds;
        }

        public StockQuery GetStockQueryByString(string queryString)
        {
                StockQuery query = new StockQuery();
                var stringSplit = queryString.Split(',');
                StockQueryDetail[] detail = new StockQueryDetail[stringSplit.Length];
                int i = 0;
                foreach (var item in stringSplit)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        detail[i] = new StockQueryDetail();
                        detail[i].FROM_MATNR = item;
                        detail[i].TO_MATNR = "";
                        detail[i].FROM_PLANT = "";
                        detail[i].TO_PLANT = "";
                        detail[i].FROM_LGORT = "";
                        detail[i].TO_LGORT = "";
                        detail[i].FROM_BATCH = "";
                        detail[i].TO_BATCH = "";
                        i += 1;
                    }
                }
                query.QueryDetail = detail;

            
            return query;
        }
    }
}
