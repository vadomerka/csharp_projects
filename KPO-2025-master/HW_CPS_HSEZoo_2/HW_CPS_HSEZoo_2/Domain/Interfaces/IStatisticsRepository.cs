namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface IStatisticsRepository
    {
        public List<string> GetEntries();
        public void AddEntry(string entry);
    }
}
