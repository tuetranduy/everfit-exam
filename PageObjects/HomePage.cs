using EverfitExam.WebElements;
using OpenQA.Selenium;

namespace EverfitExam.PageObjects;

public class HomePage : BasePage
{
    private WebObject _sideBarItem(string itemName) => new WebObject(By.XPath($"//div[contains(@class, 'sidebar__item') and .//.='{itemName}']"));
    private WebObject _sideBarSubItem(string itemName) => new WebObject(By.XPath($"//div[contains(@class, 'menu-list-item') and .//.='{itemName}']"));
    
    public HomePage()
    {
        
    }

    public void ClickOnSideBarItem(string itemName)
    {
        _sideBarItem(itemName).ClickOnElement();
    }

    public void ClickOnSideBarSubItem(string itemName)
    {
        _sideBarSubItem(itemName).ClickOnElement();
    }
}