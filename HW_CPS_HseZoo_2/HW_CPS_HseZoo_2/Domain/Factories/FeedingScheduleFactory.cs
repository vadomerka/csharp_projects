using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Interfaces;
using HW_CPS_HSEZoo_2.Domain.ValueObjects;

namespace HW_CPS_HSEZoo_2.Domain.Factories
{
    public class FeedingScheduleFactory
    {
        private int _id = 0;

        public FeedingSchedule Create(IFeedable animal, DateTime feedTime, string foodType)
        {
            return new FeedingSchedule(++_id, animal, feedTime, foodType);
        }

        public FeedingSchedule Create(IFeedable animal, FeedingScheduleDTO dto)
        {
            return new FeedingSchedule(++_id, animal, dto.Time, dto.FoodType);
        }
    }
}
