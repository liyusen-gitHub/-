using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MZ_MES_MAIN.MM_MZDemo;
using System.IO;
namespace MZ_MES_MAIN.Common
{
    public class ProductionOrderReceiptSender
    {
                        private String endPointName = "binding_SOAP12";
        private String user = String.Empty;
        private String _syncName = String.Empty;
 
        public ProductionOrderReceiptSender(String user)
        {
            this.user = user;
        }
        public ProductionOrderReceiptSender(String user, String syncName)
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
           
            ZWS_MZClient sap_ws = null;
            ZFUN_MZ sap_Obj = null;
            ZFUN_MZResponse sap_ObjResponse = null;
            String str = String.Empty;
            Utils utils = new Utils();
            try
            {
                str =utils.GetSAPRequestData(requestObj);
                sap_Obj = new ZFUN_MZ();
                sap_Obj.FCODE = EnumFCODE.ZFUN_PO_GOODS_IN_MZ;
                sap_Obj.INPUT = str;
                using (sap_ws = new ZWS_MZClient(this.endPointName))
                {
                    //System.ServiceModel.Channels.Binding bind=
                   
                    sap_ObjResponse = sap_ws.ZFUN_MZ(sap_Obj);

                    if (String.IsNullOrEmpty(sap_ObjResponse.OUTPUT))
                    {
                        throw new InvalidDataException("无效的SAP返回数据!");
                    }
                    try
                    {
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
