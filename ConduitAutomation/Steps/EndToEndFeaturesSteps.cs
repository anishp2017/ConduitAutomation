using System;
using ConduitAutomation.PageObjectModels;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace ConduitAutomation.Steps
{
    [Binding]
    public class EndToEndFeaturesSteps : BaseSteps
    {
        HomePage _homePage;
        SignInPage _signInPage;
        ConduitApiClient _conduitApiClient;

        public EndToEndFeaturesSteps(ScenarioContext scenarioContext, FeatureContext featureContext) : base(scenarioContext, featureContext)
        {
        }

        [Given(@"I am signed in with email ""(.*)"" and password ""(.*)"" endToEnd")]
        public void GivenIAmSignedInWithEmailAndPasswordEndToEnd(string email, string password)
        {
            _signInPage = SignInPage.NavigateTo(_driver, _settingsUtility);
            _signInPage.EnterDataIntoField(_signInPage.EmailInput, email);
            _signInPage.EnterDataIntoField(_signInPage.PasswordInput, password);
            _signInPage.SignInButton.Click();
            var expectedHomePageUrl = $"{_settingsUtility.BaseUrl}/";
            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlToBe(expectedHomePageUrl));
        }

        [Given(@"I am on the home page endToEnd")]
        public void GivenIAmOnTheHomePageEndToEnd()
        {
            _homePage = HomePage.NavigateTo(_driver, _settingsUtility);
        }

        [Given(@"The article ""(.*)"" exists in the system")]
        public async void GivenTheArticleExistsInTheSystem(string slug)
        {
            _conduitApiClient = new ConduitApiClient();
            var article = await _conduitApiClient.GetArticle(slug);
            article.Slug.Should().Be(slug);
        }

        [When(@"I click the preview link for article ""(.*)""")]
        public void WhenIClickThePreviewLinkForArticle(string articleName)
        {
            _homePage.WaitUntilPresenceOfAllElementsLocatedBy(By.XPath("//div[contains(@class,'article-preview')]//a"), 2);
            var articlePreviewLink = _homePage.GetArticlePreviewLink(articleName);
            articlePreviewLink.Click();
        }
        
        [Then(@"I will land on the article page for ""(.*)""")]
        public void ThenIWillLandOnTheArticlePageFor(string articleName)
        {
            var expectedArticleUrl = $"{_settingsUtility.BaseUrl}{_settingsUtility.ArticleRoute}/{articleName}";
            new WebDriverWait(_driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.UrlToBe(expectedArticleUrl));
            _driver.Url.Should().Be(expectedArticleUrl);
        }
    }
}
