using CatawikiTask.Helpers;
using CatawikiTask.Pages;
using CatawikiTask.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Tests
{
    [TestFixture]
    class SearchTests: BaseTest
    {
        public Homepage homePage;
        public SearchResultsPage searchResultsPage;
        public LotPage lotPage;

        [TestCase("train", 1)]
        public void Search(string keyword, int index)
        {            
            homePage = new Homepage(Driver, Wait);
            searchResultsPage = new SearchResultsPage(Driver, Wait);
            lotPage = new LotPage(Driver, Wait);

            //Open Catawiki website
            GoToUrl(this.Settings.Production_URL);

            //Enter search keyword
            homePage.SearchByKeyword(keyword);

            //Verify that Search Results Page has been opened
            Assert.IsTrue(searchResultsPage.Verify_SearchResultsPage_Opened(), "Search Results Page didn't open");

            //Click on intem #index in the search results page
            Assert.IsTrue(searchResultsPage.Click_LotCard(index), "Couldn't click item number " + index);

            //Verify that lot details page has been opened
            Assert.IsTrue(lotPage.Verify_LotDetailsPage_Opened());

            //Get lot details (Name, Current bid amount, Favorites counter
            Lot lot = lotPage.GetLotDetails();

            //Print lot details to consol
            var lotDetails = new[] { "Lot title: " + lot.title, "Lot Current Bid: " + lot.currentBid, "Lot Favorites Counter: " + lot.favoritesCounter};
            Console.Out.WriteLine(string.Join(Environment.NewLine, lotDetails));
            

        }
    }
}
