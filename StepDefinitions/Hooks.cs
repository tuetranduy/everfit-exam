using NUnit.Framework;
using Reqnroll;
using System;
using System.Linq;
using AventStack.ExtentReports;
using EverfitExam.Drivers;
using EverfitExam.Helpers;
using EverfitExam.Report;
using Microsoft.Extensions.Configuration;

namespace EverfitExam.StepDefinitions
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private readonly BrowserFactory _browserFactory;

        private static string _reportPath;

        public Hooks(BrowserFactory browserFactory)
        {
            _browserFactory = browserFactory;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeSetUp");
            
            ConfigurationHelper.Initialize();

            _reportPath = FileHelper.GetProjectFolderPath() + ConfigurationHelper.Config["TestResult.FilePath"];
            var reporter = HtmlReporter.CreateHtmlReporter(_reportPath);
            HtmlReporter.CreateBddReport(reporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            HtmlReporter.CreateFeature(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            Console.WriteLine("BaseTest Set up");

            HtmlReporter.CreateScenario(scenarioContext.ScenarioInfo.Title);
            featureContext.FeatureInfo.Tags.ToList().ForEach(tag => HtmlReporter.AssignCategory(tag));

            _browserFactory.InitializeDriver("chrome");
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext)
        {
            var stepDefinitionType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

            switch (stepDefinitionType)
            {
                case "Given":
                    HtmlReporter.CreateGiven(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "When":
                    HtmlReporter.CreateWhen(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "And":
                    HtmlReporter.CreateAnd(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "Then":
                    HtmlReporter.CreateThen(scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    break;
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            TestContext.Progress.WriteLine("=========> BaseTest Tear Down");

            if (scenarioContext.TestError != null)
            {
                if (scenarioContext.TestError.Message.StartsWith("This scenario is SKIPPED"))
                {
                    HtmlReporter.LogByStatus(Status.Skip, scenarioContext.TestError.Message);
                }
                else
                {
                    HtmlReporter.AttachScreenshotToStep();
                }
            }

            BrowserFactory.CleanUp();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeTearDown");
            BrowserFactory.ThreadLocalWebDriver.Dispose();
            HtmlReporter.Flush();
        }
    }
}