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
            string menuItems = $"\n0) Exit, \n1) Purchase product, \n2) Show all products, \n3) Show deposited amount, \n4) End transaction,\nChoose function:";
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
                    ShowDeposit();
                    return true;
                case "4":
                    Console.WriteLine($"Returned deposit: {vendingMachine.EndTransaction()}");
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
            string productInfo;
            int budget = 0;

            if (vendingMachine.MoneyPool == 0 )
            {
                Console.WriteLine("You need to add money to the machine! To add money enter 'y' ");
                int[] cashDeposit;
                cashDeposit = RequestMoneyFromUser();
                bool cashInserted = vendingMachine.InsertMoney(cashDeposit[0], cashDeposit[1]);
                budget = cashInserted ? vendingMachine.MoneyPool : 0;
               // Console.WriteLine(productInfo);
                Console.ReadKey();
            }

            if (budget > 0)
            {
                selected = SelectProduct();

                purchasedProduct = vendingMachine.Purchase(selected);

                //var productsInStock = vendingMachine.GetAvailableProducts();

                productInfo = purchasedProduct.Use();
                //LogWriter.LogWrite(msgToUser);
                Console.WriteLine(productInfo);
                Console.ReadKey();
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
            bool checkDenomination = int.TryParse(usrInput, out int denomIndex);
            Console.WriteLine("How many of this value would you like to insert?");

            string quantity = GetUserInput();
            bool checkQuantity = int.TryParse(quantity, out int multiple);

            if (checkQuantity && checkDenomination && denomIndex <= cd.denominations.Length ) //TOOD check <= 
            {
                inputDenomination[0] = cd.denominations[denomIndex];
                inputDenomination[1] = multiple;
            }

            return inputDenomination;
        }

        public int SelectProduct()
        {           
            int selectedItemID;
            int maxValue = vendingMachine.GetAvailableProducts().Count;
            int minValue = 0;
            Console.WriteLine(DisplayProducts());
            Console.WriteLine("\nEnter id to select product...");
            selectedItemID = GetUserInput(minValue, maxValue);
            
            return selectedItemID;
        }
 
        //Overloaded method to parse input and check if selected input int is within valid range
        private int GetUserInput(int min, int max)
        {
            string userInput;
            bool validate;
            int parsedInputInt;
            do
            {
                userInput = Console.ReadLine();
                _ = int.TryParse(userInput, out parsedInputInt);
                validate = parsedInputInt >= min && parsedInputInt < max;
                if (!validate || string.IsNullOrEmpty(userInput))
                {
                    Console.WriteLine($"Invalid input, please try again\n Only enter valid product id numbers");
                }
            } while (string.IsNullOrEmpty(userInput) || !validate);

            return parsedInputInt;
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

        public string ReturnChange()
        {
            var change = vendingMachine.EndTransaction();
            string displayChangeMsg = $"\nMoney in return from {vendingMachine.MoneyPool}kr \n";
            for (int i = cd.denominations.Length - 1; i >= 0; i--)
            {
                displayChangeMsg += change[i] > 0 ? $"\n" + change[i] + " x " + cd.denominations[i] + "kr" : "";
            }
            return displayChangeMsg;
        }


        public string DisplayProducts()
        {
            string displayItems = vendingMachine.ShowAll();

            return displayItems;
        }
    }
}
