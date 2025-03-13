using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.UI
{
    public delegate bool UIFunc();

    public struct MenuItem
    {
        public string _title;
        public UIFunc _func;

        public MenuItem(string title, UIFunc func)
        {
            _title = title;
            _func = func;
        }
    }
}
