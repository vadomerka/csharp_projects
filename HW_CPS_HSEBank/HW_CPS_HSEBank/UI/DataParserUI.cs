using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.Exceptions;
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
                    new MenuItem("JSON", ExportDataToFile<JsonDataParser>),
                    new MenuItem("YAML", ExportDataToFile<YamlDataParser>),
                    new MenuItem("CSV", ExportDataToFile<CsvDataParser>)
                };
            UtilsUI.MakeMenu(mainOptions, "Выберете формат в который хотите экспортировать:");
            return true;
        }

        private static bool ExportDataToFile<Parser>() where Parser : IDataToText {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");

            var brep = services.GetRequiredService<BankDataManager>();
            BankDataParser.Export<Parser>(brep, fileName);

            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }

        /*private static bool ExportJsonData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");

            var brep = services.GetRequiredService<BankDataRepository>();
            _ = JsonDataParser.ExportDataAsync(brep, fileName);

            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ExportYamlData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные экспортируются в {fileName}.");

            var brep = services.GetRequiredService<BankDataRepository>();
            _ = YamlDataParser.ExportDataAsync(brep, fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ExportCsvData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            fileName = Path.GetFileNameWithoutExtension(fileName);
            Console.WriteLine($"Данные экспортируются в {fileName}_[table].csv.");

            var brep = services.GetRequiredService<BankDataRepository>();
            CsvDataParser.ExportData(brep, fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return false;
        }
*/
        
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
            catch (HseBankException ex) {
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

        /*private static bool ImportJsonData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные импортируются из {fileName}.");
            BankDataRepository? newrep = new BankDataRepository();
            try
            {
                ImportFromFile(ref newrep, fileName, JsonDataParser.ImportData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не был найден.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при чтении файла.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Save(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ImportYamlData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            Console.WriteLine($"Данные импортируются из {fileName}.");
            BankDataRepository? newrep = new BankDataRepository();
            try
            {
                ImportFromFile(ref newrep, fileName, YamlDataParser.ImportData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл не был найден.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при чтении файла.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Save(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }

        private static bool ImportCsvData()
        {
            Console.Clear();
            string fileName = UtilsUI.GetReqUserString("Введите путь и название файлов (без расширений и типа таблицы).");
            fileName = Path.GetFileNameWithoutExtension(fileName);
            Console.WriteLine($"Данные импортируются из {fileName}_[table].csv.");

            BankDataRepository? newrep = new BankDataRepository();
            try
            {
                ImportFromFile(ref newrep, fileName, CsvDataParser.ImportData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Один из файлов не был найден.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при чтении файлов.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Save(newrep);

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return false;
        }*/
    }
}
