using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    /// <summary>
    /// Команда для изменения объекта.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class ChangeCommand<TData> : IBankOperation
        where TData : class, IBankDataType
    {
        // Список инициализации для изменения объекта.
        protected object[] initList;
        protected BankDataManager mgr;
        protected IServiceProvider services = CompositionRoot.Services;

        public ChangeCommand(object[] args)
        {
            mgr = services.GetRequiredService<BankDataManager>();
            initList = args;
        }

        public string Type => "Delete";

        public virtual void Execute()
        {
            mgr.ChangeData<TData>(initList);
        }

        public virtual void VisitorExecute(IVisitor visitor)
        {
            visitor.Execute(this);
        }
    }
}
