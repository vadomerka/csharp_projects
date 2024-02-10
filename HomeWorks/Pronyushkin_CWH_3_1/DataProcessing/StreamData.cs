namespace DataProcessing
{
    /// <summary>
    /// Вспомогательный класс для JsonParser. Меняет потоковый ввод/вывод.
    /// </summary>
    public static class StreamData
    {
        /// <summary>
        /// Метод ReadStreamData вызывает чтение для консоли.
        /// </summary>
        /// <returns></returns>
        public static List<Player> ReadStreamData()
        {
            return JsonParser.ReadJson();
        }

        /// <summary>
        /// Перегрузка метода ReadStreamData вызывает чтение для файла.
        /// </summary>
        /// <param name="readFilePath"></param>
        /// <returns></returns>
        public static List<Player> ReadStreamData(string readFilePath)
        {
            List<Player> resData = null;
            try
            {
                // Перенаправление потока на файл.
                using (StreamReader log = new StreamReader(readFilePath))
                {
                    Console.SetIn(log);

                    resData = JsonParser.ReadJson();
                }
            }
            finally
            {
                // Перенаправление потока обратно на консоль.
                StreamReader sr = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding);
                Console.SetIn(sr);
            }
            return resData;
        }

        /// <summary>
        /// Метод SaveStreamData вызывает сохранение для консоли.
        /// </summary>
        /// <param name="data"></param>
        public static void SaveStreamData(List<Player> data)
        {
            JsonParser.WriteJson(data);
        }

        /// <summary>
        /// Перегрузка метода SaveStreamData вызывает сохранение для файла.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="writeFilePath"></param>
        public static void SaveStreamData(List<Player> data, string writeFilePath)
        {
            try
            {
                // Перенаправление потока на файл.
                using (StreamWriter log = new StreamWriter(writeFilePath))
                {
                    Console.SetOut(log);

                    JsonParser.WriteJson(data);
                }
            }
            finally
            {
                // Перенаправление потока обратно на консоль.
                StreamWriter sw = new StreamWriter(Console.OpenStandardOutput(), Console.OutputEncoding)
                {
                    AutoFlush = true
                };
                Console.SetOut(sw);
            }
        }

    }
}
