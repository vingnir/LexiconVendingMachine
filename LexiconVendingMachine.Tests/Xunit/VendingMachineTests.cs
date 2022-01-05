using System;
using System.Collections.Generic;
using Xunit;


namespace LexiconVendingMachine.Tests
{
    public class VendingMachineTests
    {

        // Test InsertMoney, returns reciept of the transaction:  denomination * quantity
        [Theory]
        [InlineData(3, 1, 0)] // Test forbidden denominations
        [InlineData(15, 1, 0)]
        [InlineData(35, 10, 0)]
        [InlineData(25, 1, 0)]
        [InlineData(1, 15, 15)] // Test valid denominations
        [InlineData(20, 15, 300)]
        [InlineData(50, 1, 50)]
        [InlineData(500, 3, 1500)]
        [InlineData(1000, 2, 2000)]
        [InlineData(0, 0, 0)]
        [InlineData(-100, 5, 0)] // Tests to prevent malicious code from making positive number from two negatives
        [InlineData(-1, -500, 0)]
        [InlineData(-500, 1, 0)]
        [InlineData(-500, -1, 0)]
        [InlineData(-1000, 1, 0)]
        public void InsertMoney_ShouldReturnInsertedAmount(int denomination, int quantity, int expected)
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();

            // Act
            int actual = vendingMachine.InsertMoney(denomination, quantity);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetAvailableProducts_ShouldReturnDictionaryWithProducts()
        {

            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
           
            // Act
            var actual = vendingMachine.GetAvailableProducts();

            // Assert
            Assert.True(actual.Count > 0);

        }

        [Fact]
        public void GetAvailableProducts_ShouldEqualNumberOfProductsInDictionary()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            int expected = 5; //Dictionary has 5 products

            // Act
            int actual = vendingMachine.GetAvailableProducts().Count;

            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData(int.MinValue,true)]
        [InlineData(5, false)]
        [InlineData(100000, true)]
        public void GetAvailableProducts_ShouldNotEqualNumberOfProductsInDictionary(int wrongNumber, bool expected)
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();                      
            int correctNumber = vendingMachine.GetAvailableProducts().Count;

            // Act
            bool actual = wrongNumber != correctNumber; 

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Purchase_IndexShouldBeValidRange()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            Random r = new Random();
            vendingMachine.LoadProducts();
            vendingMachine.InsertMoney(1000, 5);
            int validMaxIndex = vendingMachine.GetAvailableProducts().Count -1;
            int randomIndex = r.Next(0, validMaxIndex);
            bool expected = true; 
            
            // Act
            var purchasedProduct = vendingMachine.Purchase(randomIndex);
            bool actual = purchasedProduct != null;

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void Purchase_IndexShouldNotBeValidRange()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();            
            vendingMachine.LoadProducts();
            vendingMachine.InsertMoney(1000, 5);
            int inValidMaxIndex = vendingMachine.GetAvailableProducts().Count + 1;
            
            bool expected = true;

            // Act
            var purchasedProduct = vendingMachine.Purchase(inValidMaxIndex);
            bool actual = purchasedProduct == null;

            // Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ShowAll_StringShouldBeReturned()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.LoadProducts();
            
            bool expected = true;

            // Act

            bool actual = vendingMachine.ShowAll().Length > 0;

            // Assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void ShowAll_StringIsNotEmpty()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.LoadProducts();            
            bool expected = false;

            // Act

            bool actual = vendingMachine.ShowAll() == string.Empty;

            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void EndTransaction_ShouldReturn2DArrayWithRemainingCredit()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            CurrencyDenominations cd = new CurrencyDenominations();
            
            int expected = vendingMachine.InsertMoney(100, 5);
            int actual = 0;

            // Act
            int[,] moneyInReturn = vendingMachine.EndTransaction();

            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                actual += moneyInReturn[index,0] * moneyInReturn[index, 1];
            }
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(50,5, 250)]
        [InlineData(500, 0, 0)]
        [InlineData(15, 1, 0)]
        [InlineData(int.MaxValue, 5, 0)]
        [InlineData(1000, 100, 100000)]
        [InlineData(950, 100, 0)]
        [InlineData(-25, 1, 0)]
        [InlineData(0, 1000, 0)]
        public void EndTransaction_ShouldReturn2DArrayWithRemainingCreditMultipleTests(int denomination, int quantity, int expected)
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            CurrencyDenominations cd = new CurrencyDenominations();
            vendingMachine.InsertMoney(denomination, quantity);
            int actual = 0;          

            // Act
            int[,] moneyInReturn = vendingMachine.EndTransaction();

            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                actual += moneyInReturn[index, 0] * moneyInReturn[index, 1];
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetChange__ShouldReturn2DArrayOfInsertedAmount()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            int moneyToChange = 500;
            int actual = 0;
            int expected = moneyToChange;

           
            // Act
            int[,] moneyInReturn = vendingMachine.GetChange(moneyToChange);

            for (int index = vendingMachine.cd.denominations.Length - 1; index >= 0; index--)
            {
                actual += moneyInReturn[index, 0] * moneyInReturn[index, 1];
            }

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(500, 500)]
        [InlineData(0,0)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void GetChange__ShouldReturn2DArrayOfInsertedAmounts(int moneyToChange, int expected)
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            int actual = 0;
           
            // Act
            int[,] moneyInReturn = vendingMachine.GetChange(moneyToChange);

            for (int index = vendingMachine.cd.denominations.Length - 1; index >= 0; index--)
            {
                actual += moneyInReturn[index, 0] * moneyInReturn[index, 1];
            }

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Examine__ShouldReturnStringWithProductInfo()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            
            bool actual;
            bool expected = false;

            // Act
            actual = vendingMachine.Examine() == string.Empty;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Use__VendingMachineShouldReturnKnownStringWithInstructions()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            string instructions = $"Put money in the machine and follow the instructions...";
            bool actual; 
            bool expected = true;

            // Act
            actual = vendingMachine.Use() == instructions;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
