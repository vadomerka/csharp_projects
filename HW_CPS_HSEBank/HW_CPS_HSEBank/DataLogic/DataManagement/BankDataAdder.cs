using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.Exceptions;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    /// <summary>
    /// Класс для добавления объектов в менеджер.
    /// </summary>
    public class BankDataAdder
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataAdder(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        /// <summary>
        /// Добавление неизвестного объекта.
        /// </summary>
        /// <param name="obj">IBankDataType</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddData(IBankDataType obj)
        {
            if (obj == null) throw new ArgumentNullException();
            if (obj is BankAccount) { AddAccount((BankAccount)obj); }
            if (obj is FinanceOperation) { AddFinanceOperation((FinanceOperation)obj); }
            if (obj is Category) { AddCategory((Category)obj); }
        }
        /// <summary>
        /// Импорт неизвестного объекта. Отличается от добавления измененными проверками.
        /// </summary>
        /// <param name="obj">IBankDataType</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ImportData(IBankDataType obj)
        {
            if (obj == null) throw new ArgumentNullException();
            if (obj is BankAccount) { AddAccount((BankAccount)obj, isImport: true); }
            if (obj is FinanceOperation) { AddFinanceOperation((FinanceOperation)obj, isImport: true); }
            if (obj is Category) { AddCategory((Category)obj, isImport: true); }
        }
        /// <summary>
        /// Добавить аккаунт.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isImport"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddAccount(BankAccount account, bool isImport = false)
        {
            if (!mgr.checker.CheckAccount(account)) throw new ArgumentException();
            account.Balance = account.Balance;
            br.BankAccounts.Add(account);
        }
        /// <summary>
        /// Обертка для AddNewAccount(BankAccount, bool).
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isImport"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddNewAccount(BankAccount account)
        {
            AddNewAccount(account, isImport: false);
        }
        /// <summary>
        /// Добавить аккаунт с пустым балансом.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isImport"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddNewAccount(BankAccount account, bool isImport = false)
        {
            if (!mgr.checker.CheckAccount(account)) throw new ArgumentException();
            account.Balance = 0;
            br.BankAccounts.Add(account);
        }
        /// <summary>
        /// Добавить финансовую операцию.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isImport"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddFinanceOperation(FinanceOperation operaion, bool isImport = false)
        {
            if (!mgr.checker.CheckOperation(operaion, isImportCheck: isImport)) 
                throw new FinanceOperationException("Ошибка при добавлении операции.");
            br.FinanceOperations.Add(operaion);
        }
        /// <summary>
        /// Добавить категорию.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="isImport"></param>
        /// <exception cref="ArgumentException"></exception>
        public void AddCategory(Category category, bool isImport = false)
        {
            if (!mgr.checker.CheckCategory(category)) throw new ArgumentException();
            br.Categories.Add(category);
        }
    }
}
