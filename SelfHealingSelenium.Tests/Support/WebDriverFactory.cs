using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SelfHealingSelenium.Tests.Support
{
    public class WebDriverFactory
    {

        public static IWebDriver CreateSelenoidDriver(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return CreateChromeSelenoidDriver();
                case BrowserType.Firefox:
                    return CreateFirefoxSelenoidDriver();
                default:
                    throw new NotSupportedException($"Browser type '{browserType}' is not supported. Supported browser types are: Chrome, Firefox.");
            }
        }

        private static IWebDriver CreateChromeSelenoidDriver()
        {
            var selenoidOptions = new Dictionary<string, object>
            {
                ["name"] = "Chrome Demo Test",
                ["sessionTimeout"] = "15m",
                ["env"] = new List<string> { "TZ=UTC" },
                ["labels"] = new Dictionary<string, object> { ["manual"] = "true" },
                ["enableVideo"] = false,
                ["enableVNC"] = true
            };

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddAdditionalOption("selenoid:options", selenoidOptions);

            return new RemoteWebDriver(new Uri("http://localhost:8085"), chromeOptions);
        }

        private static IWebDriver CreateFirefoxSelenoidDriver()
        {
            var selenoidOptions = new Dictionary<string, object>
            {
                ["name"] = "Firefox Demo Test",
                ["sessionTimeout"] = "15m",
                ["env"] = new List<string> { "TZ=UTC" },
                ["labels"] = new Dictionary<string, object> { ["manual"] = "true" },
                ["enableVideo"] = false,
                ["enableVNC"] = true
            };

            var firefoxOptions = new ChromeOptions();
            firefoxOptions.AddAdditionalOption("selenoid:options", selenoidOptions);

            return new RemoteWebDriver(new Uri("http://localhost:8085"), firefoxOptions);
        }
    }



    public enum BrowserType
    {
        Chrome,
        Firefox
    }
}
