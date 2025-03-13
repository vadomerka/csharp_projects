using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public interface IVisitor
    {
        public void Execute(ICommand command);
    }
}
