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

        protected void testClick(object sender, EventArgs e)
        {
            Label1.Text = "Current time: " + DateTime.Now.ToLongTimeString();
        }

        protected void addMethod(object sender, EventArgs e)
        {
            Label2.Text = simplex.Add(int.Parse(InputX.Text), int.Parse(InputY.Text)).ToString();
        }

        protected void concatMethod(object sender, EventArgs e)
        {
            Label3.Text = simplex.Concat(InputStr.Text, double.Parse(InputDouble.Text));
        }

        protected void sumMethod(object sender, EventArgs e)
        {
            SimpleClass objOne = new SimpleClass();
            SimpleClass objTwo = new SimpleClass();

            objOne.str = "";
            objOne.numberInt = 0;
            objOne.numberFloat = 0.0f;

            objTwo.str = "";
            objTwo.numberInt = 0;
            objTwo.numberFloat = 0.0f;
        }
    }
}