using HW_CPS_HSEBank.Commands.DataCommands;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using HW_CPS_HSEBank.UI.MenuUtils;
using Microsoft.Extensions.DependencyInjection;

namespace HW_CPS_HSEBank.UI.DataWorkUI.DataItemUI
{
    /// <summary>
    /// Класс добавления/изменения/удаления категорий
    /// </summary>
    public class CategoriesUI : IDataItemUI
    {
        private IServiceProvider services = CompositionRoot.Services;
        public string Title { get => "категорию"; }

        /// <summary>
        /// Метод создает список инициализации на основе данных от пользователя.
        /// </summary>
        /// <param name="message">Сообщение пользователю</param>
        /// <returns>Список инициализации</returns>
        private object[]? GetInitList(string message)
        {
            Console.WriteLine(message);
            string? name = UtilsUI.GetUserString("Введите название новой категории.");
            if (name == null) return null;
            return new object[] { name };
        }

        /// <summary>
        /// Метод добавления категории с данными от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool AddItem()
        {
            Console.Clear();

            var initList = GetInitList("Добавление категории:");
            if (initList == null) return false;

            try
            {
                var addCommand = new AddCommand<CategoryFactory, Category>(initList);
                addCommand.Execute();

                Console.WriteLine("Данные успешно добавлены.");
            } 
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка при добавлении категории.");
            }

            Console.ReadLine();

            return true;
        }

        /// <summary>
        /// Метод изменения категории с данными от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool ChangeItem()
        {
            Console.Clear();
            Console.WriteLine("Изменить категорию:");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id категории (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetCategoryById(id) == null) { Console.WriteLine("Категория не найдена."); }
                else
                {
                    var initList = GetInitList("Изменение категории:");
                    if (initList == null) return false;

                    var list = new object[1 + initList.Length];
                    list[0] = id;
                    initList.CopyTo(list, 1);
                    var change = new ChangeCommand<Category>(list);
                    change.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при изменении категории.");
                Console.WriteLine(ex);
                return UtilsUI.GetUserBool("Попробовать снова? y/n");
            }

            Console.WriteLine("Данные успешно изменены.");
            Console.ReadLine();

            return true;
        }

        /// <summary>
        /// Метод удаления аккаунта с категории от пользователя.
        /// </summary>
        /// <returns></returns>
        public bool DeleteItem()
        {
            Console.Clear();
            Console.WriteLine("Удалить категорию:");
            Console.WriteLine("Удаление категории повлечет удаление всех связанных с ней операций!");
            int id;
            do { id = UtilsUI.GetReqUserNumber<int>("Введите id категории (положительное число)."); } while (0 > id);

            var mgr = services.GetRequiredService<BankDataManager>();
            try
            {
                if (mgr.GetCategoryById(id) == null) { Console.WriteLine("Категория не найдена."); }
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
