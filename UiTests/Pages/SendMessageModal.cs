using UiTests.Helpers;
using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;
using TestFramework.Extensions;
using static UiTests.Helpers.Enums;

namespace UiTests.Pages
{
    public class SendMessageModal(IDriverWait driver, IActionsService actionsService) : ISendMessageModal
    {

        #region Element locators
        private static By AlertContainer => By.XPath("//div[@class='modal-content']//div[@class='fbkr-alert-container']");
        private static By CurrentMonthDaysBy => By.XPath("//td[contains(@class, 'rdtDay') and not(contains(@class, 'rdtNew')) and not(contains(@class, 'rdtOld'))]");
        private IWebElement CreateNewInquiry => driver.FindElement(By.XPath("//button[text()='Create new inquiry']"), DriverWait.WaitCondition.Clickable, 5);
        private IWebElement Calendar => driver.FindElement(By.Id("cf-trip-date"));
        private IWebElement NextMonthButton => driver.FindElement(By.CssSelector("th[class='rdtNext']"));
        private IWebElement GroupSizeDropdown => driver.FindElement(By.Id("cf-group-size"));
        private IWebElement TripDropdown => driver.FindElement(By.Id("cf-packages"));
        private IWebElement MessageArea => driver.FindElement(By.Id("contact-textarea"), DriverWait.WaitCondition.Visible);
        private IWebElement SendMessageButton => driver.FindElement(By.XPath("//button[text()='Send Message']"));
        #endregion

        #region Public methods
        public void TypeMessage(string message)
        {
            MessageArea.Click();
            MessageArea.SendKeys(message);
        }

        public void ClickSendMessage() => SendMessageButton.Click();
        public void SelectGroupSize(GroupSize groupSize) => GroupSizeDropdown.SelectDropDownByIndex((int)groupSize - 1);

        public void SelectTrip(TripOptions trip)
        {
            if (trip == TripOptions.Last)
                TripDropdown.SelectDropDownLast();
            else
                TripDropdown.SelectDropDownByIndex((int)trip);
        }

        public void ClickCreateNewInquiry()
        {
            try
            {
                CreateNewInquiry.Click();
            }
            catch (Exception)
            {
            }
        }

        public void ClickCalendar()
        {
            //Waiting for alert container to possibly appear as if appears it breaks following actions
            try
            {
                driver.FindElement(AlertContainer, DriverWait.WaitCondition.Visible, 2);
                Calendar.Click();
            }
            catch (Exception)
            {
                Calendar.Click();
            }
        }

        public void SelectDay(DayInTheMonth dayInTheMonth, int monthsToCheck = 6)
        {
            if (dayInTheMonth == DayInTheMonth.Last)
            {
                var currentMonthDays = driver.FindElements(CurrentMonthDaysBy);
                for (int i = 0; i < monthsToCheck && currentMonthDays.LastOrDefault()!.GetAttribute("class").Contains("rdtDisabled"); i++)
                {
                    ClickNextMonthButton();
                    currentMonthDays = driver.FindElements(CurrentMonthDaysBy);
                }
                actionsService.ClickElement(currentMonthDays.LastOrDefault()!);
            }
        }
        #endregion

        #region Private methods
        private void ClickNextMonthButton() => actionsService.ClickElement(NextMonthButton);
        #endregion
    }
}
