using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace Framework
{
    public class WebDriverFactory
    {
        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver==null)
            {
                _driver = CreateDriver(GetBrowser());
            }
            _driver.Manage().Window.Maximize();
            return _driver;
        }

        public static void Quit()
        {
            if (null != _driver)
            {
                _driver.Quit();
            }
            _driver = null;
        }

        private static BrowserType GetBrowser()
        {
            var browserName = ConfigurationManager.AppSettings["browser"];
            if (browserName == "chrome")
            {
                return BrowserType.Chrome;
            }
            else if (browserName == "firefox")
            {
                return BrowserType.Firefox;
            }
            else
            {
                throw new ArgumentNullException("Browser is not provided in the config or the browser is not correct.");
            }
        }
        

        private static IWebDriver CreateDriver(BrowserType browser)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    return GetChromeDriver();                                     
                case BrowserType.Firefox:
                    return GetFirefoxDriver();
                default:
                    throw new ArgumentOutOfRangeException("There is no configuration for such browser.");
            }
        }

        private static IWebDriver GetChromeDriver()
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers";
            return _driver = new ChromeDriver(driverDirectory);
        }
        private static IWebDriver GetFirefoxDriver()
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers";
            return _driver = new FirefoxDriver(driverDirectory);
            
        }

    }
}
