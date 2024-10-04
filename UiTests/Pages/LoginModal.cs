using UiTests.Helpers;
using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;
using TestFramework.Extensions;
using TestFramework.Helpers;

namespace UiTests.Pages
{
    public class LoginModal(IDriverWait driver) : ILoginModal
    {

        #region Element Locators
        private static By LogInBy => By.XPath("//button[text()='Log in']");
        private IWebElement EmailAddressField => driver.FindElement(By.CssSelector("input[name='email']"));
        private IWebElement ContinueWithEmailButton => driver.FindElement(By.CssSelector("button[type='submit']"));
        private IWebElement PasswordField => driver.FindElement(By.CssSelector("input[name='password']"));
        private IWebElement LogInButton => driver.FindElement(LogInBy);
        #endregion

        #region Public methods
        public void Login(User user)
        {
            EnterEmailAddress(user.Email);
            ClickContinue();
            string? password = user.Password ?? EncryptionUtil.DecryptString(user.EncryptedPassword!, user.KeyFilePath!);
            EnterPassword(password);
            ClickLogin();
        }
        #endregion

        #region Private methods
        private void EnterEmailAddress(string input) => EmailAddressField.ClearAndEnterText(input);
        private void ClickContinue() => ContinueWithEmailButton.Click();
        private void EnterPassword(string input) => PasswordField.ClearAndEnterText(input);
        private void ClickLogin()
        {
            LogInButton.Click();
            driver.FindElement(LogInBy, DriverWait.WaitCondition.Invisible);
        }
        #endregion
    }
}
