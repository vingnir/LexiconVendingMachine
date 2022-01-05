using LexiconVendingMachine.VendingMachineUIConsole;
using System;
using System.IO;
using Xunit;
namespace LexiconVendingMachine.Tests.Xunit
{

    public class VendingMachineConsoleUITests
    {
        [Fact]
        public void Initialize__ShouldReturnTrueIfProductsAreLoaded()
        {
            // Arrange  
            ConsoleUI ui = new ConsoleUI();
            bool actual;
            bool expected = true;

            //Act
            actual = ui.Initialize();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MainMenu__ShouldContainText()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("");
            bool expected = true;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            ui.MainMenu();
            string result = sw.ToString();
            bool actual = result.Contains("Purchase product");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MainMenu__ShouldNotContainText()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("");
            bool expected = false;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            ui.MainMenu();
            string result = sw.ToString();
            bool actual = result.Contains("How many ");

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void HandleUserPurchase__ShouldNotContainText()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("3\n");
            bool expected = false;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            bool creditAdded = ui.GetFreeCredit();
            if (creditAdded) ui.HandleUserPurchase();
            string result = sw.ToString();
            bool actual = result.Contains("How many ");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandleUserPurchase__ShouldContainText()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("3\n");
            bool expected = false;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            bool creditAdded = ui.GetFreeCredit();
            if (creditAdded) ui.HandleUserPurchase();
            string result = sw.ToString();
            bool actual = result.Contains("Pizza");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandleUserPurchase__ShouldNotContainFakeItems()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("3\n");
            bool expected = false;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            bool creditAdded = ui.GetFreeCredit();
            if (creditAdded) ui.HandleUserPurchase();
            string result = sw.ToString();
            bool actual = result.Contains("Lasagne");

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandleTransaction__ShouldReturnTrueIfAddedCredit()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("\n7\n5\n\n");
            bool expected = true;
            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act

            //string result = sw.ToString();
            bool actual = ui.HandleTransaction();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RequestMoneyFromUser__ShouldContainStringWithQuestion()
        {
            // Arrange
            ConsoleUI userInterface = new ConsoleUI();
            using var sw = new StringWriter();
            using var consoleInputs = new StringReader("7\n3");
            Console.SetOut(sw);
            Console.SetIn(consoleInputs);

            // Act
            userInterface.RequestMoneyFromUser();

            // Assert
            var result = sw.ToString();
            Assert.Contains("How many ", result);
        }

        [Fact]
        public void RequestMoneyFromUser__ShouldReturnArray()
        {
            // Arrange            
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var consoleInputs = new StringReader("7\n3\n");
            Console.SetOut(sw);
            Console.SetIn(consoleInputs);
            int actual;
            int expected = 3000;

            // Act           
            var result = ui.RequestMoneyFromUser();
            actual = result[0] * result[1];

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(100, 3, true)]
        [InlineData(10, 101, false)]
        [InlineData(50, 1000, false)]
        [InlineData(14, 1, false)]
        [InlineData(500, 99, true)]
        [InlineData(500, 101, false)]
        [InlineData(1, 100, true)]
        public void RequestMoneyFromUser__ShoulReturnArrayOfDenominationAndQuantity(int denomination, int quantity, bool expected)
        {
            // Arrange
            CurrencyDenominations cd = new CurrencyDenominations();
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bool actual;
            int defaultQuantity = 1;
            int index = -1;

            for (int i = 0; i < cd.denominations.Length; i++)
            {
                if (denomination == cd.denominations[i])
                {
                    index = i;
                }
            }
            string inputs = $"{index}\n{quantity}\n{defaultQuantity}\n";
            using var consoleInputs = new StringReader(inputs);
            Console.SetIn(consoleInputs);

            // Act
            var result = ui.RequestMoneyFromUser();
            actual = result[0] * result[1] == denomination * quantity;

            // Assert           
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        public void SelectProduct__ShouldReturnProductID(int id, bool expected)
        {
            // Arrange            
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            Console.SetOut(sw);
            string exitIfIncorrectID = "exit";
            string input = $"{id}\n{exitIfIncorrectID}\n";
            using var consoleInputs = new StringReader(input);
            Console.SetIn(consoleInputs);
            bool actual;

            // Act
            actual = ui.SelectProduct() == id;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, 20, 15, true)]
        [InlineData(10, 20, 11, true)]
        [InlineData(0, 20, 25, false)]
        public void GetUserInput__ShouldReturnNumberInValidRange(int min, int max, int input, bool expected)
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            string consoleInput = $"{input}\nexit\n";
            using var sr = new StringReader(consoleInput);
            bool actual;

            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            actual = ui.GetUserInput(min, max) == input;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShowDeposit__ShouldReturnZeroCredit()
        {
            // Arrange
            ConsoleUI userInterface = new ConsoleUI();
            int actual;
            int expected = 0;

            //Act
            actual = userInterface.ShowDeposit();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShowDeposit__ShouldReturn5000Credit()
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            using var sr = new StringReader("\n7\n5\n\n");
            bool expected = true;
            bool actual;

            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act
            bool result = ui.HandleTransaction();
            actual = ui.ShowDeposit() == 5000;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 100, true)]
        [InlineData(20, 100, true)]
        [InlineData(20, 5, true)]
        [InlineData(1000, 10, true)]
        public void ShowDeposit__ShouldReturnCredit(int denomination, int quantity, bool expected)
        {
            // Arrange
            CurrencyDenominations cd = new CurrencyDenominations();
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            Console.SetOut(sw);
            bool actual;
            int index = -1;

            for (int i = 0; i < cd.denominations.Length; i++)
            {
                if (denomination == cd.denominations[i])
                {
                    index = i;
                }
            }
            string inputs = $"{index}\n{quantity}\n";
            using var consoleInputs = new StringReader(inputs);
            Console.SetIn(consoleInputs);

            // Act
            bool result = ui.HandleTransaction();
            actual = ui.ShowDeposit() == denomination * quantity;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("1000")]
        [InlineData("500")]
        [InlineData("10")]
        [InlineData("100")]
        [InlineData("20kr")]
        [InlineData("10kr")]
        [InlineData("5kr")]
        [InlineData("1")]
        public void GetDenominationInfo__ShouldReturnStringWithValidDenominations(string excpected)
        {
            // Arrange
            ConsoleUI userInterface = new ConsoleUI();
            string actual;

            //Act
            actual = userInterface.GetDenominationInfo();

            //Assert
            Assert.Contains(excpected, actual);
        }

        [Theory]
        [InlineData("Kexchocklad", true)]
        [InlineData("Trocadero", true)]
        [InlineData("Gameboy", true)]
        [InlineData("Sandwich", true)]
        [InlineData("Sand", true)]
        [InlineData("wich", true)]
        [InlineData("Pizza", true)]
        [InlineData("Pi", true)]
        [InlineData("Dags o sova?", false)]
        [InlineData("Lexicon", false)]
        public void DisplayProducts__ShouldContainStringWithProductNames(string productName, bool expected)
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            ui.Initialize();
            bool actual;
            string result;

            //Act
            result = ui.DisplayProducts();
            actual = result.Contains(productName);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("There is no money to return", true)]
        [InlineData("There is a lot of money to return", false)]
        public void ReturnChange__ShouldContainStringWithNoCredit(string text, bool expected)
        {
            // Arrange
            ConsoleUI ui = new ConsoleUI();
            bool actual;
            string result;

            //Act
            result = ui.ReturnChange();
            actual = result.Contains(text);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(5, 3, true)]
        [InlineData(500, 3, true)]
        [InlineData(1500, 3, false)]
        [InlineData(20, 101, false)]
        public void ReturnChange__ShouldContainStringWithRemaingCredit(int denomination, int quantity, bool expected)
        {
            // Arrange
            CurrencyDenominations cd = new CurrencyDenominations();
            ConsoleUI ui = new ConsoleUI();
            using var sw = new StringWriter();
            string result;
            bool actual;
            int index = -1;
            int credit = denomination * quantity;

            for (int i = 0; i < cd.denominations.Length; i++)
            {
                if (denomination == cd.denominations[i])
                {
                    index = i;
                }
            }
            string inputs = $"{index}\n{quantity}\nexit\n";
            using var sr = new StringReader(inputs);

            Console.SetOut(sw);
            Console.SetIn(sr);

            // Act                      
            ui.HandleTransaction();
            result = ui.ReturnChange();
            actual = result.Contains(credit.ToString());

            //Assert
            Assert.Equal(expected, actual);
        }

    }
}

