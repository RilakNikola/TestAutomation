using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;

namespace UiTests.Pages
{
    public class MessageSentModal(IDriverWait driver) : IMessageSentModal
    {
        private IWebElement MessageSent => driver.FindElement(By.XPath("//b[text()='Message Sent!']"), DriverWait.WaitCondition.Visible);
        public string GetMessageSentText() => MessageSent.Text;
    }
}
