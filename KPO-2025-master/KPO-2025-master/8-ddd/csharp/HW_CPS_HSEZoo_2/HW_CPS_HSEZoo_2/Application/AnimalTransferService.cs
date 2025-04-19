using HW_CPS_HSEZoo_2.Domain.Aggregates;
using HW_CPS_HSEZoo_2.Domain.Entities;
using HW_CPS_HSEZoo_2.Domain.Entities.Events;

namespace HW_CPS_HSEZoo_2.Application
{
    internal class AnimalTransferService
    {
        public event ZooEvents.AnimalMovedEvent? moveEvent;

        public void Move(IEnclosure from, IEnclosable animal, IEnclosure to)
        {
            if (from.CheckRemove(animal)) throw new ArgumentException();
            if (to.CheckAdd(animal)) throw new ArgumentException();
            from.RemoveEntity(animal);
            to.AddEntity(animal);
            moveEvent?.Invoke(animal, to);
        }
    }
}
