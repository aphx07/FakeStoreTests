using OpenQA.Selenium;

namespace Framework
{
    public class MyAccountPage : BasePage
    {
        public MyAccountPage(IWebDriver driver) : base(driver)
        {
            Cockpit = new Cockpit(Driver);
        }       

        public Cockpit Cockpit { get; set; }


    }
}