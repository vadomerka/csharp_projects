using HW_CPS_HSEBank.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.UI.MenuUtils
{
    public interface IMenuCommand : ICommand
    {
        //public IMenuCommand(string title, UIFunc func) { }
        public string Title { get; }
        public DateTime StartTime { get; }
        public double Duration { get; }
        public bool Execute();
    }
}
