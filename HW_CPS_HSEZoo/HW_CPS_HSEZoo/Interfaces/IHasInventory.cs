using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Interfaces
{
    public interface IHasInventory
    {
        public void AddToInventory<T>() {}
        public List<T> GetInventoryData<T>() { return null; }
        public void WriteInventoryData<T>() {}
    }
}