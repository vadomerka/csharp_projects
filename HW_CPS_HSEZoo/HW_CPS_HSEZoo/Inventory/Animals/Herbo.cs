using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Inventory.Animals
{
    public abstract class Herbo : Animal, IMood
    {
        public int Kindness { get; }
    }
}