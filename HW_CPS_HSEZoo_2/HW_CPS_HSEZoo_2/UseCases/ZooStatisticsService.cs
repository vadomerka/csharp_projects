using HW_CPS_HSEZoo_2.Domain.Entities.Events;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.UseCases
{
    internal class ZooStatisticsService
    {
        public static void FeedingEventHandler(ISchedule sch) {
            Console.WriteLine("Someone was fed");
        }
    }
}
