using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

namespace LB4_BY_WSDL
{
    [WebService(Namespace = "FAA")]
    [WebServiceBinding(Name = "SimplexSoap", Namespace = "FAA")]
    public class Simplex : WebService
    {
        [WebMethod]
        [SoapDocumentMethod("FAA/Add", RequestNamespace = "FAA", ResponseNamespace = "FAA", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public int Add(int x, int y)
        {
            return x + y;
        }

        /// <remarks/>
        [WebMethod]
        [SoapDocumentMethod("FAA/AddS", RequestNamespace = "FAA", ResponseNamespace = "FAA", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public string AddS(int x, int y)
        {
            int sum = x + y;
            return "{d:" + sum +"}";
        }

        /// <remarks/>
        [WebMethod]
        [SoapDocumentMethod("FAA/Concat", RequestNamespace = "FAA", ResponseNamespace = "FAA", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public string Concat(string str, double numberDouble)
        {
            return str + numberDouble;
        }

        /// <remarks/>
        [WebMethod]
        [SoapDocumentMethod("FAA/Sum", RequestNamespace = "FAA", ResponseNamespace = "FAA", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        public SimpleClass Sum(SimpleClass objOne, SimpleClass objTwo)
        {
            SimpleClass result = new SimpleClass();

            result.str = objOne.str + objTwo.str;
            result.numberInt = objOne.numberInt + objTwo.numberInt;
            result.numberFloat = objOne.numberFloat + objTwo.numberFloat;

            /*string reqBody;

            Context.Request.InputStream.Position = 0;
            StreamReader reader = new StreamReader(Context.Request.InputStream);
            reqBody = reader.ReadToEnd();

            result.str += "\n\n" + reqBody + "\n\n";*/

            return result;
        }
    }

    public partial class SimpleClass
    {

        private string strField;

        private int numberIntField;

        private float numberFloatField;

        /// <remarks/>
        public string str
        {
            get
            {
                return this.strField;
            }
            set
            {
                this.strField = value;
            }
        }

        /// <remarks/>
        public int numberInt
        {
            get
            {
                return this.numberIntField;
            }
            set
            {
                this.numberIntField = value;
            }
        }

        /// <remarks/>
        public float numberFloat
        {
            get
            {
                return this.numberFloatField;
            }
            set
            {
                this.numberFloatField = value;
            }
        }

        public override string ToString()
        {
            return this.str + " --- " + this.numberInt + " --- " + this.numberFloat;
        }
    }
}