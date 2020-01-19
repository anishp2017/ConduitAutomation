using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ConduitAutomation
{
    public class Reporter
    {
        static IWebDriver _driver;
        static ISettingsUtility _settingsUtility;
        static string _scenarioName;
        static string _tableBody;
        static string _imageBody;
        static string _featureDirectory;
        static string _scenarioScreenshotDirectory;

        public Reporter(string featureName, string scenarioName, IWebDriver driver, ISettingsUtility settingsUtility, DateTime executionTimeStamp)
        {
            _scenarioName = scenarioName;
            _driver = driver;
            _settingsUtility = settingsUtility;

            _tableBody = string.Empty;
            _imageBody = string.Empty;

            var timestampDirectory = $"{Constants.TestResultsDirectory}/{executionTimeStamp.ToString(Constants.DateTimeFormat)}";
            _featureDirectory = $"{timestampDirectory}/{featureName}";
            _scenarioScreenshotDirectory = $"{_featureDirectory}/Screenshots";

            Directory.CreateDirectory(Constants.TestResultsDirectory);
            Directory.CreateDirectory(timestampDirectory);
            Directory.CreateDirectory(_featureDirectory);
            Directory.CreateDirectory(_scenarioScreenshotDirectory);
        }

        public static void Pass(string stepName, string description)
        {
            LogStep(stepName, description, Constants.Pass);
        }

        public static void Fail(string stepName, string description)
        {
            LogStep(stepName, description, Constants.Fail);
            Assert.Fail(description);
        }

        static void LogStep(string stepName, string description, string status)
        {
            AppendTableBodyRowForStep(stepName, description, status);
            if (_settingsUtility.TakeScreenshot)
            {
                var screenshotPath = GetScreenshotFilePath();
                TakeScreenshot(screenshotPath);
                AppendScreenshotImageBodyForStep(screenshotPath);
            }
        }

        static void AppendTableBodyRowForStep(string stepName, string description, string status)
        {
            var tdStatusStyle = status == Constants.Fail ? " bgcolor='#FF0000'" : string.Empty;
            var tdStatusTag = $"<td{tdStatusStyle}>{status}</td>";
            _tableBody += $@"
<tr>
    <td>{stepName}</td>
    <td>{description}</td>
    {tdStatusTag}
    <td>{DateTime.Now.ToString()}</td>
</tr>
";
        }

        static void AppendScreenshotImageBodyForStep(string screenshotPath)
        {
            _imageBody += $@"
<img src='{screenshotPath}'
     style='margin:auto; width:1200px;display:block'/>
<br />
";
        }

        public void CreateScenarioReport()
        {
            if (!string.IsNullOrEmpty(_tableBody))
            {
                var finalHtml = string.Format(Constants.HtmlFinalDoc, _scenarioName, _driver.GetType().Name, _tableBody, _imageBody);

                var filePath = $"{_featureDirectory}/{_scenarioName}{Constants.HtmlFileFormat}";
                File.WriteAllText(filePath, finalHtml);
            }
        }

        static string GetScreenshotFilePath()
        {
            return $"{_scenarioScreenshotDirectory}/{_scenarioName}{Constants.ScreenshotFileFormat}";
        }

        static void TakeScreenshot(string screenshotPath)
        {
            try
            {
                if (_driver != null)
                {
                    Screenshot ss = null;
                    ss = ((ITakesScreenshot)_driver).GetScreenshot();

                    lock (ss)
                    {
                        ss.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);
                    }
                }
            }
            catch (Exception exception)
            {
                Assert.Fail("Screenshot exception: ", exception.Message);
            }
        }

        public static class Constants
        {
            public static string CurrentDirectory = System.Environment.CurrentDirectory;
            public static string TestResultsDirectory = $"{CurrentDirectory}/TestResults";
            public static string ScreenshotFileFormat = ".png";
            public static string HtmlFileFormat = ".html";
            public static string DateTimeFormat = "yyyyMMddHHmmss";
            public static string Pass = "Pass";
            public static string Fail = "Fail";

            public static string HtmlFinalDoc = @"
<!DOCTYPE html>
<html>
    <head>
    <style>
        table{{
            font-family: arial, sans-serif;
            border-collapse: collapse;
            padding: 8px;
            color: #FFFFFF;
            width: 1200px;
            margin-left: auto;
            margin-right: auto;
        }}
        th{{
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
            background-color: #1882c9;
        }}
        td{{
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
            color:#000000;
        }}
        tr:nth-child(even) {{
            background-color: #a8dcff;
        }}
        tr:nth-child(odd) {{
            background-color: #92caef;
        }}
    </style>
</head>
    <body>
        <table>
        <tr>
            <th>Test Scenario: {0}</th>
            <th>Browser: {1}</th>
        </tr>
        </table>
        <table>
            <tr>
                <th>Test Step</th>
                <th>Description</th>
                <th>Status</th>
                <th>Time</th>
            </tr>
            {2}
        </table>
        <b>
            <p style='color:blue;font-size:20px;text-align:center'>
                Screenshots of the flow
            </p>
        </b>
        {3}
    </body>
</html>
";
        }
    }
}