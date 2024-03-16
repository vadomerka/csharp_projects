using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class CSVProcessing
    {
        public CSVProcessing() { }

        public async Task<Stream> WriteAsync(List<ICSVItem> plants)
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

        public async Task<List<ICSVItem>> ReadAsync(Stream stream)
        {
            // StreamWriter writer = new StreamWriter(stream);
            string? fileData = null;
            var res = new List<ICSVItem>();
            try
            {
                StreamReader reader = new StreamReader(stream);
                fileData = await reader.ReadToEndAsync();
                fileData = fileData.Replace("\r\n", "\n");
                string[] lines = (fileData ?? "").Split(";\n", StringSplitOptions.RemoveEmptyEntries);
                res = new List<ICSVItem>()
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
                    catch { }
                }
                stream.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            return res;
        }
    }
}
