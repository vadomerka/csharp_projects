using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;


namespace DataProcessing
{
    public class JSONProcessing
    {
        public JSONProcessing() { }

        /// <summary>
        /// Метод записывает список данных в поток.
        /// </summary>
        /// <param name="plants">Список данных для записи.</param>
        /// <returns>Поток с данными.</returns>
        public async Task<Stream> WriteAsync(List<Plant> plants)
        {
            var stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            writer.Write(JsonSerializer.Serialize(plants, options));
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
            string? fileData = null;
            List<IStreamItem>? jsonPlants = null;
            List<Plant>? plants = null;
            try
            {
                StreamReader reader = new StreamReader(stream);
                fileData = await reader.ReadToEndAsync();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                plants = JsonSerializer.Deserialize<List<Plant>>(fileData, options);
                if (plants is null) throw new ArgumentNullException();
                jsonPlants = plants.Select(x => x as IStreamItem).ToList();
            }
            catch
            {
                throw new FormatException();
            }

            if (jsonPlants == null)
            {
                throw new ArgumentException();
            }
            jsonPlants.Insert(0, new Header(true));
            jsonPlants.Insert(0, new Header(false));
            stream.Seek(0, SeekOrigin.Begin);
            return jsonPlants;
        }
    }
}
