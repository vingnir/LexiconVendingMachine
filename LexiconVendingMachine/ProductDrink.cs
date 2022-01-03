namespace LexiconVendingMachine
{
    public class ProductDrink : Product
    {
        public ProductDrink(string name, int size, int price, int stock)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "ml";
            this.InStock = stock;
        }
        public override string Use()
        {
            string instructions = $"\nSelected item is {Name} {Size}{Unit} \n Drink it and enjoy! And please don't forget to recycle...";
            return instructions;
        }
    }
}
