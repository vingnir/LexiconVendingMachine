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

        public static void DisplayChange(int moneyToReturn)
        {
            CurrencyDenominations cd = new CurrencyDenominations();
            Calculator calculator = new Calculator();
            var change = calculator.GetChange(moneyToReturn);
            for (int i = cd.denominations.Length - 1; i >= 0; i--)
            {
                if (change[i] > 0) { Console.WriteLine("And the change is " + change[i] + " x " + cd.denominations[i] + "kr"); }
            }
            Console.ReadKey();
        }

        public static void DisplayProducts()
        {
            VendingMachine vm = new VendingMachine();
            Console.WriteLine(vm.ShowAll());
            Console.ReadKey();
        }
        /*
        public void HandleUserInputs()
        {

            //TODO move to UI
             int GetAmountFromUser()
            {
                int amount;
                Console.WriteLine("How many of this value do you want to deposit");
                string input = Console.ReadLine();
                amount = int.Parse(input);

                return amount;
            }


        }
        
        public void InsertMoney()
        // TODO MOVE TO UI
        {
            CurrencyDenominations cd = new CurrencyDenominations();
            string currency = "kr";
            int counter = 1;
            Console.WriteLine("Please choose option to insert cash in any of the following values");
            foreach (var denomination in cd.denominations)
            {
                Console.WriteLine($"{ counter++ }) { denomination} { currency}");
            }

            switch (Console.ReadLine())
            {

                case "0":
                    AddToMoneyPool(cd.denominations.First, GetAmountFromUser());
                    break;
                case "2":
                    AddToMoneyPool(cd.denominations[1], GetAmountFromUser());
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                default:
                    break;



            }
            Console.ReadLine();
            //TODO
        }

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
