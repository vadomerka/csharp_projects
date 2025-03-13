using HW_CPS_HSEBank.Data;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HW_CPS_HSEBank.DataParsers
{
    public class YamlDataParser : IFileDataParser<BankDataRepository>
    {
        private IServiceProvider services = CompositionRoot.Services;

        public BankDataRepository? ImportData(string fileName = "HseBank.yaml")
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

        public void ExportData(BankDataRepository brep, string fileName = "HseBank.yaml")
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
