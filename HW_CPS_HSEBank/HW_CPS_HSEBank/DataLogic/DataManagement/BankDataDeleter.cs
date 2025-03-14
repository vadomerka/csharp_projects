using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    public class BankDataDeleter
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataDeleter(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        public void DeleteData<TData>(int id) where TData : class, IBankDataType
        {
            if (typeof(TData) == typeof(BankAccount)) DeleteAccount(id);
            if (typeof(TData) == typeof(FinanceOperation)) DeleteFinanceOperation(id);
            if (typeof(TData) == typeof(Category)) DeleteCategory(id);
        }
        public void DeleteAccount(int id)
        {
            var res = mgr.GetAccountById(id);
            if (res == null) { throw new ArgumentException(); }
            br.BankAccounts.Remove(res);

            for (int i = 0; i < br.FinanceOperations.Count; i++)
            {
                var c = br.FinanceOperations[i];
                if (c.BankAccountId == id)
                {
                    br.FinanceOperations.RemoveAt(i);
                    i--;
                }
            }
        }
        public void DeleteFinanceOperation(int id)
        {
            var res = mgr.GetOperationById(id);
            if (res == null) { throw new ArgumentException(); }
            br.FinanceOperations.Remove(res);
        }
        public void DeleteCategory(int id)
        {
            var res = mgr.GetCategoryById(id);
            if (res == null) { throw new ArgumentException(); }

            br.Categories.Remove(res);

            for (int i = 0; i < br.FinanceOperations.Count; i++) {
                var c = br.FinanceOperations[i];
                if (c.CategoryId == id) {
                    br.FinanceOperations.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
