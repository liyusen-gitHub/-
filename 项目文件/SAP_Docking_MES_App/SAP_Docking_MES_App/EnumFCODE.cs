using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAP_Docking_MES_App
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
        /// <summary>
        /// 采购订单出库回传
        /// </summary>
        public const string ZFUN_WM00005 = "9";
        /// <summary>
        /// 销售订单出库回传
        /// </summary>
        public const string ZFUN_WM00007 = "10";
        /// <summary>
        /// 其它出库回传
        /// </summary>
        public const string ZFUN_WM00011 = "11";
        /// <summary>
        /// 采购需求下发
        /// </summary>
        public const string ZFUN_WM00012 = "12";
        /// <summary>
        /// 订单冲销
        /// </summary>
        public const string ZFUN_WM00013 = "13";
        /// <summary>
        /// 库内转移
        /// </summary>
        public const string ZFUN_WM00014 = "14";
        /// <summary>
        /// 取消下发
        /// </summary>
        public const string ZFUN_WM00015 = "15";
        /// <summary>
        /// 结算金额
        /// </summary>
        public const string ZFUN_WM00016 = "16";
    }
}