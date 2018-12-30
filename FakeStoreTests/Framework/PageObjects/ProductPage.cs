using System;
using FrameworkHelpers.Helpers;
using OpenQA.Selenium;

namespace Framework.PageObjects
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }
        private By ProductTitleBy => By.CssSelector(".product_title");
        private By ProductPriceBy => By.CssSelector(".summary :not(del)> .woocommerce-Price-amount");
        private By AddToCartBy => By.CssSelector("[name='add-to-cart']");
        private By SuccessMessageBy => By.CssSelector("[role='alert']");

        public Product GetProduct()
        {
            var textPrice = Driver.FindElement(ProductPriceBy).Text;
            var product = new Product
            {
                Name = Driver.FindElement(ProductTitleBy).Text,
                Price = Helpers.ConvertPriceToNumber(textPrice)
            };
            return product;
        }

        public ProductPage AddToCart()
        {
            Driver.FindElement(AddToCartBy).Click();
            return new ProductPage(Driver);
        }

        public CartPage GoToCard()
        {
            Driver.FindElement(SuccessMessageBy).FindElement(By.CssSelector(".wc-forward")).Click();
            return new CartPage(Driver);
        }
    }
}