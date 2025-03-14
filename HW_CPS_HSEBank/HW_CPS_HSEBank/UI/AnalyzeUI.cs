using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataParsing.DataParsers;
using HW_CPS_HSEBank.DataLogic.DataAnalyze;
using Microsoft.Extensions.DependencyInjection;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.DataLogic.DataModels;

namespace HW_CPS_HSEBank.UI
{
    public static class AnalyzeUI
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static bool AnalyzeIncomeExpenceDifference()
        {
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
            } catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            Console.ReadLine();

            return true;
        }
    }
}
