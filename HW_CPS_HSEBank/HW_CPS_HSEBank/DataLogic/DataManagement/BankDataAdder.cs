using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    public class BankDataAdder
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataAdder(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        public void AddData(IBankDataType obj)
        {
            if (obj == null) throw new ArgumentNullException();
            if (obj is BankAccount) { AddAccount((BankAccount)obj); }
            if (obj is FinanceOperation) { AddFinanceOperation((FinanceOperation)obj); }
            if (obj is Category) { AddCategory((Category)obj); }
        }
        public void AddAccount(BankAccount account)
        {
            if (!mgr.checker.CheckAccount(account)) throw new ArgumentException();
            account.Balance = account.Balance;
            br.BankAccounts.Add(account);
        }
        public void AddFinanceOperation(FinanceOperation operaion)
        {
            if (!mgr.checker.CheckOperation(operaion)) throw new FinanceOperationException("Ошибка при добавлении операции.");
            br.FinanceOperations.Add(operaion);
        }
        public void AddCategory(Category category)
        {
            if (!mgr.checker.CheckCategory(category)) throw new ArgumentException();
            br.Categories.Add(category);
        }
    }
}
