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
        public static List<BankAccount> FinanceOperationsRecount(List<BankAccount> acs, List<FinanceOperation> ops)
        {
            //var service = CompositionRoot.Services;
            //var save = service.GetRequiredService<BankDataManager>(); // Ссылка для работы
            //var backup = new BankDataManager();                       // Ссылка для бекапа сохраненных данных
            //backup.CopyRepository(save.GetRepository());
            //save.CopyRepository(mgr.GetRepository());                 // Открываем репозиторий для работы.

            //var rep = save.GetRepository();
            //var ops = rep.FinanceOperations;
            //var sops = BankDataSorter.SortFinanceOperationsByDate(ops);
            //var zeroAccs = rep.BankAccounts;
            //foreach (var acc in zeroAccs) { 
            //    acc.Balance = 0;
            //}
            //foreach (var op in sops) {
            //    op.Execute();
            //}

            //mgr.CopyRepository(save.GetRepository());
            //save.CopyRepository(backup.GetRepository());

            return acs;
        }
    }
}
