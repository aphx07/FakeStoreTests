using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Framework
{
    public class WebDriverFactory
    {
        public IWebDriver Create(BrowserType browser)
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

        private IWebDriver GetChromeDriver()
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers";
            return new ChromeDriver(driverDirectory);
        }
        private IWebDriver GetFirefoxDriver()
        {
            var driverDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Drivers";
            return new FirefoxDriver(driverDirectory);
            
        }

    }
}
