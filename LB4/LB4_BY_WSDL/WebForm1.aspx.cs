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
        private Simplex simplex = new Simplex();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addMethod(object sender, EventArgs e)
        {
            Label2.Text = simplex.Add(int.Parse(InputX.Text), int.Parse(InputY.Text)).ToString();
        }

        protected void concatMethod(object sender, EventArgs e)
        {
            Label3.Text = simplex.Concat(InputStr.Text, double.Parse(InputDouble.Text.Replace(".", ",")));
        }

        protected void sumMethod(object sender, EventArgs e)
        {
            SimpleClass objOne = new SimpleClass();
            SimpleClass objTwo = new SimpleClass();

            objOne.str = InputObjOneStr.Text;
            objOne.numberInt = int.Parse(InputObjOneInt.Text);
            objOne.numberFloat = float.Parse(InputObjOneFloat.Text.Replace(".", ","));

            objTwo.str = InputObjTwoStr.Text;
            objTwo.numberInt = int.Parse(InputObjTwoInt.Text);
            objTwo.numberFloat = float.Parse(InputObjTwoFloat.Text.Replace(".", ","));

            Label4.Text = simplex.Sum(objOne, objTwo).ToString();
        }
    }
}