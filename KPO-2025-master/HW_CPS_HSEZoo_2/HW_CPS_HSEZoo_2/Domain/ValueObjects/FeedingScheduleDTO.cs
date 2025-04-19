using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.Domain.ValueObjects
{
    public struct FeedingScheduleDTO
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

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (((FeedingScheduleDTO)obj).Time != Time) return false;
            if (((FeedingScheduleDTO)obj).FoodType != FoodType) return false;
            return true;
        }
    }
}
