internal class MainMenuHelper
{
    public static void MainMenu(ref bool menu, string[] array, string[][] splittedarray)
    {
        while (menu)
        {
            Console.WriteLine("Выберите опцию из предложенного списка:");
            Console.WriteLine("1) Произвести выборку значений по StationStart.");
            Console.WriteLine("2) Произвести выборку значений по StationEnd.");
            Console.WriteLine("3) Произвести выборку значений по StationStart и StationEnd.");
            Console.WriteLine("4) Отсортировать таблицу по TimeStart в порядке увеличения времени.");
            Console.WriteLine("5) Отсортировать таблицу по TimeEnd в порядке увеличения времени.");
            Console.WriteLine("6) Выйти из программы.");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    Console.Write("Введите значение StationStart: ");
                    string searchValue = Console.ReadLine();
                    string res = DataProcessing.Selection(searchValue, splittedarray, 1);
                    CsvProcessing.PrintTable(res);
                    if (res != null)
                    {
                        Console.Write("Введите полный путь к файлу, куда хотите сохранить данные: ");
                        string path = Console.ReadLine();
                        CsvProcessing.Write(res, path);
                    }
                    Console.ReadLine();
                    return;
                case ConsoleKey.D2:
                    Console.Write("Введите значение StationEnd: ");
                    searchValue = Console.ReadLine();
                    res = DataProcessing.Selection(searchValue, splittedarray, 1);
                    CsvProcessing.PrintTable(res);
                    if (res != null)
                    {
                        Console.Write("Введите полный путь к файлу, куда хотите сохранить данные: ");
                        string path = Console.ReadLine();
                        CsvProcessing.Write(res, path);
                    }
                    Console.ReadLine();
                    return;
                case ConsoleKey.D3:
                    Console.Write("Введите значение StationStart: ");
                    searchValue = Console.ReadLine();
                    Console.Write("Введите значение StationEnd: ");
                    string searchValueS = Console.ReadLine();
                    res = DataProcessing.Selection(searchValue, searchValueS, splittedarray);
                    CsvProcessing.PrintTable(res);
                    if (res != null)
                    {
                        Console.Write("Введите полный путь к файлу, куда хотите сохранить данные: ");
                        string path = Console.ReadLine();
                        CsvProcessing.Write(res, path);
                    }
                    Console.ReadLine();
                    return;
                case ConsoleKey.D4:
                    DataProcessing.Sorted(array, 5);
                    Console.ReadLine();
                    return;
                case ConsoleKey.D5:
                    DataProcessing.Sorted(array, 4);
                    Console.ReadLine();
                    return;
                case ConsoleKey.D6:
                    menu = false;
                    return;
                default:
                    Console.Write("Некорректный ввод, повторите попытку: ");
                    continue;
            }
        }
    }
}

bool menu = true;
do
{
    Console.Clear();
    MainMenuHelper.MainMenu(ref menu, array, splittedarray);
} while (menu == true);
