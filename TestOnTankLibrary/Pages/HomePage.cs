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
            Assert.AreEqual(expected.Value, title);
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
            Assert.AreEqual(false, hasInvalidStages);
        }
    }
}
