using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    public class BankDataChanger
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataChanger(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        public void ChangeData<TData>(object[] initList) where TData : class, IBankDataType
        {
            if (typeof(TData) == typeof(BankAccount)) ChangeAccount(initList);
            if (typeof(TData) == typeof(FinanceOperation)) ChangeFinanceOperation(initList);
            if (typeof(TData) == typeof(Category)) ChangeCategory(initList);
        }
        public void ChangeAccount(object[] initList)
        {
            int id = (int)initList[0];
            var res = mgr.GetAccountById(id);
            if (res == null) { throw new ArgumentException(); }
            string oldname = res.Name;
            res.Name = (string)initList[1];
            //res.Balance = (decimal)initList[2];
            if (!mgr.checker.CheckAccount(res, checkId: false))
            {
                res.Name = oldname;
                throw new ArgumentException("Ошибка при изменении аккаунта.");
            }
        }

        private void SetOperationData(ref FinanceOperation op, object[] initList) {
            op.Type = (string)initList[1];
            op.BankAccountId = (int)initList[2];
            op.Amount = (decimal)initList[3];
            op.Date = (DateTime)initList[4];
            op.Description = (string)initList[5];
            op.CategoryId = (int)initList[6];
        }
        public void ChangeFinanceOperation(object[] initList)
        {
            int id = (int)initList[0];
            var res = mgr.GetOperationById(id);
            if (res == null) { throw new ArgumentException(); }
            var oldList = new object[] { res.Id, res.Type, res.BankAccountId, res.Amount, res.Date, res.Description, res.CategoryId };
            SetOperationData(ref res, initList);
            if (!mgr.checker.CheckOperation(res, checkId: false))
            {
                SetOperationData(ref res, oldList);
                throw new FinanceOperationException("Ошибка при изменении операции.");
            }
        }
        public void ChangeCategory(object[] initList)
        {
            int id = (int)initList[0];
            var res = mgr.GetCategoryById(id);
            if (res == null) { throw new ArgumentException(); }
            string oldType = res.Type;
            res.Type = (string)initList[1];
            if (!mgr.checker.CheckCategory(res, checkId: false))
            {
                res.Type = oldType;
                throw new ArgumentException("Ошибка при изменении категории.");
            }
        }
    }
}
