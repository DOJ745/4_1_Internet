using LB3.Models;
using System.Web.Http;
using System.Web.WebPages;
using System.Linq;
using System;
using System.Net;
using System.Collections.Generic;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LB3.Controllers
{

    public class StudentsController : ApiController
    {
        private StudentContext DB = new StudentContext();

        // GET api/students
        [ResponseType(typeof(Student))]
        [Route("api/{path:regex((students)[.](json)|(students)[.](xml))}")]
        public IHttpActionResult GetStudents(
            string path = "students.json",

            string name = null,
            string columns = "",
            string phone = null,

            int offset = 0,
            int limit = 45,

            string globalLike = "",
            string orderby = "off",

            int minId = 0,
            int maxId = 10000
        )
        {
            var students = new List<Student>();
            var sqlQuery = string.Empty;
            string orderBy;

            if (orderby.Equals("on")) { orderBy = "NAME"; }
            else { orderBy = "ID"; }

            if (globalLike.IsEmpty())
            {
                string sqlName = "";
                if (name != null) { sqlName = " AND NAME LIKE '%' + @P2 + '%' "; }

                string sqlPhone = "";
                if (phone != null) { sqlPhone = "AND PHONE = @P3 "; }

                sqlQuery = "SELECT * FROM Students " +
                    "WHERE ID > @P0 AND ID < @P1 " + sqlName + sqlPhone +
                    "ORDER BY " + orderBy + " " +
                    "OFFSET @P4 ROWS FETCH NEXT @P5 ROWS ONLY";

                students = DB.Students.SqlQuery(sqlQuery, 
                    minId, 
                    maxId, 
                    name, 
                    phone, 
                    offset, 
                    limit).ToList();
            }
            else
            {
                sqlQuery = "SELECT * FROM Students " +
                    "WHERE ID > @P0 AND " +
                    "ID < @P1 " +
                    "AND (ID LIKE '%' + @P2 + '%' OR NAME LIKE '%' + @P2 + '%' " +
                    "OR PHONE LIKE '%' + @P2 + '%') " +
                    "ORDER BY " + orderBy + " " +
                    "OFFSET @P3 ROWS FETCH NEXT @P4 ROWS ONLY";

                students = DB.Students.SqlQuery(sqlQuery, 
                    minId, 
                    maxId, 
                    globalLike, 
                    offset, 
                    limit).ToList();
            }

            string linkStudents = Request.RequestUri.GetLeftPart(UriPartial.Path);

            students.ForEach(student => student._links = 
                new HateoasLinks(linkStudents, linkStudents + student.ID));

            columns = columns.ToLower();

            if (!columns.Contains("name"))
                students.ForEach(stud => stud.NAME = null);

            if (!columns.Contains("phone"))
                students.ForEach(stud => stud.PHONE = null);

            if (!columns.Contains("id"))
                students.ForEach(stud => stud.ID = 0);

            return Ok(students);
        }

        // GET api/students/5
        [ResponseType(typeof(Student))]
        [Route("api/{path:regex((students)[.](json)|(students)[.](xml))}/{id}")]
        public IHttpActionResult Get(int? id, string path = "students.json")
        {
            try
            {
                var student = DB.Students.Where(stud => stud.ID == id).FirstOrDefault();
                string linkStudent = Request.RequestUri.GetLeftPart(UriPartial.Path);

                student._links = new HateoasLinks(
                    linkStudent.Substring(0, linkStudent.LastIndexOf("/")), 
                    linkStudent);

                return Ok(student);
            }
            catch (Exception)
            {
                return Content(
                    HttpStatusCode.BadRequest, 
                    new CustomError(4500, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }
        }

        // POST api/students
        [ResponseType(typeof(Student))]
        [Route("api/{path:regex((students)[.](json)|(students)[.](xml))}")]
        public IHttpActionResult PostStudent(Student student, string path = "students.json")
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, 
                    new CustomError(4444, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            DB.Students.Add(student);
            DB.SaveChanges();

            string linkStudents = Request.RequestUri.GetLeftPart(UriPartial.Path);
            student._links = new HateoasLinks(linkStudents, linkStudents + student.ID);

            return Ok(student);
        }

        // PUT api/students/5
        [ResponseType(typeof(Student))]
        [Route("api/{path:regex((students)[.](json)|(students)[.](xml))}/{id}")]
        public IHttpActionResult PutStudent(int id, Student student, string path = "students.json")
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, 
                    new CustomError(4444, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            if (id != student.ID)
            {
                return Content(HttpStatusCode.BadRequest, 
                    new CustomError(5000, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            DB.Entry(student).State = EntityState.Modified;

            try { DB.SaveChanges(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return Content(HttpStatusCode.BadRequest, 
                        new CustomError(4404, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
                }
                else { throw; }
            }

            string linkStudents = Request.RequestUri.GetLeftPart(UriPartial.Path);
            student._links = new HateoasLinks(linkStudents, linkStudents + student.ID);

            return Ok(student);
        }

        // DELETE api/students/5
        [ResponseType(typeof(Student))]
        [Route("api/{path:regex((students)[.](json)|(students)[.](xml))}/{id}")]
        public IHttpActionResult DeleteStudent(int id, string path = "students.json")
        {
            Student student = DB.Students.Find(id);
            if (student == null)
            {
                return Content(HttpStatusCode.BadRequest, 
                    new CustomError(4404, Request.RequestUri.GetLeftPart(UriPartial.Authority)));
            }

            DB.Students.Remove(student);
            DB.SaveChanges();

            string linkStudents = Request.RequestUri.GetLeftPart(UriPartial.Path);
            student._links = new HateoasLinks(linkStudents, linkStudents + student.ID);

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { DB.Dispose(); }
            base.Dispose(disposing);
        }

        private bool StudentExists(int id)
        {
            return DB.Students.Count(stud => stud.ID == id) > 0;
        }
    }
}
