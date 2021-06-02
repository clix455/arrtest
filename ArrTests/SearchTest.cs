using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace ArrTests
{
    [TestClass]
    public class SearchTest
    {
        private static IWebDriver driver;

        [ClassInitialize]
        public static void SetupTestClass(TestContext testContext)
        {
            driver = WebDriverFactory.CreateChromeWebDriver();
            driver.Navigate().GoToUrl("https://codility-frontend-prod.s3.amazonaws.com/media/task_static/qa_csharp_search/862b0faa506b8487c25a3384cfde8af4/static/attachments/reference_page.html");
        }

        [ClassCleanup]
        public static void CleanupTestResources()
        {
            driver.Quit();
        }

        [TestMethod]
        public void SearchQueryInputExists()
        {
            IWebElement searchInput = driver.FindElement(By.Id("search-input"));
            Assert.IsNotNull(searchInput);
        }


        [TestMethod]
        public void SearchButtonExists()
        {
            IWebElement webElement = driver.FindElement(By.Id("search-button"));
            Assert.IsNotNull(webElement);
        }


        [TestMethod]
        public void EmptySearchIsNotPermitted()
        {
            IWebElement searchInputElement = driver.FindElement(By.Id("search-input"));
            IWebElement webElement = driver.FindElement(By.Id("search-button"));

            searchInputElement.Clear();
            webElement.Click();

            Wait(TimeSpan.FromMilliseconds(150));
            IWebElement emptySearchMessageElement = driver.FindElement(By.Id("error-empty-query"));
            string message = emptySearchMessageElement.Text.Trim();

            Assert.AreEqual("Provide some query", message);
        }

        [TestMethod]
        public void AtleastOneIsland()
        {

            IWebElement searchInputElement = driver.FindElement(By.Id("search-input"));
            IWebElement searchButtonElement = driver.FindElement(By.Id("search-button"));

            searchInputElement.Clear();
            searchInputElement.SendKeys("isla");
            searchButtonElement.Click();
            Wait(TimeSpan.FromMilliseconds(150));
            var webElements = driver.FindElements(By.CssSelector("#search-results li"));

            Assert.IsTrue(webElements.Count > 0);
        }

        [TestMethod]
        public void NoCastlesFound()
        {
            IWebElement searchInputElement = driver.FindElement(By.Id("search-input"));
            IWebElement searchButtonElement = driver.FindElement(By.Id("search-button"));

            searchInputElement.Clear();
            searchInputElement.SendKeys("castle");
            searchButtonElement.Click();
            Wait(TimeSpan.FromMilliseconds(150));

            var webElement = driver.FindElement(By.Id("error-no-results"));

            Assert.AreEqual("No results", webElement.Text);
        }


        [TestMethod]
        public void OnlyOnePort()
        {
            IWebElement searchInputElement = driver.FindElement(By.Id("search-input"));
            IWebElement searchButtonElement = driver.FindElement(By.Id("search-button"));

            searchInputElement.Clear();
            searchInputElement.SendKeys("port");
            searchButtonElement.Click();
            Wait(TimeSpan.FromMilliseconds(150));
            var webElements = driver.FindElements(By.CssSelector("#search-results li"));

            Assert.IsTrue(webElements.Count == 1);
        }

        /// <summary>
        /// A simple version of wait. 
        /// </summary>
        /// <param name="time">
        /// The expected waiting time.
        /// </param>
        /// <remarks>
        /// This is to simulate not to use the Thread.Sleep(), which is not a good practice. There should be a proper implementation of waiting logic.
        /// For instance to gradually increase the wait duration, up to a upper limit.
        /// </remarks>
        private void Wait(TimeSpan time)
        {
            Thread.Sleep(time);
        }
    }
}
