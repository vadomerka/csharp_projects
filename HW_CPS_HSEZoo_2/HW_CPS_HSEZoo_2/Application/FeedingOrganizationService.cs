using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Entities.Events;

namespace HW_CPS_HSEZoo_2.Application
{
    internal class FeedingOrganizationService
    {
        public event ZooEvents.FeedingTimeEvent? feedEvent;

        public void Feed(IFeedable animal, string food) {
            animal.Feed(food);
            //feedEvent?.Invoke(animal);
        }
    }
}
