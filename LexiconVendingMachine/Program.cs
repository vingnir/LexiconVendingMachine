﻿using System;

namespace LexiconVendingMachine
{
    class Program
    {
        static void Main()
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
        }

        private static bool MainMenu()
        {
            VendingMachine vendingMachine = new VendingMachine();

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
                    return false;
                case "1":
                    vendingMachine.Purchase();
                    return true;
                case "2":
                    vendingMachine.ShowAll();
                    return true;
                case "3":
                    vendingMachine.InsertMoney();
                    return true;
                case "4":
                    vendingMachine.EndTransaction();
                    return true;

                default:
                    return true;
            }

        }
    }
}
