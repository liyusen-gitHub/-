using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;


namespace MZ_MES_MAIN
{
    [XmlRootAttribute("RESULT", IsNullable = false)]
    public class ResultWrap
    {
        public ResultWrap() { }
        public ResultWrap(EnumResultFlagType flag)
        {
            this.SIGN = flag;
        }
        public ResultWrap(EnumResultFlagType flag, String message)
        {
            this.SIGN = flag;
            this.MESSAGE = message;
        }
        public ResultWrap(EnumResultFlagType flag, String message, EnumErrorCode errorCode)
        {
            this.SIGN = flag;
            this.MESSAGE = message;
            this.ERRORCODE = errorCode;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [XmlElementAttribute("SIGN", IsNullable = false)]
        public EnumResultFlagType SIGN = EnumResultFlagType.N;
        //Nullable
        /// <summary>
        /// 数据(例如从SAP生成返回的单号)
        /// </summary>
        [XmlElementAttribute("DATA", IsNullable = false)]
        public String Data = String.Empty;

        /// <summary>
        /// 信息
        /// </summary>
        [XmlElementAttribute("MESSAGE", IsNullable = false)]
        public String MESSAGE = String.Empty;

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElementAttribute("ERRORCODE", IsNullable = false)]
        public EnumErrorCode ERRORCODE = EnumErrorCode.无错误;

        public override string ToString()
        {
            String result = String.Empty;
            MemoryStream ms = null;
            XmlSerializerNamespaces xmlSerializerNamespaces = null;
            XmlWriterSettings setting = null;
            XmlSerializer xmlSer = null;
            try
            {
                using (ms = new MemoryStream())
                {
                    xmlSer = new XmlSerializer(this.GetType());
                    xmlSerializerNamespaces = new XmlSerializerNamespaces();
                    xmlSerializerNamespaces.Add("", "");
                    setting = new XmlWriterSettings();
                    setting.Encoding = new UTF8Encoding(false);
                    setting.Indent = false;
                    using (XmlWriter writer = XmlWriter.Create(ms, setting))
                    {
                        xmlSer.Serialize(writer, this, xmlSerializerNamespaces);
                    }
                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception exc)
            {
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }
            }
            return result;
        }
    }


    [XmlRootAttribute("RESULT", IsNullable = false)]
    public class ResultError
    {
        public ResultError() { }
        public ResultError(EnumResultFlagType flag)
        {
            this.Error = flag;
        }
        public ResultError(EnumResultFlagType flag, String message)
        {
            this.Error = flag;
            this.MESSAGE = message;
        }
        public ResultError(EnumResultFlagType flag, String message, EnumErrorCode errorCode)
        {
            this.Error = flag;
            this.MESSAGE = message;
            this.ERRORCODE = errorCode;
        }
        /// <summary>
        /// 标识
        /// </summary>
        [XmlElementAttribute("ERROR", IsNullable = false)]
        public EnumResultFlagType Error = EnumResultFlagType.N;

        /// <summary>
        /// 数据(例如从SAP生成返回的单号)
        /// </summary>
        [XmlElementAttribute("DATA", IsNullable = false)]
        public String Data = String.Empty;

        /// <summary>
        /// 信息
        /// </summary>
        [XmlElementAttribute("MESSAGE", IsNullable = false)]
        public String MESSAGE = String.Empty;

        /// <summary>
        /// 错误代码
        /// </summary>
        [XmlElementAttribute("ERRORCODE", IsNullable = false)]
        public EnumErrorCode ERRORCODE = EnumErrorCode.无错误;

        public override string ToString()
        {
            String result = String.Empty;
            MemoryStream ms = null;
            try
            {
                using (ms = new MemoryStream())
                {
                    XmlSerializer xmlSer = new XmlSerializer(this.GetType());
                    xmlSer.Serialize(ms, this);
                    result = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception exc)
            {
                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }

            }
            return result;
        }
    }
}
