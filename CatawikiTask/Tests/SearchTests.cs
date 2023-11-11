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
            GoToUrl(this.Settings.Production_URL);
            homePage = new Homepage(Driver, Wait);
            homePage.SearchByKeyword(keyword);

            searchResultsPage = new SearchResultsPage(Driver, Wait);
            Assert.IsTrue(searchResultsPage.Verify_SearchResultsPage_Opened(), "Search Results Page didn't open");

            Assert.IsTrue(searchResultsPage.Click_LotCard(index), "Couldn't click item number " + index);

            lotPage = new LotPage(Driver, Wait);
            Assert.IsTrue(lotPage.Verify_LotDetailsPage_Opened());

            Lot lot = lotPage.GetLotDetails();

            Console.Out.WriteLine("Lot title: " + lot.title);
            Console.Out.WriteLine("Lot Current Bid: " + lot.currentBid);
            Console.Out.WriteLine("Lot Favorites Counter: " + lot.favoritesCounter);

        }


    }
}
