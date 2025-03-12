using HW_CPS_HSEBank.Commands;

namespace HW_CPS_HSEBank.UI
{
    public class BankUI
    {
        public delegate bool UIFunc();
        private static IServiceProvider services = CompositionRoot.Services;

        public struct MenuItem
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
                    new MenuItem("Экспортировать данные", DataParserUI.ExportData),
                    new MenuItem("Импортировать данные", DataParserUI.ImportData),
                    new MenuItem("Выход", Exit)
                };
            MakeMenu(mainOptions);
        }

        public static void WriteMenuOptions(List<MenuItem> options, string title = "")
        {
            Console.Clear();
            if (title != "") Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]._title}.");
            }

            Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
        }

        public static void MakeMenu(List<MenuItem> options, string title = "")
        {
            if (title != "") Console.WriteLine(title);
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

        public static string? GetUserString(string message = "", bool clear = false)
        {
            if (clear) Console.Clear();
            if (message != "") Console.WriteLine(message);
            string? input = Console.ReadLine();
            return input;
        }

        public static string GetReqUserString(string message = "", bool clear = false)
        {
            if (clear) Console.Clear();
            string? input = null;
            while (input == null)
            {
                if (message != "") Console.WriteLine(message);
                input = Console.ReadLine();
            }
            return input;
        }

        public static T? GetUserNumber<T>(string message = "", bool clear = false)
        {
            if (clear) Console.Clear();
            if (message != "") Console.WriteLine(message);
            string? input = Console.ReadLine();
            return (T?)Convert.ChangeType(input, typeof(T));
        }

        public static T GetReqUserNumber<T>(string message = "", bool clear = false)
        {
            if (clear) Console.Clear();
            //T? res;
            //do
            //{
            //    string? input = Console.ReadLine();
            //    res = (T?)Convert.ChangeType(input, typeof(T));
            //} while (res == null);
            //return res;
            T? res;
            while (true)
            {
                try
                {
                    if (message != "") Console.WriteLine(message);
                    res = (T?)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    if (res == null) throw new Exception();
                    return res;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка ввода!");
                    continue;
                }
            }
        }

        private static bool AddAccount()
        {
            string? name = GetUserString("Введите имя пользователя.", true);
            if (name == null) return true;
            decimal balance;
            do
            {
                balance = GetReqUserNumber<decimal>("Введите баланс пользователя (положительное число).");
            } while (0 > balance);

            AddAccountCommand addAccount = new(name, balance);
            addAccount.Execute();

            return true;
        }

        public static bool Exit()
        {
            Console.WriteLine("Выход из программы...");
            return false;
        }
    }
}
