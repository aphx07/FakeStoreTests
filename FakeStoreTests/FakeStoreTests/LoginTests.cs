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

            var expectedUser = "Geralt z Rivii";
            var accountContent = driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text;

            Assert.True(accountContent.Contains(expectedUser), expectedUser + " username was not found after login. \nText after login was: " + accountContent);

        }

        [Test]
        public void LoginWithUsername()
        {
            var username = "TestowyUser";
            var password = "";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedUser = "Geralt z Rivii";
            var accountContent = driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text;

            Assert.True(accountContent.Contains(expectedUser), expectedUser + " username was not found after login. \nText after login was: " + accountContent);
        }

        [Test]
        public void UsernameAndWrongPassword()
        {
            var username = "TestowyUser";
            var password = "dummy";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "BŁĄD: Wprowadzone hasło dla nazwy użytkownika " + username + " nie jest poprawne. Nie pamiętasz hasła?";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, 
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void EmailAndWrongPassword()
        {
            var email = "test@testelka.pl";
            var password = "dummy";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "BŁĄD: Dla adresu e-mail " + email + " podano nieprawidłowe hasło. Nie pamiętasz hasła?";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NonExististentUsername()
        {
            var username = "test";
            var password = "dummy";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "BŁĄD: Nieprawidłowa nazwa użytkownika. Nie pamiętasz hasła?";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NonExistentEmail()
        {
            var email = "test@elo.pl";
            var password = "dummy";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(email);
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "BŁĄD: Nieprawidłowy adres e-mail. Nie pamiętasz hasła?";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NoUsername()
        {
            var password = "dummy";
            driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "Błąd: Nazwa użytkownika jest wymagana.";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NoPassword()
        {
            var username = "test";
            driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            driver.FindElement(By.CssSelector("button[name='login']")).Click();

            var expectedErrorMessage = "BŁĄD: Pole „Hasło” jest puste.";
            var actualErrorMessage = driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }
    }
}
