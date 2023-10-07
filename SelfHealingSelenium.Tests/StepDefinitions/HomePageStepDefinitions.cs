using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SelfHealingSelenium.Tests.POMs;
using SelfHealingSelenium.Tests.Support;

namespace SelfHealingSelenium.Tests.StepDefinitions
{
    [Binding]
    public sealed class HomePageStepDefinitions
    {
        HomePage homePage = new HomePage(HooksForSelenoid.Driver);
        private IWebDriver driver = HooksForSelenoid.Driver;

        [Given(@"the user is on the Home page")]
        public void GivenTheUserIsOnTheHomePage()
        {
            driver.Navigate().GoToUrl("http://host.docker.internal:4200");
        }

        [When(@"the user clicks on the Angular Material button")]
        public void WhenTheUserClicksOnTheAngularMaterialButton()
        {
            homePage.AngularMaterial.Click();
        }
     
        [Then(@"a new tab should open with the Angular Material page")]
        public void ThenANewTabShouldOpenWithTheAngularMaterialPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(drv => drv.WindowHandles.Count == 2);

            IList<string> tabs = new List<string>(driver.WindowHandles);
            driver.SwitchTo().Window(tabs[1]);

            wait.Until(drv => drv.Url.Contains("material.angular.io/"));
        }

        [Then(@"the page title should be ""(.*)""")]
        public void ThenThePageTitleShouldBe(string expectedPageTitle)
        {
            string actualPageTitle = driver.Title;
            Assert.AreEqual(expectedPageTitle, actualPageTitle);
        }
    }
}