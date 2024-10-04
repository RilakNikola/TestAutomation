using PetstoreTest.Api.Models.Request;
using PetstoreTest.Api.Services.PetstoreService;
using PetstoreTest.Helpers;
using System.Net;
using TestFramework.Extensions;

namespace PetstoreTest.Tests
{
    public class PetstoreTests : IAsyncLifetime
    {
        private readonly User _user;
        private readonly IPetstoreApiService _petstoreApiService;
        private readonly CreateUserRequestDto _postRequestBody;
        private readonly UpdateUserRequestDto _putRequestBody;

        public PetstoreTests(IPetstoreApiService petstoreApiService)
        {
            _petstoreApiService = petstoreApiService;
            _user = TestDataMethods.GetRandomUser();
            _postRequestBody = UserMapper.PrepareCreateUserRequest(_user);
            _user.Id = _postRequestBody.Id;
            _putRequestBody = UserMapper.PrepareUpdateUserRequest(_user);
        }

        [Fact]
        public async Task FullFlow()
        {
            //Creating new user and verifying that data from POST request body matches with the response of the GET
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody);

            //Updating user and verifying that data from PUT request body matches with the response of the GET
            await PetstoreTestHelpers.UpdateUserAndAssertResponse(_petstoreApiService, _postRequestBody.Username!, _putRequestBody);
            await PetstoreTestHelpers.AssertUpdatedUser(_petstoreApiService, _putRequestBody);

            //As we changed username with PUT method, verifying that old username doesn't exist anymore
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody, HttpStatusCode.NotFound);

            //Deleting user and verifying it is deleted by getting 404 in GET
            await PetstoreTestHelpers.DeleteUserAndAssertResponse(_petstoreApiService, _putRequestBody.Username!);
            await PetstoreTestHelpers.AssertDeletedUser(_petstoreApiService, _putRequestBody.Username!);
        }

        [Fact]
        public async Task Get_Success()
        {
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody);
            //Note: If we have database access, this test would connect with database to get existing user and compare data with database
        }

        [Fact]
        public async Task Get_NotFound()
        {
            //Getting user with non existing username
            var getResponse = await _petstoreApiService.GetUser(GenericExtensions.GetRandomString(10));
            Assert.True(HttpStatusCode.NotFound == getResponse.StatusCode, Messages.GetUserStatusCode);
            Assert.True(getResponse.Error!.Message == Messages.UserNotFound, Messages.UserNotFoundMessage);
        }

        [Fact]
        public async Task Post_Success()
        {
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody);
        }

        [Fact]
        public async Task Post_BadRequestNull()
        {
            //Creating new user without username and id and expecting message 0 in the response
            _postRequestBody.Username = null;
            _postRequestBody.Id = null;
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody, 0.ToString());
        }
        
        [Fact]
        public async Task Put_Success()
        {
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.UpdateUserAndAssertResponse(_petstoreApiService, _postRequestBody.Username!, _putRequestBody);
            await PetstoreTestHelpers.AssertUpdatedUser(_petstoreApiService, _putRequestBody);
            //As we changed username with PUT method, verifying that old username doesn't exist anymore
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody, HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_BadRequest()
        {
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            _putRequestBody.Username = null;
            _putRequestBody.Id = null;
            //Creating new user without username and id and expecting message 0 in the response
            await PetstoreTestHelpers.UpdateUserAndAssertResponse(_petstoreApiService, _postRequestBody.Username!, _putRequestBody, 0.ToString());
            //Asserting that our user is not updated since it should be bad request
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody);
        }

        [Fact]
        public async Task Delete_Success()
        {
            await PetstoreTestHelpers.CreateUserAndAssertResponse(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.AssertCreatedUser(_petstoreApiService, _postRequestBody);
            await PetstoreTestHelpers.DeleteUserAndAssertResponse(_petstoreApiService, _postRequestBody.Username!);
            await PetstoreTestHelpers.AssertDeletedUser(_petstoreApiService, _postRequestBody.Username!);
        }

        [Fact]
        public async Task Delete_BadRequest()
        {
            //Deleting user with non existing username
            await PetstoreTestHelpers.DeleteUserAndAssertResponse(_petstoreApiService, GenericExtensions.GetRandomString(10), HttpStatusCode.NotFound);
        }

        public async Task InitializeAsync()
        {
            // Ensure the user does not exist before each test runs
            await PetstoreTestHelpers.EnsureUserDoesNotExist(_petstoreApiService, _postRequestBody.Username);
        }

        public async Task DisposeAsync()
        {
            // Clean up by deleting the user if needed
            await PetstoreTestHelpers.EnsureUserDoesNotExist(_petstoreApiService, _postRequestBody.Username);
            await PetstoreTestHelpers.EnsureUserDoesNotExist(_petstoreApiService,_putRequestBody.Username);
        }
    }
}