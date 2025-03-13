using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic
{
    public class BankDataChecker
    {
        BankDataManager mgr;
        public BankDataChecker(BankDataManager mgr) {
            this.mgr = mgr;
        }

        public bool CheckAccount(BankAccount op)
        {
            if (op == null) return false;
            if (mgr.GetAccountById(op.Id) != null) return false;

            return true;
        }
        public bool CheckOperation(FinanceOperation op)
        {
            if (op == null) return false;
            if (mgr.GetOperationById(op.Id) != null) return false;
            BankAccount? ba = mgr.GetAccountById(op.BankAccountId);
            if (ba == null) return false;
            if (mgr.GetCategoryById(op.CategoryId) == null) return false;

            if (op.Type == "расход" && ba.Balance < op.Amount) return false;

            return true;
        }
        public bool CheckCategory(Category op)
        {
            if (op == null) return false;
            if (mgr.GetCategoryById(op.Id) != null) return false;
            foreach (var cat in mgr.GetRepository().Categories) {
                if (cat.Type == op.Type) { return false; }
            }
            return true;
        }

    }
}
