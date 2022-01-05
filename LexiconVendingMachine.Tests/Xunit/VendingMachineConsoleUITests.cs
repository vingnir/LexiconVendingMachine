using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LexiconVendingMachine.VendingMachineUIConsole;
using System.IO;
using LexiconVendingMachine.Utils;
namespace LexiconVendingMachine.Tests.Xunit
{
    
    public class VendingMachineConsoleUITests
    {
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
            using var consoleInputs = new StringReader("7\n3\n"); 
            Console.SetIn(consoleInputs);
            int actual;
            int expected = 3000;

            // Act           
            var result = ui.RequestMoneyFromUser();
            actual = result[0] * result[1];

            // Assert
            Assert.Equal(expected,actual);
        }

        [Theory]
        [InlineData(100,3, true)]
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
            actual = result[0] * result[1] == denomination*quantity;

            // Assert           
            Assert.Equal(expected, actual);
        }

    
        [Theory]
        [InlineData(0,true)]
        [InlineData(1, true)]
        [InlineData(4, true)]
        [InlineData(5, false)]
        public void SelectProduct__ShouldReturnProductID(int id, bool expected)
        {
            // Arrange            
            ConsoleUI ui = new ConsoleUI();
            string exitIfIncorrectID = "exit";
            string input = $"{id}\n{exitIfIncorrectID}\n";
            using var consoleInputs = new StringReader(input);
            Console.SetIn(consoleInputs);
            bool actual;

            // Act
            actual = ui.SelectProduct() == id;

            // Assert
            Assert.Equal(expected,actual);
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
        public void ShowDeposit__ShouldReturnCurrentCredit()
        {
            // Arrange
            VendingMachine vm = new VendingMachine();
            ConsoleUI userInterface = new ConsoleUI();
            vm.InsertMoney(500, 2);
            int actual;
            int expected = 1000;

            //Act
            actual = userInterface.ShowDeposit();

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
    }
}

