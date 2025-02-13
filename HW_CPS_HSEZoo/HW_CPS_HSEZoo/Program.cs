using HW_CPS_HSEZoo.Inventory.Animals;
using HW_CPS_HSEZoo.Interfaces;
using System;
using HW_CPS_HSEZoo.Inventory;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEZoo 
{
    // ServiceCollection services = new ServiceCollection();

    class MainClass 
    {
        public static void Main (string[] args) 
        {
            
            // services.AddSingleton(VetClinic);

            HseZoo hseZoo = new HseZoo();
            hseZoo.AddToInventory(new Monkey());
            hseZoo.AddToInventory(new Rabbit());
            hseZoo.AddToInventory(new Thing());
            hseZoo.WriteInventoryData<IInventory>();

            Console.ReadLine();
        }
    }
}