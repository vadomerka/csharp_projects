using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataParsing.DataParsers
{
    public interface IDataToText
    {
        public static virtual string GetExtension() { return ".txt"; }
        public IEnumerable<TData> ImportData<TData>(TextReader tr) where TData : class;

        public TextWriter ExportData<TData>(TextWriter tw, IEnumerable<TData> records) where TData : class;
    }
}
