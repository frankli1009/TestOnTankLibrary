using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestOnTankLibrary.Domain;

namespace TestOnTankLibrary.Utilities
{
    public static class ByExtensions
    {
        public static IWebElement WaitElement(this By by, IWebDriver driver,
            int timeout = 10, bool throwException = true,
            Action<string> logError = null)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            }
            catch (WebDriverTimeoutException te)
            {
                if (logError != null)
                {
                    logError("Timeout. Element with locator: " + by + " was not found in current context page.");
                    logError(te.Message);
                    logError(te.StackTrace);
                }
                if (throwException) throw;
                else return null;
            }
            catch (Exception e)
            {
                if (logError != null)
                {
                    logError("Failed to find element with locator: " + by);
                    logError(e.Message);
                    logError(e.StackTrace);
                }
                if (throwException) throw;
                else return null;
            }
        }

        public static bool ElementTextChanged(this By by, IWebDriver driver,
            string prevText, int timeout = 10, bool throwException = true,
            Action<string> logError = null)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementWithText(by, prevText));
                return true;
            }
            catch (WebDriverTimeoutException te)
            {
                if (logError != null)
                {
                    logError("Timeout. Element with locator: " + by + " was not found in current context page.");
                    logError(te.Message);
                    logError(te.StackTrace);
                }
                if (throwException) throw;
                else return false;
            }
            catch (Exception e)
            {
                if (logError != null)
                {
                    logError("Failed to find element with locator: " + by);
                    logError(e.Message);
                    logError(e.StackTrace);
                }
                if (throwException) throw;
                else return false;
            }
        }

        public static By ElementLocation(ElementLocation location)
        {
            switch (location.LocationType)
            {
                case ElementLocationType.Id:
                    return By.Id(location.Value);
                case ElementLocationType.Name:
                    return By.Name(location.Value);
                case ElementLocationType.ClassName:
                    return By.ClassName(location.Value);
                case ElementLocationType.TagName:
                    return By.TagName(location.Value);
                case ElementLocationType.CssSelector:
                    return By.CssSelector(location.Value);
                case ElementLocationType.LinkText:
                    return By.LinkText(location.Value);
                case ElementLocationType.PartialLinkText:
                    return By.PartialLinkText(location.Value);
                case ElementLocationType.XPath:
                    return By.XPath(location.Value);
                default:
                    throw new InvalidCustomDataException("Invalid type of element location.");
            }
        }
    }
}
