﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace VL.ItsMe1110.ObjectUserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ObjectUserService.IObjectUserService")]
    public interface IObjectUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServiceNode/CheckAlive", ReplyAction="http://tempuri.org/IWCFServiceNode/CheckAliveResponse")]
        bool CheckAlive();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServiceNode/CheckAlive", ReplyAction="http://tempuri.org/IWCFServiceNode/CheckAliveResponse")]
        System.Threading.Tasks.Task<bool> CheckAliveAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServiceNode/CheckNodeReferences", ReplyAction="http://tempuri.org/IWCFServiceNode/CheckNodeReferencesResponse")]
        VL.Common.Core.Protocol.DependencyResult CheckNodeReferences();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWCFServiceNode/CheckNodeReferences", ReplyAction="http://tempuri.org/IWCFServiceNode/CheckNodeReferencesResponse")]
        System.Threading.Tasks.Task<VL.Common.Core.Protocol.DependencyResult> CheckNodeReferencesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObjectUserService/GetAllUsers", ReplyAction="http://tempuri.org/IObjectUserService/GetAllUsersResponse")]
        VL.Common.Core.Protocol.Report<System.Collections.Generic.List<VL.Common.Core.Object.VL.User.TUser>> GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObjectUserService/GetAllUsers", ReplyAction="http://tempuri.org/IObjectUserService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<VL.Common.Core.Protocol.Report<System.Collections.Generic.List<VL.Common.Core.Object.VL.User.TUser>>> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObjectUserService/CheckUserInRole", ReplyAction="http://tempuri.org/IObjectUserService/CheckUserInRoleResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(VL.Common.Core.Protocol.Report<System.Collections.Generic.List<VL.Common.Core.Object.VL.User.TUser>>))]
        VL.Common.Core.Protocol.Report CheckUserInRole(VL.Common.Core.Object.VL.User.TUser user, System.Collections.Generic.List<VL.Common.Core.Object.VL.User.ERole> roles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IObjectUserService/CheckUserInRole", ReplyAction="http://tempuri.org/IObjectUserService/CheckUserInRoleResponse")]
        System.Threading.Tasks.Task<VL.Common.Core.Protocol.Report> CheckUserInRoleAsync(VL.Common.Core.Object.VL.User.TUser user, System.Collections.Generic.List<VL.Common.Core.Object.VL.User.ERole> roles);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IObjectUserServiceChannel : VL.ItsMe1110.ObjectUserService.IObjectUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ObjectUserServiceClient : System.ServiceModel.ClientBase<VL.ItsMe1110.ObjectUserService.IObjectUserService>, VL.ItsMe1110.ObjectUserService.IObjectUserService {
        
        public ObjectUserServiceClient() {
        }
        
        public ObjectUserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ObjectUserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ObjectUserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ObjectUserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool CheckAlive() {
            return base.Channel.CheckAlive();
        }
        
        public System.Threading.Tasks.Task<bool> CheckAliveAsync() {
            return base.Channel.CheckAliveAsync();
        }
        
        public VL.Common.Core.Protocol.DependencyResult CheckNodeReferences() {
            return base.Channel.CheckNodeReferences();
        }
        
        public System.Threading.Tasks.Task<VL.Common.Core.Protocol.DependencyResult> CheckNodeReferencesAsync() {
            return base.Channel.CheckNodeReferencesAsync();
        }
        
        public VL.Common.Core.Protocol.Report<System.Collections.Generic.List<VL.Common.Core.Object.VL.User.TUser>> GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<VL.Common.Core.Protocol.Report<System.Collections.Generic.List<VL.Common.Core.Object.VL.User.TUser>>> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public VL.Common.Core.Protocol.Report CheckUserInRole(VL.Common.Core.Object.VL.User.TUser user, System.Collections.Generic.List<VL.Common.Core.Object.VL.User.ERole> roles) {
            return base.Channel.CheckUserInRole(user, roles);
        }
        
        public System.Threading.Tasks.Task<VL.Common.Core.Protocol.Report> CheckUserInRoleAsync(VL.Common.Core.Object.VL.User.TUser user, System.Collections.Generic.List<VL.Common.Core.Object.VL.User.ERole> roles) {
            return base.Channel.CheckUserInRoleAsync(user, roles);
        }
    }
}
