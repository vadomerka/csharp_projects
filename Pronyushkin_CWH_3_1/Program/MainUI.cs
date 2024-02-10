using DataProcessing;

namespace Program
{
    /// <summary>
    /// Класс MainUI обеспечивает связь межжду пользователем и библиотекой классов.
    /// </summary>
    public static class MainUI
    {
        /// <summary>
        /// Метод ReadData считывает данные из консоли или файла. Работает с пользователем.
        /// </summary>
        /// <returns></returns>
        public static List<Player> ReadData()
        {
            List <Player> resData = new List<Player>();
            // Запрашиваение у пользователя способа чтения данных.
            bool fileSave = false;
            bool choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Откуда вы хотите считать данные?");
                Console.WriteLine("1) Консоль.");
                Console.WriteLine("2) Файл.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        fileSave = false;
                        break;
                    case ConsoleKey.D2:
                        fileSave = true;
                        break;
                    default:
                        Console.WriteLine("Выберите один из вариантов.");
                        choiceLoop = true;
                        break;
                }
            }
            // Считывание данных из консоли.
            if (!fileSave)
            {
                try
                {
                    Console.WriteLine("Начните ввод данных.");
                    resData = StreamData.ReadStreamData();
                    Console.WriteLine("Данные успешно считаны.");
                    Console.WriteLine($"Считанных объектов: {resData.ToArray().Length}.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Произошла ошибка при считывании объекта в списке.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Данные неверного формата.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при считывании данных.");
                }
                return resData;
            }

            // Считывание данных из файла.
            Console.WriteLine("Введите путь к файлу, откуда вы хотите считать данные.");
            string fileName = Console.ReadLine();
            try
            {
                Console.WriteLine("Попытка считать данные..");
                resData = StreamData.ReadStreamData(fileName);
                Console.WriteLine("Данные успешно считаны.");
                Console.WriteLine($"Считанных объектов: {resData.ToArray().Length}.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Произошла ошибка при считывании объекта в списке.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Файл неверного формата.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Файл не был найден.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Файл невозможно считать.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при считывании данных.");
            }
            return resData;
        }

        /// <summary>
        /// Метод SaveData выводит данные в консоль или файл. Работает с пользователем.
        /// </summary>
        /// <param name="data"></param>
        public static void SaveData(List<Player> data, bool userChoice = true)
        {
            // Если пользователь еще не ввел данные, сообщаем об этом.
            if (data is null)
            {
                Console.WriteLine("Данные не обнаружены.");
                return;
            }
            // Если сохранение происходит не из меню, нужно уточнить необходимо ли оно.
            if (userChoice)
            {
                Console.WriteLine("Хотите ли вы сохранить данные? y/n");
                string input = Console.ReadLine()?.ToLower() ?? "";
                if (input == "n" || input == "н")
                {
                    Console.WriteLine("Данные не будут сохранены.");
                    return;
                }
            }
            // Запрашиваением у пользователя способ записи данных.
            bool fileSave = false;
            bool choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Куда вы хотите сохранить данные?");
                Console.WriteLine("1) Консоль.");
                Console.WriteLine("2) Файл.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        fileSave = false;
                        break;
                    case ConsoleKey.D2:
                        fileSave = true;
                        break;
                    default:
                        Console.WriteLine("Выберите один из вариантов.");
                        choiceLoop = true;
                        break;
                }
            }
            // Запись данных в консоль.
            if (!fileSave) 
            {
                try
                {
                    StreamData.SaveStreamData(data);
                    Console.WriteLine("Данные успешно записаны.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла непредвиденная ошибка при записи данных в консоль.");
                }
                return;
            }
            // Запись данных в файл.
            Console.WriteLine("Введите путь к файлу, куда вы хотите записать результаты.");
            string fileName = Console.ReadLine();
            try
            {
                StreamData.SaveStreamData(data, fileName);
                Console.WriteLine("Данные успешно записаны.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при записи данных в файл.");
            }
        }

        /// <summary>
        /// Метод FilterData фильтрует данные по полю и значению от пользователя.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Player> FilterData(List<Player> data)
        {
            // Если пользователь еще не ввел данные, сообщаем об этом.
            if (data is null)
            {
                Console.WriteLine("Данные не обнаружены.");
                return null;
            }
            bool choiceLoop = true;
            // Пользователь должен выбрать одну из 7ми функций для фильтрации.
            // Результат сохраняем в делегат.
            DataFilterDelegate filterRule = DataFilter.IdFilterRule;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберете вариант из списка:");
                Console.WriteLine("1) Отфильтровать по playerId.");
                Console.WriteLine("2) Отфильтровать по userName.");
                Console.WriteLine("3) Отфильтровать по level.");
                Console.WriteLine("4) Отфильтровать по gameScore.");
                Console.WriteLine("5) Отфильтровать по achievements.");
                Console.WriteLine("6) Отфильтровать по inventory.");
                Console.WriteLine("7) Отфильтровать по guild.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        filterRule = DataFilter.IdFilterRule;
                        break;
                    case ConsoleKey.D2:
                        filterRule = DataFilter.UserNameFilterRule;
                        break;
                    case ConsoleKey.D3:
                        filterRule = DataFilter.LevelFilterRule;
                        break;
                    case ConsoleKey.D4:
                        filterRule = DataFilter.ScoreFilterRule;
                        break;
                    case ConsoleKey.D5:
                        filterRule = DataFilter.AchievementsFilterRule;
                        break;
                    case ConsoleKey.D6:
                        filterRule = DataFilter.InventoryFilterRule;
                        break;
                    case ConsoleKey.D7:
                        filterRule = DataFilter.GuildFilterRule;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }
            // Значение для фильтра.
            Console.WriteLine("Введите значение, по которому хотите отфильтровать данные:");
            string filterValue = Console.ReadLine() ?? "";
            // Фильтрация.
            data = DataFilter.FilterPlayers(data, filterRule, filterValue);
            // Сохранение.
            MainUI.SaveData(data);
            return data;
        }

        /// <summary>
        /// Метод SortData сортирует данные по полю и порядку от пользователя.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Player> SortData(List<Player> data)
        {
            // Если пользователь еще не ввел данные, сообщаем об этом.
            if (data is null)
            {
                Console.WriteLine("Данные не обнаружены.");
                return null;
            }
            bool choiceLoop = true;
            // Пользователь должен выбрать одну из 7ми функций для сортировки.
            // Результат сохраняем в делегат.
            Comparison<Player> sortRule = DataSorter.PlayerIdSort;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберете вариант из списка:");
                Console.WriteLine("1) Отсортировать по playerId.");
                Console.WriteLine("2) Отсортировать по userName.");
                Console.WriteLine("3) Отсортировать по level.");
                Console.WriteLine("4) Отсортировать по gameScore.");
                Console.WriteLine("5) Отсортировать по achievements.");
                Console.WriteLine("6) Отсортировать по inventory.");
                Console.WriteLine("7) Отсортировать по guild.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        sortRule = DataSorter.PlayerIdSort;
                        break;
                    case ConsoleKey.D2:
                        sortRule = DataSorter.PlayerNameSort;
                        break;
                    case ConsoleKey.D3:
                        sortRule = DataSorter.PlayerLevelSort;
                        break;
                    case ConsoleKey.D4:
                        sortRule = DataSorter.PlayerGameScoreSort;
                        break;
                    case ConsoleKey.D5:
                        sortRule = DataSorter.PlayerAchievementsSort;
                        break;
                    case ConsoleKey.D6:
                        sortRule = DataSorter.PlayerInventorySort;
                        break;
                    case ConsoleKey.D7:
                        sortRule = DataSorter.PlayerGuildSort;
                        break;
                    default:
                        choiceLoop = true;
                        break;
                }
            }
            // Спрашиваем у пользователя в каком порядке сортировать элементы.
            bool reversed = false;
            choiceLoop = true;
            while (choiceLoop)
            {
                choiceLoop = false;
                Console.WriteLine("Выберите в каком порядке сортировать данные?");
                Console.WriteLine("1) В прямом.");
                Console.WriteLine("2) В обратном.");
                ConsoleKey inpKey = Console.ReadKey().Key;
                Console.WriteLine();
                switch (inpKey)
                {
                    case ConsoleKey.D1:
                        reversed = false;
                        break;
                    case ConsoleKey.D2:
                        reversed = true;
                        break;
                    default:
                        break;
                }
            }

            Comparison <Player> sorterDelegate = sortRule;
            // Переворачиваем правило, если необходимо.
            if (reversed) sorterDelegate = (x, y) => -sortRule(x, y);
            // Сортируем.
            data.Sort(sorterDelegate);
            // Сохраняем.
            MainUI.SaveData(data);
            return data;
        }
    }
}
