using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LB2_1.Controllers
{
    public class ValuesController : ApiController
    {
        static int VALUE = 0;
        static Stack<int> STACK = new Stack<int>();

        public IHttpActionResult Get()
        {
            int result;
            try
            {
                result = VALUE + STACK.Peek();
            }
            catch (Exception)
            {
                result = VALUE;
            }
            return Json(new { result });
        }

        // POST api/values
        public IHttpActionResult Post([FromUri] int RESULT)
        {
            try
            {
                VALUE = RESULT;
            }
            catch (Exception) { }
            return Json(new { RESULT });
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromUri] int ADD)
        {
            try
            {
                STACK.Push(ADD);
            }
            catch (Exception) { }
            return Json(new { ADD });
        }

        // DELETE api/values/5
        public IHttpActionResult Delete()
        {
            int popped = 0;
            try
            {
               popped = STACK.Pop();
            }
            catch (Exception) { }
            return Json(new { deleted = popped });
        }
    }
}
