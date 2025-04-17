namespace HW_CPS_HSEBank.DataParsing.DataParsers
{
    /// <summary>
    /// Интерфейс для парсера в TextReader/TextWriter
    /// </summary>
    public interface IDataToText
    {
        public static virtual string GetExtension() { return ".txt"; }
        public IEnumerable<TData> ImportData<TData>(TextReader tr) where TData : class;
        public TextWriter ExportData<TData>(TextWriter tw, IEnumerable<TData> records) where TData : class;
    }
}
