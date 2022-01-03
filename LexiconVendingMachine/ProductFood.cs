namespace LexiconVendingMachine
{
    class ProductFood : Product
    {
        public ProductFood(string name, int size, int price, int stock)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "g";
            this.InStock = stock;
        }
        public override string Use()
        {
            string instructions = $"\nSelected item is {Name} {Size}{Unit} \nEat and enjoy...";
            return instructions;
        }
    }
}
