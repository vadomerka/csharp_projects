using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    /// <summary>
    /// Класс который хранит ссылки на списки данных.
    /// </summary>
    public class BankDataRepository
    {
        private List<BankAccount> _accounts = new List<BankAccount>();
        private List<FinanceOperation> _operations = new List<FinanceOperation>();
        private List<Category> _categories = new List<Category>();

        public List<BankAccount> BankAccounts { get { return _accounts; } set { _accounts = value; } }
        public List<FinanceOperation> FinanceOperations { get { return _operations; } set { _operations = value; } }
        public List<Category> Categories { get { return _categories; } set { _categories = value; } }
    }
}
