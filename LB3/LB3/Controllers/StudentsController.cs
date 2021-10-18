using LB3.Models;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace LB3.Controllers
{
    /*class Error : HATEOASModel
    {
        public Error(string mdnRef)
        {
            _links.error = mdnRef;
        }
    }

    class BadRequestError : Error
    {
        public BadRequestError() : base("https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/400")
        {

        }
    }*/

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
            public IHttpActionResult Get()
        {
            return Json(TEST_STUD);
        }

        // GET api/students/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Json(new { TEST_STUD, ControllerContext.Request.Headers.Accept });
            }
            catch
            {
                //return BadRequest( JsonConvert.SerializeObject(new BadRequestError()) );
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
