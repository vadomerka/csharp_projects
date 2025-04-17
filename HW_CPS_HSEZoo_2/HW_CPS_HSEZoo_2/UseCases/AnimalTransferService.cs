using HW_CPS_HSEZoo_2.Domain.Entities.Events;
using HW_CPS_HSEZoo_2.Domain.Interfaces;

namespace HW_CPS_HSEZoo_2.UseCases
{
    internal class AnimalTransferService
    {
        public event ZooEvents.AnimalMovedEvent? moveEvent;

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
        }
    }
}
