using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Framework.PageObjects
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; set; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }
        private By DemoNoticeDismissBy => By.CssSelector(".woocommerce-store-notice__dismiss-link");
        protected IWebElement WaitForElementPresent(By elementBy, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            return wait.Until(d => Driver.FindElement(elementBy));
        }

        protected void WaitForElementNotDisplayed(By elementBy, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(Driver, timeout);
            wait.Until(d => !Driver.FindElement(elementBy).Displayed);            
        }

        public void DimissDemoNotice()
        {
            Driver.FindElement(DemoNoticeDismissBy).Click();
            WaitForElementNotDisplayed(DemoNoticeDismissBy, TimeSpan.FromSeconds(3));
        }       


    }
}
