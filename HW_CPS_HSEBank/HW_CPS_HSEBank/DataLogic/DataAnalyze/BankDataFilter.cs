using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    /// <summary>
    /// Класс для фильтрации переданных данных банка.
    /// </summary>
    public static class BankDataFilter
    {
        public static IEnumerable<TData> Filter<TData>(IEnumerable<TData> og, Func<TData, bool> filterFunc) 
            where TData : class, IBankDataType {
            return og.Where(filterFunc);
        }

        public static IEnumerable<TData> FilterByType<TData>(IEnumerable<TData> og, string type) where TData : class, IHasType, IBankDataType
        {
            return Filter<TData>(og, (TData fop) => {
                return fop.Type == type;
            });
        }

        public static IEnumerable<FinanceOperation> FilterByAccountId(IEnumerable<FinanceOperation> og, int accountId)
        {
            return Filter<FinanceOperation>(og, (FinanceOperation fop) => {
                return fop.BankAccountId == accountId;
            });
        }

        public static IEnumerable<FinanceOperation> FilterByDate(IEnumerable<FinanceOperation> og, DateTime? d1 = null, DateTime? d2 = null)
        {
            if (d1 == null) d1 = DateTime.MinValue;
            if (d2 == null) d2 = DateTime.MaxValue;
            return Filter(og, (FinanceOperation fop) => {
                return d1 <= fop.Date && fop.Date <= d2; 
            });
        }
    }
}
