using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClasses
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
        public static string[] GetLine(string data)
        {
            string[] line;
            line = data.Split("\";", StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < line.Length; j++)
            {
                // Удаляем лишние кавычки.
                line[j] = line[j].Trim('"');
            }
            return line;
        }

        /// <summary>
        /// Вспомогательный метод. Разделяет массив строк на зубчатый массив.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[][] GetTable(string[] data)
        {
            string[][] table = new string[data.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                table[i] = GetLine(data[i]);
            }
            return table;
        }

        /// <summary>
        /// Вспомогательный метод. Соединяет массив строк в одну строку нужного формата.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string LineToString(string[] line)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in line)
            {
                sb.Append('"' + s + '"' + ';');
            }
            return sb.ToString();
        }

        /// <summary>
        /// Вспомогательный метод. Соединяет массив строк в строку подходящую для записи в csv файл.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string LinesToString(string[] lines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in lines)
            {
                sb.Append(s + ";\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Вспомогательный метод. возвращает номер столбца по его имени в таблице
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnName"></param>
        /// <param name="headerId"></param>
        /// <returns></returns>
        public static int GetColumnId(string[] data, string columnName, int headerId = 0)
        {
            string[] header = GetLine(data[headerId]);
            int columnId = -1;
            for (int i = 0; i < header.Length; i++)
            {
                if (header[i] == columnName)
                {
                    columnId = i;
                }
            }
            return columnId;
        }

        /// <summary>
        /// Метод для удобного вывода данных. Выбирает нужные столбцы из таблицы данных.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnNames"></param>
        /// <param name="headerId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static string[] GetColumns(string[] data, string[] columnNames, int headerId = 0)
        {
            if (data == null || data.Length == 0) { throw new ArgumentNullException("Null data."); }
            string[][] table = GetTable(data);
            string[] header = GetLine(data[headerId]);
            // Получаем индексы нужных столбцов.
            List<int> columnIdsList = new List<int>();
            for (int i = 0; i < header.Length; i++)
            {
                for (int k = 0; k < columnNames.Length; k++)
                {
                    if (header[i] == columnNames[k])
                    {
                        columnIdsList.Add(i);
                    }
                }
            }
            int[] columnIds = columnIdsList.ToArray();
            if (columnIds.Length == 0) { throw new FormatException("Columns not found."); }

            string[] resColumns = new string[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                List<string> itemLine = new List<string>();
                for (int j = 0; j < columnIds.Length; j++)
                {
                    // Добавляем ячейки из нужных колонок в ответ.
                    itemLine.Add(table[i][columnIds[j]]);
                }
                resColumns[i] = LineToString(itemLine.ToArray());
            }
            return resColumns;
        }

        /// <summary>
        /// Метод для работы с данными. Делает выборку строк по значению в данном столбце.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnName"></param>
        /// <param name="columnValue"></param>
        /// <param name="headerRowsNumber"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string[] FilterRow(string[] data, string columnName, string columnValue, int headerRowsNumber = 2)
        {
            if (data == null || data.Length <= headerRowsNumber) { return null; }

            // Преобразуем данные в удобный формат.
            string[][] table = GetTable(data);
            List<string> resList = new List<string>();
            // Получаем номер столбца.
            int columnId = GetColumnId(data, columnName);
            if (columnId == -1) { throw new FormatException("Column not found."); }

            for (int i = 0; i < headerRowsNumber; i++)
            {
                // Добавляем к ответу заголовок.
                resList.Add(data[i]);
            }
            for (int i = headerRowsNumber; i < table.Length; i++)
            {
                // Добавляем к ответу строки, удовлетворяющие условию.
                if (table[i][columnId] == columnValue)
                {
                    resList.Add(data[i]);
                }
            }
            string[] resTable = resList.ToArray();
            // Если в ответе остался лишь заголовок - выборка пуста.
            if (resTable.Length == 2) { resTable = null; }
            return resTable;
        }

        /// <summary>
        /// Метод для работы с данными. Является перегрузкой метода FilterRows. Нужен для проведения выброки по нескольким столбцам.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnNames"></param>
        /// <param name="columnValues"></param>
        /// <param name="headerId"></param>
        /// <param name="headerRowsNumber"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string[] FilterRows(string[] data, string[] columnNames, string[] columnValues, int headerId = 0, int headerRowsNumber = 2)
        {
            if (data == null || data.Length <= headerRowsNumber) { return null; }

            string[][] table = GetTable(data);
            string[] header = GetLine(data[headerId]);
            List<int> columnIdsList = new List<int>();
            List<string> resTable = new List<string>();
            string line = "";
            // Получаем список нужных столбцов.
            for (int i = 0; i < header.Length; i++)
            {
                for (int k = 0; k < columnNames.Length; k++)
                {
                    if (header[i] == columnNames[k])
                    {
                        columnIdsList.Add(i);
                    }
                }
            }
            int[] columnIds = columnIdsList.ToArray();
            if (columnIds.Length == 0) { throw new FormatException("Columns not found."); }

            for (int i = 0; i < headerRowsNumber; i++)
            {
                resTable.Add(data[i]);
            }
            for (int i = headerRowsNumber; i < table.Length; i++)
            {
                // Добавляем строку только если она удовлетворяет всем условиям.
                bool rightRow = true;
                for (int k = 0; k < columnNames.Length; k++)
                {
                    if (table[i][columnIds[k]] != columnValues[k])
                    {
                        rightRow = false;
                    }
                }
                if (rightRow)
                {
                    resTable.Add(data[i]);
                }
            }
            string[] resString = resTable.ToArray();
            if (resString.Length == 2) { resString = null; }
            return resString;
        }

        /// <summary>
        /// Метод для работы с данными. Сортирует таблицу по введенному столбцу, в нужном направлении.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="columnName"></param>
        /// <param name="reversed"></param>
        /// <param name="headerRowsNumber"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static string[] SortColumn(string[] data, string columnName, bool reversed = false, int headerRowsNumber = 2)
        {
            if (data == null || data.Length <= headerRowsNumber) { return null; }

            // Отделяем заголовок от данных.
            string[][] table = GetTable(data)[headerRowsNumber..];
            string[] res = new string[data.Length];
            // Вычисляем номер столбца.
            int columnId = GetColumnId(data, columnName);
            if (columnId == -1) { throw new FormatException("Column not found."); }

            // Проводим сортировку по нужному столбцу.
            Array.Sort(table, (x, y) => x[columnId].CompareTo(y[columnId]));
            // Разворачиваем сортировку при необходимости.
            if (reversed) { Array.Reverse(table); }

            for (int i = 0; i < headerRowsNumber; i++)
            {
                // Добавляем заголовки в ответ.
                res[i] = data[i];
            }
            for (int i = headerRowsNumber; i < data.Length; i++)
            {
                // Добавляем остальные строки в ответ.
                res[i] = LineToString(table[i - headerRowsNumber]);
            }
            return res;
        }

        /// <summary>
        /// Метод для удобного вывода данных. Выводит данные в красивом табличном виде на экран с равным разделением пробелами.
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="FormatException"></exception>
        public static void WriteTable(string[] data)
        {
            string[][] table = GetTable(data);

            if (table == null || table.Length == 0)
            {
                throw new FormatException("Пустая таблица");
            }
            // Вычисляем максимальные длины слов в каждом столбце.
            int[] maxColLen = new int[table[0].Length];
            for (int j = 0; j < table[0].Length; j++)
            {
                for (int i = 0; i < table.Length; i++)
                {
                    if (maxColLen[j] < table[i][j].Length)
                    {
                        maxColLen[j] = table[i][j].Length;
                    }
                }
            }
            int spaceNum = 0;
            for (int i = 0; i < table.Length; i++)
            {
                for (int j = 0; j < table[0].Length; j++)
                {
                    // Вычисляем сколько пробелов нужно добавить для уравновешивания строк.
                    spaceNum = maxColLen[j] - table[i][j].Length;
                    Console.Write(table[i][j] + new string(' ', spaceNum) + '\t');
                }
                Console.WriteLine();
            }
        }
    }
}
