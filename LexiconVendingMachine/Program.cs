using LexiconVendingMachine.VendingMachineUIConsole;

namespace LexiconVendingMachine
{
    class Program
    {
        static void Main()
        {
            ConsoleUI userInterface = new ConsoleUI();
            userInterface.LoadVendingMachine();
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = userInterface.MainMenu();
            }
        }
    }
}
