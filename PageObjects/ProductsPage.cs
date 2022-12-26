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
    public class ProductsPage
    {
        private IWebDriver driver;

        private By productTitle = By.CssSelector(".card-title a");
        private By addButton = By.CssSelector("button.btn");



        public ProductsPage(IWebDriver driver) 
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }

        [FindsBy(How = How.PartialLinkText, Using = "Checkout")]
        private IWebElement checkoutButton;

        [FindsBy(How = How.TagName, Using = "app-card")]
        private IList<IWebElement> presentedProducts;
               
        public void waitProductsPageToDisplay() 
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));
        }

        public IList<IWebElement> getPresentedProducts() 
        { 
            return presentedProducts; 
        }

        public By getProductTitle() 
        { 
            return productTitle; 
        }

        public By getAddButton() 
        {
            return addButton;
        }

        public CheckoutPage checkoutClick()         
        {
            checkoutButton.Click();
            return new CheckoutPage(driver);
        }
    }
}
