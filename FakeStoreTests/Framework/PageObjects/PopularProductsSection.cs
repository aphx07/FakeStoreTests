using OpenQA.Selenium;

namespace Framework.PageObjects
{
    internal class PopularProductsSection : ProductsSection
    {
        public PopularProductsSection(IWebDriver driver) : base(driver)
        {
            SectionBy = By.CssSelector("section.storefront-popular-products");
        }
    }
}