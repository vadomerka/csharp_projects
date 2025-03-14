using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands.DataCommands
{
    public class ChangeCommand<TData> : IBankOperation
        where TData : class, IBankDataType
    {
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
