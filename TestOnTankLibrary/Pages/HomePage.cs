using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestOnTankLibrary.Domain;
using TestOnTankLibrary.Utilities;

namespace TestOnTankLibrary.Pages
{
    [TestFixture]
    public class HomePage
    {
        private IWebDriver driver;
        private Settings settings;

        public HomePage()
        {
        }

        [SetUp]
        public void Setup()
        {
            settings = Settings.GetInstance();
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(settings.Urls.Find("Home").GetValue());
        }

        [TearDown]
        public void Teardown()
        {
            driver.Close();
        }

        [Test]
        public void TestAddTank()
        {
            //Arrange
            ElementLocation location = (ElementLocation)settings.Locations.Find("Home.AddTank.Add");
            IWebElement element = driver.FindElement(location);

            //Act
            if (element != null) element.Click();

            //Assert
            ElementLocation modalLocation = (ElementLocation)settings.Locations.Find("Home.AddTank.ResetModal");
            element = driver.FindElement(modalLocation);
            if (element.Displayed)
            {
                // Warning and urging to reset data means add button worked.

                // Added test for reset data here on a temporary basis to help reset data.
                location = (ElementLocation)settings.Locations.Find("Home.AddTank.ResetData");
                element = driver.FindElement(location);
                element.Click();

                location = (ElementLocation)settings.Locations.Find("Home.AddTank.CloseModal");
                element = driver.FindElement(location, 10);
                Assert.IsTrue(element.Displayed);
            }
            else
            {
                ExpectedSetting expected = (ExpectedSetting)settings.Expecteds.Find("Add.Title");
                string title = driver.Title;
                Assert.AreEqual(expected.Value, title, $"Expecting the title of Add page '{expected.Value}' but was '{title}'.");
            }
        }

        [TestCase("Home.Group.WWI")]
        [TestCase("Home.Group.Interwar")]
        [TestCase("Home.Group.WWII")]
        [TestCase("Home.Group.Modern")]
        public void TestClickOnSpecificGroup(string groupKey)
        {
            //Arrange
            ElementLocation location = (ElementLocation)settings.Locations.Find(groupKey);
            IWebElement element = driver.FindElement(location);

            //Act
            if (element != null) element.Click();

            //Assert
            ElementLocation expectedLocation = (ElementLocation)settings.Locations.Find("Home.List.Stages");
            IReadOnlyCollection<IWebElement> stages = driver.FindElements(expectedLocation);
            bool hasInvalidStages = stages.Any(s => s.Text != location.Value);
            Assert.IsFalse(hasInvalidStages, $"Expecting all stages to be '{groupKey}' but not.");
        }

        [Test]
        public void TestClickOnAllGroup()
        {
            //Arrange
            driver.Navigate().GoToUrl(settings.Urls.Find("Home.WWI").GetValue());
            ElementLocation location = (ElementLocation)settings.Locations.Find("Home.Group.All");
            IWebElement element = driver.FindElement(location);

            //Act
            if (element != null) element.Click();

            //Assert
            ElementLocation expectedLocation = (ElementLocation)settings.Locations.Find("Home.List.Stages");
            IReadOnlyCollection<IWebElement> stages = driver.FindElements(expectedLocation);
            bool hasInvalidStages = stages.Any(s => s.Text == location.Value);
            Assert.IsFalse(hasInvalidStages, $"Expecting no stages to be '{location.Value}' but not.");
            Assert.IsTrue(stages.Any(), $"Expecting data of '{location.Value}' but nothing.");
            int groupCount = stages.GroupBy(s => s.Text).Count();
            Assert.AreEqual(4, groupCount, $"Expecting all 4 group of stages but only {groupCount}.");
        }

        [TestCase(2, "Stage")]
        [TestCase(3, "Type")]
        public void TestSelectOnList(int selectRow, string clickCol)
        {
            //Arrange

            //Get expected tank name
            string nameLocKey = $"Home.List.All.Data.Name";
            ElementLocation location = (ElementLocation)settings.Locations.Find(nameLocKey).Clone(selectRow);
            IWebElement element = driver.FindElement(location);
            string nameOfTank = element.Text;

            //Get Col to click to change the selected row
            string clickColLocKey = $"Home.List.All.Data.{clickCol}";
            location = (ElementLocation)settings.Locations.Find(clickColLocKey).Clone(selectRow);
            element = driver.FindElement(location);

            //Get the name in the description area of the current selected tank (before changing row)
            ElementLocation expectedLocation = (ElementLocation)settings.Locations.Find("Home.Desc.Name");
            IWebElement expectedElement = driver.FindElement(expectedLocation);
            string prevNameOfTank = expectedElement.Text;

            //Act
            if (element != null) element.Click();

            //Assert

            //Wait for the name in the description area of the current selected tank to change (after changing row)
            ByExtensions.ElementLocation(expectedLocation).ElementTextChanged(driver, prevNameOfTank, 10);

            //Get the new tank name
            expectedElement = driver.FindElement(expectedLocation);
            string actualNameOfTank = expectedElement.Text;

            Assert.AreEqual(nameOfTank, actualNameOfTank, $"Expecting the name of current tank '{nameOfTank}' but was {actualNameOfTank}.");
        }

        [TestCase(2)]
        [TestCase(3)]
        public void TestDisplayingDetailPage(int selectRow)
        {
            //Arrange

            //Get expected tank name
            string nameLocKey = $"Home.List.All.Data.Name";
            ElementLocation location = (ElementLocation)settings.Locations.Find(nameLocKey).Clone(selectRow);
            IWebElement element = driver.FindElement(location);
            string nameOfTank = element.Text;

            //Act
            if (element != null) element.Click();

            //Assert

            //Wait for displaying detail page
            ElementLocation expectedLocation = (ElementLocation)settings.Locations.Find("Detail.Name");
            IWebElement expectedElement = driver.FindElement(expectedLocation, 10);
            string actualNameOfTank = expectedElement.Text;

            Assert.AreEqual(nameOfTank, actualNameOfTank, $"Expecting the name of current tank '{nameOfTank}' but was {actualNameOfTank}.");
        }

        [TestCase(2, "Edit")]
        [TestCase(3, "Edit")]
        [TestCase(2, "Delete")]
        [TestCase(3, "Delete")]
        public void TestDisplayingEditPage(int selectRow, string operation)
        {
            //Arrange

            //Navigate to page 2 which only has two rows of default tank data which cannot be edited or deleted
            driver.Navigate().GoToUrl(string.Format(settings.Urls.Find("Home.Page").GetValue(), 2));

            //Get expected tank name
            string nameLocKey = $"Home.List.All.Data.Name";
            ElementLocation location = (ElementLocation)settings.Locations.Find(nameLocKey).Clone(selectRow);
            IWebElement element = driver.FindElement(location);
            string nameOfTank = element.Text;

            //Get Edit button element
            string editBtnKey = $"Home.List.All.Data.Btn.{operation}";
            location = (ElementLocation)settings.Locations.Find(editBtnKey).Clone(selectRow);
            element = driver.FindElement(location);

            //Act
            if (element != null) element.Click();

            //Assert

            //Wait for displaying edit page
            ElementLocation waitForElLocation = (ElementLocation)settings.Locations.Find("FunctionPage.Btn.BackToList");
            IWebElement waitForElement = driver.FindElement(waitForElLocation, 10);

            //Get actual name
            ElementLocation expectedLocation = (ElementLocation)settings.Locations.Find($"{operation}.Name");
            string actualNameOfTank = string.Empty;
            IWebElement expectedElement = driver.FindElementIfExists(expectedLocation);
            if (expectedElement == null)
            {
                //For default tanks, edit operation will actually redirect to detail page.
                expectedLocation = (ElementLocation)settings.Locations.Find("Detail.Name");
                expectedElement = driver.FindElement(expectedLocation);
                actualNameOfTank = expectedElement.Text;
            }
            else
            {
                actualNameOfTank = operation == "Edit" ? expectedElement.GetAttribute("value") : expectedElement.Text;
            }

            Assert.AreEqual(nameOfTank, actualNameOfTank, $"Expecting the name of current tank '{nameOfTank}' but was {actualNameOfTank}.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public void TestOnClickPageNumber(int pageNum, int curPageNum)
        {
            //Arrange

            if(curPageNum > 1)
            {
                //Navigate to current page
                driver.Navigate().GoToUrl(string.Format(settings.Urls.Find("Home.Page").GetValue(), curPageNum));
            }

            string prevNameOfTank = string.Empty;
            ElementLocation tankNameLocation = null;
            if (pageNum != curPageNum)
            {
                //Get expected tank name
                string nameLocKey = $"Home.List.All.Data.Name";
                int selectRow = 2;
                tankNameLocation = (ElementLocation)settings.Locations.Find(nameLocKey).Clone(selectRow);
                IWebElement tankNameElement = driver.FindElement(tankNameLocation);
                prevNameOfTank = tankNameElement.Text;
            }

            //Get list page control page number element
            string numBtnKey = pageNum == curPageNum ? "Home.List.Page.Disabled" : "Home.List.Page.Enabled";
            ElementLocation location = (ElementLocation)settings.Locations.Find(numBtnKey).Clone(pageNum);
            IWebElement element = driver.FindElement(location);

            //Act
            if (pageNum != curPageNum && element != null) element.Click();

            //Assert
            if (pageNum == curPageNum)
            {
                string className = element.GetAttribute("class");
                Assert.IsTrue(className.Contains("selected"), $"Expecting classname contains 'selected', but was '{className}'.");
                Assert.IsTrue(className.Contains("btn-primary"), $"Expecting classname contains 'selected', but was '{className}'.");
            }
            else
            {
                //Wait for displaying new page list
                ByExtensions.ElementLocation(tankNameLocation).ElementTextChanged(driver, prevNameOfTank, 10);

                IWebElement expectedElement = driver.FindElement(tankNameLocation);
                string actualNameOfTank = expectedElement.Text;

                Assert.AreNotEqual(prevNameOfTank, actualNameOfTank, $"Expecting a different name of the tank in the 2nd row '{prevNameOfTank}' but was the same.");
            }
        }

        [Test]
        public void TestOnPageDirectionBtns()
        {

        }
    }
}
