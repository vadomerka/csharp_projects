using HW_CPS_HSEBank.DataLogic;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HW_CPS_HSEBank.DataParsers
{
    public class YamlDataParser : IFileDataParser<BankDataManager>
    {
        private IServiceProvider services = CompositionRoot.Services;

        public BankDataManager? ImportData(string fileName = "HseBank.yaml")
        {
            using (TextReader fs = new StreamReader(fileName))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                BankDataRepository newrep = deserializer.Deserialize<BankDataRepository>(fs);
                BankDataManager? newMng = (newrep == null) ? null : new BankDataManager(newrep);
                return newMng;
            }
        }

        public void ExportData(BankDataManager bmng, string fileName = "HseBank.yaml")
        {
            var brep = bmng.GetRepository();
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
