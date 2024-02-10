namespace ClassLibrary
{
    /// <summary>
    /// Класс для чтения и записи данных в csv файлы.
    /// </summary>
    public static class CsvProcessing
    {
        /// <summary>
        /// Метод для чтения данных из csv файла.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string[] Read(string fPath)
        {
            try
            {
                string readText = File.ReadAllText(fPath);
                // В файле присутствуют строки, которые оканчиваются не на ",\n", а на "\n".
                // Поэтому я заменяю все их вхождения, чтобы получить постоянство.
                readText = readText.Replace(",\n", "\n");
                string[] lines = readText.Split("\n", StringSplitOptions.RemoveEmptyEntries);
                return lines;
            }
            catch (Exception e)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Метод для записи данных в файл.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="path"></param>
        /// <param name="add"></param>
        /// <exception cref="Exception"></exception>
        public static void Write(Item[] lines, string path, bool add = false)
        {
            try
            {
                string res = string.Empty;
                // Если мы перезаписываем данные файла, нужно добавить заголовок.
                if (!add) File.WriteAllText(path, lines[0].ToString() + ",\n");
                foreach (Item item in lines[1..]) 
                {
                    res += item.ToString() + ",\n";
                }
                File.AppendAllText(path, res);
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось записать данные в файл. " + e.ToString());
            }
        }
    }
}