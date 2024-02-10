// Пронюшкин Радомир БПИ234-1 КДЗ-3-1 Вариант 17
using DataProcessing;
using Program;

namespace MainProgram
{
    /// <summary>
    /// Класс основной программы. Работает с пользователем.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Метод Main - основной метод программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Список объектов.
            List<Player> data = null;
            // Главный цикл программы.
            bool program = true;
            while (program) {
                // Главное меню.
                Console.Clear();
                Console.WriteLine("Выберите опцию из предложенного списка:");
                Console.WriteLine("1) Считать данные.");
                Console.WriteLine("2) Фильтрация.");
                Console.WriteLine("3) Сортировка.");
                Console.WriteLine("4) Вывод (сохранение) данных.");
                Console.WriteLine("5) Выход из программы.");
                // Выбор пользователя.
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        // Считывание данных.
                        data = MainUI.ReadData();
                        break;
                    case ConsoleKey.D2:
                        // Фильтрация.
                        data = MainUI.FilterData(data);
                        break;
                    case ConsoleKey.D3:
                        // Сортировка.
                        data = MainUI.SortData(data);
                        break;
                    case ConsoleKey.D4:
                        // Сохранение.
                        MainUI.SaveData(data, false);
                        break;
                    case ConsoleKey.D5:
                        // Выход из основного цикла программы.
                        program = false;
                        Console.WriteLine("Завершение работы программы..");
                        break;
                }
                if (program)
                {
                    // Задержка между действиями.
                    Console.WriteLine("Введите 'Enter', чтобы продолжить.");
                    Console.ReadLine();
                }
            }
        }
    }
}