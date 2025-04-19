using HW_CPS_HSEZoo_2.Domain.Events;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.UseCases
{
    public class AnimalTransferService
    {
        public static event ZooEvents.AnimalMovedEvent? moveEvent;
        public static IServiceProvider services = CompositionRoot.Services;

        public AnimalTransferService() { }
        public AnimalTransferService(IServiceProvider sservices) { services = sservices; }

        public void Add(IEnclosure to, IEnclosable animal) {
            if (to.CheckAdd(animal)) throw new ArgumentException();
            to.AddEntity(animal);
        }

        public void Move(IEnclosure from, IEnclosable animal, IEnclosure to)
        {
            if (from.CheckRemove(animal)) throw new ArgumentException();
            if (to.CheckAdd(animal)) throw new ArgumentException();
            from.RemoveEntity(animal);
            to.AddEntity(animal);
            moveEvent?.Invoke(from, animal, to);
        }

        public void Remove(IEnclosure from, IEnclosable animal)
        {
            if (from.CheckRemove(animal)) throw new ArgumentException();
            from.RemoveEntity(animal);
            services.GetRequiredService<FeedingOrganizationService>().RemoveSchedules((IAnimal)animal);
        }
    }
}
