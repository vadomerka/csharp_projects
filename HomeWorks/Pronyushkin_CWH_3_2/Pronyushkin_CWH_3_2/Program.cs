// Пронюшкин Радомир БПИ234-1 КДЗ-3-1 Вариант 17
using DataProcessing;

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
                Console.WriteLine("4) Выход из программы.");

                // Выбор пользователя.
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        // Считывание данных.
                        MainUI.GetData(out patients, out doctors);
                        
                        /*
                        Console.WriteLine("Пациенты");
                        foreach (Patient? p in patients)
                        {
                            Console.WriteLine(p);
                        }
                        Console.WriteLine("Доктора");
                        foreach (KeyValuePair<int, Doctor> item in doctors)
                        {
                            Console.WriteLine(item.Value);
                        }
                        */
                        break;
                    case ConsoleKey.D2:
                        // Сортировка.
                        // data = MainUI.FilterData(data);
                        break;
                    case ConsoleKey.D3:
                        // Отредактирование объекта.
                        MainUI.ChangeObject(patients, doctors);
                        break;
                    case ConsoleKey.D4:
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