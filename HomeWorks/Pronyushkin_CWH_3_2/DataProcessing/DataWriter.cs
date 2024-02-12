using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataProcessing
{
    public static class DataWriter
    {
        public static void Write(List<Patient>? patients)
        { 
            if (patients == null) throw new ArgumentNullException();
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonData = JsonSerializer.Serialize(patients, options);
            Console.WriteLine(jsonData);
        }

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
