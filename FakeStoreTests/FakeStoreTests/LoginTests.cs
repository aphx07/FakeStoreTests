using Framework;
using Framework.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace FakeStoreTests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {        
        static public string Password => "7Eh!m6hSAXn9f1Rxl0Zoh6sl";
        static public string ExpectedUser => "Geralt z Rivii";        
        public string ExpectedErrorMessage { get; set; }
        public string ActualErrorMessage { get; set; }
        public LoginPage LoginPage { get; set; }

        [SetUp]
        public void GoToLoginPage()
        {
            LoginPage = new LoginPage(Driver).GoTo();
        }

        [TearDown]
        public void CleanUp()
        {
            ExpectedErrorMessage = null;            
            ActualErrorMessage = null;            
        }

        [TestCase("test@testelka.pl")]
        [TestCase("TestowyUser")]
        public void SuccessfulLogin(string username)
        {
            var accountContent = LoginPage.LoginAsCustomer(username, Password).
                Cockpit.DisplayedUsername.Text;            
            Assert.True(accountContent.Contains(ExpectedUser), ExpectedUser + " username was not found after login. \nText after login was: " + accountContent);
        }

        [TestCase("TestowyUser", "dummy", "BŁĄD: Wprowadzone hasło dla nazwy użytkownika TestowyUser nie jest poprawne. Nie pamiętasz hasła?")]
        [TestCase("test@testelka.pl", "dummy", "BŁĄD: Dla adresu e-mail test@testelka.pl podano nieprawidłowe hasło. Nie pamiętasz hasła?")]
        [TestCase("Test", "dummy", "BŁĄD: Nieprawidłowa nazwa użytkownika. Nie pamiętasz hasła?")]
        [TestCase("elo@testelka.pl", "dummy", "BŁĄD: Nieprawidłowy adres e-mail. Nie pamiętasz hasła?")]
        [TestCase("", "dummy", "Błąd: Nazwa użytkownika jest wymagana.")]
        [TestCase("elo@testelka.pl", "", "BŁĄD: Pole „Hasło” jest puste.")]
        public void UnsuccessfulLogin(string username, string password, string expectedErrorMessage)
        {
            ActualErrorMessage = LoginPage.LoginWithBadCredentials(username, password).ErrorMessage.Text;
            Assert.AreEqual(expectedErrorMessage, ActualErrorMessage,
                "Error message after failed login was different than expected. \nExpected: " + expectedErrorMessage + "\nActual: " + ActualErrorMessage);
            
        }        
    }
}
