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

        public Dictionary<int, Product> GetProducts()
        {
            if (Inventory.Count <= 0) { LoadProducts(); } // TODO Get from DB 

            return Inventory;
        }

        // TODO load from db
        public bool LoadProducts()
        {
            int key = 0;
            int current = Inventory.Count;
            Inventory.Add(key++, new ProductDrink("Trocadero", 330, 15, 40));
            Inventory.Add(key++, new ProductToy("Kexchocklad", 25, 80));
            Inventory.Add(key++, new ProductFood("Sandwich", 200, 45, 30));
            Inventory.Add(key++, new ProductFood("Pizza", 350, 125, 8));
            Inventory.Add(key++, new ProductToy("Gameboy", 745, 5));

            return Inventory.Count > current;
        }
    }
}
