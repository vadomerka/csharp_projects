using HW_CPS_HSEZoo_2.Domain.Entities.Events;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using System.Timers;

namespace HW_CPS_HSEZoo_2.Domain.Aggregates
{
    internal class FeedingTimeRepository : IFeedingScheduleRepository
    {
        private List<ISchedule> _entities = new List<ISchedule>();

        public FeedingTimeRepository() { }

        public List<ISchedule> GetEntities() { return _entities; }

        public ISchedule GetEntity(int id)
        {
            var res = _entities.Find((x) => x.Id == id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        public void AddEntity(ISchedule enclosure)
        {
            _entities.Add(enclosure);
        }

        public void RemoveEntity(ISchedule enclosure)
        {
            _entities.Remove(enclosure);
        }
    }
}
