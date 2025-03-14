using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Exceptions
{
    [Serializable]
    public class HseBankException : Exception
    {
        public HseBankException()
        { }

        public HseBankException(string message)
            : base(message)
        { }

        public HseBankException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
