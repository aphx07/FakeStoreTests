using OpenQA.Selenium;
using System.Configuration;

namespace Framework.PageObjects
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public IWebElement ErrorMessage => Driver.FindElement(By.CssSelector("ul[class='woocommerce-error']"));
        private IWebElement UsernameField => Driver.FindElement(By.CssSelector("input[id='username']"));
        private IWebElement PasswordField => Driver.FindElement(By.CssSelector("input[id='password']"));
        private IWebElement LogginButton => Driver.FindElement(By.CssSelector("button[name='login']"));

        public LoginPage GoTo()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["homePageUrl"] + "/moje-konto/");
            return this;
        }

        public MyAccountPage LoginAsCustomer(string username, string password)
        {
            ProvideCredentialsAndSubmit(username, password);
            return new MyAccountPage(Driver);
        }      

        public LoginPage LoginWithBadCredentials(string username, string password)
        {
            ProvideCredentialsAndSubmit(username, password);
            return this;
        }

        private void ProvideCredentialsAndSubmit(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LogginButton.Click();
        }
    }
}