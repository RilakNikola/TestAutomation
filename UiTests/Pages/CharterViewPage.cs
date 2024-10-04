using UiTests.Helpers;
using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;

namespace UiTests.Pages
{
    public class CharterViewPage(IDriverWait driver, IActionsService actionsService) : ICharterViewPage
    {
        private IWebElement MessageCaptainButton => driver.FindElement(By.Id("contact-captain"));
        public void ClickMessageCaptain() => actionsService.ClickElement(MessageCaptainButton);
    }
}
