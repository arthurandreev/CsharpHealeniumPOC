using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SeleniumExtras.PageObjects;

namespace SelfHealingSelenium.Tests.POMs
{
    public class HomePage
    {
        private readonly IWebDriver _driver;

        [FindsBy(How = How.Id, Using = "angular-material")]
        public IWebElement AngularMaterial { get; set; }
        
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }
    }
}
