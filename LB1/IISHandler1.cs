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

        public int RESULT = 0;
        public Stack<int> numbersStack = new Stack<int>();

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest req = context.Request;
            HttpResponse res = context.Response;

            res.AddHeader("Content-Type", "application/json");

            if (req.HttpMethod.Equals("GET"))
            { 
                res.Write($"{RESULT}");
            }

            if (req.HttpMethod.Equals("POST") && req.Params["RESULT"] != null)
            {
                RESULT = Convert.ToInt32(req.Params["RESULT"]);
                res.Write($"{RESULT}");
            }

            if (req.HttpMethod.Equals("PUT") && req.Params["ADD"] != null)
            {
                int toPush = Convert.ToInt32(req.Params["ADD"]);
                numbersStack.Push(toPush);
                res.Write($"{numbersStack.Peek()}");
            }

            if (req.HttpMethod.Equals("DELETE"))
            {
                res.Write($"{RESULT}");
            }
        }

        #endregion
    }
}
