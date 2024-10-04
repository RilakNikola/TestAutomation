using PetstoreTest.Api.Models.Request;
using PetstoreTest.Api.Models.Response;

namespace PetstoreTest.Helpers
{
    public class Asserts
    {
        public static void AssertCreatedUser(CreateUserRequestDto requestBody, GetUserDto user)
        {
            Assert.True(requestBody.Id == user.Id, "Id");
            Assert.True(requestBody.Username == user.Username, "Username");
            Assert.True(requestBody.FirstName == user.FirstName, "FirstName");
            Assert.True(requestBody.LastName == user.LastName, "LastName");
            Assert.True(requestBody.Email == user.Email, "Email");
            Assert.True(requestBody.Password == user.Password, "Password");
            Assert.True(requestBody.Phone == user.Phone, "Phone");
            Assert.True(requestBody.UserStatus == user.UserStatus, "UserStatus");
        }

        public static void AssertUpdatedUser(UpdateUserRequestDto requestBody, GetUserDto user)
        {
            Assert.True(requestBody.Id == user.Id, "Id");
            Assert.True(requestBody.Username == user.Username, "Username");
            Assert.True(requestBody.FirstName == user.FirstName, "FirstName");
            Assert.True(requestBody.LastName == user.LastName, "LastName");
            Assert.True(requestBody.Email == user.Email, "Email");
            Assert.True(requestBody.Password == user.Password, "Password");
            Assert.True(requestBody.Phone == user.Phone, "Phone");
            Assert.True(requestBody.UserStatus == user.UserStatus, "UserStatus");
        }
    }
}
