using OpenQA.Selenium;

namespace SelfHealingSelenium.Tests.Support
{
    [Binding]
        public class HooksForSelenoid
        {
        public static IWebDriver? Driver { get; private set; }


        [BeforeScenario]
        private void BeforeScenario()
        {
           Driver = WebDriverFactory.CreateSelenoidDriver(BrowserType.Chrome);
        }

        [AfterScenario]
        private void AfterScenario()
        {
           Driver?.Quit();
        }   
    }
}
