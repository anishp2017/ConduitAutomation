using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace ConduitAutomation.PageObjectModels
{
    public class HomePage : BasePage
    {
        public IWebElement NavigationBar => _driver.FindElement(By.XPath("//nav"));

        public IWebElement NavigationBarBrand => _driver.FindElement(By.XPath("//nav//a[contains(@class,'navbar-brand')]"));

        public IList<IWebElement> NavigationTopRightLinks => _driver.FindElements(By.XPath("//nav//li[contains(@class,'nav-item')]"));

        public IWebElement Banner => _driver.FindElement(By.XPath("//div[contains(@class,'home-page')]//div[contains(@class,'banner')]"));

        public IWebElement SignIn => _driver.FindElement(By.XPath("//nav//a[contains(@class,'nav-link') and text()='Sign in']"));

        public IWebElement GlobalFeedTab => _driver.FindElement(By.XPath("//div[contains(@class,'feed-toggle')]//a[contains(@class,'nav-link active') and text()='Global Feed']"));

        public IList<IWebElement> ArticlePreviews => _driver.FindElements(By.XPath("//div[contains(@class,'article-preview')]"));

        public HomePage(IWebDriver driver, ISettingsUtility settingsUtility) : base(driver, settingsUtility)
        {
        }

        public static HomePage NavigateTo(IWebDriver driver, ISettingsUtility settingsUtility)
        {
            var url = Util.BuildUriString(settingsUtility.BaseUrl, "/");
            driver.Navigate().GoToUrl(url);
            return new HomePage(driver, settingsUtility);
        }
    }
}
