using System;

namespace LexiconVendingMachine
{
    class ProductToy : Product
    {
        public ProductToy(string name, decimal price)
        {
            this.Name = name;
            this.Size = 1;
            this.Price = price;
            this.Unit = "pcs";
        }

        public override string Examine()
        {
            string examineItem = $"{Name} {Size} {Unit} {Price}";

            return examineItem;

        }



        public override string Use()
        {
            string instructions = "play play...";
            return instructions; 
        }
    }
}
