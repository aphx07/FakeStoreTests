using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.PageObjectsk
{
    public class Cockpit : BasePage
    {
        public Cockpit(IWebDriver driver) : base(driver) { }

        public IWebElement DisplayedUsername => Driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content'] strong:first-child"));
    }
}