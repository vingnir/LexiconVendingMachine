using System;

namespace LexiconVendingMachine
{
    public class Calculator
    {
        public int[] GetChange(int cashBack)
        {           
            CurrencyDenominations cd = new CurrencyDenominations();
            int[] denominations = cd.denominations;
            int[] change = new int[denominations.Length];
            int remainingAmount = cashBack;

            for (int i = denominations.Length -1; i >= 0; i--)
            {
                change[i] = remainingAmount / denominations[i];               
                remainingAmount %= denominations[i];               
            }                      
            return change;            
        }
    }
}
