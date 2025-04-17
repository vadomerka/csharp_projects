using System.Text.Json;

namespace HW_CPS_HSEBank.DataParsing.DataParsers
{
    /// <summary>
    /// Класс для импорта экспорта данных из json
    /// </summary>
    public class JsonDataParser : IDataToText
    {
        public static string GetExtension() { return ".json"; }
        public IEnumerable<TData> ImportData<TData>(TextReader tr) where TData : class
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            IEnumerable<TData>? newrep = JsonSerializer.Deserialize<IEnumerable<TData>>(tr.ReadToEnd(), options);
            if (newrep == null) throw new ArgumentNullException("Ошибка во время импорта данных.");
            return newrep;
        }

        public TextWriter ExportData<TData>(TextWriter tw, IEnumerable<TData> records) where TData : class
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                MaxDepth = 4,
                IgnoreReadOnlyFields = false,
            };
            tw.Write(JsonSerializer.Serialize(records, options));
            return tw;
        }
    }
}
