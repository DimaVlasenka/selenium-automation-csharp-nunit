using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using System.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace CSharpSelenFramework.Utilities
{
    public class Base
    {
        public ExtentReports extentReporter;
        public ExtentTest test;

        [OneTimeSetUp]
        public void Setup() 
        {
            String workingDirectory = Environment.CurrentDirectory;
            String projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extentReporter = new ExtentReports();
            
            extentReporter.AttachReporter(htmlReporter);
            extentReporter.AddSystemInfo("Host Name", "Local host");
            extentReporter.AddSystemInfo("Environment", "QA");
            extentReporter.AddSystemInfo("Tester Name", "Dima Valenska");
        }
        
        
        
        //public IWebDriver driver;
        public ThreadLocal <IWebDriver> driver = new ThreadLocal<IWebDriver>();

        private String browserName;

        [SetUp]
        public void setUpBrowser()         
        {
            test = extentReporter.CreateTest(TestContext.CurrentContext.Test.Name);

            browserName = TestContext.Parameters["browserName"];

            if (browserName == null)
            { 
                browserName = ConfigurationManager.AppSettings["browser"]; 
            }

            initBrowser(browserName);
            
            driver.Value.Manage().Window.Maximize();

        }
        public void initBrowser(String browserName)
        {
            switch (browserName.ToLower()) 
            {
                case "chrome" :
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;

                case "firefox" or "ff":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;

                default:
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
            }
        }

        public IWebDriver getDriver() 
        {
            return driver.Value;
        }

        public static JsonReader getDataParser() 
        {
            return new JsonReader();
        }

        [TearDown]

        public void quitBrowser() 
        {

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h-mm-ss") + ".png";

            if (status == TestStatus.Failed) 
            {
                test.Fail("Test Failed", screenShotCapture(getDriver(), fileName));
                test.Log(Status.Fail, "Test faile with logtrace " + stackTrace);
            }
            else if (status == TestStatus.Passed) 
            {
                
            }

            extentReporter.Flush();

            driver.Value.Quit();        
        }

        public MediaEntityModelProvider screenShotCapture(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot screenShotTaker = (ITakesScreenshot)driver;
            var screenShot = screenShotTaker.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenShot, screenShotName).Build();
        }
    }
}
