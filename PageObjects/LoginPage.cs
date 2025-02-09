using EverfitExam.WebElements;
using OpenQA.Selenium;

namespace EverfitExam.PageObjects;

public class LoginPage
{
    private readonly WebObject _yourEmailAddressInput = new WebObject(By.XPath("//input[@placeholder='Your Email Address']"));
    private readonly WebObject _passwordInput = new WebObject(By.XPath("//input[@placeholder='Password']"));
    private readonly WebObject _loginBtn = new WebObject(By.XPath("//button[.='Login']"));
    
    public LoginPage()
    {
        
    }

    public void EnterYourEmailAddress(string yourEmailAddress)
    {
        _yourEmailAddressInput.EnterText(yourEmailAddress);
    }

    public void EnterPassword(string yourPassword)
    {
        _passwordInput.EnterText(yourPassword);
    }

    public void ClickLoginBtn()
    {
        _loginBtn.ClickOnElement();
    }
    
}