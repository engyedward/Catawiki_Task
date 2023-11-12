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
    class SearchResultsPage : TestHelper
    {
        public SearchResultsPage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }

        private By Btn_SaveYourSearch = By.CssSelector("[data-testid='sticky-filters'] [data-testid='save-your-search_button']");
        private By Links_LotCards = By.CssSelector(".c-extended-lot-card a");
        
        public bool Verify_SearchResultsPage_Opened()
        {
            return IsElementVisible(Btn_SaveYourSearch, "Button Save Your Search");
        }

        public bool Click_LotCard(int index)
        {
            bool isSucceeded = ClickOn_ByIndexInList(Links_LotCards, index, "List of Lots Cards");

            return isSucceeded;

        }

    }
}
