using System;
using System.Collections.Generic;
using System.Linq;

namespace LexiconVendingMachine
{
    public class VendingMachine : Product, IVending
    {
        private static Dictionary<int,Product> AvailableProducts;  //TODO
        private readonly ProductFactory productFactory;
        private static bool ProductsLoaded;

        private decimal MoneyPool { get; set; }

       
        public VendingMachine()
        {           
            //this.AvailableProducts = new Dictionary<int, Product>();
            this.productFactory = new ProductFactory();
            this.MoneyPool = 100;
            
            
        }

        private bool LoadProducts()
        {
            if (AvailableProducts == null)
            {
                AvailableProducts = new Dictionary<int, Product>();
                AvailableProducts = productFactory.GetProducts();
                ProductsLoaded = true;

                return true;                
            }

            return false;
        }
        //TODO
        public bool InsertMoney(int denomination, int quantity)
        {
            bool success;
            decimal currentValue = MoneyPool;
            CurrencyDenominations cd = new CurrencyDenominations();
            LogWriter.LogWrite("Current value : " + MoneyPool); // TODO
            bool validDenomination = cd.denominations.Contains(denomination);

            // Prevent negative inputs and check so input is valid denomination
            if (denomination > 0 && quantity > 0 && validDenomination)
            {
                int insertedAmount = denomination * quantity;
                MoneyPool += insertedAmount;
                LogWriter.LogWrite("Updated value : " + MoneyPool); // TODO
                success = currentValue < MoneyPool;
            }
            else
            {
                success = false;
            }
            return success;
        }
        public bool Purchase(int key)
        {
            LogWriter.LogWrite("ProductsLoaded : " + ProductsLoaded); // TODO
            //TODO
            if (!ProductsLoaded) { LoadProducts(); }

            bool productIsAvailable = AvailableProducts.ContainsKey(key);
            LogWriter.LogWrite("ProductsLoaded : " + ProductsLoaded); // TODO
            LogWriter.LogWrite("key " + key +  "productIsAvailable : " + productIsAvailable); // TODO
            if (productIsAvailable && AvailableProducts[key].Price <= MoneyPool) 
            {
                AvailableProducts[key].InStock -= 1;
                MoneyPool -= AvailableProducts[key].Price;
                LogWriter.LogWrite("Updated value : " + MoneyPool); // TODO
            }

            return productIsAvailable;

        }

        public string ShowAll()
        {
            string productList = string.Empty;
            LogWriter.LogWrite("ProductsLoaded : " + ProductsLoaded); // TODO
            if (!ProductsLoaded) { LoadProducts(); }

            foreach (var product in AvailableProducts)
            {
                productList += $"\n{product.Value.Name}\t Size: {product.Value.Size}{product.Value.Unit} Price: {product.Value.Price} In stock: {product.Value.InStock}";
            }
            
            //TODO
            return productList;
        }


        public bool EndTransaction()
        {
            //TODO
            return true;
        }


        public override string Examine()
        {
            string examineItem = $"Vending Machine version 1.0";

            return examineItem;

        }

        public override string Use()
        {
            string instructions = $"Put money in the machine and follow the instructions";

            return instructions;
        }
    }
}

/*
 // The Add method throws an exception if the new key is
        // already in the dictionary.
        try
        {
            openWith.Add("txt", "winword.exe");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("An element with Key = \"txt\" already exists.");
        }
 */