using System.Globalization;

namespace HW_CPS_HSEBank.UI.MenuUtils
{
    /// <summary>
    /// Класс содержит базовые функции для получения данных от пользователя.
    /// </summary>
    public static class UtilsUI
    {
        /// <summary>
        /// Метод выводит пункты меню.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="title"></param>
        private static void WriteMenuOptions(List<IMenuCommand> options, string title = "")
        {
            Console.Clear();
            if (title != "") Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i].Title}.");
            }

            Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
            Console.WriteLine("(для возвращения нажмите [BackSpace])");
        }

        /// <summary>
        /// Метод создания меню из пунктов меню.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool MakeMenu(List<IMenuCommand> options, string title = "")
        {
            if (title != "") Console.WriteLine(title);
            while (true)
            {
                // Вывод пунктов.
                WriteMenuOptions(options);

                // Считываем выбор.
                int key = (int)Console.ReadKey().Key;
                Console.WriteLine();

                // Обработка если пользователь выбрал пункт меню.
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
                // Выход.
                else if (key == (int)ConsoleKey.Backspace || key == (int)ConsoleKey.Escape) { return true; }
                else { continue; }

                if (key >= options.Count) continue;
                // Запуск метода.
                if (!options[key].Execute()) { return false; };
            }
        }

        public static string? GetUserString(string message = "", bool clear = false)
        {
            if (clear) Console.Clear();
            if (message != "") Console.WriteLine(message);
            string? input = Console.ReadLine();
            return input;
        }

        public static string GetReqUserString(string message = "", List<string>? choice = null, bool clear = false)
        {
            if (clear) Console.Clear();
            string? input = null;
            while (true)
            {
                if (message != "") Console.WriteLine(message);
                input = Console.ReadLine();
                if (input == null) continue;
                if (choice != null)
                {
                    if (!choice.Contains(input.ToLower().Trim())) { continue; }
                }
                break;
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
                catch (Exception)
                {
                    Console.WriteLine("Ошибка ввода!");
                    continue;
                }
            }
        }

        public static bool GetUserBool(string message = "y/n?", bool clear = false)
        {
            if (clear) Console.Clear();
            if (message != "") Console.WriteLine(message);
            var input = Console.ReadKey().Key;
            Console.WriteLine();
            return input == ConsoleKey.Y;
        }

        public static DateTime? GetUserDateTime(string message = "", string format = "yyyy.MM.dd hh:mm:ss")
        {
            DateTime? date;
            string userDateTime = GetReqUserString($"{message} [{format}].");
            try
            {
                date = DateTime.Parse(userDateTime, CultureInfo.InvariantCulture);
            }
            catch
            {
                date = null;
            }
            return date;
        }

        public static DateTime GetReqUserDateTime(string message = "", string format = "yyyy.MM.dd hh:mm:ss")
        {
            DateTime date;
            string userDateTime = GetReqUserString($"{message} [{format}].");

            while (!DateTime.TryParse(userDateTime, CultureInfo.InvariantCulture, out date))
            {
                userDateTime = GetReqUserString($"{message} [{format}].");
            };
            return date;
        }

        public static bool ExitNotImplemented()
        {
            Console.WriteLine("Не имплементировано");
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
