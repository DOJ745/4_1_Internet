using LB3.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace LB3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
