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

            if(context.Session["sessionStack"] == null)
                context.Session.Add("sessionStack", numbersStack);

            //res.AddHeader("Content-Type", "application/json");

            if (req.HttpMethod.Equals("GET"))
            {
                Stack<int> tempNumbersStack = (Stack<int>)context.Session["sessionStack"];
                try
                {
                    RESULT += tempNumbersStack.Peek();
                }
                catch (InvalidOperationException e)
                {
                    RESULT += 0;
                }
                res.Write($"{RESULT}");
                res.Write("" +
                    "<form action='http://localhost:61592/FAA.XXX/' method='put'>" +
                    "<label for='PUT-name'>ADD value:</label>" +
                    "<br>" +
                    "<input id='PUT-name type='number' name='ADD'>" +
                    "<br><br>" +
                    "<input type='submit' value='Add to stack'>" +
                    "</form>");
            }

            if (req.HttpMethod.Equals("POST") && req.Params["RESULT"] != null)
            {
                RESULT = Convert.ToInt32(req.Params["RESULT"]);
                res.Write($"{RESULT}");
            }

            if (req.HttpMethod.Equals("PUT") && req.Params["ADD"] != null)
            {
                //context.Session["sessionStack"] = numbersStack;
                int toPush = Convert.ToInt32(req.Params["ADD"]);
                //context.Session.Add("sessionStack", numbersStack);
                Stack<int> tempNumbersStack = (Stack<int>)context.Session["sessionStack"];
                //numbersStack.Push(toPush);
                //res.Write($"{numbersStack.Peek()}");
                tempNumbersStack.Push(toPush);
                context.Session["sessionStack"] = tempNumbersStack;
                //res.Write($"<h2>{tempNumbersStack.Peek()}</h2>");
            }

            if (req.HttpMethod.Equals("DELETE"))
            {
                Stack<int> tempNumbersStack = (Stack<int>)context.Session["sessionStack"];
                tempNumbersStack.Pop();
                res.Write($"{RESULT}");
            }
        }

        #endregion
    }
}
