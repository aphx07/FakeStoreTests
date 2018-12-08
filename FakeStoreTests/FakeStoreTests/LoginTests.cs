using System;
using System.Collections.Generic;
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
        static public string Password { get => "m^&vApNUQ#WjuQWUFnj)8G22"; }
        static public string ExpectedUser { get => "Geralt z Rivii"; }        
        public string ExpectedErrorMessage { get; set; }
        public string ActualErrorMessage { get; set; }
        public string AccountContent { get; set; }

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
            ExpectedErrorMessage = null;
            AccountContent = null;
            ActualErrorMessage = null;
            Driver.Close();
            Driver.Quit();
        }

        [TestCase("test@testelka.pl")]
        [TestCase("TestowyUser")]
        public void SuccessfulLogin(string username)
        {
            Login(username, Password);
            AccountContent = Driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content']")).Text;
            Assert.True(AccountContent.Contains(ExpectedUser), ExpectedUser + " username was not found after login. \nText after login was: " + AccountContent);
        }

        [TestCase("TestowyUser", "dummy", "BŁĄD: Wprowadzone hasło dla nazwy użytkownika TestowyUser nie jest poprawne. Nie pamiętasz hasła?")]
        [TestCase("test@testelka.pl", "dummy", "BŁĄD: Dla adresu e-mail test@testelka.pl podano nieprawidłowe hasło. Nie pamiętasz hasła?")]
        [TestCase("Test", "dummy", "BŁĄD: Nieprawidłowa nazwa użytkownika. Nie pamiętasz hasła?")]
        [TestCase("elo@testelka.pl", "dummy", "BŁĄD: Nieprawidłowy adres e-mail. Nie pamiętasz hasła?")]
        [TestCase("", "dummy", "Błąd: Nazwa użytkownika jest wymagana.")]
        [TestCase("elo@testelka.pl", "", "BŁĄD: Pole „Hasło” jest puste.")]
        public void UnsuccessfulLogin(string username, string password, string expectedErrorMessage)
        {
            Login(username, password);
            ActualErrorMessage = Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']")).Text;
            Assert.AreEqual(expectedErrorMessage, ActualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + ActualErrorMessage);
        }

        private void Login(string username, string password)
        {
            Driver.FindElement(By.CssSelector("input[id='username']")).SendKeys(username);
            Driver.FindElement(By.CssSelector("input[id='password']")).SendKeys(password);
            Driver.FindElement(By.CssSelector("button[name='login']")).Click();
        }
    }
}
