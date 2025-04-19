using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Repositories
{
    public class MovingStatisticsRepository : IStatisticsRepository
    {
        private static List<string> _stats = new List<string>();

        public MovingStatisticsRepository() {
            _stats = new List<string>();
        }

        public List<string> GetEntries() { return _stats; }

        public void AddEntry(string entry)
        {
            _stats.Add(entry);
        }
    }
}
