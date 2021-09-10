using System;
using System.Web;
using System.Collections.Generic;

namespace LB1
{
    public class IISHandler1 : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

        int RESULT = 0;
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;
            res.AddHeader("Content-Type", "application/json");

            if (context.Session["sessionStack"] == null)
            {
                Stack<int> numbersStack = new Stack<int>();
                context.Session.Add("sessionStack", numbersStack);
            }
                
            if (req.HttpMethod.Equals("GET"))
            {
                try
                {
                    RESULT += (context.Session["sessionStack"] as Stack<int>).Peek();
                }
                catch (InvalidOperationException e)
                {
                    RESULT += 0;
                }
                res.Write($"{RESULT}");
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
                int toPush = Convert.ToInt32(req.Params["ADD"]);
                (context.Session["sessionStack"] as Stack<int>).Push(toPush);
                res.Write($"{(context.Session["sessionStack"] as Stack<int>).Peek()}");
            }

            if (
                (req.Form.Get("_method") != null &&
                req.Form.Get("_method").Equals("DELETE") ) || 
                req.HttpMethod.Equals("DELETE"))
            {
                (context.Session["sessionStack"] as Stack<int>).Pop();
                res.Write($"{RESULT}");
            }
        }

        #endregion
    }
}
