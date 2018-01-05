using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace SwaggerTester.Web.Models
{
    public class ErrorEnvelope : ResponseEnvelopeBase
    {
        public ErrorEnvelope()
            : this(null)
        {
        }

        public ErrorEnvelope(Exception error)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Message = error?.Message ?? HttpStatusCode.InternalServerError.ToString();
        }
    }
}
