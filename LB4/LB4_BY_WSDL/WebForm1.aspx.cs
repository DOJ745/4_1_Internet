using WINFORM_WSDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LB4_BY_WSDL
{
    public partial class WebForm1 : Page
    {
        //private Simplex simplex = new Simplex();
        public static WINFORM_WSDL.Simplex getSimplex()
        {
            WINFORM_WSDL.Simplex service = new WINFORM_WSDL.Simplex();
            service.Url = "http://localhost:10000/LB4_1/Simplex.asmx";
            service.Timeout = 100 * 1000;
            return service;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addMethod(object sender, EventArgs e)
        {
            var service = getSimplex();
            Label2.Text = service.Add(int.Parse(InputX.Text), int.Parse(InputY.Text)).ToString();
        }

        protected void concatMethod(object sender, EventArgs e)
        {
            var service = getSimplex();
            Label3.Text = service.Concat(InputStr.Text, double.Parse(InputDouble.Text.Replace(".", ",")));
        }

        protected void sumMethod(object sender, EventArgs e)
        {
            var service = getSimplex();
            WINFORM_WSDL.SimpleClass objOne = new WINFORM_WSDL.SimpleClass();
            WINFORM_WSDL.SimpleClass objTwo = new WINFORM_WSDL.SimpleClass();

            objOne.str = InputObjOneStr.Text;
            objOne.numberInt = int.Parse(InputObjOneInt.Text);
            objOne.numberFloat = float.Parse(InputObjOneFloat.Text.Replace(".", ","));

            objTwo.str = InputObjTwoStr.Text;
            objTwo.numberInt = int.Parse(InputObjTwoInt.Text);
            objTwo.numberFloat = float.Parse(InputObjTwoFloat.Text.Replace(".", ","));

            WINFORM_WSDL.SimpleClass sum = service.Sum(objOne, objTwo);

            Label4.Text = sum.str + " --- " + sum.numberInt + " --- " + sum.numberFloat;
        }
    }
}