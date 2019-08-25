using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
namespace MZ_MES_MAIN
{
    public class EnumFCODE
    {
        /// <summary>
        /// 生产订单信息同步接口
        /// </summary>
        public const string ZFUN_PP001 = "1";
        /// <summary>
        /// 木作物料主数据
        /// </summary>
        public const string ZFUN_MM01_MES_MZ = "2";
        /// <summary>
        /// 库存查询
        /// </summary>
        public const string ZFUN_KC_MES_MZ = "3";
        /// <summary>
        /// 供应商同步
        /// </summary>
        public const string ZMM_VENDOR_TO_MES_MZ = "4";
        /// <summary>
        /// 采购记录信息同步
        /// </summary>
        public const string ZFUN_MM_INFO_RECORD_MES_MZ = "5";
        /// <summary>
        /// 生产订单收货
        /// </summary>
        public const string ZFUN_PO_GOODS_IN_MZ = "7";
        /// <summary>
        /// 生产订单发货
        /// </summary>
        public const string ZFUN_PO_GOODS_OUT_MZ = "8";

    }

    public enum EnumErrorCode
    {
        /// <summary>
        /// 无错误
        /// </summary>
        [XmlEnumAttribute("0")]
        无错误 = 0,
        /// <summary>
        /// 主键重复错误
        /// </summary>
        [XmlEnumAttribute("1")]
        主键重复错误 = 1,
        /// <summary>
        /// XML解析错误
        /// </summary>
        [XmlEnumAttribute("2")]
        XML解析错误 = 2,

        /// <summary>
        /// 字段错误
        /// </summary>
        [XmlEnumAttribute("3")]
        字段错误 = 3
    }

    public enum EnumProductCargoStateType
    {
        /// <summary>
        /// 备货
        /// </summary>
        [XmlEnumAttribute("备货")]
        备货 = 1,
        /// <summary>
        /// 非备货
        /// </summary>
        [XmlEnumAttribute("非备货")]
        非备货 = 2,
        /// <summary>
        /// 默认值
        /// </summary>
        [XmlEnumAttribute("")]
        默认值 = 3
    }

    public enum EnumProductSourceType
    {
        /// <summary>
        /// 自营
        /// </summary>
        [XmlEnumAttribute("自营")]
        自营 = 1,
        /// <summary>
        /// 非自营
        /// </summary>
        [XmlEnumAttribute("非自营")]
        非自营 = 2,

        /// <summary>
        /// 默认值
        /// </summary>

        [XmlEnumAttribute("")]
        默认值 = 3
    }

    public enum EnumResultFlagType
    {
        /// <summary>
        /// 失败
        /// </summary>
        [XmlEnumAttribute("N")]
        N = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [XmlEnumAttribute("Y")]
        Y = 1,
    }

    /// <summary>
    /// 接口:格式化字符数据
    /// </summary>
    public interface IFormaterObject
    {
        /// <summary>
        /// 格式化字符串数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Object FormaterObjectData(Object data);
    }
}
