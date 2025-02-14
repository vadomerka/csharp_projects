using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEZoo.Interfaces
{
    public interface IHealthAnalizer
    {
        public bool AnalyzeHealth(IAlive? being) { return false; }
    }
}
