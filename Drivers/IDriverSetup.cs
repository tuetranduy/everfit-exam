using OpenQA.Selenium;

namespace EverfitExam.Drivers
{
    public interface IDriverSetup
    {
        IWebDriver CreateInstance();
    }
}
