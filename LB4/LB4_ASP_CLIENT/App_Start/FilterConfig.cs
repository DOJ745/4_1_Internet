using System.Web;
using System.Web.Mvc;

namespace LB4_ASP_CLIENT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
