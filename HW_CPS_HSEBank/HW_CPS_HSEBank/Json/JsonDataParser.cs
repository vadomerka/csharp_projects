﻿using HW_CPS_HSEBank.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.Json
{
    internal static class JsonDataParser
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static BankDataRepository? ImportData(string fileName = "HseBank.json") {
            var brep = services.GetRequiredService<BankDataRepository>();
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

        public static async Task ExportDataAsync(string fileName="HseBank.json")
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                var brep = services.GetRequiredService<BankDataRepository>();
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    MaxDepth = 4,
                    IgnoreReadOnlyFields = false,
                };
                await JsonSerializer.SerializeAsync(fs, brep, options);
            }
        }
    }
}
