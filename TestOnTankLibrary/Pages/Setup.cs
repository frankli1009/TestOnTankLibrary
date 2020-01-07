using System;
using System.IO;
using NUnit.Framework;
using TestOnTankLibrary.Domain;

namespace TestOnTankLibrary.Pages
{
    [SetUpFixture]
    public class Setup
    {
        public Setup()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Initialize and read the settings
            CustomDataCollection<ElementLocation> locations = (new CustomDataCollection<ElementLocation>()).LoadDataFromXls(
               Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../Data/Location.xlsx")), "Location");
            CustomDataCollection<PageUrl> urls = (new CustomDataCollection<PageUrl>()).LoadDataFromXls(
                Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../Data/Location.xlsx")), "Url");
            CustomDataCollection<ExpectedSetting> expecteds = (new CustomDataCollection<ExpectedSetting>()).LoadDataFromXls(
                Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "../../../Data/Location.xlsx")), "Expected");
            Settings.GetInstance().SetLocations(locations).SetUrls(urls).SetExpecteds(expecteds);
        }
    }
}
