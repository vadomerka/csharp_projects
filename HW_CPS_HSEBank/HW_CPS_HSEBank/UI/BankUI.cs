using HW_CPS_HSEBank.UI.DataWorkUI;
using HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI
{
    /// <summary>
    /// Класс для меню управления банком.
    /// </summary>
    public class BankUI
    {
        private static IServiceProvider s = CompositionRoot.Services;

        /// <summary>
        /// Главное меню.
        /// </summary>
        public static void Menu()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Работа с данными", DataWorkMenu),
                    new MenuItem("Анализ данных", DataAnalyze),
                    new MenuItem("Экспортировать данные", DataParserUI.ExportData),
                    new MenuItem("Импортировать данные", DataParserUI.ImportData),
                    new MenuItem("Статистика", UtilsUI.ExitNotImplemented), // Не успеваю.
                    new MenuItem("Выход", UtilsUI.Exit)
                };
            UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataWorkMenu() {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Работа с аккаунтами", DataMenu<AccountsUI>),
                    new MenuItem("Работа с операциями", DataMenu<FinanceOperationsUI>),
                    new MenuItem("Работа с категориями", DataMenu<CategoriesUI>),
                    new MenuItem("Пересчет данных", AnalyzeUI.RecountUI)
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        /// <summary>
        /// Работа с списком данных.
        /// </summary>
        /// <typeparam name="TData">IDataItemUI</typeparam>
        /// <returns></returns>
        private static bool DataMenu<TData>() where TData : IDataItemUI {
            IDataItemUI mc = s.GetRequiredService<TData>();
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem($"Добавить {mc.Title}", mc.AddItem),
                    new MenuItem($"Удалить {mc.Title}", mc.DeleteItem),
                    new MenuItem($"Изменить {mc.Title}", mc.ChangeItem),
                    //new MenuItem($"Найти {mc.Title}", mc.AddItem) // Не успеваю.
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataAnalyze() {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem($"Подсчет разницы доходов и расходов за выбранный период", 
                    AnalyzeUI.AnalyzeIncomeExpenceDifference),
                    new MenuItem($"Группировка операций по категориям", AnalyzeUI.GroupOperationsViaCategory),
                };
            return UtilsUI.MakeMenu(mainOptions);
        }
    }
}
