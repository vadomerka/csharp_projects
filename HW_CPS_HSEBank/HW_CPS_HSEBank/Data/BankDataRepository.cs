namespace HW_CPS_HSEBank.Data
{
    public class BankDataRepository
    {
        private List<BankAccount> _accounts = new List<BankAccount>();
        private List<FinanceOperation> _operations = new List<FinanceOperation>();
        // todo??
        //private List<FinanceOperation> _unsaved_operations = new List<FinanceOperation>();
        private List<Category> _categories = new List<Category>();

        public List<BankAccount> BankAccounts {
            get { return _accounts; } 
            set { _accounts = value; } }
        public List<FinanceOperation> FinanceOperations { get { return _operations; } set { _operations = value; } }
        public List<Category> Categories { get { return _categories; } set { _categories = value; } }

        public void AddData(IBankDataType obj) {
            if (obj == null) throw new ArgumentNullException();
            if (obj is BankAccount) { AddAccount((BankAccount)obj); }
            if (obj is FinanceOperation) { AddFinanceOperation((FinanceOperation)obj); }
            if (obj is Category) { AddCategory((Category)obj); }
        }

        private bool ContainsAccountById(int id) {
            for (int i = 0; i < _accounts.Count; i++)
            {
                if (_accounts[i].Id == id) {
                    return true;
                }
            }
            return false;
        }
        private bool ContainsCategoryById(int id)
        {
            for (int i = 0; i < _categories.Count; i++)
            {
                if (_categories[i].Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckOperation(FinanceOperation op) {
            if (op == null) return false;
            if (!ContainsAccountById(op.BankAccountId)) return false;
            if (!ContainsCategoryById(op.CategoryId)) return false;
            //if (op.Amount) return false;
            return true;
        }

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
