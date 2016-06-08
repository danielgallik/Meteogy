using System;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions.Internal;
using System.Drawing;
using System.IO;
using System.Threading;
using Meteogy.Models;

namespace Meteogy.Tests
{
    [TestClass]
    public class SeleniumTests
    {
        private const int maxLoadingTime = 10; // seconds

        [TestMethod]
        public void SuccessLoadMapTest()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Window.Size = new Size(960, 1024);
                driver.Navigate().GoToUrl("http://meteogy.azurewebsites.net/");
                var from = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[1]");
                var to = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[2]");
                var submit = driver.FindElementByXPath("//*[@id=\"UI_FormParameters\"]/div/button");

                from.SendKeys("01/01/2015");
                to.SendKeys("01/01/2017");
                Thread.Sleep(1000);
                submit.Click();

                int i = 0;
                while (i < maxLoadingTime)
                {
                    Thread.Sleep(1000);
                    i++;
                    if (submit.Text != "Loading...")
                    {
                        break;
                    }
                }

                if (i >= maxLoadingTime)
                {
                    Assert.IsTrue(false, String.Format("Loading time is more than {0} seconds.", maxLoadingTime));
                }
                var labelWarning = driver.FindElementByXPath("//*[@id=\"UI_AlertFormWarning\"]");
                var labelError = driver.FindElementByXPath("//*[@id=\"UI_AlertFormError\"]");
                Assert.IsFalse(labelWarning.Displayed, "Warning is displayed.");
                Assert.IsFalse(labelError.Displayed, "Error is displayed.");

                driver.GetScreenshot().SaveAsFile("test1.png", ImageFormat.Png);
                Bitmap scr = new Bitmap(new MemoryStream(driver.GetScreenshot().AsByteArray));
                var actual = scr.GetPixel(460, 300);
                Assert.IsTrue(actual.R < 255 || actual.G < 255 || actual.B < 255, "Map is not rendered.");
            }
        }

        [TestMethod]
        public void NoMeasurementFoundTest()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Window.Size = new Size(960, 1024);
                driver.Navigate().GoToUrl("http://meteogy.azurewebsites.net/");
                var from = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[1]");
                var to = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[2]");
                var submit = driver.FindElementByXPath("//*[@id=\"UI_FormParameters\"]/div/button");

                from.SendKeys("01/01/2017");
                to.SendKeys("01/01/2017");
                Thread.Sleep(1000);
                submit.Click();

                int i = 0;
                while (i < maxLoadingTime)
                {
                    Thread.Sleep(1000);
                    i++;
                    if (submit.Text != "Loading...")
                    {
                        break;
                    }
                }

                if (i >= maxLoadingTime)
                {
                    Assert.IsTrue(false, String.Format("Loading time is more than {0} seconds.", maxLoadingTime));
                }

                var label = driver.FindElementByXPath("//*[@id=\"UI_AlertFormWarning\"]");
                Assert.IsTrue(label.Displayed, "Warning is hidden.");
            }
        }

        [TestMethod]
        public void SuccessLoadAllParameters()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Manage().Window.Size = new Size(960, 1024);
                driver.Navigate().GoToUrl("http://meteogy.azurewebsites.net/");
                var from = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[1]");
                var to = driver.FindElementByXPath("//*[@id=\"datepicker\"]/div/input[2]");
                var submit = driver.FindElementByXPath("//*[@id=\"UI_FormParameters\"]/div/button");
                var select = new SelectElement(driver.FindElementByXPath("//*[@id=\"UI_Parameter\"]"));

                from.SendKeys("01/01/2015");
                to.Clear();
                to.SendKeys("01/01/2017");

                foreach(Parameter param in Enum.GetValues(typeof(Parameter)))
                {
                    select.SelectByValue(param.ToString().ToLower());
                    Thread.Sleep(1000);
                    submit.Click();

                    int i = 0;
                    while (i < maxLoadingTime)
                    {
                        Thread.Sleep(1000);
                        i++;
                        if (submit.Text != "Loading...")
                        {
                            break;
                        }
                    }

                    if (i >= maxLoadingTime)
                    {
                        Assert.IsTrue(false, String.Format("Loading time is more than {0} seconds.", maxLoadingTime));
                    }
                    var labelWarning = driver.FindElementByXPath("//*[@id=\"UI_AlertFormWarning\"]");
                    var labelError = driver.FindElementByXPath("//*[@id=\"UI_AlertFormError\"]");
                    Assert.IsFalse(labelWarning.Displayed, String.Format("Warning is displayed for parameter {0}.", param));
                    Assert.IsFalse(labelError.Displayed, String.Format("Error is displayed for parameter {0}.", param));
                }
            }
        }
    }
}
