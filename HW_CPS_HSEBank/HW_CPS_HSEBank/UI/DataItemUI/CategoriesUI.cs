using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.Commands.DataCommands;
using HW_CPS_HSEBank.DataLogic.DataManagement;
using HW_CPS_HSEBank.DataLogic.DataModels;
using HW_CPS_HSEBank.DataLogic.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_CPS_HSEBank.UI.DataItemUI
{
    public class CategoriesUI : IDataItemUI
    {
        private IServiceProvider services = CompositionRoot.Services;
        public string Title { get => "категорию"; }

        private object[]? GetInitList(string message)
        {
            Console.WriteLine(message);
            string? name = UtilsUI.GetUserString("Введите название новой категории.");
            if (name == null) return null;
            return new object[] { name };
        }

        public bool AddItem()
        {
            Console.Clear();

            var initList = GetInitList("Добавление категории:");
            if (initList == null) return false;

            var addCommand = new AddCommand<CategoryFactory, Category>(initList);
            addCommand.Execute();

            return true;
        }

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
