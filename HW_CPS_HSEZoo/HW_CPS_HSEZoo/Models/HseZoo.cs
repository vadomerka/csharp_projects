using System.Text;
using HW_CPS_HSEZoo.Interfaces;
using HW_CPS_HSEZoo.Models.Inventory.Animals;

namespace HW_CPS_HSEZoo.Models
{
    internal class HseZoo : IHasInventory
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
                if (!inventory.GetType().IsAssignableTo(typeof(T))) { continue; }
                T tap = (T)inventory;
                ret.Add(tap);
            }
            return ret;
        }

        public void WriteInventoryData<T>()
        {
            StringBuilder sb = new StringBuilder();
            var items = GetInventoryData<T>();
            if (items.Count == 0) { sb.AppendLine("Список пуст."); }
            foreach (IInventory? item in items)
            {
                if (item == null) { continue; }
                sb.Append($"Id: {item.Number}; Вид животного: ");
                sb.Append(item.GetType().Name);
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public void WriteAnimalFoodData()
        {
            StringBuilder sb = new StringBuilder();
            var items = GetInventoryData<Animal>();
            if (items.Count == 0) { sb.AppendLine("Список пуст."); }
            foreach (Animal? item in items)
            {
                if (item == null) { continue; }
                sb.Append($"Id: {item.Number}; Вид животного: ");
                sb.Append(item.GetType().Name);
                sb.Append("; ");
                sb.Append($"Количество еды: {item.Food}");
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }

        public void WriteContactAnimalsData()
        {
            StringBuilder sb = new StringBuilder();
            var items = GetInventoryData<Herbo>();
            if (items.Count == 0) { sb.AppendLine("Список пуст."); }
            foreach (Herbo? item in items)
            {
                if (item == null) { continue; }
                if (item.Kindness < 5) { continue; }
                sb.Append($"Id: {item.Number}; Вид животного: ");
                sb.Append(item.GetType().Name);
                sb.Append("; ");
                sb.Append($"Доброта животного: {item.Kindness}");
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }
    }
}