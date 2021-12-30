﻿using LexiconVendingMachine.VendingMachineUIConsole;

namespace LexiconVendingMachine
{
    class Program
    {
        static void Main()
        {
            ConsoleUI userInterface = new ConsoleUI();
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = userInterface.MainMenu();
            }
        }
    }
}
