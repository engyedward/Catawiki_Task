using CatawikiTask.Helpers;
using CatawikiTask.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Pages
{
    class LotPage : TestHelper
    {
        public LotPage(IWebDriver driver, WebDriverWait wait)
        {
            this.Driver = driver;
            this.Wait = wait;
        }

        private By lbl_lotTitle = By.CssSelector("div[class*=main-content] h1");
        private By lbl_bidAmount = By.CssSelector("div[class*=bid-amount]");
        private By lbl_favoritesCounter = By.CssSelector("div[class*=be-lot-details-scrollable-gallery__container] span");


        public bool Verify_LotDetailsPage_Opened()
        {
            return IsElementVisible(lbl_lotTitle, "Lot Name ");
        }

        public Lot GetLotDetails()
        {
            Lot lot=new Lot();
            lot.title = GetTextOf(lbl_lotTitle);
            lot.currentBid= GetTextOf(lbl_bidAmount);
            lot.favoritesCounter = GetTextOf(lbl_favoritesCounter);

            return lot;
            
        }
    }
}
