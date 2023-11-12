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
    class SearchResultsPage : Common
    {
        public SearchResultsPage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }

        private By Btn_SaveYourSearch_withResults = By.CssSelector("[data-testid='sticky-filters'] [data-testid='save-your-search_button']");
        private By Btn_SaveYourSearch_noResults = By.CssSelector("[data-testid='save-your-search_button']");
        private By Links_LotCards = By.CssSelector(".c-extended-lot-card a");


        /// <summary>
        /// Verifies that Search Results Page is Opened by Trying to Locate Button "Save Your Search" in the page
        /// </summary>
        /// <returns>True if Button "Save Your Search" has been found in the page, and False if not found</returns>
        public bool Verify_SearchResultsPage_Opened()
        {
            bool PageOpened;

            if (FindElements(Links_LotCards).Count == 0)
            { PageOpened = IsElementVisible(Btn_SaveYourSearch_noResults, "Button Save Your Search while no Results"); }
            else 
            { PageOpened = IsElementVisible(Btn_SaveYourSearch_withResults, "Button Save Your Search while Results exist"); }

            return PageOpened;
        }

        /// <summary>
        /// Clicks on a specific search result item by its index in the search results grid
        /// </summary>
        /// <param name="index"></param>
        /// <returns>True if it managed to click on the item by its index (if the list is shorter than expected, it will return False)</returns>
        public bool Click_LotCard(int index)
        {
            bool isSucceeded = ClickOn_ByIndexInList(Links_LotCards, index, "Item #"+index+ " in List of Lots Cards");

            return isSucceeded;

        }

    }
}
