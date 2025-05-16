using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    public class BankAccount
    {
        public int balance { get; set; }
        private readonly ILogBook _logBook;
        public BankAccount(ILogBook logBook) {
            balance = 0;
            _logBook = logBook;  
        }
        public bool Deposit(int amount)
        {
            _logBook.Message("Deposti invoked");
            balance += amount;
            return true;
        }

        public bool Withdraw(int amount)
        {
            if (amount <= 0)
            {
                
            _logBook.LogToDb("Withdrawal Amount: " + amount.ToString());    
            balance -= amount;
            return _logBook.LogBalanceAfterWithdrawal(balance);

            }
            return _logBook.LogBalanceAfterWithdrawal(balance-amount);
        }

        public int GetBalance() { return  balance; }
    }
}
