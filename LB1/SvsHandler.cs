using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace lab1
{
	public class SvsHandler : IHttpHandler, IRequiresSessionState
	{
		/// <summary>
		/// Вам потребуется настроить этот обработчик в файле Web.config вашего 
		/// веб-сайта и зарегистрировать его с помощью IIS, чтобы затем воспользоваться им.
		/// см. на этой странице: https://go.microsoft.com/?linkid=8101007
		/// </summary>
		#region Члены IHttpHandler

		private static int Result = 0;

		public bool IsReusable
		{
			// Верните значение false в том случае, если ваш управляемый обработчик не может быть повторно использован для другого запроса.
			// Обычно значение false соответствует случаю, когда некоторые данные о состоянии сохранены по запросу.
			get { return true; }
		}

		public void ProcessRequest(HttpContext context)
		{
			if (context.Session["Stack"] == null)
			{
				context.Session["Stack"] = new Stack<int>();
			}

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
				result = Result
					+ (context.Session["Stack"] as Stack<int>).Peek();
			}
			catch (Exception)
			{
				result = Result;
			}
			context.Response.Write("{\"result\": \"" + result + "\"}");
		}

		private void Post(HttpContext context)
		{
			try
			{
				string a = context.Request.Params["Result"];
				int result = int.Parse(context.Request.Params["Result"]);
				Result = result;
			}
			catch (Exception) { }
		}

		private void Put(HttpContext context)
		{
			try
			{
				int newValue = int.Parse(context.Request.Params["Add"]);
				(context.Session["Stack"] as Stack<int>).Push(newValue);
			}
			catch (Exception) { }
		}

		private void Delete(HttpContext context)
		{
			try
			{
				int value = (context.Session["Stack"] as Stack<int>).Pop();
			}
			catch (Exception) { }
		}

		#endregion
	}
}
