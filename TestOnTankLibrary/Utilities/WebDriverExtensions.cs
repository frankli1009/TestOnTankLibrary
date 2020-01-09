using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TestOnTankLibrary.Domain;

namespace TestOnTankLibrary.Utilities
{
    public static class WebDriverExtensions
    {
        /// <summary>
        /// Find an element by ElementLocation object
        /// </summary>
        /// <param name="driver">The IWebDriver to find the element.</param>
        /// <param name="location">The element location.</param>
        /// <returns>The element found.</returns>
        public static IWebElement FindElement(this IWebDriver driver, ElementLocation location, int timeout = 0)
        {
            if (location == null)
            {
                throw new InvalidCustomDataException("Element location is null.");
            }
            if (string.IsNullOrWhiteSpace(location.Value))
            {
                throw new InvalidCustomDataException("The value of element location is null or empty.");
            }
            switch(location.LocationType)
            {
                case ElementLocationType.Id:
                    return timeout == 0 ? driver.FindElement(By.Id(location.Value))
                        : By.Id(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.Name:
                    return timeout == 0 ? driver.FindElement(By.Name(location.Value))
                        : By.Name(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.ClassName:
                    return timeout == 0 ? driver.FindElement(By.ClassName(location.Value))
                        : By.ClassName(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.TagName:
                    return timeout == 0 ? driver.FindElement(By.TagName(location.Value))
                        : By.TagName(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.CssSelector:
                    return timeout == 0 ? driver.FindElement(By.CssSelector(location.Value))
                        : By.CssSelector(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.LinkText:
                    return timeout == 0 ? driver.FindElement(By.LinkText(location.Value))
                        : By.LinkText(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.PartialLinkText:
                    return timeout == 0 ? driver.FindElement(By.PartialLinkText(location.Value))
                        : By.PartialLinkText(location.Value).WaitElement(driver, timeout);
                case ElementLocationType.XPath:
                    return timeout == 0 ? driver.FindElement(By.XPath(location.Value))
                        : By.XPath(location.Value).WaitElement(driver, timeout);
                default:
                    throw new InvalidCustomDataException("Invalid type of element location.");
            }
        }

        /// <summary>
        /// Find ReadOnlyCollection of elements by ElementLocation object
        /// </summary>
        /// <param name="driver">The IWebDirver to find the elements.</param>
        /// <param name="location">The location of the elements.</param>
        /// <returns>ReadOnlyCollection of elements found.</returns>
        public static IReadOnlyCollection<IWebElement> FindElements(this IWebDriver driver, ElementLocation location)
        {
            if (location == null)
            {
                throw new InvalidCustomDataException("Element location is null.");
            }
            if (string.IsNullOrWhiteSpace(location.Value))
            {
                throw new InvalidCustomDataException("The value of element location is null or empty.");
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
                    throw new InvalidCustomDataException("Invalid type of element location.");
            }
        }

        /// <summary>
        /// Find the first element if exists
        /// </summary>
        /// <param name="driver">The IWebDirver to find the element.</param>
        /// <param name="by">The way to find the element.</param>
        /// <returns>The element that found or null if not exists.</returns>
        public static IWebElement FindElementIfExists(this IWebDriver driver, By by)
        {
            var elements = driver.FindElements(by);
            return (elements.Count >= 1) ? elements.First() : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver">The IWebDirver to find the element.</param>
        /// <param name="location">The element location.</param>
        /// <returns>The element that found or null if not exists.</returns>
        public static IWebElement FindElementIfExists(this IWebDriver driver, ElementLocation location)
        {
            if (location == null)
            {
                throw new InvalidCustomDataException("Element location is null.");
            }
            if (string.IsNullOrWhiteSpace(location.Value))
            {
                throw new InvalidCustomDataException("The value of element location is null or empty.");
            }
            switch (location.LocationType)
            {
                case ElementLocationType.Id:
                    return driver.FindElementIfExists(By.Id(location.Value));
                case ElementLocationType.Name:
                    return driver.FindElementIfExists(By.Name(location.Value));
                case ElementLocationType.ClassName:
                    return driver.FindElementIfExists(By.ClassName(location.Value));
                case ElementLocationType.TagName:
                    return driver.FindElementIfExists(By.TagName(location.Value));
                case ElementLocationType.CssSelector:
                    return driver.FindElementIfExists(By.CssSelector(location.Value));
                case ElementLocationType.LinkText:
                    return driver.FindElementIfExists(By.LinkText(location.Value));
                case ElementLocationType.PartialLinkText:
                    return driver.FindElementIfExists(By.PartialLinkText(location.Value));
                case ElementLocationType.XPath:
                    return driver.FindElementIfExists(By.XPath(location.Value));
                default:
                    throw new InvalidCustomDataException("Invalid type of element location.");
            }
        }

    }
}
