using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    public class AddCommand<TFactory, TData> : IBankOperation
        where TFactory : IDataFactory<TData> where TData : IBankDataType
    {
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
        public AddCommand(BankDataManager mgr, object[] args)
        {
            this.mgr = mgr;
            var factory = services.GetRequiredService<TFactory>();
            bankData = factory.Create(args);
        }
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
