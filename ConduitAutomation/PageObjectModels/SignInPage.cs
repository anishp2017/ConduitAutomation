using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace ConduitAutomation.PageObjectModels
{
    public class SignInPage : BasePage
    {
        public IWebElement NavigationBar => _driver.FindElement(By.XPath("//nav"));

        public IWebElement NavigationBarBrand => _driver.FindElement(By.XPath("//nav//a[contains(@class,'navbar-brand')]"));

        public IList<IWebElement> NavigationTopRightLinks => _driver.FindElements(By.XPath("//nav//li[contains(@class,'nav-item')]"));

        public IWebElement Header => _driver.FindElement(By.XPath("//div[contains(@class,'auth-page')]//h1"));
        
        public IWebElement RegisterAccountLink => _driver.FindElement(By.XPath("//div[contains(@class,'auth-page')]//a"));

        public IWebElement EmailInput => _driver.FindElement(By.XPath("//div[contains(@class,'auth-page')]//form//input[contains(@type,'email')]"));
        
        public IWebElement PasswordInput => _driver.FindElement(By.XPath("//div[contains(@class,'auth-page')]//form//input[contains(@type,'password')]"));
        
        public IWebElement SignInButton => _driver.FindElement(By.XPath("//div[contains(@class,'auth-page')]//form//button"));

        public SignInPage(IWebDriver driver, ISettingsUtility settingsUtility) : base(driver, settingsUtility)
        {
        }

        public static SignInPage NavigateTo(IWebDriver driver, ISettingsUtility settingsUtility)
        {
            var url = Util.BuildUriString(settingsUtility.BaseUrl, settingsUtility.SignInRoute);
            driver.Navigate().GoToUrl(url);
            return new SignInPage(driver, settingsUtility);
        }
    }
}
