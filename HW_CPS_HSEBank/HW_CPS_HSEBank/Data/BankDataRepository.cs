namespace HW_CPS_HSEBank.Data
{
    public class BankDataRepository
    {
        private List<BankAccount> _accounts = new List<BankAccount>();
        private List<FinanceOperation> _operations = new List<FinanceOperation>();
        private List<Category> _categories = new List<Category>();
        //private static IServiceProvider services = CompositionRoot.Services;
        // BankAccountsRepository accounts;

        public List<BankAccount> BankAccounts { 
            get { return _accounts; } 
            set { _accounts = value; } }
        public List<FinanceOperation> FinanceOperations { get { return _operations; } set { _operations = value; } }
        public List<Category> Categories { get { return _categories; } set { _categories = value; } }


        public void AddAccount(BankAccount account)
        {
            _accounts.Add(account);
        }
        public void AddFinanceOperation(FinanceOperation operaion)
        {
            _operations.Add(operaion);
        }
        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void Swap(BankDataRepository other) {
            _accounts = other._accounts;
            _operations = other._operations;
            _categories = other._categories;
        }
    }
}
