using System;

namespace LexiconVendingMachine.VendingMachineUIConsole
{
    public class ConsoleUI
    {
        private readonly VendingMachine vendingMachine;
        private readonly CurrencyDenominations cd;
        private bool activeTransaction;

        public ConsoleUI()
        {
            vendingMachine = new VendingMachine();
            cd = new CurrencyDenominations();
            this.activeTransaction = false;
        }

        public bool MainMenu()
        {
            string menuItems = $"\n0) Exit, \n1) Purchase product, \n2) Show all products, \n3) Insert money, \n4) End transaction,\nChoose function:";
            Console.Clear();
            Console.WriteLine(menuItems);
            

            switch (Console.ReadLine())
            {
                case "0":
                    RequestMoneyFromUser();
                    return false;
                case "1":
                    SelectProduct();
                    return true;
                case "2":
                    Console.WriteLine(DisplayProducts());
                    Console.ReadKey();
                    return true;
                case "3":
                    vendingMachine.InsertMoney(100, 1); // TODO
                    return true;
                case "4":
                    vendingMachine.EndTransaction();
                    Console.ReadKey();
                    return true;
                default:
                    return true;
            }
        }

        public string GetDenominationList()
        {
            string denominationList = "\nSelect value to insert:\n";
            for (int i = cd.denominations.Length - 1; i >= 0; i--)
            {
                denominationList += $"\n{i}) {cd.denominations[i]}kr";
            }
            return denominationList;           
        }

        public int[] RequestMoneyFromUser()
        {                       
            int[] inputDenomination = new int[2];
            Console.WriteLine(GetDenominationList());
            string usrInput = GetUserInput();
            bool checkDenomination = int.TryParse(usrInput, out int denomination);
            Console.WriteLine("How many of this denomination would you like to insert?");
            string quantity = GetUserInput();
            bool checkQuantity = int.TryParse(quantity, out int multiple);

            if (checkQuantity && checkDenomination && denomination <= cd.denominations.Length)
            {
                inputDenomination[0] = denomination;
                inputDenomination[1] = multiple;
            }          
            return inputDenomination;
        }

        public int SelectProduct()
        {
            this.activeTransaction = true;
            bool inputValidated = false;
            int userInput = -1;
            
            while (activeTransaction)
            {
                Console.Clear();
                Console.WriteLine(vendingMachine.ShowAll());
                Console.WriteLine("\nEnter id to select product...");

                userInput = GetUserInput(inputValidated);
            }          
                        
            return userInput;
        }

        private string GetUserInput()
        {
            string usrInput;
            do
            {
                usrInput = Console.ReadLine();

                if (string.IsNullOrEmpty(usrInput))
                {
                    Console.WriteLine("Invalid input, please try again...");
                }
            } while (string.IsNullOrEmpty(usrInput));

            return usrInput;
        }

        //Overloaded method to parse input and check if selected ID is within valid range
        private int GetUserInput(bool validateProducts)
        {
            ProductFactory pf = new ProductFactory();
            string usrOption;
            bool validate = validateProducts;
            int parsedOption;

            do
            {
                usrOption = Console.ReadLine();
                _ = int.TryParse(usrOption, out parsedOption);
                validate = parsedOption < pf.GetProducts().Count;
                if (!validate || string.IsNullOrEmpty(usrOption))
                {
                    Console.WriteLine($"Invalid input, please try again\n Only enter valid product id:s between 0 and {pf.GetProducts().Count}");
                }
            } while (string.IsNullOrEmpty(usrOption) || !validate);

            return parsedOption;
        }

        public string DisplayChange(int moneyToReturn)
        {
            CurrencyDenominations cd = new CurrencyDenominations();
            Calculator calculator = new Calculator();
            var change = calculator.GetChange(moneyToReturn);
            string displayChangeMsg = $"\nMoney in return from {moneyToReturn}kr \n";
            for (int i = cd.denominations.Length - 1; i >= 0; i--)
            {
                displayChangeMsg += change[i] > 0 ? $"\n" + change[i] + " x " + cd.denominations[i] + "kr" : "";
            }
            return displayChangeMsg;
        }

        public string DisplayProducts()
        {
            VendingMachine vm = new VendingMachine();
            string displayItems = vm.ShowAll();

            return displayItems;
        }
    }
}
