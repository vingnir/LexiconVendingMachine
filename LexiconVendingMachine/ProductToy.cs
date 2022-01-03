namespace LexiconVendingMachine
{
    class ProductToy : Product
    {
        public ProductToy(string name, int price, int stock)
        {
            this.Name = name;
            this.Size = 1;
            this.Price = price;
            this.Unit = "pcs";
            this.InStock = stock;
        }
        public override string Use()
        {
            string instructions = $"\nSelected item is {Name} \nPlay and have fun...";
            return instructions;
        }
    }
}
