namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface ISchedule : IEntity
    {
        IFeedable Animal { get; }
        DateTime Time { get; }
    }
}
