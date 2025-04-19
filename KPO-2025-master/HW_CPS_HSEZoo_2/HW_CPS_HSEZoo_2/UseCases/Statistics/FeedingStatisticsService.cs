using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.Repositories;

namespace HW_CPS_HSEZoo_2.UseCases.Statistics
{
    public class FeedingStatisticsService
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public FeedingStatisticsService() { }
        public FeedingStatisticsService(IServiceProvider sservices)
        {
            services = sservices;
            FeedingOrganizationService.feedEvent += EventHandler;
        }

        public List<string> GetStatistics()
        {
            var rep = services.GetRequiredService<FeedingStatisticsRepository>();
            return rep.GetEntries();
        }

        public static void EventHandler(ISchedule sch)
        {
            var rep = services.GetRequiredService<FeedingStatisticsRepository>();
            rep.AddEntry($"Animal {sch.Animal.Id} was fed at {sch.Time} with {sch.FoodType}.");
        }
    }
}
