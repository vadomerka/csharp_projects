using HW_CPS_HSEBank.DataLogic;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataParsers;
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
            //services.AddSingleton<BankAccountsRepository>();
            services.AddSingleton<BankDataRepository>();
            services.AddSingleton<BankDataManager>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
