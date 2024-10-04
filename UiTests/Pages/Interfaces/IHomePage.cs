using UiTests.Helpers;

namespace UiTests.Pages.Interfaces
{
    public interface IHomePage
    {
        void ClickCalendar();
        void ClickCheckAvailability();
        void ClickGroupSize();
        void ClickLogin();
        void ClickNextMonthArrow();
        void EnterDestination(string input);
        string SelectDayInCalendar(Enums.DateOption dateOption, DateTime? date = null, int monthToCheck = 3);
        void SelectDestinationDropdownByExactText(string input);
        void SelectDestinationDropdownByOptionByPosition(Enums.Option position);
        void SelectPeopleCount(Enums.PersonType type, int count);
    }
}