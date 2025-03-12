using HW_CPS_HSEBank.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using CsvHelper;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.Data.Factories;

namespace HW_CPS_HSEBank.DataParsers
{
    public static class CsvDataParser
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static BankDataRepository? ImportData(string fileName = "hseBank.csv")
        {
            using (var reader = new StreamReader(fileName + "_accounts.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var record = new BankAccount();
                var records = csv.EnumerateRecords(record);
                var answer = new List<BankAccount>();
                foreach (var r in records)
                {
                    var factory = services.GetRequiredService<AccountFactory>();
                    answer.Add(factory.CreateAccount(r));
                }
                return services.GetRequiredService<BankDataRepository>();
                // todo: all other data types.
            }
        }

        public static void ExportData(BankDataRepository brep, string fileName = "hseBank")
        {
            using (var writer = new StreamWriter(fileName + "_accounts.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var records1 = brep.BankAccounts;
                csv.WriteRecords(records1);
            }
            using (var writer = new StreamWriter(fileName + "_operations.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var records2 = brep.FinanceOperations;
                csv.WriteRecords(records2);
            }
            using (var writer = new StreamWriter(fileName + "_categories.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var records3 = brep.Categories;
                csv.WriteRecords(records3);
            }
        }
    }
}
