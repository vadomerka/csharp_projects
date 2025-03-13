using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data.Factories
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
        public Category Create(Category obj)
        {
            return new Category(obj.Id, obj.Type);
        }
    }
}
