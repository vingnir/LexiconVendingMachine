namespace LexiconVendingMachine
{
    interface IVending
    {
        bool Purchase(); //TODO

        string[] ShowAll();

        bool InsertMoney(int denomination, int quantity);

        bool EndTransaction();
    }
}
