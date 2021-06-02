using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrTests
{
	public static class WebDriverFactory
	{
		/// <summary>
		/// Create a default web driver using the Google Chrome.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="ChromeDriver"/>.
		/// </returns>
		public static IWebDriver CreateChromeWebDriver()
		{
			var options = new ChromeOptions();
			options.AddArguments("test-type");
			options.AddArgument("--start-maximized");

			options.AddArguments("--incognito");
			return new ChromeDriver(options);
		}
	}
}
