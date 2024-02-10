using ProcessingClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="fileData"></param>
        /// <returns></returns>
        public static CsvProcessing GetFileData(ref string[] fileData)
        {
            Console.WriteLine("Введите название файла, который необходимо считать.");
            Console.WriteLine("Для выхода из программы введите exit.");
            string path = Console.ReadLine();
            // Реализована возможность выхода из программы на этапе ввода названия файла.
            while (path != "exit")
            {
                try
                {
                    // Если пользователь ввел файл неверного формата, возвращаем ошибку.
                    if (!path.EndsWith(".csv")) { throw new FormatException(); }

                    CsvProcessing cp = new CsvProcessing(path);
                    fileData = cp.Read();
                    return cp;
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
        /// Метод работает с пользователем, сохраняет данные в нужный файл.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cv"></param>
        public static void SaveData(string[] data, CsvProcessing cv)
        {
            Console.WriteLine("Вы хотите перезаписать содержимое этого файла (1), или дописать данные в другой? (2)");
            Console.WriteLine("Введите 1 или 2.");
            // Цикл, который заставляет пользователя выбрать либо 1 либо 2.
            // Так как break прерывает только switch, я реализовал выход из этого цикла через переменную.
            bool caseLoop = true;
            while (caseLoop)
            {
                caseLoop = false;
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine();
                        try
                        {
                            // Перезаписываем данные нашего файла.
                            cv.Write(data);
                            Console.WriteLine("Данные успешно записаны.");
                        }
                        catch
                        {
                            Console.WriteLine("Произошла непредвиденная ошибка при загрузке в файл.");
                        }
                        break;
                    case ConsoleKey.D2:
                        Console.WriteLine();
                        Console.WriteLine("Введите название файла, куда вы хотите сохранить результаты.");
                        string saveName;
                        saveName = Console.ReadLine();
                        // Цикл для запроса правильного имени файла.
                        while (true)
                        {
                            try
                            {
                                // Добавляем данные к новому файлу.
                                cv.Write(DataProcessing.LinesToString(data), saveName);
                                Console.WriteLine("Данные успешно записаны.");
                                break;
                            }
                            catch (FormatException e)
                            {
                                // Реализуем повтор при неправильном вводе.
                                Console.WriteLine("Название файла неправильного формата. Повторите попытку.");
                                saveName = Console.ReadLine();
                            }
                            catch (Exception)
                            {
                                // Реализуем повтор при неправильном вводе.
                                Console.WriteLine("Произошла ошибка при загрузке в файл. Повторите попытку.");
                                saveName = Console.ReadLine();
                            }
                        }
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Введите 1 или 2.");
                        // Присваиваем caseLoop истину, чтобы реализовать повтор цикла.
                        caseLoop = true;
                        continue;
                }
            }
        }

        /// <summary>
        /// Метод работает с пользователем, делает выборку по столбцу переданному через параметры с введенным пользователем значением ячейки.
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="cv"></param>
        /// <param name="columnName"></param>
        public static void UserFilter(string[] fileData, CsvProcessing cv, string columnName)
        {
            string columnInput;
            string[] filteredData = new string[0];
            string[] printedColumns;
            string input;
            bool boolInput;
            Console.Clear();
            Console.WriteLine($"Введите значение столбца '{columnName}' для выборки строк.");
            columnInput = Console.ReadLine();
            // Производим выборку по нужным данным.
            try { filteredData = DataProcessing.FilterRow(fileData, columnName, columnInput); }
            catch { Console.WriteLine("Столбец не найден"); }
            // На экран будут выведены лишь некоторые столбцы, для удобства просмотра данных.
            printedColumns = new string[] { "ID", "Name", "LatinName", columnName };
            if (filteredData != null && filteredData.Length != 0)
            {
                Console.WriteLine("Полученная выборка: ");
                Console.WriteLine("(Для удобства на экране выведены только некоторые столбцы.)");
                // Выводим непустую выборку, выбрав нужные столбцы, на экран.
                DataProcessing.WriteTable(DataProcessing.GetColumns(filteredData, printedColumns));
            }
            else
            {
                // Если выборка пустая, сообщаяем об этом пользователю.
                Console.WriteLine("Метод вернул пустую выборку.");
                Console.WriteLine("Нажмите 'Enter', чтобы продолжить.");
                Console.ReadLine();
                return;
            }

            // Спрашиваем у пользователя, нужно ли сохранять результаты.
            Console.WriteLine("Сохранить результаты выборки? y/n");
            Console.WriteLine("(При сохранении будет сохраняться полная таблица.)");
            input = Console.ReadLine().ToLower();
            boolInput = false;
            if (input == "y" || input == "д" || input == "да" || input == "yes") { boolInput = true; }
            if (!boolInput)
            {
                Console.WriteLine("Результаты не будут сохранены.");
                Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
                Console.ReadLine();
                return;
            }

            // Сохраняем результаты.
            SaveData(filteredData, cv);

            Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
            Console.ReadLine();
            return;
        }

        /// <summary>
        /// Метод, который является перегрузкой UserFilter. Делает выборку по нескольким столбцам.
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="cv"></param>
        /// <param name="columnNames"></param>
        public static void UserFilter(string[] fileData, CsvProcessing cv, string[] columnNames)
        {
            string[] columnInput = new string[columnNames.Length];
            string[] filteredData = { };
            string[] printedColumns;
            string input;
            bool boolInput;
            List<string> printedList = new List<string> { "ID", "Name", "LatinName" };

            Console.Clear();
            // Считываем значения для выборок.
            for (int i = 0; i < columnNames.Length; i++)
            {
                Console.WriteLine($"Введите значение столбца '{columnNames[i]}' для выборки строк.");
                columnInput[i] = Console.ReadLine();
                printedList.Add(columnNames[i]);
            }
            try { filteredData = DataProcessing.FilterRows(fileData, columnNames, columnInput); }
            catch { Console.WriteLine("Столбец не найден"); }
            printedColumns = printedList.ToArray();
            if (filteredData != null && filteredData.Length != 0)
            {
                Console.WriteLine("Полученная выборка: ");
                Console.WriteLine("(Для удобства на экране выведены только некоторые столбцы.)");
                DataProcessing.WriteTable(DataProcessing.GetColumns(filteredData, printedColumns));
            }
            else
            {
                Console.WriteLine("Метод вернул пустую выборку.");
                Console.WriteLine("Нажмите 'Enter', чтобы продолжить.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Сохранить результаты выборки? y/n");
            Console.WriteLine("(При сохранении будет сохраняться полная таблица.)");
            input = Console.ReadLine().ToLower();
            boolInput = false;
            if (input == "y" || input == "д" || input == "да" || input == "yes") { boolInput = true; }
            if (!boolInput)
            {
                Console.WriteLine("Результаты не будут сохранены.");
                Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
                Console.ReadLine();
                return;
            }
            SaveData(filteredData, cv);

            Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
            Console.ReadLine();
            return;
        }

        /// <summary>
        /// Метод работает с пользователем, сортирует данные и выводит их на экран.
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="cv"></param>
        /// <param name="columnName"></param>
        /// <param name="reversed"></param>
        public static void UserSort(string[] fileData, CsvProcessing cv, string columnName, bool reversed=false)
        {
            string[] sortedData = { };
            string[] printedColumns;
            string input;
            bool boolInput;
            Console.Clear();
            // Сообщения к пользователю отличаются, в зависимости от направления сортировки.
            if (!reversed)
            {
                Console.WriteLine($"Выбрана сортировка по столбцу '{columnName}' в алфавитном порядке.");
            }
            else
            {
                Console.WriteLine($"Выбрана сортировка по столбцу '{columnName}' в обратном алфавитном порядке.");
            }

            // Сортируем данные.
            try { sortedData = DataProcessing.SortColumn(fileData, columnName, reversed); }
            catch { Console.WriteLine("Столбец не найден"); }
            printedColumns = new string[] { "ID", "Name", columnName };
            if (sortedData != null && sortedData.Length != 0)
            {
                // Выводим таблицу.
                Console.WriteLine("Полученная таблица: ");
                Console.WriteLine("(Для удобства на экране выведены только некоторые столбцы.)");
                DataProcessing.WriteTable(DataProcessing.GetColumns(sortedData, printedColumns));
            }
            else
            {
                Console.WriteLine("Метод вернул пустую таблицу.");
                Console.WriteLine("Нажмите 'Enter', чтобы продолжить.");
                Console.ReadLine();
                return;
            }

            // Спрашиваем у пользователя, нужно ли сохранять результаты.
            Console.WriteLine("Сохранить результаты сортировки? y/n");
            Console.WriteLine("(При сохранении будет сохраняться полная таблица.)");
            input = Console.ReadLine().ToLower();
            boolInput = false;
            if (input == "y" || input == "д" || input == "да" || input == "yes") { boolInput = true; }
            if (!boolInput)
            {
                Console.WriteLine("Результаты не будут сохранены.");
                Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
                Console.ReadLine();
                return;
            }
            // Сохраняем результаты.
            SaveData(sortedData, cv);

            Console.WriteLine("Нажмите 'Enter', чтобы вернуться в меню.");
            Console.ReadLine();
        }
    }
}
