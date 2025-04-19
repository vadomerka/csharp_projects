using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Infrastructure;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.Interface
{
    public class ZooStatisticsFacadeTests
    {
        private IServiceProvider CreateServiceProvider()
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

        [Fact]
        public void ZooStatisticsFacade_ShouldGetFeedingStats()
        {
            var services = CreateServiceProvider();
            ZooStatisticsFacade.Services = services;
            ZooStatisticsFacade.StatsInit();
            var rep = services.GetRequiredService<FeedingStatisticsRepository>();
            string check1 = "123";
            rep.AddEntry(check1);
            var check2 = ZooStatisticsFacade.GetFeedingStats();
            Assert.Equal(check1, check2[0]);
        }

        [Fact]
        public void ZooStatisticsFacade_ShouldGetMovingStats()
        {
            var services = CreateServiceProvider();
            ZooStatisticsFacade.Services = services;
            ZooStatisticsFacade.StatsInit();
            var rep = services.GetRequiredService<MovingStatisticsRepository>();
            string check1 = "123";
            rep.AddEntry(check1);
            var check2 = ZooStatisticsFacade.GetMovingStats();
            Assert.Equal(check1, check2[0]);
        }
    }
}
