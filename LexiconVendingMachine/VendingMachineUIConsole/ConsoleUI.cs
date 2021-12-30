using System;

namespace LexiconVendingMachine.VendingMachineUIConsole
{
    public class ConsoleUI
    {
        public bool MainMenu()
        {
            VendingMachine vendingMachine = new VendingMachine(); ;

            Console.Clear();
            Console.WriteLine("...Lexicon Vending Machine...\n");
            Console.WriteLine("0) Exit");
            Console.WriteLine("1) Purchase product");
            Console.WriteLine("2) Show all products");
            Console.WriteLine("3) Insert money");
            Console.WriteLine("4) End transaction");
            Console.Write("\r\nVälj funktion: ");

            switch (Console.ReadLine())
            {
                case "0":

                    DisplayChange(135); //TODO

                    return true;
                case "1":
                    vendingMachine.Purchase(1);
                    return true;
                case "2":
                       DisplayProducts();
                    return true;
                case "3":
                    vendingMachine.InsertMoney(100, 1); // TODO
                    return true;
                case "4":
                    vendingMachine.EndTransaction();
                    return true;

                default:
                    return true;
            }

        }

        public string DisplayChange(int moneyToReturn)
        {
            CurrencyDenominations cd = new CurrencyDenominations();
            Calculator calculator = new Calculator();
            var change = calculator.GetChange(moneyToReturn);
            string displayChangeMsg = $"\nMoney in return from {moneyToReturn}kr \n";
            for (int i = cd.denominations.Length - 1; i >= 0; i--)
            {
                displayChangeMsg += change[i] > 0 ? $"\n" + change[i] + " x " + cd.denominations[i] + "kr":""; 
            }        
            return displayChangeMsg;
        }

        public string DisplayProducts()
        {
            VendingMachine vm = new VendingMachine();
            string displayItems = vm.ShowAll();
           
            return displayItems;
        }
        /*
       

        public int GetAmountFromUser()
        {
            int amount;
            Console.WriteLine("How many of this value do you want to deposit");
            string input = Console.ReadLine();
            amount = int.Parse(input);

            return amount;
        }
        */
    }
}
