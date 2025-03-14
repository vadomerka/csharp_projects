using CsvHelper;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using System.Reflection.PortableExecutable;

namespace HW_CPS_HSEBank.DataParsing.DataParsers
{
    public class CsvDataParser : IDataToText // : IFileDataParser<BankDataManager>
    {
        public static string GetExtension() { return ".csv"; }
        public IEnumerable<TData> ImportData<TData>(TextReader tr) where TData : class
        {
            using (var csv = new CsvReader(tr, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<TData>();
            }
        }

        public TextWriter ExportData<TData>(TextWriter tw, IEnumerable<TData> records) where TData : class
        {
            using (var csv = new CsvWriter(tw, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
            return tw;
        }
    }
}
