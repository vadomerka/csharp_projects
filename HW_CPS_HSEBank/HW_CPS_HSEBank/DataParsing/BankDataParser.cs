using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;

namespace HW_CPS_HSEBank.DataParsing
{
    public class BankDataParser
    {
        private static string?[] GetFileNames<Parser>(string? fileName) where Parser : IDataToText
        {
            string?[] fs = { null, null, null };
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                string extension = Parser.GetExtension();
                fs = new string[] { $"{fileName}_accounts{extension}",
                                    $"{fileName}_operations{extension}",
                                    $"{fileName}_categories{extension}"
                };
            }
            return fs;
        }
        public static BankDataManager Import<Parser>(string? fileName = null) where Parser : IDataToText
        {
            BankDataManager mgr = new BankDataManager();
            string?[] fs = GetFileNames<Parser>(fileName);

            List<BankAccount> accounts =
                BankListParser<Parser>.Import<BankAccount>(fs[0]).ToList();
            List<FinanceOperation> operations =
                BankListParser<Parser>.Import<FinanceOperation>(fs[1]).ToList();
            List<Category> categories =
                BankListParser<Parser>.Import<Category>(fs[2]).ToList();
            mgr.AddData(accounts);
            mgr.AddData(categories);
            mgr.AddData(operations);
            return mgr;
        }

        public static void Export<Parser>(BankDataManager mgr, string? fileName = null, string extension = ".txt") where Parser : IDataToText
        {
            var rep = mgr.GetRepository();
            string?[] fs = GetFileNames<Parser>(fileName);

            BankListParser<Parser>.Export<BankAccount>(rep.BankAccounts, fs[0]);
            BankListParser<Parser>.Export<FinanceOperation>(rep.FinanceOperations, fs[1]);
            BankListParser<Parser>.Export<Category>(rep.Categories, fs[2]);
        }
    }
}
