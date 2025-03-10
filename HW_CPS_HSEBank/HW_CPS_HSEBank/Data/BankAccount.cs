using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data
{
    internal class BankAccount : IAccount
    {
        int id;
        string name;
        int balance;

        public BankAccount(int id, string name, int balance) {
            this.id = id;
            this.name = name;
            this.balance = balance;
        }

        public int Id { get => id; }
        public string Name { get => name; set { name = value; } }
        public int Balance { get; set; }
    }
}
