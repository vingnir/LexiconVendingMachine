using Xunit;


namespace LexiconVendingMachine.Tests
{
    public class VendingMachineTests
    {

        // Test InsertMoney
        [Theory]
        [InlineData(3, 1, false)] // Test forbidden denominations
        [InlineData(15, 1, false)]
        [InlineData(25, 1, false)]
        [InlineData(1, 15, true)] // Test allowed denominations
        [InlineData(20, 15, true)]
        [InlineData(50, 1, true)]
        [InlineData(500, 3, true)]
        [InlineData(1000, 2, true)]
        [InlineData(0, 0, false)]
        [InlineData(-100, 5, false)] // Tests to prevent malicious code from making positive number from two negatives
        [InlineData(-1, -500, false)]
        [InlineData(-500, 1, false)]
        [InlineData(-500, -1, false)]
        public void InsertMoney_ShouldReturnTrueIfValueIsAdded(int denomination, int quantity, bool expected)
        {
            VendingMachine vendingMachine = new VendingMachine();
            // Arrange

            // Act
            bool actual = vendingMachine.InsertMoney(denomination, quantity);

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
