using System;

namespace LexiconVendingMachine.VendingMachineUIConsole
{
    public class ConsoleUI
    {
        private readonly VendingMachine vendingMachine;
        private readonly CurrencyDenominations cd;
        private bool ActiveTransaction;

        public ConsoleUI()
        {
            vendingMachine = new VendingMachine();
            cd = new CurrencyDenominations();
            this.ActiveTransaction = false;
        }

        public bool MainMenu()
        {
            string menuItems = $"\n0) Exit, \n1) Purchase product, \n2) Show all products,\n3) Add credit, \n4) Show credit, \n5) End transaction \nChoose function:";
            Console.Clear();
            Console.WriteLine(menuItems);
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    HandleUserInteraction();
                    return true;
                case "2":
                    Console.WriteLine(DisplayProducts());
                    Console.ReadKey();
                    return true;
                case "3":
                    HandleTransaction();
                    return true;
                case "4":
                    Console.WriteLine($"\nCredit: {ShowDeposit()}");
                    Console.ReadKey();
                    return true;
                case "5":
                    Console.WriteLine($"\nCurrent credit: {ShowDeposit()} \n{ReturnChange()}");
                    Console.ReadKey();
                    return true;
                default:
                    return true;
            }
        }

        public void HandleUserInteraction() //TODO
        {
            int selected;
            Product purchasedProduct;
            ConsoleKeyInfo usrReply;
            string productInfo;
            var availableProducts = vendingMachine.GetAvailableProducts();
            this.ActiveTransaction = true;
            while (ActiveTransaction)
            {
                Console.Clear();
                Console.WriteLine(DisplayProducts());
                Console.WriteLine($"\nTotal credit: {ShowDeposit()}\nEnter id to select product:");
                selected = SelectProduct();
                if(selected < 0) { MainMenu(); }
                else if (availableProducts[selected].Price > ShowDeposit())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nCredit is to low\n");
                    Console.ResetColor();
                    Console.WriteLine($"Current credit = {ShowDeposit()} \n\tPress 'y' to add more credit or enter to choose another item");
                    usrReply = Console.ReadKey(true);
                    if (usrReply.Key == ConsoleKey.Y) { HandleTransaction(); }
                    continue;
                }
                else if (availableProducts[selected].InStock == 0)
                {
                    Console.WriteLine($"{availableProducts[selected].Name} is out of stock\nPlease choose another product\n Press enter to continue...");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    purchasedProduct = vendingMachine.Purchase(selected);
                    productInfo = $"Selected product: {purchasedProduct.Name}\n Price: {purchasedProduct.Price}kr\n { purchasedProduct.Use()}";
                    Console.WriteLine($"{productInfo}\n Press enter to continue... ");
                    Console.ReadKey();
                }
            }
        }

        public int ShowDeposit()
        {
            int credit = vendingMachine.MoneyPool;
            return credit;
        }

        public string GetDenominationList()
        {
            string denominationList = "\nSelect value to insert:\n";
            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                denominationList += $"\n{index}) {cd.denominations[index]}kr";
            }
            return denominationList;
        }

        public bool HandleTransaction()
        {
            ConsoleKeyInfo answer;
            bool activeRequest = true;
            while (activeRequest)
            {
                Console.Clear();
                Console.WriteLine($"\nPlease input money to the machine or type 'exit' for Main menu \nCurrent credit = {ShowDeposit()}");
                int[] cashDeposit;
                cashDeposit = RequestMoneyFromUser();
                vendingMachine.InsertMoney(cashDeposit[0], cashDeposit[1]);
                Console.WriteLine($"Current credit = {ShowDeposit()} \n\tInsert more money? 'y' or press enter to continue...");
                answer = Console.ReadKey(true);
                if (answer.Key == ConsoleKey.Y) { continue; } else { activeRequest = false; }
            }
            return true;
        }
        public int[] RequestMoneyFromUser()
        {
            int[] inputDenomination = new int[2];
            Console.WriteLine(GetDenominationList());
            int maxValue = cd.denominations.Length - 1;
            int minValue = 0;
            int denominationIndex = GetUserInput(minValue, maxValue);
            Console.WriteLine($"Selected denomination: {cd.denominations[denominationIndex]} \nHow many of this value would you like to deposit?");
            // Set max quantity 
            maxValue = 100;
            int quantity = GetUserInput(minValue, maxValue);
            inputDenomination[0] = cd.denominations[denominationIndex];
            inputDenomination[1] = quantity;

            return inputDenomination;
        }

        public int SelectProduct()
        {
            int selectedItemID;
            int maxValue = vendingMachine.GetAvailableProducts().Count - 1;
            int minValue = 0;
            selectedItemID = GetUserInput(minValue, maxValue);

            return selectedItemID;
        }

        //Method to parse input and check if selected input int is within valid range
        private int GetUserInput(int min, int max)
        {
            string userInput;
            bool validate;
            int parsedInputInt;

            do
            {
                userInput = Console.ReadLine();
                _ = int.TryParse(userInput, out parsedInputInt);
                validate = parsedInputInt >= min && parsedInputInt <= max;
                if (userInput == "exit")
                {
                    ActiveTransaction = false;
                    parsedInputInt = -1;
                    MainMenu();
                    break;
                }
                else if (string.IsNullOrEmpty(userInput) || !validate)
                {
                    Console.WriteLine($"Invalid input, please try again or type 'exit' for main menu...\n Only enter numbers between {min} and {max} ");
                    parsedInputInt = -1;
                }
            }
            while (string.IsNullOrEmpty(userInput) || !validate || parsedInputInt == -1);

            return parsedInputInt;
        }

        public string ReturnChange()
        {
            string displayChangeMsg = $"\nMoney in return from {ShowDeposit()}kr \n";
            var change = vendingMachine.EndTransaction();
            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                displayChangeMsg += change[index] > 0 ? $"\n" + change[index] + " x " + cd.denominations[index] + "kr" : "";
            }
            ActiveTransaction = false;
            return displayChangeMsg;
        }


        public string DisplayProducts()
        {
            string displayItems = vendingMachine.ShowAll();
            return displayItems;
        }
    }
}
