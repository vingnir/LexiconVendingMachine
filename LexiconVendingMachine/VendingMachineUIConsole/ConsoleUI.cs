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
                    return false;
                case "1":
                    HandleUserInteraction(1);
                    return true;
                case "2":
                    Console.WriteLine(DisplayProducts());
                    Console.ReadKey();
                    return true;
                case "3":
                    Console.WriteLine(ReturnChange());
                    Console.ReadKey();// TODO
                    return true;
                case "4":
                    vendingMachine.EndTransaction();
                    Console.ReadKey();
                    return true;
                default:
                    return true;
            }
        }

        public bool HandleUserInteraction(int option)
        {
            int selected;
            bool returnValue = option == 1;
            string msgToUser;
            
            if (vendingMachine.MoneyPool == 0)
            {
                Console.WriteLine("You need to add money to the machine! To add money enter 'y' ");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    int[] cashDeposit = new int[2];
                    cashDeposit = RequestMoneyFromUser();
                    bool cashInserted = vendingMachine.InsertMoney(cashDeposit[0],cashDeposit[1]);
                    
                    string userInfo = cashInserted ? $"Available amount is {vendingMachine.MoneyPool}" : "Transaction could not be performed";
                    Console.WriteLine(userInfo);
                    Console.ReadKey();
                    
                }
                selected = SelectProduct();
                LogWriter.LogWrite("pool före" + vendingMachine.MoneyPool);
                returnValue = vendingMachine.Purchase(selected);
                var productsInStock = vendingMachine.GetAvailableProducts();
                LogWriter.LogWrite("available products is loaded");
                msgToUser = productsInStock[selected].Use();
                LogWriter.LogWrite(msgToUser);
                Console.Write(msgToUser);
                Console.ReadKey();
                               
            }
            else { returnValue = false; return returnValue; }


            return returnValue;
        }
        public bool HandleUserInteraction()
        {
            return true;
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
            string usrInput = ValidateUserInput();
            bool checkDenomination = int.TryParse(usrInput, out int denomIndex);
            Console.WriteLine("How many of this denomination would you like to insert?");
           
            string quantity = ValidateUserInput();
            bool checkQuantity = int.TryParse(quantity, out int multiple);

            if (checkQuantity && checkDenomination && denomIndex <= cd.denominations.Length)
            {
                inputDenomination[0] = cd.denominations[denomIndex];
                inputDenomination[1] = multiple;
            }     
            
            return inputDenomination;
        }

        public int SelectProduct()
        {
            //Console.Clear();
            ProductFactory pf = new ProductFactory();
            this.activeTransaction = true;
            int userInput = -1;
            int maxValue = pf.GetProducts().Count;
            int minValue = 0;
            while (activeTransaction)
            {
                
                Console.WriteLine(DisplayProducts());
                Console.WriteLine("\nEnter id to select product...");
                userInput = ValidateUserInput(minValue, maxValue);
                Console.ReadKey();
            }          
            
            return userInput;
        }

        private string ValidateUserInput()
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

        //Overloaded method to parse input and check if selected input int is within valid range
        private int ValidateUserInput(int min, int max)
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
