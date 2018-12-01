using System;
using Framework;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Framework
{
    [TestFixture]
    public class LoginTests
    {
        IWebDriver Driver { get; set; }
        [SetUp]
        public void DriverSetUp()
        {
            var factory = new WebDriverFactory();
            Driver = factory.Create(BrowserType.Firefox);

            
            Driver.Navigate().GoToUrl("https://fakestore.testelka.pl/moje-konto/");
        }

        [TearDown]
        public void DriverQuitAndClose()
        {
            Driver.Close();
            Driver.Quit();
        }        

        [Test]
        public void LoginWithEmail()
        {
            var email = "test@testelka.pl";
            var password = "";
            Login(email, password);

            var expectedUser = "Geralt z Rivii";
            var accountContent = Driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text;

            Assert.True(accountContent.Contains(expectedUser), expectedUser + " username was not found after login. \nText after login was: " + accountContent);

        }

        [Test]
        public void LoginWithUsername()
        {
            var username = "TestowyUser";
            var password = "";
            Login(username, password);

            var expectedUser = "Geralt z Rivii";
            var accountContent = Driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text;

            Assert.True(accountContent.Contains(expectedUser), expectedUser + " username was not found after login. \nText after login was: " + accountContent);
        }

        [Test]
        public void UsernameAndWrongPassword()
        {
            var username = "TestowyUser";
            var password = "dummy";
            Login(username, password);

            var expectedErrorMessage = "BŁĄD: Wprowadzone hasło dla nazwy użytkownika " + username + " nie jest poprawne. Nie pamiętasz hasła?";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage, 
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void EmailAndWrongPassword()
        {
            var email = "test@testelka.pl";
            var password = "dummy";
            Login(email, password);

            var expectedErrorMessage = "BŁĄD: Dla adresu e-mail " + email + " podano nieprawidłowe hasło. Nie pamiętasz hasła?";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NonExististentUsername()
        {
            var username = "test";
            var password = "dummy";
            Login(username, password);

            var expectedErrorMessage = "BŁĄD: Nieprawidłowa nazwa użytkownika. Nie pamiętasz hasła?";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NonExistentEmail()
        {
            var email = "test@elo.pl";
            var password = "dummy";
            Login(email, password);

            var expectedErrorMessage = "BŁĄD: Nieprawidłowy adres e-mail. Nie pamiętasz hasła?";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NoUsername()
        {
            var username = "";
            var password = "dummy";
            Login(username, password);

            var expectedErrorMessage = "Błąd: Nazwa użytkownika jest wymagana.";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        [Test]
        public void NoPassword()
        {
            var username = "test";
            var password = "";
            Login(username, password);

            var expectedErrorMessage = "BŁĄD: Pole „Hasło” jest puste.";
            var actualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;

            Assert.AreEqual(expectedErrorMessage, actualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + actualErrorMessage);
        }

        private void Login(string username, string password)
        {
            Driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            Driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            Driver.FindElement(By.CssSelector("button[name='login']")).Click();
        }
    }
}
