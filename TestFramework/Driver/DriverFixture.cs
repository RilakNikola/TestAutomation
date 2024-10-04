using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using TestFramework.Config;
using TestFramework.Helpers;

namespace TestFramework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private readonly TestSettings _testSettings;
    public IWebDriver Driver { get; }
    private bool _disposed = false;

    public DriverFixture(TestSettings testSettings)
    {
        _testSettings = testSettings;
        ChromeOptions options = new();
        //Extension to handle browser authentication
        options.AddArgument($"--load-extension=C:\\Users\\Public\\http-auto-auth-develop");
        Driver = GetWebDriver(options);
        Driver.Manage().Window.Maximize();
        string? password = _testSettings.AccessCredentials!.Pass;
        password ??= EncryptionUtil.DecryptString(_testSettings.AccessCredentials.EncryptedPassword!, _testSettings.AccessCredentials.KeyFilePath!);
        string url = $"{_testSettings.ApplicationUrl}?credentials={_testSettings.AccessCredentials.Username}:{password}";
        Driver.Navigate().GoToUrl(url);
    }

    private IWebDriver GetWebDriver(ChromeOptions options)
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => new ChromeDriver(options),
            BrowserType.Firefox => new FirefoxDriver(),
            BrowserType.Safari => new SafariDriver(),
            _ => new ChromeDriver()
        };
    }

    public void SwitchToNewWindow()
    {
        string originalWindow = Driver.CurrentWindowHandle;
        var allWindows = Driver.WindowHandles;
        foreach (var window in allWindows)
        {
            if (window != originalWindow)
            {
                Driver.SwitchTo().Window(window);
                break;
            }
        }
    }

    //public void Dispose() => Driver.Quit();
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // Dispose managed resources here
                Driver?.Quit(); // Ensure Driver is disposed
            }
            // Dispose unmanaged resources here if any

            _disposed = true;
        }
    }

    ~DriverFixture()
    {
        Dispose(false);
    }
}

public enum BrowserType
{
    Chrome,
    Firefox,
    Safari,
    EdgeChromium
}
