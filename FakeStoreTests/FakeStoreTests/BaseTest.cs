using Framework;
using NUnit.Framework;
using OpenQA.Selenium;

namespace FakeStoreTests
{
    public class BaseTest
    {
        public IWebDriver Driver { get; set; }

        [SetUp]
        public void DriverSetUp()
        {
            Driver = WebDriverFactory.GetDriver();
        }

        [TearDown]
        public void DriverQuitAndClose()
        {
            WebDriverFactory.Quit();
        }
    }
}
