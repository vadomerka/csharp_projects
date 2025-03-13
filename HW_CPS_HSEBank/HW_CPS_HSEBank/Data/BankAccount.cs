using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data
{
    public class BankAccount : IBankDataType
    {
        private int id;
        private string name;
        private decimal balance;

        public BankAccount() {
            this.id = 0;
            this.name = "";
            this.balance = 0;
        }

        public BankAccount(int id, string name, decimal balance) {
            this.id = id;
            this.name = name;
            this.balance = balance;
        }

        public int Id { get => id; set { id = value; } }
        public string Name { get => name; set { name = value; } }
        public decimal Balance { get => balance; set => balance = value; }
    }
}
