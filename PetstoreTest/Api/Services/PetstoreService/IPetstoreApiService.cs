using PetstoreTest.Api.Models.Request;
using PetstoreTest.Api.Models.Response;
using TestFramework.Api;

namespace PetstoreTest.Api.Services.PetstoreService
{
    public interface IPetstoreApiService
    {
        Task<HttpResponse<BaseResponse>> CreateUser(CreateUserRequestDto requestBody);
        Task<HttpResponse<BaseResponse>> DeleteUser(string username);
        Task<HttpResponse<GetUserDto>> GetUser(string username);
        Task<HttpResponse<BaseResponse>> UpdateUser(string username, UpdateUserRequestDto requestBody);
    }
}