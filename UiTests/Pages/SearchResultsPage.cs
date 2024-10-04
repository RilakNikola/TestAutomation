using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;
using static UiTests.Helpers.Enums;

namespace UiTests.Pages
{
    public class SearchResultsPage(IDriverWait driver) : ISearchResultsPage
    {

        #region Element locators
        private static By NotificationX => By.XPath("//div[@id='pageViewsDestinationAlert']/button");
        private static By SearchResultsLoader => By.Id("search-result-loader");
        private IWebElement CharterTitle => driver.FindElement(By.CssSelector("h1[class='headline']"), DriverWait.WaitCondition.Visible);
        private IWebElement SearchInput => driver.FindElement(By.Id("charterpageSearch-input"), DriverWait.WaitCondition.Visible);
        private IWebElement BookingDate => driver.FindElement(By.Id("search_booking_date"), DriverWait.WaitCondition.Visible);
        private IWebElement PeopleCount => driver.FindElement(By.XPath("//div[@class='search-form-item']//div[@class='search-form-persons']/input[1]"), DriverWait.WaitCondition.Visible);
        private IWebElement WeatherContainer => driver.FindElement(By.Id("weather-container"), DriverWait.WaitCondition.Visible, 0);
        private IWebElement FiltersButton => driver.FindElement(By.Id("search-results-modal-btn-container"));
        private IWebElement PaginationNext => driver.FindElement(By.CssSelector("a[data-event-label='Next']"));
        private IEnumerable<IWebElement> Pagination => driver.FindElements(By.XPath("//div[@id='pagination']//div[@class='page-box']"));
        private IEnumerable<IWebElement> ResultsWithAnglersChoiceSticker => driver.FindElements(By.XPath
            ("//div[@class='listing-card-anglers-choice-header']/parent::div/parent::div/following-sibling::div//a[@data-event-label='Charter title']"));
        private IWebElement AnglersChoiceResult(Option index)
        {
            if ((int)index > ResultsWithAnglersChoiceSticker.Count())
            {
                throw new Exception($"Number of results with anglers choice sticker => {ResultsWithAnglersChoiceSticker.Count()} is lower than index => {index}");
            }
            int lastPaginationNumber = GetLastPaginationNumber(Pagination);
            int i = 0;
            do
            {
                try
                {
                    return ResultsWithAnglersChoiceSticker.ToList()[(int)index];
                }
                catch (NoSuchElementException)
                {
                    ClickPaginationNext();
                }
                catch (WebDriverTimeoutException)
                {
                    ClickPaginationNext();
                }
                catch (NullReferenceException)
                {
                    ClickPaginationNext();
                }
                i++;
            }
            while (i < lastPaginationNumber - 1);

            throw new Exception("No element found");
        }
        #endregion

        #region Public methods
        public string GetCharterTitle() => CharterTitle.Text;
        public string GetSearchInput() => SearchInput.GetAttribute("value");
        public string GetSelectedDate() => BookingDate.GetAttribute("value");
        public string GetPeopleCount() => PeopleCount.GetAttribute("placeholder");
        public void ClickFilters() => FiltersButton.Click();
        private void ClickPaginationNext() => PaginationNext.Click();
        public string ClickAnglersChoiceResult(Option index)
        {
            AnglersChoiceResult(index).Click();
            return AnglersChoiceResult(index).GetAttribute("title");
        }

        public void WaitForSearchLoaderToBecomeInvisible() =>
            driver.FindElement(SearchResultsLoader, DriverWait.WaitCondition.Invisible, 20);

        public void WaitForWeatherContainerToDissappear()
        {
            try
            {
                while (WeatherContainer.GetAttribute("style") != "display: none;")
                    WaitForWeatherContainerToDissappear();
            }
            catch (Exception)
            {
            }
        }

        public void ClickNotificationXButtonIfExists()
        {
            try
            {
                driver.FindElement(NotificationX, timeoutInSeconds: 0).Click();
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Private methods
        private static int GetLastPaginationNumber(IEnumerable<IWebElement> pagination)
        {
            int lastPageNumber = 0;
            foreach (var page in pagination)
            {
                int pageNumber = Convert.ToInt32(page.Text.Trim());
                lastPageNumber = (pageNumber > lastPageNumber) ? pageNumber : lastPageNumber;
            }

            return lastPageNumber;
        }
        #endregion
    }
}
