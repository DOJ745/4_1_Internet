using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;

namespace LB2_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        /*
        [HttpGet]
        public void GET()
        {
            int result;
            try
            {
                result = RESULT + STACK.Peek();
            }
            catch (Exception)
            {
                result = RESULT;
            }
            this.HttpContext.Response.Write("{\"result\": \"" + result + "\"}");
        }

        [HttpPost]
        public IHttp POST(string? result)
        {
            try
            {
                string temp = this.HttpContext.Request.Params["RESULT"];
                int result = int.Parse(this.HttpContext.Request.Params["RESULT"]);
                RESULT = result;
            }
            catch (Exception) { }
        }

        [HttpPut]
        public void PUT()
        {
            try
            {
                int newValue = int.Parse(this.HttpContext.Request.Params["ADD"]);
                STACK.Push(newValue);
            }
            catch (Exception) { }
        }

        [HttpDelete]
        public void DELETE()
        {
            try
            {
                int popped = STACK.Pop();
            }
            catch (Exception) { }
        }*/
    }
}
