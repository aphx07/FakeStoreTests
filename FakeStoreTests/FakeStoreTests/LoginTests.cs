using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FakeStoreTests
{
    [TestFixture]
    public class LoginTests
    {
        IWebDriver driver;
        [SetUp]
        public void DriverSetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://fakestore.testelka.pl/moje-konto/");
        }

        [TearDown]
        public void DriverQuitAndClose()
        {
            driver.Close();
            driver.Quit();
        }        

        [Test]
        public void LoginWithEmail()
        {
            var email = "test@testelka.pl";
            var password = "";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var user = "Geralt z Rivii";

            Assert.True(driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text.Contains(user));

        }

        [Test]
        public void LoginWithUsername()
        {
            var email = "TestowyUser";
            var password = "";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var user = "Geralt z Rivii";

            Assert.True(driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text.Contains(user));
        }
    }
}
