using HW_CPS_HSEZoo_2;
using HW_CPS_HSEZoo_2.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace HseZoo2_Tests
{
    public class CompositionRootTests
    {
        private readonly Mock<EnclosureRepository> _class1;
        private readonly Mock<FeedingScheduleRepository> _class2;
        private readonly Mock<FeedingStatisticsRepository> _class3;

        public CompositionRootTests()
        {
            _class1 = new Mock<EnclosureRepository>();
            _class2 = new Mock<FeedingScheduleRepository>();
            _class3 = new Mock<FeedingStatisticsRepository>();
        }

        [Fact]
        public void CompositionRootServices_ShouldReturnSameInstance()
        {
            var services1 = CompositionRoot.Services;
            var services2 = CompositionRoot.Services;

            Assert.Same(services1, services2);
        }

        [Fact]
        public void CompositionRootServices_ShouldProvideRegistratedServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton(_class1.Object);
            services.AddSingleton(_class2.Object);
            services.AddSingleton(_class3.Object);
            var serviceProvider = services.BuildServiceProvider();

            Assert.NotNull(serviceProvider.GetService<EnclosureRepository>());
            Assert.NotNull(serviceProvider.GetService<FeedingScheduleRepository>());
            Assert.NotNull(serviceProvider.GetService<FeedingStatisticsRepository>());
        }
    }
}