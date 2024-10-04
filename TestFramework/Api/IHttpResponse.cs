using System.Net;

namespace TestFramework.Api
{
    public interface IHttpResponse
    {
        /// <summary>
        /// Response body as string
        /// </summary>
        string Content { get; set; }
        /// <summary>
        /// Indicates the sort of content within Content field. Useful because APIs may return HTML in error conditions. ie application/json 
        /// </summary>
        string ContentType { get; set; }
        bool IsSuccess { get; set; }
        /// <summary>
        /// If available, PaycorError from domain or self-generated. Safe to expose to caller
        /// </summary>
        Error Error { get; set; }
        /// <summary>
        /// Request is only saved if there was an error. Do not reveal to caller. Only log non-sensitive properties.
        /// </summary>
        HttpRequestMessage Request { get; set; }
        /// <summary>
        /// HTTP numeric code returned by API
        /// </summary>
        HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Safe to expose to caller to help Paycor more quickly identify error source. One of Constants.cs:ApiComponentCodes
        /// </summary>
        string ComponentCode { get; set; }
        public double TimeMS { get; set; }
    }
}
