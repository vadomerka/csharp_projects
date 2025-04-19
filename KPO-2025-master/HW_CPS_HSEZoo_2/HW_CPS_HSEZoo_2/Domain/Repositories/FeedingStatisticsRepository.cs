using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Repositories
{
    public class FeedingStatisticsRepository : IStatisticsRepository
    {
        private static List<string> _feedingStats = new List<string>();

        public FeedingStatisticsRepository() {
            _feedingStats = new List<string>();
        }

        public List<string> GetEntries() { return _feedingStats; }

        public void AddEntry(string entry)
        {
            _feedingStats.Add(entry);
        }
    }
}
