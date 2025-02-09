using NUnit.Framework;
using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.Extensions;

namespace EverfitExam.Drivers
{
    public class DriverUtils
    {
        public static void GoToUrl(string url)
        {
            try
            {
                BrowserFactory.GetWebDriver().Url = url;
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        public static void ClearSessionData()
        {
            ((IJavaScriptExecutor)BrowserFactory.GetWebDriver()).ExecuteScript("sessionStorage.clear();");
            ((IJavaScriptExecutor)BrowserFactory.GetWebDriver()).ExecuteScript("localStorage.clear();");
        }

        public static string CaptureScreenshot(IWebDriver driver)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();

            return screenshot.AsBase64EncodedString;
        }

        public static string GetUrl()
        {
            return BrowserFactory.GetWebDriver().Url;
        }

        public static string GetPageSource()
        {
            return BrowserFactory.GetWebDriver().PageSource;
        }

        public static void ReloadPage()
        {
            BrowserFactory.GetWebDriver().Navigate().Refresh();
        }

        public static void BackPreviousPage()
        {
            BrowserFactory.GetWebDriver().Navigate().Back();
        }
        
        public static T ExecuteScript<T>(string script)
        {
            return BrowserFactory.GetWebDriver().ExecuteJavaScript<T>($"{script}");
        }
    }
}