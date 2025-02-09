using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace EverfitExam.Drivers
{
    public class FirefoxDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());

            return new FirefoxDriver(GetDriverOptions());
        }

        private FirefoxOptions GetDriverOptions()
        {
            var firefoxOptions = new FirefoxOptions();
            firefoxOptions.AddArgument("--window-size=1325x744");
            if (TestContext.Parameters.Get("HeadlessMode").ToLower().Equals("true"))
            {
                firefoxOptions.AddArgument("headless");
            }
            return firefoxOptions;
        }
    }
}
