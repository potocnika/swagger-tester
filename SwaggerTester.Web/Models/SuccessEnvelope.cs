using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace SwaggerTester.Web.Models
{
    public class SuccessEnvelope : ResponseEnvelopeBase
    {
        public SuccessEnvelope()
        {
            StatusCode = StatusCodes.Status200OK;
            Message = HttpStatusCode.OK.ToString();
        }

        public List<Animal> Animals { get; set; }
    }
}
