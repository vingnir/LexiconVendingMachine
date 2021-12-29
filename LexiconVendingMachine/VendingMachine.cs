using System;
using System.Collections.Generic;
using System.Linq;

namespace LexiconVendingMachine
{
    public class VendingMachine : Product, IVending
    {
        private SortedDictionary<int,Product> AvailableProducts;  //TODO
        private readonly ProductFactory productFactory;

        private decimal MoneyPool { get; set; }

       
        public VendingMachine()
        {           
            this.AvailableProducts = new SortedDictionary<int, Product>();
            this.productFactory = new ProductFactory();
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
        public bool Purchase()
        {
            //TODO
            

            return true;
        }

        public string[] ShowAll()
        {
            if (AvailableProducts.Count == 0) AvailableProducts = productFactory.GetProducts();

            string[] display = new string[AvailableProducts.Count];
            for (int i = 0; i < AvailableProducts.Count; i++)
            {
                var add = AvailableProducts[i];
                display[i] = add.Name + add.Size + add.Unit + add.Price;
                //LogWriter.LogWrite(product + "testar");
                
            }
            //TODO
            return display;
        }


        public bool EndTransaction()
        {
            //TODO
            return true;
        }

        public void DisplayChangeMessage()
        {
            //TODO
        }

        public override string Examine()
        {
            string examineItem = $"{Name} {Size} {Unit} {Price}";// TODO fix

            return examineItem;

        }

        public override string Use()
        {
            throw new NotImplementedException();
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