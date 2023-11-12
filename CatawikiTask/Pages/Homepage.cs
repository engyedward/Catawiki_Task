using CatawikiTask.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CatawikiTask.Pages
{
    class Homepage : Common
    {
        public Homepage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }
        private By txt_Search = By.CssSelector("[data-testid='search-field']");
        private By btn_Search = By.CssSelector(".c-search-field div div button");



        /// <summary>
        /// Search By Keyword :  Enter keyword in the Search Box and Clicks Magnifier button
        /// </summary>
        /// <param name="searchKeyWork">The Keyword that will be typed in the Search Box</param>
        public void SearchByKeyword(string searchKeyWork)   
        {
            TypeText(txt_Search, searchKeyWork, "Search Box");
            ClickOn(btn_Search, "Button Search");
        }
    }
}
