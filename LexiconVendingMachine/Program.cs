using System;

namespace LexiconVendingMachine
{
    class Program
    {
        private static VendingMachine vendingMachine;
       
        static void Main()
        {
            vendingMachine = new VendingMachine();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            

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
                    Program.DisplayProducts();
                    return true;
                case "3":
                    vendingMachine.InsertMoney(100,1); // TODO
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
                if (change[i] > 0) { Console.WriteLine("And the change is " + change[i] + " x " + cd.denominations[i]+ "kr"); }
            }
            Console.ReadKey();
        }

            public static void DisplayProducts()
        {
            VendingMachine vm = new VendingMachine();            
            Console.WriteLine(vm.ShowAll());
            Console.ReadKey();
        }
    }
}
