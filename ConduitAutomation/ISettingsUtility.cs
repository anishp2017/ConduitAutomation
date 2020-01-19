using Newtonsoft.Json.Linq;

namespace ConduitAutomation
{
    public interface ISettingsUtility
    {
        Environment Environment { get; set; }
        Browser Browser { get; set; }
        bool IsHeadless { get; set; }
        bool TakeScreenshot { get; set; }
        string BaseUrl { get; set; }
        string SignInRoute { get; set; }

        void CreateSettingsFileIfNotExists();
        void InitializeSettings();
        JObject ReadSettingsFile();
        JToken GetSettingValue(JObject settingsJObject, string propertyName);

        // TODO: ChangeSetting
        // void ChangeSetting(JObject settingsJObject, string settingKey);
    }
}