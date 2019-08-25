using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dynamic_Quote_Service;

namespace MZ_MES_Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string ServiceUrl = "http://192.168.124.66/SAP_WebServices/QueryServices.asmx";
            //MES_Services.QueryServicesSoapClient service = new MES_Services.QueryServicesSoapClient();
            //DataSet dt= service.GetQueryInfoMation(TextBox1.Text.Trim());
            //DataSet dt = new WebServiceProxy().GetStockByNumber("JDQ-SP000274");
            DataSet dt = WebServiceProxy.InvokeWebservice(ServiceUrl, "", "GetQueryInfoMation", "JB-B-M-XIANDAI0,JB-B-M-XIANDAI0") as DataSet;
            GridView1.DataSource = dt.Tables["DATALINE"];
            GridView1.DataBind();
        }
    }
}