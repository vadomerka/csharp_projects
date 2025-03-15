using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.DataLogic.DataModels
{
    /// <summary>
    /// Класс финанс операций
    /// </summary>
    public class FinanceOperation : IBankDataType, IBankOperation, IHasType
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
            this.id = id;
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

        /// <summary>
        /// Метод для совершения финансовой операции. Добавление/Убавление денег со счета аккаунта.
        /// </summary>
        /// <param name="mgr"></param>
        /// <exception cref="FinanceOperationException"></exception>
        public void Execute(BankDataManager? mgr)
        {
            // Получение менеджера, которому нужно менять данные.
            var services = CompositionRoot.Services;
            if (mgr == null) { mgr = services.GetRequiredService<BankDataManager>(); }
            decimal change = amount;
            if (type == "расход") { change *= -1; }
            // Поиск пользователя.
            BankAccount? bankAccount = mgr.GetAccountById(bankAccountId);
            if (bankAccount == null) throw new FinanceOperationException($"Аккаунт {bankAccountId} не найден!");
            // Совершение операции.
            bankAccount.Balance += change;
        }

        public void Execute()
        {
            Execute(null);
        }

        public void VisitorExecute(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
