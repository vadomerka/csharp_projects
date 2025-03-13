using HW_CPS_HSEBank.DataLogic;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataParsers
{
    public class JsonDataParser : IFileDataParser<BankDataManager>
    {
        private IServiceProvider services = CompositionRoot.Services;

        public BankDataManager? ImportData(string fileName = "HseBank.json") {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                BankDataRepository? newrep = JsonSerializer.Deserialize<BankDataRepository>(fs);
                BankDataManager? newMng = (newrep == null) ? null : new BankDataManager(newrep);
                return newMng;
            }
        }

        private async Task ExportDataAsync(BankDataRepository brep, string fileName = "HseBank.json")
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    MaxDepth = 4,
                    IgnoreReadOnlyFields = false,
                };
                await JsonSerializer.SerializeAsync(fs, brep, options);
            }
        }

        public void ExportData(BankDataManager bmng, string fileName)
        {
            ExportDataAsync(bmng.GetRepository(), fileName).Wait();
        }
    }
}
