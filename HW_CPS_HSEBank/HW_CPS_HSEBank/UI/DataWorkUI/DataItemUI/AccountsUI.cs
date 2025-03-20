using HW_CPS_HSEBank.Commands.DataCommands;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.UI.MenuUtils;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI
{
    /// <summary>
    /// Класс добавления/изменения/удаления аккаунтов. Класс использует DataCommands.
    /// </summary>
    public class AccountsUI : IDataItemUI
    {
        private IServiceProvider services = CompositionRoot.Services;

        public string Title { get => "аккаунт"; }

        /// <summary>
        /// Метод создает список инициализации на основе данных от пользователя.
        /// </summary>
        /// <param name="message">Сообщение пользователю</param>
        /// <returns>Список инициализации</returns>
        private object[]? GetInitList(string message)
        {
            Console.WriteLine(message);
            string? name = UtilsUI.GetUserString("Введите название аккаунта.");
            if (name == null) return null;
            // Пользователь не имеет права добавлять аккаунт с не пустым балансом.
            // Чтобы добавить баланс, нужно создать соответсвующую операцию.
            return new object[] { name, (decimal)0 };
        }

        /// <summary>
        /// Метод добавления аккаунта с данными от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool AddItem()
        {
            Console.Clear();
            var initList = GetInitList("Добавление аккаунта:");
            if (initList == null) return false;

            try
            {
                var addAccount = new AddCommand<AccountFactory, BankAccount>(initList);
                addAccount.Execute();
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при добавлении аккаунта.");
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            Console.WriteLine("Данные успешно добавлены.");
            Console.ReadLine();

            return true;
        }

        /// <summary>
        /// Метод изменения аккаунта с данными от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool ChangeItem()
        {
            Console.Clear();
            Console.WriteLine("Изменить аккаунт:");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id аккаунта (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetAccountById(id) == null) { Console.WriteLine("Аккаунт не найден."); }
                else
                {
                    var initList = GetInitList("Изменение аккаунта:");
                    if (initList == null) return false;

                    var list = new object[1 + initList.Length];
                    // id нужен для нахождения элемента в системе.
                    list[0] = id;
                    initList.CopyTo(list, 1);
                    var changeAccount = new ChangeCommand<BankAccount>(list);
                    changeAccount.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при изменении аккаунта.");
                Console.WriteLine(ex);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            Console.WriteLine("Данные успешно изменены.");
            Console.ReadLine();

            return true;
        }

        /// <summary>
        /// Метод удаления аккаунта с данными от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem()
        {
            Console.Clear();
            Console.WriteLine("Удалить аккаунт:");
            Console.WriteLine("Удаление аккаунта повлечет удаление всех связанных с ним операций!");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id аккаунта (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetAccountById(id) == null) { Console.WriteLine("Аккаунт не найден."); }
                else
                {
                    var deleteAccount = new DeleteCommand<BankAccount>(new object[] { id });
                    deleteAccount.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при удалении аккаунта.");
                Console.WriteLine(ex);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }
            Console.WriteLine("Данные успешно удалены.");
            Console.ReadLine();

            return true;
        }

        /// <summary>
        /// Не имплементированно
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool FindItem()
        {
            Console.Clear();
            Console.WriteLine("Найти аккаунт:");
            int bankAccountId;
            do { bankAccountId = UtilsUI.GetReqUserNumber<int>("Введите id аккаунта (положительное число)."); } while (0 > bankAccountId);

            var mgr = services.GetRequiredService<BankDataManager>();
            if (mgr.GetAccountById(bankAccountId) == null) { Console.WriteLine("Аккаунт не найден."); }
            else
            {
                throw new NotImplementedException();
            }

            return true;
        }
    }
}
