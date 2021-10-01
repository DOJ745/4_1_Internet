using LB3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LB3.Controllers
{
    public class StudentsController : ApiController
    {
        private string tempValue = "RESPONSE";
        private StudentsContext DB = new StudentsContext();

        // GET api/students
        public IHttpActionResult Get()
        {
            return Json(new { tempValue });
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
