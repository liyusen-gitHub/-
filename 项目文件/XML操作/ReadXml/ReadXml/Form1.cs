using System;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string XML = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<root>
<SOURCESYSTEMID>ERP</SOURCESYSTEMID>
<SOURCESYSTEMNAME>MES</SOURCESYSTEMNAME>
<USERID>DYRS_YUNWEI</USERID>
<USERNAME>DYRS_YUNWEI</USERNAME>
<SUBMITDATE>2017-12-15</SUBMITDATE>
<SUBMITTIME>23:30:46</SUBMITTIME>
<ZFUNCTION>ZFUN_WMS00003</ZFUNCTION>
<CURRENT_PAGE>000</CURRENT_PAGE>
<TOTAL_RECORD>000</TOTAL_RECORD>

<line>
<ZBWART>Z37</ZBWART>
<ZSOBKZ/>
<ZYDLXMS>厂家承担责任领用</ZYDLXMS>
<ZGRGIMARK>1</ZGRGIMARK>
<ZJKNAME>ZFUN_WMS00011</ZJKNAME>
<ZCHANGE_IND>I</ZCHANGE_IND>
<VERSION>1</VERSION>
<ZXTCHBS>MES</ZXTCHBS>
<ZMEMO1/>
<ZMEMO2/>
<ZMEMO3/>
</line>

<line>
<ZBWART>Z38</ZBWART>
<ZSOBKZ/>
<ZYDLXMS>厂家承担责任领用-冲销</ZYDLXMS>
<ZGRGIMARK>2</ZGRGIMARK>
<ZJKNAME>ZFUN_WMS00011</ZJKNAME>
<ZCHANGE_IND>I</ZCHANGE_IND>
<VERSION>2</VERSION>
<ZXTCHBS>MES</ZXTCHBS>
<ZMEMO1/>
<ZMEMO2/>
<ZMEMO3/>
</line>
</root>";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XML);
            XmlNode SingleLineNode= doc.SelectSingleNode(@"root/line");

            DataTable Dt = new DataTable("root");
            DataColumn Dc = null;
            for (int i = 0; i < SingleLineNode.ChildNodes.Count; i++)
            {
                XmlNode SingleChildNode = SingleLineNode.ChildNodes[i];
                Dc = Dt.Columns.Add(SingleChildNode.Name, Type.GetType("System.String"));
            }

            XmlNodeList NodeList = doc.SelectNodes(@"root/line");
            for (int i = 0; i < NodeList.Count; i++)
            {
                 XmlNode LineNode=   NodeList[i];
                 DataRow newRow;
                 newRow = Dt.NewRow();
                 for (int j = 0; j < LineNode.ChildNodes.Count; j++)
                 {
                     XmlNode ChildNode = LineNode.ChildNodes[j];
                     newRow[ChildNode.Name] = ChildNode.InnerText;
                  }
                 Dt.Rows.Add(newRow);

            }
            string test = "2";
             
           
        }
    }
}
