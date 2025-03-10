using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    internal interface IBankOperation : ICommand
    {
        public string? ToString();
    }
}
