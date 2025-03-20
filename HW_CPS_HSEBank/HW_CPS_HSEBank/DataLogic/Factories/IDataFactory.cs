using HW_CPS_HSEBank.DataLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    /// <summary>
    /// Интерфейс фабрик
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataFactory<T> where T : class, IBankDataType
    {
        /// <summary>
        /// Метод создания дефолтного объекта
        /// </summary>
        /// <returns></returns>
        public T Create();

        /// <summary>
        /// Метод создания объекта с списком инициализации
        /// </summary>
        /// <returns></returns>
        public T Create(object[] args);

        /// <summary>
        /// Метод копирования объекта
        /// </summary>
        /// <returns></returns>
        public T Create(T obj);

        public int Id { set; }
    }
}
