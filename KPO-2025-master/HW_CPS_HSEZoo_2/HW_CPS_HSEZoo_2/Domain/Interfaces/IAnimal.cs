using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Interfaces
{
    public interface IAnimal : IEnclosable, IFeedable
    {
        public string Species { get; }
        public List<string> AnimalTypes { get; }
        public string Name { get; }
        public DateOnly BirthDate { get; }
        public Gender Gender { get; }
        public string FavouriteFood { get; }
        public bool IsHealthy { get; }
    }
}
