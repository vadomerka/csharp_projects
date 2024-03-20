using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    /// <summary>
    /// Класс для работы с потоком.
    /// </summary>
    public class CSVProcessing
    {
        public CSVProcessing() { }

        /// <summary>
        /// Метод записывает список данных в поток.
        /// </summary>
        /// <param name="plants">Список данных для записи.</param>
        /// <returns>Поток с данными.</returns>
        public async Task<Stream> WriteAsync(List<IStreamItem> plants)
        {
            var stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            for (int i = 0; i < plants.Count; i++)
            {
                var plant = plants[i];
                var a = plant.ToString();
                await writer.WriteLineAsync(a);
            }
            await writer.FlushAsync();
            return stream;
        }

        /// <summary>
        /// Метод считывает список данных из потока.
        /// </summary>
        /// <param name="stream">Поток с данными.</param>
        /// <returns>Список данных.</returns>
        /// <exception cref="ArgumentException">Ошибка чтения файла.</exception>
        /// <exception cref="FormatException">Файл неверного формата.</exception>
        public async Task<List<IStreamItem>> ReadAsync(Stream stream)
        {
            var res = new List<IStreamItem>();
            try
            {
                StreamReader reader = new StreamReader(stream);
                string? fileData = await reader.ReadToEndAsync();
                fileData = fileData.Replace("\r\n", "\n");
                string[] lines = (fileData ?? "").Split(";\n", StringSplitOptions.RemoveEmptyEntries);
                res = new List<IStreamItem>()
                {
                    new Header(lines[0]),
                    new Header(lines[1])
                };
                for (int i = 2; i < lines.Length; i++)
                {
                    try
                    {
                        res.Add(new Plant(lines[i]));
                    }
                    catch 
                    {
                        throw new FormatException();
                    }
                }
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (FormatException)
            {
                throw new FormatException();
            }
            catch
            {
                throw new ArgumentException();
            }
            return res;
        }
    }
}
