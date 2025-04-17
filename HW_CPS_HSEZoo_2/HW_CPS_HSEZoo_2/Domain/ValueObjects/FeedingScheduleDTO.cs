using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    public class FeedingScheduleDTO
    { 
        public FeedingScheduleDTO() {
            Time = DateTime.MinValue;
            FoodType = "";
        }
        public FeedingScheduleDTO(DateTime feedTime, string foodType)
        {
            Time = feedTime;
            FoodType = foodType;
        }

        public DateTime Time { get; set; }
        public string FoodType { get; set; }
    }
}
