using System.Collections.Generic;

namespace LexiconVendingMachine
{
    public class ProductFactory
    {
        private readonly Dictionary<int, Product> Inventory;

        public ProductFactory()
        {
            Inventory = new Dictionary<int, Product>();
        }

        // TODO Get from DB
        public Dictionary<int, Product> GetProducts()
        {
            if (Inventory.Count <= 0)
            {
                int key = 0;
                Inventory.Add(key++, new ProductDrink("Trocadero", 330, 15, 40));
                Inventory.Add(key++, new ProductFood("Kexchocklad", 55, 25, 80));
                Inventory.Add(key++, new ProductFood("Sandwich", 200, 45, 30));
                Inventory.Add(key++, new ProductFood("Pizza", 350, 125, 8));
                Inventory.Add(key++, new ProductToy("Gameboy", 745, 5));
            }

            return Inventory;
        }
    }
}
