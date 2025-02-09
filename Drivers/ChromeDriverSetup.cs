using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;

namespace EverfitExam.Drivers
{
    public class ChromeDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            return new ChromeDriver(GetDriverOptions());
        }

        private ChromeOptions GetDriverOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("test-type --no-sandbox --start-maximized");
            chromeOptions.AddArguments("--window-size=1920,1080");

            return chromeOptions;
        }
    }
}
