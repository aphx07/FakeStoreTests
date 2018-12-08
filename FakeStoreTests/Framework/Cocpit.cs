using OpenQA.Selenium;

namespace Framework
{
    public class Cocpit : BasePage
    {
        public Cocpit(IWebDriver driver) : base(driver) { }

        public IWebElement DisplayedUsername => Driver.FindElement(By.CssSelector("div[class='woocommerce-MyAccount-content'] strong:first-child"));
    }
}