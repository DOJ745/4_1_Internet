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

        public string Get()
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
            return "{\"result\": \"" + result + "\"}";
        }

        // POST api/values
        public string Post([FromUri] int RESULT)
        {
            try
            {
                VALUE = RESULT;
            }
            catch (Exception) { }
            return "post";
        }

        // PUT api/values/5
        public void Put([FromUri] int ADD)
        {
            try
            {
                STACK.Push(ADD);
            }
            catch (Exception) { }
        }

        // DELETE api/values/5
        public void Delete()
        {
            try
            {
                int popped = STACK.Pop();
            }
            catch (Exception) { }
        }
    }
}
