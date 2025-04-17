using HW_CPS_HSEZoo_2.UseCases;
using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEZoo_2
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

            services.AddSingleton<EnclosureRepository>();
            services.AddSingleton<FeedingTimeRepository>();

            services.AddSingleton<EnclosureDataService>();
            services.AddSingleton<AnimalDataService>();
            services.AddSingleton<AnimalTransferService>();
            services.AddSingleton<FeedingOrganizationService>();
            services.AddSingleton<ZooStatisticsService>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
