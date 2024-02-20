// Пронюшкин Радомир БПИ234-1 КДЗ-3-2 Вариант 15
using DataProcessing.Objects;

namespace MainProgram
{
    /// <summary>
    /// Класс основной программы. Работает с пользователем.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Основной метод программы.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Главный цикл программы.
            bool program = true;
            List<Patient>? patients = null;
            Dictionary<int, Doctor>? doctors = null;
            while (program)
            {
                // Главное меню.
                Console.Clear();
                Console.WriteLine("Выберите опцию из предложенного списка:");
                Console.WriteLine("1) Считать данные.");
                Console.WriteLine("2) Сортировка.");
                Console.WriteLine("3) Отредактировать объект.");
                Console.WriteLine("4) Вывести данные.");
                Console.WriteLine("5) Выход из программы.");

                // Выбор пользователя.
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        // Считывание данных.
                        MainUI.GetData(out patients, out doctors);
                        break;
                    case ConsoleKey.D2:
                        // Сортировка.
                        MainUI.SortData(patients);
                        break;
                    case ConsoleKey.D3:
                        // Редактирование объекта.
                        MainUI.ChangeObject(patients, doctors);
                        break;
                    case ConsoleKey.D4:
                        // Вывод данных.
                        MainUI.WriteData(patients);
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