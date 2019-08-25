using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SAP_DataTo_SQL
{
    public class SqlHelper
    {
        string con = ConfigurationManager.ConnectionStrings["SqlConn"].ConnectionString.ToString();
        /// <summary>
        /// SAP传过来的数据写入到MES数据库(移动类型下发)
        /// </summary>
        /// <param name="table">传过来的XML转换成的表</param>
        public int SAP_TableTo_SQL(DataTable table)
        {
            using (SqlBulkCopy Copy = new SqlBulkCopy(con))
            {
                try
                {
                    Copy.DestinationTableName = "SAPToMESTable";
                    Copy.BatchSize = table.Rows.Count;
                    Copy.ColumnMappings.Add("Id", "Id");
                    Copy.ColumnMappings.Add("ZBWART", "ZBWART");
                    Copy.ColumnMappings.Add("ZSOBKZ", "ZSOBKZ");
                    Copy.ColumnMappings.Add("ZYDLXMS", "ZYDLXMS");
                    Copy.ColumnMappings.Add("ZGRGIMARK", "ZGRGIMARK");
                    Copy.ColumnMappings.Add("ZJKNAME", "ZJKNAME");
                    Copy.ColumnMappings.Add("ZCHANGE_IND", "ZCHANGE_IND");
                    Copy.ColumnMappings.Add("VERSION", "VERSION");
                    Copy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                    Copy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                    Copy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                    Copy.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                    Copy.WriteToServer(table);
                    if (table.Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    table.Dispose();
                }
            }

        }
        /// <summary>
        /// 工厂库存地下发
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public int Plant_Stock_ToSQL(DataTable table)
        {
            using (SqlBulkCopy Copy = new SqlBulkCopy(con))
            {
                try
                {
                    Copy.DestinationTableName = "Plant_Stock_Table";
                    Copy.BatchSize = table.Rows.Count;
                    Copy.ColumnMappings.Add("Id", "Id");
                    Copy.ColumnMappings.Add("WERKS", "WERKS");
                    Copy.ColumnMappings.Add("NAME1", "NAME1");
                    Copy.ColumnMappings.Add("BUKRS", "BUKRS");
                    Copy.ColumnMappings.Add("BUTXT", "BUTXT");
                    Copy.ColumnMappings.Add("ZLGORT", "ZLGORT");
                    Copy.ColumnMappings.Add("ZLGOBE", "ZLGOBE");
                    Copy.ColumnMappings.Add("SOURCESYSTEMID", "SOURCESYSTEMID");
                    Copy.ColumnMappings.Add("TMSSOURCESYSTEMID", "TMSSOURCESYSTEMID");
                    Copy.ColumnMappings.Add("WLBUKRS", "WLBUKRS");
                    Copy.ColumnMappings.Add("WLNAME", "WLNAME");
                    Copy.ColumnMappings.Add("WLWERKS", "WLWERKS");
                    Copy.ColumnMappings.Add("WLNAME1", "WLNAME1");
                    Copy.ColumnMappings.Add("WLLGORT", "WLLGORT");
                    Copy.ColumnMappings.Add("WLADD", "WLADD");
                    Copy.ColumnMappings.Add("CHANGE_IND", "CHANGE_IND");
                    Copy.ColumnMappings.Add("VERSION", "VERSION");
                    Copy.ColumnMappings.Add("ZUPDDATE", "ZUPDDATE");
                    Copy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                    Copy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                    Copy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                    Copy.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                    Copy.WriteToServer(table);
                    if (table.Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    table.Dispose();
                }
            }
        }


        /// <summary>
        /// SAP传过来的数据写入到MES数据库(销售出库下发)
        /// </summary>
        /// <param name="table">传过来的XML转换成的表</param>
        public int SAP_SellOutOfTheWarehouseTo_SQL(DataTable table, DataTable Table)
        {
            try
            {
                SqlBulkCopy Copy = new SqlBulkCopy(con);
                //销售出库下发表插入SQL数据库
                Copy.DestinationTableName = "SellOutOfTheWarehouseTable";
                Copy.BatchSize = table.Rows.Count;
                Copy.ColumnMappings.Add("SellOutOfTheWarehouseId", "SellOutOfTheWarehouseId");
                Copy.ColumnMappings.Add("VBELN", "VBELN");
                Copy.ColumnMappings.Add("AUART", "AUART");
                Copy.ColumnMappings.Add("WERKS", "WERKS");
                Copy.ColumnMappings.Add("BUKRS", "BUKRS");
                Copy.ColumnMappings.Add("BWART", "BWART");
                Copy.ColumnMappings.Add("ZCHANGE_IND", "ZCHANGE_IND");
                Copy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                Copy.ColumnMappings.Add("ZHSSUM", "ZHSSUM");
                Copy.ColumnMappings.Add("KUNNR", "KUNNR");
                Copy.ColumnMappings.Add("NAME1", "NAME1");
                Copy.ColumnMappings.Add("STRAS", "STRAS");
                Copy.ColumnMappings.Add("ZLXNM", "ZLXNM");
                Copy.ColumnMappings.Add("TELF1", "TELF1");
                Copy.ColumnMappings.Add("PSTLZ", "PSTLZ");
                Copy.ColumnMappings.Add("ZYSFS", "ZYSFS");
                Copy.ColumnMappings.Add("ZYJCHSJ", "ZYJCHSJ");
                Copy.ColumnMappings.Add("ZXSVBELN", "ZXSVBELN");
                Copy.ColumnMappings.Add("ZXSHDTXT", "ZXSHDTXT");
                Copy.ColumnMappings.Add("ZPSPID", "ZPSPID");
                Copy.ColumnMappings.Add("EBELN", "EBELN");
                Copy.ColumnMappings.Add("ZSJS", "ZSJS");
                Copy.ColumnMappings.Add("ZYFCD", "ZYFCD");
                Copy.ColumnMappings.Add("ZSTO", "ZSTO");
                Copy.ColumnMappings.Add("ZPRCTR", "ZPRCTR");
                Copy.ColumnMappings.Add("IHREZ", "IHREZ");
                Copy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                Copy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                Copy.ColumnMappings.Add("ZFSHENG", "ZFSHENG");
                Copy.ColumnMappings.Add("ZFSHI", "ZFSHI");
                Copy.ColumnMappings.Add("ZFQU", "ZFQU");
                Copy.ColumnMappings.Add("ZFHDZ", "ZFHDZ");
                Copy.ColumnMappings.Add("ZSSHENG", "ZSSHENG");
                Copy.ColumnMappings.Add("ZSSHI", "ZSSHI");
                Copy.ColumnMappings.Add("ZSQU", "ZSQU");
                Copy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                Copy.ColumnMappings.Add("ZMMI001", "ZMMI001");
                Copy.ColumnMappings.Add("ZMMI002", "ZMMI002");
                Copy.ColumnMappings.Add("ZMMI003", "ZMMI003");
                Copy.WriteToServer(table);
                SqlBulkCopy ItemsCopy = new SqlBulkCopy(con);
                //Items表插入到数据库
                ItemsCopy.DestinationTableName = "Items";
                ItemsCopy.BatchSize = Table.Rows.Count;
                ItemsCopy.ColumnMappings.Add("ItemsId", "ItemsId");
                ItemsCopy.ColumnMappings.Add("SellOutOfTheWarehouseId", "SellOutOfTheWarehouseId");
                ItemsCopy.ColumnMappings.Add("POSNR", "POSNR");
                ItemsCopy.ColumnMappings.Add("MATNR", "MATNR");
                ItemsCopy.ColumnMappings.Add("MAKTX", "MAKTX");
                ItemsCopy.ColumnMappings.Add("LGORT", "LGORT");
                ItemsCopy.ColumnMappings.Add("CHARG", "CHARG");
                ItemsCopy.ColumnMappings.Add("LFIMG", "LFIMG");
                ItemsCopy.ColumnMappings.Add("MEINS", "MEINS");
                ItemsCopy.ColumnMappings.Add("ENWRT", "ENWRT");
                ItemsCopy.ColumnMappings.Add("BKLAS", "BKLAS");
                ItemsCopy.ColumnMappings.Add("ZSYWZ", "ZSYWZ");
                ItemsCopy.ColumnMappings.Add("MATKL", "MATKL");
                ItemsCopy.ColumnMappings.Add("ZZHONGL", "ZZHONGL");
                ItemsCopy.ColumnMappings.Add("ZVBELN", "ZVBELN");
                ItemsCopy.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                ItemsCopy.ColumnMappings.Add("ZMEMO4", "ZMEMO4");
                ItemsCopy.ColumnMappings.Add("BS", "BS");
                ItemsCopy.ColumnMappings.Add("DATA", "DATA");
                ItemsCopy.ColumnMappings.Add("MBLNR", "MBLNR");
                ItemsCopy.ColumnMappings.Add("MJAHR", "MJAHR");
                ItemsCopy.ColumnMappings.Add("VBELN", "VBELN");
                ItemsCopy.ColumnMappings.Add("VSTEL", "VSTEL");

                ItemsCopy.WriteToServer(Table);
                if (table.Rows.Count > 0 && Table.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                table.Dispose();
                Table.Dispose();
            }
        }
        /// <summary>
        /// 物料主数据接口
        /// </summary>
        /// <param name="table">XML转换的Table</param>
        /// <returns>返回导入到MES是否成功 1：成功  0：失败</returns>
        public int MaterialMasterData(DataTable table)
        {
            using (SqlBulkCopy Copy = new SqlBulkCopy(con))
            {
                try
                {
                    Copy.DestinationTableName = "MaterialMasterData";
                    Copy.BatchSize = table.Rows.Count;
                    Copy.ColumnMappings.Add("AUSME", "AUSME");
                    Copy.ColumnMappings.Add("BESKZ", "BESKZ");
                    Copy.ColumnMappings.Add("BKLAS", "BKLAS");
                    Copy.ColumnMappings.Add("BRGEW", "BRGEW");
                    Copy.ColumnMappings.Add("CHANGE_IND", "CHANGE_IND");
                    Copy.ColumnMappings.Add("DISLS", "DISLS");
                    Copy.ColumnMappings.Add("DISMM", "DISMM");
                    Copy.ColumnMappings.Add("DISPO", "DISPO");
                    Copy.ColumnMappings.Add("DZEIT", "DZEIT");
                    Copy.ColumnMappings.Add("EISBE", "EISBE");
                    Copy.ColumnMappings.Add("EKGRP", "EKGRP");
                    Copy.ColumnMappings.Add("GEWEI", "GEWEI");
                    Copy.ColumnMappings.Add("MABST", "MABST");
                    Copy.ColumnMappings.Add("MAKTX", "MAKTX");
                    Copy.ColumnMappings.Add("MaterialMasterDataId", "MaterialMasterDataId");
                    Copy.ColumnMappings.Add("MATKL", "MATKL");
                    Copy.ColumnMappings.Add("MATNR", "MATNR");
                    Copy.ColumnMappings.Add("MEINS", "MEINS");
                    Copy.ColumnMappings.Add("MTVFP", "MTVFP");
                    Copy.ColumnMappings.Add("NTGEW", "NTGEW");
                    Copy.ColumnMappings.Add("PLIFZ", "PLIFZ");
                    Copy.ColumnMappings.Add("PRCTR", "PRCTR");
                    Copy.ColumnMappings.Add("SPART", "SPART");
                    Copy.ColumnMappings.Add("WERKS", "WERKS");
                    Copy.ColumnMappings.Add("WGBEZ", "WGBEZ");
                    Copy.ColumnMappings.Add("XCHPF", "XCHPF");
                    Copy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                    Copy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                    Copy.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                    Copy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                    Copy.WriteToServer(table);
                    if (table.Rows.Count > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    table.Dispose();
                }
            }
        }

        /// <summary>
        /// 采购订单数据同步
        /// </summary>
        /// <param name="Table">PurchaseOrderSynchronization表数据</param>
        /// <param name="table">PurchaseOrderSynchronizationItem表数据</param>
        /// <returns></returns>
        public int Purchase_OrderData_ToMES(DataTable Table, DataTable table)
        {
            SqlBulkCopy TableCopy = new SqlBulkCopy(con);
            SqlBulkCopy tableCopy = new SqlBulkCopy(con);
            try
            {
                tableCopy.DestinationTableName = "PurchaseOrderSynchronization";
                tableCopy.BatchSize = Table.Rows.Count;
                tableCopy.ColumnMappings.Add("PurchaseOrderSynchronizationId", "PurchaseOrderSynchronizationId");
                tableCopy.ColumnMappings.Add("VBELN", "VBELN");
                tableCopy.ColumnMappings.Add("BSART", "BSART");
                tableCopy.ColumnMappings.Add("BUKRS", "BUKRS");
                tableCopy.ColumnMappings.Add("WERKS", "WERKS");
                tableCopy.ColumnMappings.Add("LIFNR", "LIFNR");
                tableCopy.ColumnMappings.Add("NAME1", "NAME1");
                tableCopy.ColumnMappings.Add("ZLGORT", "ZLGORT");
                tableCopy.ColumnMappings.Add("BWART", "BWART");
                tableCopy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                tableCopy.ColumnMappings.Add("BEDAT", "BEDAT");
                tableCopy.ColumnMappings.Add("ZHSSUM", "ZHSSUM");
                tableCopy.ColumnMappings.Add("ZCHANGE_IND", "ZCHANGE_IND");
                tableCopy.ColumnMappings.Add("ZGIPLANT", "ZGIPLANT");
                tableCopy.ColumnMappings.Add("ZGRPLANT", "ZGRPLANT");
                tableCopy.ColumnMappings.Add("ZKHDZ", "ZKHDZ");
                tableCopy.ColumnMappings.Add("ZNAME", "ZNAME");
                tableCopy.ColumnMappings.Add("ZTEL", "ZTEL");
                tableCopy.ColumnMappings.Add("KHXM", "KHXM");
                tableCopy.ColumnMappings.Add("KHDZ", "KHDZ");
                tableCopy.ColumnMappings.Add("ZKHTEL", "ZKHTEL");
                tableCopy.ColumnMappings.Add("ZGRGIMARK", "ZGRGIMARK");
                tableCopy.ColumnMappings.Add("ZYJCHSJ", "ZYJCHSJ");
                tableCopy.ColumnMappings.Add("POST1", "POST1");
                tableCopy.ColumnMappings.Add("ZFSHENG", "ZFSHENG");
                tableCopy.ColumnMappings.Add("ZFSHI", "ZFSHI");
                tableCopy.ColumnMappings.Add("ZFQU", "ZFQU");
                tableCopy.ColumnMappings.Add("ZFHDZ", "ZFHDZ");
                tableCopy.ColumnMappings.Add("ZSSHENG", "ZSSHENG");
                tableCopy.ColumnMappings.Add("ZSSHI", "ZSSHI");
                tableCopy.ColumnMappings.Add("ZSQU", "ZSQU");
                tableCopy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                tableCopy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                tableCopy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                tableCopy.ColumnMappings.Add("ZYSFS", "ZYSFS");
                tableCopy.ColumnMappings.Add("IHREZ", "IHREZ");
                tableCopy.WriteToServer(Table);

                TableCopy.DestinationTableName = "PurchaseOrderSynchronizationItem";
                TableCopy.BatchSize = table.Rows.Count;
                TableCopy.ColumnMappings.Add("PurchaseOrderSynchronizationItemId", "PurchaseOrderSynchronizationItemId");
                TableCopy.ColumnMappings.Add("PurchaseOrderSynchronizationId", "PurchaseOrderSynchronizationId");
                TableCopy.ColumnMappings.Add("EBELP", "EBELP");
                TableCopy.ColumnMappings.Add("PSTYP", "PSTYP");
                TableCopy.ColumnMappings.Add("MATNR", "MATNR");
                TableCopy.ColumnMappings.Add("TXZ01", "TXZ01");
                TableCopy.ColumnMappings.Add("BANFN", "BANFN");
                TableCopy.ColumnMappings.Add("LGORT", "LGORT");
                TableCopy.ColumnMappings.Add("CHARG", "CHARG");
                TableCopy.ColumnMappings.Add("MENGE", "MENGE");
                TableCopy.ColumnMappings.Add("MEINS", "MEINS");
                TableCopy.ColumnMappings.Add("LOEKZ", "LOEKZ");
                TableCopy.ColumnMappings.Add("PSPID", "PSPID");
                TableCopy.ColumnMappings.Add("RETPO", "RETPO");
                TableCopy.ColumnMappings.Add("ZDBFS", "ZDBFS");
                TableCopy.ColumnMappings.Add("BKLAS", "BKLAS");
                TableCopy.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                TableCopy.ColumnMappings.Add("ZMEMO4", "ZMEMO4");
                TableCopy.ColumnMappings.Add("MATKL", "MATKL");
                TableCopy.ColumnMappings.Add("ZZHONGL", "ZZHONGL");
                TableCopy.ColumnMappings.Add("ZVBELN", "ZVBELN");
                TableCopy.ColumnMappings.Add("BS", "BS");
                TableCopy.ColumnMappings.Add("DATA", "DATA");
                TableCopy.ColumnMappings.Add("MBLNR", "MBLNR");
                TableCopy.ColumnMappings.Add("MJAHR", "MJAHR");
                TableCopy.ColumnMappings.Add("VBELN", "VBELN");
                TableCopy.ColumnMappings.Add("VSTEL", "VSTEL");

                TableCopy.WriteToServer(table);
                if (table.Rows.Count > 0 && Table.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                table.Dispose();
                Table.Dispose();
            }

        }
        /// <summary>
        /// 采购订单取消下发修改ZCHANGE_IND为Q（取消）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int ReturnVBELN(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update PurchaseOrderSynchronization set ZCHANGE_IND='Q' where VBELN='{0}' and ZCHANGE_IND='I'", Id);
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    if (comm.ExecuteNonQuery() > 0)
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
        /// 保存采购订单出库回传生成的SAP凭证
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Credentials"></param>
        /// <returns></returns>
        public int SaveCredentials(string Id, string Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update PurchaseOrderSynchronizationItem set [Credentials]='{0}' where PurchaseOrderSynchronizationId='{1}'", Credentials, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    conn.Close();
                    conn.Dispose();
                    return comm.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 删除下发的数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public int DelVBELN(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select PurchaseOrderSynchronizationId from PurchaseOrderSynchronization where VBELN='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    string PurchaseOrderSynchronizationId = comm.ExecuteScalar().ToString();

                    string SQL2 = string.Format("delete from PurchaseOrderSynchronization where PurchaseOrderSynchronizationId='{0}'", PurchaseOrderSynchronizationId);
                    string SQL3 = string.Format("delete from PurchaseOrderSynchronizationItem where PurchaseOrderSynchronizationId='{0}'", PurchaseOrderSynchronizationId);

                    string SQL4 = string.Format("delete from Dump_The_Order_Outbound_ReturnTable where EBELN='{0}'", Id);
                    string SQL5 = string.Format("delete from Dump_The_Order_Outbound_ReturnekpoTable where VBELN='{0}'", Id);
                    string SQL6 = string.Format("delete from Purchase_RequisitionTable where EBELN='{0}'", Id);
                    string SQL7 = string.Format("delete from Purchase_RequisitionTablekpoTable where VBELN='{0}'", Id);

                    comm.CommandText = SQL2;
                    int a = comm.ExecuteNonQuery();
                    comm.CommandText = SQL3;
                    int b = comm.ExecuteNonQuery();
                    comm.CommandText = SQL4;
                    int c = comm.ExecuteNonQuery();
                    comm.CommandText = SQL5;
                    int d = comm.ExecuteNonQuery();
                    comm.CommandText = SQL6;
                    int e = comm.ExecuteNonQuery();
                    comm.CommandText = SQL7;
                    int f = comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    if (a > 0 && b > 0 && c > 0 && d > 0 && e > 0 && f > 0)
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
        /// 其它出库下发接口
        /// </summary>
        /// <param name="table">XML转换的表</param>
        /// <returns></returns>
        public int Other_Outbound_Shipments(DataTable table, DataTable Table)
        {
            SqlBulkCopy TableCopy = new SqlBulkCopy(con);
            SqlBulkCopy TableCopy2 = new SqlBulkCopy(con);
            try
            {
                TableCopy.BatchSize = table.Rows.Count;
                TableCopy.DestinationTableName = "Other_Outbound_Shipments_Table";
                TableCopy.ColumnMappings.Add("Other_Outbound_Shipments_TableId", "Other_Outbound_Shipments_TableId");
                TableCopy.ColumnMappings.Add("VBELN", "VBELN");
                TableCopy.ColumnMappings.Add("ZBDART", "ZBDART");
                TableCopy.ColumnMappings.Add("WERKS", "WERKS");
                TableCopy.ColumnMappings.Add("BUKRS", "BUKRS");
                TableCopy.ColumnMappings.Add("BWART", "BWART");
                TableCopy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                TableCopy.ColumnMappings.Add("ZHSSUM", "ZHSSUM");
                TableCopy.ColumnMappings.Add("KUNNR", "KUNNR");
                TableCopy.ColumnMappings.Add("NAME1", "NAME1");
                TableCopy.ColumnMappings.Add("STRAS", "STRAS");
                TableCopy.ColumnMappings.Add("KHXM", "KHXM");
                TableCopy.ColumnMappings.Add("TELF1", "TELF1");
                TableCopy.ColumnMappings.Add("PSTLZ", "PSTLZ");
                TableCopy.ColumnMappings.Add("ZYSFS", "ZYSFS");
                TableCopy.ColumnMappings.Add("ZYJCHSJ", "ZYJCHSJ");
                TableCopy.ColumnMappings.Add("PSPID", "PSPID");
                TableCopy.ColumnMappings.Add("EBELN", "EBELN");
                TableCopy.ColumnMappings.Add("EBELP", "EBELP");
                TableCopy.ColumnMappings.Add("BANFN", "BANFN");
                TableCopy.ColumnMappings.Add("ZZKTEXT", "ZZKTEXT");
                TableCopy.ColumnMappings.Add("ZLWGS", "ZLWGS");
                TableCopy.ColumnMappings.Add("ZWERKS", "ZWERKS");
                TableCopy.ColumnMappings.Add("ZLGORT", "ZLGORT");
                TableCopy.ColumnMappings.Add("ZMEMO1", "ZMEMO1");
                TableCopy.ColumnMappings.Add("ZMEMO2", "ZMEMO2");
                TableCopy.ColumnMappings.Add("ZFSHENG", "ZFSHENG");
                TableCopy.ColumnMappings.Add("ZFSHI", "ZFSHI");
                TableCopy.ColumnMappings.Add("ZFQU", "ZFQU");
                TableCopy.ColumnMappings.Add("ZSSHENG", "ZSSHENG");
                TableCopy.ColumnMappings.Add("ZSSHI", "ZSSHI");
                TableCopy.ColumnMappings.Add("ZSQU", "ZSQU");
                TableCopy.ColumnMappings.Add("ZCHANGE_IND", "ZCHANGE_IND");
                TableCopy.ColumnMappings.Add("ZXTCHBS", "ZXTCHBS");
                TableCopy.ColumnMappings.Add("ZMMI001", "ZMMI001");
                TableCopy.ColumnMappings.Add("ZMMI002", "ZMMI002");
                TableCopy.ColumnMappings.Add("ZMMI003", "ZMMI003");
                TableCopy.WriteToServer(table);

                TableCopy2.BatchSize = Table.Rows.Count;
                TableCopy2.DestinationTableName = "Other_Outbound_Shipments_Item";
                TableCopy2.ColumnMappings.Add("Other_Outbound_Shipments_ItemId", "Other_Outbound_Shipments_ItemId");
                TableCopy2.ColumnMappings.Add("Other_Outbound_Shipments_TableId", "Other_Outbound_Shipments_TableId");
                TableCopy2.ColumnMappings.Add("POSNR", "POSNR");
                TableCopy2.ColumnMappings.Add("MATNR", "MATNR");
                TableCopy2.ColumnMappings.Add("MAKTX", "MAKTX");
                TableCopy2.ColumnMappings.Add("LGORT", "LGORT");
                TableCopy2.ColumnMappings.Add("CHARG", "CHARG");
                TableCopy2.ColumnMappings.Add("PSPID", "PSPID");
                TableCopy2.ColumnMappings.Add("MEINS", "MEINS");
                TableCopy2.ColumnMappings.Add("LFIMG", "LFIMG");
                TableCopy2.ColumnMappings.Add("MATKL", "MATKL");
                TableCopy2.ColumnMappings.Add("ZMEMO3", "ZMEMO3");
                TableCopy2.ColumnMappings.Add("ZMEMO4", "ZMEMO4");
                TableCopy2.WriteToServer(Table);
                if (table.Rows.Count > 0 && Table.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 其它出库回传生成351出库单
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public int Dump_The_Order_Outbound_ReturnTable(DataTable Table, DataTable table)
        {
            SqlBulkCopy TableCopy = new SqlBulkCopy(con);
            SqlBulkCopy TableCopy2 = new SqlBulkCopy(con);
            try
            {
                TableCopy.BatchSize = Table.Rows.Count;
                TableCopy.DestinationTableName = "Dump_The_Order_Outbound_ReturnTable";
                TableCopy.ColumnMappings.Add("Dump_The_Order_Outbound_ReturnTableId", "Dump_The_Order_Outbound_ReturnTableId");
                TableCopy.ColumnMappings.Add("EBELN", "EBELN");
                TableCopy.ColumnMappings.Add("BWART", "BWART");
                TableCopy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                TableCopy.ColumnMappings.Add("ZWLGRNUM", "ZWLGRNUM");
                TableCopy.ColumnMappings.Add("BUDAT", "BUDAT");
                TableCopy.ColumnMappings.Add("BLDAT", "BLDAT");
                TableCopy.WriteToServer(Table);

                TableCopy2.BatchSize = table.Rows.Count;
                TableCopy2.DestinationTableName = "Dump_The_Order_Outbound_ReturnekpoTable";
                TableCopy2.ColumnMappings.Add("Dump_The_Order_Outbound_ReturnekpoTableId", "Dump_The_Order_Outbound_ReturnekpoTableId");
                TableCopy2.ColumnMappings.Add("Dump_The_Order_Outbound_ReturnTableId", "Dump_The_Order_Outbound_ReturnTableId");
                TableCopy2.ColumnMappings.Add("EBELP", "EBELP");
                TableCopy2.ColumnMappings.Add("MATNR", "MATNR");
                TableCopy2.ColumnMappings.Add("CHARG", "CHARG");
                TableCopy2.ColumnMappings.Add("LGORT", "LGORT");
                TableCopy2.ColumnMappings.Add("MENGE", "MENGE");
                TableCopy2.ColumnMappings.Add("ZMENGE", "ZMENGE");
                TableCopy2.ColumnMappings.Add("WERKS", "WERKS");
                TableCopy2.ColumnMappings.Add("MEINS", "MEINS");
                TableCopy2.ColumnMappings.Add("VBELN", "VBELN");
                TableCopy2.WriteToServer(table);

                if (Table.Rows.Count > 0 && table.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 生成101采购入库单
        /// </summary>
        /// <param name="Table">采购入库单主表（EKKO--->Purchase_RequisitionTable）</param>
        /// <param name="table">采购入库单子表（EKPO--->Purchase_RequisitionTablekpoTable）</param>
        /// <returns></returns>
        public int Purchase_RequisitionTable(DataTable Table, DataTable table)
        {
            SqlBulkCopy TableCopy = new SqlBulkCopy(con);
            SqlBulkCopy TableCopy2 = new SqlBulkCopy(con);
            try
            {
                TableCopy.BatchSize = Table.Rows.Count;
                TableCopy.DestinationTableName = "Purchase_RequisitionTable";
                TableCopy.ColumnMappings.Add("Purchase_RequisitionTableId", "Purchase_RequisitionTableId");
                TableCopy.ColumnMappings.Add("EBELN", "EBELN");
                TableCopy.ColumnMappings.Add("BWART", "BWART");
                TableCopy.ColumnMappings.Add("SOBKZ", "SOBKZ");
                TableCopy.ColumnMappings.Add("ZWLGRNUM", "ZWLGRNUM");
                TableCopy.ColumnMappings.Add("BUDAT", "BUDAT");
                TableCopy.ColumnMappings.Add("BLDAT", "BLDAT");
                TableCopy.WriteToServer(Table);

                TableCopy2.BatchSize = table.Rows.Count;
                TableCopy2.DestinationTableName = "Purchase_RequisitionTablekpoTable";
                TableCopy2.ColumnMappings.Add("Purchase_RequisitionTablekpoTableId", "Purchase_RequisitionTablekpoTableId");
                TableCopy2.ColumnMappings.Add("Purchase_RequisitionTableId", "Purchase_RequisitionTableId");
                TableCopy2.ColumnMappings.Add("EBELP", "EBELP");
                TableCopy2.ColumnMappings.Add("MATNR", "MATNR");
                TableCopy2.ColumnMappings.Add("CHARG", "CHARG");
                TableCopy2.ColumnMappings.Add("LGORT", "LGORT");
                TableCopy2.ColumnMappings.Add("MENGE", "MENGE");
                TableCopy2.ColumnMappings.Add("ZMENGE", "ZMENGE");
                TableCopy2.ColumnMappings.Add("WERKS", "WERKS");
                TableCopy2.ColumnMappings.Add("MEINS", "MEINS");
                TableCopy2.ColumnMappings.Add("VBELN", "VBELN");
                TableCopy2.WriteToServer(table);

                if (Table.Rows.Count > 0 && table.Rows.Count > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 判断下发数据是否存在防止重复下发
        /// </summary>
        /// <param name="VBELN">下发单号</param>
        /// <returns></returns>
        public Object VbelnExists(string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("select count(*) from SellOutOfTheWarehouseTable where VBELN='{0}'", VBELN);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    Object A = comm.ExecuteScalar();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return A;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断采购订单下发数据是否存在防止重复下发
        /// </summary>
        /// <param name="VBELN">下发单号</param>
        /// <returns></returns>
        public Object VbelnExistsThe_Purchase_Order(string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("select count(*) from PurchaseOrderSynchronization where VBELN='{0}'", VBELN);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    Object A = comm.ExecuteScalar();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return A;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 修改下发订单的变更属性
        /// </summary>
        /// <param name="ZCHANGE_IND">修改之前的变更属性</param>
        /// <param name="VBELN">单号</param>
        /// <returns></returns>
        public int UpdZCHANGE_IND(string ZCHANGE_IND, string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("update SellOutOfTheWarehouseTable set ZCHANGE_IND='{0}' where VBELN='{1}'", ZCHANGE_IND, VBELN);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int A = comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return A;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 修改采购订单下发的变更属性
        /// </summary>
        /// <param name="ZCHANGE_IND">修改之前的变更属性</param>
        /// <param name="VBELN">单号</param>
        /// <returns></returns>
        public int UpdThe_Purchase_Order(string ZCHANGE_IND, string VBELN)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("update PurchaseOrderSynchronization set ZCHANGE_IND='{0}' where VBELN='{1}' and ZCHANGE_IND='I'", ZCHANGE_IND, VBELN);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int A = comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return A;
                }
            }
            catch (Exception ex)
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
                    string SQL1 = string.Format("select ProductOrderNO as AUFNR,101 as BWART,'' as SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end as BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end as BLDAT  from productorder where productorderid='{0}'", Id);
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
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm1.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Inbound_Credentials
        /// <summary>
        /// 保存生产订单收获生成的凭证
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Inbound_Credentials"></param>
        /// <returns></returns>
        public int Save_Production_Order_Receiving_Inbound_Credentials(string Id, string Inbound_Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("select Inbound_Credentials from ProductOrder where ProductOrderId='{0}'", Id);
                    SqlCommand comm1 = new SqlCommand();
                    comm1.Connection = conn;
                    comm1.CommandText = SQL;
                    object Inbound_Credentials1 = comm1.ExecuteScalar();
                    SqlCommand comm = new SqlCommand();
                    if (Inbound_Credentials1.ToString().Length > 0)
                    {
                        string sql = string.Format("Update ProductOrder set Inbound_Credentials='{0}' where ProductOrderId='{1}'", Inbound_Credentials1.ToString()+","+Inbound_Credentials, Id);
                        comm.Connection = conn;
                        comm.CommandText = sql;
                    }
                    else
                    {
                        string sql = string.Format("Update ProductOrder set Inbound_Credentials='{0}' where ProductOrderId='{1}'", Inbound_Credentials, Id);
                        comm.Connection = conn;
                        comm.CommandText = sql;
                    }
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();

                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //调用SAP接口

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
                    conn.Open();
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("SELECT PurchaseOrderSynchronization.VBELN AS EBELN,BWART,SOBKZ,PurchaseOrderSynchronizationId AS ZWLGRNUM,BEDAT AS BUDAT,BEDAT AS BLDAT FROM PurchaseOrderSynchronization where PurchaseOrderSynchronizationId='{0}'", Id);
                    SqlCommand com = new SqlCommand();
                    com.CommandText = SQL1;
                    com.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com;
                    adapter.Fill(DS, "PurchaseOrderSynchronization");

                    string SQL2 = string.Format("SELECT EBELP,MATNR,CHARG,LGORT,Qty as MENGE,'' as ZMENGE,WERKS,MEINS FROM PurchaseOrderSynchronizationItem JOIN PurchaseOrderSynchronization ON PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId=PurchaseOrderSynchronization.PurchaseOrderSynchronizationId WHERE PurchaseOrderSynchronization.PurchaseOrderSynchronizationId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand();
                    com2.Connection = conn;
                    com2.CommandText = SQL2;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = com2;
                    adapter2.Fill(DS, "PurchaseOrderSynchronizationItem");
                    conn.Close();
                    conn.Dispose();
                    com.Dispose();
                    com2.Dispose();
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
                    conn.Open();
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
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    com.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();
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
                    conn.Open();
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
                    conn.Close();
                    conn.Dispose();
                    com1.Dispose();
                    com2.Dispose();
                    adapter.Dispose();
                    adapter2.Dispose();
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
                    conn.Open();
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
                    conn.Close();
                    conn.Dispose();
                    com.Dispose();
                    com2.Dispose();
                    adapter.Dispose();
                    adapter2.Dispose();
                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 保存销售出库回传SAP下发的凭证号
        /// </summary>
        /// <param name="Id">销售出库下发MES单号</param>
        /// <param name="Credentials">SAP生成的凭证号</param>
        /// <returns></returns>
        public int SaveSell_The_Outbound_Return_Credentials(string Id, string Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    string SQL = string.Format("update Items set [Credentials]='{0}' where SellOutOfTheWarehouseId='{1}'", Credentials, Id);
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取销售订单冲销数据根据订单号
        /// </summary>
        /// <param name="Id">销售订单MES单号</param>
        /// <returns></returns>
        public DataSet GetSell_The_Outbound_Return_Credentials(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select '1' as BS,DATA,Items.[Credentials] as MBLNR,MJAHR,SellOutOfTheWarehouseTable.VBELN,'6006' as VSTEL FROM Items join SellOutOfTheWarehouseTable on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId where Items.SellOutOfTheWarehouseId='{0}'", Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(set);
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    adapter.Dispose();
                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取销售订单冲销数据根据SAP凭证号
        /// </summary>
        /// <param name="Id">销售订单MES单号</param>
        /// <returns></returns>
        public DataSet GetSell_The_Outbound_Return_Credentials1(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select '1' as BS,DATA,Items.[Credentials] as MBLNR,MJAHR,SellOutOfTheWarehouseTable.VBELN,'6006' as VSTEL FROM Items join SellOutOfTheWarehouseTable on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId where Items.[Credentials]='{0}'", Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(set);
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    adapter.Dispose();
               
                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 销售订单冲销接口
        /// </summary>
        /// <param name="Id">销售订单MES单号</param>
        /// <param name="Credentials">冲销后的SAP凭证号</param>
        /// <returns></returns>
        public int UpdSell_The_Outbound_Return_Credentials(string Id, string Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update Items set [Credentials]='{0}' where SellOutOfTheWarehouseId='{1}'", Credentials, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();

                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 采购订单冲销接口
        /// </summary>
        /// <param name="Id">采购订单冲销Id</param>
        /// <returns></returns>
        public DataSet Orders_For_Sterilisation(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL1 = string.Format("select distinct BS,DATA,PurchaseOrderSynchronizationItem.[Credentials] as MBLNR,MJAHR,PurchaseOrderSynchronizationItem.VBELN,VSTEL FROM PurchaseOrderSynchronizationItem join PurchaseOrderSynchronization on PurchaseOrderSynchronizationItem.PurchaseOrderSynchronizationId=PurchaseOrderSynchronization.PurchaseOrderSynchronizationId where PurchaseOrderSynchronization.PurchaseOrderSynchronizationId='{0}'", Id);
                    DataSet SET = new DataSet();
                    SqlCommand COM = new SqlCommand(SQL1, conn);
                    SqlDataAdapter ADAPTER = new SqlDataAdapter();
                    ADAPTER.SelectCommand = COM;
                    ADAPTER.Fill(SET);
                    conn.Close();
                    conn.Dispose();
                    COM.Dispose();
                    ADAPTER.Dispose();
                    return SET;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 采购订单冲销更新SAP凭证
        /// </summary>
        /// <param name="Id">采购订单MES单号</param>
        /// <param name="Credentials">更新后的凭证号</param>
        /// <returns></returns>
        public int UpdCredentials(string Id, string Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update PurchaseOrderSynchronizationItem set [Credentials]='{0}' where PurchaseOrderSynchronizationId='{1}'", Credentials, Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    conn.Open();
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
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
                    string SQl1 = string.Format(" select  ProjectOutPutNO as VBELN , CompanyProof as ZWLGINUM , Factory as WERKS, MoveType as BWART, SpecificalStore as SOBKZ , CostCenter as KOSTL, PostDate as BLDAT, ProofDate  as BUDAT from OrderOutPutRecords	where OrderOutPutRecordsId='{0}'", Id);
                    DataSet DS = new DataSet();
                    conn.Open();
                    SqlCommand com1 = new SqlCommand();
                    com1.CommandText = SQl1;
                    com1.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = com1;
                    adapter.Fill(DS, "Other_Outbound_Shipments_Table");


                    //, OrderProjectNO as KDPOS
                    string SQL2 = string.Format("select   LineProjectNO as POSNR,MatetialNO as MATNR, MaterialDescription as MAKTX, StoragePlace as LGORT, SNNO as CHARG, OutStorageQty as LFIMG, Unit as MEINS, SaleOrderNO as KDAUF  from OrderOutPutRecords	where OrderOutPutRecordsId='{0}'", Id);
                    SqlCommand com2 = new SqlCommand();
                    com2.CommandText = SQL2;
                    com2.Connection = conn;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter.SelectCommand = com2;
                    adapter.Fill(DS, "Other_Outbound_Shipments_Item");

                    string SQL3 = string.Format("select * from The_Movement_Type");
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL3;
                    SqlDataAdapter adapter3 = new SqlDataAdapter();
                    adapter3.SelectCommand = comm;
                    adapter3.Fill(DS, "The_Movement_Type");

                    conn.Close();
                    conn.Dispose();
                    com1.Dispose();
                    com2.Dispose();
                    comm.Dispose();
                    adapter.Dispose();
                    adapter2.Dispose();
                    adapter3.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存其它出库回传生成的凭证根据订单Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CompanyProof1"></param>
        /// <returns></returns>
        public int Save_Other_Outbound_Returns_CompanyProofOrderOutPutRecordsId(string Id, string OrderOutPutRecordsId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string Sql = string.Format("Update OrderOutPutRecords set SAPOrder='{0}' where OrderOutPutRecordsId='{1}'", OrderOutPutRecordsId, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = Sql;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 保存其它出库回传生成的凭证根据SAP凭证号
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CompanyProof1"></param>
        /// <returns></returns>
        public int Save_Other_Outbound_Returns_CompanyProofSAPOrder(string Id, string SAPOrder)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string Sql = string.Format("Update OrderOutPutRecords set SAPOrder='{0}' where SAPOrder='{1}'", SAPOrder, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = Sql;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 获取其它出库回传冲销数据根据订单号
        /// </summary>
        /// <param name="Id">销售订单MES单号</param>
        /// <returns></returns>
        public DataSet GetOrderOutPutRecords_by_OrderOutPutRecordsId(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select '0' as BS,PostDate as [DATA],SAPOrder as MBLNR,SAPData as MJAHR,ProjectOutPutNO as VBELN,'6006' as VSTEL from OrderOutPutRecords where OrderOutPutRecordsId='{0}'", Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(set);
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    adapter.Dispose();
                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取其它出库回传冲销数据根据SAP凭证
        /// </summary>
        /// <param name="Id">其它出库SAP凭证</param>
        /// <returns></returns>
        public DataSet GetOrderOutPutRecords_by_SAPOrder(string Id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("select '0' as BS,PostDate as [DATA],SAPOrder as MBLNR,SAPData as MJAHR,ProjectOutPutNO as VBELN,'6006' as VSTEL from OrderOutPutRecords where SAPOrder='{0}'", Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    DataSet set = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(set);
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    adapter.Dispose();
                    return set;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 其它出库回传冲销接口根据订单号保存SAP生成的凭证
        /// </summary>
        /// <param name="Id">其它出库回传MES单号</param>
        /// <param name="OrderOutPutRecordsId">冲销后的SAP凭证号</param>
        /// <returns></returns>
        public int UpdSAPOrder_by_OrderOutPutRecordsId(string Id, string OrderOutPutRecordsId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update OrderOutPutRecords set SAPOrder='{0}' where OrderOutPutRecordsId='{1}'", OrderOutPutRecordsId, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 其它出库回传冲销接口根据SAP凭证保存SAP生成的凭证
        /// </summary>
        /// <param name="Id">其它出库回传MES单号</param>
        /// <param name="SAPOrder">冲销后的SAP凭证号</param>
        /// <returns></returns>
        public int UpdSAPOrder_by_SAPOrder(string Id, string SAPOrder)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update OrderOutPutRecords set SAPOrder='{0}' where SAPOrder='{1}'", SAPOrder, Id);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
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
                    string SQL1 = string.Format(" select 'NB' as BSART,'' as ITEXT1,10 as BNFPO,ProductName as MATNR,PurchaseQty as MENGE,PurchaseUnit as MEINS,   Factory as WERKS,StoreLocation as LGORT,'' as FIFNR,FixedVendor as FLTEF,PlanDate as LFDAT,PurchaseTissue as  EKORG    from 	PurchaseDetails	where PurchaseDetailsId='{0}'", Id);
                    conn.Open();
                    SqlCommand com1 = new SqlCommand();
                    com1.CommandText = SQL1;
                    com1.Connection = conn;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = com1;
                    adapter1.Fill(DS, "PurchaseDetails");

                    //string SQL2 = string.Format("SELECT '' AS BNFPO,PRODUCTNAME AS MATNR,PURCHASEQTY AS MENGE,PURCHASEUNIT AS MEINS,FACTORY AS WERKS,STORELOCATION AS LGORT,VENDORNO AS LIFNR,FIXEDVENDOR AS FLIEF,REQUESTARRIVEDATE AS LFDAT,PURCHASETISSUE AS EKORG FROM purchasedetails WHERE PurchaseApplyId='{0}'", Id);
                    //SqlCommand com2 = new SqlCommand();
                    //com2.CommandText = SQL2;
                    //com2.Connection = conn;
                    //SqlDataAdapter adapter2 = new SqlDataAdapter();
                    //adapter2.SelectCommand = com2;
                    //adapter2.Fill(DS, "purchasedetails");

                    conn.Close();
                    conn.Dispose();
                    com1.Dispose();
                    adapter1.Dispose();

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
        public int SaveThe_Purchasing_Requisition(string PurchaseDetailsId, string DATA)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("update PurchaseDetails set DATA='{0}' where PurchaseDetailsId='{1}'", DATA, PurchaseDetailsId);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// <summary>
        /// 生产订单发货
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Order_Delivery(string Id, string ResourceId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BLDAT,case when ProductType in(select UserCodeName from OrBitXI.dbo.UserCode where ParentUserCodeId='URC1000001IX') then 'CC60062004' else 'CC60062001' end KOSTL from ProductOrder where ProductOrderNo='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select distinct '' as EBELP, GetMaterialbills.ProductName as MATNR,'' AS CHARG,SendMaterialRepertory.WareHouseCode as LGORT, SendMaterialRepertory.QTY AS MENGE,'6006' AS WERKS,'' AS MEINS FROM GetMaterialbills   join ProductOrder on GetMaterialbills.ProductOrderId=ProductOrder.ProductOrderId   left join MaterialTypeName on GetMaterialbills.ProductName=MaterialTypeName.ProductName   inner join SendMaterialRepertory on GetMaterialBills.ProductOrderId=SendMaterialRepertory.ProductOrderId and GetMaterialBills.ProductName=SendMaterialRepertory.ProductName  where ProductOrder.ProductOrderNO='{0}'  and  SendMaterialRepertory.ResourceId='{1}' and   SendMaterialRepertory.State=0    group by GetMaterialbills.ProductName,GetMaterialbills.Unit,GetMaterialbills.WareHouseCode,SendMaterialRepertory.WareHouseCode,SendMaterialRepertory.QTY", Id, ResourceId);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();


                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 生产订单发货2
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Order_Delivery2(string Id, string ResourceId, string WorkcenterId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BLDAT,case when ProductType in(select UserCodeName from OrBitXI.dbo.UserCode where ParentUserCodeId='URC1000001IX') then 'CC60062004' else 'CC60062001' end KOSTL from ProductOrder where ProductOrderNo='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select '' as EBELP, s.ProductName as MATNR,'' AS CHARG,WareHouseCode as LGORT, QTY AS MENGE,'6006' AS WERKS,'' AS MEINS from SendMaterialAPSIIRepertory s inner join ProductOrder p on s.ProductOrderId=p.ProductOrderId where State=0 and ProductOrderNO='{0}' and ResourceId='{1}' and WorkcenterId='{2}'", Id, ResourceId, WorkcenterId);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// <summary>
        /// 生产订单发货3
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Order_Delivery3(string Id, string ResourceId, string WorkcenterId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BLDAT,case when ProductType in(select UserCodeName from OrBitXI.dbo.UserCode where ParentUserCodeId='URC1000001IX') then 'CC60062004' else 'CC60062001' end KOSTL from ProductOrder where ProductOrderNo='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select '' as EBELP, s.ProductName as MATNR,'' AS CHARG,WareHouseCode as LGORT, QTY AS MENGE,'6006' AS WERKS,'' AS MEINS from InDoorSendMaterialRepertory s inner join ProductOrder p on s.ProductOrderId=p.ProductOrderId where State=0 and ProductOrderNO='{0}' and ResourceId='{1}' and WorkcenterId='{2}'", Id, ResourceId, WorkcenterId);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 生产订单发货4
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Order_Delivery4(string Id, string ResourceId, string WorkcenterId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BLDAT,case when ProductType in(select UserCodeName from OrBitXI.dbo.UserCode where ParentUserCodeId='URC1000001IX') then 'CC60062004' else 'CC60062001' end KOSTL from ProductOrder where ProductOrderNo='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select '' as EBELP, s.ProductName as MATNR,'' AS CHARG,WareHouseCode as LGORT, QTY AS MENGE,'6006' AS WERKS,'' AS MEINS from SendMaterialAPSIIRepertory s inner join ProductOrder p on s.ProductOrderId=p.ProductOrderId where State=0 and ProductOrderNO='{0}' and ResourceId='{1}' and WorkcenterId='{2}'", Id, ResourceId, WorkcenterId);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 获取价格（根据订单号获取明细）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet Get_PriceByProductOrderNo(string ProductOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();

                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select ProductOrderNo as AUFNR,'261' AS BWART,'' AS SOBKZ,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BUDAT,case when  DatePart (DD,getdate())<27 then getdate() else replace(CONVERT(varchar(100), dateadd(month,1,getdate()), 21),SUBSTRING (  CONVERT(varchar(100), dateadd(month,1,getdate()), 21) , 9 , 2 ),'01')  end AS BLDAT,case when ProductType in(select UserCodeName from OrBitXI.dbo.UserCode where ParentUserCodeId='URC1000001IX') then 'CC60062004' else 'CC60062001' end KOSTL from ProductOrder where ProductOrderNo='{0}'", ProductOrderNo);
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL1;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "ProductOrder");

                    string SQL2 = string.Format("select '' as EBELP, s.ProductName as MATNR,'' AS CHARG,WareHouseCode as LGORT, QTY AS MENGE,'6006' AS WERKS,'' AS MEINS from SendMaterialAPSIIRepertory s inner join ProductOrder p on s.ProductOrderId=p.ProductOrderId where State=0 and ProductOrderNO='{0}' and ResourceId='{1}' and WorkcenterId='{2}'", ProductOrderNo);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter1 = new SqlDataAdapter();
                    adapter1.SelectCommand = comm2;
                    adapter1.Fill(DS, "GetMaterialBills");

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter1.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 讲SAP下发的价格信息保存到数据库
        /// </summary>
        /// <param name="DS">SAP下发的价格信息转换成的表</param>
        /// <returns>0，失败 1，成功</returns>
        public int SAP_MES_GetPriceToSQL(DataTable DT)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    //conn.ConnectionString = "Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;Password=admin@2017";
                    conn.Open();
                    int Count = 0;
                    SqlCommand comm2 = new SqlCommand();
                    for (int i = 0; i < DT.Rows.Count; i++)
			        {

                        string SQL = string.Format("update MarketOrderTable set SettlementAmount='{0}',BY1='{1}',BY2='{2}' where Saleno='{3}'", DT.Rows[i]["SettlementAmount"], DT.Rows[i]["BY1"], DT.Rows[i]["BY2"], DT.Rows[i]["ProductOrderNo"]);
                        comm2.Connection = conn;
                        comm2.CommandText = SQL;
                        Count =Count+ comm2.ExecuteNonQuery();
			        }
                    conn.Close();
                    conn.Dispose();
                    if (Count == DT.Rows.Count)
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
        /// OMS传入订单号获取数据
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public DataSet OMSGet_DateByProductOrderNo(string ProductOrderNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    DataSet DS = new DataSet();
                    SqlConnection sqlconn = new SqlConnection();
                    SqlCommand cmd = new SqlCommand("OMSGetDateByProductOrderNo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProductOrderNo", SqlDbType.NVarChar));
                    //如cmd.Parameters.Add(new SqlParameter("@riqi", SqlDbType.DateTime, 8));
                    //把具体的值传给输入参数
                    cmd.Parameters["@ProductOrderNo"].Value = ProductOrderNo;
                    
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    adapter.Fill(DS, "ProductOrder");

                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                    adapter.Dispose();
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
        public int ReturnNum(string Id, string Outbound_Credentials)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL1 = string.Format("select Outbound_Credentials from ProductOrder where ProductOrderNO='{0}'", Id);
                    SqlCommand comm1 = new SqlCommand();
                    conn.Close();
                    conn.Open();
                    comm1.Connection = conn;
                    comm1.CommandText = SQL1;
                    object Outbound_Credentials1 = comm1.ExecuteScalar();
                    SqlCommand comm = new SqlCommand();
                    if(Outbound_Credentials1.ToString().Length>0)
                    {
                        conn.Close();
                        string SQL = string.Format("Update ProductOrder set Outbound_Credentials='{0}' where ProductOrderNO='{1}'",Outbound_Credentials1.ToString()+","+Outbound_Credentials, Id);
                        conn.Open();
                        comm.Connection = conn;
                        comm.CommandText = SQL;
                    }
                    else
                    {
                        conn.Close();
                        string SQL = string.Format("Update ProductOrder set Outbound_Credentials='{0}' where ProductOrderNO='{1}'", Outbound_Credentials, Id);
                        conn.Open();
                        comm.Connection = conn;
                        comm.CommandText = SQL;
                    }
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm1.Dispose();
                    return a;
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
                    conn.Open();

                    DataSet DS = new DataSet();
                    string SQL1 = string.Format("select  CompanyProof as ZWLTRNUM,MoveTypeName as BWART,  PostDate as BUDAT, Factory as WERKS from StorageInMove where StorageInMoveId='{0}'", Id);
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = SQL1;
                    comm.Connection = conn;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = comm;
                    adapter.Fill(DS, "Transfer_The_Rolls");

                    string SQL2 = string.Format("select  '' as POSNR, MaterialNO as MATNR,ChangeMaterialNO as UMMAT, Vendor as LIFNR, SendStoragePlace as LGORT, SpecificalStore as FCSOBKZ, SendProjectNO as FCPSPID, ReceiveStoragePlace as UMLGO, ReceiveSpecificalStore as JSSOBKZ, ReceivePorjectNO as JSPSPID, BeforeChanceSN as CHARG, BehindChanceSN as UMCHA, Qty as MENGE, SendSalesOrderNO as FCVBELN, ReceiveSalesOrderNO as JSVBELN, BeforeChanceStoreStatus as INSMK, BehindChanceStoreStatus as BESTQ, MoveReason  as ZGRUND from StorageInMove where StorageInMoveId='{0}'", Id);
                    SqlCommand comm2 = new SqlCommand();
                    comm2.Connection = conn;
                    comm2.CommandText = SQL2;
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    adapter2.SelectCommand = comm2;
                    adapter2.Fill(DS);

                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    comm2.Dispose();
                    adapter.Dispose();
                    adapter2.Dispose();

                    return DS;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存库内转移生成的凭证
        /// </summary>
        /// <returns></returns>
        public int Save_Transfer_The_Rolls_CompanyProof1(string Id, string CompanyProof1)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    string SQL = string.Format("Update OrderOutPutRecords set CompanyProof1='{0}' where OrderOutPutRecordsId='{1}'", CompanyProof1, Id);
                    SqlCommand comm = new SqlCommand();
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    conn.Close();
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
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
        public int Cancellation_Issued(string Str)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    string SQL = string.Format("select SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId from Items join  SellOutOfTheWarehouseTable on Items.SellOutOfTheWarehouseId=SellOutOfTheWarehouseTable.SellOutOfTheWarehouseId where SellOutOfTheWarehouseTable.VBELN='{0}'", Str);
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
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();

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
                    //string sql3 = string.Format("delete from Dump_The_Order_Outbound_ReturnTable where EBELN='{0}'", VBELN);
                    //string sql4 = string.Format("delete from Dump_The_Order_Outbound_ReturnekpoTable where VBELN='{0}'", VBELN);
                    //string sql5 = string.Format("delete from Purchase_RequisitionTable where EBELN='{0}'", VBELN);
                    //string sql6 = string.Format("delete from Purchase_RequisitionTablekpoTable where VBELN='{0}'", VBELN);
                    List<String> list = new List<string>();
                    list.Add(SQL);
                    list.Add(sql2);
                    //list.Add(sql3);
                    //list.Add(sql4);
                    //list.Add(sql5);
                    //list.Add(sql6);

                    comm.Connection = conn;
                    int a = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        comm.CommandText = list[i].ToString();
                        if (comm.ExecuteNonQuery() > 0)
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
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    
                    if (a == 2)
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
        /// 工厂库存地信息修改
        /// </summary>
        /// <returns></returns>
        public int UpdatePlant_Stock_Table(string ZLGORT)
        {
            try
            {
                using(SqlConnection conn=new SqlConnection(con))
                {
                    string SQL = string.Format("delete from Plant_Stock_Table where ZLGORT={0}",ZLGORT);
                    conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = SQL;
                    int a=comm.ExecuteNonQuery();
                    conn.Close();
                    conn.Dispose();
                    comm.Dispose();
                    return a;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 从OMS获取2020销售金额
        /// </summary>
        /// <param name="XMLString"></param>
        /// <returns></returns>
        public int Get_OMS2020_Price(string XMLString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(con))
                {
                    conn.Open();
                    SqlConnection sqlconn = new SqlConnection();
                    SqlCommand cmd = new SqlCommand("Get_OMS2020_PriceByProductOrderNo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StringXml", SqlDbType.NVarChar));
                    //如cmd.Parameters.Add(new SqlParameter("@riqi", SqlDbType.DateTime, 8));
                    SqlParameter parOutput = cmd.Parameters.Add("@IsSuccess", SqlDbType.Int);　　//定义输出参数
                    parOutput.Direction = ParameterDirection.Output;　　//参数类型为Output
                    //把具体的值传给输入参数
                    cmd.Parameters["@StringXml"].Value = XMLString;

                    cmd.ExecuteNonQuery();
                    string returnStr = parOutput.Value.ToString();

                    conn.Close();
                    conn.Dispose();
                    cmd.Dispose();
                    return 1;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
