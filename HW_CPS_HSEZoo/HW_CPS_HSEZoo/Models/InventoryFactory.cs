using HW_CPS_HSEZoo.Interfaces.Factories;
using HW_CPS_HSEZoo.Models.Inventory.Animals;
using HW_CPS_HSEZoo.Models.Inventory.Things;

namespace HW_CPS_HSEZoo.Models
{
    /// <summary>
    /// Создает инвентарь.
    /// </summary>
    public class InventoryFactory : IAnimalFactory, IInventoryFactory
    {
        protected int index = 0;

        public Animal CreateAnimal(string itemType)
        {
            itemType = itemType.ToLower();
            Animal? ret = itemType == "monkey" ? new Monkey(index) :
                itemType == "rabbit" ? new Rabbit(index) :
                itemType == "tiger" ? new Tiger(index) :
                itemType == "wolf" ? new Wolf(index) : null;
            if (ret == null) { throw new ArgumentException(); }
            index++;
            return ret;
        }

        public Thing CreateThing(string itemType)
        {
            itemType = itemType.ToLower();
            Thing? ret = itemType == "table" ? new Table(index) :
                itemType == "computer" ? new Computer(index) : null;
            if (ret == null) { throw new ArgumentException(); }
            index++;
            return ret;
        }
    }
}
