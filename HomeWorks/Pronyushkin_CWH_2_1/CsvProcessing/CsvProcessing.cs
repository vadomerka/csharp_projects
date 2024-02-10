using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessingClasses
{
    /// <summary>
    /// Класс для чтения и записи данных в csv файлы.
    /// </summary>
    public class CsvProcessing
    {
        // Путь к файлу.
        static string fPath;

        // Конструктор класса.
        public CsvProcessing(string fname) { fPath = fname; }

        /// <summary>
        /// Метод для чтения данных из csv файла
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string[] Read()
        {
            try
            {
                string readText = File.ReadAllText(fPath);
                // Разделяем текст на строчки, используя ";\n".
                // Если использовать обычный переход на новую строчку, данные считаются неправильно.
                // Так как в некоторых ячейках тоже может встречаться переход на новую строчку.
                string[] lines = readText.Split(";\n", StringSplitOptions.RemoveEmptyEntries);
                return lines;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Метод для записи строковых данных в новый csv файл. Если файл существует, данные добавляются в файл.
        /// </summary>
        /// <param name="line"></param>
        /// <param name="nPath"></param>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Exception"></exception>
        public void Write(string line, string nPath) 
        {
            try
            {
                int headerRowsNumber = 2;
                // Необходимо обработать данные перед записью.
                string[] dataLines = line.Split(";\n", StringSplitOptions.RemoveEmptyEntries);
                // Отделяем заголовок от остальных строк.
                string header = DataProcessing.LinesToString(dataLines[..headerRowsNumber]);
                string data = DataProcessing.LinesToString(dataLines[headerRowsNumber..]);

                if (!nPath.EndsWith(".csv")) { throw new FormatException(); }

                if (!File.Exists(nPath))
                {
                    // Добавляем заголовок в файл только если он пуст.
                    File.WriteAllText(nPath, header);
                }
                // Записываем остальные данные.
                File.AppendAllText(nPath, data);
            }
            catch (FormatException e)
            {
                throw new FormatException("Неверное расширение файла. " + e.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось записать данные в файл. " + e.ToString());
            }
        }

        /// <summary>
        /// Метод для записи списка строк в данный csv файл. Если файл существует, файл перезаписывается с новыми данными.
        /// </summary>
        /// <param name="lines"></param>
        /// <exception cref="Exception"></exception>
        public void Write(string[] lines)
        {
            try
            {
                // Удаляем предыдущие данные в файле.
                File.WriteAllText(fPath, lines[0] + ";\n");
                for (int i = 1; i < lines.Length; i++) 
                {
                    // Добавляем оставшиеся.
                    File.AppendAllText(fPath, lines[i] + ";\n");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось записать данные в файл. " + e.ToString());
            }

        }
    }
}