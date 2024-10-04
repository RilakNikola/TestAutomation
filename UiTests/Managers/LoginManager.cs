using UiTests.Helpers;
using UiTests.Managers.Interfaces;
using UiTests.Pages.Interfaces;

namespace UiTests.Managers
{
    public class LoginManager(IHomePage homePage, ILoginModal loginModal) : ILoginManager
    {
        public void Login(User user)
        {
            homePage.ClickLogin();
            loginModal.Login(user);
        }
    }
}
