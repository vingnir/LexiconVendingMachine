using LexiconVendingMachine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LexiconVendingMachine
{
    public class VendingMachine : Product, IVending
    {
        private static Dictionary<int, Product> AvailableProducts;
        private static bool ProductsLoaded;
        public readonly CurrencyDenominations cd;
        public int MoneyPool { get; private set; }

        public VendingMachine()
        {
            this.cd = new CurrencyDenominations();
        }

        public int InsertMoney(int denomination, int quantity)
        {
            int recieptInserted = 0;
            int previousValue = MoneyPool;

            // Prevent negative inputs and check so input is of valid denomination
            bool validDenomination = cd.denominations.Contains(denomination) && denomination > 0 && quantity > 0;
            if (validDenomination)
            {
                int insertedAmount = denomination * quantity;
                MoneyPool += insertedAmount;
                recieptInserted = MoneyPool - previousValue;
                LogWriter.LogWrite($"Inserted amount = {recieptInserted}, Moneypool = {MoneyPool}");
            }
            return recieptInserted;
        }

        public Dictionary<int, Product> GetAvailableProducts()
        {
            if (!ProductsLoaded) { LoadProducts(); }
            return AvailableProducts;
        }

        public Product Purchase(int key)
        {
            Product purchasedItem = null;
            bool productIsAvailable = AvailableProducts.ContainsKey(key) && AvailableProducts[key].InStock > 0;

            if (productIsAvailable && AvailableProducts[key].Price <= MoneyPool)
            {
                AvailableProducts[key].InStock -= 1;
                MoneyPool -= AvailableProducts[key].Price;
                purchasedItem = AvailableProducts[key];
            }

            return purchasedItem;
        }

        public string ShowAll()
        {
            string productList = $"\nId|Name\t\t Size\t Price\t In stock";

            if (!ProductsLoaded) { LoadProducts(); }

            foreach (var product in AvailableProducts)
            {
                productList += $"\n {product.Key}|{product.Value.Name}\t {product.Value.Size}{product.Value.Unit}\t {product.Value.Price}kr\t {product.Value.InStock}";
            }
            return productList;
        }

        public int[,] EndTransaction()
        {
            int[,] changeToReturn = GetChange(MoneyPool);
            MoneyPool = 0;
            return changeToReturn;
        }

        public int[,] GetChange(int moneyBack)
        {
            int[] denominations = cd.denominations;
            int size = cd.denominations.Length;
            int[,] change = new int[size, size];
            int remainingAmount = moneyBack;

            for (int i = size - 1; i >= 0; i--)
            {
                change[i, 0] = remainingAmount / denominations[i];
                change[i, 1] = denominations[i];
                remainingAmount %= denominations[i];
            }
            return change;
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
        public bool LoadProducts()
        {
            ProductFactory productFactory = new ProductFactory();
            if (!ProductsLoaded)
            {
                AvailableProducts = new Dictionary<int, Product>();
                AvailableProducts = productFactory.GetProducts();
                ProductsLoaded = true;
            }
            return ProductsLoaded;
        }
    }
}