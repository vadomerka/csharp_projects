using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HseZoo2_Tests.Domain
{
    public class RepositoriesTests
    {
        private static IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<EnclosureRepository>();
            services.AddSingleton<FeedingScheduleRepository>();
            services.AddSingleton<FeedingStatisticsRepository>();
            services.AddSingleton<MovingStatisticsRepository>();

            return services.BuildServiceProvider(); // строим контейнер зависимостей
        }

        [Fact]
        public void Repositories_ShouldBeInServices()
        {
            var services = CreateServiceProvider();
            var rep1 = services.GetService<EnclosureRepository>();
            var rep2 = services.GetService<FeedingScheduleRepository>();
            var rep3 = services.GetService<FeedingStatisticsRepository>();
            var rep4 = services.GetService<MovingStatisticsRepository>();

            Assert.NotNull(rep1);
            Assert.NotNull(rep2);
            Assert.NotNull(rep3);
            Assert.NotNull(rep4);
        }

        [Fact]
        public void EnclosureRepository_ShouldExtendInterface()
        {
            var services = CreateServiceProvider();
            var rep = services.GetService<EnclosureRepository>();

            Assert.NotNull(rep);
            Assert.NotNull(rep.GetEntities());
            Assert.False(rep.GetEntities().Any());
            var ent = new Enclosure();
            rep.AddEntity(ent);
            Assert.True(rep.GetEntities().Any());
            Assert.Equal(ent, rep.GetEntity(ent.Id));
            rep.RemoveEntity(ent);
            Assert.False(rep.GetEntities().Any());
            Assert.Throws<ArgumentException>(() => rep.GetEntity(ent.Id));
        }

        [Fact]
        public void FeedingScheduleRepository_ShouldExtendInterface()
        {
            var services = CreateServiceProvider();
            var rep = services.GetService<FeedingScheduleRepository>();

            Assert.NotNull(rep);
            Assert.NotNull(rep.GetEntities());
            Assert.False(rep.GetEntities().Any());
            var ent = new FeedingSchedule(0, new Animal(), DateTime.MaxValue, "");
            rep.AddEntity(ent);
            Assert.True(rep.GetEntities().Any());
            Assert.Equal(ent, rep.GetEntity(ent.Id));
            rep.RemoveEntity(ent);
            Assert.False(rep.GetEntities().Any());
            Assert.Throws<ArgumentException>(() => rep.GetEntity(ent.Id));
        }

        [Theory]
        [MemberData(nameof(Foo))]
        public void IStatisticsRepository_ShouldExtendInterface(string check, IStatisticsRepository rep)
        {
            Assert.NotNull(rep);
            Assert.NotNull(rep.GetEntries());
            Assert.False(rep.GetEntries().Any());
            rep.AddEntry(check);
            Assert.True(rep.GetEntries().Any());
            Assert.Equal(rep.GetEntries()[0], check);
        }

        public static IEnumerable<object[]> Foo()
        {
            var services = CreateServiceProvider();
            yield return new object[] { "123", services.GetRequiredService<FeedingStatisticsRepository>() };
            yield return new object[] { "123", services.GetRequiredService<MovingStatisticsRepository>() };
        }
    }
}
