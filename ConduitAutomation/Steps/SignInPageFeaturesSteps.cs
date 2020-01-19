using System;
using System.Reflection;
using ConduitAutomation.PageObjectModels;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace ConduitAutomation.Steps
{
    [Binding]
    public class SignInPageFeaturesSteps : BaseSteps
    {
        SignInPage _signInPage;

        public SignInPageFeaturesSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Given(@"I am on the sign in page")]
        public void GivenIAmOnTheSignInPage()
        {
            _signInPage = SignInPage.NavigateTo(_driver, _settingsUtility);
            Reporter.Pass(MethodBase.GetCurrentMethod().Name, "Navigated to the sign in page");
        }

        [Then(@"I should see the navigation bar at the top of the page for Sign In page")]
        public void ThenIShouldSeeTheNavigationBarAtTheTopOfThePageForSignInPage()
        {
            _signInPage.IsWebElementPresent(_signInPage.NavigationBar).Should().BeTrue();
        }

        [Then(@"I should see the brand link on the navigation bar for Sign In page")]
        public void ThenIShouldSeeTheBrandLinkOnTheNavigationBarForSignInPage()
        {
            _signInPage.IsWebElementPresent(_signInPage.NavigationBarBrand).Should().BeTrue();
        }

        [Then(@"I should see the links ""(.*)"" on the right side of the navigation bar for Sign In page")]
        public void ThenIShouldSeeTheLinksOnTheRightSideOfTheNavigationBarForSignInPage(string navigationLinks)
        {
            try
            {
                var expectedNavigationLinks = navigationLinks.Split('|');
                var actualNavigationLinks = Util.GetListOfItems(_signInPage.NavigationTopRightLinks);

                actualNavigationLinks.Should().BeEquivalentTo(expectedNavigationLinks);
                Reporter.Pass(MethodBase.GetCurrentMethod().Name, "Navigation links are correct.");
            }
            catch (Exception e)
            {
                Reporter.Fail(MethodBase.GetCurrentMethod().Name, e.Message);
                throw e;
            }
        }
        
        [Then(@"I should see ""(.*)"" as the header for the page")]
        public void ThenIShouldSeeAsTheHeaderForThePage(string header)
        {
            _signInPage.Header.Text.Should().Be(header);
        }
        
        [Then(@"I should see ""(.*)"" link")]
        public void ThenIShouldSeeLink(string registerAccountLink)
        {
            _signInPage.RegisterAccountLink.Text.Should().Be(registerAccountLink);
        }
        
        [Then(@"I should see a field to input email address")]
        public void ThenIShouldSeeAFieldToInputEmailAddress()
        {
            _signInPage.IsWebElementPresent(_signInPage.EmailInput).Should().BeTrue();
        }
        
        [Then(@"I should see a field to input password")]
        public void ThenIShouldSeeAFieldToInputPassword()
        {
            _signInPage.IsWebElementPresent(_signInPage.PasswordInput).Should().BeTrue();
        }

        [Then(@"I should see a ""(.*)"" button")]
        public void ThenIShouldSeeAButton(string signIn)
        {
            _signInPage.SignInButton.Text.Should().Be(signIn);
        }
    }
}
