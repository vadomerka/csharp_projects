using HW_CPS_HSEZoo.Models.Inventory.Things;

namespace HW_CPS_HSEZoo.Interfaces.Factories
{
    public interface IThingFactory : IInventoryFactory
    {
        public Thing? CreateThing(string itemType);
    }
}
