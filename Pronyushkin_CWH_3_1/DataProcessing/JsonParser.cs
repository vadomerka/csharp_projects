using System.Text;

namespace DataProcessing
{
    /// <summary>
    /// Класс JsonParser для считывания и записи данных в поток.
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Метод WriteJson выводит объекты в поток.
        /// </summary>
        /// <param name="data"></param>
        public static void WriteJson(List<Player> data)
        {
            Console.WriteLine("[");
            if (data != null && data.Count > 0)
            {
                // Выписываем все объекты в json формате.
                for (int i = 0; i < data.Count - 1; i++)
                {
                    Player player = data[i];
                    Console.WriteLine($"{player.Jsonify()},");
                }
                Console.WriteLine($"{data[^1].Jsonify()}");
            }
            Console.WriteLine("]");
        }

        /// <summary>
        /// Метод ReadJson считывает объекты из потока. Работает по принципу конечного автомата.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        public static List<Player> ReadJson()
        {
            // Считываем лишние символы перед вводом.
            while (char.IsWhiteSpace((char)Console.In.Peek())) Console.In.Read();
            // Проверка на правильный формат.
            if ((char)Console.In.Read() != '[') throw new FormatException();

            var playersList = new List<Player>();
            char ch;
            try
            {
                while (true)
                {
                    // Считываем данные посимвольно.
                    ch = (char)Console.In.Read();
                    // Если символ начала объекта - считываем весь объект.
                    if (ch == '{')
                    {
                        playersList.Add(ReadObject());
                    }
                    else if (ch == ',')
                    {
                        // Если два символа ',' подряд - ошибка формата.
                        if (Console.In.Peek() == ',')
                        {
                            throw new FormatException();
                        }
                    }
                    // Если закрытие списка объектов - возвращаем считанные объекты.
                    else if (ch == ']')
                    {
                        break;
                    }
                    else if (char.IsWhiteSpace(ch))
                    {
                        continue;
                    }
                    else throw new FormatException();
                }
            }
            finally
            {
                // Пропускаем все лишние несчитанные символы.
                while (Console.In.Peek() != -1) Console.In.Read();
            }
            
            return playersList;
        }

        /// <summary>
        /// Вспомогательный метод ReadObject. Считывает объект из потока. Работает по принципу конечного автомата.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        private static Player ReadObject()
        {
            char ch;
            // Список всех значений внутри объекта.
            List<object> deepAttributes = new List<object>();
            while (true)
            {
                ch = (char)Console.In.Read();
                // Если символ '}' - объект закончился, возвращаем результат.
                if (ch == '}')
                {
                    try
                    {
                        return new Player(deepAttributes.ToArray());
                    }
                    catch 
                    {
                        throw new FormatException();
                    }
                }
                // Считывание аттрибутов.
                else if (ch == '"')
                {
                    // Считываем название аттрибута.
                    ReadString();

                    // Удаляем лишние пробелы и продвигаем курсор.
                    do { ch = (char)Console.In.Read(); } while (char.IsWhiteSpace(ch));

                    if (ch != ':') { throw new FormatException(); }

                    // Удаляем лишние пробелы.
                    while (char.IsWhiteSpace((char)Console.In.Peek())) Console.In.Read();

                    // Будущее значение.
                    object atr = null;

                    // Символ нужно просматривать, а не читать, чтобы его можно было прочитать в числе.
                    ch = (char)Console.In.Peek();
                    // Если кавычка - считываем строку.
                    if (ch == '"')
                    {
                        // Пропускаем кавычку.
                        ch = (char)Console.In.Read();
                        atr = ReadString();
                    }
                    // Если цифра - считываем число.
                    else if (char.IsDigit(ch))
                    {
                        atr = ReadInt();
                    }
                    // Если начало списка - считываем список.
                    else if (ch == '[')
                    {
                        ch = (char)Console.In.Read();
                        atr = ReadList();
                    }
                    // Добавляем считанное значение в список объекта.
                    deepAttributes.Add(atr);
                }
                else if (ch == ',')
                {
                    // Если два символа ',' подряд - ошибка формата.
                    if (Console.In.Peek() == ',')
                    {
                        throw new FormatException();
                    }
                }
                else if (char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else { throw new FormatException(); }
            }
        }

        /// <summary>
        /// Вспомогательный метод ReadString. Считывает строку из потока. Работает по принципу конечного автомата.
        /// </summary>
        /// <returns></returns>
        private static string ReadString()
        {
            char ch;
            StringBuilder newString = new StringBuilder();
            while (true)
            {
                ch = (char)Console.In.Read();
                // Если кавычка - строка кончилась, возвращаем значение.
                if (ch == '"')
                {
                    return newString.ToString();
                }
                // Иначе - добавляем символ.
                else
                {
                    newString.Append(ch);
                }
            }
        }

        /// <summary>
        /// Вспомогательный метод ReadInt. Считывает число из потока. Работает по принципу конечного автомата.
        /// </summary>
        /// <returns></returns>
        private static int ReadInt()
        {
            char ch;
            StringBuilder newString = new StringBuilder();
            // Считываем число, пока не встретим запятую.
            do
            {
                ch = (char)Console.In.Read();
                newString.Append(ch);
            } while (Console.In.Peek() != ',');
            // Пытаемся конвертировать.
            try
            {
                return int.Parse(newString.ToString());
            }
            catch
            {
                throw new FormatException();
            }
        }

        /// <summary>
        /// Вспомогательный метод ReadList. Считывает список строк из потока. Работает по принципу конечного автомата.
        /// </summary>
        /// <returns></returns>
        private static string[] ReadList()
        {
            char ch;
            string newString = "";
            List<string> strList = new List<string>();
            while(true)
            {
                ch = (char)Console.In.Read();
                // Если кавычка - считываем строку.
                if (ch == '"')
                {
                    newString = ReadString();
                    strList.Add(newString);
                }
                // Если конец списка - возвращаем результат.
                else if (ch == ']')
                {
                    return strList.ToArray();
                }
                else if (ch == ',')
                {
                    // Если два символа ',' подряд - ошибка формата.
                    if (Console.In.Peek() == ',')
                    {
                        throw new FormatException();
                    }
                }
                else if (char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else { throw new FormatException(); }
            }
        }
    }
}
