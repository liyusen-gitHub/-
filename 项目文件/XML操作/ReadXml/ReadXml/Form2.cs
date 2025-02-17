﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ReadXml
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string XML = @"<?xml version=""1.0"" encoding=""UTF-8""?>
 
<ROOT> 
  <SOURCESYSTEMID>MES</SOURCESYSTEMID>  
  <SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME>  
  <USERID>路颖MM009</USERID>  
  <USERNAME>路颖MM009</USERNAME>  
  <SUBMITDATE>20170626</SUBMITDATE>  
  <SUBMITTIME>172413</SUBMITTIME>  
  <ZFUNCTION>ZFUN_WMS00006</ZFUNCTION>  
  <CURRENT_PAGE>001</CURRENT_PAGE>  
  <TOTAL_RECORD>001</TOTAL_RECORD>  
  <HEAD> 
    <VBELN>0080156967</VBELN>  
    <AUART>ZS02</AUART>  
    <WERKS>6006</WERKS>  
    <BUKRS>6006</BUKRS>  
    <BWART>601</BWART>  
    <ZCHANGE_IND>I</ZCHANGE_IND>  
    <SOBKZ/>  
    <ZHSSUM>2</ZHSSUM>  
    <KUNNR>6200000109</KUNNR>  
    <NAME1>河南速美超级家装饰工程有限公司</NAME1>  
    <STRAS>商丘市梁园区胜利东路与睢阳大道交叉口鼎雄新里程</STRAS>  
    <ZLXNM>李战胜</ZLXNM>  
    <TELF1>13903700426</TELF1>  
    <PSTLZ/>  
    <ZYSFS>物流</ZYSFS>  
    <ZYJCHSJ>2017-03-28</ZYJCHSJ>  
    <ZXSVBELN>0000057925</ZXSVBELN>  
    <ZXSHDTXT>河南省郑州市花园路农科路西200米万达中心1605-1608室李战胜13071071517</ZXSHDTXT>  
    <ZPSPID/>  
    <EBELN>6421703280</EBELN>  
    <ZSJS>张绿花023555</ZSJS>  
    <ZYFCD/>  
    <ZSTO/>  
    <ZPRCTR>速美电商公共</ZPRCTR>  
    <IHREZ>642170328050</IHREZ>  
    <ZMEMO1/>  
    <ZMEMO2>河南速美超级家装饰工程有限公司</ZMEMO2>  
    <ZFSHENG>410000</ZFSHENG>  
    <ZFSHI>410100</ZFSHI>  
    <ZFQU>410122</ZFQU>  
    <ZFHDZ>河南省郑州市经济技术开发区前程大道延福路陇南工业园亨泽物流西库</ZFHDZ>  
    <ZSSHENG>410000</ZSSHENG>  
    <ZSSHI>411400</ZSSHI>  
    <ZSQU>411403</ZSQU>  
    <ZXTCHBS>MES</ZXTCHBS>  
    <ZMMI001/>  
    <ZMMI002/>  
    <ZMMI003/>  
    <ITEMS> 
      <ITEM>
        <POSNR>000010</POSNR>
        <MATNR>000000002000017814</MATNR>
        <MAKTX>美标坐厕305+CP2033.004.04+CCAS2033-2200400C0</MAKTX>
        <LGORT>ZZJ1</LGORT>
        <CHARG/>
        <LFIMG>2.0</LFIMG>
        <MEINS>套</MEINS>
        <ENWRT>0.0</ENWRT>
        <BKLAS>3005</BKLAS>
        <ZSYWZ>补货328</ZSYWZ>
        <MATKL>010201</MATKL>
        <ZZHONGL>1.000</ZZHONGL>
        <ZVBELN>0000057925/000010</ZVBELN>
        <ZMEMO3/>
        <ZMEMO4/>
      </ITEM>
      <ITEM>
        <POSNR>000020</POSNR>
        <MATNR>000000002000017814</MATNR>
        <MAKTX>美标坐厕305+CP2033.004.04+CCAS2033-2200400C0</MAKTX>
        <LGORT>ZZJ1</LGORT>
        <CHARG/>
        <LFIMG>2.0</LFIMG>
        <MEINS>套</MEINS>
        <ENWRT>0.0</ENWRT>
        <BKLAS>3005</BKLAS>
        <ZSYWZ>补货328</ZSYWZ>
        <MATKL>010201</MATKL>
        <ZZHONGL>1.000</ZZHONGL>
        <ZVBELN>0000057925/000010</ZVBELN>
        <ZMEMO3/>
        <ZMEMO4/>
      </ITEM> 
    </ITEMS> 
  </HEAD> 
</ROOT>
";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML.ToLower());


            XmlNode SingleLineNode = doc.SelectSingleNode(@"root/head/items/item");

            DataTable Dt = new DataTable("root");
            DataColumn Dc = null;
            for (int i = 0; i < SingleLineNode.ChildNodes.Count; i++)
            {
                XmlNode SingleChildNode = SingleLineNode.ChildNodes[i];
                Dc = Dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
            }

            XmlNodeList NodeList = doc.SelectNodes(@"root/head/items/item");
            for (int i = 0; i < NodeList.Count; i++)
            {
                XmlNode LineNode = NodeList[i];
                DataRow newRow;
                newRow = Dt.NewRow();
                for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                {
                    XmlNode ChildNode = LineNode.ChildNodes[j];
                    newRow[ChildNode.Name] = ChildNode.InnerText;
                }
                Dt.Rows.Add(newRow);

            }
   
        }
    }
}
