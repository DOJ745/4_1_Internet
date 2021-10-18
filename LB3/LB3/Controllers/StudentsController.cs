using LB3.Models;
using System.Web.Http;
using System.Web.WebPages;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Net;
using System.Collections.Generic;

namespace LB3.Controllers
{

    public class StudentsController : ApiController
    {
        private Student TEST_STUD = new Student(10, "stud", "+1 111-11-11");
        private StudentsContext DB = new StudentsContext();

        [Route("api/students/search")]
        [HttpGet]
        public IHttpActionResult SearchStudents()
        {
            return Json(TEST_STUD);
        }

        // GET api/students
        public IHttpActionResult GetStudents(
            string name = null,
            string columns = "",
            string phone = null,

            int offset = 0,
            int limit = 45,

            string globalLike = "",
            string orderby = "off",

            int minId = Int32.MinValue,
            int maxId = Int32.MaxValue
        )
        {
            var students = new List<Student>();
            var sqlQuery = string.Empty;
            string orderBy;

            if (orderby.Equals("on")) { orderBy = "Name"; }
            else { orderBy = "Id"; }

            if (globalLike.IsEmpty())
            {
                string sqlName = "";
                if (name != null) { sqlName = " AND Name LIKE '%' + @P2 + '%' "; }

                string sqlPhone = "";
                if (phone != null) { sqlPhone = "AND Phone = @P3 "; }

                sqlQuery = "SELECT * FROM Student " +
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
                sqlQuery = "SELECT * FROM Student " +
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
                new HateoasLinks(linkStudents, linkStudents + "/" + student.ID));

            columns = columns.ToLower();

            if (!columns.Contains("name"))
                students.ForEach(x => x.NAME = null);

            if (!columns.Contains("phone"))
                students.ForEach(x => x.PHONE = null);

            if (!columns.Contains("id"))
                students.ForEach(x => x.ID = 0);

            return Ok(students);
        }

        // GET api/students/5
        [Route("api/Students/{id}")]
        public IHttpActionResult Get(int? id)
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
        public IHttpActionResult Post([FromBody] string value)
        {
            return Json(new { TEST_STUD });
        }

        // PUT api/students/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            return Json(new { TEST_STUD });
        }

        // DELETE api/students/5
        public IHttpActionResult Delete(int id)
        {
            return Json(new { TEST_STUD });
        }
    }
}
