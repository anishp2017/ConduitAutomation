using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace ConduitAutomation.PageObjectModels
{
    public class ArticlePage : BasePage
    {
        public ArticlePage(IWebDriver driver, ISettingsUtility settingsUtility) : base(driver, settingsUtility)
        {
        }

        public static ArticlePage NavigateTo(IWebDriver driver, ISettingsUtility settingsUtility)
        {
            var url = Util.BuildUriString(settingsUtility.BaseUrl, settingsUtility.ArticleRoute);
            driver.Navigate().GoToUrl(url);
            return new ArticlePage(driver, settingsUtility);
        }
    }
}
