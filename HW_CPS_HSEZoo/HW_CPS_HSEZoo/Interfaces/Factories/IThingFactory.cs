using HW_CPS_HSEZoo.Models.Inventory.Things;

namespace HW_CPS_HSEZoo.Interfaces.Factories
{
    internal interface IThingFactory : IInventoryFactory
    {
        public Thing? CreateThing(string itemType);
    }
}
