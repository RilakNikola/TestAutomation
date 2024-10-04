using TestFramework.Driver;

namespace TestFramework.Helpers
{
    public class DriverUtilities(IDriverFixture driver) : IDriverUtilities
    {
        public void SwitchToNewWindow()
        {
            var newWindowHandle = driver.Driver.WindowHandles.Last();
            driver.Driver.SwitchTo().Window(newWindowHandle);
        }
    }
}
