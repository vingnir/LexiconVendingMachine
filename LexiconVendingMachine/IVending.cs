namespace LexiconVendingMachine
{
    interface IVending
    {
        bool Purchase(int key); //TODO

        string ShowAll();

        bool InsertMoney(int denomination, int quantity);

        bool EndTransaction();
    }
}
