using System;
using System.Web;
using System.Collections.Generic;
using System.Web.SessionState;

namespace LB1
{
    public class IISHandler1 : IHttpHandler, IRequiresSessionState
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
        Stack<int> sessionStack;
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

            res.AddHeader("Content-Type", "application/json");

            if (context.Session["sessionStack"] == null)
            {
                //Stack<int> numbersStack = new Stack<int>();
                //context.Session.Add("sessionStack", numbersStack);

                sessionStack = new Stack<int>();
                context.Session.Add("sessionStack", sessionStack);

                //sessionStack = context.Session["sessionStack"] as Stack<int>;
            }
                
            if (req.HttpMethod.Equals("GET"))
            {
                try
                {
                    res.Write($"{RESULT + (context.Session["sessionStack"] as Stack<int>).Peek()}");
                    //res.Write($"{RESULT + sessionStack.Peek()}");
                }
                catch (InvalidOperationException e)
                {
                    res.Write($"{RESULT}");
                }
            }

            if (req.HttpMethod.Equals("POST") && req.Params["RESULT"] != null)
            {
                RESULT = Convert.ToInt32(req.Params["RESULT"]);
                res.Write($"{RESULT}");
            }

            if (
                ( req.Form.Get("_method") != null &&
                req.Form.Get("_method").Equals("PUT") &&
                req.Params["ADD"] != null ) ||
                req.HttpMethod.Equals("PUT"))
            {
                context.Session.Add("sessionStack", sessionStack);
                int toPush = Convert.ToInt32(req.Params["ADD"]);
                (context.Session["sessionStack"] as Stack<int>).Push(toPush);
                //sessionStack.Push(toPush);

                //res.Write($"{sessionStack.Peek()}");
                res.Write($"{(context.Session["sessionStack"] as Stack<int>).Peek()}");
            }

            if (
                (req.Form.Get("_method") != null &&
                req.Form.Get("_method").Equals("DELETE") ) || 
                req.HttpMethod.Equals("DELETE") 
                )
            {
                try
                {
                    context.Session.Add("sessionStack", sessionStack);
                    res.Write($"{RESULT + (context.Session["sessionStack"] as Stack<int>).Peek()}");
                    //res.Write($"{RESULT + sessionStack.Peek()}");
                }
                catch (InvalidOperationException e)
                {
                    res.Write($"{RESULT}");
                }
            }
        }

        #endregion
    }
}
