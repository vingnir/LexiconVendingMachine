using System;

namespace LexiconVendingMachine
{
    class ProductFood : Product, IEatable
    {
        public ProductFood(string name, int size, decimal price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "g";
        }
        public void Eat()
        {
            Console.WriteLine("Yum yum...");
        }
    }
}
