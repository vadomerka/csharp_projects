using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using YamlDotNet.Core;

namespace HW_CPS_HSEBank.DataParsing.DataParserUtils
{
    public class DataToConsole<Parser> where Parser : IDataToText
    {
        public static IEnumerable<TData> ImportData<TData>() where TData : class
        {
            using (var sr = new StreamReader(Console.OpenStandardInput()))
            {
                return DataToStream<Parser>.ImportData<TData>(sr);
            }
        }

        public static void ExportData<TData>(IEnumerable<TData> records) where TData : class
        {
            using (var sr = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true })
            {
                DataToStream<Parser>.ExportData(sr, records);
            }
        }
    }
}
