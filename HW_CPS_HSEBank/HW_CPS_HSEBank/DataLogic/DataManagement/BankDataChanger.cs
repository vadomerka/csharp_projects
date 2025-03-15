using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.Exceptions;
namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    /// <summary>
    /// Класс для изменения объектов в менеджере.
    /// </summary>
    public class BankDataChanger
    {
        BankDataManager mgr;
        BankDataRepository br;
        public BankDataChanger(BankDataManager mgr)
        {
            this.mgr = mgr;
            br = mgr.GetRepository();
        }

        /// <summary>
        /// Изменить данные.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="initList"></param>
        public void ChangeData<TData>(object[] initList) where TData : class, IBankDataType
        {
            if (typeof(TData) == typeof(BankAccount)) ChangeAccount(initList);
            if (typeof(TData) == typeof(FinanceOperation)) ChangeFinanceOperation(initList);
            if (typeof(TData) == typeof(Category)) ChangeCategory(initList);
        }

        /// <summary>
        /// Изменить аккаунт.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="initList"></param>
        public void ChangeAccount(object[] initList)
        {
            int id = (int)initList[0];
            var res = mgr.GetAccountById(id);
            if (res == null) { throw new ArgumentException(); }
            string oldname = res.Name;
            res.Name = (string)initList[1];
            // Изменить баланс можно только с добавлением операций.
            //res.Balance = (decimal)initList[2];
            if (!mgr.checker.CheckAccount(res, checkId: false))
            {
                // Возвращение предыдущих данных.
                res.Name = oldname;
                throw new ArgumentException("Ошибка при изменении аккаунта.");
            }
        }

        /// <summary>
        /// Вспомогательная функция для установки данных операции.
        /// </summary>
        /// <param name="op"></param>
        /// <param name="initList"></param>
        private void SetOperationData(ref FinanceOperation op, object[] initList) {
            op.Type = (string)initList[1];
            op.BankAccountId = (int)initList[2];
            op.Amount = (decimal)initList[3];
            op.Date = (DateTime)initList[4];
            op.Description = (string)initList[5];
            op.CategoryId = (int)initList[6];
        }
        /// <summary>
        /// Изменить операцию.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="initList"></param>
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

        /// <summary>
        /// Изменить категорию.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="initList"></param>
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
