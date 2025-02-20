using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumLab;
using System;
using System.Threading;

namespace SeleniumTests
{
    [TestFixture]
    public class FormTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void Teardown()
        {
            Thread.Sleep(2000);  
            driver.Quit();
        }

        [Test]
        public void SuccessForm()
        {
            FormTest.SuccessForm(driver);
        }

        [Test]
        public void RequiredFields()
        {
            FormTest.RequiredFields(driver);
        }

        [Test]
        public void FileValidation()
        {
            FormTest.FileValidation(driver);
        }

        [Test]
        public void SelectionMenu()
        {
            FormTest.SelectionMenu(driver);
        }

        [Test]
        public void TestRadioButtons()
        {
            FormTest.TestRadioButtons(driver);
        }

        [Test]
        public void TestCheckboxes()
        {
            FormTest.TestCheckboxes(driver);
        }

        [Test]
        public void TestFileSizeValidation()
        {
            FormTest.TestFileSizeValidation(driver);
        }

        [Test]
        public void TestTooltipDisplay()
        {
            FormTest.TestTooltipDisplay(driver);
        }

        [Test]
        public void TestImageDeletion()
        {
            FormTest.TestImageDeletion(driver);
        }
    }
}
