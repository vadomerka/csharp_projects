using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Inventory.Animals;
using HW_CPS_HSEZoo.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEZoo
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
            // StringBuilder sb = new StringBuilder();
            List<T> ret = new List<T>();
            foreach (IInventory inventory in _inventoryList)
            {
                if (!inventory.GetType().IsAssignableTo(typeof(T))) { continue; }
                ret.Append((T)inventory);
            }
            return ret;
        }

        public void WriteInventoryData<T>() 
        {
            StringBuilder sb = new StringBuilder();

            foreach (IInventory item in GetInventoryData<T>())
            {
                sb.Append($"Id: {item.Number}, item type: ");
                sb.Append(item.GetType().ToString());
                // sb.Append("; ");
                // sb.Append(item.GetType().IsAssignableTo(typeof(IAlive)));
                sb.AppendLine();
            }
            Console.Write(sb.ToString());
        }
    }
}