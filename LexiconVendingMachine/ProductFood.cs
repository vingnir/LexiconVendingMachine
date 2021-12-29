using System;

namespace LexiconVendingMachine
{
    class ProductFood : Product
    {
        public ProductFood(string name, int size, decimal price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "g";
        }

        public override string Examine()
        {
            string examineItem = $"{Name} {Size} {Unit} {Price}";

            return examineItem;

        }

        public override string Use()
        {
            string instructions = "eat...";
            return instructions;
        }
    }
}
