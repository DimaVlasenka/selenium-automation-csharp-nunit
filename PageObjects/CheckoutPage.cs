using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSelenFramework.PageObjects
{
    public class CheckoutPage
    {
        private IWebDriver driver;

        public CheckoutPage(IWebDriver driver) 
        { 
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
                
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        private IList<IWebElement> selectedProducts;

        //driver.FindElement(By.CssSelector(".btn-success")).Click();
        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement chechoutButton;

        public IList<IWebElement> getSelectedProducts() 
        { 
            return selectedProducts; 
        }

        public DeliveryPage checkoutButtonClick() 
        {
            chechoutButton.Click();
            return new DeliveryPage(driver);
        }
    }
}
