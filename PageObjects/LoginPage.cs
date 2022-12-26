using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelenFramework.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }
        //driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement userNameField;

        //driver.FindElement(By.Name("password")).SendKeys("learning");
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        //driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();
        [FindsBy(How = How.XPath, Using = "//div[@class='form-group'][5]/label/span/input")]
        private IWebElement checkbox;

        //driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();
        [FindsBy(How = How.XPath, Using = "//input[@value='Sign In']")]
        private IWebElement signInButon;

        public IWebElement getUserNameField() 
        { 
            return userNameField; 
        }

        public IWebElement getPasswordField() 
        { 
            return passwordField; 
        }

        public IWebElement getCheckbox() 
        {
            return checkbox;
        }

        public ProductsPage signIn(String userName, String password) 
        {
            getUserNameField().SendKeys(userName);
            getPasswordField().SendKeys(password);
            getCheckbox().Click();
            signInButon.Click();
            return new ProductsPage(driver);
        }
    }
}
