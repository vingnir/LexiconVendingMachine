using System;

namespace LexiconVendingMachine
{
    class ProductToy : Product, IPlayable
    {
        public ProductToy(string name, int size, decimal price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "pcs";
        }

        public override string[] Examine()
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            Console.WriteLine("play play...");
        }

        public override string Use()
        {
            throw new NotImplementedException();
        }
    }
}
