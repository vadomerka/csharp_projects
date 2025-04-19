using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.UseCases.Statistics;
using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Infrastructure;

namespace HseZoo2_Tests.Interface
{
    public class AnimalFacadeTests
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
        public void AnimalFacade_ShouldGetAnimal()
        {
            var services = CreateServiceProvider();
            AnimalFacade.Services = services;
            var rep = services.GetRequiredService<EnclosureRepository>();
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            var check2 = new Animal(1);
            check1.AddEntity(check2);
            rep.AddEntity(check1);

            Assert.Equal(check2, AnimalFacade.GetAnimal(check1.Id, check2.Id));
        }

        [Fact]
        public void AnimalFacade_ShouldAddAnimal()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<EnclosureRepository>();
            AnimalFacade.Services = services;

            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            rep.AddEntity(check1);
            var dto = new AnimalDTO("species", new List<string>() { "animalTypes1", "animalTypes2" },
                "name", DateOnly.MaxValue, Gender.Female, "favouriteFood", false);
            var check2 = new Animal(1, dto.Species, dto.AnimalTypes, dto.Name,
                dto.BirthDate, dto.Gender, dto.FavouriteFood, dto.IsHealthy);

            AnimalFacade.AddAnimal(check1.Id, dto);
            Assert.Equal(check2, check1.GetEntity(check2.Id));
        }

        [Fact]
        public void AnimalFacade_ShouldDeleteAnimal()
        {
            var services = CreateServiceProvider();
            AnimalFacade.Services = services;
            var rep = services.GetRequiredService<EnclosureRepository>();
            var check1 = new Enclosure(1);
            check1.MaxCount = 2;
            var check2 = new Animal(1);
            check1.AddEntity(check2);
            rep.AddEntity(check1);

            AnimalFacade.DeleteAnimal(check1.Id, check2.Id);
            Assert.False(check1.GetEntities().Any());
        }

        [Fact]
        public void AnimalFacade_ShouldMoveAnimal()
        {
            var services = CreateServiceProvider();
            AnimalFacade.Services = services;
            var rep = services.GetRequiredService<EnclosureRepository>();
            var check1 = new Enclosure(1);
            var check3 = new Enclosure(2);
            check1.MaxCount = 2;
            check3.MaxCount = 2;
            var check2 = new Animal(1);
            check1.AddEntity(check2);
            rep.AddEntity(check1);
            rep.AddEntity(check3);

            AnimalFacade.MoveAnimal(check1.Id, check2.Id, check3.Id);
            Assert.False(check1.GetEntities().Any());
            Assert.Equal(check2, check3.GetEntities()[0]);
        }
    }
}
