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
    public class AddFinanceOperation<TFactory, TData> : AddCommand<TFactory, TData>
        where TFactory : IDataFactory<TData> where TData : FinanceOperation
    {
        public AddFinanceOperation(object[] args) : base(args) { }
        public AddFinanceOperation(TData other) : base(other) { }
        public AddFinanceOperation(BankDataManager mgr, object[] args) : base(mgr, args) { }
        public AddFinanceOperation(BankDataManager mgr, TData other) : base(mgr, other) { }

        public override void Execute()
        {
            //var mb = services.GetRequiredService<BankDataManager>();
            mgr.AddData(bankData);
            bankData.Execute();
        }
    }
}
