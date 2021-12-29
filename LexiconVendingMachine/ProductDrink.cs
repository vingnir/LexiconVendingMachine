using System;

namespace LexiconVendingMachine
{
    public class ProductDrink : Product, IDrinkable
    {

        public ProductDrink(string name, int size, decimal price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "ml";
        }


        public void Drink()
        {
            Console.WriteLine("Klunk klunk...");
        }

        public override string[] Examine()
        {
            throw new NotImplementedException();
        }

        public override string Use()
        {
            throw new NotImplementedException();
        }
    }
}
