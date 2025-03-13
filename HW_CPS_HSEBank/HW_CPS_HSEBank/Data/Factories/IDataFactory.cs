﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data.Factories
{
    public interface IDataFactory<T>
    {
        public T Create();
        public T Create(T obj);
    }
}
