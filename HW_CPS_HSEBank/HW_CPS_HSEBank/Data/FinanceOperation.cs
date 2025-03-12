using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW_CPS_HSEBank.Data
{
    public class FinanceOperation
    {
        private int id;
        private string type;
        private int bankAccountId;
        private decimal amount;
        private DateTime date;
        private string description;
        private int categoryId;

        public FinanceOperation() {
            this.id = 0;
            this.type = "";
            this.bankAccountId = 0;
            this.amount = 0;
            this.date = DateTime.Now;
            this.description = "";
            this.categoryId = 0;
        }

        public FinanceOperation(int id, string type, int bankAccountId,
                                decimal amount, DateTime date, string description,
                                int categoryId)
        {
            this.id = id;
            this.type = type;
            this.bankAccountId = bankAccountId;
            this.amount = amount;
            this.date = date;
            this.description = description;
            this.categoryId = categoryId;
        }

        public int Id { get => id; set { id = value; } }

        public string Type { get => type; set { type = value; } }

        public int BankAccountId { get => bankAccountId; set { bankAccountId = value; } }

        public decimal Amount { get => amount; set { amount = value; } }

        public DateTime Date { get => date; set { date = value; } }

        public string Description { get => description; set { description = value; } }

        public int CategoryId { get => categoryId; set { categoryId = value; } }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
