using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.Data.Factories;
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
            //services.AddSingleton<BankAccountsRepository>();
            services.AddSingleton<BankDataRepository>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
