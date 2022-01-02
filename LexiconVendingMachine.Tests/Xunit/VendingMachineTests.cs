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
        public void InsertMoney_ShouldReturnIntRecieptOfInsertedAmount(int denomination, int quantity, int expected)
        {
            VendingMachine vendingMachine = new VendingMachine();
            // Arrange

            // Act
            int actual = vendingMachine.InsertMoney(denomination, quantity);

            // Assert
            Assert.Equal(expected, actual);
        }




        // Testing method with no arguments to setup inventory
        [Fact]
        public void LoadProducts_ShouldReturnTrueIfProductsAreCreated()
        {
            // Arrange
            ProductFactory pf = new ProductFactory();
            bool expected = true;

            // Act
            bool actual = pf.LoadProducts();

            // Assert
            Assert.Equal(expected, actual);
        }




    }
}
