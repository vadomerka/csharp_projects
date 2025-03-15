using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    /// <summary>
    /// Класс команды добавления финансовой операции. 
    /// Отличается от простого добавления выполнением операции при выполнении команды.
    /// </summary>
    /// <typeparam name="TFactory"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class AddFinanceOperation<TFactory, TData> : AddCommand<TFactory, TData>
        where TFactory : IDataFactory<TData> where TData : FinanceOperation
    {
        public AddFinanceOperation(object[] args) : base(args) { }
        public AddFinanceOperation(TData other) : base(other) { }
        public AddFinanceOperation(BankDataManager mgr, object[] args) : base(mgr, args) { }
        public AddFinanceOperation(BankDataManager mgr, TData other) : base(mgr, other) { }

        public override void Execute()
        {
            mgr.AddData(bankData);
            bankData.Execute(mgr);
        }
    }
}
