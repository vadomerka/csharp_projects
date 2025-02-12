using HW_CPS_HSEZoo.Inventory.Animals;
using HW_CPS_HSEZoo.Interfaces;
using System;
using HW_CPS_HSEZoo.Inventory;

namespace HW_CPS_HSEZoo 
{
    class MainClass 
    {
        public static void Main (string[] args) 
        {
            HseZoo hseZoo = new HseZoo();
            hseZoo.AddInventory(new Monkey());
            hseZoo.AddInventory(new Rabbit());
            hseZoo.AddInventory(new Thing());
            hseZoo.WriteList<IInventory>();

            Console.ReadLine();
        }
    }
}