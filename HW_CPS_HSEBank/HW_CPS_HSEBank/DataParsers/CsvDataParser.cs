using CsvHelper;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.DataManagement;

namespace HW_CPS_HSEBank.DataParsers
{
    public class CsvDataParser : IFileDataParser<BankDataManager>
    {
        private IServiceProvider services = CompositionRoot.Services;
        
        public BankDataManager? ImportData(string fileName = "hseBank")
        {
            BankDataManager newMng = BankDataManager.GetNewManager();
            List<string> fileNames = new List<string> { fileName + "_accounts.csv",
                                                        fileName + "_operations.csv",
                                                        fileName + "_categories.csv"};
            ImportTData<AccountFactory, BankAccount>(fileNames[0], ref newMng);
            ImportTData<FinanceOperationFactory, FinanceOperation>(fileNames[1], ref newMng);
            ImportTData<CategoryFactory, Category>(fileNames[2], ref newMng);
            return newMng;
        }

        private void ImportTData<TFactory, TData>(string fileName, ref BankDataManager newBank) 
            where TFactory : IDataFactory<TData> where TData : IBankDataType
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var factory = services.GetRequiredService<TFactory>();
                var record = factory.Create();
                var records = csv.EnumerateRecords(record);
                foreach (TData r in records)
                {
                    newBank.AddData(factory.Create(r));
                }
            }
        }

        public void ExportData(BankDataManager bmng, string fileName = "hseBank")
        {
            var brep = bmng.GetRepository();
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
