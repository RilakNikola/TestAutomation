using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Extensions;

public static class WebElementExtension
{
    public static void SelectDropDownByText(this IWebElement element, string text)
    {
        var select = new SelectElement(element);
        select.SelectByText(text);
    }

    public static void SelectDropDownByValue(this IWebElement element, string value)
    {
        var select = new SelectElement(element);
        select.SelectByValue(value);
    }

    public static void SelectDropDownByIndex(this IWebElement element, int index)
    {
        var select = new SelectElement(element);
        select.SelectByIndex(index);
    }

    public static void SelectDropDownFirst(this IWebElement element)
    {
        var select = new SelectElement(element);
        select.SelectByIndex(0);
    }

    public static void SelectDropDownLast(this IWebElement element)
    {
        var select = new SelectElement(element);
        select.SelectByIndex(select.Options.Count - 1);
    }

    public static void ClearAndEnterText(this IWebElement element, string value)
    {
        element.Clear();
        element.SendKeys(value);
    }
}