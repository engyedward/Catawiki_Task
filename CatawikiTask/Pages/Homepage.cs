using CatawikiTask.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Pages
{
    class Homepage : TestHelper
    {
        public Homepage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }

        public By txt_Search = By.Id("textField11");
        public By btn_Search = By.CssSelector(".c-search-field div div button");
        public By btn_Cookies = By.Id("cookie_bar_agree_button");

        
        public void SearchByKeyword(string searchKeyWork)
        {
            ClickOn(btn_Cookies, "Accept Cookies Button");
            TypeText(txt_Search, searchKeyWork, "Search Box");
            ClickOn(btn_Search, "Button Search");
        }

    }
}
