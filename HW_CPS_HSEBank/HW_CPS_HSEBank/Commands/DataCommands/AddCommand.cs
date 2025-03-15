using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    /// <summary>
    /// Команда для добавления объекта.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public class AddCommand<TFactory, TData> : IBankOperation
        where TFactory : IDataFactory<TData> where TData : IBankDataType
    {
        // Объект для добавления.
        protected readonly TData bankData;
        protected BankDataManager mgr;
        protected IServiceProvider services = CompositionRoot.Services;

        public AddCommand(object[] args)
        {
            mgr = services.GetRequiredService<BankDataManager>();
            var factory = services.GetRequiredService<TFactory>();
            bankData = factory.Create(args);
        }
        public AddCommand(TData other)
        {
            mgr = services.GetRequiredService<BankDataManager>();
            var factory = services.GetRequiredService<TFactory>();
            bankData = factory.Create(other);
        }
        /// <summary>
        /// Конструктор для добавления к конкретному объекту.
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="args"></param>
        public AddCommand(BankDataManager mgr, object[] args)
        {
            this.mgr = mgr;
            var factory = services.GetRequiredService<TFactory>();
            bankData = factory.Create(args);
        }
        /// <summary>
        /// Конструктор для добавления к конкретному объекту.
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="other"></param>
        public AddCommand(BankDataManager mgr, TData other)
        {
            this.mgr = mgr;
            var factory = services.GetRequiredService<TFactory>();
            bankData = factory.Create(other);
        }

        public string Type => "Add";

        public virtual void Execute()
        {
            mgr.AddData(bankData);
        }

        public virtual void VisitorExecute(IVisitor visitor)
        {
            visitor.Execute(this);
        }
    }
}
