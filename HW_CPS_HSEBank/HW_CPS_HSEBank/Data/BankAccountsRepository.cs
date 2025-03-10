using HW_CPS_HSEBank.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data
{
    internal class BankAccountsRepository
    {
        List<IAccount> _accounts;

        public BankAccountsRepository() {
            _accounts = new List<IAccount>();
        }

        public List<IAccount> Accounts { get; }

        public void AddAccount(IAccount account)
        {
            _accounts.Add(account);
        }
    }
}
