using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web.Mvc;

namespace LB6_7.Controllers
{
    public class HomeController : Controller
    {
        private WSSDAEntities context;
        private MainWcfDataService mainService = new MainWcfDataService();
        //private Uri svcUri = new Uri("http://localhost:56386/MainWcfDataService.svc");
        public ActionResult Index()
        {
            context = new WSSDAEntities();

            var students = mainService.getStudents(context);
            ViewBag.Students = students;

            return View();
        }

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