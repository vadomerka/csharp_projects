using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.Repositories;

namespace HW_CPS_HSEZoo_2.UseCases.Statistics
{
    public class MovingStatisticsService
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public MovingStatisticsService() { }
        public MovingStatisticsService(IServiceProvider sservices)
        {
            services = sservices;
            AnimalTransferService.moveEvent += EventHandler;
        }

        public List<string> GetStatistics()
        {
            var rep = services.GetRequiredService<MovingStatisticsRepository>();
            return rep.GetEntries();
        }

        public static void EventHandler(IEnclosure e1, IEnclosable a1, IEnclosure e2)
        {
            var rep = services.GetRequiredService<MovingStatisticsRepository>();
            rep.AddEntry($"Animal {a1.Id} was moved from enclosure {e1.Id} to enclosure {e2.Id}.");
        }
    }
}
