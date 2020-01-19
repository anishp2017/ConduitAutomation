using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConduitAutomation
{
    public class SettingsUtility : ISettingsUtility
    {
        public static class Constants
        {
            public const string SettingsFilePath = "./settings.json";

            public static class Keys
            {
                public const string Environment = "environment";
                public const string Browser = "browser";
                public const string IsHeadless = "isHeadless";
                public const string BaseUrl = "baseUrl";
                public const string SignInRoute = "signInRoute";
                public const string TakeScreenshot = "takeScreenshot";
            }

            public static class DefaultValues
            {
                public const string Environment = "local";
                public const string Browser = "chrome";
                public const string IsHeadless = "true";
                public const string BaseUrl = "http://localhost:4100";
                public const string SignInRoute = "/login";
                public const string TakeScreenshot = "true";
            }
        }

        public Environment Environment { get; set; }
        public Browser Browser { get; set; }
        public bool IsHeadless { get; set; }
        public bool TakeScreenshot { get; set; }
        public string BaseUrl { get; set; }
        public string SignInRoute { get; set; }

        public SettingsUtility()
        {
            CreateSettingsFileIfNotExists();
            InitializeSettings();
        }

        public void CreateSettingsFileIfNotExists()
        {
            if (!File.Exists(Constants.SettingsFilePath))
            {
                JObject settingsJObject = new JObject(
                    new JProperty(Constants.Keys.Environment, Constants.DefaultValues.Environment),
                    new JProperty(Constants.Keys.Browser, Constants.DefaultValues.Browser),
                    new JProperty(Constants.Keys.IsHeadless, Constants.DefaultValues.IsHeadless),
                    new JProperty(Constants.Keys.TakeScreenshot, Constants.DefaultValues.TakeScreenshot),
                    new JProperty(Constants.Keys.BaseUrl, Constants.DefaultValues.BaseUrl),
                    new JProperty(Constants.Keys.SignInRoute, Constants.DefaultValues.SignInRoute)
                    );

                File.WriteAllText(Constants.SettingsFilePath, settingsJObject.ToString());
            }
        }

        public void InitializeSettings()
        {
            JObject settingsJObject = ReadSettingsFile();

            var environmentValue = GetSettingValue(settingsJObject, Constants.Keys.Environment).ToString();
            Environment = Util.GetEnvironmentEnumFromDescription(environmentValue);

            var browserValue = GetSettingValue(settingsJObject, Constants.Keys.Browser).ToString();
            Browser = Util.GetBrowserEnumFromDescription(browserValue);

            var isHeadlessValue = GetSettingValue(settingsJObject, Constants.Keys.IsHeadless).ToString();
            bool.TryParse(isHeadlessValue, out var headlessBool);
            IsHeadless = headlessBool;

            var takeScreenshot = GetSettingValue(settingsJObject, Constants.Keys.TakeScreenshot).ToString();
            bool.TryParse(takeScreenshot, out var takeScreenshotBool);
            TakeScreenshot = takeScreenshotBool;

            BaseUrl = GetSettingValue(settingsJObject, Constants.Keys.BaseUrl).ToString();
            SignInRoute = GetSettingValue(settingsJObject, Constants.Keys.SignInRoute).ToString();
        }

        public JObject ReadSettingsFile()
        {
            JObject settingsJObject = JObject.Parse(File.ReadAllText(Constants.SettingsFilePath));
            return settingsJObject;
        }

        public JToken GetSettingValue(JObject settingsJObject, string propertyName)
        {
            if (!settingsJObject.TryGetValue(propertyName, out var jToken))
            {
                throw new Exception($"Could not read setting value for {propertyName}");
            }

            return jToken;
        }
    }
}
