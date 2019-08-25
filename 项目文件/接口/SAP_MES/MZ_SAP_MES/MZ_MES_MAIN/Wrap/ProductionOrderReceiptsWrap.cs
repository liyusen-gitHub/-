using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MZ_MES_MAIN.Wrap
{
    [XmlRootAttribute("HEAD", Namespace = "", IsNullable = false)]
   public class ProductionOrderReceiptsWrap
    {
      /// <summary>
      /// 生产订单号
      /// </summary>
        [XmlElementAttribute(IsNullable = false, Order = 1)]
        [FieldVerificationAttribute(FieldLength = 25)]
        public string AUFNR{get;set;}
        /// <summary>
        /// 移动类型代码
        /// </summary>
        [XmlElementAttribute(IsNullable = false, Order = 2)]
        [FieldVerificationAttribute(FieldLength = 25)]
        public string BWART{get;set;}
        /// <summary>
        /// 特殊库存
        /// </summary>
        /// 
        [XmlElementAttribute(IsNullable = false, Order = 3)]
        [FieldVerificationAttribute(FieldLength = 25)]
        public string SOBKZ{get;set;}
        /// <summary>
        /// 过账日期
        /// </summary>
        /// 
        [XmlElementAttribute(IsNullable = false, Order = 4)]
        [FieldVerificationAttribute(FieldLength = 25)]
        public string BUDAT{get;set;}
        /// <summary>
        /// 凭证日期
        /// </summary>
        /// 
        [XmlElementAttribute(IsNullable = false, Order = 5)]
        [FieldVerificationAttribute(FieldLength = 25)]
        public string BLDAT { get; set; }

        /// <summary>
        /// 订单明细
        /// </summary>
        [XmlElementAttribute("ITEM", IsNullable = false, Type = typeof(ProductionOrderReceiptsDetailWrap), Order = 6)]
        public ProductionOrderReceiptsDetailWrap[] ReceiptsDetail = null;


    }

   public class ProductionOrderReceiptsDetailWrap
    { 
        /// <summary>
        /// 行项目
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 1)]
       [FieldVerificationAttribute(FieldLength = 25)]
        public string EBELP{ get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 2)]
       [FieldVerificationAttribute(FieldLength = 100)]
        public string MATNR{ get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 3)]
       [FieldVerificationAttribute(FieldLength = 100)]
        public string CHARG{ get; set; }
        /// <summary>
        /// 库存地代码
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 4)]
       [FieldVerificationAttribute(FieldLength = 25)]
        public string LGORT{ get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 5)]
       [FieldVerificationAttribute(FieldLength = 25)]
        public string MENGE{ get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 6)]
       [FieldVerificationAttribute(FieldLength = 25)]
        public string WERKS{ get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// 
       [XmlElementAttribute(IsNullable = false, Order = 7)]
       [FieldVerificationAttribute(FieldLength = 25)]
        public string MEINS { get; set; }

    }
}
