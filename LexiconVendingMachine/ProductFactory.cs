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
            int current = Inventory.Count;
            Inventory.Add(0, new ProductDrink("Coca Cola Can", 330, 15, 40));           
            Inventory.Add(1, new ProductToy("KinderEgg", 25, 80));            
            Inventory.Add(2, new ProductFood("Sandwich", 200, 45,30));
            LogWriter.LogWrite($"Current items in stock {Inventory[0].Name} {Inventory[0].InStock}, {Inventory[1].Name} {Inventory[1].InStock}, {Inventory[2].Name} {Inventory[2].InStock} ");

            return Inventory.Count > current;
        }                        
    }
}
