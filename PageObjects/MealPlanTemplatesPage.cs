using EverfitExam.Helpers;
using EverfitExam.WebElements;
using OpenQA.Selenium;

namespace EverfitExam.PageObjects;

public class MealPlanTemplatesPage : BasePage
{
    private readonly WebObject _createMealPlanBtn = new WebObject(By.XPath($"//button[.='Create Meal Plan']"));
    private readonly WebObject _mealPlanNameInput = new WebObject(By.Name($"name"));
    private readonly WebObject _numberOfWeeksInput = new WebObject(By.Name($"numberOfWeeks"));
    private readonly WebObject _createBtn = new WebObject(By.XPath($"//button[.='Create']"));
    private readonly WebObject _saveBtn = new WebObject(By.XPath($"//button[.='Save']"));

    private readonly WebObject _mealPlanStatus =
        new WebObject(By.XPath($"//div[@class='app-navbar__left__main-content']//div//div"));

    private readonly WebObject _mealPlanName =
        new WebObject(By.XPath($"//div[@class='app-navbar__left__main-content']//div//span"));

    private readonly WebObject _ownerDropdown =
        new WebObject(By.XPath(
            $"//div[@role='listbox' and .//preceding::div[contains(@class, 'client_type_field') and .='OWNER']]"));

    private readonly WebObject _shareWithOrgDropdown = new WebObject(By.XPath(
        $"//div[@role='listbox' and .//preceding::div[contains(@class, 'share-with-org') and .='SHARE WITH ORG?']]"));

    private readonly WebObject _coverImage = new WebObject(By.XPath($"//div[.='cover image']//parent::div//input"));
    private readonly WebObject _coverImageAfterUpload = new WebObject(By.CssSelector($".modal .image-with-fallback"));
    private readonly WebObject _editInfoBtn = new WebObject(By.XPath($".//button[.='Edit Info']"));
    private readonly WebObject _confirmRemoveBtn = new WebObject(By.CssSelector($".confirm-yes-button"));
    private readonly WebObject _toastMessageContent = new WebObject(By.CssSelector($".Toastify__toast-body"));
    private readonly WebObject _loadingIndicator = new WebObject(By.CssSelector($".loading"));

    public MealPlanTemplatesPage()
    {
    }

    public void ClickCreateMealPlanBtn()
    {
        _createMealPlanBtn.ClickOnElement();
    }

    public void EnterMealPlanName(string mealPlanName)
    {
        _mealPlanNameInput.EnterText(mealPlanName);
    }

    public void EnterNumberOfWeeks(int numberOfWeeks)
    {
        _numberOfWeeksInput.EnterText($"{numberOfWeeks}");
    }

    public void ClickCreateBtn()
    {
        _createBtn.ClickOnElement();
    }

    public string GetMealPlanName()
    {
        return _mealPlanName.GetTextFromElement();
    }

    public string GetMealPlanStatus()
    {
        return _mealPlanStatus.GetTextFromElement();
    }

    public void ChooseMealPlanOwner(string ownerName)
    {
        _ownerDropdown.ClickOnElement();
        WebObject dropdownOption = new WebObject(By.XPath($"//div[@role='option' and .='{ownerName}']"));
        dropdownOption.ClickOnElement();
    }

    public void ChooseMealPlanShareWithOrg(string option)
    {
        _shareWithOrgDropdown.ClickOnElement();
        WebObject dropdownOption = new WebObject(By.XPath($"//div[@role='option' and .='{option}']"));
        dropdownOption.ClickOnElement();
    }

    public void UploadCoverImage(string coverImage)
    {
        _coverImage.UploadFile(FileHelper.GetProjectFolderPath() + coverImage);

        _coverImageAfterUpload.WaitForElementToBeVisible();
    }

    public void GoToMealPlan(string mealPlanName)
    {
        var mealPlanLct = new WebObject(By.XPath($"//div[contains(text(), '{mealPlanName}')]"));

        mealPlanLct.ClickOnElement();
    }

    public void ClickEditMealPlanInfo()
    {
        _editInfoBtn.ClickOnElement();
    }

    public void ClickSaveButton()
    {
        _saveBtn.ClickOnElement();
    }

    public void ChooseMealPlanActions(string actionName, string mealPlanId)
    {
        var moreLocator = new WebObject(By.XPath($"//div[@data-for='more-options__tooltip-{mealPlanId}']"));
        moreLocator.ClickOnElement();
        
        moreLocator.Find(By.XPath($".//div[contains(@class, 'evf-dropdown__option') and .='{actionName}']")).Click();
    }

    public void ClickConfirmRemoveMealPlan()
    {
        _confirmRemoveBtn.ClickOnElement();
    }

    public string GetToastMessageContent()
    {
        return _toastMessageContent.GetTextFromElement();
    }

    public void WaitForLoading()
    {
        _loadingIndicator.WaitForElementToBeInvisible();
    }
}