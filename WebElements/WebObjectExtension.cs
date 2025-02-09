using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using EverfitExam.Drivers;

namespace EverfitExam.WebElements
{
    public static class WebObjectExtension
    {
        public static int GetWaitTimeoutSeconds()
        {
            return 60;
        }

        public static IWebElement WaitForElementToBeVisible(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
                
                return BrowserFactory.GetWebDriver().FindElement(webObject.By);
            }
            catch (WebDriverTimeoutException exception)
            {
                throw exception;
            }
        }
        
        public static void WaitForElementToBeInvisible(this WebObject webObject, int timeout = -1)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), timeout >= 0 ? TimeSpan.FromSeconds(timeout) : TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.PollingInterval = TimeSpan.FromMilliseconds(250);
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                throw exception;
            }
        }

        public static IWebElement WaitForElementToBeExisted(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
                wait.Until(ExpectedConditions.ElementExists(webObject.By));

                return BrowserFactory.GetWebDriver().FindElement(webObject.By);
            }
            catch (WebDriverTimeoutException exception)
            {
                throw exception;
            }
        }

        public static void ClickOnElement(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ClickUsingJS(webObject);
            }
            catch (WebDriverTimeoutException exception)
            {
                throw exception;
            }
        }
        
        public static void ClickUsingJS(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();
                IJavaScriptExecutor executor = (IJavaScriptExecutor)BrowserFactory.GetWebDriver();
                executor.ExecuteScript("arguments[0].click();", element);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void EnterText(this WebObject webObject, string text, bool bypassClearText = false)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();
                element.Clear();
                element.SendKeys(text);
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }
        }
        
        public static IWebElement FindRelativeElementBelow(this WebObject webObject, IWebElement element)
        {
            try
            {
                webObject.WaitForElementToBeVisible();
                var result = BrowserFactory.GetWebDriver().FindElement(RelativeBy.WithLocator(webObject.By).Above(element));
                
                return result;
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }
        }

        public static string GetTextFromElement(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();

                return element.Text;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public static void UploadFile(this WebObject webObject, string text)
        {
            try
            {
                var element = webObject.WaitForElementToBeExisted();
                element.SendKeys(text);
            }
            catch (WebDriverException ex)
            {
                throw ex;
            }
        }
        
        public static IWebElement Find(this WebObject webObject, By childElement)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By)).FindElement(childElement);
            }
            catch (Exception exception) when (exception is WebDriverTimeoutException || exception is NoSuchElementException)
            {
                throw exception;
            }
        }
    }
}