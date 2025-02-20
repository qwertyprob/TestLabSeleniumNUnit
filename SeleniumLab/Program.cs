//using System;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Interactions;
//using System.Threading;
//using SeleniumLab;

//class Program
//{
//    static void Main()
//    {
//        IWebDriver driver = new ChromeDriver();
//        driver.Manage().Window.Maximize();

//        try
//        {
//            FormTest.SuccessForm(driver);
//            //FormTest.RequiredFields(driver);
//            //FormTest.FileValidation(driver);
//            //FormTest.SelectionMenu(driver);
//            //FormTest.TestRadioButtons(driver);
//            //FormTest.TestCheckboxes(driver);
//            //FormTest.TestFileSizeValidation(driver);
//            //FormTest.TestTooltipDisplay(driver);
//            //FormTest.TestImageDeletion(driver);
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine($"Ошибка: {ex.Message}");
//        }
//        finally
//        {
//            Thread.Sleep(5000); 
//            driver.Quit();
//        }
//    }
//}
