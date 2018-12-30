using System;
using System.Configuration;
using FrameworkHelpers.Helpers;
using OpenQA.Selenium;

namespace Framework.PageObjects
{
    public class MainPage : BasePage
    {
        public MainPage(IWebDriver driver) : base(driver)
        {
            ProductsSection = new PopularProductsSection(Driver);
        }

        public ProductsSection ProductsSection { get; set; }

        private IWebElement PopularProductsSection => Driver.FindElement(By.CssSelector("section.storefront-popular-products"));
             

        public MainPage GoTo()
        {
            Driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["homePageUrl"]);
            return this;
        }

        public ProductsSection GetProductsSection(Section section)
        {  
            switch (section)
            {
                case Section.Popular:
                    return new PopularProductsSection(Driver);
                default:
                    throw new NotImplementedException("This type of section is not supported");
            }            
        }
    }
}