using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace SAP_DataTo_SQL
{
    public class SqlHelper
    {
        string con = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString.ToString();

        /// <summary>
        /// 采购订单出库回传
        /// </summary>
        /// <returns>返回dataset</returns>
        public DataSet Return_Of_Purchase_Order(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("SELECT VBELN AS EBELN,BWART,SOBKZ,''AS ZWLGRNUM,BEDAT AS BUDAT,BEDAT AS BLDAT FROM PurchaseOrderSynchronization WHERE PurchaseOrderSynchronizationId='{0}'", Id);
                    SqlCommand com = new SqlCommand();
                    com.CommandText = SQL1;
                    com.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;
                    adapter.Fill(DS, "PurchaseOrderSynchronization");

                    string SQL2 = string.Format("SELECT EBELP,MATNR,CHARG,LGORT,MENGE,'' as ZMENGE,WERKS,MEINS FROM PurchaseOrderSynchronizationItem JOIN PurchaseOrderSynchronization ON PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId=PurchaseOrderSynchronization.PurchaseOrderSynchronizationId WHERE PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand();
                    com2.Connection = conn;
                    com2.CommandText = SQL2;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = com2;
                    adapter2.Fill(DS, "PurchaseOrderSynchronizationItem");
                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 转储订单出库回传
        /// </summary>
        /// <returns></returns>
        public int DumpTheOrderOutboundReturn(string PurchaseOrderSynchronizationId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select distinct VBELN,BWART,SOBKZ,BEDAT from PurchaseOrderSynchronization join PurchaseOrderSynchronizationItem on PurchaseOrderSynchronization.PurchaseOrderSynchronizationId=PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId where PurchaseOrderSynchronization.PurchaseOrderSynchronizationId='{0}'", PurchaseOrderSynchronizationId);
                    SqlCommand com = new SqlCommand();
                    com.CommandText = SQL;
                    com.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter(com);
                    DataTable Dt = new DataTable();
                    adapter.Fill(Dt);
                    string SQL1 = string.Format("select EBELP,MATNR,CHARG,LGORT,MENGE,WERKS,MEINS from PurchaseOrderSynchronization join PurchaseOrderSynchronizationItem on PurchaseOrderSynchronization.PurchaseOrderSynchronizationId=PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId where PurchaseOrderSynchronization.PurchaseOrderSynchronizationId='{0}'", PurchaseOrderSynchronizationId);
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = SQL1;
                    comm.Connection = conn;
                    SqlDataAdapter adapter1 = new SqlDataAdapter(comm);
                    DataTable DT = new DataTable();
                    adapter1.Fill(DT);
                    SqlBulkCopy DtCopy = new SqlBulkCopy(con);
                    DtCopy.BatchSize = Dt.Rows.Count;
                    DtCopy.ColumnMappings.Add("PurchaseOrderSynchronizationId", "PurchaseOrderSynchronizationId");
                    DtCopy.ColumnMappings.Add("VBELN", "VBELN");
                    DtCopy.ColumnMappings.Add("BWART", "BWART");
                    DtCopy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                    DtCopy.ColumnMappings.Add("BEDAT", "BEDAT");
                    DtCopy.WriteToServer(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从数据库获取生产订单发货信息
        /// </summary>
        /// <param name="Id">生产订单发货表Id</param>
        /// <returns></returns>
        public DataSet Get_Order_Delivery(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    DataSet Ds = new DataSet();
                    //一级菜单表
                    string SQl1 = string.Format("Select ProductOrderId,OrdId,ProductType,ProductOrderNO,CustomerName,Company from ProductOrder where ProductOrderId='{0}'", Id);
                    SqlCommand com1 = new SqlCommand();
                    com1.Connection = conn;
                    com1.CommandText = SQl1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com1;
                    adapter.Fill(Ds, "ProductOrder");
                    //二级菜单表
                    string SQl2 = string.Format("Select OrderDetailId,ProductOrderId,ProductDescription,Wide,High,Deth,Qty from OrderDetail where ProductOrderId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand();
                    com2.Connection = conn;
                    com2.CommandText = SQl2;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = com2;
                    adapter2.Fill(Ds, "OrderDetail");

                    return Ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 销售出库回传接口
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Sell_The_Outbound_Return(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    String SQl1 = string.Format("SELECT VBELN AS VBELN_IM,BWART,KUNNR AS ZWLGINUM,ZYJCHSJ AS BUDAT FROM SellOutOfTheWarehouseTable WHERE SellOutOfTheWarehouseId='{0}'", Id);
                    DataSet set = new DataSet();
                    SqlCommand com = new SqlCommand(SQl1, conn);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;
                    adapter.Fill(set, "SellOutOfTheWarehouseTable");

                    string SQl2 = string.Format("SELECT POSNR,MATNR,LGORT,CHARG,LFIMG,MEINS FROM Items WHERE SellOutOfTheWarehouseId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand(SQl2, conn);
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = com2;
                    adapter2.Fill(set, "Items");

                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 订单冲销接口
        /// </summary>
        /// <param name="Id">订单冲销Id</param>
        /// <returns></returns>
        public DataSet Orders_For_Sterilisation(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL1 = string.Format("select BS,DATA,MBLNR,MJAHR,VBELN,VSTEL FROM Items where VBELN='{0}'", Id);
                    DataSet SET = new DataSet();
                    SqlCommand COM = new SqlCommand(SQL1, conn);
                    SqlDataAdapter ADAPTER = new SqlDataAdapter();
                    ADAPTER.SelectCommand = COM;
                    ADAPTER.Fill(SET);
                    return SET;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 其他出库回传接口
        /// </summary>
        /// <param name="Id">回传Id</param>
        /// <returns></returns>
        public DataSet Other_Outbound_Returns(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQl1 = string.Format("SELECT VBELN,'' AS ZWLGINUM,WERKS,BWART,SOBKZ,Ctime AS BUDAT FROM Other_Outbound_Shipments_Table WHERE Other_Outbound_Shipments_TableId='{0}'", Id);
                    DataSet DS = new DataSet();

                    SqlCommand com1 = new SqlCommand();
                    com1.CommandText = SQl1;
                    com1.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com1;
                    adapter.Fill(DS, "Other_Outbound_Shipments_Table");

                    string SQL2 = string.Format("SELECT POSNR,MATNR,MAKTX,LGORT,CHARG,LFIMG,MEINS FROM Other_Outbound_Shipments_Item WHERE Other_Outbound_Shipments_TableId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand();
                    com2.CommandText = SQL2;
                    com2.Connection = conn;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter.SelectCommand = com2;
                    adapter.Fill(DS, "Other_Outbound_Shipments_Item");

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 采购申请创建
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet The_Purchasing_Requisition(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    //,a.ProductDescription as 物料描述
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("  select 'NB'as BSART,d.Text as ITEXT1,10 as BNFPO,a.ProductName as MATNR, a.PurchaseQty as MENGE,a.PurchaseUnit as MEINS,'6006' as WERKS,c.WareHouseCode as LGORT, ''as FIFNR,''as FLTEF,a.RequestArriveDate as LFDAT,'6006'as EKORG  from PurchaseDetails a inner join LocationMaterialRelation b on a.ProductName=b.MaterialNumber  inner join WareHouseManage c on b.LocationName=c.WareHouseName  inner join PurchaseApply d on a.PurchaseApplyId=d.PurchaseApplyId  where a.PurchaseApplyId='{0}'", Id);
                    SqlCommand com1 = new SqlCommand();
                    com1.CommandText = SQL1;
                    com1.Connection = conn;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = com1;
                    adapter1.Fill(DS, "PurchaseApply");

                    //string SQL2 = string.Format("SELECT '' AS BNFPO,PRODUCTNAME AS MATNR,PURCHASEQTY AS MENGE,PURCHASEUNIT AS MEINS,FACTORY AS WERKS,STORELOCATION AS LGORT,VENDORNO AS LIFNR,FIXEDVENDOR AS FLIEF,REQUESTARRIVEDATE AS LFDAT,PURCHASETISSUE AS EKORG FROM purchasedetails WHERE PurchaseApplyId='{0}'", Id);
                    //SqlCommand com2 = new SqlCommand();
                    //com2.CommandText = SQL2;
                    //com2.Connection = conn;
                    //SqlDataAdapter adapter2 = new SqlDataAdapter();
                    //adapter2.SelectCommand = com2;
                    //adapter2.Fill(DS, "purchasedetails");
                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 采购申请成功后保存SAP返回的信息
        /// </summary>
        /// <returns></returns>
        public int SaveThe_Purchasing_Requisition()
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(con))
                {
                    string SQL = string.Format("");
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// 生产订单发货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Order_Delivery(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,'' AS BUDAT,'' AS BLDAT,'CC60062001' as KOSTL from ProductOrder where ProductOrderNo='{0}' ", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select distinct '' as EBELP, GetMaterialbills.ProductName as MATNR,'' AS CHARG,'1001' AS LGORT, SUM(QTY) AS MENGE,'6006' AS WERKS,UNIT AS MEINS FROM GetMaterialbills join ProductOrder on GetMaterialbills.ProductOrderId=ProductOrder.ProductOrderId where ProductOrder.ProductOrderNO='S300134170' and  GetMaterialbills.ProductName not in( 'JJJ-XS-TJB002','JJL-XS000006',   'JWJ-LS000343','JJL-XS000007','JJJ-XS-HX002','JWJ-JC000406','JJL-BL000007','JWJ-JC000559','JWJ-JC000225','JWJ-JC000134','JWJ-JC000258','JBC-BH250029','JSM-PM000008','JJL-QT000009','JJJ-XS-ZJ003','JFB-PC102239','JFB-PC102920','JWJ-JC000233','JBC-MD250009','JJJ-XS-FK001','JWJ-JC000382','JLC-YG000006','JBC-BH120028','JWJ-JC000264','JWJ-JC000392')group by GetMaterialbills.ProductName,GetMaterialbills.Unit", Id);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");
                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 把生成的凭证保存到数据库（生产订单发货）
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Outbound_Credentials"></param>
        /// <returns></returns>
        public int ReturnNum(string Id,string Outbound_Credentials)
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(con))
                {
                    string SQL =string.Format( "Update ProductOrder set Outbound_Credentials='{0}' where ProductOrderNO='{1}'",Outbound_Credentials,Id);
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    return comm.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生产订单收货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Production_Order_Receiving(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNO as AUFNR,101 as BWART,'' as SOBKZ,getdate(),getdate() from productorder where productorderid='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = SQL1;
                    comm.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select '' as EBELP,productname as MATNR,''as CHARG,3001 as LGORT,1 as MENGE,6006 as WERKS,'套' as MEINS from ProductOrder where productorderid='{0}'", Id);
                    SqlCommand comm1 = new SqlCommand();
                    comm1.CommandText = SQL2;
                    comm1.Connection = conn;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm1;
                    adapter1.Fill(DS, "GetMaterialBills");
                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 库内转移接口
        /// </summary>
        /// <returns></returns>
        public DataSet Transfer_The_Rolls(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ZWLTRNUM,BWART,BUDAT,WERKS FROM Transfer_The_Rolls WHERE Transfer_The_RollsId='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = SQL1;
                    comm.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "Transfer_The_Rolls");

                    string SQL2 = string.Format("select MATNR,UMMAT,LIFNR,LGORT,FCSOBKZ,FCPSPID,UMLGO,JSSOBKZ,JSPSPID,CHARG,UMCHA,MENGE,FCVBELN,JSVBELN,INSMK,BESTQ,ZGRUND FROM Transfer_The_Rolls_KNZYLN WHERE Transfer_The_RollsId='{0}'", Id);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = comm2;
                    adapter2.Fill(DS);
                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 撤销销售出库下发数据
        /// </summary>
        /// <param name="VBELN"></param>
        /// <returns></returns>
        public int Cancellation_Issued(string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("select SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId from Items join  SellOutOfTheWarehouseTable on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId where SellOutOfTheWarehouseTable.VBELN='{0}'", VBELN);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    object SellOutOfTheWarehouseId = comm.ExecuteScalar();

                    string sql2 = string.Format("DELETE FROM SellOutOfTheWarehouseTable WHERE SellOutOfTheWarehouseId='{0}' and ZCHANGE_IND='Q'", SellOutOfTheWarehouseId);
                    string sql3 = string.Format("DELETE FROM Items  WHERE SellOutOfTheWarehouseId='{0}'", SellOutOfTheWarehouseId);
                    comm.CommandText = sql2;
                    int a = comm.ExecuteNonQuery();
                    comm.CommandText = sql3;
                    int b = comm.ExecuteNonQuery();
                    if (a > 0 && b > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 撤销采购订单下发数据
        /// </summary>
        /// <param name="VBELN"></param>
        /// <returns></returns>
        public int Cancel_The_Purchase_Order(string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("delete from PurchaseOrderSynchronization where VBELN='{0}'", VBELN);

                    SqlCommand comm = new SqlCommand();

                    string sql2 = string.Format("delete from PurchaseOrderSynchronizationItem where VBELN='{0}'", VBELN);
                    string sql3 = string.Format("delete from Dump_The_Order_Outbound_ReturnTable where EBELN='{0}'", VBELN);
                    string sql4 = string.Format("delete from Dump_The_Order_Outbound_ReturnekpoTable where VBELN='{0}'", VBELN);
                    string sql5 = string.Format("delete from Purchase_RequisitionTable where EBELN='{0}'", VBELN);
                    string sql6 = string.Format("delete from Purchase_RequisitionTablekpoTable where VBELN='{0}'", VBELN);
                    List<String> list = new List<string>();
                    list.Add(SQL);
                    list.Add(sql2);
                    list.Add(sql3);
                    list.Add(sql4);
                    list.Add(sql5);
                    list.Add(sql6);

                    comm.Connection = conn;
                    int a = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        comm.CommandText = list[i].ToString();
                        if(comm.ExecuteNonQuery()>0)
                        {
                            a++;
                        }
                    }
                    //comm.CommandText = SQL;
                    //object SellOutOfTheWarehouseId = comm.ExecuteScalar();
                    //comm.CommandText = sql2;
                    //int a = comm.ExecuteNonQuery();
                    //comm.CommandText = sql3;
                    //int b = comm.ExecuteNonQuery();
                    if (a == 6)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        



    }
}
