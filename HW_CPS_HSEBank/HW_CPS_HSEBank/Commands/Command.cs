using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Commands
{
    public class Command : ICommand
    {

        public Command() { }

        public string Type => throw new NotImplementedException();

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void VisitorExecute(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
