using PetstoreTest.Api.Models.Request;
using TestFramework.Extensions;

namespace PetstoreTest.Helpers
{
    public class UserMapper
    {
        public static CreateUserRequestDto PrepareCreateUserRequest(User user)
        {
            return new CreateUserRequestDto
            {
                Id = GenericExtensions.GetRandomInt(),
                Username = user.Username + GenericExtensions.GetRandomInt(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Phone = user.Phone,
                UserStatus = user.UserStatus,
            };
        }

        public static UpdateUserRequestDto PrepareUpdateUserRequest(User user)
        {
            return new UpdateUserRequestDto
            {
                Id = user.Id,
                Username = user.Username + GenericExtensions.GetRandomString(2),
                FirstName = user.FirstName + GenericExtensions.GetRandomString(2),
                LastName = user.LastName + GenericExtensions.GetRandomString(2),
                Email = GenericExtensions.GetRandomString(2) + user.Email,
                Password = user.Password + GenericExtensions.GetRandomString(2),
                Phone = user.Phone + GenericExtensions.GetRandomInt(1, 9),
                UserStatus = user.UserStatus,
            };
        }
    }
}
