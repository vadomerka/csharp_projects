using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data.Factories
{
    public class FinanceOperationFactory : IDataFactory<FinanceOperation>
    {
        private int lastId = 0;
        public FinanceOperation Create()
        {
            return new FinanceOperation(++lastId);
        }

        public FinanceOperation Create(FinanceOperation obj)
        {
            return new FinanceOperation(obj.Id, obj.Type, obj.BankAccountId, obj.Amount, 
                obj.Date, obj.Description, obj.CategoryId);
        }
    }
}
