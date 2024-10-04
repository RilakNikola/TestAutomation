using UiTests.Helpers;
using UiTests.Managers.Interfaces;
using UiTests.Pages.Interfaces;
using TestFramework.Helpers;
using static UiTests.Helpers.Enums;

namespace UiTests.Managers
{
    public class SearchManager(
        ISearchResultsFiltersModal searchResultsFiltersModal,
        ISearchResultsPage searchResultsPage,
        IDriverUtilities driverUtilities,
        IHomePage homePage) : ISearchManager
    {
        private readonly ISearchResultsFiltersModal _searchResultsFiltersModal = searchResultsFiltersModal;
        private readonly ISearchResultsPage _searchResultsPage = searchResultsPage;
        private readonly IHomePage _homePage = homePage;
        private readonly IDriverUtilities _driverUtilities = driverUtilities;

        public void SelectFilters(FiltersData filtersData)
        {
            _searchResultsPage.ClickFilters();
            _searchResultsFiltersModal.ClickClearAllButton();

            if (filtersData.ReviewScore != null)
                _searchResultsFiltersModal.SelectReviewScore(filtersData.ReviewScore.Value);

            if (filtersData.FishingType != null)
                _searchResultsFiltersModal.SelectFishingType(filtersData.FishingType.Value);

            if (filtersData.TargetedSpecies != null)
                _searchResultsFiltersModal.SelectTargetedSpecies(filtersData.TargetedSpecies.Value);

            _searchResultsFiltersModal.ClickShowChartersButton();
            _searchResultsPage.WaitForSearchLoaderToBecomeInvisible();
        }

        public void SelectResult(ResultsData resultsData)
        {
            string title = "";
            if (resultsData.Sticker!.Value == Stickers.AnglersChoice)
                title = _searchResultsPage.ClickAnglersChoiceResult(resultsData.Option!.Value);

            _driverUtilities.SwitchToNewWindow();
            Assert.Equal(title.Trim(), _searchResultsPage.GetCharterTitle().Trim());
        }

        public void FillAndSubmitSearchForm(SearchFormData searchForm)
        {
            _homePage.EnterDestination(searchForm.Destination);
            _homePage.SelectDestinationDropdownByExactText(searchForm.Destination);
            _homePage.ClickCalendar();
            string selectedDate = _homePage.SelectDayInCalendar(searchForm.DateOption!.Value, searchForm.Date);
            _homePage.ClickGroupSize();
            _homePage.SelectPeopleCount(PersonType.Adult, searchForm.NumberOfAdults);
            _homePage.SelectPeopleCount(PersonType.Chilrden, searchForm.NumberOfChildren);
            _homePage.ClickCheckAvailability();
            CheckIfCorrectDataIsSent(searchForm.Destination, selectedDate, searchForm.NumberOfAdults, searchForm.NumberOfChildren);
            _searchResultsPage.WaitForWeatherContainerToDissappear();
            _searchResultsPage.ClickNotificationXButtonIfExists();
        }

        private void CheckIfCorrectDataIsSent(string destination, string selectedDate, int adultsCount, int childrenCount)
        {
            Assert.Contains(_searchResultsPage.GetSearchInput(), destination, StringComparison.OrdinalIgnoreCase);
            Assert.Equal(selectedDate, _searchResultsPage.GetSelectedDate());
            Assert.Equal($"{adultsCount} adults ⋅ {childrenCount} children", _searchResultsPage.GetPeopleCount());
        }
    }
}
