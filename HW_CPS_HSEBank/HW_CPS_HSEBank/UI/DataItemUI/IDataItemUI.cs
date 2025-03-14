using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.UI.DataItemUI
{
    public interface IDataItemUI
    {
        public string Title { get; }

        public bool AddItem();
        public bool FindItem();
        public bool DeleteItem();
        public bool ChangeItem();
    }
}
