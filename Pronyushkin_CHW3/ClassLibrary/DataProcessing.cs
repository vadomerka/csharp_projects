namespace ClassLibrary
{
    /// <summary>
    /// Класс для обработки данных.
    /// </summary>
    public static class DataProcessing
    {
        /// <summary>
        /// Вспомогательный метод. Разделяет строку данных на массив строк.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string[] GetLine(string data)
        {
            string[] line;
            line = data.Split(",");
            return line;
        }

        /// <summary>
        /// Вспомогательный метод. Разделяет массив строк на зубчатый массив.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string[][] GetTable(string[] data)
        {
            // В файле задания заголовок содержит 16 ячеек, а большинство последующих строк 18.
            // Так как последние две ячейки не играют роли в сортировке, фильтрации и выводе в консоли,
            // Я решил не сохранять данные, которые превышают количество ячеек заголовка.
            string[][] table = new string[data.Length][];
            // Заголовок.
            table[0] = GetLine(data[0]);
            // Количество ячеек заголовка.
            int headerLength = table[0].Length;
            for (int i = 1; i < data.Length; i++)
            {
                // Просматриваем строчки адвокатов:
                string[] gline = GetLine(data[i]);
                // Если количество ячеек строчки меньше чем в заголовке.
                if (gline.Length < headerLength)
                {
                    // Добавляем пустые значения.
                    string[] newGLine = new string[headerLength];
                    for (int j = 0; j < gline.Length; j++)
                    {
                        newGLine[j] = gline[j];
                    }
                    for (int j = gline.Length; j < headerLength; j++)
                    {
                        newGLine[j] = "";
                    }
                    gline = newGLine;
                }
                // Если наоборот.
                else if (gline.Length > headerLength)
                {
                    // Не добавляем лишние значения.
                    string[] newGLine = new string[headerLength];
                    for (int j = 0; j < headerLength; j++)
                    {
                        newGLine[j] = gline[j];
                    }
                    gline = newGLine;
                }
                table[i] = gline;
            }
            return table;
        }

        /// <summary>
        /// Вспомогательный метод. Создает массив адвокатов из списка строк.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Item[] GetLawers(string[] data)
        {
            string[][] table = GetTable(data);
            Item[] res = new Item[table.Length];
            // Создаем экземпляр заголовка.
            res[0] = new Header(table[0]);
            for (int i = 1; i < table.Length; i++)
            {
                // Заполняем список адвокатами.
                res[i] = new Lawer(table[i]);
            }
            return res;
        }

        /// <summary>
        /// Метод для работы с данными. Сортирует таблицу по введенному столбцу, в нужном направлении.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnName"></param>
        /// <param name="reversed"></param>
        /// <param name="headerRowsNumber"></param>
        /// <returns></returns>
        public static Item[] SortColumn(Item[] data, string columnName, bool reversed = false, int headerRowsNumber = 1)
        {
            // Проверка на пустые значения.
            if (data == null) return null;
            if (data.Length <= headerRowsNumber) return data;

            // Отделяем заголовок от данных.
            Item[] table = data[headerRowsNumber..];

            Item[] res = new Item[data.Length];

            // Проводим сортировку по нужному столбцу.
            Array.Sort(table, (x, y) => ((Lawer)x).LastName.CompareTo(((Lawer)y).LastName));
            // Разворачиваем сортировку при необходимости.
            if (reversed) { Array.Reverse(table); }

            res[0] = data[0];
            for (int i = headerRowsNumber; i < data.Length; i++)
            {
                // Добавляем остальные строки в ответ.
                res[i] = table[i - headerRowsNumber];
            }
            return res;
        }

        /// <summary>
        /// Метод для работы с данными. Делает выборку строк по значению в данном столбце.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnValue"></param>
        /// <param name="columnChoice"></param>
        /// <param name="headerRowsNumber"></param>
        /// <returns></returns>
        public static Item[] FilterItems(Item[] data, string columnValue, int columnChoice = 1, int headerRowsNumber = 1)
        {
            // Проверка на пустые значения.
            if (data == null) return null;
            if (data.Length <= headerRowsNumber) return data;

            // Отделяем заголовок от данных.
            Item[] table = data[headerRowsNumber..];
            List<Item> resList = new List<Item>() { data[0] };

            int filterColumn = 2;
            if (columnChoice == 2) filterColumn = 4;

            for (int i = 0; i < table.Length; i++)
            {
                // Добавляем к ответу строки, удовлетворяющие условию.
                Requisites curReq = ((Lawer)table[i]).Requisites[filterColumn];
                // Если пользователь ввел пустую строку - нужно вывести все пустые значения.
                if (((curReq.ToString() == " от ") && (columnValue == "")) ||
                    curReq.ToString() == columnValue)
                {
                    resList.Add(table[i]);
                }
            }
            Item[] resTable = resList.ToArray();
            return resTable;
        }

        /// <summary>
        /// Метод для красивого вывода массива адвокатов на экран.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="lineNum"></param>
        /// <param name="reverse"></param>
        public static void WriteItems(Item[] items, int lineNum = 10, bool reverse = false)
        {
            int id = 0;
            // Длины слов заголовка.
            int wl1 = "№".Length;
            int wl2 = "Фамилия".Length;
            int wl3 = "Имя".Length;
            int wl4 = "Отчество".Length;
            try
            {
                for (int i = 0; i < lineNum; i++)
                {
                    id = i + 1;
                    if (reverse) { id = items.Length - id - 1; }
                    // Вычисляем самые длинные слова в каждом столбце.
                    wl1 = Math.Max(((Lawer)items[id]).Number.Length, wl1);
                    wl2 = Math.Max(((Lawer)items[id]).LastName.Length, wl2);
                    wl3 = Math.Max(((Lawer)items[id]).Name.Length, wl3);
                    wl4 = Math.Max(((Lawer)items[id]).SurName.Length, wl4);
                }
                // Выводим заголовок.
                Console.WriteLine(items[0].ToSmartString(new int[] { wl1, wl2, wl3, wl4 }));
                for (int i = 0; i < lineNum; i++)
                {
                    // Выводим всех адвокатов.
                    id = i + 1;
                    if (reverse) { id = items.Length - id - 1; }
                    Lawer item = (Lawer)items[id];
                    Console.WriteLine(item.ToSmartString(new int[] { wl1, wl2, wl3, wl4 }));
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
}
