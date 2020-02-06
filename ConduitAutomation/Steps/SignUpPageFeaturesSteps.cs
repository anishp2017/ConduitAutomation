using System;
using System.Reflection;
using ConduitAutomation.PageObjectModels;
using FluentAssertions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace ConduitAutomation.Steps
{
    [Binding]
    public class SignUpPageFeaturesSteps : BaseSteps
    {
        SignUpPage _signUpPage;
        ConduitApiClient _conduitApiClient;

        public SignUpPageFeaturesSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Given(@"I am on the Sign up page")]
        public void GivenIAmOnTheSignUpPage()
        {
            _signUpPage = SignUpPage.NavigateTo(_driver, _settingsUtility);
            Reporter.Pass(MethodBase.GetCurrentMethod().Name, "Navigated to the sign up page");
        }
        
        [When(@"I enter ""(.*)"" for Email")]
        public void WhenIEnterForEmail(string email)
        {
            _signUpPage.EnterDataIntoField(_signUpPage.EmailInput, email);
        }
        
        [When(@"I click on the Sign up button")]
        public void WhenIClickOnTheSignUpButton()
        {
            _signUpPage.SignUpButton.Click();
        }
        
        [Then(@"I will get an alert message")]
        public void ThenIWillGetAnAlertMessage()
        {
            var validationMessage = _signUpPage.EmailInput.GetAttribute("validationMessage");
            validationMessage.Should().NotBeNullOrEmpty();
        }

        [Given(@"user ""(.*)"" exists")]
        public async void GivenUserExists(string username)
        {
            _conduitApiClient = new ConduitApiClient();
            var user = await _conduitApiClient.GetUser(username);
            user.Username.Should().Be(username);
        }

        [When(@"I enter ""(.*)"" for Username")]
        public void WhenIEnterForUsername(string username)
        {
            _signUpPage.EnterDataIntoField(_signUpPage.UsernameInput, username);
        }

        [When(@"I enter ""(.*)"" for password on the sign up page")]
        public void WhenIEnterForPasswordOnTheSignUpPage(string password)
        {
            _signUpPage.EnterDataIntoField(_signUpPage.PasswordInput, password);
        }

        [Then(@"I will get an error message ""(.*)""")]
        public void ThenIWillGetAnErrorMessage(string errorMessage)
        {
            _signUpPage.WaitUntilPresenceOfAllElementsLocatedBy(By.XPath("//ul[contains(@class,'error-messages')]"), 2);
            Util.GetListOfItems(_signUpPage.ErrorMessages).Contains(errorMessage).Should().BeTrue();
        }
    }
}
