using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public interface ICommand
    {
        string Type{ get; }
        void Execute();
    }
}
