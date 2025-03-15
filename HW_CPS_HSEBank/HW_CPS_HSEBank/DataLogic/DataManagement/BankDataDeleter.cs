using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    /// <summary>
    /// Класс для удаления объектов из менеджера.
    /// </summary>
    public class BankDataDeleter
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataDeleter(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        /// <summary>
        /// Удалить данные.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="initList"></param>
        public void DeleteData<TData>(int id) where TData : class, IBankDataType
        {
            if (typeof(TData) == typeof(BankAccount)) DeleteAccount(id);
            if (typeof(TData) == typeof(FinanceOperation)) DeleteFinanceOperation(id);
            if (typeof(TData) == typeof(Category)) DeleteCategory(id);
        }


        /// <summary>
        /// Удалить аккаунт.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteAccount(int id)
        {
            var res = mgr.GetAccountById(id);
            if (res == null) { throw new ArgumentException(); }
            br.BankAccounts.Remove(res);

            // Удаление всех связанных операций.
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


        /// <summary>
        /// Удалить операцию.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteFinanceOperation(int id)
        {
            var res = mgr.GetOperationById(id);
            if (res == null) { throw new ArgumentException(); }
            br.FinanceOperations.Remove(res);
        }

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="ArgumentException"></exception>
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
