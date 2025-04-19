namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface ISchedule : IEntity
    {
        public new int Id { get; }
        IAnimal Animal { get; }
        DateTime Time { get; }
        public string FoodType { get; }
    }
}
