using OpenQA.Selenium;

namespace UiTests.Helpers
{
    public interface IActionsService
    {
        void ClickElement(IWebElement element);
        void MoveToElement(IWebElement element);
    }
}