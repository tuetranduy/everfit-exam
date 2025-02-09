using OpenQA.Selenium;

namespace EverfitExam.WebElements
{
    public class WebObject
    {
        public By By { get; set; }
        public string Name { get; set; }

        public WebObject(By by, string name = "")
        {
            By = by;
            Name = name;
        }
    }
}