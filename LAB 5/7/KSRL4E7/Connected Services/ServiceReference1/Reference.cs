﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KSRL4E7.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IZadanie5")]
    public interface IZadanie5 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie5/ScalNapisy", ReplyAction="http://tempuri.org/IZadanie5/ScalNapisyResponse")]
        string ScalNapisy(string a, string b);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IZadanie5/ScalNapisy", ReplyAction="http://tempuri.org/IZadanie5/ScalNapisyResponse")]
        System.Threading.Tasks.Task<string> ScalNapisyAsync(string a, string b);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie5Channel : KSRL4E7.ServiceReference1.IZadanie5, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Zadanie5Client : System.ServiceModel.ClientBase<KSRL4E7.ServiceReference1.IZadanie5>, KSRL4E7.ServiceReference1.IZadanie5 {
        
        public Zadanie5Client() {
        }
        
        public Zadanie5Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Zadanie5Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie5Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie5Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ScalNapisy(string a, string b) {
            return base.Channel.ScalNapisy(a, b);
        }
        
        public System.Threading.Tasks.Task<string> ScalNapisyAsync(string a, string b) {
            return base.Channel.ScalNapisyAsync(a, b);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IZadanie6", CallbackContract=typeof(KSRL4E7.ServiceReference1.IZadanie6Callback))]
    public interface IZadanie6 {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IZadanie6/Dodaj")]
        void Dodaj(int a, int b);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IZadanie6/Dodaj")]
        System.Threading.Tasks.Task DodajAsync(int a, int b);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie6Callback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IZadanie6/Wynik")]
        void Wynik(int wyn);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IZadanie6Channel : KSRL4E7.ServiceReference1.IZadanie6, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Zadanie6Client : System.ServiceModel.DuplexClientBase<KSRL4E7.ServiceReference1.IZadanie6>, KSRL4E7.ServiceReference1.IZadanie6 {
        
        public Zadanie6Client(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public Zadanie6Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public Zadanie6Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie6Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Zadanie6Client(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Dodaj(int a, int b) {
            base.Channel.Dodaj(a, b);
        }
        
        public System.Threading.Tasks.Task DodajAsync(int a, int b) {
            return base.Channel.DodajAsync(a, b);
        }
    }
}