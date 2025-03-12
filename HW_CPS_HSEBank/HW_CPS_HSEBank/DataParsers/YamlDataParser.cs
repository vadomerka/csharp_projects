using HW_CPS_HSEBank.Data;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HW_CPS_HSEBank.DataParsers
{
    public class YamlDataParser
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static BankDataRepository? ImportData(string fileName = "HseBank.yaml")
        {
            using (TextReader fs = new StreamReader(fileName))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                BankDataRepository newrep = deserializer.Deserialize<BankDataRepository>(fs);
                return newrep;
            }
        }

        public static async Task ExportDataAsync(BankDataRepository brep, string fileName = "HseBank.yaml")
        {
            using (TextWriter fs = new StreamWriter(fileName, false))
            {
                ISerializer serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                serializer.Serialize(fs, brep, typeof(BankDataRepository));
            }
        }
    }
}
