using static UiTests.Helpers.Enums;

namespace UiTests.Pages.Interfaces
{
    public interface ISendMessageModal
    {
        void ClickCalendar();
        void ClickCreateNewInquiry();
        void ClickSendMessage();
        void SelectDay(DayInTheMonth dayInTheMonth, int monthsToCheck = 6);
        void SelectGroupSize(GroupSize groupSize);
        void SelectTrip(TripOptions trip);
        void TypeMessage(string message);
    }
}