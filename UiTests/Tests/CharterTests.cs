using UiTests.Helpers;
using UiTests.Managers.Interfaces;

namespace UiTests.Tests
{
    public class CharterTests(ILoginManager loginManager, ISearchManager searchResultsManager, ICharterViewManager charterViewManager)
    {
        private readonly User _user = TestDataMethods.GetUserById(1);
        private readonly SearchFormData _searchFormData = TestDataMethods.GetSearchFormDataById(1);
        private readonly FiltersData _filtersData = TestDataMethods.GetFiltersDataById(1);
        private readonly MessageCaptainData _messageCaptainData = TestDataMethods.GetMessageCaptainDataById(1);
        private readonly ResultsData _resultData = TestDataMethods.GetResultsDataById(1);

        [Fact]
        public void SendCharterInquiry()
        {
            loginManager.Login(_user);
            searchResultsManager.FillAndSubmitSearchForm(_searchFormData);
            searchResultsManager.SelectFilters(_filtersData);
            searchResultsManager.SelectResult(_resultData);
            charterViewManager.MessageCaptain(_messageCaptainData);
        }
    }
}