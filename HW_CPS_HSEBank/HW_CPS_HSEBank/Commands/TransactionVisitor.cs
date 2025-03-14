using HW_CPS_HSEBank.Commands.DataCommands;
using HW_CPS_HSEBank.DataLogic;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public class TransactionVisitor : IVisitor
    {
        public void Execute(ICommand command)
        {
            if (!(command is AddCommand<FinanceOperationFactory, FinanceOperation>)) { return; }

            var op = (AddCommand<FinanceOperationFactory, FinanceOperation>)command;
            //op.
            // ??
            //var mb = services.GetRequiredService<BankDataManager>();
            //mb.AddData(bankData);
        }
    }
}
