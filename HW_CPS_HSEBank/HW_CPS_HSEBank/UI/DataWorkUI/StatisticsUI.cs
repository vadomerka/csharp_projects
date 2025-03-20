using HW_CPS_HSEBank.Statistics;
using HW_CPS_HSEBank.UI.MenuUtils;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI.DataWorkUI
{
    public static class StatisticsUI
    {
        public static bool MenuStatisticsExport() {
            Console.Clear();
            //string fileName = UtilsUI.GetReqUserString("Введите путь к файлу.");
            string fileName = "menu_statistics";
            Console.WriteLine($"Данные экспортируются в {fileName}.");

            try
            {
                var services = CompositionRoot.Services;
                services.GetRequiredService<MenuStatistics>().WriteToFile(fileName);
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка записи в файл.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return true;
        }
    }
}
