using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace LB6_7.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}