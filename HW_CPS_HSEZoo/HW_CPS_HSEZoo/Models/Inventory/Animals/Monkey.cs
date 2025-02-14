using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Models.Inventory.Animals
{
    public class Monkey : Herbo
    {
        public Monkey(int num, int food = 0, int kindness = 0)
        {
            _number = num;
            _food = food;
            _kindness = kindness;
        }
    }
}