using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    /// <summary>
    /// Класс для сортировки переданных данных банка.
    /// </summary>
    public static class BankDataSorter
    {
        public static void SortBankDataById<TData>(ref IEnumerable<TData> data) where TData : IBankDataType
        {
            data = data.OrderBy(o => o.Id);
        }

        public static IEnumerable<TData> SortBankDataByIntProperty<TData>(IEnumerable<TData> data, Func<TData, int> comparer) 
            where TData : IBankDataType
        {
            return data.OrderBy(comparer);
        }

        public static IEnumerable<FinanceOperation> SortFinanceOperationsByDate(IEnumerable<FinanceOperation> data)
        {
            return data.OrderBy(o => o.Date).ToList();
        }
    }
}
