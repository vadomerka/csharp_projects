using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Infrastructure;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEZoo_2.Domain.Entities;

namespace HseZoo2_Tests.Interface
{
    public class FeedingScheduleFacadeTests
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
        public void FeedingScheduleFacade_ShouldGetFeedingSchedule()
        {
            var services = CreateServiceProvider();
            FeedingScheduleFacade.Services = services;
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            var check1 = new FeedingSchedule(1);
            check1.Time = DateTime.MaxValue;
            rep.AddEntity(check1);

            Assert.Equal(check1, FeedingScheduleFacade.GetFeedingSchedule(check1.Id));
            rep.RemoveEntity(check1);
        }

        [Fact]
        public void FeedingScheduleFacade_ShouldAddDeleteFeedingSchedule()
        {
            var services = CreateServiceProvider();
            FeedingScheduleFacade.Services = services;

            var rep1 = services.GetRequiredService<EnclosureRepository>();
            var rep2 = services.GetRequiredService<FeedingScheduleRepository>();
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            var check2 = new Animal(1);
            check1.AddEntity(check2);
            rep1.AddEntity(check1);

            var dto = new FeedingScheduleDTO(DateTime.MaxValue, "foodType");
            var check3 = new FeedingSchedule(1, check2, DateTime.MaxValue, "foodType");

            FeedingScheduleFacade.AddFeedingSchedule(check1.Id, check2.Id, dto);
            Assert.Equal(check3, rep2.GetEntity(check3.Id));
        }
    }
}
