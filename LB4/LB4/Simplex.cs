using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace LB4
{
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [WebService(Description = "Simplex web service",
        Namespace = "FAA")]
    public class Simplex : WebService
    {
        [WebMethod(Description = "Sum of two integer", 
            MessageName = "Add")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [WebMethod(Description = "AJAX Sum of two integer",
            MessageName = "AddS")]
        public int AddS(int x, int y)
        {
            return x + y;
        }

        [WebMethod(Description = "Concatenation of string and double", 
            MessageName = "Concat")]
        public string Concat(string str, double numberDouble)
        {
            return str + numberDouble;
        }

        [WebMethod(Description = "Sum of two classes",
            MessageName = "Sum")]
        public SimpleClass Sum(SimpleClass objOne, SimpleClass objTwo)
        {
            SimpleClass result = new SimpleClass();

            result.str = objOne.str + objTwo.str;
            result.numberInt = objOne.numberInt + objTwo.numberInt;
            result.numberFloat = objOne.numberFloat + objTwo.numberFloat;

            string reqBody;

            Context.Request.InputStream.Position = 0;
            StreamReader str = new StreamReader(Context.Request.InputStream);
            reqBody = str.ReadToEnd();

            result.str += "\n\n" + reqBody + "\n\n";

            return result;
        }
    }
}