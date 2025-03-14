using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank
{

    public static class CompositionRoot

    {
        private static IServiceProvider? _services;

        public static IServiceProvider Services => _services ??= CreateServiceProvider();

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<AccountFactory>();
            services.AddSingleton<FinanceOperationFactory>();
            services.AddSingleton<CategoryFactory>();

            services.AddSingleton<JsonDataParser>();
            services.AddSingleton<YamlDataParser>();
            services.AddSingleton<CsvDataParser>();

            services.AddSingleton<AccountsUI>();
            services.AddSingleton<FinanceOperationsUI>();
            services.AddSingleton<CategoriesUI>();

            services.AddSingleton<BankDataRepository>();
            services.AddSingleton<BankDataManager>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
