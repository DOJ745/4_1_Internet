using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LB8.Controllers
{
    public class ValuesController : ApiController
    {
        public class ReqJsonRPC
        {
            public string Id { get; set; }
            public string Jsonrpc { get; set; }
            public string Method { get; set; }
            public int[] Params { get; set; }
        }

        public class ResJsonRPC
        {
            public string Id { get; set; }
            public string Jsonrpc { get; set; }
            public string Method { get; set; }
            public int Result { get; set; }
        }




        Dictionary<string, int> valuePairs = new Dictionary<string, int>();
        bool ErrorMark = false;

        public ValuesController()
        {
            HttpContext.Current.Application.Add("VPS", valuePairs);
            HttpContext.Current.Application.Add("ErrorMark", ErrorMark);
        }

        [Route("SetMP")]
        [HttpPost]
        public ResJsonRPC[] SetM([FromBody] ReqJsonRPC[] jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC[] resJson = new ResJsonRPC[jsonreq.Length];

            if (!ErrorMark)
            {
                for (int i = 0; i < jsonreq.Length; i++)
                {
                    resJson[i] = new ResJsonRPC();

                    valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");

                    string k = jsonreq[i].Params[0].ToString();
                    int x = jsonreq[i].Params[1];

                    valuePairs[k] = x;
                    HttpContext.Current.Application.Set("VPS", valuePairs);

                    resJson[i].Id = jsonreq[i].Id;
                    resJson[i].Jsonrpc = jsonreq[i].Jsonrpc;
                    resJson[i].Method = jsonreq[i].Method;
                    resJson[i].Result = x;
                }
            }
            else { resJson[0] = ErrorExit(); }

            return resJson;
        }

        [Route("SetM")]
        [HttpPost]
        public ResJsonRPC SetM([FromBody] ReqJsonRPC jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC resJson = new ResJsonRPC();
            if (!ErrorMark)
            {
                valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");

                string k = jsonreq.Params[0].ToString();
                int x = jsonreq.Params[1];

                valuePairs[k] = x;
                HttpContext.Current.Application.Set("VPS", valuePairs);

                resJson.Id = jsonreq.Id;
                resJson.Jsonrpc = jsonreq.Jsonrpc;
                resJson.Method = jsonreq.Method;
                resJson.Result = x;
            }
            else { resJson = ErrorExit(); }

            return resJson;
        }

        [Route("GetM")]
        [HttpPost]
        public ResJsonRPC GetM([FromBody] ReqJsonRPC jsonRPC)
        {
            valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
            string k = jsonRPC.Params[0].ToString();
            ResJsonRPC resJson = new ResJsonRPC();
            int x = 0;
            try
            {
                x = valuePairs[k];
                resJson.Id = jsonRPC.Id;
                resJson.Jsonrpc = jsonRPC.Jsonrpc;
                resJson.Method = jsonRPC.Method;
                resJson.Result = x;
            }
            catch (Exception)
            {
                bool mark = true;

                HttpContext.Current.Application.Set("ErrorMark", mark);
                HttpContext.Current.Application.Set("VPS", valuePairs);

                resJson = ErrorExit();
            }

            return resJson;
        }

        [Route("AddMP")]
        [HttpPost]
        public ResJsonRPC[] AddM([FromBody] ReqJsonRPC[] jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC[] resJson = new ResJsonRPC[jsonreq.Length];
            if (!ErrorMark)
            {
                for (int i = 0; i < jsonreq.Length; i++)
                {
                    resJson[i] = new ResJsonRPC();
                    valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");

                    string k = jsonreq[i].Params[0].ToString();
                    int x = jsonreq[i].Params[1];
                    valuePairs[k] += x;

                    HttpContext.Current.Application.Set("VPS", valuePairs);
                    resJson[i].Id = jsonreq[i].Id;
                    resJson[i].Jsonrpc = jsonreq[i].Jsonrpc;
                    resJson[i].Method = jsonreq[i].Method;
                    resJson[i].Result = valuePairs[k];
                }
            }
            else { resJson[0] = ErrorExit(); }

            return resJson;
        }

        [Route("AddM")]
        [HttpPost]
        public ResJsonRPC AddM([FromBody] ReqJsonRPC jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC resJson = new ResJsonRPC();
            if (!ErrorMark)
            {
                valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
                string k = jsonreq.Params[0].ToString();
                int x = jsonreq.Params[1];
                valuePairs[k] += x;

                HttpContext.Current.Application.Set("VPS", valuePairs);
                resJson.Id = jsonreq.Id;
                resJson.Jsonrpc = jsonreq.Jsonrpc;
                resJson.Method = jsonreq.Method;
                resJson.Result = valuePairs[k];
            }
            else
            {
                resJson = ErrorExit();
            }

            return resJson;
        }

        [Route("SubMP")]
        [HttpPost]
        public ResJsonRPC[] SubM([FromBody] ReqJsonRPC[] jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC[] resJson = new ResJsonRPC[jsonreq.Length];
            if (!ErrorMark)
            {
                for (int i = 0; i < jsonreq.Length; i++)
                {
                    resJson[i] = new ResJsonRPC();
                    valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");

                    string k = jsonreq[i].Params[0].ToString();
                    int x = jsonreq[i].Params[1];
                    valuePairs[k] -= x;

                    HttpContext.Current.Application.Set("VPS", valuePairs);

                    resJson[i].Id = jsonreq[i].Id;
                    resJson[i].Jsonrpc = jsonreq[i].Jsonrpc;
                    resJson[i].Method = jsonreq[i].Method;
                    resJson[i].Result = valuePairs[k];
                }
            }
            else { resJson[0] = ErrorExit(); }

            return resJson;
        }

        [Route("SubM")]
        [HttpPost]
        public ResJsonRPC SubM([FromBody] ReqJsonRPC jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC resJson = new ResJsonRPC();
            if (!ErrorMark)
            {
                valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
                string k = jsonreq.Params[0].ToString();
                int x = jsonreq.Params[1];
                valuePairs[k] -= x;

                HttpContext.Current.Application.Set("VPS", valuePairs);
                resJson.Id = jsonreq.Id;
                resJson.Jsonrpc = jsonreq.Jsonrpc;
                resJson.Method = jsonreq.Method;
                resJson.Result = valuePairs[k];
            }
            else { resJson = ErrorExit(); }

            return resJson;
        }

        [Route("MulMP")]
        [HttpPost]
        public ResJsonRPC[] MulM([FromBody] ReqJsonRPC[] jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC[] resJson = new ResJsonRPC[jsonreq.Length];
            if (!ErrorMark)
            {
                for (int i = 0; i < jsonreq.Length; i++)
                {
                    resJson[i] = new ResJsonRPC();

                    valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
                    string k = jsonreq[i].Params[0].ToString();
                    int x = jsonreq[i].Params[1];
                    valuePairs[k] *= x;

                    HttpContext.Current.Application.Set("VPS", valuePairs);

                    resJson[i].Id = jsonreq[i].Id;
                    resJson[i].Jsonrpc = jsonreq[i].Jsonrpc;
                    resJson[i].Method = jsonreq[i].Method;
                    resJson[i].Result = valuePairs[k];
                }
            }
            else { resJson[0] = ErrorExit(); }

            return resJson;
        }

        [Route("MulM")]
        [HttpPost]
        public ResJsonRPC MulM([FromBody] ReqJsonRPC jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC resJson = new ResJsonRPC();
            if (!ErrorMark)
            {
                valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");

                string k = jsonreq.Params[0].ToString();
                int x = jsonreq.Params[1];
                valuePairs[k] *= x;

                HttpContext.Current.Application.Set("VPS", valuePairs);

                resJson.Id = jsonreq.Id;
                resJson.Jsonrpc = jsonreq.Jsonrpc;
                resJson.Method = jsonreq.Method;
                resJson.Result = valuePairs[k];
            }
            else { resJson = ErrorExit(); }

            return resJson;
        }

        [Route("DivMP")]
        [HttpPost]
        public ResJsonRPC[] DivM([FromBody] ReqJsonRPC[] jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC[] resJson = new ResJsonRPC[jsonreq.Length];
            if (!ErrorMark)
            {
                for (int i = 0; i < jsonreq.Length; i++)
                {
                    resJson[i] = new ResJsonRPC();

                    valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
                    string k = jsonreq[i].Params[0].ToString();
                    int x = jsonreq[i].Params[1];
                    valuePairs[k] /= x;

                    HttpContext.Current.Application.Set("VPS", valuePairs);
                    resJson[i].Id = jsonreq[i].Id;
                    resJson[i].Jsonrpc = jsonreq[i].Jsonrpc;
                    resJson[i].Method = jsonreq[i].Method;
                    resJson[i].Result = valuePairs[k];
                }
            }
            else
            {
                resJson[0] = ErrorExit();
            }

            return resJson;
        }

        [Route("DivM")]
        [HttpPost]
        public ResJsonRPC DivM([FromBody] ReqJsonRPC jsonreq)
        {
            ErrorMark = (bool)HttpContext.Current.Application.Get("ErrorMark");
            ResJsonRPC resJson = new ResJsonRPC();
            if (!ErrorMark)
            {
                valuePairs = (Dictionary<string, int>)HttpContext.Current.Application.Get("VPS");
                string k = jsonreq.Params[0].ToString();
                int x = jsonreq.Params[1];
                valuePairs[k] /= x;

                HttpContext.Current.Application.Set("VPS", valuePairs);
                resJson.Id = jsonreq.Id;
                resJson.Jsonrpc = jsonreq.Jsonrpc;
                resJson.Method = jsonreq.Method;
                resJson.Result = valuePairs[k];
            }
            else
            {
                resJson = ErrorExit();
            }

            return resJson;
        }

        public ResJsonRPC ErrorExit()
        {
            ResJsonRPC resJson = new ResJsonRPC()
            {
                Id = "404",
                Jsonrpc = "2.0",
                Method = "ErrorExit()",
                Result = 0
            };

            return resJson;
        }
    }
}
