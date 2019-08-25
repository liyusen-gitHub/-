using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

namespace ImportDisassemblyData
{
    public class DataToSQL
    {
        /// <summary>
        /// 将传进来的DS数据全部保存到数据库中
        /// </summary>
        /// <param name="DS">传入的DS集合</param>
        /// <returns></returns>
        public int DataToSQLobj(DataSet DS)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;Persist Security Info=True;User ID=sa;Password=admin@2017"))
            {
                conn.Open();

                try
                {
                    for (int i = 0; i < DS.Tables.Count; i++)
                    {
                        SqlBulkCopy TableCopy = new SqlBulkCopy(conn);
                        Regex r = new Regex("^[a-zA-Z_]+$");
                        if (r.IsMatch(DS.Tables[i].TableName))
                        {
                            TableCopy.DestinationTableName = DS.Tables[i].TableName;
                            TableCopy.BatchSize = DS.Tables[i].Rows.Count;
                            for (int j = 0; j < DS.Tables[i].Columns.Count; j++)
                            {
                                TableCopy.ColumnMappings.Add(DS.Tables[i].Columns[j].ColumnName, DS.Tables[i].Columns[j].ColumnName);
                            }
                            TableCopy.WriteToServer(DS.Tables[i]);
                        }
                        else
                        {
                            if (DS.Tables[i].TableName.Contains("6") || DS.Tables[i].TableName.Contains("_"))
                            {
                                string TableName = DS.Tables[i].TableName;
                                string TabNum = System.Text.RegularExpressions.Regex.Replace(TableName, @"[^0-9]+", "");
                                if (TabNum.Length > 1)
                                {
                                    TabNum = TabNum.Substring(1);
                                    int Index = TableName.LastIndexOf(TabNum);
                                    TableName = TableName.Substring(0, Index);
                                }
                                TableCopy.DestinationTableName = TableName;
                                TableCopy.BatchSize = DS.Tables[i].Rows.Count;
                                for (int j = 0; j < DS.Tables[i].Columns.Count; j++)
                                {
                                    TableCopy.ColumnMappings.Add(DS.Tables[i].Columns[j].ColumnName, DS.Tables[i].Columns[j].ColumnName);
                                }
                                TableCopy.WriteToServer(DS.Tables[i]);
                            }
                            else
                            {
                                string TableName = DS.Tables[i].TableName.Substring(0, DS.Tables[i].TableName.Length - 1);
                                TableCopy.DestinationTableName = TableName;
                                TableCopy.BatchSize = DS.Tables[i].Rows.Count;
                                for (int j = 0; j < DS.Tables[i].Columns.Count; j++)
                                {
                                    TableCopy.ColumnMappings.Add(DS.Tables[i].Columns[j].ColumnName, DS.Tables[i].Columns[j].ColumnName);
                                }
                                TableCopy.WriteToServer(DS.Tables[i]);
                            }
                        }
                        DS.Tables[i].Clear();
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        /// <summary>
        /// 修改订单推送状态
        /// </summary>
        /// <param name="Id">订单Id</param>
        /// <returns>成功返回1，失败返回0</returns>
        public int UpdProductOrderCurrentStatus(string Id)
        {
            SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;PassWord=admin@2017");
            SqlCommand comm = new SqlCommand(string.Format("Update ProductOrder set CurrentStatus='未推送' where ProductOrderId='{0}'", Id), con);
            con.Open();
            if (comm.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 上传Excel文件到服务器
        /// </summary>
        /// <Path>Excel路径</Path>
        /// <returns>1，成功  0，失败</returns>
        public int UpLoadExcel(string Path)
        {
            string FileName = Path.Substring(Path.LastIndexOf("\\") + 1);
            FileInfo fileInfo = new FileInfo(Path);

            //绝对路径
            string image = Path;
            //  是指XXX.jpg
            string picpath = FileName;
            //年份
            string YearPath = DateTime.Now.Year.ToString() + "年";
            //月份
            string MonthPath = DateTime.Now.Month.ToString().PadLeft(2, '0') + "月";
            //日期
            string DatePath = DateTime.Now.ToString("yyyy-MM-dd");
            string ServerPath = "////192.168.124.66//D$//FBExcel//" + YearPath + "//" + MonthPath + "//" + DatePath + "//";
            DirectoryInfo FileName1 = new DirectoryInfo(@ServerPath);
            if (!FileName1.Exists)
            {
                FileName1.Create();
            }
            File.Copy(image, @ServerPath + picpath, true);
            return 1;
        }


        /// <summary>
        /// 获取芯材
        /// </summary>
        /// <param name="Id">ProductOrderId</param>
        /// <returns></returns>
        public int GetXinCai(string Id)
        {
            SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;PassWord=admin@2017");
            SqlCommand comm = new SqlCommand(string.Format(" update IssueMaterialBills set IssueMaterialBills.CenterMaterials=MaterialTexture.TextTureDesc  from MaterialTexture inner join IssueMaterialBills on IssueMaterialBills.MaterialNote=MaterialTexture.Texture and IssueMaterialBills.CutThick=MaterialTexture.Thickness where IssueMaterialBills.ProductOrderId='{0}'", Id), con);
            con.Open();
            if (comm.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 特殊处理
        /// </summary>
        /// <param name="Id">ProductOrderId</param>
        /// <returns></returns>
        public int Special_Handling(string MaterialParentType, string Id)
        {
            SqlConnection con = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;User ID=sa;PassWord=admin@2017");
            SqlCommand comm = new SqlCommand(string.Format("update GetMaterialBills set MaterialParentType=(case when MaterialType in('组装五金','烟机','拉篮','电器')then '组装五金'	when MaterialType='灯具' then '安装五金' 	when ProductDes like '%实木线条%'or ProductDes like '%凯撒实木雕花51*51*7%'or ProductDes like '%镌琢白推拉门小百叶YJTLM%' then '半成品1' 	when ProductDes like '%灯箱底板%' then '组装五金'	when MaterialType in('PU一分光','PU三分光','PU清底','PU白底','PU高光','亚光')then '在线'	when MaterialType in('UV底漆''UV清底','UV白底','UV腻子')then '机台'	when MaterialType in('饰面','铝材','胶类','封边','板材') then CateGory	when MaterialType in('拉手') then '安装五金'	when MaterialType='衣壁柜半成品' then '半成品'	when MaterialType in('家具清油半成品','家具混油半成品') and '{0}'='帕拉迪奥' then '帕拉迪奥'	when MaterialType in('外购半成品','厨浴柜半成品')then '花头花线'	when MaterialType like '%玻璃%'and Unit='块' then '外协玻璃' 	when MaterialType like '%玻璃%'and Unit='平米' then MaterialType	when ProductDes like '%推拉门定位器%'or ProductDes like '%C型轮子32.25中心%'or ProductDes like '%推拉门自攻钉350个/盒%' then '轮子'	when ProductDes like '%饼干片%' then ProductDes else MaterialType end)where ProductOrderId='{1}'",MaterialParentType, Id), con);
            con.Open();
            if (comm.ExecuteNonQuery() > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }







        #region
        /// <summary>
        /// 测试2
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //public int DataToSQLobj2(DataTable dt)
        //{
        //    using (SqlConnection conn = new SqlConnection("Data Source=192.168.124.72;Initial Catalog=OrBitXI;Persist Security Info=True;User ID=sa;Password=admin@2017"))
        //    {
        //        conn.Open();
        //        SqlBulkCopy TableCopy = new SqlBulkCopy(conn);
        //        try
        //        {
        //            TableCopy.DestinationTableName = dt.TableName;
        //            TableCopy.BatchSize = dt.Rows.Count;
        //            TableCopy.ColumnMappings.Add("IssueMaterialBillsId", "IssueMaterialBillsId");
        //            TableCopy.ColumnMappings.Add("OrderDetailId", "OrderDetailId");
        //            TableCopy.ColumnMappings.Add("ProductOrderId", "ProductOrderId");
        //            TableCopy.ColumnMappings.Add("PlateName", "PlateName");
        //            TableCopy.ColumnMappings.Add("PlateType", "PlateType");
        //            TableCopy.ColumnMappings.Add("CutThick", "CutThick");
        //            TableCopy.ColumnMappings.Add("CutWide", "CutWide");
        //            TableCopy.ColumnMappings.Add("CutHigh", "CutHigh");
        //            TableCopy.ColumnMappings.Add("CutQty", "CutQty");
        //            TableCopy.ColumnMappings.Add("MaterialNote", "MaterialNote");
        //            TableCopy.ColumnMappings.Add("Remark", "Remark");
        //            TableCopy.WriteToServer(dt);
        //            return 1;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //} 
        #endregion



    }
}
