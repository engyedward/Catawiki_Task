using CatawikiTask.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CatawikiTask.Pages
{
    class Homepage : TestHelper
    {
        public Homepage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }
        private By txt_Search = By.CssSelector("[data-testid='search-field']");
        //private By txt_Search = By.Id("textField11");
        private By btn_Search = By.CssSelector(".c-search-field div div button");
        private By btn_Cookies = By.Id("cookie_bar_agree_button");

        
        public void SearchByKeyword(string searchKeyWork)
        {
            ClickOn(btn_Cookies, "Accept Cookies Button");
            TypeText(txt_Search, searchKeyWork, "Search Box");
            ClickOn(btn_Search, "Button Search");
        }
    }
}
