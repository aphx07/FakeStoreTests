using System;
using System.Collections.Generic;
using FrameworkHelpers.Helpers;
using OpenQA.Selenium;

namespace Framework.PageObjects
{
    public class CartPage : BasePage
    {
        public CartPage(IWebDriver driver) : base(driver) { }
        private By ProductNameBy => By.CssSelector("td.product-name");
        private By ProductPriceBy => By.CssSelector("td.product-price");
        private By ProductCointainersBy => By.CssSelector("tr.cart_item");

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            var productContainers = Driver.FindElements(ProductCointainersBy);
            var product = new Product();
            foreach (var productItem in productContainers)
            {
                var textPrice = productItem.FindElement(ProductPriceBy).Text;
                product.Name = productItem.FindElement(ProductNameBy).Text;
                product.Price = Helpers.ConvertPriceToNumber(textPrice);
                products.Add(product);
            }
            return products;
        }
    }
}