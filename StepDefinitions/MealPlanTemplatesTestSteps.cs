using System.Data;
using System.Text.RegularExpressions;
using EverfitExam.APIs;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using EverfitExam.Drivers;
using EverfitExam.Helpers;
using EverfitExam.Models;
using EverfitExam.PageObjects;
using FluentAssertions;

namespace EverfitExam.StepDefinitions;

[Binding]
public sealed class MealPlanTemplatesTestSteps
{
    private readonly ScenarioContext _scenarioContext;

    private readonly IConfiguration _configuration;
    private readonly LoginPage _loginPage;
    private readonly HomePage _homePage;
    private readonly MealPlanTemplatesPage _mealPlanTemplatesPage;
    
    private readonly MealPlanApiHelpers _mealPlanApiHelpers;

    public MealPlanTemplatesTestSteps(IConfiguration configuration, LoginPage loginPage, HomePage homePage,
        MealPlanTemplatesPage mealPlanTemplatesPage, ScenarioContext scenarioContext, MealPlanApiHelpers mealPlanApiHelpers)
    {
        _configuration = configuration;
        _loginPage = loginPage;
        _homePage = homePage;
        _mealPlanTemplatesPage = mealPlanTemplatesPage;
        _scenarioContext = scenarioContext;
        _mealPlanApiHelpers = mealPlanApiHelpers;
    }

    [BeforeScenario]
    public void InitializeTestData()
    {
        var mealPlanTestData = JsonHelper.LoadJson<MealPlanInfo>("MealPlanTestData");

        _scenarioContext.SetScenarioContext("MEAL_PLAN_TEST_DATA", mealPlanTestData);
    }
    
    [AfterScenario(Order = 1)]
    public void CleanupTestData()
    {
        var mealPlanId = _scenarioContext.GetScenarioContext("MEAL_PLAN_ID", "");

        if (!string.IsNullOrEmpty(mealPlanId))
        {
            _mealPlanApiHelpers.DeleteMealPlan(mealPlanId);
        }
    }

    [Given(@"^User go to login page$")]
    public void GoToUrl()
    {
        DriverUtils.GoToUrl(_configuration["Base.Url"]);
    }

    [When(@"^User enter valid credentials$")]
    public void EnterValidCreds()
    {
        _loginPage.EnterYourEmailAddress(_configuration["User.Email"]);
        _loginPage.EnterPassword(_configuration["User.Password"]);
        _loginPage.ClickLoginBtn();
    }

    [When(@"^User select ""(.*)""$")]
    public void UserSelectSideNavItem(string itemName)
    {
        _homePage.ClickOnSideBarItem(itemName);
    }

    [When(@"^User goes to ""(.*)"" screen")]
    public void UserGoesToMealPlanTemplatesScreen(string itemName)
    {
        _homePage.ClickOnSideBarSubItem(itemName);
    }

    [Given(@"^User click Create Meal Plan")]
    public void UserClickCreateMealPlanBtn()
    {
        _mealPlanTemplatesPage.ClickCreateMealPlanBtn();
    }

    [When(@"^User enter meal plan name")]
    public void UserEnterMealPlanName()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());

        _mealPlanTemplatesPage.EnterMealPlanName(mealPlan.MealPlanName);
    }
    
    [When(@"^User enter Number Of Weeks")]
    public void UserEnterNumberOfWeeks()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());

        _mealPlanTemplatesPage.EnterNumberOfWeeks(mealPlan.NoOfWeek);
    }
    
    [When(@"^User choose Owner option")]
    public void UserChooseOwnerOption()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());

        _mealPlanTemplatesPage.ChooseMealPlanOwner(mealPlan.Owner);
    }
    
    [When(@"^User choose Share with org option")]
    public void UserChooseShareWithOrgOption()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());

        _mealPlanTemplatesPage.ChooseMealPlanShareWithOrg(mealPlan.ShareWithOrg);
    }
    
    [When(@"^User upload Cover Image")]
    public void UserUploadCoverImage()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());

        _mealPlanTemplatesPage.UploadCoverImage(mealPlan.CoverImage);
    }
    
    [When(@"^User click Create button")]
    public void UserClickCreateButton()
    {
        _mealPlanTemplatesPage.ClickCreateBtn();
    }
    
    [Then(@"^Meal Plan have ""(.*)"" status")]
    public void VerifyMealPlanHasStatus(string status)
    {
        var actualStatus = _mealPlanTemplatesPage.GetMealPlanStatus();
        
        actualStatus.Should().Be(status);
    }
    
    [Then(@"^User is in Meal Plan details screen")]
    public void VerifyUserIsInMealPlanDetailsScreen()
    {
        var mealPlan = _scenarioContext.GetScenarioContext("MEAL_PLAN_TEST_DATA", new MealPlanInfo());
        
        var actualMealPlanName = _mealPlanTemplatesPage.GetMealPlanName();
        
        actualMealPlanName.Should().Be(mealPlan.MealPlanName);

        var urlRegex = @"https://dev.everfit.io/home/meal-plans/(.*)/weeks/(?:.*)";
        var currentUrl = DriverUtils.GetUrl();
        
        currentUrl.Should().MatchRegex(urlRegex);

        var mealPlanId = Regex.Match(currentUrl, urlRegex).Groups[1].Value;
        _scenarioContext.SetScenarioContext("MEAL_PLAN_ID", mealPlanId);
    }

    [Given(@"Existing meal plan with")]
    public void GivenExistingMealPlanWith(Table table)
    {
        var dt = TableExtensions.ToDataTable(table);
        
        var mealPlanData = new MealPlanInfo()
        {
            CoverImage = "",
            MealPlanName = dt.Rows[0].Field<string>("MealPlanName"),
            NoOfWeek = int.Parse(dt.Rows[0].Field<string>("NumberOfWeeks")),
            Owner = dt.Rows[0].Field<string>("Owner"),
            ShareWithOrg = dt.Rows[0].Field<string>("ShareWithOrg"),
        };
        
       var response = _mealPlanApiHelpers.CreateMealPlan(mealPlanData);
       var mealPlanId = response.Data.Id;
       
        _scenarioContext.SetScenarioContext("MEAL_PLAN_NAME", mealPlanData.MealPlanName);
        _scenarioContext.SetScenarioContext("MEAL_PLAN_ID", mealPlanId);
    }
    
    [When(@"User goes to Draft screen")]
    public void UserGoToDraftScreen()
    {
        DriverUtils.GoToUrl($"{_configuration["Base.Url"]}home/meal-plans?tab=draft");
    }
    
    [When(@"User goes to meal plan details")]
    public void UserGoToMealPlanDetails()
    {
        var mealPlanName = _scenarioContext.GetScenarioContext("MEAL_PLAN_NAME", "");
        
        _mealPlanTemplatesPage.GoToMealPlan(mealPlanName);
        
        _mealPlanTemplatesPage.WaitForLoading();
    }

    [When(@"User click Edit Info button")]
    public void UserClickEditInfoButton()
    {
        _mealPlanTemplatesPage.ClickEditMealPlanInfo();
    }
    
    [When(@"User update new name for meal plan")]
    public void UserUpdateNewNameForMealPlan(Table table)
    {
        var dt = TableExtensions.ToDataTable(table);

        var updatedMealPlanName = dt.Rows[0].Field<string>("MealPlanName");
        
        _mealPlanTemplatesPage.WaitForLoading();
        _mealPlanTemplatesPage.EnterMealPlanName(updatedMealPlanName);
        _mealPlanTemplatesPage.ClickSaveButton();
        
        var urlRegex = @"https://dev.everfit.io/home/meal-plans/(.*)/weeks/(?:.*)";
        var currentUrl = DriverUtils.GetUrl();
        
        var mealPlanId = Regex.Match(currentUrl, urlRegex).Groups[1].Value;
        _scenarioContext.SetScenarioContext("MEAL_PLAN_ID", mealPlanId);
        _scenarioContext.SetScenarioContext("MEAL_PLAN_NAME", updatedMealPlanName);
    }
    
    [When(@"User click ""(.*)"" in context menu")]
    public void UserChooseContextMenuItem(string ctxName)
    {
       var mealPlanId = _scenarioContext.GetScenarioContext("MEAL_PLAN_ID", "");
        
        _mealPlanTemplatesPage.ChooseMealPlanActions(ctxName, mealPlanId);
    }
    
    [When(@"User Confirm remove meal plan")]
    public void UserConfirmRemoveMealPlan()
    {
        _mealPlanTemplatesPage.ClickConfirmRemoveMealPlan();
    }
    
    [Then(@"Meal Plan name updated successfully")]
    public void VerifyMealPlanNameUpdatedSuccessfully()
    {
        var mealPlanName = _scenarioContext.GetScenarioContext("MEAL_PLAN_NAME", "");
        
        var actualMsg = _mealPlanTemplatesPage.GetToastMessageContent().Trim();
        var expectedMsg = "Successfully updated.";
        actualMsg.Should().Be(expectedMsg);
        
        var actualMealPlanName = _mealPlanTemplatesPage.GetMealPlanName();
        actualMealPlanName.Should().Be(mealPlanName);
    }
}