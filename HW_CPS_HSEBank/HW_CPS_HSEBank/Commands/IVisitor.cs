using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public interface IVisitor // <TData> where TData : IBankDataType
    {
        public void Execute(ICommand command);
    }
}
