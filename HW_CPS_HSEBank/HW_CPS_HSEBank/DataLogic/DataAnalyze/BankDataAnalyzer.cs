using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    /// <summary>
    /// Класс для функций, которые производят анализ данных банка.
    /// </summary>
    public static class BankDataAnalyzer
    {
        /// <summary>
        /// Пересчет балансов аккаунтов менеджера.
        /// </summary>
        /// <param name="mgr"></param>
        /// <returns></returns>
        public static BankDataManager FinanceOperationsRecount(BankDataManager mgr)
        {
            // Просто создаем нового менеджера. Пересчет встроен в функцию создания.
            var nmgr = new BankDataManager(mgr.GetRepository());
            return nmgr;
        }

        /// <summary>
        /// Метод для подсчета дохода/расхода аккаунта за некоторый период времени.
        /// </summary>
        /// <param name="mgr"></param>
        /// <param name="accId"></param>
        /// <param name="type"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static decimal AccountMoneySum(BankDataManager mgr, int accId, string type, DateTime? d1 = null, DateTime? d2 = null)
        {
            decimal dif = 0;
            var rep = mgr.GetRepository();
            var accFiltered = BankDataFilter.FilterByAccountId(rep.FinanceOperations, accId);
            if (accFiltered.Count() == 0) { throw new ArgumentNullException("Аккаунты с этим id не были найдены."); }

            var dateFiltered = BankDataFilter.FilterByDate(accFiltered, d1, d2);
            if (dateFiltered.Count() == 0) { throw new ArgumentNullException("Операции в этот промежуток времени не были найдены."); }

            var inc = BankDataFilter.FilterByType(dateFiltered, type);
            foreach (var fop in inc) { dif += fop.Amount; }
            return dif;
        }

        /// <summary>
        /// Метод для сортировки по категории.
        /// </summary>
        /// <param name="ops"></param>
        /// <returns></returns>
        public static IEnumerable<FinanceOperation> FinanceOperationsSortByCategory(IEnumerable<FinanceOperation> ops)
        {
            var nops = BankDataSorter.SortFinanceOperationsByDate(ops);
            var res = BankDataSorter.SortBankDataByIntProperty(nops, (FinanceOperation op1) => { 
                return op1.CategoryId;
            });
            
            return res;
        }
    }
}
