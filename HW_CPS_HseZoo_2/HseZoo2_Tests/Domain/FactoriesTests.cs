using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.Domain
{
    public class FactoriesTests
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<AnimalFactory>();
            services.AddSingleton<EnclosureFactory>();
            services.AddSingleton<FeedingScheduleFactory>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
        [Fact]
        public void AnimalFactory_ShouldMakeAnimal() {
            var services = CreateServiceProvider();
            var dto = new AnimalDTO();
            var check1 = new Animal(1, dto.Species, dto.AnimalTypes, dto.Name,
                dto.BirthDate, dto.Gender, dto.FavouriteFood, dto.IsHealthy);
            var check2 = services.GetRequiredService<AnimalFactory>().Create(dto);
            Assert.Equal(check1, check2);
        }

        [Fact]
        public void EnclosureFactory_ShouldMakeEnclosure()
        {
            var services = CreateServiceProvider();
            var check1 = new Enclosure(1, new List<string>() { "type1" }, new EnclosureSize(0, 1, 2), 2);
            var check2 = services.GetRequiredService<EnclosureFactory>().Create(new List<string>() { "type1" }, new EnclosureSize(0, 1, 2), 2);
            Assert.Equal(check1, check2);
        }

        [Fact]
        public void FeedingScheduleFactory_ShouldMakeFeedingSchedule()
        {
            var services = CreateServiceProvider();
            var ch = new Animal();
            var check1 = new FeedingSchedule(1, ch, DateTime.MaxValue, "1");
            var check2 = services.GetRequiredService<FeedingScheduleFactory>().Create(ch, DateTime.MaxValue, "1");
            Assert.Equal(check1, check2);
        }
    }
}
