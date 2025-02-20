using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLab
{
    public static class FormTest
    {
        public static void SuccessForm(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");

            driver.FindElement(By.Id("userEmail")).SendKeys("johndoe@example.com");

            driver.FindElement(By.CssSelector("label[for='gender-radio-1']")).Click();

            driver.FindElement(By.Id("userNumber")).SendKeys("1234567890");

            driver.FindElement(By.Id("dateOfBirthInput")).Click();
            driver.FindElement(By.ClassName("react-datepicker__year-select")).SendKeys("1995");
            driver.FindElement(By.ClassName("react-datepicker__month-select")).SendKeys("July");
            driver.FindElement(By.ClassName("react-datepicker__day--015")).Click();

            IWebElement subjectInput = driver.FindElement(By.Id("subjectsInput"));
            subjectInput.SendKeys("Maths");
            Thread.Sleep(500);
            subjectInput.SendKeys(Keys.Enter);

            driver.FindElement(By.CssSelector("label[for='hobbies-checkbox-1']")).Click();

            driver.FindElement(By.Id("uploadPicture")).SendKeys(@"C:\Users\eqspe\source\repos\SeleniumLab\SeleniumLab\img\sample.jpg");

            driver.FindElement(By.Id("currentAddress")).SendKeys("123 Main Street, NY");

            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown).Perform();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("submit")).Click();
            Thread.Sleep(2000);

            bool isSuccess = driver.FindElement(By.ClassName("modal-content")).Displayed;
            Console.WriteLine(isSuccess ? "Форма успешно отправлена!" : " Ошибка при отправке формы!");
        }

        public static void RequiredFields(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            // Прокручиваем вниз к кнопке Submit
            IWebElement submitButton = driver.FindElement(By.Id("submit"));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", submitButton);
            Thread.Sleep(500);

            // Нажимаем кнопку Submit, не заполняя форму
            submitButton.Click();
            Thread.Sleep(1000);

            // Проверяем, появились ли сообщения об ошибке
            bool isFirstNameErrorDisplayed = driver.FindElement(By.Id("firstName")).GetAttribute("class").Contains("field-error");
            bool isLastNameErrorDisplayed = driver.FindElement(By.Id("lastName")).GetAttribute("class").Contains("field-error");
            bool isEmailErrorDisplayed = driver.FindElement(By.Id("userEmail")).GetAttribute("class").Contains("field-error");
            bool isPhoneErrorDisplayed = driver.FindElement(By.Id("userNumber")).GetAttribute("class").Contains("field-error");

            if (isFirstNameErrorDisplayed && isLastNameErrorDisplayed && isEmailErrorDisplayed && isPhoneErrorDisplayed)
            {
                Console.WriteLine("Тест на обязательные поля пройден: ошибки отображаются.");
            }
            else
            {
                Console.WriteLine("Ошибка: не все обязательные поля показали ошибку!");
            }
        }

        public static void FileValidation(IWebDriver driver) 
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            // Находим поле загрузки файла
            IWebElement uploadElement = driver.FindElement(By.Id("uploadPicture"));

            // Попытка загрузить файл с некорректным форматом (например, .txt)
            string invalidFilePath = @"C:\Users\eqspe\source\repos\SeleniumLab\SeleniumLab\img\invalid-file.txt";
            uploadElement.SendKeys(invalidFilePath);
            Thread.Sleep(1000);

            // Проверяем, появилось ли сообщение об ошибке (если оно есть)
            bool isErrorDisplayed = false;
            try
            {
                IWebElement errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'Invalid file format')]"));
                isErrorDisplayed = errorMessage.Displayed;
            }
            catch (NoSuchElementException)
            {
                isErrorDisplayed = false;
            }

            if (isErrorDisplayed)
            {
                Console.WriteLine("Тест пройден: Ошибка загружаемого файла отображается.");
            }
            else
            {
                Console.WriteLine(" Ошибка: Система не вывела сообщение об ошибке!");
            }
        
    }

        public static void SelectionMenu(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/select-menu");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Ожидание и выбор элемента из обычного Select
            IWebElement dropdown = wait.Until(d => d.FindElement(By.Id("oldSelectMenu")));
            SelectElement select = new SelectElement(dropdown);
            select.SelectByValue("4"); // Выбираем "Purple"
            Console.WriteLine("Выбрано значение: " + select.SelectedOption.Text);

            // Проверка, что выбранное значение отображается
            if (select.SelectedOption.Text != "Purple")
            {
                throw new Exception("Ошибка: Некорректное значение в выпадающем меню.");
            }

            // Выбор элемента в кастомном меню (React Select)
            IWebElement customDropdown = wait.Until(d => d.FindElement(By.Id("react-select-2-input")));
            customDropdown.SendKeys("Green");
            customDropdown.SendKeys(Keys.Enter);
            Console.WriteLine("Выбрано значение: Green");

            // Проверка отображения выбранного значения
            IWebElement selectedValue = wait.Until(d => d.FindElement(By.ClassName("css-1uccc91-singleValue")));
            if (selectedValue.Text != "Green")
            {
                throw new Exception("Ошибка: Выбранное значение в кастомном меню не совпадает.");
            }

            Console.WriteLine("Тест успешно выполнен!");
        }

        public static void TestRadioButtons(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            var radioButtons = driver.FindElements(By.Name("radioOption"));
            foreach (var radio in radioButtons)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", radio);
                Console.WriteLine($"Выбрана радиокнопка: {radio.GetAttribute("value")}");
                Thread.Sleep(500);
            }
        }

        public static void TestCheckboxes(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            var checkboxes = driver.FindElements(By.ClassName("checkboxClass"));
            foreach (var checkbox in checkboxes)
            {
                checkbox.Click();
                Console.WriteLine($"Флажок {checkbox.GetAttribute("value")} установлен.");
                Thread.Sleep(500);
                checkbox.Click();
                Console.WriteLine($"Флажок {checkbox.GetAttribute("value")} снят.");
            }
        }

        public static void TestFileSizeValidation(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            IWebElement uploadElement = driver.FindElement(By.Id("uploadPicture"));
            string largeFilePath = @"C:\Users\eqspe\source\repos\SeleniumLab\SeleniumLab\img\30mb.jpg";
            uploadElement.SendKeys(largeFilePath);
            Thread.Sleep(1000);

            try
            {
                IWebElement errorMessage = driver.FindElement(By.XPath("//div[contains(text(),'File size too large')]"));
                Console.WriteLine("Тест пройден: Сообщение о слишком большом файле отображается.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Ошибка: Сообщение о размере файла не появилось!");
            }
        }

        public static void TestTooltipDisplay(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            // Проверяем, есть ли элемент
            IWebElement element;
            try
            {
                element = driver.FindElement(By.Id("tooltipElement"));
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Ошибка: Элемент с ID 'tooltipElement' не найден!");
                return;
            }

            // Наводим курсор
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveToElement(element).Perform();

            try
            {
                // Ждём, пока появится подсказка
                IWebElement tooltip = wait.Until(d => d.FindElement(By.XPath("//*[contains(@class, 'tooltip')]")));

                Console.WriteLine("✅ Тест пройден: Подсказка отображается.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("❌ Ошибка: Подсказка не появилась!");
            }
        }

        public static void TestImageDeletion(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            Thread.Sleep(2000);

            IWebElement deleteButton = driver.FindElement(By.Id("deleteImage"));
            deleteButton.Click();
            Thread.Sleep(1000);

            try
            {
                driver.FindElement(By.Id("uploadedImage"));
                Console.WriteLine("Ошибка: Изображение не удалено!");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Тест пройден: Изображение успешно удалено.");
            }
        }
    }
    
    
    
}
