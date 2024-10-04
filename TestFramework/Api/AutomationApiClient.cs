using Newtonsoft.Json;
using System.Text;

namespace TestFramework.Api
{
    public class AutomationApiClient
    {
        protected readonly HttpClient Client;

        public AutomationApiClient()
        {
            Client = new HttpClient();
        }

        public static HttpRequestMessage CreateRequest(HttpMethod method, string url, object? body = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
                request.Content = new StringContent(JsonConvert.SerializeObject(body, jsonSettings), Encoding.UTF8, "application/json");
            }
                

            return request;
        }

        protected async Task<HttpResponse<T>> ExecuteAsync<T>(HttpRequestMessage request)
        {
            HttpResponseMessage? response = await Client.SendAsync(request);

            var result = new HttpResponse<T>
            {
                StatusCode = response.StatusCode,
                Content = await response.Content.ReadAsStringAsync(),
                ContentType = response.Content.Headers?.ContentType?.MediaType,
                Request = request,
            };

            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                result.Data = JsonConvert.DeserializeObject<T>(result.Content);
                return result;
            }

            result.Error = JsonConvert.DeserializeObject<Error>(result.Content);
            result.IsSuccess = false;

            return result;
        }
    }
}

