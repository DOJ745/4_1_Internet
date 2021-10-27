using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LB4
{
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [WebService(Description = "Simplex web service")]
    public class Simplex : WebService
    {
        [WebMethod(Description = "Sum of two integer")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [WebMethod(Description = "Concatenation of string and double")]
        public string Concat(string str, double numberDouble)
        {
            return str + numberDouble;
        }

        [WebMethod(Description = "Sum of two classes")]
        public SimpleClass Sum(SimpleClass objOne, SimpleClass objTwo)
        {
            SimpleClass result = new SimpleClass();

            result.str = objOne.str + objTwo.str;
            result.numberInt = objOne.numberInt + objTwo.numberInt;
            result.numberFloat = objOne.numberFloat + objTwo.numberFloat;

            return result;
        }

        [WebMethod]
        public TestReport GetReport(int reportID)
        {
            TestReport testReport = new TestReport()
            {
                ReportID = reportID,
                Date = DateTime.Now,
                Info = "Some info"
            };

            return testReport;
        }
    }

    public class TestReport
    {
        public int ReportID { get; set; }
        public DateTime Date { get; set; }
        public string Info { get; set; }
    }
}