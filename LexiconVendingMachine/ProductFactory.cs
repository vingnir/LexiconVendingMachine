using System.Collections.Generic;

namespace LexiconVendingMachine
{
    public class ProductFactory : Product
    {
        private readonly SortedDictionary<int, Product> Inventory;

        public ProductFactory()
        {
            Inventory = new SortedDictionary<int, Product>();
        }

        public SortedDictionary<int, Product> GetProducts()
        {
           if(Inventory.Count <=0 ) AddProducts(); // TODO Get from DB 

            return Inventory;
        }

        // TODO DELETE Test method
        public bool AddProducts()
        {
            //int id = 1;
            Inventory.Add(0, new ProductDrink("Coca Cola Can", 330, 15));           
            Inventory.Add(1, new ProductToy("KinderEgg", 50, 25));            
            Inventory.Add(2, new ProductFood("Sandwich", 200, 45));
            LogWriter.LogWrite($"Current items in dictonary {Inventory[0].Name}, {Inventory[1].Name}, {Inventory[2].Name}");

            return Inventory.Count > 0;
        }

        public bool AddProducts(Product newProduct)
        {
            int id = Inventory.Count + 1; //TODO fix

            Inventory.Add(id, newProduct);
            LogWriter.LogWrite($"product was added {Inventory[id].Name}");
            return Inventory.ContainsKey(id);
        }

        public bool AddProducts(Product newProduct, int quantity)
        {
            int id = Inventory.Count + 1; //TODO fix
            for (int i = 0; i < quantity; i++)
            {
                id++;
                Inventory.Add(id, newProduct);
                //TODO Delete
                bool hasValue = Inventory.TryGetValue(id, out var value);
                if (hasValue)
                {
                    LogWriter.LogWrite($"Current values in dictonary : {id} {value.Name} {value.Size}{value.Unit} {value.Price}"); // TODO                                                                                                                        
                }
                else
                {
                    LogWriter.LogWrite("Key not present");
                }
            }

            return Inventory.ContainsKey(id);
        }

        public override string[] Examine()
        {
            throw new System.NotImplementedException();
        }

        public override string Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
