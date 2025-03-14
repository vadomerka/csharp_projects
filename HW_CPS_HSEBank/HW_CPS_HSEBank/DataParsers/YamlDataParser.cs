using HW_CPS_HSEBank.DataLogic.DataManagement;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HW_CPS_HSEBank.DataParsers
{
    public class YamlDataParser : IFileDataParser<BankDataManager>
    {
        private IServiceProvider services = CompositionRoot.Services;

        public BankDataManager? ImportData(string fileName = "HseBank")
        {
            using (TextReader fs = new StreamReader(fileName + ".yaml"))
            {
                IDeserializer deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                BankDataRepository newrep = deserializer.Deserialize<BankDataRepository>(fs);
                BankDataManager? newMng = (newrep == null) ? null : new BankDataManager(newrep);
                return newMng;
            }
        }

        public void ExportData(BankDataManager bmng, string fileName = "HseBank")
        {
            var brep = bmng.GetRepository();
            using (TextWriter fs = new StreamWriter(fileName + ".yaml", false))
            {
                ISerializer serializer = new SerializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();
                serializer.Serialize(fs, brep, typeof(BankDataRepository));
            }
        }
    }
}
