using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW_CPS_HSEZoo.Interfaces;

namespace HW_CPS_HSEZoo.Models.Inventory.Animals
{
    public abstract class Animal : IAlive
    {
        protected int _number;
        protected int _food;

        public int Number { get { return _number; } }
        public int Food { get { return _food; } set { _food = value; } }
    }
}