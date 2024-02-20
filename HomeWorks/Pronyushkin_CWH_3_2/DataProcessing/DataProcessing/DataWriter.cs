using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataProcessing.Objects;

namespace DataProcessing.DataProcessing
{
    /// <summary>
    /// Класс выводит данные в консоль или файл.
    /// </summary>
    public static class DataWriter
    {
        /// <summary>
        /// Выводит данные в консоль.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        /// <exception cref="ArgumentNullException">Выбрасывает ошибку при пустом списке</exception>
        public static void Write(List<Patient>? patients)
        {
            if (patients == null) throw new ArgumentNullException();
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonData = JsonSerializer.Serialize(patients, options);
            Console.WriteLine(jsonData);
        }

        /// <summary>
        /// Выводит данные в файл.
        /// </summary>
        /// <param name="patients">Список пациентов</param>
        /// <param name="fPath">Пусть к файлу</param>
        /// <exception cref="ArgumentNullException">Выбрасывает ошибку при пустом списке</exception>
        public static void Write(List<Patient>? patients, string? fPath)
        {
            if (patients == null) throw new ArgumentNullException();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonData = JsonSerializer.Serialize(patients, options);
            File.WriteAllText(fPath ?? "", jsonData);
        }
    }
}
