using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatawikiTask.Helpers
{
    class BaseTest : TestHelper
    {
        public TestContext TestContext { get; set; }

        [OneTimeSetUp]
        public void DeleteLogFile()
        {
            if (File.Exists(SetDir("IssuesLogFile.txt")))
                File.Delete(SetDir("IssuesLogFile.txt"));
        }

        [SetUp]
        public void Setup()
        {
            Driver = Initialize(out Wait, timeToWaitInMinutes: Settings.timeToWaitInMinutes);
        }

        [TearDown]
        public void CleanUp()
        {
            Driver.Close();
            Driver.Dispose();

        }
    }
}
