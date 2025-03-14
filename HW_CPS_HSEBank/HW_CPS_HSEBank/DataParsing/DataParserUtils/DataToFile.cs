using HW_CPS_HSEBank.DataParsing.DataParsers;

namespace HW_CPS_HSEBank.DataParsing.DataParserUtils
{
    public class DataToFile<Parser> where Parser : IDataToText
    {
        public static IEnumerable<TData> ImportData<TData>(string path) where TData : class
        {
            using (var sr = new StreamReader(path))
            {
                return DataToStream<Parser>.ImportData<TData>(sr);
            }
        }

        public static void ExportData<TData>(IEnumerable<TData> records, string path) where TData : class
        {
            using (var sr = new StreamWriter(path))
            {
                DataToStream<Parser>.ExportData(sr, records);
            }
        }
    }
}
