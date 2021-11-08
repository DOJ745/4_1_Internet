using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Web.Mvc;

namespace LB6_7.Controllers
{
    public class HomeController : Controller
    {
        private WSSDAEntities context = new WSSDAEntities();
        private MainWcfDataService mainService = new MainWcfDataService();
        //private Uri svcUri = new Uri("http://localhost:56386/MainWcfDataService.svc");
        public ActionResult Index()
        {
            //context = new WSSDAEntities();

            var students = mainService.getStudents(context);
            var notes = mainService.getNotes(context);

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
            Student newStud = new Student();
            newStud.name = name;

            mainService.addStudent(newStud, context);

            ViewBag.Students = context.Student;
            return Redirect("/Home/Index");
        }

        public ActionResult UpdateStud()
        {
            return View();
        }



        public ActionResult AddNote()
        {
            return View();
        }
        public RedirectResult AddNoteSave(int studId, int mark, string subject)
        {
            Note newNote = new Note();

            newNote.studentId = studId;
            newNote.note1 = mark;
            newNote.subject = subject;

            mainService.addNote(newNote, context);

            ViewBag.Notes = context.Note;
            return Redirect("/Home/Index");
        }
        public ActionResult UpdateNote()
        {
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