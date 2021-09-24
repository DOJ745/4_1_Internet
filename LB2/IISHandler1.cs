﻿using System;
using System.Web;
using System.Collections.Generic;

namespace LB2
{
    public class IISHandler1 : IHttpHandler
    {
        /// <summary>
        /// Вам потребуется настроить этот обработчик в файле Web.config вашего 
        /// веб-сайта и зарегистрировать его с помощью IIS, чтобы затем воспользоваться им.
        /// см. на этой странице: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region Члены IHttpHandler

        public bool IsReusable
        {
            // Верните значение false в том случае, если ваш управляемый обработчик не может быть повторно использован для другого запроса.
            // Обычно значение false соответствует случаю, когда некоторые данные о состоянии сохранены по запросу.
            get { return true; }
        }

        static int RESULT = 0;
        static Stack<int> STACK = new Stack<int>();
        public void ProcessRequest(HttpContext context)
        {
            /*if (context.Session["sessionStack"] == null)
            {
                context.Session["sessionStack"] = new Stack<int>();
            }*/

            switch (context.Request.HttpMethod)
            {
                case "GET":
                    Get(context);
                    break;
                case "POST":
                    Post(context);
                    break;
                case "PUT":
                    Put(context);
                    break;
                case "DELETE":
                    Delete(context);
                    break;
                default:
                    context.Response.Write("Error");
                    break;
            }
        }

        private void Get(HttpContext context)
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
            context.Response.Write("{\"result\": \"" + result + "\"}");
        }

        private void Post(HttpContext context)
        {
            try
            {
                string temp = context.Request.Params["RESULT"];
                int result = int.Parse(context.Request.Params["RESULT"]);
                RESULT = result;
            }
            catch (Exception) { }
        }

        private void Put(HttpContext context)
        {
            try
            {
                int newValue = int.Parse(context.Request.Params["ADD"]);
                STACK.Push(newValue);
            }
            catch (Exception) { }
        }

        private void Delete(HttpContext context)
        {
            try
            {
                int popped = STACK.Pop();
            }
            catch (Exception) { }
        }

        #endregion
    }
}
