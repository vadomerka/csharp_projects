using HW_CPS_HSEBank.Commands;
using HW_CPS_HSEBank.Data;
using HW_CPS_HSEBank.Data.Factories;
using HW_CPS_HSEBank.Json;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace HW_CPS_HSEBank
{
    public class BankUI
    {
        private delegate bool UIFunc();
        struct MenuItem
        {
            public string _title;
            public UIFunc _func;

            public MenuItem(string title, UIFunc func)
            {
                _title = title;
                _func = func;
            }
        }

        public static void Menu()
        {
            List<MenuItem> mainOptions = new List<MenuItem>
                {
                    new MenuItem("Добавить аккаунт", AddAccount),
                    new MenuItem("Экспортировать данные", ExportData),
                    new MenuItem("Импортировать данные", ImportData),
                    new MenuItem("Выход", Exit)
                };
            MakeMenu(mainOptions);
        }


        private static void WriteMenuOptions(List<MenuItem> options, string title = "")
        {
            Console.Clear();
            if (title != "") Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]._title}.");
            }

            Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
        }

        private static void MakeMenu(List<MenuItem> options)
        {
            while (true)
            {
                WriteMenuOptions(options);

                int key = (int)Console.ReadKey().Key;
                Console.WriteLine();

                int n1 = (int)ConsoleKey.NumPad1;
                int n2 = (int)ConsoleKey.NumPad9;
                int d1 = (int)ConsoleKey.D1;
                int d2 = (int)ConsoleKey.D9;
                if (n1 <= key && key <= n2)
                {
                    key -= n1;
                }
                else if (d1 <= key && key <= d2)
                {
                    key -= d1;
                }
                else
                {
                    continue;
                }
                if (key >= options.Count) continue;
                if (!options[key]._func()) { return; };
            }
        }


        public static bool AddAccount() {
            //List<MenuItem> mainOptions = new List<MenuItem>
            //    {
            //        new MenuItem("Добавить default аккаунт", AddAccount),
            //        new MenuItem("Выход", Exit)
            //    };
            //MakeMenu(mainOptions);
            Console.Clear();
            Console.WriteLine("Впишите имя пользователя.");
            string? name = Console.ReadLine();
            if (name == null) return true;

            AddAccountCommand addAccount = new(name, 0);
            addAccount.Execute();

            return true;
        }

        public static bool ExportData() {
            Console.Clear();
            string fileName = Console.ReadLine();
            Console.WriteLine($"Данные экспортируются в {fileName}.");
            //string? name = Console.ReadLine();
            //if (name == null) return true;

            _ = JsonDataParser.ExportDataAsync(fileName);
            Console.WriteLine("Данные были записаны.");
            Console.ReadLine();

            return true;
        }

        public static bool ImportData()
        {
            Console.Clear();
            string fileName = Console.ReadLine();
            Console.WriteLine($"Данные импортируются из {fileName}.");
            //string? name = Console.ReadLine();
            //if (name == null) return true;

            BankDataRepository? newrep = JsonDataParser.ImportData(fileName);
            if (newrep == null) return true;

            IServiceProvider services = CompositionRoot.Services;
            var oldrep = services.GetRequiredService<BankDataRepository>();
            oldrep.Swap(newrep);
            oldrep.AddAccount(new BankAccount(4, "новичок морячок", 100000));

            Console.WriteLine("Данные были считаны.");
            Console.ReadLine();

            return true;
        }

        public static bool Exit()
        {
            Console.WriteLine("Выход из программы...");
            return false;
        }
    }
}
