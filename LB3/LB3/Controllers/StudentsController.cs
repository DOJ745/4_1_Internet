using LB3.Models;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Net;

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
