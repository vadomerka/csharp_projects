using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Repositories
{
    public class FeedingScheduleRepository : IFeedingScheduleRepository
    {
        private List<ISchedule> _entities = new List<ISchedule>();

        public FeedingScheduleRepository() { }

        public List<ISchedule> GetEntities() { 
            return _entities; 
        }

        public ISchedule GetEntity(int id)
        {
            var res = _entities.Find((x) => x.Id == id);
            if (res == null) throw new ArgumentException();
            return res;
        }

        public void AddEntity(ISchedule entity)
        {
            _entities.Add(entity);
        }

        public bool CheckRemove(ISchedule entity) { 
            return _entities.Contains(entity);
        }

        public void RemoveEntity(ISchedule entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveEntitiesByAnimal(IAnimal animal)
        {
            int k = 0;
            while (k < _entities.Count)
            {
                if (_entities[k].Animal.Id == animal.Id)
                {
                    _entities.RemoveAt(k);
                    k--;
                }
                k++;
            }
        }

        public void SortByTime()
        {
            _entities.Sort((a, b) => a.Time.CompareTo(b.Time));
        }
    }
}
