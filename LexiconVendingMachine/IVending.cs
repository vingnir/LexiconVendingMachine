using System;
using System.Collections.Generic;
using System.Text;

namespace LexiconVendingMachine
{
    interface IVending
    {
       void Purchase(); //TODO

        void ShowAll();

        void InsertMoney();

        void EndTransaction();
    }
}
