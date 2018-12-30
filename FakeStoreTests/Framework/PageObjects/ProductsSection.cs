using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using FrameworkHelpers.Helpers;
using OpenQA.Selenium;

namespace Framework.PageObjects
{
    public abstract class ProductsSection : BasePage
    {
        public ProductsSection(IWebDriver driver) : base(driver){}
        protected By SectionBy;
        private By AddToCartButtonBy => By.CssSelector("a.add_to_cart_button");
        private By ViewCartButtonBy => By.CssSelector(".added_to_cart");
        private By ProductPriceBy => By.CssSelector(":not(del)> .woocommerce-Price-amount");
        private By ProductNameBy => By.CssSelector(".woocommerce-loop-product__title");

        public MainPage AddProductToCart(int productNumber)
        {        
            GetProductContainer(productNumber).FindElement(AddToCartButtonBy).Click();            
            return new MainPage(Driver);
        }

        private IWebElement GetProductContainer(int productNumber)
        {
            return Driver.FindElement(SectionBy).FindElement(By.CssSelector("li:nth-of-type(" + productNumber.ToString() + ")"));
        }

        public Product GetProduct(int productNumber)
        {
            var textPrice = GetProductContainer(productNumber).FindElement(ProductPriceBy).Text;
            var product = new Product
            {
                Name = GetProductContainer(productNumber).FindElement(ProductNameBy).Text,
                Price = Helpers.ConvertPriceToNumber(textPrice)
            };                        
            return product;
        }

        public ProductPage GoToProductPage(int productNumber)
        {
            GetProductContainer(productNumber).FindElement(ProductNameBy).Click();
            return new ProductPage(Driver);
        }

        public CartPage GotoCart(int productNumber)
        {
            WaitForElementPresent(ViewCartButtonBy, TimeSpan.FromSeconds(5)).Click();
            return new CartPage(Driver);
        }        
    }
}