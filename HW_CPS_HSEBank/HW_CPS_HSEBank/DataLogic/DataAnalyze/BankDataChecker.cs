using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataAnalyze
{
    public class BankDataChecker
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataChecker(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        public bool CheckAccount(BankAccount op, bool checkId = true)
        {
            if (op == null) return false;
            if (checkId && mgr.GetAccountById(op.Id) != null) return false;

            return true;
        }
        public bool CheckOperation(FinanceOperation op, bool isPostCheck = false, bool checkId = true)
        {
            if (op == null) return false;
            if (checkId && mgr.GetOperationById(op.Id) != null) return false;
            BankAccount? ba = mgr.GetAccountById(op.BankAccountId);
            if (ba == null) return false;
            if (mgr.GetCategoryById(op.CategoryId) == null) return false;
            if (op.Type != "расход" && op.Type != "доход") return false;

            // Проверяем только если проверка идет для операции сразу.
            // Если проверка для не последней операции - проверка недействительна.
            if (!isPostCheck && op.Type == "расход" && ba.Balance < op.Amount) return false;

            return true;
        }
        public bool CheckCategory(Category op, bool checkId = true)
        {
            if (op == null) return false;
            if (checkId && mgr.GetCategoryById(op.Id) != null) return false;
            foreach (var cat in mgr.GetRepository().Categories)
            {
                if (cat.Type == op.Type) { return false; }
            }
            return true;
        }

        public bool CheckAllData()
        {
            if (!br.BankAccounts.All((item) => CheckAccount(item))) { return false; }
            if (!br.FinanceOperations.All((item) => CheckOperation(item, isPostCheck : true))) { return false; }
            if (!br.Categories.All((item) => CheckCategory(item))) { return false; }
            //if (!CheckOperationsValidity()) return false;

            return true;
        }
    }
}
