using Newtonsoft.Json;
using System.Net;

namespace TestFramework.Api
{
    public class HttpResponse<T> : IHttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// Response body (Content), deserialized
        /// </summary>
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public Error? Error { get; set; }
        public string? Content { get; set; }
        public string? ContentType { get; set; }
        public HttpRequestMessage? Request { get; set; }
        public string? ComponentCode { get; set; }
        public double TimeMS { get; set; }
        public int Page { get; set; }
    }

    public class Support
    {
        public string? Url { get; set; }
        public string? Text { get; set; }
    }

    public class PagedResult<T>
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty("data")]
        public List<T>? Data { get; set; }

        [JsonProperty("support")]
        public Support? Support { get; set; }
    }
}
