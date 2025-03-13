using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataParsers
{
    public interface IFileDataParser<T>
    {
        public T? ImportData(string fileName);

        public void ExportData(T bmng, string fileName);
    }
}
