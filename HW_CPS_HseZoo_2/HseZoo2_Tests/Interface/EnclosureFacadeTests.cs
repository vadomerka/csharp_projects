using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.Infrastructure;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.Interface
{
    public class EnclosureFacadeTests
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
        public void EnclosureFacade_ShouldGetEnclosure()
        {
            var services = CreateServiceProvider();
            EnclosureFacade.Services = services;
            var rep = services.GetRequiredService<EnclosureRepository>();
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            rep.AddEntity(check1);

            Assert.Equal(check1, EnclosureFacade.GetEnclosure(check1.Id));
        }

        [Fact]
        public void EnclosureFacade_ShouldAddEnclosure()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<EnclosureRepository>();
            EnclosureFacade.Services = services;

            var types = new List<string>() { "123" };
            var size = new EnclosureSize(1, 2, 3);
            var str = 123;
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            rep.AddEntity(check1);

            EnclosureFacade.AddEnclosure(types, size, str);
            Assert.Equal(check1, rep.GetEntity(check1.Id));
        }

        [Fact]
        public void EnclosureFacade_ShouldDeleteAnimal()
        {
            var services = CreateServiceProvider();
            EnclosureFacade.Services = services;
            var rep = services.GetRequiredService<EnclosureRepository>();
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            rep.AddEntity(check1);

            EnclosureFacade.DeleteEnclosure(check1.Id);
            Assert.False(rep.GetEntities().Any());
        }
    }
}
