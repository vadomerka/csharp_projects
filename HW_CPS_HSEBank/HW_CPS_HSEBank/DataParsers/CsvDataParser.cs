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
using System.Xml.Serialization;

namespace HW_CPS_HSEBank.DataParsers
{
    public static class CsvDataParser
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static BankDataRepository? ImportData(string fileName = "hseBank")
        {
            BankDataRepository newBank = new();
            List<string> fileNames = new List<string> { fileName + "_accounts.csv",
                                                        fileName + "_operations.csv",
                                                        fileName + "_categories.csv"};
            ImportTData<AccountFactory, BankAccount>(fileNames[0], ref newBank);
            ImportTData<FinanceOperationFactory, FinanceOperation>(fileNames[1], ref newBank);
            ImportTData<CategoryFactory, Category>(fileNames[2], ref newBank);
            return newBank;
        }

        private static void ImportTData<TFactory, TData>(string fileName, ref BankDataRepository newBank) {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var factory = (IDataFactory<TData>)services.GetRequiredService<TFactory>();
                var record = factory.Create();
                var records = csv.EnumerateRecords(record);
                foreach (TData r in records)
                {

                    if (factory is IDataFactory<TData>)
                    {
                        newBank.AddData((IBankDataType)factory.Create(r));
                    }
                }
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
