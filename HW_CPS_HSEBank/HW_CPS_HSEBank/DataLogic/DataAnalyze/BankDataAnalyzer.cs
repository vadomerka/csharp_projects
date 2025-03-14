using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    public static class BankDataAnalyzer
    {
        public static BankDataManager FinanceOperationsRecount(BankDataManager mgr)
        {
            var nmgr = new BankDataManager(mgr.GetRepository());
            return nmgr;
        }

        public static decimal AccountMoneySum(BankDataManager mgr, int accId, string type, DateTime? d1 = null, DateTime? d2 = null)
        {
            decimal dif = 0;
            var rep = mgr.GetRepository();
            var accFiltered = BankDataFilter.FilterByAccountId(rep.FinanceOperations, accId);
            if (accFiltered.Count() == 0) { throw new ArgumentNullException("Аккаунты с этим id не были найдены."); }
            var dateFiltered = BankDataFilter.FilterByDate(accFiltered, d1, d2);
            if (dateFiltered.Count() == 0) { throw new ArgumentNullException("Операции в этот промежуток времени не были найдены."); }
            var inc = BankDataFilter.FilterByType(dateFiltered, type);
            //int koef = type == "доход" ? 1 : -1;
            foreach (var fop in inc) { dif += fop.Amount; }
            return dif;
        }

        //public static decimal DifferenceIncomeExpence(BankDataManager mgr, int accId, DateTime? d1 = null, DateTime? d2 = null) {
        //    decimal dif = 0;
        //    var rep = mgr.GetRepository();
        //    var accFiltered = BankDataFilter.FilterByAccountId(rep.FinanceOperations, accId);
        //    var dateFiltered = BankDataFilter.FilterByDate(accFiltered, d1, d2);
        //    var inc = BankDataFilter.FilterByType(dateFiltered, "доход");
        //    var exp = BankDataFilter.FilterByType(dateFiltered, "расход");
        //    foreach ( var fop in inc ) { dif += fop.Amount; }
        //    foreach (var fop in exp) { dif += fop.Amount; }
        //    return dif;
        //}
    }
}
