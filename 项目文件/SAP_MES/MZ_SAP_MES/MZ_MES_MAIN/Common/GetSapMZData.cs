using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MZ_MES_MAIN.MM_MZDemo;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using log4net.Config;

namespace MZ_MES_MAIN
{
    public class GetSapMZData
    {
        private String endPointName = "binding_SOAP12";
        private String user = String.Empty;
        private String _syncName = String.Empty;
 
        public GetSapMZData(String user)
        {
            this.user = user;
        }
        public GetSapMZData(String user, String syncName)
        {
            this._syncName = syncName;
            this.user = user;
        }

        /// <summary>
        /// 从ERP把PS数据推给SAP并从SAP获取推送结果
        /// </summary>
        /// <param name="requestObj">要推送的PS数据</param>
        /// <param name="endPointName">端点名称</param>
        /// <returns></returns>
        public  Object SendDataToSAP(Object requestObj)
        {
            Object rw = null;
            //服务对象
            ZWS_MZClient sap_ws = null;
            //传入的字符串对象
            ZFUN_MZ sap_Obj = null;
            //输出对象
            ZFUN_MZResponse sap_ObjResponse = null;
            String str = String.Empty;
            Utils utils = new Utils();
            try
            {
                //str为处理后的XML字符串
                str =utils.GetSAPRequestData(requestObj);
                sap_Obj = new ZFUN_MZ();
                sap_Obj.FCODE = EnumFCODE.ZFUN_MM01_MES_MZ;
                sap_Obj.INPUT = str;
                using (sap_ws = new ZWS_MZClient(this.endPointName))
                {
                    sap_ObjResponse = sap_ws.ZFUN_MZ(sap_Obj);

                    if (String.IsNullOrEmpty(sap_ObjResponse.OUTPUT))
                    {
                        throw new InvalidDataException("无效的SAP返回数据!");
                    }
                    try
                    {
                        //byte[] array = Encoding.UTF8.GetBytes(sap_ObjResponse.OUTPUT);
                        //MemoryStream stream = new MemoryStream(array);
                        //DataSet ds = new DataSet();
                        //ds.ReadXml(stream, XmlReadMode.Auto);
                        //var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
                        //XmlConfigurator.ConfigureAndWatch(logCfg);
                        //var logger =log4net.LogManager.GetLogger(typeof(GetSapMZData));
                        //logger.Info(sap_ObjResponse.OUTPUT);
                        rw = new ResultWrap() { SIGN=EnumResultFlagType.Y, MESSAGE="木作数据抓取成功", Data=sap_ObjResponse.OUTPUT };
                        return rw;
                    }
                    catch (InvalidOperationException ex)
                    {
                        throw ex;
                        //rw = base.GetSAPResponseData(sap_ObjResponse.OUTPUT, typeof(ResultWrap));
                    }
                }
            }
            catch (Exception exc)
            {
                rw = null;
                if (sap_ws != null)
                {
                    sap_ws.Abort();
                }
                throw exc;
            }
            finally
            {
                if (sap_ws != null)
                {
                    sap_ws.Close();
                }
            }
        }




    }
}
