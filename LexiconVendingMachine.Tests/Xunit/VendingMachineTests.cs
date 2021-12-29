using Xunit;


namespace LexiconVendingMachine.Tests
{
    public class VendingMachineTests
    {

        // Test AddToMoneyPool
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
        public void AddToMoneyPool_ShouldReturnTrueIfValueIsAdded(int denomination, int quantity, bool expected)
        {
            VendingMachine vendingMachine = new VendingMachine();
            // Arrange

            // Act
            bool actual = vendingMachine.AddToMoneyPool(denomination, quantity);

            // Assert
            Assert.Equal(expected, actual);
        }



        // Test AddProducts
        [Fact]
        public void AddProducts_ShouldReturnTrueIfMultipleProductAreAdded()
        {

            // Arrange
            ProductFactory pf = new ProductFactory();
            bool expected = true;

            // Act
            bool actual = pf.AddProducts(new ProductDrink("Coca Cola Tank", 1000, 250), 10); // adding 10

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddProducts_ShouldReturnFalseIfMultipleProductAreNOTAdded()
        {
            // Arrange
            ProductFactory pf = new ProductFactory();
            bool expected = false;

            // Act
            bool actual = pf.AddProducts(new ProductDrink("Coca Cola Tank", 1000, 250), 0); // adding 10

            // Assert
            Assert.Equal(expected, actual);
        }

        // Testing method with no arguments to setup inventory
        [Fact]
        public void AddProducts_ShouldReturnTrueIfProductsAreCreated()
        {


            // Arrange
            ProductFactory pf = new ProductFactory();
            bool expected = true;

            // Act
            bool actual = pf.AddProducts();

            // Assert
            Assert.Equal(expected, actual);
        }

        // Testing overloaded method with 1 argument to add 1 product
        [Fact]
        public void AddProducts_ShouldReturnTrueIfOneProductAreCreated()
        {
            // Arrange
            ProductFactory pf = new ProductFactory();
            bool expected = true;

            // Act
            bool actual = pf.AddProducts(new ProductDrink("Pepsi Cola Can", 330, 25));

            // Assert
            Assert.Equal(expected, actual);
        }


        // Testing overloaded method with 1 argument to add 1 product
        [Fact]
        public void ShowAll_ShouldReturnDictonaryWithProducts()
        {
            // Arrange
            VendingMachine vendingMachine = new VendingMachine();
            object expected = null;

            // Act
            var actual = vendingMachine.ShowAll();

            // Assert
            Assert.Equal(expected, actual);
        }
        //TODO GetProducts
    }
}
