using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WINFORM_SUM
{
    public class SimpleClass
    {
        public string str { get; set; }
        public int numberInt { get; set; }
        public float numberFloat { get; set; }

        public string formSOAP(SimpleClass otherObject)
        {
            string SOAP = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
"<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n" +
"  <soap:Body>\n" +
"    <Sum xmlns=\"FAA\">\n" +
"      <objOne>\n" +
"        <str>" + this.str +"</str>\n" +
"        <numberInt>" + this.numberInt + "</numberInt>\n" +
"        <numberFloat>" + this.numberFloat.ToString().Replace(",", ".") + "</numberFloat>\n" +
"      </objOne>\n" +
"      <objTwo>\n" +
"        <str>" + otherObject.str + "</str>\n" +
"        <numberInt>" + otherObject.numberInt + "</numberInt>\n" +
"        <numberFloat>" + otherObject.numberFloat.ToString().Replace(",", ".") + "</numberFloat>\n" +
"      </objTwo>\n" +
"    </Sum>\n" +
"  </soap:Body>\n" +
"</soap:Envelope>";
            return SOAP;
        }
    }
}
