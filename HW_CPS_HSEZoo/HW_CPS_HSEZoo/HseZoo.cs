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
            foreach (IInventory item in _inventoryList)
            {
                // IAlive? item = inventory as IAlive;
                // if (item.GetType() == typeof(T)) { continue; }
                if (item.GetType().IsAssignableTo(typeof(T))) { continue; }
                
                // typeof(T).ToString();
                sb.Append($"Id: {item.Number}, item type: ");
                sb.Append(item.GetType().ToString());
                sb.Append("; ");
                sb.Append(typeof(T));
                sb.Append("; ");
                sb.Append(item.GetType());
                sb.Append("; ");
                // IAlive alive = (IAlive)item;
                sb.Append(item.GetType().IsAssignableTo(typeof(T)));
                sb.AppendLine();

            }
            Console.Write(sb.ToString());
        }
    }
}