using static UiTests.Helpers.Enums;

namespace UiTests.Pages.Interfaces
{
    public interface ISearchResultsPage
    {
        string ClickAnglersChoiceResult(Option index);
        string GetCharterTitle();
        void ClickFilters();
        void ClickNotificationXButtonIfExists();
        string GetSelectedDate();
        string GetPeopleCount();
        string GetSearchInput();
        void WaitForSearchLoaderToBecomeInvisible();
        void WaitForWeatherContainerToDissappear();
    }
}