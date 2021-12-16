using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LB8.Controllers
{
    public class JRServiceController : ApiController
    {

        public class JsonRPCRequest
        {
            public string Id { get; set; }
            public string JsonRpc { get; set; }
            public string Method { get; set; }
            public Object[] Params { get; set; }
        }

        public class JsonRPCResponse
        {
            public string id { get; set; }
            public string jsonrpc { get; set; }
            public string method { get; set; }
            public string result { get; set; }
        }

        public class JsonRPCError
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        public class JsonRPCErrorResponse: JsonRPCResponse
        {
            public JsonRPCError error;
        }

        [HttpPost]
        public List<JsonRPCResponse> Post(JsonRPCRequest[] request)
        {
            List<JsonRPCResponse> response = new List<JsonRPCResponse>();

            foreach (JsonRPCRequest requestItem in request)
            {
                JsonRPCResponse responseItem = null;

                switch (requestItem.Method.ToLower())
                {
                    case "setm":
                        responseItem = SetM(requestItem);
                        break;

                    case "getm":
                        responseItem = GetM(requestItem);
                        break;

                    case "addm":
                        responseItem = AddM(requestItem);
                        break;

                    case "subm":
                        responseItem = SubM(requestItem);
                        break;

                    case "mulm":
                        responseItem = MulM(requestItem);
                        break;

                    case "divm":
                        responseItem = DivM(requestItem);
                        break;

                    case "errorexit":
                        return response;

                    default:
                        responseItem = methodNotFound(requestItem);
                        break;
                }
                response.Add(responseItem);
            }
            return response;
        }

        [NonAction]
        private JsonRPCResponse SetM(JsonRPCRequest request)
        {
            try
            {
                string key = request.Params[0] as string;
                int value = int.Parse(request.Params[1].ToString());

                HttpContext.Current.Session[key] = value;

                var response = new JsonRPCResponse();

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = HttpContext.Current.Session[key].ToString();

                return response;
            }
            catch (Exception) { return invalidParams(request); }
        }

        [NonAction]
        private JsonRPCResponse GetM(JsonRPCRequest request)
        {
            try
            {
                var key = request.Params[0] as string;
                var response = new JsonRPCResponse();
                
                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = HttpContext.Current.Session[key].ToString();

                return response;
            }
            catch (Exception) { return invalidParams(request); }
        }

        [NonAction]
        private JsonRPCResponse AddM(JsonRPCRequest request)
        {
            try
            {
                var response = new JsonRPCResponse();
                var key = request.Params[0] as string;
                int value = int.Parse(request.Params[1].ToString());

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;

                int sum = (int)HttpContext.Current.Session[key] + value;
                response.result = sum.ToString();

                HttpContext.Current.Session[key] = sum;

                return response;
            }
            catch (Exception)
            {
                return invalidParams(request);
            }
        }

        [NonAction]
        private JsonRPCResponse SubM(JsonRPCRequest request)
        {
            try
            {
                var response = new JsonRPCResponse();
                var key = request.Params[0] as string;
                int value = int.Parse(request.Params[1].ToString());

                response.id = request.Id; ;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                int sub = (int)HttpContext.Current.Session[key] - value;
                response.result = sub.ToString();

                HttpContext.Current.Session[key] = sub;

                return response;
            }
            catch (Exception) { return invalidParams(request); }
        }

        [NonAction]
        private JsonRPCResponse MulM(JsonRPCRequest request)
        {
            try
            {
                var response = new JsonRPCResponse();
                var key = request.Params[0] as string;
                int value = int.Parse(request.Params[1].ToString());

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                int mul = (int)HttpContext.Current.Session[key] * value;
                response.result = mul.ToString();

                HttpContext.Current.Session[key] = mul;

                return response;
            }
            catch (Exception) { return invalidParams(request); }
        }

        [NonAction]
        private JsonRPCResponse DivM(JsonRPCRequest request)
        {
            try
            {
                var response = new JsonRPCResponse();
                var key = request.Params[0] as string;
                int value = int.Parse(request.Params[1].ToString());

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                int div = (int)HttpContext.Current.Session[key] / value;
                response.result = div.ToString();

                HttpContext.Current.Session[key] = div;

                return response;
            }
            catch (Exception) { return invalidParams(request); }
        }

        [NonAction]
        private JsonRPCResponse methodNotFound(JsonRPCRequest request)
        {
            JsonRPCErrorResponse errorRes = new JsonRPCErrorResponse();

            errorRes.jsonrpc = request.JsonRpc;
            errorRes.id = request.Id;
            errorRes.method = request.Method;
            errorRes.result = null;

            JsonRPCError error = new JsonRPCError();

            error.code = -32601;
            error.message = "Method not found";

            errorRes.error = error;

            return errorRes;
        }

        [NonAction]
        private JsonRPCResponse invalidParams(JsonRPCRequest request)
        {
            JsonRPCErrorResponse errorRes = new JsonRPCErrorResponse();

            errorRes.jsonrpc = request.JsonRpc;
            errorRes.id = request.Id;
            errorRes.method = request.Method;
            errorRes.result = null;

            JsonRPCError error = new JsonRPCError();

            error.code = -32602;
            error.message = "Invalid params";
            
            errorRes.error = error;

            return errorRes;
        }
    }
}
