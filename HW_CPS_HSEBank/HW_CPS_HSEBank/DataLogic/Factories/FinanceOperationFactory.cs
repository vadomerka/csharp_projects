using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    /// <summary>
    /// Фабрика для создания финансовых операций
    /// </summary>
    public class FinanceOperationFactory : IDataFactory<FinanceOperation>
    {
        private int lastId = 0;
        public FinanceOperation Create()
        {
            return new FinanceOperation(++lastId);
        }

        public FinanceOperation Create(string type, int bankAccountId,
                                decimal amount, DateTime date, string description,
                                int categoryId)
        {
            return new FinanceOperation(++lastId, type, bankAccountId, amount, date, description, categoryId);
        }

        public FinanceOperation Create(object[] args)
        {
            if (args.Length != 6) throw new ArgumentException();
            return new FinanceOperation(++lastId, (string)args[0], (int)args[1], (decimal)args[2],
                                                  (DateTime)args[3], (string)args[4], (int)args[5]);
        }

        public FinanceOperation Create(FinanceOperation obj)
        {
            return new FinanceOperation(obj.Id, obj.Type, obj.BankAccountId, obj.Amount, 
                obj.Date, obj.Description, obj.CategoryId);
        }
    }
}
