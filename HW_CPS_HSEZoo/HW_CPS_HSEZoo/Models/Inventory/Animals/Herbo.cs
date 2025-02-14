using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Models.Inventory.Animals
{
    public abstract class Herbo : Animal, IMood
    {
        protected int _kindness;

        public int Kindness { get { return _kindness; } set { _kindness = value; } }
    }
}