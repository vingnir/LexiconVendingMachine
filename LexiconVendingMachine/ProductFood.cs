using System;

namespace LexiconVendingMachine
{
    class ProductFood : Product
    {
        public ProductFood(string name, int size, decimal price, int stock)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "g";
            this.InStock = stock;
        }

        

        public override string Use()
        {
            string instructions = "Eat and enjoy...";
            return instructions;
        }
    }
}
