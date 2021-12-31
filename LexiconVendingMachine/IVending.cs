namespace LexiconVendingMachine
{
    interface IVending
    {
        Product Purchase(int key);

        string ShowAll();

        bool InsertMoney(int denomination, int quantity); //TODO return reciept INT or string , inserted value , total value

        int[] EndTransaction();
    }
}
