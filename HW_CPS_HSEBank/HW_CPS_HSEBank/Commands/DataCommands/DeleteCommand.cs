using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    /// <summary>
    /// Команда для удаления объекта.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class DeleteCommand<TData> : IBankOperation
        where TData : class, IBankDataType
    {
        // id объекта для удаления.
        protected readonly int id;
        protected BankDataManager mgr;
        protected IServiceProvider services = CompositionRoot.Services;

        public DeleteCommand(object[] args)
        {
            mgr = services.GetRequiredService<BankDataManager>();
            id = (int)args[0];
        }
        public DeleteCommand(TData other)
        {
            mgr = services.GetRequiredService<BankDataManager>();
            id = other.Id;
        }

        public string Type => "Delete";

        public virtual void Execute()
        {
            mgr.DeleteData<TData>(id);
        }

        public virtual void VisitorExecute(IVisitor visitor)
        {
            visitor.Execute(this);
        }
    }
}
