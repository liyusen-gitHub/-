﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAP_Docking_MES_App.OMS_ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OMS_ServiceReference.IMESService")]
    public interface IMESService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMESService/SynchronizeOrderTimes", ReplyAction="http://tempuri.org/IMESService/SynchronizeOrderTimesResponse")]
        string SynchronizeOrderTimes(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMESService/SynchronizeOrderTimes", ReplyAction="http://tempuri.org/IMESService/SynchronizeOrderTimesResponse")]
        System.Threading.Tasks.Task<string> SynchronizeOrderTimesAsync(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMESService/Get2020SalesPrice", ReplyAction="http://tempuri.org/IMESService/Get2020SalesPriceResponse")]
        string Get2020SalesPrice(string data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMESService/Get2020SalesPrice", ReplyAction="http://tempuri.org/IMESService/Get2020SalesPriceResponse")]
        System.Threading.Tasks.Task<string> Get2020SalesPriceAsync(string data);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMESServiceChannel : SAP_Docking_MES_App.OMS_ServiceReference.IMESService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MESServiceClient : System.ServiceModel.ClientBase<SAP_Docking_MES_App.OMS_ServiceReference.IMESService>, SAP_Docking_MES_App.OMS_ServiceReference.IMESService {
        
        public MESServiceClient() {
        }
        
        public MESServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MESServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MESServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MESServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string SynchronizeOrderTimes(string data) {
            return base.Channel.SynchronizeOrderTimes(data);
        }
        
        public System.Threading.Tasks.Task<string> SynchronizeOrderTimesAsync(string data) {
            return base.Channel.SynchronizeOrderTimesAsync(data);
        }
        
        public string Get2020SalesPrice(string data) {
            return base.Channel.Get2020SalesPrice(data);
        }
        
        public System.Threading.Tasks.Task<string> Get2020SalesPriceAsync(string data) {
            return base.Channel.Get2020SalesPriceAsync(data);
        }
    }
}
