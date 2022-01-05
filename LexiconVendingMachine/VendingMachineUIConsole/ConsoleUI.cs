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

        public bool Initialize()
        {
            bool confirm = vendingMachine.LoadProducts();
            return confirm;
        }

        public bool MainMenu()
        {
            string exitMsg = "\n\n Press any key to exit...";
            string menuItems = $"\n0) Exit, \n1) Purchase product, \n2) Show all products,\n3) Add credit, \n4) Show credit, \n5) End transaction \nChoose function:";
            // Console.Clear(); //TODO Uncomment Stringreader will get erased during test
            Console.WriteLine(menuItems);
            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    HandleUserPurchase();
                    return true;
                case "2":
                    Console.WriteLine($"{DisplayProducts()} ");
                    Console.ReadKey();
                    return true;
                case "3":
                    HandleTransaction();
                    return true;
                case "4":
                    Console.WriteLine($"\nCredit: {ShowDeposit()}kr {exitMsg}");
                    Console.ReadKey();
                    return true;
                case "5":
                    Console.WriteLine($"\nCurrent credit: {ShowDeposit()} \n\n{ReturnChange()} {exitMsg}");
                    Console.ReadKey();
                    return true;
                default:
                    return true;
            }
        }

        public void HandleUserPurchase()
        {
            int selected;
            Product purchasedProduct;
            ConsoleKeyInfo usrReply;
            var availableProducts = vendingMachine.GetAvailableProducts();
            this.ActiveTransaction = true;
            while (ActiveTransaction)
            {
                //  Console.Clear(); //TODO Uncomment Stringreader will get erased during test
                Console.WriteLine(DisplayProducts());
                Console.WriteLine($"\nTotal credit: {ShowDeposit()}kr \n\nEnter id to select product:");
                selected = SelectProduct();
                if (selected < 0) { ActiveTransaction = false; MainMenu(); }
                else if (availableProducts[selected].Price > ShowDeposit())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nCredit is to low\n");
                    Console.ResetColor();
                    Console.WriteLine($"Current credit = {ShowDeposit()}kr \n\tPress 'y' to add more credit or enter to choose another item");
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
                    Console.WriteLine($"{purchasedProduct.Use()}\n\n Press enter to continue... ");
                    Console.ReadKey();
                }
            }
        }

        public int ShowDeposit()
        {
            int credit = vendingMachine.MoneyPool;
            return credit;
        }

        public string GetDenominationInfo()
        {
            string denominationInfo = "\nSelect value to insert:\n";
            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                denominationInfo += $"\n{index}) {cd.denominations[index]}kr";
            }
            return denominationInfo;
        }

        public bool HandleTransaction()
        {
            string reply;
            bool activeRequest = true;
            while (activeRequest)
            {
                // Console.Clear(); //TODO Comment out while testing 
                Console.WriteLine($"\nPlease input money to the machine! \nCurrent credit = {ShowDeposit()}kr");
                int[] cashDeposit = RequestMoneyFromUser();
                vendingMachine.InsertMoney(cashDeposit[0], cashDeposit[1]);
                Console.WriteLine($"Current credit = {ShowDeposit()}kr \n\tInsert more money? Press 'y' or press any key to exit...");
                reply = Console.ReadLine();
                if (reply == "y") { continue; } else { activeRequest = false; }
            }
            return true;
        }
        public int[] RequestMoneyFromUser()
        {
            int denominationId;
            int[] inputDenomination = new int[2];
            Console.WriteLine(GetDenominationInfo());
            int maxValue = cd.denominations.Length - 1;
            int minValue = 0;
            denominationId = GetUserInput(minValue, maxValue);
            if (denominationId < 0) { inputDenomination[1] = 0; return inputDenomination; }
            Console.WriteLine($"Selected denomination: {cd.denominations[denominationId]}kr \nHow many of this value would you like to deposit?");
            // Set max quantity 
            maxValue = 100;
            int quantity = GetUserInput(minValue, maxValue);
            inputDenomination[0] = cd.denominations[denominationId];
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

        //Method to parse input to Integer and check if value is within valid range       
        public int GetUserInput(int min, int max)
        {
            //string userInput;
            int validNum = -1;
            bool validated = false;
            string errorMsg = $" Invalid input!\n\nEnter a number between {min} & {max} or type 'exit' for main menu";
            Console.WriteLine($"\nEnter a number between {min} & {max}");
            while (!validated)
            {
                string userInput = Console.ReadLine();
                if (userInput == "exit") { ActiveTransaction = false; return -1; }
                else if (int.TryParse(userInput, out validNum))
                {
                    if (min <= validNum && validNum <= max)
                    { validated = true; }
                    else { Console.WriteLine(errorMsg); continue; }
                }
                else
                {
                    Console.WriteLine(errorMsg); continue;
                }
            }
            return validNum;
        }

        public string ReturnChange()
        {
            int deposit = ShowDeposit();
            string displayChangeMsg = $"Money in return from {deposit} kr";
            var change = vendingMachine.EndTransaction();
            if (deposit == 0) { displayChangeMsg += "\n\nThere is no money to return\n"; }
            for (int index = cd.denominations.Length - 1; index >= 0; index--)
            {
                displayChangeMsg += change[index, 0] > 0 ? $"\n\t" + change[index, 0] + " x " + cd.denominations[index] + "kr" : "";
            }
            ActiveTransaction = false;
            return displayChangeMsg;
        }

        public string DisplayProducts()
        {
            string displayItems = vendingMachine.ShowAll();
            return displayItems;
        }

        // TODO DELETE Only for testing purpose
        public bool GetFreeCredit()
        {
            bool result = vendingMachine.InsertMoney(6, 1) > 0;
            return result;

        }
    }
}
