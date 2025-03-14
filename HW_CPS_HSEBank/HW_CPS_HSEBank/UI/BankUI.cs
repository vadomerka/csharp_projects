using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.DataLogic;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.UI.DataWorkUI;
using HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HW_CPS_HSEBank.UI
{
    public class BankUI
    {
        private static IServiceProvider s = CompositionRoot.Services;

        public static void Menu()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Работа с данными", DataWorkMenu),
                    new MenuItem("Анализ данных", DataAnalyze),
                    new MenuItem("Экспортировать данные", DataParserUI.ExportData),
                    new MenuItem("Импортировать данные", DataParserUI.ImportData),
                    new MenuItem("Статистика", Exit),
                    new MenuItem("Выход", Exit)
                };
            UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataWorkMenu() {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Работа с аккаунтами", DataMenu<AccountsUI>),
                    new MenuItem("Работа с операциями", DataMenu<FinanceOperationsUI>),
                    new MenuItem("Работа с категориями", DataMenu<CategoriesUI>)
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataMenu<TData>() where TData : IDataItemUI {
            IDataItemUI mc = s.GetRequiredService<TData>();
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem($"Добавить {mc.Title}", mc.AddItem),
                    new MenuItem($"Удалить {mc.Title}", mc.DeleteItem),
                    new MenuItem($"Изменить {mc.Title}", mc.ChangeItem),
                    //new MenuItem($"Изменить {mc.Title}", mc.AddItem)
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataAnalyze() {
            //IDataItemUI mc = s.GetRequiredService<TData>();
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem($"Подсчет разницы доходов и расходов за выбранный период.", AnalyzeUI.AnalyzeIncomeExpenceDifference),
                    //new MenuItem($"Удалить {mc.Title}", mc.DeleteItem),
                    //new MenuItem($"Изменить {mc.Title}", mc.ChangeItem),
                    //new MenuItem($"Изменить {mc.Title}", mc.AddItem)
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        public static bool MenuReturn()
        {
            return true;
        }
        public static bool Exit()
        {
            Console.WriteLine("Выход из программы...");
            return false;
        }
    }
}
