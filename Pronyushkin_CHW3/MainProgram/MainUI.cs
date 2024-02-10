using ClassLibrary;
using System.ComponentModel.Design;

namespace Pronyushkin_CWH
{
    /// <summary>
    /// Класс для работы с пользователем.
    /// </summary>
    public class MainUI
    {
        /// <summary>
        /// Метод получает путь до файла. Проверяет его. И возвращает данные в удобном для работы виде.
        /// </summary>
        /// <returns></returns>
        public static Item[] OpenNewFile()
        {
            Console.WriteLine("Введите название файла, который необходимо считать.");
            Console.WriteLine("Для выхода в меню введите exit.");
            string path = Console.ReadLine();
            // Реализована возможность выхода из программы на этапе ввода названия файла.
            while (path != "exit")
            {
                try
                {
                    // Если пользователь ввел файл неверного формата, возвращаем ошибку.
                    if (!path.EndsWith(".csv")) { throw new FormatException(); }

                    string[] fileData = CsvProcessing.Read(path);

                    Item[] fileLawers = DataProcessing.GetLawers(fileData);
                    Console.WriteLine("Данные успешно считаны.");
                    return fileLawers;
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Файл не найден. Повторите попытку.");
                    path = Console.ReadLine();
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Неверное расширение файла.");
                    path = Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Произошла непредвиденная ошибка. Повторите попытку.");
                    path = Console.ReadLine();
                }
            }
            // Если пользователь ввел "exit", возвращается null, вместо экземпляра CsvProcessing.
            // Это позволяет остановить бесконечный цикл программы в главном файле.
            return null;
        }

        /// <summary>
        /// enum Запись выбора сохраняет значение, введенное пользователем.
        /// </summary>
        private enum RecordChoice
        {
            Top,
            Bottom,
            Exit
        }

        /// <summary>
        /// Метод выводит список данных на экран в удобном формате.
        /// </summary>
        /// <param name="data"></param>
        public static void WriteFileData(Item[] data)
        {
            // Проверка на пустой список.
            if (data == null)
            {
                Console.WriteLine("Список адвокатов пуст.");
                return;
            }
            // Проверка на пустой список после фильтрации.
            else if (data.Length <= 1)
            {
                Console.WriteLine("Данные содержат только заголовок.");
                return;
            }

            Console.WriteLine("Введите номер варианта:");
            Console.WriteLine("0) Просмотреть первые N строк списка.");
            Console.WriteLine("1) Просмотреть последние N строк списка.");
            Console.WriteLine("2) Не просматривать список.");
            int choiceNum;
            bool input = false;
            bool choiceCycle = true;
            bool reverse = false;
            while (choiceCycle)
            {
                choiceCycle = false;
                input = int.TryParse(Console.ReadLine(), out choiceNum);
                if (!input || (choiceNum != 0 && choiceNum != 1 && choiceNum != 2))
                {
                    Console.WriteLine("Неверный ввод, повторите попытку.");
                    continue;
                }

                // Преобразуем ввод пользователя используя enum.
                RecordChoice recordChoice = (RecordChoice)choiceNum;
                switch (recordChoice)
                {
                    case RecordChoice.Top:
                        reverse = false;
                        break;
                    case RecordChoice.Bottom:
                        reverse = true;
                        break;
                    case RecordChoice.Exit:
                        Console.WriteLine();
                        Console.WriteLine("Список данных не будет выведен.");
                        return;
                        break;
                }
            }

            Console.WriteLine("Сколько строк вы хотите вывести?");
            // Подсчет максимального числа строк на экране.
            // Консоль может вывести около 9000 строк за раз, поэтому я ограничил его этим числом.
            int maxN = Math.Min(data.Length - 1, 9000);
            Console.WriteLine($"Введите натуральное число от 1 до {maxN}.");
            int n = 0;
            input = int.TryParse(Console.ReadLine(), out n);
            while (!input || n < 1 || n > maxN)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
                input = int.TryParse(Console.ReadLine(), out n);
            }
            // Вывод списка через библиотеку классов.
            DataProcessing.WriteItems(data, n, reverse);
        }

        /// <summary>
        /// Метод для сохранения обработанных данных.
        /// </summary>
        /// <param name="data"></param>
        public static void SaveData(Item[] data)
        {
            Console.WriteLine("Хотите ли вы сохранить результат? y/n");
            string input = Console.ReadLine().ToLower();
            if (input == "n" || input == "no" || input == "н" || input == "нет")
            {
                Console.WriteLine("Изменения не будут сохранены.");
                return; 
            }

            Console.WriteLine("Как вы хотите сохранить данные?");
            Console.WriteLine("1) Создать новый файл.");
            Console.WriteLine("2) Перезаписать содержимое существующего файла.");
            Console.WriteLine("3) Добавить данные к существующему файлу.");

            string path;
            bool choiceCycle = true;
            while (choiceCycle)
            {
                choiceCycle = false;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine();
                        Console.WriteLine("Введите название файла.");
                        path = Console.ReadLine();
                        while (true)
                        {
                            try
                            {
                                // Пользователь выбрал создание нового файла.
                                // Если файл уже существует - предупреждаем об этом пользователя.
                                if (File.Exists(path)) throw new ArgumentException();
                                // Если файл неверного формата - запрашиваем путь повторно.
                                if (!path.EndsWith(".csv")) throw new FormatException();

                                Console.WriteLine("Попытка записи...");
                                CsvProcessing.Write(data, path);
                                Console.WriteLine("Данные успешно записаны.");
                                break;
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Файл с таким именем уже существует. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Неверное расширение файла. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine("Произошла непредвиденная ошибка при загрузке в файл. Повторите попытку.");
                                path = Console.ReadLine();
                            }
                        }
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine();
                        Console.WriteLine("Введите название файла.");
                        path = Console.ReadLine();
                        while (true)
                        {
                            try
                            {
                                // Пользователь выбрал перезапись данных файла.
                                // Если файл не существует - предупреждаем об этом пользователя.
                                if (!File.Exists(path)) throw new ArgumentException();
                                if (!path.EndsWith(".csv")) throw new FormatException();

                                Console.WriteLine("Попытка записи...");
                                CsvProcessing.Write(data, path);
                                Console.WriteLine("Данные успешно записаны.");
                                break;
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Файл не найден. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Неверное расширение файла. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine("Произошла непредвиденная ошибка при загрузке в файл. Повторите попытку.");
                                path = Console.ReadLine();
                            }
                        }
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine();
                        Console.WriteLine("Введите название файла.");
                        path = Console.ReadLine();
                        while (true)
                        {
                            try
                            {
                                // Пользователь выбрал добавление данных к существующему файлу.
                                // Если файл не существует - предупреждаем об этом пользователя.
                                if (!File.Exists(path)) throw new ArgumentException();
                                if (!path.EndsWith(".csv")) throw new FormatException();

                                Console.WriteLine("Попытка записи...");
                                CsvProcessing.Write(data, path, add: true);
                                Console.WriteLine("Данные успешно записаны.");
                                break;
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine("Файл не найден. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Неверное расширение файла. Введите другое имя файла.");
                                path = Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine("Произошла непредвиденная ошибка при загрузке в файл. Повторите попытку.");
                                path = Console.ReadLine();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Выберите вариант из списка.");
                        choiceCycle = true;
                        continue;
                }
            }
        }

        /// <summary>
        /// Метод для сортировки данных по столбцу "Фамилия".
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Item[] SortFileData(Item[] data)
        {
            if (data == null)
            {
                Console.WriteLine("Список адвокатов пуст.");
                return null;
            }

            // Узнаем у пользователя как он хочет отсортировать данные.
            Console.WriteLine("Выберите из вариантов:");
            Console.WriteLine("1) Отсортировать данные в алфавитном порядке.");
            Console.WriteLine("2) Отсортировать данные в обратном алфавитном порядке.");
            bool choiceCycle = true;
            bool reverse = false;
            while (choiceCycle)
            {
                choiceCycle = false;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        reverse = false;
                        break;
                    case ConsoleKey.D2:
                        reverse = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Выберите из списка.");
                        choiceCycle = true;
                        break;
                }
            }
            Item[] sortedData = data;
            try
            {
                // Сортируем данные.
                sortedData = DataProcessing.SortColumn(data, "Фамилия", reverse);
                Console.WriteLine();
                Console.WriteLine("Данные успешно отсортированы.");
                // Показываем результат пользователю.
                MainUI.WriteFileData(sortedData);
            }
            catch { Console.WriteLine("Произошла непредвиденная ошибка."); }
            return sortedData;
        }

        /// <summary>
        /// Метод фильтрует данные по значению, полученному от пользователя.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Item[] FilterFileData(Item[] data)
        {
            if (data == null)
            {
                Console.WriteLine("Список адвокатов пуст.");
                return null;
            }

            // Узнаем у пользователя по каким столбцам фильтровать данные.
            Console.WriteLine("Выберите из вариантов:");
            Console.WriteLine("1) Отфильтровать данные по столбцу \"Приостановление статуса адвоката\".");
            Console.WriteLine("2) Отфильтровать данные по столбцу \"Возобновление статуса адвоката\".");
            bool choiceCycle = true;
            int choice = 0;
            while (choiceCycle)
            {
                choiceCycle = false;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        choice = 1;
                        break;
                    case ConsoleKey.D2:
                        choice = 2;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Выберите из списка.");
                        choiceCycle = true;
                        break;
                }
            }
            Item[] filteredData = data;
            try
            {
                // Получаем значение ячейки для фильтра.
                Console.WriteLine();
                Console.WriteLine("Введите значение для фильтра.");
                string columnValue = Console.ReadLine();

                // Фильтруем данные.
                if (choice == 1) filteredData = DataProcessing.FilterItems(data, columnValue, 1);
                else filteredData = DataProcessing.FilterItems(data, columnValue, 2);

                Console.WriteLine("Данные успешно отфильтрованы.");
                // Показываем результат пользователю.
                MainUI.WriteFileData(filteredData);
            }
            catch { Console.WriteLine("Произошла непредвиденная ошибка."); }
            return filteredData;
        }
    }
}
