﻿using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    /// <summary>
    /// Фабрика для создания категорий
    /// </summary>
    public class CategoryFactory : IDataFactory<Category>
    {
        protected int lastId = 0;
        public int Id { set => lastId = value; }

        public Category Create()
        {
            return new Category(++lastId);
        }
        public Category Create(string type)
        {
            return new Category(++lastId, type);
        }
        public Category Create(object[] args)
        {
            if (args.Length != 1) throw new ArgumentException();
            return new Category(++lastId, (string)args[0]);
        }
        public Category Create(Category obj)
        {
            return new Category(obj.Id, obj.Type);
        }
    }
}
