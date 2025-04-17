using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Entities.Events
{
    internal static class ZooEvents
    {
        public delegate void AnimalMovedEvent(IEnclosure from, IEnclosable animal, IEnclosure to);
        public delegate void FeedingTimeEvent(ISchedule schedule);
    }
}