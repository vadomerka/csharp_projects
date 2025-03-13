using System.Globalization;

namespace HW_CPS_HSEBank.UI
{
    public static class UtilsUI
    {
        public static void WriteMenuOptions(List<MenuItem> options, string title = "")
        {
            Console.Clear();
            if (title != "") Console.WriteLine(title);
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]._title}.");
            }

            Console.WriteLine("чтобы выбрать действие, нажмите соответсвующую клавишу");
            Console.WriteLine("(для возвращения нажмите [BackSpace])");
        }

        public static bool MakeMenu(List<MenuItem> options, string title = "")
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
                else if (key == (int)ConsoleKey.Backspace || key == (int)ConsoleKey.Escape)
                {
                    return true;
                }
                else
                {
                    continue;
                }
                if (key >= options.Count) continue;
                if (!options[key]._func()) { return false; };
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
            return input == ConsoleKey.Y;
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
