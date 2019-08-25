using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace IntransitImport
{
    public class DissaccemblyDataTable
    {
        /// <summary>
        /// 柜体导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitDataTableToDB(DataTable orderDaetail, DataTable IssueMaterialBills, DataTable GetMaterialBillsDataTable, DataTable PackingListTable, DataTable GlassIssueMaterialBills1Table, DataTable GlassIssueMaterialBills2Table, DataTable Packing_ListTable, DataTable Packing_ListA6Table, DataTable A6NoPaintSheetTable, DataTable MeiWenqiBlankingSheetTable, DataTable MeiWenqiRequisitionSheetTable, DataTable A6WenqiBlankingSheetTable, DataTable SuctionMoldingDoorSheetMaterialListTable, DataTable FeedSingleBlisterDoorPlankSheetTable)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_OrderDetail", SqlDbType.Structured)
                {
                    Value = orderDaetail
                },
                new SqlParameter("@Pro_IssueMaterialBills", SqlDbType.Structured)
                {
                    Value = IssueMaterialBills
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBillsDataTable
                },
                new SqlParameter("@Pro_PackingList", SqlDbType.Structured)
                {
                    Value = PackingListTable
                },
                new SqlParameter("@Pro_GlassIssueMaterialBills1", SqlDbType.Structured)
                {
                    Value = GlassIssueMaterialBills1Table
                },
                new SqlParameter("@Pro_GlassIssueMaterialBills2", SqlDbType.Structured)
                {
                    Value = GlassIssueMaterialBills2Table
                },
                new SqlParameter("@Pro_Packaging", SqlDbType.Structured)
                {
                    Value = Packing_ListTable
                },
                new SqlParameter("@Pro_PackagingA6", SqlDbType.Structured)
                {
                    Value = Packing_ListA6Table
                },
                new SqlParameter("@A6NoPaintSheetTable", SqlDbType.Structured)
                {
                    Value = A6NoPaintSheetTable
                },
                new SqlParameter("@MeiWenqiBlankingSheetTable", SqlDbType.Structured)
                {
                    Value = MeiWenqiBlankingSheetTable
                },
                new SqlParameter("@MeiWenqiRequisitionSheetTable", SqlDbType.Structured)
                {
                    Value = MeiWenqiRequisitionSheetTable
                },
                new SqlParameter("@A6WenqiBlankingSheetTable", SqlDbType.Structured)
                {
                    Value = A6WenqiBlankingSheetTable
                },
                new SqlParameter("@Pro_SuctionMoldingDoorSheetMaterialListTable", SqlDbType.Structured)
                {
                    Value = SuctionMoldingDoorSheetMaterialListTable
                },
                new SqlParameter("@Pro_FeedSingleBlisterDoorPlankSheetTable", SqlDbType.Structured)
                {
                    Value = FeedSingleBlisterDoorPlankSheetTable
                }
            };

            DBUtility.ExecStoredProcedure("[InsertCabinetMaterialListToSQL]", param);
        }


        /// <summary>
        /// 柜体导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void ShengCanJuTableToDB(DataTable CabinetTable, DataTable IssueMaterialBillsTable, DataTable AluminumGlassLeftTable, DataTable AluminumGlassRightTable, DataTable MaterialRequisitionTopTable, DataTable MaterialRequisitionUnderTable, DataTable A6PackagingSheetTable, DataTable SpeedDfBeautyTable, DataTable PlateDoorSheetWorkSheetTable, DataTable PlateDoorHandleSheetTable, DataTable ClassicDoorSheetTable, DataTable ClassicDoorHandleSheetTable, DataTable SidibJobListSheetTable, DataTable SiddibRequisitionFormSheetTable, DataTable TurandotWorkSheetTable, DataTable MaterialRequisitionSheetTable, DataTable TurandotEbonyWorkSheetTable, DataTable TurandotBlackSandalwoodMaterialListTable)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_CabinetTableTable", SqlDbType.Structured)
                {
                    Value = CabinetTable
                },
                new SqlParameter("@Pro_IssueMaterialBillsTable", SqlDbType.Structured)
                {
                    Value = IssueMaterialBillsTable
                },
                new SqlParameter("@Pro_AluminumGlassLeftTable", SqlDbType.Structured)
                {
                    Value = AluminumGlassLeftTable
                },
                new SqlParameter("@Pro_AluminumGlassRightTable", SqlDbType.Structured)
                {
                    Value = AluminumGlassRightTable
                },
                new SqlParameter("@Pro_MaterialRequisitionTopTable", SqlDbType.Structured)
                {
                    Value = MaterialRequisitionTopTable
                },
                new SqlParameter("@Pro_MaterialRequisitionUnderTable", SqlDbType.Structured)
                {
                    Value = MaterialRequisitionUnderTable
                },
                new SqlParameter("@Pro_A6PackagingSheetTable", SqlDbType.Structured)
                {
                    Value = A6PackagingSheetTable
                },
                new SqlParameter("@Pro_SpeedDfBeautyTable", SqlDbType.Structured)
                {
                    Value = SpeedDfBeautyTable
                },
                new SqlParameter("@Pro_PlateDoorSheetWorkSheetTable", SqlDbType.Structured)
                {
                    Value = PlateDoorSheetWorkSheetTable
                },
                new SqlParameter("@Pro_PlateDoorHandleSheetTable", SqlDbType.Structured)
                {
                    Value = PlateDoorHandleSheetTable
                },
                new SqlParameter("@Pro_ClassicDoorSheetTable", SqlDbType.Structured)
                {
                    Value = ClassicDoorSheetTable
                },
                new SqlParameter("@Pro_ClassicDoorHandleSheetTable", SqlDbType.Structured)
                {
                    Value = ClassicDoorHandleSheetTable
                },
                new SqlParameter("@Pro_SidibJobListSheetTable", SqlDbType.Structured)
                {
                    Value = SidibJobListSheetTable
                },
                new SqlParameter("@Pro_SiddibRequisitionFormSheetTable", SqlDbType.Structured)
                {
                    Value = SiddibRequisitionFormSheetTable
                },
                new SqlParameter("@Pro_TurandotWorkSheetTable", SqlDbType.Structured)
                {
                    Value = TurandotWorkSheetTable
                },
                new SqlParameter("@Pro_MaterialRequisitionSheetTable", SqlDbType.Structured)
                {
                    Value = MaterialRequisitionSheetTable
                },
                new SqlParameter("@Pro_TurandotEbonyWorkSheetTable", SqlDbType.Structured)
                {
                    Value = TurandotEbonyWorkSheetTable
                },
                new SqlParameter("@Pro_TurandotBlackSandalwoodMaterialListTable", SqlDbType.Structured)
                {
                    Value = TurandotBlackSandalwoodMaterialListTable
                }
            };

            DBUtility.ExecStoredProcedure("[InsertShengCanJuTableToSQL]", param);
        }
        
        //CommitGetMaterialBillsTableToDB
        /// <summary>
        /// 领料单导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitGetMaterialBillsTableToDB(DataTable GetMaterialBillsTable, DataTable PackingListTable)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBillsTable
                },
                new SqlParameter("@Pro_PackingList", SqlDbType.Structured)
                {
                    Value = PackingListTable
                }
            };

            DBUtility.ExecStoredProcedure("[InsertGetMaterialBillsAndPackingList]", param);
        }
        /// <summary>
        /// 衣壁柜数据导入数据库
        /// </summary>
        public static void CommitTheClothesClosetToDB(DataTable FuyeIssueMaterialBillsDataTable, DataTable GuitiIssueMaterialBillsDataTable, DataTable Li_PackingListTable, DataTable IssueMaterialBillsDataTable, DataTable GetMaterialBillsDataTable, DataTable PackingListTable, DataTable GuitiGetMaterialBillsDataTable)
        {
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@FuyeIssueMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=FuyeIssueMaterialBillsDataTable
                },
                new SqlParameter("@GuitiIssueMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=GuitiIssueMaterialBillsDataTable
                },
                new SqlParameter("@Li_PackingListTable",SqlDbType.Structured)
                {
                    Value=Li_PackingListTable
                },
                new SqlParameter("@IssueMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=IssueMaterialBillsDataTable
                },
                new SqlParameter("@GetMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=GetMaterialBillsDataTable
                },
                new SqlParameter("@PackingListTable",SqlDbType.Structured)
                {
                    Value=PackingListTable
                },
                new SqlParameter("@GuitiGetMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=GuitiGetMaterialBillsDataTable
                }
            };
            DBUtility.ExecStoredProcedure("[InsertCommitTheClothesCloset]", para);
        }




        /// <summary>
        /// 无毒系列作业单，领料单导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitNon_toxicSeriesOfMaterialsTableToDB(DataTable NoPaintDoorSheet, DataTable GetMaterialBills)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_NoPaintDoorSheet", SqlDbType.Structured)
                {
                    Value = NoPaintDoorSheet
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBills
                }
            };
            DBUtility.ExecStoredProcedure("InsertNoPaintDoorSheetTOIssueMaterialBillsAndGetMaterialBills", param);

        }





        /// <summary>
        /// 速美免漆门板单，免漆料单导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitQuickBeautyFreePaintDoorSheetTableToDB(DataTable IssueMaterialBills, DataTable GetMaterialBills)
        {
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@Pro_QuickBeautyFreePaintDoorSheet Pro_QuickBeautyFreePaintDoorSheet", SqlDbType.Structured)
                {
                    Value = IssueMaterialBills
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBills
                }
            };

            DBUtility.ExecStoredProcedure("[InsertQuickBeautyFreePaintDoorSheetTOIssueMaterialBillsAndGetMaterialBills]", param);
        }





        ///CommitNoPaintDoorSheetTableToDB

        /// <summary>
        /// 免漆门板单，免漆料单导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitNoPaintDoorSheetTableToDB(DataTable IssueMaterialBills, DataTable GetMaterialBills)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_NorthPointExperienceMuseumDataTable", SqlDbType.Structured)
                {
                    Value = IssueMaterialBills
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBills
                }
            };

            DBUtility.ExecStoredProcedure("[InsertNorthPointExperienceMuseumData]", param);
        }

        //CommitNontoxicPlateDoorTemplateTableToDB
        /// <summary>
        /// 无毒平板门板模板
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitNontoxicPlateDoorTemplateTableToDB(DataTable IssueMaterialBills, DataTable GetMaterialBills)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_WenqiDoorPlank", SqlDbType.Structured)
                {
                    Value = IssueMaterialBills
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBills
                }
            };

            DBUtility.ExecStoredProcedure("[InsertWenqiDoorPlankTOIssueMaterialBillsAndGetMaterialBills]", param);
        }
        //CommitBlisterTemplateMESTableToDB
        /// <summary>
        /// 吸塑模板导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitBlisterTemplateMESTableToDB(DataTable BlisterTemplateMESTable, DataTable GetMaterialBills)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_WenqiDoorPlank", SqlDbType.Structured)
                {
                    Value = BlisterTemplateMESTable
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBills
                }
            };

            DBUtility.ExecStoredProcedure("[InsertWenqiDoorPlankTOIssueMaterialBillsAndGetMaterialBills]", param);
        }
        /// <summary>
        /// 无毒柜体导入数据库
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void CommitNon_ToxicCabinetTableToDB(DataTable orderDaetail, DataTable IssueMaterialBills, DataTable GetMaterialBillsDataTable, DataTable PackingListTable, DataTable AluminiumSheetSlassTable1, DataTable AluminiumSheetSlassTable2, DataTable Packing_ListTable, DataTable Packing_ListA6Table)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_OrderDetail", SqlDbType.Structured)
                {
                    Value = orderDaetail
                },
                new SqlParameter("@Pro_IssueMaterialBills", SqlDbType.Structured)
                {
                    Value = IssueMaterialBills
                },
                new SqlParameter("@Pro_GetMaterialBills", SqlDbType.Structured)
                {
                    Value = GetMaterialBillsDataTable
                },
                new SqlParameter("@Pro_PackingList", SqlDbType.Structured)
                {
                    Value = PackingListTable
                },
                new SqlParameter("@Pro_AluminiumSheetGlass", SqlDbType.Structured)
                {
                    Value = AluminiumSheetSlassTable1
                },
                new SqlParameter("@Pro_AluminiumSheetGlass2", SqlDbType.Structured)
                {
                    Value = AluminiumSheetSlassTable2
                },
                new SqlParameter("@Pro_Packaging", SqlDbType.Structured)
                {
                    Value = Packing_ListTable
                },
                new SqlParameter("@Pro_PackagingA6", SqlDbType.Structured)
                {
                    Value = Packing_ListA6Table
                }
            };
           
            DBUtility.ExecStoredProcedure("[InsertCabinetMaterialListToSQL]", param);
        }
        
        
        
        
        //CommitBlisterTemplateMESTableToDB
        /// <summary>
        /// 包装
        /// </summary>
        /// <param name="orderDaetail"></param>
        /// <param name="IssueMaterialBills"></param>
        public static void Packing_ListTableTableDataToSQL(DataTable Packing_ListTable)
        {
            SqlParameter[] param = new SqlParameter[]{
                 new SqlParameter("@Pro_Packaging", SqlDbType.Structured)
                {
                    Value = Packing_ListTable
                }
            };

            DBUtility.ExecStoredProcedure("[Insert_Packaging]", param);
        }



        /// <summary>
        /// CheckTicket
        /// </summary>
        public static void CheckTicketDataToDB(DataTable orderDetailDataTable)
        {
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@FuyeIssueMaterialBillsDataTable",SqlDbType.Structured)
                {
                    Value=orderDetailDataTable
                }
            };
            DBUtility.ExecStoredProcedure("[InsertCommitTheClothesCloset]", para);
        }


        /// <summary>
        /// 导入吸塑门板领料单数据
        /// </summary>
        /// <param name="BlisterDoorPlankMaterialRequisitionTable"></param>
        public static void CommitBlisterDoorPlankMaterialRequisitionMESTableToDB(DataTable BlisterDoorPlankMaterialRequisitionTable)
        {
            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@BlisterDoorPlankMaterialRequisitionTable",SqlDbType.Structured)
                {
                    Value=BlisterDoorPlankMaterialRequisitionTable
                }
            };
            DBUtility.ExecStoredProcedure("[InsertBlisterDoorPlankMaterialRequisitionTable]", para);
        }

    }
}
