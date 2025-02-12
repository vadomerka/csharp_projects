using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo
{
    public abstract class Herbo : Animal, IMood
    {
        public int Kindness { get; }
    }
}