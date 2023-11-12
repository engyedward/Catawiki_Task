using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Helpers
{
    class Common : TestHelper
    {
        private By btn_Cookies = By.Id("cookie_bar_agree_button");

        /// <summary>
        /// Clicks on Accept Cookies button if it is visible
        /// </summary>
        public void CheckForCookieAlert()
        {
            if(IsElementVisible (btn_Cookies,"Accept Cookies button")) 
                ClickOn(btn_Cookies, "Accept Cookies Button");
        }

    }
}
