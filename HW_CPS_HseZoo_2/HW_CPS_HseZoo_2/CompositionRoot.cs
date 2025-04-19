using HW_CPS_HSEZoo_2.UseCases;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.Factories;

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
            services.AddSingleton<FeedingScheduleRepository>();
            services.AddSingleton<FeedingStatisticsRepository>();
            services.AddSingleton<MovingStatisticsRepository>();

            services.AddSingleton<AnimalFactory>();
            services.AddSingleton<EnclosureFactory>();
            services.AddSingleton<FeedingScheduleFactory>();

            services.AddSingleton<EnclosureDataService>();
            services.AddSingleton<AnimalDataService>();
            services.AddSingleton<AnimalTransferService>();
            services.AddSingleton<FeedingOrganizationService>();
            services.AddSingleton<FeedingStatisticsService>();
            services.AddSingleton<MovingStatisticsService>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
    }
}
