using System;

namespace LexiconVendingMachine
{
    public class ProductDrink : Product
    {

        public ProductDrink(string name, int size, decimal price)
        {
            this.Name = name;
            this.Size = size;
            this.Price = price;
            this.Unit = "ml";
        }


        
        public override string Examine()
        {
            string examineItem = $"{Name} {Size} {Unit} {Price}";

            return examineItem;

        }

        public override string Use()
        {
            string instructions = "Drink it and enjoy! don't forget to recycle the can...";
            return instructions;
        }
    }
}
