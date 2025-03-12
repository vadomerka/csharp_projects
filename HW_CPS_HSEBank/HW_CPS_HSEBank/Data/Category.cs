using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Data
{
    public class Category
    {
        private int id;
        private string type;
        private string name;

        public Category()
        {
            this.id = 0;
            this.type = "";
            this.name = "";
        }

        public Category(int id, string type, string name)
        {
            this.id = id;
            this.type = type;
            this.name = name;
        }

        public int Id { get => id; set { id = value; } }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
    }
}
