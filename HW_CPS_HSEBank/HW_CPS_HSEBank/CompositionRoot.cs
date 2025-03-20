using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.Statistics;
using HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank
{
    /// <summary>
    /// Класс содержащий все зависимости.
    /// </summary>
    public static class CompositionRoot

    {
        private static IServiceProvider? _services;

        public static IServiceProvider Services => _services ??= CreateServiceProvider();

        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            // Factories.
            services.AddSingleton<AccountFactory>();
            services.AddSingleton<FinanceOperationFactory>();
            services.AddSingleton<CategoryFactory>();

            // Parsers.
            services.AddSingleton<JsonDataParser>();
            services.AddSingleton<YamlDataParser>();
            services.AddSingleton<CsvDataParser>();

            // UI.
            services.AddSingleton<AccountsUI>();
            services.AddSingleton<FinanceOperationsUI>();
            services.AddSingleton<CategoriesUI>();

            // Data.
            services.AddSingleton<BankDataRepository>();
            services.AddSingleton<BankDataManager>();

            // Statistics.
            services.AddSingleton<MenuStatistics>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
