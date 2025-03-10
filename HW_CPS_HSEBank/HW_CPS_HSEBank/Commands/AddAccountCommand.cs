using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    internal class AddAccountCommand : IBankOperation
    {
        private readonly string name = "";
        private readonly int balance = 0;

        public AddAccountCommand(string name, int balance)
        {
            this.name = name;
            this.balance = balance;
        }

        public string Type => "Add Account";

        public void Execute()
        {

            AccountFactory accountFactory = new AccountFactory();
            BankAccountsRepository mb = new(); // TODO
            mb.AddAccount(accountFactory.CreateAccount(name, balance));
        }
    }
}
