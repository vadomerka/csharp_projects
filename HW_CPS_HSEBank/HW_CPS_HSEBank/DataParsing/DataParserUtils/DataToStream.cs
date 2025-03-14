using CsvHelper;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataParsing.DataParserUtils
{
    public static class DataToStream<Parser> where Parser : IDataToText // : IFileDataParser<BankDataManager>
    {
        private static Parser parser = CompositionRoot.Services.GetRequiredService<Parser>();
        public static IEnumerable<TData> ImportData<TData>(StreamReader sr) where TData : class
        {
            return parser.ImportData<TData>(sr);
        }

        public static void ExportData<TData>(StreamWriter sw, IEnumerable<TData> records) where TData : class
        {
            TextWriter res = parser.ExportData(sw, records);
            //string check = res.ToString();
            //sw.Write(res);
            //check = sw.ToString();
        }
    }
}
