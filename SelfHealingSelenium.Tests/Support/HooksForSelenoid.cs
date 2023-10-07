using OpenQA.Selenium;

namespace SelfHealingSelenium.Tests.Support
{
    [Binding]
        public class HooksForSelenoid
        {
            public static IWebDriver Driver { get; private set; }


            [BeforeScenario]
            public void BeforeScenario()
            {
                Driver = WebDriverFactory.CreateSelenoidDriver(BrowserType.Chrome);
            }

        [AfterScenario]
        public void AfterScenario()
        {
           Driver?.Quit();
        }   
    }
}
