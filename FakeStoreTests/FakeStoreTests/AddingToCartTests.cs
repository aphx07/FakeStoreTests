using Framework;
using Framework.PageObjects;
using FrameworkHelpers.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeStoreTests
{
    public class AddingToCartTests : BaseTest
    {
        public MainPage MainPage { get; set; }
        public CartPage CartPage { get; set; }
        public ProductPage ProductPage { get; set; }

        [SetUp]
        public void GoToLoginPage()
        {
            MainPage = new MainPage(Driver).GoTo();
            MainPage.DimissDemoNotice();
        }

        [Test]
        public void AddProductFromMainPage()
        {
            MainPage.GetProductsSection(Section.Popular).AddProductToCart(2);
            var expectedProduct = MainPage.GetProductsSection(Section.Popular).GetProduct(2);
            CartPage = MainPage.GetProductsSection(Section.Popular).GotoCart(2);
            var products = CartPage.GetProducts();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, products.Count, "Number of products is not what expected.");
                Assert.AreEqual(expectedProduct.Price, products[0].Price, "Price of the product is not what expected.");
                Assert.AreEqual(expectedProduct.Name, products[0].Name, "Name of the product is not what expected.");
            });

        }
        [Test]
        public void AddProductFromProductPage()
        {
            ProductPage = MainPage.GetProductsSection(Section.Popular).GoToProductPage(3);
            var expectedProduct = ProductPage.GetProduct();
            var products = ProductPage.AddToCart().GoToCard().GetProducts();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(1, products.Count, "Number of products is not what expected.");
                Assert.AreEqual(expectedProduct.Price, products[0].Price, "Price of the product is not what expected.");
                Assert.AreEqual(expectedProduct.Name, products[0].Name, "Name of the product is not what expected.");
            });
        }        
    }
}
