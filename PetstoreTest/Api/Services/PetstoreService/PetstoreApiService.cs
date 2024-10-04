using PetstoreTest.Api.Models.Request;
using PetstoreTest.Api.Models.Response;
using TestFramework.Api;
using TestFramework.Config;

namespace PetstoreTest.Api.Services.PetstoreService
{
    public class PetstoreApiService : AutomationApiClient, IPetstoreApiService
    {
        public PetstoreApiService()
        {
            var config = ConfigReader.ReadConfig();
            Client.BaseAddress = config.ApiBaseUrl;
        }

        #region User
        public async Task<HttpResponse<GetUserDto>> GetUser(string username)
        {
            var request = CreateRequest(HttpMethod.Get, PetstoreApiRoutes.GetUserByUsername(username));
            var response = await ExecuteAsync<GetUserDto>(request);
            return response;
        }

        public async Task<HttpResponse<BaseResponse>> CreateUser(CreateUserRequestDto requestBody)
        {
            var request = CreateRequest(HttpMethod.Post, PetstoreApiRoutes.CreateUser(), requestBody);
            var response = await ExecuteAsync<BaseResponse>(request);
            return response;
        }

        public async Task<HttpResponse<BaseResponse>> UpdateUser(string username, UpdateUserRequestDto requestBody)
        {
            var request = CreateRequest(HttpMethod.Put, PetstoreApiRoutes.UpdateUser(username), requestBody);
            var response = await ExecuteAsync<BaseResponse>(request);
            return response;
        }

        public async Task<HttpResponse<BaseResponse>> DeleteUser(string username)
        {
            var request = CreateRequest(HttpMethod.Delete, PetstoreApiRoutes.DeleteUser(username));
            var response = await ExecuteAsync<BaseResponse>(request);
            return response;
        }
        #endregion
    }
}
