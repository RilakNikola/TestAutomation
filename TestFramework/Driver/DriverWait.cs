using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using TestFramework.Config;

namespace TestFramework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverFixture _driverFixture;
    private readonly TestSettings _testSettings;
    private readonly Lazy<WebDriverWait> _webDriverWait;

    public DriverWait(IDriverFixture driverFixture, TestSettings testSettings)
    {
        _driverFixture = driverFixture;
        _testSettings = testSettings;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
    }

    public IWebElement FindElementByLocator(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverFixture.Driver.FindElement(elementLocator));
    }

    public IWebElement FindElement(By elementLocator, WaitCondition condition = WaitCondition.Clickable, int timeoutInSeconds = 15)
    {
        WebDriverWait wait = new(_driverFixture.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

        switch (condition)
        {
            case WaitCondition.Visible:
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator));

            case WaitCondition.Clickable:
                return wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator));

            case WaitCondition.Exists:
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));

            case WaitCondition.Invisible:
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementLocator));
                return null;

            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }

    public IReadOnlyCollection<IWebElement> FindElements(By elementLocator, WaitCondition condition = WaitCondition.Clickable, int timeoutInSeconds = 10)
    {
        WebDriverWait wait = new(_driverFixture.Driver, TimeSpan.FromSeconds(timeoutInSeconds));

        switch (condition)
        {
            case WaitCondition.Visible:
                return wait.Until(driver =>
                {
                    var elements = driver.FindElements(elementLocator);
                    return elements.Where(e => e.Displayed).ToList();
                });

            case WaitCondition.Clickable:
                return wait.Until(driver =>
                {
                    var elements = driver.FindElements(elementLocator);
                    return elements.Where(e => e.Displayed && e.Enabled).ToList();
                });

            case WaitCondition.Exists:
                return wait.Until(driver =>
                {
                    var elements = driver.FindElements(elementLocator);
                    return elements.Count > 0 ? elements : null;
                });

            case WaitCondition.Invisible:
                wait.Until(driver =>
                {
                    var elements = driver.FindElements(elementLocator);
                    return elements.All(e => !e.Displayed) ? elements : null;
                });
                return [];

            default:
                throw new ArgumentOutOfRangeException(nameof(condition), condition, null);
        }
    }

    private WebDriverWait GetWaitDriver()
    {
        return new(_driverFixture.Driver, timeout: TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 30))
        {
            PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 1)
        };
    }

    public enum WaitCondition
    {
        Visible,
        Clickable,
        Invisible,
        Exists
    }
}