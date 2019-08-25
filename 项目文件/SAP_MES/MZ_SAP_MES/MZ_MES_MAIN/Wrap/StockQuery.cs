using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MZ_MES_MAIN
{
     [XmlRootAttribute("HEAD", Namespace = "", IsNullable = false)]
    public class StockQuery
    {
        /// <summary>
        /// 物料信息
        /// </summary>
        [XmlElementAttribute("ITEM", Type = typeof(StockQueryDetail), IsNullable = false)]
         public StockQueryDetail[] QueryDetail = null;
    }

    public class StockQueryDetail
    {
        /// <summary>
        /// 物料编码开始
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsRequired = true)]
       public string FROM_MATNR{get;set;}
        /// <summary>
        /// 物料编码结束
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsRequired = false)]
       public string TO_MATNR{get;set;}
        /// <summary>
        /// 工厂代码开始
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50,IsNumberic=true,IsRequired = false)]
        public string FROM_PLANT{get;set;}
        /// <summary>
        /// 工厂代码结束
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsNumberic = true, IsRequired = false)]
        public string TO_PLANT{get;set;}
        /// <summary>
        /// 存储位置开始
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsRequired = false)]
        public string FROM_LGORT{get;set;}
        /// <summary>
        /// 存储位置结束
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsRequired = false)]
        public string TO_LGORT{get;set;}
        /// <summary>
        /// 批次开始
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50, IsRequired = false)]
        public string FROM_BATCH{get;set;}
        /// <summary>
        /// 批次结束
        /// </summary>
        [FieldVerificationAttribute(FieldLength = 50,IsRequired = false)]
        public string TO_BATCH{get;set;}
    }








}
