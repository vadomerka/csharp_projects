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
    internal class HseZoo 
    {
        List<IInventory> _inventoryList = new List<IInventory>();

        public void AddInventory(IInventory inventory)
        {
            _inventoryList.Add(inventory);
        }

        public void WriteList<T>() 
        {
            StringBuilder sb = new StringBuilder();
            foreach (IInventory inventory in _inventoryList)
            {
                IAlive? item = inventory as IAlive;
                if (item == null) { continue; }
                sb.Append($"Id: {item.Number}, item type: ");
                sb.Append(item.GetType().ToString());
                sb.Append("; ");
                // IAlive alive = (IAlive)item;
                sb.Append(item.GetType().IsAssignableTo(typeof(IAlive)));
                sb.AppendLine();

            }
            Console.Write(sb.ToString());
        }
    }
}