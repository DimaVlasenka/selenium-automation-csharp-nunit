using AngleSharp;
using CSharpSelenFramework.PageObjects;
using CSharpSelenFramework.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;

namespace CSharpSelenFramework.Tests
{
    public class EndToEndWithPageObjects : Base
    {
        //dotnet test .\CSharpSelenFramework.csproj -- 'TestRunParameters.Parameter(name=\"browserName\", value=\"ff\")'

        [Test, TestCaseSource(nameof(testCaseDataSet))]
        //[TestCase ("rahulshettyacademy", "learning", "Bel", "Belgium")]
        //[TestCase("rahulshettyacademy", "learning", "Ger", "Germany")]
        
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlowWithPageObjects(String userName, String password, String textToType, String countryToSelect, String[] productsToSelect)
        {

            //string[] productsToSelect = { "iphone X", "Blackberry" };

            //driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            getDriver().Url = ConfigurationManager.AppSettings["url"];

            LoginPage loginPage = new LoginPage(getDriver());
            //enter username
            //enter password
            //check checkboxLabel
            //click on Sign In button
            
            ProductsPage productsPage = loginPage.signIn(userName, password);
            productsPage.waitProductsPageToDisplay();

            //click on Add for products from productToSelect list
            
            IList<IWebElement> presentedProducts = productsPage.getPresentedProducts();

            foreach (IWebElement presentedProduct in presentedProducts)
            {
                if (productsToSelect.Contains(presentedProduct.FindElement(productsPage.getProductTitle()).Text))
                {
                    presentedProduct.FindElement(productsPage.getAddButton()).Click();
                }
            }

            //click on Checkout button
            CheckoutPage checkoutPage = productsPage.checkoutClick();

            //verify the list of selected products
            IList<IWebElement> selectedProducts = checkoutPage.getSelectedProducts();

            string[] selectedProductText = new string[selectedProducts.Count];

            for (int i = 0; i < selectedProducts.Count; i++)
            {
                selectedProductText[i] = selectedProducts[i].Text;
            }

            Assert.That(selectedProductText, Is.EqualTo(productsToSelect));
            //Assert.AreEqual(productsToSelect,selectedProductText);

            //click on Checkout
            DeliveryPage deliveryPage = checkoutPage.checkoutButtonClick();

            //type Bel into text fiel and select Belarus
            String typeText = textToType;
            //String countryToSelect = country;
            deliveryPage.selectCountry(typeText, countryToSelect);
                      
             
            string selectedCountry = deliveryPage.getCountryField().GetAttribute("value");
            TestContext.Progress.WriteLine(selectedCountry);

            Assert.That(selectedCountry, Is.EqualTo(countryToSelect));

            //check checkboxLabel
            deliveryPage.checkboxClick();
            Assert.True(deliveryPage.getCheckbox().Selected);


            //click on Purchase
            deliveryPage.purchaseButtonClick();

            //verify success message
            string successMessage = deliveryPage.getSuccessMessage().Text;
            TestContext.Progress.WriteLine(successMessage);
            string expectedSuccessMessage = "Success! Thank you! Your order will be delivered in next few weeks";

            StringAssert.Contains(expectedSuccessMessage, successMessage);
        }

        public static IEnumerable<TestCaseData> testCaseDataSet() 
        {
            yield return new TestCaseData(
                getDataParser().extractData("username"),
                getDataParser().extractData("incor_pass"),
                getDataParser().extractData("textToType_bel"),
                getDataParser().extractData("countryToSelect_bel"),
                getDataParser().extractDataArray("productsToSelect")
                );
            yield return new TestCaseData(
                getDataParser().extractData("username"),
                getDataParser().extractData("password"),
                getDataParser().extractData("textToType_ger"),
                getDataParser().extractData("countryToSelect_ger"), 
                getDataParser().extractDataArray("productsToSelect")
                );
            yield return new TestCaseData(
                getDataParser().extractData("username"),
                getDataParser().extractData("password"),
                getDataParser().extractData("textToType_pol"),
                getDataParser().extractData("countryToSelect_pol"),
                getDataParser().extractDataArray("productsToSelect")
                );
        }
    }
}