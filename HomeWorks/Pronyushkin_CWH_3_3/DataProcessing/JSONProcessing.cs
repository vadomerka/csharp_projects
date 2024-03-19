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

        public async Task<List<ICSVItem>> ReadAsync(Stream stream)
        {
            string? fileData = null;
            List<ICSVItem>? jsonPlants = null;
            List<Plant>? plants = null;
            try
            {
                StreamReader reader = new StreamReader(stream);
                fileData = await reader.ReadToEndAsync();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                plants = JsonSerializer.Deserialize<List<Plant>>(fileData, options);
                if (plants is null) throw new ArgumentNullException();
                jsonPlants = plants.Select(x => x as ICSVItem).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (jsonPlants == null)
            {
                throw new ArgumentNullException();
            }
            jsonPlants.Insert(0, new Header());
            jsonPlants.Insert(0, new Header());
            stream.Seek(0, SeekOrigin.Begin);
            return jsonPlants;
        }
    }
}
