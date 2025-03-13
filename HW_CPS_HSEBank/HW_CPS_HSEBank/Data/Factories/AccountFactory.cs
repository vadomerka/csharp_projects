using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data.Factories
{
    public class AccountFactory : IDataFactory<BankAccount>
    {
        private int lastId = 0;

        public BankAccount Create()
        {
            return new BankAccount(++lastId, "", 0);
        }
        public BankAccount Create(string name, decimal balance) { 
            BankAccount bankAccount = new BankAccount(++lastId, name, balance);
            return bankAccount;
        }

        public BankAccount Create(BankAccount nAcc)
        {
            BankAccount bankAccount = new BankAccount(nAcc.Id, nAcc.Name, nAcc.Balance);
            return bankAccount;
        }
    }
}
