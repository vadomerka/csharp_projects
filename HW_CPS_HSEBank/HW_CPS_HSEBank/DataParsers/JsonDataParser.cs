using HW_CPS_HSEBank.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.DataParsers
{
    public class JsonDataParser : IFileDataParser<BankDataRepository>
    {
        private IServiceProvider services = CompositionRoot.Services;

        public BankDataRepository? ImportData(string fileName = "HseBank.json") {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                BankDataRepository? newrep = JsonSerializer.Deserialize<BankDataRepository>(fs);
                return newrep;
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

        public void ExportData(BankDataRepository brep, string fileName)
        {
            ExportDataAsync(brep, fileName).Wait();
        }
    }
}
