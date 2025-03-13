using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.DataLogic.Factories
{
    public class CategoryFactory : IDataFactory<Category>
    {
        private int lastId = 0;
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
