using HW_CPS_HSEBank.Commands.AccountCommands;
using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.Data.Factories;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace HW_CPS_HSEBank.UI
{
    public class BankUI
    {
        private static IServiceProvider services = CompositionRoot.Services;

        public static void Menu()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Работа с данными", DataWorkMenu),
                    new MenuItem("Экспортировать данные", DataParserUI.ExportData),
                    new MenuItem("Импортировать данные", DataParserUI.ImportData),
                    new MenuItem("Выход", Exit)
                };
            UtilsUI.MakeMenu(mainOptions);
        }

        private static bool DataWorkMenu() {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Создать аккаунт", AddAccount),
                    new MenuItem("Создать категорию", AddCategory),
                    new MenuItem("Произвести финансовую операцию", AddFinanceOperation)
                };
            return UtilsUI.MakeMenu(mainOptions);
        }

        

        private static bool AddAccount()
        {
            Console.Clear();
            Console.WriteLine("Добавление аккаунта:");
            string? name = UtilsUI.GetUserString("Введите имя пользователя.");
            if (name == null) return true;
            decimal balance;
            do
            {
                balance = UtilsUI.GetReqUserNumber<decimal>("Введите баланс пользователя (положительное число).");
            } while (0 > balance);

            AddAccountCommand addAccount = new(name, balance);
            addAccount.Execute();

            return true;
        }

        private static bool AddFinanceOperation()
        {
            Console.Clear();
            Console.WriteLine("Добавление финансовой операции:");
            Console.WriteLine("Финансовые операции могут проиcходить только между существующими пользователями.");
            var options = new List<string>() { "доход", "расход" };
            string type = UtilsUI.GetReqUserString("Введите тип операции (доход или расход).", options);
            int bankAccountId;
            do { bankAccountId = UtilsUI.GetReqUserNumber<int>("Введите id пользователя (положительное число)."); } while (0 > bankAccountId);
            decimal amount;
            do { amount = UtilsUI.GetReqUserNumber<decimal>("Введите сумму операции (положительное число)."); } while (0 > amount);

            DateTime date = UtilsUI.GetReqUserDateTime("Введите время операции");

            string? description = UtilsUI.GetUserString("Введите описание операции (опционально).");
            int categoryId;
            do { categoryId = UtilsUI.GetReqUserNumber<int>("Введите id категории (положительное число)."); } while (0 > categoryId);

            // Todo: commands?
            IServiceProvider services = CompositionRoot.Services;
            var factory = services.GetRequiredService<FinanceOperationFactory>();
            var mb = services.GetRequiredService<BankDataRepository>();
            mb.AddFinanceOperation(factory
                .Create(type, bankAccountId, amount, date, description ??= "", categoryId));

            return true;
        }

        private static bool AddCategory() {
            Console.Clear();
            Console.WriteLine("Добавление категории:");

            string name = UtilsUI.GetReqUserString("Введите название новой категории.");

            // Todo: commands?
            IServiceProvider services = CompositionRoot.Services;
            var factory = services.GetRequiredService<CategoryFactory>();
            var mb = services.GetRequiredService<BankDataRepository>();
            mb.AddCategory(factory.Create(name));

            return true;
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
