using HW_CPS_HSEBank.DataParsing.DataParsers;

namespace HW_CPS_HSEBank.DataParsing.DataParserUtils
{
    /// <summary>
    /// Класс для импорта экспорта данных с стримом
    /// </summary>
    /// <typeparam name="Parser"></typeparam>
    public static class DataToStream<Parser> where Parser : IDataToText
    {
        private static Parser parser;
        public static IEnumerable<TData> ImportData<TData>(StreamReader sr) where TData : class
        {
            return parser.ImportData<TData>(sr);
        }

        public static void ExportData<TData>(StreamWriter sw, IEnumerable<TData> records) where TData : class
        {
            parser.ExportData(sw, records);
        }
    }
}
