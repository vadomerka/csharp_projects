using HW_CPS_HSEZoo_2.UseCases.Statistics;

namespace HW_CPS_HSEZoo_2.Infrastructure
{
    public class ZooStatisticsFacade
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static IServiceProvider Services { get { return services; } set { services = value; } }

        public static void StatsInit() {
            var rep1 = services.GetRequiredService<FeedingStatisticsService>();
            var rep2 = services.GetRequiredService<MovingStatisticsService>();
        }

        public static List<string> GetFeedingStats()
        {
            var rep = services.GetRequiredService<FeedingStatisticsService>();
            return rep.GetStatistics();
        }

        public static List<string> GetMovingStats()
        {
            var rep = services.GetRequiredService<MovingStatisticsService>();
            return rep.GetStatistics();
        }
    }
}
