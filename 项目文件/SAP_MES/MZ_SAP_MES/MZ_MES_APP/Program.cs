using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MZ_MES_MAIN;
using log4net.Config;
using System.IO;
using System.Data;

namespace MZ_MES_APP
{
    class Program
    {
        static void Main(string[] args)
        {

            #region 同步木作物料主数据

            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            var logger = log4net.LogManager.GetLogger(typeof(Program));
            
            Boolean result = false;
            DateTime startDt = DateTime.Now.AddDays(-1);
            startDt = new DateTime(startDt.Year, startDt.Month, startDt.Day, 0, 0, 0);
            DateTime endDt = new DateTime(startDt.Year, startDt.Month, startDt.Day, 23, 59, 59);

            //DateTime startDt = DateTime.Parse("2017-10-12 00:00:00");
            //DateTime endDt = DateTime.Parse("2017-10-12 23:59:59");
            //Object tmpObj = null;
            if (args.Length > 0 && args[0].ToUpper().Substring(1) == "MMS")
            {
                startDt = DateTime.Parse(args[1] + " " + args[2]);
                endDt = DateTime.Parse(args[3] + " " + args[4]);
            }
            //构造查询时间段
            MaterialSyncRequestWrap materialObj = new MaterialSyncRequestWrap();
            materialObj.SetStartDatetime(startDt);
            materialObj.SetEndDatetime(endDt);
            try
            {
                GetSapMZData mmSender = new GetSapMZData("同步物料主数据", "MMS");
                string dateString = "程序开始时间:"+DateTime.Now.ToString();
                ResultWrap resultWrap = (ResultWrap)mmSender.SendDataToSAP(materialObj);
                dateString += " SAP接口返回数据时间："+DateTime.Now.ToString();
                //判断接口调用返回正常读取XML结构
                if (resultWrap.SIGN==EnumResultFlagType.Y)
                {
                    byte[] array = Encoding.UTF8.GetBytes(resultWrap.Data);
                    MemoryStream stream = new MemoryStream(array);
                    DataSet ds = new DataSet();
                    ds.ReadXml(stream, XmlReadMode.Auto);
                    dateString += "XML结构转化为DATASET时间：" + DateTime.Now.ToString();
                    //判断返回的XML是有效数据后操作数据
                    if (ds.Tables.Count > 0 && ds.Tables[0].Columns.Count==29)
                    {
                        bool resultBool = MZ_MES_DAL.DataOperation.AddMetralInfo(ds.Tables[0]);          
                    }
                    dateString += "数据保存到MES数据库时间：" + DateTime.Now.ToString();
                    //记录日志
                   logger.Info(resultWrap.Data+dateString+"\r\n\r\n\r\n");                    
                }
                else
                {
                    logger.Info(resultWrap.Data+"\r\n\r\n\r\n");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex+"\r\n\r\n\r\n");
            }
            #endregion
        }
    }
}
