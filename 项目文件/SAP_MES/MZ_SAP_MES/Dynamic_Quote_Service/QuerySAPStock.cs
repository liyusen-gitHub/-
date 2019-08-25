using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
namespace Dynamic_Quote_Service
{
    [WebServiceBinding(Namespace = "http://tempuri.org/")]
    public partial class QuerySAPStock:SoapHttpClientProtocol
    {

        protected string ServiceUrl = "http://192.168.124.66/SAP_WebServices/QueryServices.asmx";
        public QuerySAPStock()
        {
            InitializeComponent();
        }

        public QuerySAPStock(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        [SoapDocumentMethod]
        public DataSet GetStockByNumber(string queryString)
        {
            base.Url = ServiceUrl;
            return base.Invoke("GetQueryInfoMation", new object[] { queryString })[0] as DataSet;

        }
    }
}
