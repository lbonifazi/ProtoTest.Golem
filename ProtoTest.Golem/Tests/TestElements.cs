﻿using System.Linq;
using MbUnit.Framework;
using OpenQA.Selenium;
using ProtoTest.Golem.WebDriver;

namespace ProtoTest.Golem.Tests
{
    internal class TestElements : WebDriverTestBase
    {
        [Test]
        public void TestElementAPI()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            var elements = new ElementCollection(driver.FindElements(By.Name("q")));
            elements.First().Click();
        }

        [Test]
        public void TestLinq()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            var elements = new ElementCollection(driver.FindElements(By.XPath("//*")));
            elements.First(x => x.GetAttribute("name") == "q" && x.Displayed).Click();
        }

        [Test]
        public void testCount()
        {
            driver.Navigate().GoToUrl("http://www.google.com");
            var elements = new ElementCollection(driver.FindElements(By.Name("q")));
            driver.Navigate().GoToUrl("http://www.google.com");
            Assert.Count(1, elements.Where(x => x.IsStale()));
        }

        [Test]
        public void TestBy()
        {
            var elements = new ElementCollection(By.Name("q"));
            driver.Navigate().GoToUrl("http://www.google.com");
            elements.ForEach(x => x.Click());
            driver.Navigate().GoToUrl("http://www.google.com");
            elements.ForEach(x => x.SendKeys("test"));
        }
    }
}