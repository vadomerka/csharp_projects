using System.Windows.Input;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.Entities
{
    public class FeedingSchedule : ISchedule
    {
        public FeedingSchedule(int id, IFeedable animal, DateTime feedTime, string foodType)
        {
            Id = id;
            Animal = animal;
            Time = feedTime;
            FoodType = foodType;
        }

        public int Id { get; }
        public IFeedable Animal { get; set; }
        public DateTime Time { get; set; }
        public string FoodType { get; set; }
    }
}
