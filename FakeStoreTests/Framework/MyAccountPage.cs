using OpenQA.Selenium;

namespace Framework
{
    public class MyAccountPage : BasePage
    {
        public MyAccountPage(IWebDriver driver) : base(driver)
        {
            Cocpit = new Cocpit(Driver);
        }       

        public Cocpit Cocpit { get; set; }


    }
}