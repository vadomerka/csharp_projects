using HW_CPS_HSEZoo.Models.Inventory.Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Interfaces.Factories
{
    public interface IAnimalFactory : IInventoryFactory
    {
        public Animal? CreateAnimal(string itemType);
    }
}
