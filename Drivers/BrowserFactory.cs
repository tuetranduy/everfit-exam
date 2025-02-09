using OpenQA.Selenium;
using System;
using System.Threading;

namespace EverfitExam.Drivers
{
    public class BrowserFactory
    {
        public static ThreadLocal<IWebDriver> ThreadLocalWebDriver = new ThreadLocal<IWebDriver>();

        public void InitializeDriver(string browserName = "chrome")
        {
            IDriverSetup driverSetup = browserName.ToLower() switch
            {
                "chrome" => new ChromeDriverSetup(),
                "firefox" => new FirefoxDriverSetup(),
                _ => throw new ArgumentOutOfRangeException(browserName),
            };

            ThreadLocalWebDriver.Value = driverSetup.CreateInstance();
        }

        public static IWebDriver GetWebDriver()
        {
            return ThreadLocalWebDriver.Value;
        }

        public static void CleanUp()
        {
            if (GetWebDriver() != null)
            {
                foreach (string window in GetWebDriver().WindowHandles)
                {
                    GetWebDriver().SwitchTo().Window(window);
                    GetWebDriver().Close();
                }

                GetWebDriver().Quit();
                GetWebDriver().Dispose();
            }
        }
    }
}
