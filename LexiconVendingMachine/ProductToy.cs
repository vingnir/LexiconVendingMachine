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

        

        public override string Use()
        {
            string instructions = "Play and have fun...";
            return instructions; 
        }
    }
}
