namespace SwaggerTester.Web.Models
{
    public abstract class ResponseEnvelopeBase
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
