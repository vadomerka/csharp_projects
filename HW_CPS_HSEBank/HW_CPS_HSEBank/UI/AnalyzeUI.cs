using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.DataLogic.DataAnalyze;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.UI.DataWorkUI;

namespace HW_CPS_HSEBank.UI
{
    public static class AnalyzeUI
    {
        private static IServiceProvider services = CompositionRoot.Services;

        /// <summary>
        /// Метод для пересчета.
        /// </summary>
        /// <returns></returns>
        public static bool RecountUI()
        {
            Console.Clear();
            Console.WriteLine("Пересчет балансов пользователей.");

            var mgr = services.GetRequiredService<BankDataManager>();

            try
            {
                var res = BankDataAnalyzer.FinanceOperationsRecount(mgr);

                if (UtilsUI.GetUserBool("Вывести результаты в консоль? y/n")) 
                {
                    DataParserUI.ExportDataToConsole<CsvDataParser>(res, clearFlag: false, outro: false);
                }

                if (UtilsUI.GetUserBool("Сохранить результаты? y/n")) { mgr.Save(res); }

                Console.WriteLine("Пересчет завершен.");
            }
            catch (ArgumentNullException)
            {
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            catch (Exception)
            {
                Console.WriteLine("Пересчет завершился ошибкой.");
                Console.WriteLine("Введите другие данные.");
            }

            Console.ReadLine();

            return true;
        }

        public static bool AnalyzeIncomeExpenceDifference()
        {
            Console.Clear();
            Console.WriteLine("Подсчет разницы доходов и расходов за выбранный период.");

            var mgr = services.GetRequiredService<BankDataManager>();
            int accId;
            do { accId = UtilsUI.GetUserNumber<int>("Введите id аккаунта."); } while (accId < 0);

            DateTime? date1 = UtilsUI.GetUserDateTime("Введите время начала (enter чтобы пропустить)");
            DateTime? date2 = UtilsUI.GetUserDateTime("Введите время конца (enter чтобы пропустить)");

            try
            {
                decimal p = BankDataAnalyzer.AccountMoneySum(mgr, accId, "доход", date1, date2);
                decimal m = BankDataAnalyzer.AccountMoneySum(mgr, accId, "расход", date1, date2);
                decimal res = p - m;
                Console.WriteLine($"Общая сумма доходов {p}.");
                Console.WriteLine($"Общая сумма расходов {m}.");
                Console.WriteLine($"Результат разности: {res}.");
            } catch (ArgumentNullException)
            {
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            Console.ReadLine();

            return true;
        }

        public static bool GroupOperationsViaCategory() {
            Console.Clear();
            Console.WriteLine("Группировка операций по категориям.");

            var mgr = services.GetRequiredService<BankDataManager>();

            try
            {
                var ops = mgr.GetRepository().FinanceOperations;
                var outList = BankDataAnalyzer.FinanceOperationsSortByCategory(ops);

                DataParserUI.ExportDataToConsole<CsvDataParser, FinanceOperation>(outList, clearFlag: false);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            Console.ReadLine();

            return true;
        }
    }
}
