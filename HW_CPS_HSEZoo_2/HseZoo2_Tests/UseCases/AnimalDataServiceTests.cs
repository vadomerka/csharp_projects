using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.UseCases
{
    public class AnimalDataServiceTests
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
        public void AnimalDataService_ShouldGetEnclosure() {
            var services = CreateServiceProvider();
            var ser = new AnimalDataService(services);
            var check1 = new Enclosure(1, new List<string>(), new EnclosureSize(0, 0, 0), 2);
            var check2 = new Animal(1);
            check1.AddEntity(check2);
            Assert.Equal(check2, ser.GetAnimal(check1, check2.Id));
        }

        [Fact]
        public void AnimalDataService_ShouldCreate()
        {
            var services = CreateServiceProvider();
            var ser = new AnimalDataService(services);
            var dto = new AnimalDTO();
            dto.Species = "species";
            dto.AnimalTypes = new List<string>() { "animalTypes1", "animalTypes2" };
            dto.Name = "name";
            dto.BirthDate = DateOnly.MaxValue;
            dto.Gender = Gender.Female;
            dto.FavouriteFood = "favouriteFood";
            dto.IsHealthy = false;
            Assert.Equal(new Animal(1, dto.Species, dto.AnimalTypes, dto.Name,
                dto.BirthDate, dto.Gender, dto.FavouriteFood, dto.IsHealthy), ser.Create(dto));
        }
    }
}
