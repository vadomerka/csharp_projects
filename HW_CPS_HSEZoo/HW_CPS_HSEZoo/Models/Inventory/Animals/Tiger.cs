using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Models.Inventory.Animals
{
    public class Tiger : Predator
    {
        public Tiger(int num, int food = 0)
        {
            _number = num;
            _food = food;
        }
    }
}