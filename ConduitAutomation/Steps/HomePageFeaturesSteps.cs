using System;
using System.Reflection;
using ConduitAutomation.PageObjectModels;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ConduitAutomation.Steps
{
    [Binding]
    public class HomePageFeaturesSteps : BaseSteps
    {
        HomePage _homePage;

        public HomePageFeaturesSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Given(@"I am not signed in")]
        public void GivenIAmNotSignedIn()
        {
            _homePage.IsWebElementPresent(_homePage.SignIn).Should().BeTrue();
        }
        
        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage = HomePage.NavigateTo(_driver, _settingsUtility);
            Reporter.Pass(MethodBase.GetCurrentMethod().Name, "Navigated to the home page");
        }
        
        [Then(@"I should see the navigation bar at the top of the page")]
        public void ThenIShouldSeeTheNavigationBarAtTheTopOfThePage()
        {
            _homePage.IsWebElementPresent(_homePage.NavigationBar).Should().BeTrue();
        }

        [Then(@"I should see the brand link on the navigation bar")]
        public void ThenIShouldSeeTheBrandLinkOnTheNavigationBar()
        {
            _homePage.IsWebElementPresent(_homePage.NavigationBarBrand).Should().BeTrue();
        }

        [Then(@"I should see the links ""(.*)"" on the right side of the navigation bar")]
        public void ThenIShouldSeeTheLinksOnTheRightSideOfTheNavigationBar(string navigationLinks)
        {
            try
            {
                var expectedNavigationLinks = navigationLinks.Split('|');
                var actualNavigationLinks = Util.GetListOfItems(_homePage.NavigationTopRightLinks);

                actualNavigationLinks.Should().BeEquivalentTo(expectedNavigationLinks);
                Reporter.Pass(MethodBase.GetCurrentMethod().Name, "Navigation links are correct.");
            }
            catch (Exception e)
            {
                Reporter.Fail(MethodBase.GetCurrentMethod().Name, e.Message);
                throw e;
            }
        }

        [Then(@"I should see the banner")]
        public void ThenIShouldSeeTheBanner()
        {
            _homePage.IsWebElementPresent(_homePage.Banner).Should().BeTrue();
        }

        [Given(@"There is at least one article in the system")]
        public void GivenThereIsAtLeastOneArticleInTheSystem()
        {
            // TODO: should call api and get count of articles
            return;
        }

        [Then(@"I should see ""(.*)"" tab")]
        public void ThenIShouldSeeTab(string feedTab)
        {
            _homePage.IsWebElementPresent(_homePage.GlobalFeedTab).Should().BeTrue();
        }

        [Then(@"I should see article preview")]
        public void ThenIShouldSeeArticlePreview()
        {
            _homePage.ArticlePreviews.Count.Should().BeGreaterThan(0);
        }

        [Given(@"I am signed in with email ""(.*)"" and password ""(.*)""")]
        public void GivenIAmSignedInWithEmailAndPassword(string email, string password)
        {
            _homePage.SignIn.Click();
            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlToBe($"{_settingsUtility.SignInRoute}/"));


        }

    }
}
