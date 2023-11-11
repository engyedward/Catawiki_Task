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
        

        protected void TypeText(By locator, string value, string locatorDescription = "")
        {
            IWebElement element = null;
            try
            {
                element = Wait.Until(ExpectedConditions.ElementExists(locator));
                Wait.Until(d => element.Displayed);
                Wait.Until(d => element.Enabled);
                element.SendKeys(value);
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod() + " trying to find locator " + locatorDescription);
                throw e;
            }
        }

        protected virtual void ClickOn(By locator, string locatorDescription = "")
        {
            //IWebElement element = null;
            try
            {           
                var element = Wait.Until(ExpectedConditions.ElementExists(locator));
                Wait.Until(d => element.Displayed);
                Wait.Until(d => element.Enabled);

                element.Click();

               
            }
            catch (Exception e)
            {
                LogIssue("Exception occurred in function \"" + GetCurrentMethod() + "\". trying to find '" + locatorDescription + "' : " + e.Message);

                throw e;
            }

        }

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
                LogIssue("Exception occurred in function \"" + GetCurrentMethod() + "\". trying to find button " + locatorDescription + " : " + e.Message);

                throw e;
            }

            return (outOfIndex ? false : true);
            
        }


        public virtual IWebElement FindElement(By locator)
        {
            return Driver.FindElement(locator);
        }

        public virtual IList<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }

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

        public static void LogIssue( string textToBeSaved)
        {
            Log("IssuesLogFile",  textToBeSaved);
        }

        public static void Log(string fileName, string textToBeSaved)
        {

            var path = SetDir(fileName.Contains(".txt") ? fileName : (fileName + ".txt"));
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

        public static string SetDir(string folderName)
        {
            return TestContext.CurrentContext.TestDirectory + (string.IsNullOrEmpty(folderName) ? "" : "\\" + folderName);
        }

        public static string GetCurrentMethod(int level = 1)
        {

            var st = new StackTrace();
            var sf = st.GetFrame(level);

            return sf.GetMethod().Name;

        }

        public virtual IWebDriver Initialize(out WebDriverWait wait, double timeToWaitInMinutes = 2)
        {
            try
            {
                var driversDirectory = TestContext.CurrentContext.TestDirectory;

                IWebDriver driver;

                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("test-type");
                chromeOptions.AddArguments("start-maximized");
                chromeOptions.AddArguments("--js-flags=--expose-gc");
                chromeOptions.AddArguments("--enable-precise-memory-info");
                chromeOptions.AddArguments("--disable-popup-blocking");
                chromeOptions.AddArguments("--disable-default-apps");
                chromeOptions.AddArguments("test-type=browser");
                chromeOptions.AddArguments("disable-infobars");
                chromeOptions.AddArguments("--disable-notifications");
                chromeOptions.AddArguments("--disable-device-discovery-notifications");
                //chromeOptions.AddArguments("disable-features=DownloadUI");
                chromeOptions.AddExcludedArgument("enable-automation");
                chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.Severe);
                chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                chromeOptions.AddUserProfilePreference("profile.exit_type", "none");


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
