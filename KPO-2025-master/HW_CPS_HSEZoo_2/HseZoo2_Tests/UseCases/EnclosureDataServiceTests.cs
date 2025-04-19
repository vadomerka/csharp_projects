using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Factories;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HseZoo2_Tests.UseCases
{
    public class EnclosureDataServiceTests
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<EnclosureRepository>();
            services.AddSingleton<EnclosureFactory>();
            services.AddSingleton<EnclosureDataService>();
            services.AddSingleton<AnimalTransferService>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }
        [Fact]
        public void EnclosureDataService_ShouldGetEnclosure() {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<EnclosureRepository>();
            var ser = new EnclosureDataService(services);
            var check = new Enclosure();
            rep.AddEntity(check);
            Assert.Equal(check, ser.GetEnclosure(check.Id));
        }

        [Fact]
        public void EnclosureDataService_ShouldCreate()
        {
            var services = CreateServiceProvider();
            var ser = new EnclosureDataService(services);
            var types = new List<string>() { "type" };
            var size = new EnclosureSize(0, 1, 3);
            var maxCount = 3;
            var check = new Enclosure(1, types, size, maxCount);
            var check2 = ser.Create(types, size, maxCount);
        }

        [Fact]
        public void EnclosureDataService_ShouldAddRemove()
        {
            var services = CreateServiceProvider();
            var rep = services.GetRequiredService<EnclosureRepository>();
            var ser = new EnclosureDataService(services);
            var types = new List<string>() { "type" };
            var size = new EnclosureSize(0, 1, 3);
            var maxCount = 3;
            var check = new Enclosure(1, types, size, maxCount);
            Assert.False(rep.GetEntities().Any());
            ser.AddEnclosure(check);
            Assert.True(rep.GetEntities().Any());
            Assert.Equal(check, rep.GetEntities()[0]);
            ser.RemoveEnclosure(check);
            Assert.False(rep.GetEntities().Any());
        }
    }
}
