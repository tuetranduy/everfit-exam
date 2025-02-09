using System.Threading;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using EverfitExam.Drivers;

namespace EverfitExam.Report;

public static class HtmlReporter
{
    private static ExtentSparkReporter _reporter;
    private static ExtentReports _report;

    private static ThreadLocal<ExtentTest> _feature = new ThreadLocal<ExtentTest>();
    private static ThreadLocal<ExtentTest> _scenario = new ThreadLocal<ExtentTest>();
    private static ThreadLocal<ExtentTest> _step = new ThreadLocal<ExtentTest>();

    public static ExtentTest GetCurrentFeature()
    {
        return _feature.Value;
    }

    public static ExtentTest GetCurrentScenario()
    {
        return _scenario.Value;
    }

    public static ExtentTest GetCurrentStep()
    {
        return _step.Value;
    }

    public static void SetCurrentFeature(ExtentTest feature)
    {
        _feature.Value = feature;
    }

    public static void SetCurrentScenario(ExtentTest scenario)
    {
        _scenario.Value = scenario;
    }

    public static void SetCurrentStep(ExtentTest step)
    {
        _step.Value = step;
    }

    public static ExtentSparkReporter CreateHtmlReporter(string reportPath)
    {
        if (_reporter == null)
        {
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPath);
            _reporter = htmlReporter;
        }

        return _reporter;
    }

    public static void CreateBddReport(ExtentSparkReporter reporter)
    {
        if (_report == null)
        {
            ExtentReports bddReport = new ExtentReports();

            bddReport.AttachReporter(reporter);
            bddReport.AddSystemInfo("Environment", "test");
            bddReport.AddSystemInfo("Browser", "chrome");
            bddReport.Report.AnalysisStrategy = AnalysisStrategy.BDD;

            _report = bddReport;
        }
    }

    public static void Flush()
    {
        _report.Flush();
    }

    public static void CreateFeature(string name)
    {
        if (_report != null)
        {
            SetCurrentFeature(_report.CreateTest<Feature>(name));
        }
    }

    public static void CreateScenario(string name)
    {
        if (_scenario != null)
        {
            var currentFeature = GetCurrentFeature();
            SetCurrentScenario(currentFeature.CreateNode<Scenario>(name));
        }
    }

    public static void CreateGiven(string name)
    {
        var currentScenario = GetCurrentScenario();
        SetCurrentStep(currentScenario.CreateNode<Given>(name));
    }

    public static void CreateAnd(string name)
    {
        var currentScenario = GetCurrentScenario();
        SetCurrentStep(currentScenario.CreateNode<And>(name));
    }

    public static void CreateWhen(string name)
    {
        var currentScenario = GetCurrentScenario();
        SetCurrentStep(currentScenario.CreateNode<When>(name));
    }

    public static void CreateThen(string name)
    {
        var currentScenario = GetCurrentScenario();
        SetCurrentStep(currentScenario.CreateNode<Then>(name));
    }

    public static void ReportPass(string message)
    {
        GetCurrentStep().Pass(message);
    }

    public static void ReportFail(string message)
    {
        GetCurrentStep().Fail(message);
    }

    public static void Log(string message)
    {
        GetCurrentStep().Info(message);
    }

    public static void LogByStatus(Status status, string message)
    {
        GetCurrentStep().Log(status, message);
    }

    public static void Attach(string path, string title = "")
    {
        GetCurrentStep().AddScreenCaptureFromPath(path, title);
    }

    public static void AssignCategory(string tag)
    {
        GetCurrentScenario().AssignCategory(tag);
    }

    public static void AttachScreenshotToStep()
    {
        object screenshotBase64;

        screenshotBase64 = DriverUtils.CaptureScreenshot(BrowserFactory.GetWebDriver());

        GetCurrentStep().AddScreenCaptureFromBase64String($"{screenshotBase64}");
    }

    public static void ReportFails(string message, string separator = "|")
    {
        string[] arrMsg = message.Split(separator);
        foreach (string s in arrMsg)
        {
            GetCurrentStep().Fail(s);
        }
    }
}