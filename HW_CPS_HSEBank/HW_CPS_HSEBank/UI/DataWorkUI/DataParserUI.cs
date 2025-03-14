using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using static HW_CPS_HSEBank.UI.BankUI;

namespace HW_CPS_HSEBank.UI.DataWorkUI
{
    public static class DataParserUI
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static bool ExportData()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("JSON", ExportDataToFile<JsonDataParser>),
                    new MenuItem("YAML", ExportDataToFile<YamlDataParser>),
                    new MenuItem("CSV", ExportDataToFile<CsvDataParser>)
                };
            UtilsUI.MakeMenu(mainOptions, "Выберете формат в который хотите экспортировать:");
            return true;
        }

        private static bool ExportDataToFile<Parser>() where Parser : IDataToText
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");

            var brep = services.GetRequiredService<BankDataManager>();
            BankDataParser.Export<Parser>(brep, fileName);

            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }

        public static bool ImportData()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("JSON", ImportDataFromFile<JsonDataParser>),
                    new MenuItem("YAML", ImportDataFromFile<YamlDataParser>),
                    new MenuItem("CSV", ImportDataFromFile<CsvDataParser>)
                };
            UtilsUI.MakeMenu(mainOptions, "Выберете формат файла из которого хотите импортировать:");
            return true;
        }

        private static bool ImportDataFromFile<Parser>() where Parser : IDataToText
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу (без расширения).");
            fileName = Path.GetFileNameWithoutExtension(fileName);
            Console.WriteLine($"Данные импортируются из {fileName}.");
            BankDataManager? newrep;
            //var parser = services.GetRequiredService<Parser>();
            newrep = BankDataParser.Import<Parser>(fileName);
            if (newrep == null) throw new ArgumentNullException();
            try
            {
                // return
            }
            catch (HseBankException ex)
            {
                Console.WriteLine(ex.Message);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не был найден.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при чтении файла.");
                Console.WriteLine(ex);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            var oldrep = services.GetRequiredService<BankDataManager>();
            oldrep.Save(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }
    }
}
