using System;
namespace TestOnTankLibrary.Domain
{
    /// <summary>
    /// The settings of Urls, Locations and ExpectedSettings for testing
    /// </summary>
    public class Settings
    {
        private static Settings settings;

        private CustomDataCollection<PageUrl> urls;
        private CustomDataCollection<ElementLocation> locations;
        private CustomDataCollection<ExpectedSetting> expecteds;
        public CustomDataCollection<PageUrl> Urls => urls;
        public CustomDataCollection<ElementLocation> Locations => locations;
        public CustomDataCollection<ExpectedSetting> Expecteds => expecteds;

        /// <summary>
        /// Make constructor private to force using of settings by GetInstance
        /// </summary>
        private Settings()
        {
        }

        /// <summary>
        /// Get the instance of Settings which will be shared by all requests
        /// </summary>
        /// <returns></returns>
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
