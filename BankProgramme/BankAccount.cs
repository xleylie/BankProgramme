using System;
using System.Collections.Generic;
using System.Text;

namespace BankProgramme
{
    internal class BankAccount
    {
        public int Balance=1000;

        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public int GetBalance()
        {
            return Balance;
        }
    }
}
