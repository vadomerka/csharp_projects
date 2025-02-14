using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Models
{
    public class HseZoo : IHasInventory
    {
        protected List<IInventory> _inventoryList = new List<IInventory>();
        public void AddToInventory(IInventory inventory)
        {
            _inventoryList.Add(inventory);
        }

        public List<T> GetInventoryData<T>()
        {
            List<T> ret = new List<T>();
            foreach (IInventory inventory in _inventoryList)
            {
                if (!(inventory is T)) { continue; } // inventory.GetType().IsAssignableTo(typeof(T))
                T tap = (T)inventory;
                ret.Add(tap);
            }
            return ret;
        }
    }
}