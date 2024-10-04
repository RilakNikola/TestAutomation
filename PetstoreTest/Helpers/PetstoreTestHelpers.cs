using PetstoreTest.Api.Models.Request;
using PetstoreTest.Api.Services.PetstoreService;
using System.Net;

namespace PetstoreTest.Helpers
{
    public static class PetstoreTestHelpers
    {
        public static async Task CreateUserAndAssertResponse(IPetstoreApiService petstoreApiService, CreateUserRequestDto requestBody, string? message = null)
        {
            var postResponse = await petstoreApiService.CreateUser(requestBody);
            Assert.True(HttpStatusCode.OK == postResponse.StatusCode, Messages.CreateUserStatusCode);
            if (message == null)
                Assert.True(postResponse.Data.Message == requestBody.Id.ToString(), Messages.CreateUserMessage);
            else
                Assert.True(postResponse.Data.Message == message, Messages.CreateUserMessage);
        }

        public static async Task UpdateUserAndAssertResponse(IPetstoreApiService petstoreApiService, string username, UpdateUserRequestDto requestBody, string? message = null)
        {
            var putResponse = await petstoreApiService.UpdateUser(username, requestBody);
            Assert.True(HttpStatusCode.OK == putResponse.StatusCode, Messages.CreateUserStatusCode);
            if (message == null)
                Assert.True(putResponse.Data.Message == requestBody.Id.ToString(), Messages.UpdateUserMessage);
            else
                Assert.True(putResponse.Data.Message == message, Messages.UpdateUserMessage);
        }

        public static async Task DeleteUserAndAssertResponse(IPetstoreApiService petstoreApiService, string username, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var deleteResponse = await petstoreApiService.DeleteUser(username);
            Assert.True(statusCode == deleteResponse.StatusCode, Messages.DeleteUserStatusCode);
            if (statusCode == HttpStatusCode.OK)
                Assert.True(username == deleteResponse.Data.Message, Messages.DeleteUserMessage);
        }

        public static async Task AssertCreatedUser(IPetstoreApiService petstoreApiService, CreateUserRequestDto requestBody, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var getResponse = await petstoreApiService.GetUser(requestBody.Username);
            Assert.True(statusCode == getResponse.StatusCode, Messages.GetUserStatusCode);
            if (statusCode == HttpStatusCode.OK)
            {
                Asserts.AssertCreatedUser(requestBody, getResponse.Data);
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                Assert.True(HttpStatusCode.NotFound == getResponse.StatusCode, Messages.GetUserStatusCode);
                Assert.True(getResponse.Error!.Message == Messages.UserNotFound, Messages.UserNotFoundMessage);
            }
        }

        public static async Task AssertUpdatedUser(IPetstoreApiService petstoreApiService, UpdateUserRequestDto requestBody)
        {
            var getResponse = await petstoreApiService.GetUser(requestBody.Username);
            Assert.True(HttpStatusCode.OK == getResponse.StatusCode, Messages.GetUserStatusCode);
            Asserts.AssertUpdatedUser(requestBody, getResponse.Data);
        }

        public static async Task AssertDeletedUser(IPetstoreApiService petstoreApiService, string username)
        {
            var getResponse = await petstoreApiService.GetUser(username);
            Assert.True(HttpStatusCode.NotFound == getResponse.StatusCode, Messages.GetUserStatusCode);
            Assert.True(getResponse.Error!.Message == Messages.UserNotFound, Messages.UserNotFoundMessage);
        }

        public static async Task EnsureUserDoesNotExist(IPetstoreApiService petstoreApiService, string? username)
        {
            if (username != null)
            {
                //Trying to get user with provided username and if exists delete it so create user doesn't encounter error
                var getResponse = await petstoreApiService.GetUser(username);
                if (getResponse.StatusCode == HttpStatusCode.OK)
                {
                    var deleteResponse = await petstoreApiService.DeleteUser(username);
                    Assert.True(HttpStatusCode.OK == deleteResponse.StatusCode, Messages.DeleteUserStatusCode);
                }
            }
        }
    }
}
