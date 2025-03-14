using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Exceptions
{ 
    [Serializable]
    public class FinanceOperationException : HseBankException
    {
        public FinanceOperationException()
        { }

        public FinanceOperationException(string message)
            : base(message)
        { }

        public FinanceOperationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
