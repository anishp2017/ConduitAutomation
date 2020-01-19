using System.ComponentModel;

namespace ConduitAutomation
{
    public enum Environment
    {
        [Description("local")]
        Local,

        [Description("qa1")]
        QA1
    }

    public enum Browser
    {
        [Description("chrome")]
        Chrome,

        [Description("firefox")]
        Firefox,

        [Description("internetexplorer")]
        InternetExplorer,

        [Description("safari")]
        Safari
    }
}