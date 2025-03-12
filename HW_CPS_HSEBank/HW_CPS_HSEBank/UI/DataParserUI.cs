using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.DataParsers;
using Microsoft.Extensions.DependencyInjection;
using static HW_CPS_HSEBank.UI.BankUI;

namespace HW_CPS_HSEBank.UI
{
    public static class DataParserUI
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static bool ExportData()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("JSON", ExportJsonData),
                    new MenuItem("YAML", ExportYamlData),
                    new MenuItem("CSV", ExportCsvData)
                };
            MakeMenu(mainOptions, "Выберете формат в который хотите экспортировать:");
            return true;
        }

        private static bool ExportJsonData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");
            //string? name = Console.ReadLine();
            //if (name == null) return true;

            var brep = services.GetRequiredService<BankDataRepository>();
            _ = JsonDataParser.ExportDataAsync(brep, fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ExportYamlData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");
            //string? name = Console.ReadLine();
            //if (name == null) return true;

            var brep = services.GetRequiredService<BankDataRepository>();
            _ = YamlDataParser.ExportDataAsync(brep, fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return true;
        }

        private static bool ExportCsvData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            fileName = Path.GetFileNameWithoutExtension(fileName);
            Console.WriteLine($"Данные экспортируются в {fileName}_[table].csv.");

            var brep = services.GetRequiredService<BankDataRepository>();
            CsvDataParser.ExportData(brep, fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return true;
        }

        public static bool ImportData()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("JSON", ImportJsonData),
                    new MenuItem("YAML", ImportYamlData),
                    new MenuItem("CSV", ImportCsvData)
                };
            MakeMenu(mainOptions, "Выберете формат файла из которого хотите импортировать:");
            return true;
        }

        private static bool ImportJsonData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные импортируются из {fileName}.");

            BankDataRepository? newrep = JsonDataParser.ImportData(fileName);
            if (newrep == null) return true;

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Swap(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }

        //private static BankDataRepository? ImportTData<T>() {
        //    Console.Clear();
        //    string fileName = GetReqUserString("Введите путь к файлу.");
        //    Console.WriteLine($"Данные импортируются из {fileName}.");

        //    BankDataRepository? newrep = T.ImportData(fileName);
        //}

        private static bool ImportYamlData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные импортируются из {fileName}.");

            BankDataRepository? newrep = YamlDataParser.ImportData(fileName);
            if (newrep == null) return true;

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Swap(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ImportCsvData()
        {
            Console.Clear();
            string fileName = GetReqUserString("Введите путь к файлу.");
            fileName = Path.GetFileNameWithoutExtension(fileName);
            Console.WriteLine($"Данные импортируются из {fileName}_[table].csv.");

            BankDataRepository? newrep = CsvDataParser.ImportData(fileName);
            if (newrep == null) return true;

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Swap(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }
    }
}
