using HW_CPS_HSEZoo_2;
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
    public class AnimalTransferServiceTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<EnclosureRepository>();
            services.AddSingleton<FeedingScheduleRepository>();

            services.AddSingleton<AnimalTransferService>();
            services.AddSingleton<FeedingOrganizationService>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }

        [Fact]
        public void AnimalTransferService_ShouldMove()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<EnclosureRepository>();
            var ser = new AnimalTransferService(services);
            var types = new List<string>() { "type" };
            var size = new EnclosureSize(0, 1, 3);
            var maxCount = 3;
            var check1 = new Enclosure(1, types, size, maxCount);
            var check2 = new Animal();
            var check3 = new Enclosure(2, types, size, maxCount);

            Assert.Throws<ArgumentException>(() => ser.Move(check1, check2, check3));
            check1.AddEntity(check2);
            Assert.Equal(check1.GetEntity(check2.Id), check2);
            ser.Move(check1, check2, check3);
            Assert.Throws<ArgumentException>(() => check1.GetEntity(check2.Id));
            Assert.Equal(check3.GetEntity(check2.Id), check2);
        }

        [Fact]
        public void AnimalTransferService_ShouldAddRemove()
        {
            var services = CreateServiceProvider();
            var ser = new AnimalTransferService(services);
            var types = new List<string>() { "type" };
            var size = new EnclosureSize(0, 1, 3);
            var maxCount = 3;
            var check1 = new Enclosure(1, types, size, maxCount);
            var check2 = new Animal();
            Assert.False(check1.GetEntities().Any());
            ser.Add(check1, check2);
            Assert.True(check1.GetEntities().Any());
            Assert.Equal(check2, check1.GetEntities()[0]);
            ser.Remove(check1, check2);
            Assert.False(check1.GetEntities().Any());
        }
    }
}
