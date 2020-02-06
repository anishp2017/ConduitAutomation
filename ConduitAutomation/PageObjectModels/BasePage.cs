using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConduitAutomation.PageObjectModels
{
    public class BasePage
    {
        protected IWebDriver _driver;
        protected ISettingsUtility _settingsUtility;
        protected static string _pageUri;

        public BasePage(IWebDriver driver, ISettingsUtility settingsUtility)
        {
            _driver = driver;
            _settingsUtility = settingsUtility;
        }

        public bool IsWebElementPresent(IWebElement webElement)
        {
            try
            {
                return (webElement.Displayed && webElement.Enabled) ? true : false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsWebElementPresentAndDisabled(IWebElement webElement)
        {
            try
            {
                return (webElement.Displayed && !webElement.Enabled) ? true : false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsListElementPresent(IList<IWebElement> ListElement)
        {
            try
            {
                if (ListElement != null)
                {
                    return (ListElement.Count >= 1) ? true : false;
                }
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void WaitUntilPresenceOfAllElementsLocatedBy(By webElementSelector, int seconds)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(webElementSelector));
        }

        public void WaitUntilInvisibilityOfElementLocatedBy(By webElementSelector, int seconds)
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.InvisibilityOfElementLocated(webElementSelector));
        }
        public void RemoveElementsFromDOM(By selector)
        {
            IJavaScriptExecutor Js = (IJavaScriptExecutor)_driver;
            if (TryFindElement(selector, out IWebElement element))
            {
                Js.ExecuteScript("arguments[0].remove()", element);
            }
        }
        public bool TryFindElement(By by, out IWebElement element)
        {
            try
            {
                element = _driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                element = null;
                return false;
            }
            return true;
        }
        public void EnterDataIntoField(IWebElement InputFld, string InputValue)
        {
            try
            {
                if (IsWebElementPresent(InputFld))
                {
                    InputFld.Click();
                    InputFld.Clear();
                    while (InputFld.GetAttribute("value") != "")
                        InputFld.SendKeys(Keys.Backspace);
                    InputFld.SendKeys(InputValue);
                }
                else
                {
                    Reporter.Fail("EnterDataIntoField", "Input field is not available to input the data");
                }
            }
            catch (WebDriverException Wde)
            {
                Reporter.Fail("EnterDataIntoField", "Webelement is not found : " + Wde.InnerException);
            }
        }

        public bool TryFindElement(IWebElement parentElement, By by, out IWebElement ChildElement)
        {
            try
            {
                ChildElement = parentElement.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                ChildElement = null;
                return false;
            }
            return true;
        }

        public bool IsWebElementPresentAfterWait(IWebElement WebElement, int WaitTimeInSeconds)
        {
            try
            {
                new WebDriverWait(_driver, TimeSpan.FromSeconds(WaitTimeInSeconds)).Until(webDriver =>
                    WebElement.Displayed);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (WebDriverException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
