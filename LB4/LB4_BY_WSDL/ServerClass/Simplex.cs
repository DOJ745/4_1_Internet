﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by wsdl, Version=4.8.3928.0.
// 
namespace LB4_BY_WSDL.ServerClass {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.Web.Services.WebServiceAttribute(Namespace="http://FAA/")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SimplexSoap", Namespace="http://FAA/")]
    public abstract partial class Simplex : System.Web.Services.WebService {
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://FAA/Add", RequestNamespace="http://FAA/", ResponseNamespace="http://FAA/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public abstract int Add(int x, int y);
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://FAA/AddS", RequestNamespace="http://FAA/", ResponseNamespace="http://FAA/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public abstract string AddS(int x, int y);
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://FAA/Concat", RequestNamespace="http://FAA/", ResponseNamespace="http://FAA/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public abstract string Concat(string str, double numberDouble);
        
        /// <remarks/>
        [System.Web.Services.WebMethodAttribute()]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://FAA/Sum", RequestNamespace="http://FAA/", ResponseNamespace="http://FAA/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public abstract SimpleClass Sum(SimpleClass objOne, SimpleClass objTwo);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://FAA/")]
    public partial class SimpleClass {
        
        private string strField;
        
        private int numberIntField;
        
        private float numberFloatField;
        
        /// <remarks/>
        public string str {
            get {
                return this.strField;
            }
            set {
                this.strField = value;
            }
        }
        
        /// <remarks/>
        public int numberInt {
            get {
                return this.numberIntField;
            }
            set {
                this.numberIntField = value;
            }
        }
        
        /// <remarks/>
        public float numberFloat {
            get {
                return this.numberFloatField;
            }
            set {
                this.numberFloatField = value;
            }
        }
    }
}
