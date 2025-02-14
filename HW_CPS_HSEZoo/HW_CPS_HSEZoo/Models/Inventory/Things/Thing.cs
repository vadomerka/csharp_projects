using HW_CPS_HSEZoo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Models.Inventory.Things
{
    internal abstract class Thing : IInventory
    {
        protected int _number;

        public int Number { get { return _number; } }
    }
}
