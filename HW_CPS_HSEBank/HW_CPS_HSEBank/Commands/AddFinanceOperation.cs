using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.Commands
{
    public class AddFinanceOperation<TFactory, TData> : AddCommand<TFactory, TData>
        where TFactory : IDataFactory<TData> where TData : FinanceOperation
    {
        public AddFinanceOperation(object[] args) : base(args) { }

        public AddFinanceOperation(TData other) : base(other) { }

        public override void Execute()
        {
            var mb = services.GetRequiredService<BankDataManager>();
            mb.AddData(bankData);
            bankData.Execute();
        }
    }
}
