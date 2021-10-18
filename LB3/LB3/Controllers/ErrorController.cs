using LB3.Models;
using System.Web.Http;

namespace LB3.Controllers
{
    public class ErrorController : ApiController
    {
        [Route("api/Error/{code}")]
        public IHttpActionResult Get(int code)
        {
            CustomErrorDetails errorDetails;
            switch (code)
            {
                case 4500:
                    errorDetails = new CustomErrorDetails(4500, "Server error");
                    break;

                case 4444:
                    errorDetails = new CustomErrorDetails(4444, "Model is invalid");
                    break;

                case 4404:
                    errorDetails = new CustomErrorDetails(4404, "Not found");
                    break;

                default:
                    errorDetails = new CustomErrorDetails(4999, "Unknown error code");
                    break;
            }

            return Ok(errorDetails);
        }
    }
}
