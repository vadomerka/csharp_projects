using HW_CPS_HSEBank.DataLogic.DataAnalyze;
using HW_CPS_HSEBank.DataLogic.DataModels;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.DataLogic.DataManagement
{
    /// <summary>
    /// Класс для управления данными.
    /// </summary>
    public class BankDataManager
    {
        private IServiceProvider services = CompositionRoot.Services;
        // Внутренние сервисы. Не выносятся в DI потому что привязаны к конкретному объекту
        private BankDataRepository br;
        public BankDataChecker checker;
        private BankDataAdder adder;
        private BankDataChanger changer;
        private BankDataDeleter deleter;

        /// <summary>
        /// Менеджер текущего репозитория.
        /// </summary>
        public BankDataManager()
        {
            br = services.GetRequiredService<BankDataRepository>();
            UtilsInit();
        }
        /// <summary>
        /// Создание менеджера на основе другого репозитория.
        /// </summary>
        /// <param name="brr"></param>
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
        /// <summary>
        /// Применение репозитория. Операции совершаются во время добавления.
        /// </summary>
        /// <param name="brr"></param>
        public void ApplyRepository(BankDataRepository brr)
        {
            // Создание списка нулевых аккаунтов
            br.BankAccounts.Clear();
            brr.BankAccounts.ForEach(adder.AddNewAccount);
            br.Categories.Clear();
            brr.Categories.ForEach(AddData);

            br.FinanceOperations.Clear();
            // Сортировка для верного применения операций
            var nops = BankDataSorter.SortFinanceOperationsByDate(brr.FinanceOperations);
            foreach (FinanceOperation item in nops)
            {
                // Добавление.
                AddData(item);
                // Применение.
                item.Execute(this);
            }
        }
        /// <summary>
        /// Копирование репозитория. Никаких дополнительных действий.
        /// </summary>
        /// <param name="other"></param>
        private void CopyRepository(BankDataRepository other)
        {
            br.BankAccounts.Clear();
            other.BankAccounts.ForEach(AddData);
            br.Categories.Clear();
            other.Categories.ForEach(AddData);
            // Операции должны идти после добавления категорий и аккаунтов, чтобы проверка не бунтовала.
            br.FinanceOperations.Clear();
            other.FinanceOperations.ForEach(AddData);
        }

        // Получение данных по id.
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

        /// <summary>
        /// Импорт списка данных.
        /// </summary>
        /// <param name="list"></param>
        public void ImportData(IEnumerable<IBankDataType> list)
        {
            foreach (var item in list) { ImportData(item); }
        }
        /// <summary>
        /// Импорт объекта
        /// </summary>
        /// <param name="obj"></param>
        public void ImportData(IBankDataType obj)
        {
            adder.ImportData(obj);
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="obj"></param>
        public void AddData(IBankDataType obj) { 
            adder.AddData(obj);
        }
        /// <summary>
        /// Изменение объекта
        /// </summary>
        /// <param name="obj"></param>
        public void ChangeData<TData>(object[] initList) where TData : class, IBankDataType
        {
            changer.ChangeData<TData>(initList);
        }
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteData<TData>(int id) where TData : class, IBankDataType
        {
            deleter.DeleteData<TData>(id);
        }

        /// <summary>
        /// Скопировать репозиторий.
        /// </summary>
        /// <param name="other"></param>
        public void Save(BankDataRepository other)
        {
            CopyRepository(other);
        }
        /// <summary>
        /// Скопировать репозиторий менеджера.
        /// </summary>
        /// <param name="other"></param>
        public void Save(BankDataManager otherMng)
        {
            Save(otherMng.GetRepository());
        }
    }
}
