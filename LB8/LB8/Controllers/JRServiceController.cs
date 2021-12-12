using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            /*
            public string getMethod()
            {
                return method;
            }

            public void setMethod(string _method)
            {
                method = _method;
            }
            */
            public string result { get; set; }
            /*
            public string getResult()
            {
                return result;
            }

            public void setResult(string _result)
            {
                result = _result;
            }
            */
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

        public class CustomCache
        {
            private Dictionary<String, int> cache = new Dictionary<String, int>();

            public void AddValue(string key, int value)
            {
                cache[key] = value;
            }

            public int GetValue(string key)
            {
                try { return cache[key]; }
                catch (KeyNotFoundException) { return 0; }
            }
            public void Clear() { cache.Clear(); }
        }

        private static CustomCache cache = new CustomCache();

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
                        cache.Clear();
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
                cache.AddValue(key, value);

                var response = new JsonRPCResponse();

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = value.ToString();
                //response.setMethod(request.Method);
                //response.setResult(value.ToString());

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
                response.result = cache.GetValue(key).ToString();
                //response.setMethod(request.Method);
                //response.setResult(cache.GetValue(key).ToString());

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
                cache.AddValue(key, cache.GetValue(key) + value);

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = cache.GetValue(key).ToString();
                //response.setMethod(request.Method);
                //response.setResult(cache.GetValue(key).ToString());

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

                cache.AddValue(key, cache.GetValue(key) - value);

                response.id = request.Id; ;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = cache.GetValue(key).ToString();
                //response.setMethod(request.Method);
                //response.setResult(cache.GetValue(key).ToString());

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

                cache.AddValue(key, cache.GetValue(key) * value);

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = cache.GetValue(key).ToString();
                //response.setMethod(request.Method);
                //response.setResult(cache.GetValue(key).ToString());

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

                cache.AddValue(key, cache.GetValue(key) / value);

                response.id = request.Id;
                response.jsonrpc = request.JsonRpc;
                response.method = request.Method;
                response.result = cache.GetValue(key).ToString();
                //response.setMethod(request.Method);
                //response.setResult(cache.GetValue(key).ToString());

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
            //errorRes.setMethod(request.Method);
            //errorRes.setResult(null);

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
