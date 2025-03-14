using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.Commands.DataCommands;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI
{
    public class FinanceOperationsUI : IDataItemUI
    {
        private IServiceProvider services = CompositionRoot.Services;
        public string Title { get => "операцию"; }

        private object[]? GetInitList(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Финансовые операции могут проиcходить только между существующими пользователями.");
            var options = new List<string>() { "доход", "расход" };
            string type = UtilsUI.GetReqUserString("Введите тип операции (доход или расход).", options);
            int bankAccountId;
            do { bankAccountId = UtilsUI.GetReqUserNumber<int>("Введите id аккаунта (положительное число)."); } while (0 > bankAccountId);
            decimal amount;
            do { amount = UtilsUI.GetReqUserNumber<decimal>("Введите сумму операции (положительное число)."); } while (0 > amount);

            DateTime date = UtilsUI.GetReqUserDateTime("Введите время операции");

            string? description = UtilsUI.GetUserString("Введите описание операции (опционально).");
            int categoryId;
            do { categoryId = UtilsUI.GetReqUserNumber<int>("Введите id категории (положительное число)."); } while (0 > categoryId);
            return new object[] { type, bankAccountId, amount, date, description ??= "", categoryId };
        }

        public bool AddItem()
        {
            Console.Clear();

            var initList = GetInitList("Добавление финансовой операции:");
            if (initList == null) return false;

            try
            {
                var addCommand = new AddFinanceOperation<FinanceOperationFactory, FinanceOperation>(initList);
                addCommand.Execute();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Операция недействительна.");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при выполнении операции.");
            }

            return true;
        }

        public bool ChangeItem()
        {
            Console.Clear();
            Console.WriteLine("Изменить операцию:");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id операции (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetCategoryById(id) == null) { Console.WriteLine("Операция не найдена."); }
                else
                {
                    var initList = GetInitList("Изменение операции:");
                    if (initList == null) return false;

                    var list = new object[1 + initList.Length];
                    list[0] = id;
                    initList.CopyTo(list, 1);
                    var change = new ChangeCommand<FinanceOperation>(list);
                    change.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при изменении операции.");
                Console.WriteLine(ex);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            Console.WriteLine("Данные успешно изменены.");
            Console.ReadLine();

            return true;
        }

        public bool DeleteItem()
        {
            Console.Clear();
            Console.WriteLine("Удалить категорию:");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id категории (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetAccountById(id) == null) { Console.WriteLine("Категория не найдена."); }
                else
                {
                    var deleteAccount = new DeleteCommand<Category>(new object[] { id });
                    deleteAccount.Execute();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при удалении категории.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            Console.WriteLine("Данные успешно удалены.");
            Console.ReadLine();

            return true;
        }

        public bool FindItem()
        {
            throw new NotImplementedException();
        }
    }
}
