using LB3.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace LB3.Controllers
{
    public class HomeController : Controller
    {
        StudentsContext DB = new StudentsContext();
        public ActionResult Index()
        {
            ViewBag.StudentsList = DB.Students.OrderBy(entry => entry.ID);
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
