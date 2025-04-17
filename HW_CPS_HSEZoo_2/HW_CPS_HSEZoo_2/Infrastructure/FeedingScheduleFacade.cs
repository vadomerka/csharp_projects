using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;
using HW_CPS_HSEZoo_2.UseCases;

namespace HW_CPS_HSEZoo_2.Infrastructure
{
    public class FeedingScheduleFacade
    {
        private static IServiceProvider services = CompositionRoot.Services;
        public static ISchedule GetFeedingSchedule(int id)
        {
            var ser = services.GetRequiredService<FeedingOrganizationService>();
            return ser.GetSchedule(id);
        }

        public static void AddFeedingSchedule(int enId, int anId, FeedingScheduleDTO dto)
        {
            var ser = services.GetRequiredService<FeedingOrganizationService>();
            var ans = services.GetRequiredService<AnimalDataService>();
            var enc = services.GetRequiredService<EnclosureDataService>();

            var animal = ans.GetAnimal(enc.GetEnclosure(enId), anId);
            ser.AddSchedule(ser.Create(animal, dto));
        }

        public static void DeleteFeedingSchedule(int id)
        {
            var ser = services.GetRequiredService<FeedingOrganizationService>();
            ser.RemoveSchedule(ser.GetSchedule(id));
        }
    }
}
