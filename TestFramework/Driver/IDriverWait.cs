using OpenQA.Selenium;

namespace TestFramework.Driver
{
    public interface IDriverWait
    {
        IWebElement FindElementByLocator(By elementLocator);
        IWebElement FindElement(By elementLocator, DriverWait.WaitCondition condition = DriverWait.WaitCondition.Clickable, int timeoutInSeconds = 10);
        IReadOnlyCollection<IWebElement> FindElements(By elementLocator, DriverWait.WaitCondition condition = DriverWait.WaitCondition.Clickable, int timeoutInSeconds = 10);
    }
}