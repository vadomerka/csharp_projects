using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public class AddCommand<TFactory, TData> : IBankOperation 
        where TFactory : IDataFactory<TData> where TData : IBankDataType
    {
        protected readonly TData bankData;
        protected IServiceProvider services = CompositionRoot.Services;

        public AddCommand(object[] args)
        {
            var accountFactory = services.GetRequiredService<TFactory>();
            bankData = accountFactory.Create(args);
        }
        public AddCommand(TData other)
        {
            var accountFactory = services.GetRequiredService<TFactory>();
            bankData = accountFactory.Create(other);
        }

        public string Type => "Add";

        public virtual void Execute()
        {
            var mb = services.GetRequiredService<BankDataManager>();
            mb.AddData(bankData);
        }

        public virtual void VisitorExecute(IVisitor visitor)
        {
            visitor.Execute(this);
        }
    }
}
