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
            ElementLocation location = (ElementLocation)settings.Locations.Find("Home.AddTank");
            IWebElement element = driver.FindElement(location);

            //Act
            if (element != null) element.Click();

            //Assert
            ExpectedSetting expected = (ExpectedSetting)settings.Expecteds.Find("Add.Title");
            string title = driver.Title;
            Assert.AreEqual(expected.Value, title, $"Expecting the title of Add page '{expected.Value}' but was '{title}'.");
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
            string nameLocKey = $"Home.List.All.Data{selectRow}.Name";
            ElementLocation location = (ElementLocation)settings.Locations.Find(nameLocKey);
            IWebElement element = driver.FindElement(location);
            string nameOfTank = element.Text;

            //Get Col to click to change the selected row
            string clickColLocKey = $"Home.List.All.Data{selectRow}.{clickCol}";
            location = (ElementLocation)settings.Locations.Find(clickColLocKey);
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
    }
}
