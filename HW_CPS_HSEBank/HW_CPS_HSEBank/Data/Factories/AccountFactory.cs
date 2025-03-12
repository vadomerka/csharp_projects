using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data.Factories
{
    internal class AccountFactory : IDataFactory
    {
        int lastId = 0;

        public BankAccount CreateAccount(string name, decimal balance) { 
            BankAccount bankAccount = new BankAccount(++lastId, name, balance);
            return bankAccount;
        }

        public BankAccount CreateAccount(BankAccount nAcc)
        {
            BankAccount bankAccount = new BankAccount(nAcc.Id, nAcc.Name, nAcc.Balance);
            return bankAccount;
        }
    }
}
