using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int Id { get => id; }

        public string Type { get => type; }

        public int BankAccountId { get => bankAccountId; }

        public decimal Amount { get => amount; }

        public DateTime Date { get => date; }

        public string Description { get => description; }

        public int CategoryId { get => categoryId; }

        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
