using LB3.Models;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

namespace LB3.Controllers
{
    public class StudentsController : ApiController
    {
        private string tempValue = "RESPONSE";
        private Student TEST_STUD = new Student(10, "stud", "+1111");
        private StudentsContext DB = new StudentsContext();

        // GET api/students
        public IHttpActionResult Get()
        {
            return Json(new { TEST_STUD });
        }

        // GET api/students/5
        public IHttpActionResult Get(int id)
        {
            return Json(new { tempValue });
        }

        // POST api/students
        public IHttpActionResult Post([FromBody] string value)
        {
            return Json(new { tempValue });
        }

        // PUT api/students/5
        public IHttpActionResult Put(int id, [FromBody] string value)
        {
            return Json(new { tempValue });
        }

        // DELETE api/students/5
        public IHttpActionResult Delete(int id)
        {
            return Json(new { tempValue });
        }
    }
}
