using LexiconVendingMachine.VendingMachineUIConsole;

namespace LexiconVendingMachine
{
    class Program
    {
        static void Main()
        {
            ConsoleUI userInterface = new ConsoleUI();
            userInterface.Initialize();
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = userInterface.MainMenu();
            }
        }
    }
}
