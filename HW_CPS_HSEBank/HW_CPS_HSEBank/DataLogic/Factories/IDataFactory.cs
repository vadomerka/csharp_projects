using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    public interface IDataFactory<T>
    {
        public T Create();
        public T Create(object[] args);
        public T Create(T obj);
    }
}
