using System;
using System.Collections.Generic;
using System.Linq;

namespace LexiconVendingMachine
{
    public class VendingMachine : Product, IVending
    {
        private static Dictionary<int,Product> AvailableProducts;        
        private static bool ProductsLoaded;
        private int MoneyPool { get; set; }
                   
        private bool LoadProducts()
        {
            ProductFactory productFactory = new ProductFactory();
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
            int currentValue = MoneyPool;
            CurrencyDenominations cd = new CurrencyDenominations();

            // Prevent negative inputs and check so input is valid denomination
            bool validDenomination = cd.denominations.Contains(denomination) && denomination > 0 && quantity > 0;
           
            if (validDenomination)
            {
                int insertedAmount = denomination * quantity;
                MoneyPool += insertedAmount;              
                success = currentValue < MoneyPool;
            }
            else success = false;
            
            return success;
        }

        public bool Purchase(int key)
        {
            MoneyPool = 100; //TODO DELETE
            if (!ProductsLoaded) { LoadProducts(); }

            bool productIsAvailable = AvailableProducts.ContainsKey(key) && AvailableProducts[key].Price <= MoneyPool;

            if (productIsAvailable)
            {
                AvailableProducts[key].InStock -= 1;
                MoneyPool -= AvailableProducts[key].Price;
                
                return true;
            }
            else return false;            
        }

        public string ShowAll()
        {
            string productList = string.Empty;
            
            if (!ProductsLoaded) { LoadProducts(); }

            foreach (var product in AvailableProducts)
            {
                productList += $"\n{product.Value.Name}\t Size: {product.Value.Size}{product.Value.Unit} Price: {product.Value.Price} In stock: {product.Value.InStock}";
            }            
            return productList;
        }

        public int[] EndTransaction()
        {
            Calculator calculator = new Calculator();
            int[] depositToReturn = calculator.GetChange(MoneyPool);
            
            return depositToReturn;
        }

        public override string Examine()
        {
            string machineInfo = $"Vending Machine version 1.0";
            return machineInfo;
        }

        public override string Use()
        {
            string instructions = $"Put money in the machine and follow the instructions...";
            return instructions;
        }
    }
}