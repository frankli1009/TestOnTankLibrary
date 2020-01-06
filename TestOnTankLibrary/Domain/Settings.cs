using System;
namespace TestOnTankLibrary.Domain
{
    public class Settings
    {
        private static Settings settings;

        private CustomDataCollection<PageUrl> urls;
        private CustomDataCollection<ElementLocation> locations;
        private CustomDataCollection<ExpectedSetting> expecteds;
        public CustomDataCollection<PageUrl> Urls => urls;
        public CustomDataCollection<ElementLocation> Locations => locations;
        public CustomDataCollection<ExpectedSetting> Expecteds => expecteds;

        public Settings()
        {
        }

        public static Settings GetInstance()
        {
            if (settings == null) settings = new Settings();

            return settings;
        }

        public Settings SetLocations(CustomDataCollection<ElementLocation> locations)
        {
            this.locations = locations;
            return this;
        }

        public Settings SetUrls(CustomDataCollection<PageUrl> urls)
        {
            this.urls = urls;
            return this;
        }

        public Settings SetExpecteds(CustomDataCollection<ExpectedSetting> expecteds)
        {
            this.expecteds = expecteds;
            return this;
        }
    }
}
