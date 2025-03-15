using HW_CPS_HSEBank.DataParsing.DataParsers;

namespace HW_CPS_HSEBank.DataParsing.DataParserUtils
{
    /// <summary>
    /// Класс для импорта экспорта данных с консолью
    /// </summary>
    /// <typeparam name="Parser"></typeparam>
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
