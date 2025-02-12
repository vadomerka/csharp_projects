using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo
{
    public abstract class Animal : IAlive
    {
        public int Food { get; }
    }
}