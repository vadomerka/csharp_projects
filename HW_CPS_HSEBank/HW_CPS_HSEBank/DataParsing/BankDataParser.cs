using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.DataParsing
{
    /// <summary>
    /// Класс для импорта экспорта данных
    /// </summary>
    public class BankDataParser
    {
        /// <summary>
        /// Метод возвращает стандартные названия файлов для работы парсеров.
        /// </summary>
        /// <typeparam name="Parser">Парсер для получения расширения файла</typeparam>
        /// <param name="fileName">Название файла.</param>
        /// <returns>Список названий файлов.</returns>
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
        /// <summary>
        /// Метод импортирует объект BankDataManager
        /// </summary>
        /// <typeparam name="Parser">Парсер</typeparam>
        /// <param name="fileName"></param>
        /// <returns>Считанный объект</returns>
        public static BankDataManager Import<Parser>(string? fileName = null) where Parser : IDataToText
        {
            BankDataManager mgr = BankDataManager.GetNewManager();
            string?[] fs = GetFileNames<Parser>(fileName);

            // Получаем списки объектов.
            List<BankAccount> accounts =
                ListParser<Parser>.Import<BankAccount>(fs[0]).ToList();
            List<FinanceOperation> operations =
                ListParser<Parser>.Import<FinanceOperation>(fs[1]).ToList();
            List<Category> categories =
                ListParser<Parser>.Import<Category>(fs[2]).ToList();
            // Пытаемся их добавить (внутри происходит проверка).
            var services = CompositionRoot.Services;
            mgr.ImportData(services.GetRequiredService<AccountFactory>(), accounts);
            mgr.ImportData(services.GetRequiredService<CategoryFactory>(), categories);
            mgr.ImportData(services.GetRequiredService<FinanceOperationFactory>(), operations);
            return mgr;
        }

        public static void Export<Parser>(BankDataManager mgr, string? fileName = null, string extension = ".txt") where Parser : IDataToText
        {
            var rep = mgr.GetRepository();
            string?[] fs = GetFileNames<Parser>(fileName);

            ListParser<Parser>.Export<BankAccount>(rep.BankAccounts, fs[0]);
            ListParser<Parser>.Export<FinanceOperation>(rep.FinanceOperations, fs[1]);
            ListParser<Parser>.Export<Category>(rep.Categories, fs[2]);
        }
    }
}
