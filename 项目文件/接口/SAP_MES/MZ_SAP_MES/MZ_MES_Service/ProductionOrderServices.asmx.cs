﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MZ_MES_Service
{
    /// <summary>
    /// ProductionOrderServices 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ProductionOrderServices : System.Web.Services.WebService
    {

        /// <summary>
        /// 对接SAP，实现MES系统自动生产订单入库操作
        /// </summary>
        /// <param name="sOrder">生产订单编号</param>
        /// <returns>返回是否成功标志</returns>
        [WebMethod(EnableSession =true,Description ="对接生产订单")]
        public string GetProductionOrderReceiptSapResult(string sOrder)
        {
            return "";
        }

        /// <summary>
        /// 对接SAP，实现MES系统自动生产订单入库操作
        /// </summary>
        /// <param name="sOrder">生产订单编号</param>
        /// <returns>返回是否成功标志</returns>
        [WebMethod(EnableSession = true, Description = "对接生产订单")]
        public string GetProductionOrderDeliverySapResult(string sOrder)
        {
            return "";
        }
    }
}
