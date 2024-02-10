// Пронюшкин Радомир БПИ234-1 КДЗ-2-3
using ClassLibrary;

namespace Pronyushkin_CWH
{
    public class MyFileReader
    {
        /// <summary>
        /// Главное меню программы. Реализовано через цикл, с повтором решения. Все методы подключены через MainUI.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Вы запустили программу для работы с csv файлами.");

            Item[] fileData = null;
            Item[] changedData = null;

            bool program = true;
            while (program)
            {
                // Главное меню.
                Console.Clear();
                Console.WriteLine("Выберите опцию из предложенного списка:");
                Console.WriteLine("1) Открыть *.csv файл.");
                Console.WriteLine("2) Просмотреть первые или последние N строк файла.");
                Console.WriteLine("3) Произвести сортировку по столбцу \"Фамилия\".");
                Console.WriteLine("4) Произвести фильтрацию по значениям в столбце.");
                Console.WriteLine("5) Выйти из программы.");
                
                // Выбор методов из меню через нажатие цифр на клавиатуре.
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine();
                        fileData = MainUI.OpenNewFile();
                        Console.WriteLine("Введите \"Enter\" для возврата в меню.");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine();
                        MainUI.WriteFileData(fileData);
                        Console.WriteLine("Введите \"Enter\" для возврата в меню.");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine();
                        changedData = MainUI.SortFileData(fileData);
                        if (changedData != null) MainUI.SaveData(changedData);
                        Console.WriteLine("Введите \"Enter\" для возврата в меню.");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine();
                        changedData = MainUI.FilterFileData(fileData);
                        if (changedData != null) MainUI.SaveData(changedData);
                        Console.WriteLine("Введите \"Enter\" для возврата в меню.");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine();
                        Console.Write("Выбран выход из программы.");
                        program = false;
                        break;
                }
            }
        }
    }
}