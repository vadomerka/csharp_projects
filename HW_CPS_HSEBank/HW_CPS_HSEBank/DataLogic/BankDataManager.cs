using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.DataLogic
{
    public class BankDataManager
    {
        IServiceProvider services = CompositionRoot.Services;
        BankDataRepository br;
        BankDataChecker checker;

        public BankDataManager() {
            br = services.GetRequiredService<BankDataRepository>();
            checker = new BankDataChecker(this);
        }
        public BankDataManager(BankDataRepository br)
        {
            this.br = br;
            checker = new BankDataChecker(this);
        }
        public static BankDataManager GetNewManager() {
            return new BankDataManager(new BankDataRepository());
        }

        public BankDataRepository GetRepository()
        {
            return br;
        }
        public BankAccount? GetAccountById(int id)
        {
            for (int i = 0; i < br.BankAccounts.Count; i++)
            {
                if (br.BankAccounts[i].Id == id)
                {
                    return br.BankAccounts[i];
                }
            }
            return null;
        }
        public FinanceOperation? GetOperationById(int id)
        {
            for (int i = 0; i < br.FinanceOperations.Count; i++)
            {
                if (br.FinanceOperations[i].Id == id)
                {
                    return br.FinanceOperations[i];
                }
            }
            return null;
        }
        public Category? GetCategoryById(int id)
        {
            for (int i = 0; i < br.Categories.Count; i++)
            {
                if (br.Categories[i].Id == id)
                {
                    return br.Categories[i];
                }
            }
            return null;
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
            if (!checker.CheckAccount(account)) throw new ArgumentException();
            br.BankAccounts.Add(account);
        }
        public void AddFinanceOperation(FinanceOperation operaion)
        {
            if (!checker.CheckOperation(operaion)) throw new ArgumentException();
            br.FinanceOperations.Add(operaion);
        }
        public void AddCategory(Category category)
        {
            if (!checker.CheckCategory(category)) throw new ArgumentException();
            br.Categories.Add(category);
        }

        public void Save(BankDataRepository other)
        {
            br.BankAccounts = other.BankAccounts;
            br.FinanceOperations = other.FinanceOperations;
            br.Categories = other.Categories;
        }
        public void Save(BankDataManager otherMng)
        {
            Save(otherMng.GetRepository());
        }
    }
}
