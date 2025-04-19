using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    public class FeedingSchedule : ISchedule
    {
        private IFeedable _animal;

        public FeedingSchedule(int id = 0)
        {
            Id = id;
            _animal = new Animal();
            Time = DateTime.MinValue;
            FoodType = "";
        }

        public FeedingSchedule(int id, IFeedable animal, DateTime feedTime, string foodType)
        {
            Id = id;
            _animal = animal;
            Time = feedTime;
            FoodType = foodType;
        }

        public int Id { get; set; }
        public IAnimal Animal { get { return (IAnimal)_animal; } set { _animal = value; } }
        public DateTime Time { get; set; }
        public string FoodType { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return ((FeedingSchedule)obj).Id == Id;
        }
    }
}
