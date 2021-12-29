namespace LexiconVendingMachine
{
    interface IVending
    {
        bool Purchase(); //TODO

        string[] ShowAll();

        bool InsertMoney();

        bool EndTransaction();
    }
}
