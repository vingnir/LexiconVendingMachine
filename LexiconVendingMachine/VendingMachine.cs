using System;
using System.Collections.Generic;

namespace LexiconVendingMachine
{
    class VendingMachine : Product, IVending
    {
        private readonly SortedDictionary<Product, int> availableProducts; //TODO
        private decimal[] MoneyPool; 
        

        public void Purchase()
        {
            //TODO
        }

        public void ShowAll()
        {
            //Test
            //availableProducts.Add();

            foreach (var product in availableProducts)
            {
                Console.WriteLine(product);
            }
            //TODO
        }

        public void InsertMoney()
        {
            CurrencyDenominations cd = new CurrencyDenominations();
            string currency = "kr";
            Console.WriteLine("Please insert cash in any of the following values");
            foreach(var denomination in cd.denominations)
            {
                Console.WriteLine(denomination + currency);

            }
            Console.ReadLine();
            //TODO
        }

        public void EndTransaction()
        {
            //TODO
        }

        public void DisplayChangeMessage()
        {
            //TODO
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