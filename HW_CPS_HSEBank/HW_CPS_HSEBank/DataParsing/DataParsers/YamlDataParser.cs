using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HW_CPS_HSEBank.DataParsing.DataParsers
{
    /// <summary>
    /// Класс для импорта экспорта данных из yaml
    /// </summary>
    public class YamlDataParser : IDataToText
    {
        public static string GetExtension() { return ".yaml"; }
        public IEnumerable<TData> ImportData<TData>(TextReader tr) where TData : class
        {
            IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
            IEnumerable<TData> newrep = deserializer.Deserialize<IEnumerable<TData>>(tr.ReadToEnd());
            return newrep;
        }

        public TextWriter ExportData<TData>(TextWriter tw, IEnumerable<TData> records) where TData : class
        {
            ISerializer serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
            //serializer.Serialize(fs, brep, typeof(BankDataRepository));
            serializer.Serialize(tw, records, records.GetType());
            tw.Write(serializer.Serialize(records));
            return tw;
        }
    }
}
