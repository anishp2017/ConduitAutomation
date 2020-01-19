using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace ConduitAutomation.Steps
{
    [Binding]
    public class BaseSteps
    {
        protected static IWebDriver _driver;
        protected static ISettingsUtility _settingsUtility;
        protected static Reporter _reporter;
        protected static int _executedTestCount = 0;
        protected static int _passedTestCount = 0;
        protected static int _failedTestCount = 0;
        protected static IDictionary<string, IList<string>> _failedScenarios = new Dictionary<string, IList<string>>();
        protected static ScenarioContext _scenarioContext;
        protected static FeatureContext _featureContext;
        protected static DateTime _executionTimeStamp;

        public BaseSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        private static void BeforeTestRun()
        {
            _executionTimeStamp = DateTime.Now;
            _settingsUtility = new SettingsUtility();
            InitializeDriver();
        }

        [AfterTestRun]
        private static void AfterTestRun()
        {
            WriteResultsSummary();
            _driver.Quit();
            _driver.Dispose();
            KillProcesses();
        }

        [BeforeFeature]
        private static void BeforeFeature()
        {
            var a = true;
        }

        [AfterFeature]
        private static void AfterFeature()
        {
            var a = true;
        }

        [BeforeScenario]
        private void BeforeScenario()
        {
            _reporter = new Reporter(_featureContext.FeatureInfo.Title, _scenarioContext.ScenarioInfo.Title, _driver, _settingsUtility, _executionTimeStamp);
        }

        [AfterScenario]
        private void AfterScenario()
        {
            _executedTestCount++;
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Passed:
                    _passedTestCount++;
                    break;
                case TestStatus.Failed:
                    _failedTestCount++;
                    var featureName = _featureContext.FeatureInfo.Title;
                    if (!_failedScenarios.TryGetValue(featureName, out var failedScenariosForFeature))
                    {
                        failedScenariosForFeature = new List<string>();
                        _failedScenarios.Add(featureName, failedScenariosForFeature);
                    }
                    failedScenariosForFeature.Add(_scenarioContext.ScenarioInfo.Title);
                    break;
            }

            _reporter.CreateScenarioReport();
        }

        static void InitializeDriver()
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            switch (_settingsUtility.Browser)
            {
                case Browser.Chrome:
                    var chromeOptions = new ChromeOptions();
                    if (_settingsUtility.IsHeadless)
                    {
                        chromeOptions.AddArgument("headless");
                    }

                    _driver = new ChromeDriver(driverDirectory, chromeOptions);
                    break;
                case Browser.InternetExplorer:
                    // IE does not support headless mode
                    _driver = new InternetExplorerDriver(driverDirectory);
                    break;

                    // TODO: add other browsers
            }
        }

        static void WriteResultsSummary()
        {
            string HtmlResultsDoc = @"
<!DOCTYPE html>
<html>
    <head>
        <style>
        </style>
    </head>
    <body>
        Tests executed: {0}
        <br>
        Tests passed: {1}
        <br>
        Tests failed: {2}
        <br>
        <br>
        {3}
    </body>
</html>
";
            var summaryResultsFilePath = $"{Reporter.Constants.TestResultsDirectory}/{_executionTimeStamp.ToString(Reporter.Constants.DateTimeFormat)}/ResultsSummary.html";
            var failedScenarioLinks = GetFailedScenarioLinks();
            var finalHtml = string.Format(HtmlResultsDoc, _executedTestCount, _passedTestCount, _failedTestCount, failedScenarioLinks);
            File.WriteAllText(summaryResultsFilePath, finalHtml);
        }

        static string GetFailedScenarioLinks()
        {
            var failedScenarioLinks = string.Empty;
            if (_failedScenarios.Count > 0)
            {
                failedScenarioLinks += "Failed scenarios: <br>";
                foreach (var featureName in _failedScenarios.Keys.OrderBy(x => x))
                {
                    failedScenarioLinks += $"Feature: {featureName} <br>";
                    foreach (var scenarioName in _failedScenarios[featureName].OrderBy(x => x))
                    {
                        failedScenarioLinks += $@"<a href=""./{featureName}/{scenarioName}.html"">{scenarioName}</a> <br>";
                    }

                    failedScenarioLinks += "<br>";
                }
            }

            return failedScenarioLinks;
        }

        static void KillProcesses()
        {
            // There is a known issue that the task resources are still hanging upon driver.Quit(). So this is the workaround to kill the processes.
            var processes = new List<string>
            {
                "chromedriver",
                "iexplore",
                "IEDriverServer"
            };

            processes.ForEach(KillProcessesByName);
        }

        static void KillProcessesByName(string processName)
        {
            var processes = Process.GetProcessesByName(processName);

            foreach (Process p in processes)
            {
                p.Kill();
            }
        }

        static void KillProcessesForIE()
        {
            // For IE, there is a known issue that the task resources are still hanging upon driver.Quit(). So this is the workaround to kill the processes.
            var processes = Process.GetProcessesByName("iexplore");

            foreach (Process process in processes)
            {
                process.Kill();
            }

            processes = Process.GetProcessesByName("IEDriverServer");

            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
    }
}
