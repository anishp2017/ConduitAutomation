using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace ConduitAutomation.PageObjectModels
{
    public class SignUpPage : BasePage
    {
        public IWebElement UsernameInput => _driver.FindElement(By.XPath("//form//input[contains(@type,'text')]"));
        public IWebElement EmailInput => _driver.FindElement(By.XPath("//form//input[contains(@type,'email')]"));
        public IWebElement PasswordInput => _driver.FindElement(By.XPath("//form//input[contains(@type,'password')]"));
        public IWebElement SignUpButton => _driver.FindElement(By.XPath("//form//button"));
        public IWebElement ErrorMessageList => _driver.FindElement(By.XPath("//ul[contains(@class,'error-messages')]"));
        public IList<IWebElement> ErrorMessages => _driver.FindElements(By.XPath("//ul[contains(@class,'error-messages')]//li"));

        public SignUpPage(IWebDriver driver, ISettingsUtility settingsUtility) : base(driver, settingsUtility)
        {
        }

        public static SignUpPage NavigateTo(IWebDriver driver, ISettingsUtility settingsUtility)
        {
            var url = Util.BuildUriString(settingsUtility.BaseUrl, settingsUtility.SignUpRoute);
            driver.Navigate().GoToUrl(url);
            return new SignUpPage(driver, settingsUtility);
        }
    }
}
