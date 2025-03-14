using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    public class DeleteCommand<TData> : IBankOperation
        where TData : class, IBankDataType
    {
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
