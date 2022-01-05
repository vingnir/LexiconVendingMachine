namespace LexiconVendingMachine
{
    interface IVending
    {
        Product Purchase(int key);

        string ShowAll();

        int InsertMoney(int denomination, int quantity);

        int[,] EndTransaction();
    }
}
