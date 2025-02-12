using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Inventory.Animals
{
    public abstract class Animal : IAlive
    {
        public int Number { get; }
        public int Food { get; }
    }
}