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
        public ActionResult Index()
        {

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

        public ActionResult UpdateStud(string id)
        {
            ViewBag.Id = id;
            ViewBag.Students = mainService.getStudents(context);
            return View();
        }

        public RedirectResult UpdateStudSave(string id, string name)
        {
            mainService.updateStudent(id, name, context);
            ViewBag.Students = mainService.getStudents(context);
            return Redirect("/Home/Index");
        }

        public ActionResult DeleteStud(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public RedirectResult DeleteStudSave(string id)
        {
            mainService.deleteStudent(id, context);
            ViewBag.Students = mainService.getStudents(context);
            return Redirect("/Home/Index");
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
        public ActionResult UpdateNote(string id)
        {
            ViewBag.Id = id;
            ViewBag.Notes = mainService.getNotes(context);
            return View();
        }

        public RedirectResult UpdateNoteSave(string id, int mark, int studentId, string subject)
        {
            mainService.updateNote(id, studentId, mark, subject, context);
            ViewBag.Notes = mainService.getNotes(context);
            return Redirect("/Home/Index");
        }

        public ActionResult DeleteNote(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public RedirectResult DeleteNoteSave(string id)
        {
            mainService.deleteNote(id, context);
            ViewBag.Notes = mainService.getNotes(context);
            return Redirect("/Home/Index");
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