using UiTests.Pages.Interfaces;
using OpenQA.Selenium;
using TestFramework.Driver;
using TestFramework.Extensions;
using static UiTests.Helpers.Enums;

namespace UiTests.Pages;

public class HomePage(IDriverWait driver) : IHomePage
{

    #region Element locators
    private static readonly By FirstNotDisabledDay = By.XPath("//tbody[@class='rdp-tbody']//button[not(@disabled)]");
    private static readonly By DestinationFieldX = By.CssSelector("span[data-testid='search-form-input-clear-button']");
    private static readonly By NextMonthArrow = By.CssSelector("button[data-testid='search-form-next-month-button']");
    private IWebElement LogInButton => driver.FindElement(By.CssSelector("a[data-testid='login-button-desktop']"));
    private IWebElement DestinationField => driver.FindElement(By.CssSelector("input[data-testid='search-form-input-field']"));
    private IWebElement DestinationFieldXButton => driver.FindElement(DestinationFieldX);
    private IWebElement Calendar => driver.FindElement(By.Id("date-picker"));
    private IWebElement NextMonthArrowButton => driver.FindElement(NextMonthArrow);
    private IWebElement GroupSize => driver.FindElement(By.Id("group-size-picker-input"));
    private IWebElement AdultsPlus => driver.FindElement(By.Id("adults-plus"));
    private IWebElement AdultsMinus => driver.FindElement(By.Id("adults-minus"));
    private IWebElement ChildrenPlus => driver.FindElement(By.Id("children-plus"));
    private IWebElement ChildrenMinus => driver.FindElement(By.Id("children-minus"));
    private IWebElement CheckAvailability => driver.FindElement(By.CssSelector("a[data-testid='search-form-check-availability-button']"));
    private IWebElement CurrentMonthText => driver.FindElement(By.CssSelector("strong[data-testid='date_picker_current_month_and_year']"));
    private IWebElement DestinationDropdownSuggestion(int index) => driver.FindElement(By.CssSelector($"div[data-testid='search-form-suggestion-{index}']"));
    private IWebElement DestinationDropdownSuggestion(string input) => driver.FindElement(By.XPath($"//div[@data-testid='search-form-search-results-dropdown']//span[text()='{input}']"));
    private IWebElement DayInCalendar(int day) => driver.FindElement(By.XPath($"//button[@data-day='{day}' and not(contains(@class, 'rdp-day_outside'))]"));
    private IWebElement FirstAvailableDay(int maxMonthsToCheck = 3)
    {
        //Looking for the first not disabled day in month. If no days available it will go to next month
        for (int i = 0; i < maxMonthsToCheck; i++)
        {
            try
            {
                return driver.FindElement(FirstNotDisabledDay);
            }
            catch (WebDriverTimeoutException)
            {
                ClickNextMonthArrow();
            }
        }
        throw new NoSuchElementException($"No available days found in the next {maxMonthsToCheck} months.");
    }
    #endregion

    #region Public methods
    public void ClickLogin() => LogInButton.Click();
    public void ClickCheckAvailability() => CheckAvailability.Click();
    public void ClickCalendar() => Calendar.Click();
    public void ClickNextMonthArrow() => NextMonthArrowButton.Click();
    public void ClickGroupSize() => GroupSize.Click();
    public void SelectDestinationDropdownByOptionByPosition(Option position) => DestinationDropdownSuggestion((int)position).Click();
    public void SelectDestinationDropdownByExactText(string input) => DestinationDropdownSuggestion(input).Click();

    public void EnterDestination(string input)
    {
        //Looking if X button is present to clear the input area. --Clear text was not reliable for this field
        if (IsElementPresent(DestinationFieldX))
            DestinationFieldXButton.Click();
        DestinationField.ClearAndEnterText(input);
    }

    public string SelectDayInCalendar(DateOption dateOption, DateTime? date = null, int monthsToCheck = 3)
    {
        switch (dateOption)
        {
            case DateOption.FirstAvailable:
                SelectFirstAvailableDay(monthsToCheck);
                break;
            case DateOption.SpecificDay:
                if (!date.HasValue)
                    throw new ArgumentException("Date parameter is required for Specific Day option.");
                if (date.Value.Date < DateTime.Today)
                    throw new ArgumentException("Date parameter can't be in past.");
                SelectMonth(date.Value);
                SelectDay(date.Value.Day);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dateOption), dateOption, "Invalid day option.");
        }
        return Calendar.GetAttribute("value");
    }

    public void SelectPeopleCount(PersonType type, int count)
    {
        //Passing person type (Adult or Child) and wanted count
        //Getting current count present on the website and alligning it with wanted count by pressing plus and minus
        int currentCount = type == PersonType.Adult ? GetAdultsCount() : GetChildrenCount();
        if (currentCount < count)
        {
            while (currentCount < count)
            {
                if (type == PersonType.Adult)
                {
                    ClickAdultsPlus();
                    currentCount = GetAdultsCount();
                }
                else if (type == PersonType.Chilrden)
                {
                    ClickChildrenPlus();
                    currentCount = GetChildrenCount();
                }
            }
        }
        else if (currentCount > count)
        {
            while (currentCount > count)
            {
                if (type == PersonType.Adult)
                {
                    ClickAdultsMinus();
                    currentCount = GetAdultsCount();
                }
                else if (type == PersonType.Chilrden)
                {
                    ClickChildrenMinus();
                    currentCount = GetChildrenCount();
                }
            }
        }
    }
    #endregion

    #region Private methods
    private void SelectDay(int day) => DayInCalendar(day).Click();
    private void SelectMonth(DateTime date)
    {
        string desiredMonthName = date.ToString("MMMM");
        string desiredYear = date.Year.ToString();
        string monthYear = $"{desiredMonthName} {desiredYear}";
        while (GetSelectedMonthText() != monthYear)
            driver.FindElement(NextMonthArrow).Click();
    }
    private void ClickAdultsPlus() => AdultsPlus.Click();
    private void ClickAdultsMinus() => AdultsMinus.Click();
    private void ClickChildrenPlus() => ChildrenPlus.Click();
    private void ClickChildrenMinus() => ChildrenMinus.Click();
    private int GetAdultsCount() => Convert.ToInt32(AdultsPlus.GetAttribute("count"));
    private int GetChildrenCount() => Convert.ToInt32(ChildrenPlus.GetAttribute("count"));
    private string GetSelectedMonthText() => CurrentMonthText.Text;
    private void SelectFirstAvailableDay(int maxMonthsToCheck)
    {
        //Checking if first available day is already selected. If it is - do nothing, if it's not - select it
        string isSelected = FirstAvailableDay(maxMonthsToCheck).GetAttribute("aria-selected");
        if (isSelected != "true")
            FirstAvailableDay(maxMonthsToCheck).Click();
    }
    private bool IsElementPresent(By locator)
    {
        try
        {
            driver.FindElement(locator, DriverWait.WaitCondition.Exists, 1);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
        catch (WebDriverTimeoutException)
        {
            return false;
        }
    }
    #endregion
}