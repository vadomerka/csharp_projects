using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    public static class BankDataFilter
    {
        public static IEnumerable<TData> Filter<TData>(IEnumerable<TData> og, Func<TData, bool> filterFunc) 
            where TData : class, IBankDataType {
            return og.Where(filterFunc);
        }

        public static IEnumerable<TData> FilterByType<TData>(IEnumerable<TData> og, string type) where TData : class, IHasType
        {
            return og.Where((TData fop) => {
                return fop.Type == type;
            });
        }

        public static IEnumerable<FinanceOperation> FilterByAccountId(IEnumerable<FinanceOperation> og, int accountId)
        {
            return og.Where((FinanceOperation fop) => {
                return fop.BankAccountId == accountId;
            });
        }
        public static IEnumerable<FinanceOperation> FilterByDate(IEnumerable<FinanceOperation> og, DateTime? d1 = null, DateTime? d2 = null)
        {
            if (d1 == null) d1 = DateTime.MinValue;
            if (d2 == null) d2 = DateTime.MaxValue;
            return og.Where((FinanceOperation fop) => {
                return d1 <= fop.Date && fop.Date <= d2; 
            });
        }
    }
}
