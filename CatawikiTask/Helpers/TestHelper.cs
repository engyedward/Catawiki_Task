using CatawikiTask.Properties;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Helpers
{
    class TestHelper
    {
        internal Settings Settings = Properties.Settings.Default;

        /// <summary>
        /// Global Driver used throughout the TestHelper Functions
        /// </summary>
        protected IWebDriver Driver;
        /// <summary>
        /// Global Wait used throughout the TestHelper Functions
        /// </summary>
        protected WebDriverWait Wait;

        /// <summary>
        /// Type Text: Tries to type a given Text in an Element using its By Locator 
        /// </summary>
        /// <param name="locator">By Locator of Element that needs to be Clicked</param>
        /// <param name="text">to be typed in the Element</param>
        /// <param name="locatorDescription">Description of the Element for Logging</param>
        protected void TypeText(By locator, string text, string locatorDescription = "")
        {
            try
            {
                var element = Wait.Until(ExpectedConditions.ElementExists(locator));
                Wait.Until(d => element.Displayed);
                Wait.Until(d => element.Enabled);
                element.SendKeys(text);
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod(2) + " trying to find locator " + locatorDescription);
                throw e;
            }
        }

        /// <summary>
        /// Click on Element: Tries to Click on an Element using its By Locator 
        /// </summary>
        /// <param name="locator">By Locator of Element that needs to be Clicked</param>
        /// <param name="locatorDescription">Description of the Element for Logging</param>
        protected virtual void ClickOn(By locator, string locatorDescription = "")
        {
            try
            {           
                var element = Wait.Until(ExpectedConditions.ElementExists(locator));
                Wait.Until(d => element.Displayed);
                Wait.Until(d => element.Enabled);

                element.Click();
               
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod(2) + "\". trying to find '" + locatorDescription + "' : " + e.Message);

                throw e;
            }

        }

        /// <summary>
        /// Click on Element By Index In List: Gets list of elements by locator, then try to click an Element using its index in the list
        /// </summary>
        /// <param name="locator">By Locator that gets list of Elements
        /// <param name="Index">of the Element in the list to be Clicked</param>
        /// <param name="locatorDescription">Description of the Element for Logging</param>
        protected virtual bool ClickOn_ByIndexInList(By locator, int index, string locatorDescription = "")
        {
            bool outOfIndex=false;
            try
            {
                IList<IWebElement>  ElementsList = FindElements(locator);

                if(ElementsList.Count <= index)
                { outOfIndex = true; }
                else
                { ElementsList[index].Click(); }
               
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod(2) + "\". trying to find button " + locatorDescription + " : " + e.Message);

                throw e;
            }

            return (outOfIndex ? false : true);
            
        }

        /// <summary>
        /// Finds the first element using the given locator
        /// </summary>
        /// <param name="locator">By locator of the element</param>
        /// <returns>Element found</returns>
        public virtual IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        /// <summary>
        /// Finds all elements using the given locator
        /// </summary>
        /// <param name="locator">By locator of the elements</param>
        /// <returns>List of elements found</returns>
        public virtual IList<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

        /// <summary>
        /// Is Element Visible: Checks whether an Element is Visible or Not
        /// </summary>
        /// <param name="locator">By Locator of Element that need to be checked</param>
        /// <param name="locatorDescription">Description of the Element for Logging</param>
        /// <returns>True in case element is visible and False in case element is not visible</returns>
        public virtual bool IsElementVisible(By locator, string locatorDescription = "")
        {
            try
            {

                return FindElement(locator).Displayed;

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Text of: gets the text of the element sent as parameter
        /// </summary>
        /// <param name="locator">Locator of the Element whose text will be returned</param>
        /// <param name="locatorDescription">Description of the Element for Logging</param>
        /// <returns>The Text of the Element</returns>
        public virtual string GetTextOf(By locator, string locatorDescription = "")
        {

            try
            {
                return FindElement(locator).Text;
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod() + "\". trying to find locator " + locatorDescription + " : " + e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Create Or Update Issues And Errors To Save Text: Creates or Updates "IssuesLogFile.txt" File based on the Given Text
        /// </summary>
        /// <param name="textToBeSaved">Text that is Required to be Logged</param>
        public static void LogIssue( string textToBeSaved)
        {
            var path = SetDir("IssuesLogFile.txt");
            TextWriter tw;

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
                tw = new StreamWriter(path);
            }
            else
            {
                tw = new StreamWriter(path, true);
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Insert(0, "Date and Time: " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + Environment.NewLine);

            stringBuilder.Append("" + textToBeSaved + Environment.NewLine);
            stringBuilder.Append("===========================================" + Environment.NewLine);


            stringBuilder.AppendLine();

            tw.WriteLine(stringBuilder.ToString());
            TestContext.WriteLine(stringBuilder.ToString());

            tw.Close();
        }

        /// <summary>
        /// set Directory: Get the full path of certain Folder inside the Project
        /// </summary>
        /// <param name="folderName">Container Folder inside the Project </param>
        /// <returns>The File Full Path</returns>
        public static string SetDir(string folderName)
        {
            return TestContext.CurrentContext.TestDirectory + (string.IsNullOrEmpty(folderName) ? "" : "\\" + folderName);
        }

        /// <summary>
        /// Get Current Method: Returns the Name of the Method in Given Level, By Default it Returns the Name of the Method Calling the GetCurrentMethod Function
        /// </summary>
        /// <param name="level">Index of the Method whose Name will be Returned in the Call Stack</param>
        /// <returns></returns>
        public static string GetCurrentMethod(int level = 1)
        {

            var st = new StackTrace();
            var sf = st.GetFrame(level);

            return sf.GetMethod().Name;

        }

        /// <summary>
        /// Initialize: Open New Chrome Browser and Return the Driver and the Explicit Wait
        /// <param name="wait">Predefined WebDriverWait for the Created Web Driver returned as an out parameter</param>
        /// <param name="timeToWaitInMinutes">Time in Minutes to Define the Wait (Explicit and Implicit) with</param>
        /// <returns>Web Driver that will be Used</returns>
        public virtual IWebDriver Initialize(out WebDriverWait wait, double timeToWaitInMinutes = 2)
        {
            try
            {
                var driversDirectory = TestContext.CurrentContext.TestDirectory;

                IWebDriver driver;

                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("test-type");
                chromeOptions.AddArguments("start-maximized");
                chromeOptions.AddArguments("--disable-popup-blocking");
                chromeOptions.AddArguments("--disable-default-apps");
                chromeOptions.AddArguments("test-type=browser");
                chromeOptions.AddArguments("disable-infobars");
                chromeOptions.AddArguments("--disable-notifications");
                chromeOptions.AddArguments("--disable-device-discovery-notifications");
                chromeOptions.AddExcludedArgument("enable-automation");

                driver = new ChromeDriver(driversDirectory, chromeOptions, TimeSpan.FromMinutes(timeToWaitInMinutes));
                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(timeToWaitInMinutes));

                return driver;
            }


            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod());
                throw e;
            }


        }

        /// <summary>
        /// Navigate to given URL
        /// </summary>
        /// <param name="url">URL to go to</param>
        public virtual void GoToUrl(string url)
        {

            try
            {
                Driver.Url = url;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
