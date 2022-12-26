using AngleSharp;
using CSharpSelenFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace CSharpSelenFramework.Tests
{
    public class EndToEnd : Base
    {

        [Test]
        public void EndToEndFlow()
        {

            string[] productsToSelect = { "iphone X", "Blackberry" };

            //driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Url = ConfigurationManager.AppSettings["url"];

            //enter username
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");

            //enter password
            driver.FindElement(By.Name("password")).SendKeys("learning");

            // check checkboxLabel
            driver.FindElement(By.XPath("//div[@class='form-group'][5]/label/span/input")).Click();

            //click on Sign In button
            driver.FindElement(By.XPath("//input[@value='Sign In']")).Click();

            //click on Add for prodicts from productToSelect list
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("Checkout")));

            IList<IWebElement> presentedProducts = driver.FindElements(By.TagName("app-card"));

            foreach (IWebElement presentedProduct in presentedProducts)
            {

                if (productsToSelect.Contains(presentedProduct.FindElement(By.CssSelector(".card-title a")).Text))
                {

                    presentedProduct.FindElement(By.CssSelector("button.btn")).Click();
                }
            }

            //click on Checkout button
            driver.FindElement(By.PartialLinkText("Checkout")).Click();

            //verify the list of selected products
            IList<IWebElement> selectedProducts = driver.FindElements(By.CssSelector("h4 a"));

            string[] selectedProductText = new string[selectedProducts.Count];

            for (int i = 0; i < selectedProducts.Count; i++)
            {

                selectedProductText[i] = selectedProducts[i].Text;

            }

            Assert.That(selectedProductText, Is.EqualTo(productsToSelect));
            //Assert.AreEqual(productsToSelect,selectedProductText);

            //click on Checkout
            driver.FindElement(By.CssSelector(".btn-success")).Click();

            //type Bel into text fiel and select Belarus
            driver.FindElement(By.Id("country")).SendKeys("Bel");

            string countryToSelect = "Belarus";

            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='suggestions']/ul")));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.LinkText(countryToSelect)));
            driver.FindElement(By.LinkText("Belarus")).Click();

            /*IList<IWebElement> countriesList = driver.FindElements(By.XPath("//div[@class='suggestions']/ul"));

            foreach (IWebElement country in countriesList) {

                if (country.Text.Equals(countryToSelect))
                {
                    country.Click();
                    break;
                }
                
            }
            */

            string selectedCountry = driver.FindElement(By.Id("country")).GetAttribute("value");

            Assert.That(selectedCountry, Is.EqualTo(countryToSelect));

            //check checkboxLabel
            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            Assert.True(driver.FindElement(By.XPath("//input[@type='checkboxLabel']")).Selected);


            //click on Purchase
            driver.FindElement(By.XPath("//input[@value='Purchase']")).Click();

            //verify success message
            string successMessage = driver.FindElement(By.CssSelector(".alert-success")).Text;
            TestContext.Progress.WriteLine(successMessage);
            string expectedSuccessMessage = "Success! Thank you! Your order will be delivered in next few weeks";

            StringAssert.Contains(expectedSuccessMessage, successMessage);
        }
    }
}