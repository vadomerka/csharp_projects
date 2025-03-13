using HW_CPS_HSEBank.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.DataLogic.DataModels
{
    public class FinanceOperation : IBankDataType, IBankOperation
    {
        private int id;
        private string type;
        private int bankAccountId;
        private decimal amount;
        private DateTime date;
        private string description;
        private int categoryId;

        public FinanceOperation()
        {
            id = 0;
            type = "";
            bankAccountId = 0;
            amount = 0;
            date = DateTime.Now;
            description = "";
            categoryId = 0;
        }

        public FinanceOperation(int id) : this()
        {
            this.id = 0;
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
            var services = CompositionRoot.Services;
            var mgr = services.GetRequiredService<BankDataManager>();
            decimal change = amount;
            if (type == "расход") { change *= -1; }
            BankAccount? bankAccount = mgr.GetAccountById(bankAccountId);
            if (bankAccount == null) throw new InvalidOperationException();
            bankAccount.Balance += change;
        }

        public void VisitorExecute(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
