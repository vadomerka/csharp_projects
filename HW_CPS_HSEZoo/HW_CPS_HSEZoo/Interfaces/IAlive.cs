using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Interfaces
{
    public interface IAlive : IInventory
    {
        public int Food { get; }
    }
}
