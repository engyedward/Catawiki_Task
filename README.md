# Catawiki_Task
This project was done using Selenium Webdriver, NUnit, using POM design pattern. 

Following POM design pattern:
- a separate class is created for each web page, the class includes the page elements and functions.
- a separate class is created for each test feature [in our task, it's only Testing Search feature]

NameSpace "Helpers" is created to hold all helper functions as follows:
- TestHelper.cs holds all the general actions that can be used in any other UI automation project other than Catawiki (e.g. Open URL, Click a button, Type text, Log issue, initialize browser,...)
- Common.cs [inherits from the Test Helper] and it holds all the general functions that are specific to Catawiki and not specific to one page object.
- BaseTest.cs [inherits from the Common] holds the setup, teardown, onetimesetup that should be carried out with each test case or test run, and test classes inherits from it.

Test Case Details:
- Open https://www.catawiki.com/en/
- At the top of the page in search field, type a search keyword "train"
- click magnifier button within the search field
- Assert that the search results page is opened - by looking for a known control in the page if it's visible or not
- Click on a specific search result (by an index)
- Assert that Lot’s page is opened - by looking for a known control in the page if it's visible or not
- Print to consol (lot's name, “favorites” counter, current bid)

Other Cases Handled:
- Keyword and index are put inside TestCase annotation in order to be variable
- If the Search Keyword didn't get Results, and so the index provided will be out of range, Assertion of clicking the item by its index will fail with an assertion message printed
- Clicking the "Accept Cookie" button is put as a common function, and the code checks first if it is visible only it will be clicked.
