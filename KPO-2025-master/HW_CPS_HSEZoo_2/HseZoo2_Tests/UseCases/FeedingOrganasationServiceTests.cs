using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.UseCases
{
    public class FeedingOrganasationServiceTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<FeedingScheduleRepository>();
            services.AddSingleton<FeedingStatisticsRepository>();

            services.AddSingleton<FeedingScheduleFactory>();

            services.AddSingleton<AnimalDataService>();
            services.AddSingleton<FeedingOrganizationService>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }

        [Fact]
        public void FeedingOrganasationService_ShouldGetSchedule() {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            var ser = new FeedingOrganizationService(services);
            var check = new FeedingSchedule();
            rep.AddEntity(check);
            Assert.Equal(check, ser.GetSchedule(check.Id));
        }

        [Fact]
        public void FeedingOrganasationService_ShouldCreate()
        {
            var services = CreateServiceProvider();
            var ser = new FeedingOrganizationService(services);
            var check = new FeedingSchedule();
            check.Time = DateTime.MaxValue;
            check.FoodType = "foodType";
            var an = new Animal();
            check.Animal = an;
            check.Id = 1;
            var check2 = ser.Create(an, new FeedingScheduleDTO(DateTime.MaxValue, "foodType"));
            Assert.Equal(check, check2);
        }

        [Fact]
        public void FeedingOrganasationService_ShouldAddRemove()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            var ser = new FeedingOrganizationService(services);
            var check = new FeedingSchedule();
            check.Time = DateTime.MaxValue;
            check.FoodType = "foodType";
            var an = new Animal();
            check.Animal = an;
            Assert.False(rep.GetEntities().Any());
            ser.AddSchedule(check);
            Assert.True(rep.GetEntities().Any());
            Assert.Equal(check, rep.GetEntities()[0]);
            ser.RemoveSchedule(check);
            Assert.False(rep.GetEntities().Any());
        }

        [Fact]
        public void FeedingOrganasationService_ShouldNotAddFinished()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<FeedingScheduleRepository>();
            var ser = new FeedingOrganizationService(services);
            var check = new FeedingSchedule();
            check.Time = DateTime.MinValue;
            check.FoodType = "foodType";
            var an = new Animal();
            check.Animal = an;
            Assert.False(rep.GetEntities().Any());
            ser.AddSchedule(check);
            Assert.False(rep.GetEntities().Any());
            Assert.Throws<ArgumentException>(() => ser.RemoveSchedule(check));
        }
    }
}
