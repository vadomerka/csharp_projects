using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    public static class BankDataSorter
    {
        public static void SortBankDataById<TData>(ref IEnumerable<TData> data) where TData : IBankDataType
        {
            data = data.OrderBy(o => o.Id);
        }

        public static List<FinanceOperation> SortFinanceOperationsByDate(List<FinanceOperation> data)
        {
            return data.OrderBy(o => o.Date).ToList();
        }
    }
}
