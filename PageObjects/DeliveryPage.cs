using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelenFramework.PageObjects
{
    public class DeliveryPage
    {
        private IWebDriver driver;
        public DeliveryPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        
        [FindsBy(How = How.Id, Using = "country")]
        private IWebElement countryField;
                
        [FindsBy(How = How.XPath, Using = "//label[@for='checkbox2']")]
        private IWebElement checkboxLabel;

        [FindsBy(How = How.XPath, Using = "//input[@type='checkbox']")]
        private IWebElement checkbox;
                
        [FindsBy(How = How.XPath, Using = "//input[@value='Purchase']")]
        private IWebElement purchaseButton;

        //verify success message
        [FindsBy(How = How.CssSelector, Using = ".alert-success")]
        private IWebElement successMessage;

        public IWebElement getCountryField() 
        { 
            return countryField; 
        }

        public IWebElement getCheckbox() 
        {
            return checkbox;
        }

        public IWebElement getSuccessMessage() 
        {
            return successMessage;
        }

        public void selectCountry(String typeText, String countryToSelect) 
        {
            countryField.SendKeys(typeText);
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText(countryToSelect)));
            driver.FindElement(By.LinkText(countryToSelect)).Click();
        }

        public void checkboxClick() 
        {
            checkboxLabel.Click();
        }

        public void purchaseButtonClick() 
        {
            purchaseButton.Click();
        }

    }
}
