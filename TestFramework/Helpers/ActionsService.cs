using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.Driver;

namespace FishingBookerTest.Helpers
{
    public class ActionsService : IActionsService
    {
        private readonly IWebDriver _driver;
        private readonly Actions _actions;

        public ActionsService(IDriverFixture driverFixture)
        {
            _driver = driverFixture.Driver;
            _actions = new Actions(_driver);
        }

        public void MoveToElement(IWebElement element)
        {
            _actions.MoveToElement(element).Perform();
        }

        public void ClickElement(IWebElement element)
        {
            _actions.MoveToElement(element).Click().Perform();
        }
    }
}
