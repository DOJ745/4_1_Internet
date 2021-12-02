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
        public static WSSDAEntities CONTEXT = new WSSDAEntities();
        private MainWcfDataService mainService = new MainWcfDataService();
        public ActionResult Index()
        {

            var students = mainService.getStudents();
            var notes = mainService.getNotes();

            ViewBag.Students = students;
            ViewBag.Notes = notes;

            return View();
        }

        public ActionResult AddStud()
        {
            return View();
        }

        public RedirectResult AddStudSave(string name)
        {
            mainService.addStudent(name);

            ViewBag.Students = CONTEXT.Student;
            return Redirect("/Home/Index");
        }
        
        public ActionResult UpdateStud(string id)
        {
            ViewBag.Id = id;
            ViewBag.Students = mainService.getStudents();
            return View();
        }

        public RedirectResult UpdateStudSave(string id, string name)
        {
            mainService.updateStudent(id, name);
            ViewBag.Students = mainService.getStudents();
            return Redirect("/Home/Index");
        }

        public ActionResult DeleteStud(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public RedirectResult DeleteStudSave(string id)
        {
            mainService.deleteStudent(id);
            ViewBag.Students = mainService.getStudents();
            return Redirect("/Home/Index");
        }


        
        public ActionResult AddNote()
        {
            return View();
        }
        public RedirectResult AddNoteSave(int studId, int mark, string subject)
        {
            mainService.addNote(studId, mark, subject);

            ViewBag.Notes = CONTEXT.Note;
            return Redirect("/Home/Index");
        }
        public ActionResult UpdateNote(string id)
        {
            ViewBag.Id = id;
            ViewBag.Notes = mainService.getNotes();
            return View();
        }

        public RedirectResult UpdateNoteSave(string id, int mark, int studentId, string subject)
        {
            mainService.updateNote(id, studentId, mark, subject);
            ViewBag.Notes = mainService.getNotes();
            return Redirect("/Home/Index");
        }

        public ActionResult DeleteNote(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public RedirectResult DeleteNoteSave(string id)
        {
            mainService.deleteNote(id);
            ViewBag.Notes = mainService.getNotes();
            return Redirect("/Home/Index");
        }

        // WCF (LB7)
        public ActionResult Client()
        {
            string result = "";

            XmlReader xmlReader = XmlReader.Create("http://localhost:8733/Design_Time_Addresses/LB7_WCF/Feed1");
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
            foreach (SyndicationItem item in feed.Items)
            {
                result += item.Title.Text + "\n";
            }

            ViewBag.Result = result;
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