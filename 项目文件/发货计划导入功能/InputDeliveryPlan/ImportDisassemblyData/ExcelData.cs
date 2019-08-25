using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ImportDisassemblyData
{
    /// <summary>
    /// 柜体ToSQL
    /// </summary>
    public abstract class ExcelData
    {
        protected DataTable orderDetailDataTable = new DataTable();
        protected DataTable materialBillsDataTable = new DataTable();
        protected DataTable GetMaterialBillsDataTable = new DataTable();
        protected DataTable PackingListTable = new DataTable();
        protected DataTable GlassIssueMaterialBills1Table = new DataTable();
        protected DataTable GlassIssueMaterialBills2Table = new DataTable();
        protected DataTable Packing_ListTable = new DataTable();
        protected DataTable Packing_ListA6Table = new DataTable();
        protected DataTable A6NoPaintSheetTable = new DataTable();
        protected DataTable MeiWenqiBlankingSheetTable = new DataTable();
        protected DataTable MeiWenqiRequisitionSheetTable = new DataTable();
        protected DataTable A6WenqiBlankingSheetTable = new DataTable();
        protected DataTable SuctionMoldingDoorSheetMaterialListTable = new DataTable();
        protected DataTable FeedSingleBlisterDoorPlankSheetTable = new DataTable();
        protected string ParentId;

        public ExcelData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //吸塑门板下料单
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add("Qty", typeof(float));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //吸塑门板下料单
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutLong", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutWide", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutHigh", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutThick", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutQty", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FLong", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FWide", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FHigh", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("Qty", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side1"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side2"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side3"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side4"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //创建table添加列 A6免漆领料单
            A6NoPaintSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add("Qty", typeof(float));
            A6NoPaintSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));

            //DataTable MeiWenqiBlankingSheetTable = new DataTable();速美免漆下料单
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add("CutThick", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutWide", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutHigh", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutQty", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("FWide", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("FHigh", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("Qty", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("CombineMark"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //速美免漆领料单
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add("Qty", typeof(float));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //A6免漆下料单
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add("FWide", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("FHigh", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("Qty", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutThick", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutWide", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutHigh", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutQty", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));



            //下料单
            orderDetailDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductDescription"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add("Wide", typeof(float));
            orderDetailDataTable.Columns.Add("High", typeof(float));
            orderDetailDataTable.Columns.Add("Deth", typeof(float));
            orderDetailDataTable.Columns.Add("Qty", typeof(float));
            orderDetailDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            materialBillsDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            materialBillsDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            //materialBillsDataTable.Columns.Add(string.Format("CustomerName"), Type.GetType("System.String"));//客户名称
            //materialBillsDataTable.Columns.Add(string.Format("Materials"), Type.GetType("System.String"));//材质
            //materialBillsDataTable.Columns.Add(string.Format("VersionNumber"), Type.GetType("System.String"));//
            //materialBillsDataTable.Columns.Add(string.Format("TheNameOfThePanel"), Type.GetType("System.String"));//
            materialBillsDataTable.Columns.Add("CutThick", typeof(float));//厚
            materialBillsDataTable.Columns.Add("CutHigh", typeof(float));//高
            materialBillsDataTable.Columns.Add("CutWide", typeof(float));//宽
            materialBillsDataTable.Columns.Add("CutQty", typeof(float));//数量
            materialBillsDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注

            //领料单
            GetMaterialBillsDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //装箱清单
            PackingListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            //铝材玻璃单
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //铝材玻璃单
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));



            //A6包装
            Packing_ListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListTable.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListTable.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListTable.Columns.Add("Qty", typeof(float));//数量

            Packing_ListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));



            //A6包装
            Packing_ListA6Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListA6Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListA6Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListA6Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListA6Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListA6Table.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListA6Table.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListA6Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListA6Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListA6Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListA6Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListA6Table.Columns.Add("Qty", typeof(float));//数量

            Packing_ListA6Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListA6Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListA6Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListA6Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListA6Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadExcelData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportExcelDataToDB()
        {
            DissaccemblyDataTable.CommitDataTableToDB(orderDetailDataTable, materialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, GlassIssueMaterialBills1Table, GlassIssueMaterialBills2Table, Packing_ListTable, Packing_ListA6Table, A6NoPaintSheetTable, MeiWenqiBlankingSheetTable, MeiWenqiRequisitionSheetTable, A6WenqiBlankingSheetTable, SuctionMoldingDoorSheetMaterialListTable, FeedSingleBlisterDoorPlankSheetTable);

        }

    }

    /// <summary>
    /// 盛可居数据导入SQL
    /// </summary>
    public abstract class ShengCanJuToSQL
    {
        protected string ParentId;
        //下料单
        protected DataTable CabinetTable = new DataTable();
        protected DataTable IssueMaterialBillsTable = new DataTable();
        //铝材玻璃单
        protected DataTable AluminumGlassLeftTable = new DataTable();
        protected DataTable AluminumGlassRightTable = new DataTable();
        //铝材玻璃单
        protected DataTable MaterialRequisitionTopTable = new DataTable();
        protected DataTable MaterialRequisitionUnderTable = new DataTable();
        //A6包装
        protected DataTable A6PackagingSheetTable = new DataTable();
        //速美包装
        protected DataTable SpeedDfBeautyTable = new DataTable();
        //平板门板作业单
        protected DataTable PlateDoorSheetWorkSheetTable = new DataTable();
        //平板门板领料单
        protected DataTable PlateDoorHandleSheetTable = new DataTable();
        //古典门板作业单
        protected DataTable ClassicDoorSheetTable = new DataTable();
        //古典门板领料单
        protected DataTable ClassicDoorHandleSheetTable = new DataTable();
        //西迪布赛作业单
        protected DataTable SidibJobListSheetTable = new DataTable();

        //西迪布赛领料单
        protected DataTable SiddibRequisitionFormSheetTable = new DataTable();
        //图兰朵作业单
        protected DataTable TurandotWorkSheetTable = new DataTable();
        //图兰朵领料单
        protected DataTable MaterialRequisitionSheetTable = new DataTable();
        //图兰朵黑檀作业单
        protected DataTable TurandotEbonyWorkSheetTable = new DataTable();
        //图兰朵黑檀领料单
        protected DataTable TurandotBlackSandalwoodMaterialListTable = new DataTable();

        public ShengCanJuToSQL()
        {
            CreateDataTable();
        }
        void CreateDataTable()
        {
            //下料单
            //柜体表
            CabinetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            CabinetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            CabinetTable.Columns.Add(string.Format("ProductDescription"), Type.GetType("System.String"));
            CabinetTable.Columns.Add(string.Format("Wide"), typeof(float));
            CabinetTable.Columns.Add(string.Format("High"), typeof(float));
            CabinetTable.Columns.Add(string.Format("Deth"), typeof(float));
            CabinetTable.Columns.Add(string.Format("Qty"), typeof(float));
            CabinetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //下料单
            //板件表
            IssueMaterialBillsTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("CutLong"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("CutWide"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("CutHigh"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("CutThick"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("CutQty"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("FLong"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("FWide"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("FHigh"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("FThick"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("Qty"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("Side1"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("Side2"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("Side3"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("Side4"), typeof(float));
            IssueMaterialBillsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            IssueMaterialBillsTable.Columns.Add(string.Format("FaceConduct"), typeof(float));

            //铝材玻璃单
            //左侧
            AluminumGlassLeftTable.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("Num"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("Qty"), typeof(float));
            AluminumGlassLeftTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            AluminumGlassLeftTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //铝材玻璃单
            //右侧
            AluminumGlassRightTable.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("Num"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("Qty"), typeof(float));
            AluminumGlassRightTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            AluminumGlassRightTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //领料单
            MaterialRequisitionTopTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add(string.Format("NumericalOrder"), typeof(float));
            MaterialRequisitionTopTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            MaterialRequisitionTopTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add("Qty", typeof(float));
            MaterialRequisitionTopTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MaterialRequisitionTopTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //领料单
            MaterialRequisitionUnderTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("NumericalOrder"), typeof(float));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            MaterialRequisitionUnderTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add("Qty", typeof(float));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MaterialRequisitionUnderTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //A6包装
            A6PackagingSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            A6PackagingSheetTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            A6PackagingSheetTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            A6PackagingSheetTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            A6PackagingSheetTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            A6PackagingSheetTable.Columns.Add("Quantity", typeof(float));//数量
            A6PackagingSheetTable.Columns.Add("Thickness", typeof(float));//厚度
            A6PackagingSheetTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            A6PackagingSheetTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码
            A6PackagingSheetTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            A6PackagingSheetTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            A6PackagingSheetTable.Columns.Add("Qty", typeof(float));//数量
            A6PackagingSheetTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配
            A6PackagingSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            A6PackagingSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            A6PackagingSheetTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            A6PackagingSheetTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));

            //速美包装
            SpeedDfBeautyTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            SpeedDfBeautyTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            SpeedDfBeautyTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            SpeedDfBeautyTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            SpeedDfBeautyTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            SpeedDfBeautyTable.Columns.Add("Quantity", typeof(float));//数量
            SpeedDfBeautyTable.Columns.Add("Thickness", typeof(float));//厚度

            SpeedDfBeautyTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            SpeedDfBeautyTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            SpeedDfBeautyTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            SpeedDfBeautyTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            SpeedDfBeautyTable.Columns.Add("Qty", typeof(float));//数量

            SpeedDfBeautyTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            SpeedDfBeautyTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            SpeedDfBeautyTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            SpeedDfBeautyTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            SpeedDfBeautyTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));

            //平板门班作业单
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add("CutLong", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("CutWide", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("CutThick", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("CutQty", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("FLong", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("FWide", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add("Qty", typeof(float));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            PlateDoorSheetWorkSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //平板门板领料单
            PlateDoorHandleSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add("Qty", typeof(float));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PlateDoorHandleSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //古典门板作业单
            ClassicDoorSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add("CutLong", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutWide", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutThick", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutQty", typeof(float));
            ClassicDoorSheetTable.Columns.Add("FLong", typeof(float));
            ClassicDoorSheetTable.Columns.Add("FWide", typeof(float));
            ClassicDoorSheetTable.Columns.Add("Qty", typeof(float));
            ClassicDoorSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //古典门板领料单
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add("Qty", typeof(float));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //西迪布赛作业单
            SidibJobListSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add("CutLong", typeof(float));
            SidibJobListSheetTable.Columns.Add("CutWide", typeof(float));
            SidibJobListSheetTable.Columns.Add("CutHigh", typeof(float));
            SidibJobListSheetTable.Columns.Add("CutThick", typeof(float));
            SidibJobListSheetTable.Columns.Add("CutQty", typeof(float));
            SidibJobListSheetTable.Columns.Add("FLong", typeof(float));
            SidibJobListSheetTable.Columns.Add("FWide", typeof(float));
            SidibJobListSheetTable.Columns.Add("FHigh", typeof(float));
            SidibJobListSheetTable.Columns.Add("FQty", typeof(float));
            SidibJobListSheetTable.Columns.Add(string.Format("Side1"), typeof(float));
            SidibJobListSheetTable.Columns.Add(string.Format("Side2"), typeof(float));
            SidibJobListSheetTable.Columns.Add(string.Format("Side3"), typeof(float));
            SidibJobListSheetTable.Columns.Add(string.Format("Side4"), typeof(float));
            SidibJobListSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String")); //财质汇总
            SidibJobListSheetTable.Columns.Add(string.Format("AreaOfContour"), typeof(float)); //投影面积
            SidibJobListSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));//加工备注
            SidibJobListSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            SidibJobListSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //西迪布赛领料单
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add("Qty", typeof(float));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            SiddibRequisitionFormSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));


            //图兰朵作业单
            TurandotWorkSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add("CutLong", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutWide", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutHigh", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutThick", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutQty", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FLong", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FWide", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FHigh", typeof(float));
            TurandotWorkSheetTable.Columns.Add("Qty", typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("Side1"), typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("Side2"), typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("Side3"), typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("Side4"), typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String")); //财质汇总
            TurandotWorkSheetTable.Columns.Add(string.Format("AreaOfContour"), typeof(float)); //投影面积
            TurandotWorkSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));//加工备注
            TurandotWorkSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//财质备注
            TurandotWorkSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //图兰朵领料单
            MaterialRequisitionSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add("Qty", typeof(float));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));


            //图兰朵黑檀作业单
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//类型
            TurandotEbonyWorkSheetTable.Columns.Add("CutWide", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("CutHigh", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("CutThick", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("CutQty", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("FWide", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("FHigh", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add("FQty", typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("Side1"), typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("Side2"), typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("Side3"), typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("Side4"), typeof(float));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("TheNumberOfFloorSales"), Type.GetType("System.String"));//层板销售数量
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            TurandotEbonyWorkSheetTable.Columns.Add(string.Format("PositionDescription"), Type.GetType("System.String"));//位置说明
            //图兰朵黑檀领料单
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add("Qty", typeof(float));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            TurandotBlackSandalwoodMaterialListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadShengCanJuData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ShengCanJuDataToDB()
        {
            DissaccemblyDataTable.ShengCanJuTableToDB(CabinetTable, IssueMaterialBillsTable, AluminumGlassLeftTable, AluminumGlassRightTable, MaterialRequisitionTopTable, MaterialRequisitionUnderTable, A6PackagingSheetTable, SpeedDfBeautyTable, PlateDoorSheetWorkSheetTable, PlateDoorHandleSheetTable, ClassicDoorSheetTable, ClassicDoorHandleSheetTable, SidibJobListSheetTable, SiddibRequisitionFormSheetTable, TurandotWorkSheetTable, MaterialRequisitionSheetTable, TurandotEbonyWorkSheetTable, TurandotBlackSandalwoodMaterialListTable);
        }

    }
    /// <summary>
    /// 领料单ToSQL
    /// </summary>
    public abstract class GetMaterialBillsDataToSQL
    {
        protected DataTable GetMaterialBillsDataTable = new DataTable();
        protected DataTable PackingListTable = new DataTable();
        protected string ParentId;

        public GetMaterialBillsDataToSQL()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));

            PackingListTable.Columns.Add(string.Format("PackingListId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadGetMaterialBillsData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitGetMaterialBillsTableToDB(GetMaterialBillsDataTable, PackingListTable);

        }

    }
    /// <summary>
    /// 无毒系列作业单，无毒系列领料单
    /// </summary>
    public abstract class Non_toxicSeriesOfMaterialsData
    {
        //无度系列作业单
        protected DataTable Non_toxicSeriesOfMaterialsTable = new DataTable();
        //无毒系列领料单
        protected DataTable GetMaterialBillsTable = new DataTable();
        protected string ParentId;

        public Non_toxicSeriesOfMaterialsData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //无毒系列作业单
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutThick", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutQty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("Qty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //无毒系列领料单
            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNon_toxicSeriesOfMaterialsData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitNon_toxicSeriesOfMaterialsTableToDB(Non_toxicSeriesOfMaterialsTable, GetMaterialBillsTable);

        }

    }
    /// <summary>
    /// 速美免漆门板单，免漆料单
    /// </summary>
    public abstract class QuickBeautyFreePaintDoorSheetData
    {
        protected DataTable NoPaintDoorSheetTable = new DataTable();
        protected DataTable GetMaterialBillsTable = new DataTable();
        protected string ParentId;

        public QuickBeautyFreePaintDoorSheetData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            NoPaintDoorSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("PlateName", Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("CutHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutThick", Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("CutQty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("Qty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));//加工备注
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色(材质备注)
            //NoPaintDoorSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            NoPaintDoorSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            NoPaintDoorSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            //NoPaintDoorSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            NoPaintDoorSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注



            GetMaterialBillsTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadQuickBeautyFreePaintDoorSheetData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitNoPaintDoorSheetTableToDB(NoPaintDoorSheetTable, GetMaterialBillsTable);

        }

    }

    /// <summary>
    /// 盛可居
    /// </summary>
    public abstract class NorthPointExperienceMuseumData
    {
        protected DataTable NoPaintDoorSheetTable = new DataTable();
        protected DataTable GetMaterialBillsTable = new DataTable();

        public NorthPointExperienceMuseumData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            NoPaintDoorSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("PlateName", Type.GetType("System.String"));

            NoPaintDoorSheetTable.Columns.Add("CutWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutQty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutThick", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FQty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FThick", typeof(float));
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            NoPaintDoorSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));

            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNorthPointExperienceMuseumData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void NorthPointExperienceMuseumDataToDB()
        {
            DissaccemblyDataTable.CommitNoPaintDoorSheetTableToDB(NoPaintDoorSheetTable, GetMaterialBillsTable);

        }
    }

    /// <summary>
    /// 盛可居柜体ToSQL
    /// </summary>
    public abstract class NorthPointCabinetData
    {
        protected DataTable orderDetailDataTable = new DataTable();
        protected DataTable materialBillsDataTable = new DataTable();
        protected DataTable GetMaterialBillsDataTable = new DataTable();
        protected DataTable PackingListTable = new DataTable();
        protected DataTable GlassIssueMaterialBills1Table = new DataTable();
        protected DataTable GlassIssueMaterialBills2Table = new DataTable();
        protected DataTable Packing_ListTable = new DataTable();
        protected DataTable Packing_ListA6Table = new DataTable();
        protected DataTable A6NoPaintSheetTable = new DataTable();
        protected DataTable MeiWenqiBlankingSheetTable = new DataTable();
        protected DataTable MeiWenqiRequisitionSheetTable = new DataTable();
        protected DataTable A6WenqiBlankingSheetTable = new DataTable();
        protected DataTable SuctionMoldingDoorSheetMaterialListTable = new DataTable();
        protected DataTable FeedSingleBlisterDoorPlankSheetTable = new DataTable();
        protected string ParentId;

        public NorthPointCabinetData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //吸塑门板下料单
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add("Qty", typeof(float));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            SuctionMoldingDoorSheetMaterialListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //吸塑门板下料单
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutLong", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutWide", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutHigh", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutThick", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("CutQty", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FLong", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FWide", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("FHigh", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add("Qty", typeof(float));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side1"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side2"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side3"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Side4"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            FeedSingleBlisterDoorPlankSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //创建table添加列 A6免漆领料单
            A6NoPaintSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add("Qty", typeof(float));
            A6NoPaintSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            A6NoPaintSheetTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));

            //DataTable MeiWenqiBlankingSheetTable = new DataTable();速美免漆下料单
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add("CutThick", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutWide", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutHigh", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("CutQty", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("FWide", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("FHigh", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add("Qty", typeof(float));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("CombineMark"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MeiWenqiBlankingSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //速美免漆领料单
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add("Qty", typeof(float));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MeiWenqiRequisitionSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //A6免漆下料单
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add("FWide", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("FHigh", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("Qty", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutThick", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutWide", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutHigh", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add("CutQty", typeof(float));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            A6WenqiBlankingSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));



            //下料单
            orderDetailDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductDescription"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add("Wide", typeof(float));
            orderDetailDataTable.Columns.Add("High", typeof(float));
            orderDetailDataTable.Columns.Add("Deth", typeof(float));
            orderDetailDataTable.Columns.Add("Qty", typeof(float));
            orderDetailDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            materialBillsDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            materialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            materialBillsDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            //materialBillsDataTable.Columns.Add(string.Format("CustomerName"), Type.GetType("System.String"));//客户名称
            //materialBillsDataTable.Columns.Add(string.Format("Materials"), Type.GetType("System.String"));//材质
            //materialBillsDataTable.Columns.Add(string.Format("VersionNumber"), Type.GetType("System.String"));//
            //materialBillsDataTable.Columns.Add(string.Format("TheNameOfThePanel"), Type.GetType("System.String"));//
            materialBillsDataTable.Columns.Add("CutThick", typeof(float));//厚
            materialBillsDataTable.Columns.Add("CutHigh", typeof(float));//高
            materialBillsDataTable.Columns.Add("CutWide", typeof(float));//宽
            materialBillsDataTable.Columns.Add("CutQty", typeof(float));//数量
            materialBillsDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注

            //领料单
            GetMaterialBillsDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //装箱清单
            PackingListTable.Columns.Add(string.Format("PackingListId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            //铝材玻璃单
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GlassIssueMaterialBills1Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //铝材玻璃单
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GlassIssueMaterialBills2Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));



            //A6包装
            Packing_ListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListTable.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListTable.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListTable.Columns.Add("Qty", typeof(float));//数量

            Packing_ListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));



            //A6包装
            Packing_ListA6Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListA6Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListA6Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListA6Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListA6Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListA6Table.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListA6Table.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListA6Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListA6Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListA6Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListA6Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListA6Table.Columns.Add("Qty", typeof(float));//数量

            Packing_ListA6Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListA6Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListA6Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListA6Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListA6Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNorthPointCabinetData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ReadNorthPointCabinetDataToDB()
        {
            DissaccemblyDataTable.CommitDataTableToDB(orderDetailDataTable, materialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, GlassIssueMaterialBills1Table, GlassIssueMaterialBills2Table, Packing_ListTable, Packing_ListA6Table, A6NoPaintSheetTable, MeiWenqiBlankingSheetTable, MeiWenqiRequisitionSheetTable, A6WenqiBlankingSheetTable, SuctionMoldingDoorSheetMaterialListTable, FeedSingleBlisterDoorPlankSheetTable);

        }

    }


    /// <summary>
    /// 免漆门板单，免漆料单
    /// </summary>
    public abstract class NoPaintDoorSheetData
    {
        protected DataTable NoPaintDoorSheetTable = new DataTable();
        protected DataTable GetMaterialBillsTable = new DataTable();
        protected string ParentId;

        public NoPaintDoorSheetData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            NoPaintDoorSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("PlateName", Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("CutHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("CutThick", Type.GetType("System.String"));
            NoPaintDoorSheetTable.Columns.Add("CutQty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FWide", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("FHigh", typeof(float));
            NoPaintDoorSheetTable.Columns.Add("Qty", typeof(float));
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));//加工备注
            NoPaintDoorSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色(材质备注)
            //NoPaintDoorSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            NoPaintDoorSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            NoPaintDoorSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            NoPaintDoorSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            NoPaintDoorSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注

            GetMaterialBillsTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNoPaintDoorSheetData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitNoPaintDoorSheetTableToDB(NoPaintDoorSheetTable, GetMaterialBillsTable);

        }

    }

    /// <summary>
    /// 无毒平板门板模板
    /// </summary>
    public abstract class NontoxicPlateDoorTemplateData
    {
        protected DataTable NontoxicPlateDoorTemplateTable = new DataTable();
        protected DataTable GetMaterialBillsTable = new DataTable();
        protected string ParentId;

        public NontoxicPlateDoorTemplateData()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add("CutLong", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add("CutWide", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add("CutQty", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add("FLong", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add("FWide", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add("FThick", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add("Qty", typeof(float));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            NontoxicPlateDoorTemplateTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));





            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNontoxicPlateDoorTemplateData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitNontoxicPlateDoorTemplateTableToDB(NontoxicPlateDoorTemplateTable, GetMaterialBillsTable);

        }

    }



    /// <summary>
    /// 吸塑模板MES模板
    /// </summary>
    public abstract class BlisterTemplateMES
    {
        protected DataTable BlisterTemplateMESTable = new DataTable();
        protected DataTable GetMaterialBillsTable = new DataTable();
        protected string ParentId;

        public BlisterTemplateMES()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            BlisterTemplateMESTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add("CutHigh", typeof(float));
            BlisterTemplateMESTable.Columns.Add("CutWide", typeof(float));
            BlisterTemplateMESTable.Columns.Add("CutThick", typeof(float));
            BlisterTemplateMESTable.Columns.Add("CutQty", typeof(float));
            BlisterTemplateMESTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add("FLong", typeof(float));
            BlisterTemplateMESTable.Columns.Add("FWide", typeof(float));
            BlisterTemplateMESTable.Columns.Add("FHigh", typeof(float));
            BlisterTemplateMESTable.Columns.Add("Qty", typeof(float));
            BlisterTemplateMESTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            BlisterTemplateMESTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));


            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            //GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadBlisterTemplateMESData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitBlisterTemplateMESTableToDB(BlisterTemplateMESTable, GetMaterialBillsTable);

        }

    }

    /// <summary>
    /// 吸塑模板MES领料单模板
    /// </summary>
    public abstract class BlisterDoorPlankMaterialRequisitionMES
    {
        protected DataTable BlisterDoorPlankMaterialRequisitionTable = new DataTable();
        protected string ParentId;

        public BlisterDoorPlankMaterialRequisitionMES()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {

            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add("Qty", typeof(float));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            BlisterDoorPlankMaterialRequisitionTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadBlisterDoorPlankMaterialRequisitionMESData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitBlisterDoorPlankMaterialRequisitionMESTableToDB(BlisterDoorPlankMaterialRequisitionTable);

        }

    }

    /// <summary>
    /// 无毒厨浴柜柜体ToSQL2018.7.6
    /// </summary>
    public abstract class Non_ToxicCabinetTable
    {
        protected DataTable orderDetailDataTable = new DataTable();
        protected DataTable materialBillsDataTable = new DataTable();
        //装箱清单不导入数据库
        //protected DataTable GetMaterialBillsDataTable = new DataTable();
        protected DataTable GetMaterialBillsDataTable1 = new DataTable();
        protected DataTable PackingListTable = new DataTable();
        protected DataTable AluminiumSheetSlassTable1 = new DataTable();
        protected DataTable AluminiumSheetSlassTable2 = new DataTable();
        protected DataTable Packing_ListTable = new DataTable();
        protected DataTable Packing_ListA6Table = new DataTable();
        //无毒平板下料单
        protected DataTable Non_toxicSeriesOfMaterialsTable = new DataTable();
        //无毒平板领料单
        protected DataTable GetMaterialBillsTable = new DataTable();

        //帕格尼尼下料单
        protected DataTable PaGeNiNiIssueMaterialBillsTable = new DataTable();
        //帕格尼尼领料单
        protected DataTable PaGeNiNiGetMaterialBillsTable = new DataTable();

        //西迪布赛下料单
        protected DataTable XIDiBuSaiIssueMaterialBillsTable = new DataTable();
        //西迪布赛领料单
        protected DataTable XIDiBuSaiGetMaterialBillsTable = new DataTable();

        //齐彭代尔下料单
        protected DataTable QiPengDaiErIssueMaterialBillsTable = new DataTable();
        //齐彭代尔领料单
        protected DataTable QiPengDaiErGetMaterialBillsTable = new DataTable();
        //图兰朵下料单
        protected DataTable TurandotWorkSheetTable = new DataTable();
        //图兰朵领料单
        protected DataTable MaterialRequisitionSheetTable = new DataTable();

        //古典门板作业单
        protected DataTable ClassicDoorSheetTable = new DataTable();
        //古典门板领料单
        protected DataTable ClassicDoorHandleSheetTable = new DataTable();
        //现代新中式作业单
        protected DataTable XianDaiXinZhongShiIssueMaterialBillsTable = new DataTable();
        //现代新中式领料单
        protected DataTable XianDaiXinZhongShiGetMaterialBillsTable = new DataTable();

        protected string ParentId;

        public Non_ToxicCabinetTable()
        {
            CreateNon_ToxicCabinetTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateNon_ToxicCabinetTable()
        {

            orderDetailDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add("Wide", typeof(float));
            orderDetailDataTable.Columns.Add("High", typeof(float));
            orderDetailDataTable.Columns.Add("Deth", typeof(float));
            orderDetailDataTable.Columns.Add("Qty", typeof(float));
            orderDetailDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            materialBillsDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//纹理
            materialBillsDataTable.Columns.Add(string.Format("CutThick"), Type.GetType("System.String"));
            //materialBillsDataTable.Columns.Add(string.Format("CutLong"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("CutHigh"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("CutWide"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("CutQty"), Type.GetType("System.String"));//下料数量
            materialBillsDataTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));//成型数量
            materialBillsDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            materialBillsDataTable.Columns.Add(string.Format("CabinetNO"), Type.GetType("System.String"));//柜号
            //materialBillsDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质备注
            materialBillsDataTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));//材质汇总
            materialBillsDataTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            


            //装箱清单不导入数据库
            //GetMaterialBillsDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            ////GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));

            GetMaterialBillsDataTable1.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            //GetMaterialBillsDataTable1.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            GetMaterialBillsDataTable1.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));


            PackingListTable.Columns.Add(string.Format("PackingListId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));


            AluminiumSheetSlassTable1.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            AluminiumSheetSlassTable1.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable1.Columns.Add(string.Format("AluminiumSheetGlassNum"), Type.GetType("System.String"));
            AluminiumSheetSlassTable1.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            AluminiumSheetSlassTable1.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable1.Columns.Add(string.Format("Cells"), Type.GetType("System.String"));
            AluminiumSheetSlassTable1.Columns.Add("Qty", typeof(float));
            AluminiumSheetSlassTable1.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            AluminiumSheetSlassTable1.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable1.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));


            AluminiumSheetSlassTable2.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));
            AluminiumSheetSlassTable2.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable2.Columns.Add(string.Format("AluminiumSheetGlassNum"), Type.GetType("System.String"));
            AluminiumSheetSlassTable2.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));
            AluminiumSheetSlassTable2.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable2.Columns.Add(string.Format("Cells"), Type.GetType("System.String"));
            AluminiumSheetSlassTable2.Columns.Add("Qty", typeof(float));
            AluminiumSheetSlassTable2.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            AluminiumSheetSlassTable2.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //AluminiumSheetSlassTable2.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));


            //速美包装
            Packing_ListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListTable.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListTable.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListTable.Columns.Add("Qty", typeof(float));//数量

            Packing_ListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));



            //A6包装
            Packing_ListA6Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListA6Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListA6Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListA6Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListA6Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListA6Table.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListA6Table.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListA6Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListA6Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListA6Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListA6Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListA6Table.Columns.Add("Qty", typeof(float));//数量

            Packing_ListA6Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListA6Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListA6Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListA6Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListA6Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));

            //无毒平板下料单
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutThick", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutQty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("Qty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //无毒平板领料单
            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //帕格尼尼下料单
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("CutHigh", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("CutWide", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("CutThick", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("CutQty", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("FHigh", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("FWide", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add("Qty", typeof(float));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            PaGeNiNiIssueMaterialBillsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //帕格尼尼领料单
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            PaGeNiNiGetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //西迪布赛下料单
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("CutHigh", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("CutWide", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("CutThick", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("CutQty", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("FHigh", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("FWide", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add("Qty", typeof(float));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            XIDiBuSaiIssueMaterialBillsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //西迪布赛领料单
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            XIDiBuSaiGetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //齐彭代尔下料单
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("CutHigh", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("CutWide", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("CutThick", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("CutQty", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("FHigh", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("FWide", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add("Qty", typeof(float));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            QiPengDaiErIssueMaterialBillsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //齐彭代尔领料单
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            QiPengDaiErGetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //图兰朵下料单
            TurandotWorkSheetTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add("CutLong", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutWide", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutHigh", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutThick", typeof(float));
            TurandotWorkSheetTable.Columns.Add("CutQty", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FLong", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FWide", typeof(float));
            TurandotWorkSheetTable.Columns.Add("FHigh", typeof(float));
            TurandotWorkSheetTable.Columns.Add("Qty", typeof(float));
            TurandotWorkSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));////加工备注,刀型
            TurandotWorkSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//财质备注
            TurandotWorkSheetTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            TurandotWorkSheetTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式





            //图兰朵领料单
            MaterialRequisitionSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add("Qty", typeof(float));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MaterialRequisitionSheetTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));

            //古典门板作业单
            ClassicDoorSheetTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add("CutLong", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutWide", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutThick", typeof(float));
            ClassicDoorSheetTable.Columns.Add("CutQty", typeof(float));
            ClassicDoorSheetTable.Columns.Add("FLong", typeof(float));
            ClassicDoorSheetTable.Columns.Add("FWide", typeof(float));
            ClassicDoorSheetTable.Columns.Add("Qty", typeof(float));
            ClassicDoorSheetTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            ClassicDoorSheetTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));


            //古典门板领料单
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add("Qty", typeof(float));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            ClassicDoorHandleSheetTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //现代新中式作业单
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("CutLong", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("CutWide", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("CutThick", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("CutQty", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("FLong", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("FWide", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add("Qty", typeof(float));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            XianDaiXinZhongShiIssueMaterialBillsTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));


            //现代新中式领料单
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add("Qty", typeof(float));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            XianDaiXinZhongShiGetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadNon_ToxicCabinetTableData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        //public void ImportExcelDataToDB()
        //{
        //    DissaccemblyDataTable.CommitNon_ToxicCabinetTableToDB(orderDetailDataTable, materialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, AluminiumSheetSlassTable1, AluminiumSheetSlassTable2, Packing_ListTable, Packing_ListA6Table);

        //}

    }

    /// <summary>
    /// 免漆厨浴柜体数据导入SQL
    /// </summary>
    public abstract class MianqiCabinetToSQL
    {
        //创建柜体下料单对象
        protected DataTable GuiTi_Table = new DataTable();
        //创建板件下料单对象+抽屉备用条等
        protected DataTable Xialiaodan_Table = new DataTable();
        //创建铝材玻璃单对象
        protected DataTable LvcaiBoli_Table = new DataTable();
        //创建领料单对象
        protected DataTable Lingliaodan_Table = new DataTable();
        //创建A6包装对象
        protected DataTable A6BaoZhuang_Table = new DataTable();
        //创建速美包装对象
        protected DataTable SuMei_Table = new DataTable();
        //创建A6免漆下料单对象
        protected DataTable A6MianQiXialiaodan_Table = new DataTable();
        //创建A6免漆领料单对象
        protected DataTable A6MianQiLingliaodan_Table = new DataTable();

        //创建A6免漆下料单对象(2)
        protected DataTable A6MianQiXialiaodan_Table2 = new DataTable();
        //创建A6免漆领料单对象(2)
        protected DataTable A6MianQiLingliaodan_Table2 = new DataTable();


        //创建速美免漆下料单对象
        protected DataTable SuMeiMianqiXialiaodan_Table = new DataTable();
        //创建速美免漆领料单对象
        protected DataTable SuMeiMianqiLingliaodan_Table = new DataTable();
        //创建吸塑门板下料单对象
        protected DataTable XiSuMenbanXialiaodan_Table = new DataTable();
        //创建吸塑门板领料单对象
        protected DataTable XiSuMenbanLingliaodan_Table = new DataTable();
        //无毒系列作业单
        protected DataTable Non_toxicSeriesOfMaterialsTable = new DataTable();
        //无毒系列领料单
        protected DataTable GetMaterialBillsTable = new DataTable();
        //创建油漆包装对象
        protected DataTable YouQi_Table = new DataTable();
        //混油帕拉迪奥系列作业单
        protected DataTable PalaDiaoXiaLiaoDanTable = new DataTable();
        //混油帕拉迪奥系列领料单
        protected DataTable PalaDiaoLingLiaoDanTable = new DataTable();


        protected string ParentId;

        public MianqiCabinetToSQL()
        {
            CreateMianqiCabinetToSQL();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateMianqiCabinetToSQL()
        {
            //下料单对象添加列
            GuiTi_Table.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            GuiTi_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GuiTi_Table.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GuiTi_Table.Columns.Add("Wide", typeof(float));
            GuiTi_Table.Columns.Add("High", typeof(float));
            GuiTi_Table.Columns.Add("Deth", typeof(float));
            GuiTi_Table.Columns.Add("Qty", typeof(float));
            GuiTi_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            //添加板件下料单对象+抽屉备用条等列
            Xialiaodan_Table.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//板件Id
            Xialiaodan_Table.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//对应的柜体ID
            Xialiaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单Id
            Xialiaodan_Table.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            Xialiaodan_Table.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类别
            Xialiaodan_Table.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//横纹，竖纹
            Xialiaodan_Table.Columns.Add(string.Format("CutThick"), Type.GetType("System.String"));//下料厚
            Xialiaodan_Table.Columns.Add(string.Format("CutLong"), Type.GetType("System.String"));
            Xialiaodan_Table.Columns.Add(string.Format("CutHigh"), Type.GetType("System.String"));//下料高
            Xialiaodan_Table.Columns.Add(string.Format("CutWide"), Type.GetType("System.String"));//下料宽
            Xialiaodan_Table.Columns.Add(string.Format("CutQty"), Type.GetType("System.String"));//下料数量
            Xialiaodan_Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));//成型数量
            Xialiaodan_Table.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材料，材质
            Xialiaodan_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Xialiaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            Xialiaodan_Table.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));//芯材
            Xialiaodan_Table.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));//封边信息

            //添加铝材玻璃单对象列
            LvcaiBoli_Table.Columns.Add(string.Format("GlassIssueMaterialBillsId"), Type.GetType("System.String"));//物料Id
            LvcaiBoli_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            LvcaiBoli_Table.Columns.Add(string.Format("MaterialName"), Type.GetType("System.String"));//物料名称
            LvcaiBoli_Table.Columns.Add(string.Format("StandardsName"), Type.GetType("System.String"));//物料规格
            LvcaiBoli_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            LvcaiBoli_Table.Columns.Add("Qty", typeof(float));//数量
            LvcaiBoli_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//加工备注
            //添加领料单列
            Lingliaodan_Table.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            Lingliaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单Id
            Lingliaodan_Table.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));//订单Id
            Lingliaodan_Table.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));//领料单类别
            Lingliaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            Lingliaodan_Table.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            Lingliaodan_Table.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));//物料描述
            Lingliaodan_Table.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));//物料类型
            Lingliaodan_Table.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));//材质
            Lingliaodan_Table.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));//数量
            Lingliaodan_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Lingliaodan_Table.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));//装箱确认

            //添加A6包装列
            A6BaoZhuang_Table.Columns.Add(string.Format("PackagingId"), Type.GetType("System.String"));//包装Id
            A6BaoZhuang_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单Id
            A6BaoZhuang_Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//包装序号
            A6BaoZhuang_Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽
            A6BaoZhuang_Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高
            A6BaoZhuang_Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深
            A6BaoZhuang_Table.Columns.Add("Quantity", typeof(float));//数量
            A6BaoZhuang_Table.Columns.Add("Thickness", typeof(float));//厚度
            A6BaoZhuang_Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//列
            A6BaoZhuang_Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码
            A6BaoZhuang_Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型
            A6BaoZhuang_Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            A6BaoZhuang_Table.Columns.Add("Qty", typeof(float));//数量
            A6BaoZhuang_Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配
            A6BaoZhuang_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            A6BaoZhuang_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            A6BaoZhuang_Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));//包装部分
            A6BaoZhuang_Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));//包装类别

            //添加速美包装列
            SuMei_Table.Columns.Add(string.Format("PackagingId"), Type.GetType("System.String"));//包装Id
            SuMei_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单Id
            SuMei_Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//包装序号
            SuMei_Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽
            SuMei_Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高
            SuMei_Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深
            SuMei_Table.Columns.Add("Quantity", typeof(float));//数量
            SuMei_Table.Columns.Add("Thickness", typeof(float));//厚度
            SuMei_Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//列
            SuMei_Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码
            SuMei_Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型
            SuMei_Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            SuMei_Table.Columns.Add("Qty", typeof(float));//数量
            SuMei_Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配
            SuMei_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            SuMei_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            SuMei_Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));//包装部分
            SuMei_Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));//包装类别


            //添加A6免漆下料单列
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//柜体Id
            A6MianQiXialiaodan_Table.Columns.Add("FWide", typeof(float));//完工宽
            A6MianQiXialiaodan_Table.Columns.Add("FHigh", typeof(float));//完工高
            A6MianQiXialiaodan_Table.Columns.Add("FLong", typeof(float));//完工长
            A6MianQiXialiaodan_Table.Columns.Add("Qty", typeof(float));//完工数量
            A6MianQiXialiaodan_Table.Columns.Add("CutThick", typeof(float));//下料厚
            A6MianQiXialiaodan_Table.Columns.Add("CutWide", typeof(float));//下料宽
            A6MianQiXialiaodan_Table.Columns.Add("CutHigh", typeof(float));//下料高
            A6MianQiXialiaodan_Table.Columns.Add("CutLong", typeof(float));//下料长
            A6MianQiXialiaodan_Table.Columns.Add("CutQty", typeof(float));//下料数量
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色，材质备注
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));//材质汇总
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));//芯材
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//纹理
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式
            A6MianQiXialiaodan_Table.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));//门板封边信息






            //添加A6免漆领料单列
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));//订单id
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));//领料单类型
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));//材质
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));//物料描述
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));//物料类型
            A6MianQiLingliaodan_Table.Columns.Add("Qty", typeof(float));//数量
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            A6MianQiLingliaodan_Table.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));//装箱确认
            


            //添加A6免漆下料单列
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//柜体Id
            A6MianQiXialiaodan_Table2.Columns.Add("FWide", typeof(float));//完工宽
            A6MianQiXialiaodan_Table2.Columns.Add("FHigh", typeof(float));//完工高
            A6MianQiXialiaodan_Table2.Columns.Add("Qty", typeof(float));//完工数量
            A6MianQiXialiaodan_Table2.Columns.Add("CutThick", typeof(float));//下料厚
            A6MianQiXialiaodan_Table2.Columns.Add("CutWide", typeof(float));//下料宽
            A6MianQiXialiaodan_Table2.Columns.Add("CutHigh", typeof(float));//下料高
            A6MianQiXialiaodan_Table2.Columns.Add("CutQty", typeof(float));//下料数量
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色，材质备注
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));//材质汇总
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));//芯材
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//纹理
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式
            A6MianQiXialiaodan_Table2.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));//门板封边信息






            //添加A6免漆领料单列
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));//订单id
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));//领料单类型
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));//材质
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));//物料描述
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));//物料类型
            A6MianQiLingliaodan_Table2.Columns.Add("Qty", typeof(float));//数量
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            A6MianQiLingliaodan_Table2.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));//装箱确认
            




            //添加速美免漆下料单列
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//柜体Id
            SuMeiMianqiXialiaodan_Table.Columns.Add("FWide", typeof(float));//完工宽
            SuMeiMianqiXialiaodan_Table.Columns.Add("FHigh", typeof(float));//完工高
            SuMeiMianqiXialiaodan_Table.Columns.Add("FLong", typeof(float));//完工高
            SuMeiMianqiXialiaodan_Table.Columns.Add("Qty", typeof(float));//完工数量
            SuMeiMianqiXialiaodan_Table.Columns.Add("CutThick", typeof(float));//下料厚
            SuMeiMianqiXialiaodan_Table.Columns.Add("CutWide", typeof(float));//下料宽
            SuMeiMianqiXialiaodan_Table.Columns.Add("CutHigh", typeof(float));//下料高
            SuMeiMianqiXialiaodan_Table.Columns.Add("CutLong", typeof(float));//完工高
            SuMeiMianqiXialiaodan_Table.Columns.Add("CutQty", typeof(float));//下料数量
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色，材质备注
            //SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));//材质汇总
            //SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）Vein
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));//芯材
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//芯材
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式
            SuMeiMianqiXialiaodan_Table.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));//门板封边

            //添加速美免漆领料单列
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));//领料单类型
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));//材质
             SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));//物料描述
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));//物料类型
            SuMeiMianqiLingliaodan_Table.Columns.Add("Qty", typeof(float));//数量
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            SuMeiMianqiLingliaodan_Table.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));//装箱确认
            

            //添加吸塑门板下料单列
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));//柜体Id
            XiSuMenbanXialiaodan_Table.Columns.Add("FWide", typeof(float));//完工宽
            XiSuMenbanXialiaodan_Table.Columns.Add("FHigh", typeof(float));//完工高
            XiSuMenbanXialiaodan_Table.Columns.Add("FLong", typeof(float));//完工长
            XiSuMenbanXialiaodan_Table.Columns.Add("Qty", typeof(float));//完工数量
            XiSuMenbanXialiaodan_Table.Columns.Add("CutThick", typeof(float));//下料厚
            XiSuMenbanXialiaodan_Table.Columns.Add("CutWide", typeof(float));//下料宽
            XiSuMenbanXialiaodan_Table.Columns.Add("CutHigh", typeof(float));//下料高
            XiSuMenbanXialiaodan_Table.Columns.Add("CutLong", typeof(float));//下料长
            XiSuMenbanXialiaodan_Table.Columns.Add("CutQty", typeof(float));//下料数量
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质颜色，材质备注
            //XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));//材质汇总
            //XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));//投影面积
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));//拉手类型
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));//芯材
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("Vein"), Type.GetType("System.String"));//纹理
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式
            //XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));//门板封边信息
            XiSuMenbanXialiaodan_Table.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));//加工备注

            //添加吸塑门板领料单列
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));//领料单Id
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单id
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));//领料单类型
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));//面材处理方式（免漆，油漆。。。。）
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));//材质
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));//物料号
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));//物料描述
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));//物料类型
            XiSuMenbanLingliaodan_Table.Columns.Add("Qty", typeof(float));//数量
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            XiSuMenbanLingliaodan_Table.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));//装箱确认
            


            //无毒系列作业单
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutThick", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("CutQty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FHigh", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("FWide", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add("Qty", typeof(float));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            Non_toxicSeriesOfMaterialsTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));

            //无毒系列领料单
            GetMaterialBillsTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProjectName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //添加速美包装列
            YouQi_Table.Columns.Add(string.Format("PackagingId"), Type.GetType("System.String"));//包装Id
            YouQi_Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单Id
            YouQi_Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//包装序号
            YouQi_Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽
            YouQi_Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高
            YouQi_Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深
            YouQi_Table.Columns.Add("Quantity", typeof(float));//数量
            YouQi_Table.Columns.Add("Thickness", typeof(float));//厚度
            YouQi_Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//列
            YouQi_Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码
            YouQi_Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型
            YouQi_Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            YouQi_Table.Columns.Add("Qty", typeof(float));//数量
            YouQi_Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配
            YouQi_Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            YouQi_Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            YouQi_Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));//包装部分
            YouQi_Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));//包装类别

            //混油帕拉迪奥下料单
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("HandType"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add("CutHigh", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("CutWide", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("CutThick", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("CutQty", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("FHigh", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("FWide", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add("Qty", typeof(float));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("MaterialDescription"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("DisplayMaterialSummary"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("AreaOfContour"), Type.GetType("System.String"));
            PalaDiaoXiaLiaoDanTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //混油帕拉迪奥领料单
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("CateGory"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("Qty"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("Texture"), Type.GetType("System.String"));
            PalaDiaoLingLiaoDanTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadMianqiCabinetData(string Path, string ParentId);



        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        //public void ImportExcelDataToDB()
        //{
        //    DissaccemblyDataTable.CommitNon_ToxicCabinetTableToDB(Xialiaodan_Table, Xialiaodan_Table, LvcaiBoli_Table, Lingliaodan_Table, A6BaoZhuang_Table, SuMei_Table, Packing_ListTable, A6MianQiLingliaodan_Table);

        //}
    }





    /// <summary>
    /// 速美包装ToSQL
    /// </summary>
    public abstract class Packing_List
    {
        protected DataTable Packing_ListTable = new DataTable();
        protected string ParentId;

        public Packing_List()
        {
            CreatePacking_ListTableTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreatePacking_ListTableTable()
        {
            Packing_ListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListTable.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListTable.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListTable.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListTable.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListTable.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListTable.Columns.Add("Qty", typeof(float));//数量

            Packing_ListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadPacking_ListTableTableData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void Packing_ListTableTableDataToSQL()
        {
            DissaccemblyDataTable.Packing_ListTableTableDataToSQL(Packing_ListTable);

        }

    }


    /// <summary>
    /// A6包装ToSQL
    /// </summary>
    public abstract class Packing_List_ToSQL
    {
        protected DataTable Packing_ListA6Table = new DataTable();
        protected string ParentId;

        public Packing_List_ToSQL()
        {
            CreatePacking_ListTableTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreatePacking_ListTableTable()
        {
            Packing_ListA6Table.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));//订单号
            Packing_ListA6Table.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));//序号
            Packing_ListA6Table.Columns.Add(string.Format("Wide"), Type.GetType("System.String"));//宽度
            Packing_ListA6Table.Columns.Add(string.Format("High"), Type.GetType("System.String"));//高度
            Packing_ListA6Table.Columns.Add(string.Format("Depth"), Type.GetType("System.String"));//深度
            Packing_ListA6Table.Columns.Add("Quantity", typeof(float));//数量
            Packing_ListA6Table.Columns.Add("Thickness", typeof(float));//厚度

            Packing_ListA6Table.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));//空白格
            Packing_ListA6Table.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));//物料编码

            Packing_ListA6Table.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));//柜型  
            Packing_ListA6Table.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));//包装材料名称
            Packing_ListA6Table.Columns.Add("Qty", typeof(float));//数量

            Packing_ListA6Table.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));//车间调配

            Packing_ListA6Table.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));//单位
            Packing_ListA6Table.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));//备注
            Packing_ListA6Table.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Packing_ListA6Table.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadPacking_ListTableTableData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void Packing_ListTableTableDataToSQL()
        {
            DissaccemblyDataTable.Packing_ListTableTableDataToSQL(Packing_ListA6Table);

        }

    }


    /// <summary>
    /// 衣壁柜数据导入数据库
    /// </summary>
    public abstract class ClothesClosetMaterialList_ToSQL
    {
        protected DataTable GetMaterialBillsDataTable = new DataTable();
        protected DataTable PackingListTable = new DataTable();
        protected DataTable IssueMaterialBillsDataTable = new DataTable();
        protected DataTable Li_PackingListTable = new DataTable();
        protected DataTable GuitiIssueMaterialBillsDataTable = new DataTable();
        protected DataTable FuyeIssueMaterialBillsDataTable = new DataTable();
        protected DataTable GuitiGetMaterialBillsDataTable = new DataTable();
        protected string ParentId;

        public ClothesClosetMaterialList_ToSQL()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //附页衣壁柜下料单
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add("Deth", typeof(float));
            FuyeIssueMaterialBillsDataTable.Columns.Add("Wide", typeof(float));
            FuyeIssueMaterialBillsDataTable.Columns.Add("High", typeof(float));
            FuyeIssueMaterialBillsDataTable.Columns.Add("Qty", typeof(float));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("Side"), Type.GetType("System.String"));
            FuyeIssueMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //衣壁柜柜体下料单
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add("Deth", typeof(float));
            GuitiIssueMaterialBillsDataTable.Columns.Add("Wide", typeof(float));
            GuitiIssueMaterialBillsDataTable.Columns.Add("High", typeof(float));
            GuitiIssueMaterialBillsDataTable.Columns.Add("Qty", typeof(float));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("Side"), Type.GetType("System.String"));
            GuitiIssueMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //衣壁柜领料单
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add("Qty", typeof(float));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GuitiGetMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //包装
            Li_PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("Wide"), typeof(string));
            Li_PackingListTable.Columns.Add("High", typeof(string));
            Li_PackingListTable.Columns.Add("Depth", typeof(string));
            Li_PackingListTable.Columns.Add("Quantity", typeof(float));
            Li_PackingListTable.Columns.Add("Thickness", typeof(float));
            Li_PackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add("Qty", typeof(float));
            Li_PackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            Li_PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
            //吸塑
            IssueMaterialBillsDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add("Deth", typeof(float));
            IssueMaterialBillsDataTable.Columns.Add("Wide", typeof(float));
            IssueMaterialBillsDataTable.Columns.Add("High", typeof(float));
            IssueMaterialBillsDataTable.Columns.Add("Qty", typeof(float));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("Side"), Type.GetType("System.String"));
            IssueMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //吸料
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add("Qty", typeof(float));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            GetMaterialBillsDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //料单
            PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            PackingListTable.Columns.Add("Wide", typeof(float));
            PackingListTable.Columns.Add("High", typeof(float));
            PackingListTable.Columns.Add("Depth", typeof(float));
            PackingListTable.Columns.Add(string.Format("Quantity"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Thickness"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            PackingListTable.Columns.Add("Qty", typeof(float));
            PackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadClothesClosetMaterialListData(string Path, string ParentId);

        /// <summary>
        /// 调用ImportExcelDataToDB方法导入数据库
        /// </summary>
        public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        {
            DissaccemblyDataTable.CommitTheClothesClosetToDB(FuyeIssueMaterialBillsDataTable, GuitiIssueMaterialBillsDataTable, Li_PackingListTable, IssueMaterialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, GuitiGetMaterialBillsDataTable);
        }
    }


    /// <summary>
    /// 免漆衣壁柜数据导入数据库
    /// </summary>
    public abstract class MianQiYiBiGui_ToSQL
    {
        //柜体sheet表
        protected DataTable MianQiYiBiGui_GuiTiDataTable = new DataTable();
        //附页sheet表
        protected DataTable MianQiYiBiGui_FuyeDataTable = new DataTable();
        //衣壁柜料单sheet表
        protected DataTable MianQiYiBiGui_LiaoDanDataTable = new DataTable();
        //衣壁柜料单sheet表
        protected DataTable MianQiYiBiGui_ZhuangXiangQingDanDataTable = new DataTable();
        //A6包装
        protected DataTable A6Li_PackingListTable = new DataTable();
        //速美包装
        protected DataTable SuMeiPackingListTable = new DataTable();
        //吸料
        protected DataTable XiLiao_DataTable = new DataTable();
        //吸塑
        protected DataTable XiSu_DataTable = new DataTable();
        protected string ParentId;

        public MianQiYiBiGui_ToSQL()
        {
            CreateMianQiYiBiGuiTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateMianQiYiBiGuiTable()
        {

            //衣壁柜柜体下料单
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("CabinetNO"), Type.GetType("System.String"));//柜号
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式名称
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质说明
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("FThick", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("CutWide", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("CutHigh", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //附页衣壁柜下料单
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("CabinetNO"), Type.GetType("System.String"));//柜号
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式名称
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质说明
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutThick", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutWide", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutHigh", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //衣壁柜料单
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));



            //衣壁柜装箱清单GetMaterialBillsId
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("Requisition_Type"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //A6包装
            A6Li_PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Wide"), typeof(string));
            A6Li_PackingListTable.Columns.Add("High", typeof(string));
            A6Li_PackingListTable.Columns.Add("Depth", typeof(string));
            A6Li_PackingListTable.Columns.Add("Quantity", typeof(float));
            A6Li_PackingListTable.Columns.Add("Thickness", typeof(float));
            A6Li_PackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add("Qty", typeof(float));
            A6Li_PackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            //A6Li_PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
            //速美包装
            SuMeiPackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Wide"), typeof(string));
            SuMeiPackingListTable.Columns.Add("High", typeof(string));
            SuMeiPackingListTable.Columns.Add("Depth", typeof(string));
            SuMeiPackingListTable.Columns.Add("Quantity", typeof(float));
            SuMeiPackingListTable.Columns.Add("Thickness", typeof(float));
            SuMeiPackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add("Qty", typeof(float));
            SuMeiPackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            //A6Li_PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
            //吸塑
            XiSu_DataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add("CutThick", typeof(float));
            XiSu_DataTable.Columns.Add("CutWide", typeof(float));
            XiSu_DataTable.Columns.Add("CutHigh", typeof(float));
            XiSu_DataTable.Columns.Add("Qty", typeof(float));
            //XiSu_DataTable.Columns.Add(string.Format("Side"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //吸料
            XiLiao_DataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            //XiLiao_DataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            //XiLiao_DataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add("Qty", typeof(float));
            XiLiao_DataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            ////料单
            //SuMeiPackingListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            ////SuMeiPackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            ////SuMeiPackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductDesCription"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add("Qty", typeof(float));
            //SuMeiPackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadMianQiYiBiGuiData(string Path, string ParentId);

        ///// <summary>
        ///// 调用ImportExcelDataToDB方法导入数据库
        ///// </summary>
        //public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        //{
        //    DissaccemblyDataTable.CommitTheClothesClosetToDB(MianQiYiBiGui_FuyeDataTable, GuitiIssueMaterialBillsDataTable, Li_PackingListTable, IssueMaterialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, GuitiGetMaterialBillsDataTable);
        //}
    }


    /// <summary>
    /// 油漆+混油衣壁柜数据导入数据库
    /// </summary>
    public abstract class The_PaintToSQL
    {
        //柜体sheet表
        protected DataTable MianQiYiBiGui_GuiTiDataTable = new DataTable();
        //附页sheet表
        protected DataTable MianQiYiBiGui_FuyeDataTable = new DataTable();
        //衣壁柜料单sheet表
        protected DataTable MianQiYiBiGui_LiaoDanDataTable = new DataTable();
        //衣壁柜料单sheet表
        protected DataTable MianQiYiBiGui_ZhuangXiangQingDanDataTable = new DataTable();
        //A6包装
        protected DataTable A6Li_PackingListTable = new DataTable();
        //速美包装
        protected DataTable SuMeiPackingListTable = new DataTable();
        //吸料
        protected DataTable XiLiao_DataTable = new DataTable();
        //吸塑
        protected DataTable XiSu_DataTable = new DataTable();
        protected string ParentId;

        public The_PaintToSQL()
        {
            CreateThe_PaintToSQLTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateThe_PaintToSQLTable()
        {

            //衣壁柜柜体下料单
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("CabinetNO"), Type.GetType("System.String"));//柜号
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式名称
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质说明
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("FThick", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("CutWide", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("CutHigh", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));
            MianQiYiBiGui_GuiTiDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //附页衣壁柜下料单
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("CabinetNO"), Type.GetType("System.String"));//柜号
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));//板件名称
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("PlateType"), Type.GetType("System.String"));//板件类型
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("Stytle"), Type.GetType("System.String"));//款式名称
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("MaterialNote"), Type.GetType("System.String"));//材质说明
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutThick", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutWide", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("CutHigh", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("SealingSide"), Type.GetType("System.String"));
            MianQiYiBiGui_FuyeDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //衣壁柜料单
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            MianQiYiBiGui_LiaoDanDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //衣壁柜装箱清单GetMaterialBillsId
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add("Qty", typeof(float));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            MianQiYiBiGui_ZhuangXiangQingDanDataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //A6包装
            A6Li_PackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Wide"), typeof(string));
            A6Li_PackingListTable.Columns.Add("High", typeof(string));
            A6Li_PackingListTable.Columns.Add("Depth", typeof(string));
            A6Li_PackingListTable.Columns.Add("Quantity", typeof(float));
            A6Li_PackingListTable.Columns.Add("Thickness", typeof(float));
            A6Li_PackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add("Qty", typeof(float));
            A6Li_PackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            A6Li_PackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            //A6Li_PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
            //速美包装
            SuMeiPackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("PackingNumber"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Wide"), typeof(string));
            SuMeiPackingListTable.Columns.Add("High", typeof(string));
            SuMeiPackingListTable.Columns.Add("Depth", typeof(string));
            SuMeiPackingListTable.Columns.Add("Quantity", typeof(float));
            SuMeiPackingListTable.Columns.Add("Thickness", typeof(float));
            SuMeiPackingListTable.Columns.Add(string.Format("Cell"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("CabinetType"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("NameOfPackagingMaterials"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("MaterialCode"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add("Qty", typeof(float));
            SuMeiPackingListTable.Columns.Add(string.Format("PackingQuantity"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));
            SuMeiPackingListTable.Columns.Add(string.Format("TheDeliveryCateGory"), Type.GetType("System.String"));
            //A6Li_PackingListTable.Columns.Add(string.Format("PackingGroup"), Type.GetType("System.String"));
            //吸塑
            XiSu_DataTable.Columns.Add(string.Format("IssueMaterialBillsId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("PlateName"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("FaceConduct"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add("CutThick", typeof(float));
            XiSu_DataTable.Columns.Add("CutWide", typeof(float));
            XiSu_DataTable.Columns.Add("CutHigh", typeof(float));
            XiSu_DataTable.Columns.Add("Qty", typeof(float));
            //XiSu_DataTable.Columns.Add(string.Format("Side"), Type.GetType("System.String"));
            XiSu_DataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            //吸料
            XiLiao_DataTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            //XiLiao_DataTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            //XiLiao_DataTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("ProductDes"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add("Qty", typeof(float));
            XiLiao_DataTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            XiLiao_DataTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

            ////料单
            //SuMeiPackingListTable.Columns.Add(string.Format("GetMaterialBillsId"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductOrderId"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("MaterialType"), Type.GetType("System.String"));
            ////SuMeiPackingListTable.Columns.Add(string.Format("NumericalOrder"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            ////SuMeiPackingListTable.Columns.Add(string.Format("HardwareFitting"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("ProductDesCription"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add("Qty", typeof(float));
            //SuMeiPackingListTable.Columns.Add(string.Format("Unit"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("PackingConfirmed"), Type.GetType("System.String"));
            //SuMeiPackingListTable.Columns.Add(string.Format("Remark"), Type.GetType("System.String"));

        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void ReadThe_PaintToSQLData(string Path, string ParentId);

        ///// <summary>
        ///// 调用ImportExcelDataToDB方法导入数据库
        ///// </summary>
        //public void ImportNon_toxicSeriesOfMaterialsDataToDB()
        //{
        //    DissaccemblyDataTable.CommitTheClothesClosetToDB(MianQiYiBiGui_FuyeDataTable, GuitiIssueMaterialBillsDataTable, Li_PackingListTable, IssueMaterialBillsDataTable, GetMaterialBillsDataTable, PackingListTable, GuitiGetMaterialBillsDataTable);
        //}
    }


   






    /// <summary>
    /// CheckTicket
    /// </summary>
    public abstract class CheckTicket_ToSQL
    {


        protected DataTable orderDetailDataTable = new DataTable();

        protected string ParentId;
        public CheckTicket_ToSQL()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //附页衣壁柜下料单
            orderDetailDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add("CutLong", typeof(float));
            orderDetailDataTable.Columns.Add("CutWide", typeof(float));
            orderDetailDataTable.Columns.Add("CutThick", typeof(float));
            orderDetailDataTable.Columns.Add("Qty", typeof(float));
            orderDetailDataTable.Columns.Add(string.Format("Side1"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("Side2"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("Side3"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("Side4"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void CheckTicketData(string Path, string ParentId, System.Windows.Forms.DataGridView Gridview);

        ///// <summary>
        ///// 调用ImportExcelDataToDB方法导入数据库
        ///// </summary>
        //public void CheckTicketDataToDB()
        //{
        //    DissaccemblyDataTable.CheckTicketDataToDB(orderDetailDataTable);
        //}
    }


    /// <summary>
    /// CheckTicket
    /// </summary>
    public abstract class CheckTicketDoorSheet_ToSQL
    {


        protected DataTable orderDetailDataTable = new DataTable();

        protected string ParentId;
        public CheckTicketDoorSheet_ToSQL()
        {
            CreateDataTable();
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        void CreateDataTable()
        {
            //附页衣壁柜下料单
            orderDetailDataTable.Columns.Add(string.Format("OrderDetailId"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add(string.Format("ProductName"), Type.GetType("System.String"));
            orderDetailDataTable.Columns.Add("CutLong", typeof(float));
            orderDetailDataTable.Columns.Add("CutWide", typeof(float));
            orderDetailDataTable.Columns.Add("FLong", typeof(float));
            orderDetailDataTable.Columns.Add("FWide", typeof(float));
            orderDetailDataTable.Columns.Add("Side1", typeof(float));
            orderDetailDataTable.Columns.Add("Side2", typeof(float));
            orderDetailDataTable.Columns.Add("Side3", typeof(float));
            orderDetailDataTable.Columns.Add("Side4", typeof(float));
            orderDetailDataTable.Columns.Add("Qty", typeof(float));
            orderDetailDataTable.Columns.Add(string.Format("CenterMaterials"), Type.GetType("System.String"));
        }
        /// <summary>
        /// 创建抽象方法
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="ParentId"></param>
        public abstract void CheckTicketDoorSheetData(string Path, string ParentId, System.Windows.Forms.DataGridView Gridview);

        ///// <summary>
        ///// 调用ImportExcelDataToDB方法导入数据库
        ///// </summary>
        //public void CheckTicketDataToDB()
        //{
        //    DissaccemblyDataTable.CheckTicketDataToDB(orderDetailDataTable);
        //}
    }

}
