using HW_CPS_HSEZoo_2.Domain.Aggregates;

namespace HW_CPS_HSEZoo_2.Domain.Entities.Events
{
    internal static class ZooEvents
    {
        public delegate void AnimalMovedEvent(IEnclosable animal, IEnclosure enclosure);
        public delegate void FeedingTimeEvent(ISchedule schedule);
    }
}