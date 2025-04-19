using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Events
{
    public static class ZooEvents
    {
        public delegate void AnimalMovedEvent(IEnclosure from, IEnclosable animal, IEnclosure to);
        public delegate void FeedingTimeEvent(ISchedule schedule);
    }
}