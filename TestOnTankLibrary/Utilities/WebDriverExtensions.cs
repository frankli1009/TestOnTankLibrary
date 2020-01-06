using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestOnTankLibrary.Domain;

namespace TestOnTankLibrary.Utilities
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, ElementLocation location)
        {
            if (location == null)
            {
                throw new InvalidElementLocationException("Element location is null.");
            }
            if (string.IsNullOrWhiteSpace(location.Value))
            {
                throw new InvalidElementLocationException("The value of element location is null or empty.");
            }
            switch(location.LocationType)
            {
                case ElementLocationType.Id:
                    return driver.FindElement(By.Id(location.Value));
                case ElementLocationType.Name:
                    return driver.FindElement(By.Name(location.Value));
                case ElementLocationType.ClassName:
                    return driver.FindElement(By.ClassName(location.Value));
                case ElementLocationType.TagName:
                    return driver.FindElement(By.TagName(location.Value));
                case ElementLocationType.CssSelector:
                    return driver.FindElement(By.CssSelector(location.Value));
                case ElementLocationType.LinkText:
                    return driver.FindElement(By.LinkText(location.Value));
                case ElementLocationType.PartialLinkText:
                    return driver.FindElement(By.PartialLinkText(location.Value));
                case ElementLocationType.XPath:
                    return driver.FindElement(By.XPath(location.Value));
                default:
                    throw new InvalidElementLocationException("Invalid type of element location.");
            }
        }

        public static IReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, ElementLocation location)
        {
            if (location == null)
            {
                throw new InvalidElementLocationException("Element location is null.");
            }
            if (string.IsNullOrWhiteSpace(location.Value))
            {
                throw new InvalidElementLocationException("The value of element location is null or empty.");
            }
            switch (location.LocationType)
            {
                case ElementLocationType.Id:
                    return driver.FindElements(By.Id(location.Value));
                case ElementLocationType.Name:
                    return driver.FindElements(By.Name(location.Value));
                case ElementLocationType.ClassName:
                    return driver.FindElements(By.ClassName(location.Value));
                case ElementLocationType.TagName:
                    return driver.FindElements(By.TagName(location.Value));
                case ElementLocationType.CssSelector:
                    return driver.FindElements(By.CssSelector(location.Value));
                case ElementLocationType.LinkText:
                    return driver.FindElements(By.LinkText(location.Value));
                case ElementLocationType.PartialLinkText:
                    return driver.FindElements(By.PartialLinkText(location.Value));
                case ElementLocationType.XPath:
                    return driver.FindElements(By.XPath(location.Value));
                default:
                    throw new InvalidElementLocationException("Invalid type of element location.");
            }
        }
    }
}
