namespace PetstoreTest.Api.Services.PetstoreService
{
    public class PetstoreApiRoutes
    {
        #region User
        public static string GetUserByUsername(string username) => $"/v2/user/{username}";
        public static string CreateUser() => $"/v2/user";
        public static string UpdateUser(string username) => $"/v2/user/{username}";
        public static string DeleteUser(string username) => $"/v2/user/{username}";
        #endregion
    }
}
