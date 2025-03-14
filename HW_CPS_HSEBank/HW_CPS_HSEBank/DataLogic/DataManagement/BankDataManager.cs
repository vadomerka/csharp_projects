using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.DataLogic.DataAnalyze;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    public class BankDataManager
    {
        private IServiceProvider services = CompositionRoot.Services;
        private BankDataRepository br;
        public BankDataChecker checker;
        private BankDataAdder adder;
        private BankDataChanger changer;
        private BankDataDeleter deleter;

        public BankDataManager()
        {
            br = services.GetRequiredService<BankDataRepository>();
            UtilsInit();
        }
        public BankDataManager(BankDataRepository brr)
        {
            br = new BankDataRepository();
            UtilsInit();
            ApplyRepository(brr);
        }
        public static BankDataManager GetNewManager()
        {
            return new BankDataManager(new BankDataRepository());
        }

        private void UtilsInit() {
            checker = new BankDataChecker(this);
            adder = new BankDataAdder(this);
            changer = new BankDataChanger(this);
            deleter = new BankDataDeleter(this);
        }

        public BankDataRepository GetRepository()
        {
            return br;
        }
        public void ApplyRepository(BankDataRepository brr)
        {
            br.BankAccounts.Clear();
            brr.BankAccounts.ForEach(AddData);
            br.Categories.Clear();
            brr.Categories.ForEach(AddData);

            br.FinanceOperations.Clear();
            var nops = BankDataSorter.SortFinanceOperationsByDate(brr.FinanceOperations);
            foreach (FinanceOperation item in nops)
            {
                AddData(item);
                item.Execute(this);
            }
        }
        public void CopyRepository(BankDataRepository other)
        {
            br.BankAccounts.Clear();
            other.BankAccounts.ForEach(AddData);
            br.Categories.Clear();
            other.Categories.ForEach(AddData);

            br.FinanceOperations.Clear();
            other.FinanceOperations.ForEach(AddData);
        }

        public TData? GetDataById<TData>(int id) where TData : class, IBankDataType {
            IBankDataType? res = null;
            if (typeof(TData) == typeof(BankAccount)) res = GetAccountById(id);
            if (typeof(TData) == typeof(FinanceOperation)) res = GetOperationById(id);
            if (typeof(TData) == typeof(Category)) res = GetCategoryById(id);
            return (TData?)res;
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

        public void AddData(IEnumerable<IBankDataType> list)
        {
            foreach (var item in list) { AddData(item); }
        }
        public void AddData(IBankDataType obj) { 
            adder.AddData(obj);
        }
        public void ChangeData<TData>(object[] initList) where TData : class, IBankDataType
        {
            changer.ChangeData<TData>(initList);
        }
        public void DeleteData<TData>(int id) where TData : class, IBankDataType
        {
            deleter.DeleteData<TData>(id);
        }

        public void Save(BankDataRepository other)
        {
            CopyRepository(other);
        }
        public void Save(BankDataManager otherMng)
        {
            Save(otherMng.GetRepository());
        }
    }
}
