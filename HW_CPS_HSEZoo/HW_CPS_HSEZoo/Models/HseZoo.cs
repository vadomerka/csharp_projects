using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Models
{
    /// <summary>
    /// Хранит и фильтрует инвентарь.
    /// </summary>
    public class HseZoo : IHasInventory
    {
        protected List<IInventory> _inventoryList = new List<IInventory>();
        public void AddToInventory(IInventory inventory)
        {
            _inventoryList.Add(inventory);
        }

        /// <summary>
        /// Возвращает список инвентаря только нужного типа.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetInventoryData<T>()
        {
            List<T> ret = new List<T>();
            foreach (IInventory inventory in _inventoryList)
            {
                if (!(inventory is T)) { continue; }
                T tap = (T)inventory;
                ret.Add(tap);
            }
            return ret;
        }
    }
}