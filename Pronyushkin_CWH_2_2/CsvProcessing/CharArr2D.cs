using System.Text;

namespace DataClasses
{
    /// <summary>
    /// Класс для создания и обработки структуры данных.
    /// </summary>
    public class CharArr2D
    {
        // Объявление структуры данных.
        private char[][] _charArr;

        // Пустой конструктор класса.
        public CharArr2D() { }

        /// <summary>
        /// Конструктор класса. Создает экземпляр класса, используя строку.
        /// </summary>
        /// <param name="sentence"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public CharArr2D(string sentence)
        {
            // Проверка входных данных.
            if (sentence == null || sentence == "") throw new ArgumentNullException();
            if (!sentence.EndsWith(".")) throw new FormatException();

            List<List<char>> words = new List<List<char>>();
            int wordCount = 0;
            if (sentence != ".")
            {
                // Если предложение непустое, создаем первое слово.
                words.Add(new List<char>());
            }
            else
            {
                // Здесь необходимо уменьшить счетчик, так как позже он увеличится на 1.
                wordCount--;
            }
            // Текущий и предыдущий знак.
            char curChar = ' ', prevChar;
            
            for (int i = 0; i < sentence.Length; i++)
            {
                prevChar = curChar;
                curChar = sentence[i];
                // Если встретили точку - предложение закончилось.
                if (curChar == '.')
                {
                    // Если в предложении несколько точек - вызываем ошибку.
                    if (i + 1 != sentence.Length) throw new FormatException();
                    break;
                }
                // Если встретили пробел:
                if (curChar == ' ')
                {
                    // И если предыдущий символ не пробел:
                    if (prevChar != ' ')
                    {
                        // Создаем новое слово.
                        words.Add(new List<char>());
                        wordCount++;
                    }
                    continue;
                }
                // Иначе добавляем символ в последнее слово.
                words[wordCount].Add(curChar);
            }
            wordCount++;
            
            _charArr = new char[wordCount][];

            for (int i = 0; i < words.ToArray().Length; i++)
            {
                // Заполняем структуру данных.
                _charArr[i] = words[i].ToArray();
            }
        }

        /// <summary>
        /// Конструктор класса. Создает экземпляр класса, используя структуру данных.
        /// </summary>
        /// <param name="arr"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public CharArr2D(char[][] arr)
        {
            // Проверка входных данных.
            if (arr == null) throw new ArgumentNullException();
            _charArr = new char[arr.Length][];
            for (int i = 0; i < arr.Length; i++)
            {
                _charArr[i] = new char[arr[i].Length];
                // Копируем структуру данных.
                Array.Copy(arr[i], _charArr[i], arr[i].Length);
            }
        }

        /// <summary>
        /// Вспомогательный метод. Проверяет, является ли символ согласной буквой латинского алфавита.
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public bool IsConsonant(char ch)
        {
            // Список согласных.
            char[] consonants = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p',
                                  'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' };
            for (int i = 0; i < consonants.Length; i++)
            {
                // Проверка.
                if (ch == consonants[i] || ch.ToString() == consonants[i].ToString().ToUpper())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Метод для обработки данных. Возвращает структуру данных, 
        /// заполненную словами, состоящими только из согласных букв.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public char[][] OnlyConsonants()
        {
            // Проверка входных данных.
            if (_charArr == null) throw new ArgumentNullException();

            List<char[]> res = new List<char[]>();

            for (int i = 0; i < _charArr.Length; i++)
            {
                char[] word = _charArr[i];
                bool cFlag = true;
                // По условию слово должно быть ненулевой длины.
                if (word.Length == 0) cFlag = false;
                for (int l = 0; l < word.Length; l++)
                {
                    // Если в слове есть не согласные буквы - не добавляем его.
                    if (!IsConsonant(word[l]))
                    {
                        cFlag = false;
                    }
                }
                if (cFlag)
                {
                    // Если слово подходит - добавляем его.
                    res.Add(word);
                }
            }
            return res.ToArray();
        }

        /// <summary>
        /// Вспомогательный метод. Выводит структуру данных экземпляра класса на экран.
        /// </summary>
        public void PrintCharArr()
        {
            if (_charArr == null)
            {
                Console.WriteLine();
            }
            else
            {
                PrintCharArr(_charArr);
            }
        }

        /// <summary>
        /// Вспомогательный метод. Выводит переданную структуру данных на экран.
        /// </summary>
        /// <param name="arr"></param>
        public void PrintCharArr(char[][] arr)
        {
            if (_charArr == null || _charArr.Length == 0) Console.WriteLine();
            else
            {
                Console.Write(string.Join("", arr[0]));
                for (int i = 1; i < arr.Length; i++)
                {
                    Console.Write(" ");
                    Console.Write(string.Join("", arr[i]));
                }
                Console.WriteLine(".");
            }
        }

        /// <summary>
        /// Вспомогательный метод. Возвращает длину структуры данных.
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return _charArr.Length;
        }

        /// <summary>
        /// Переопределение методы ToString. Возвращает структуру данных ввиде строки. 
        /// Используется при записи результатов в файл.
        /// </summary>
        /// <returns></returns>
        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            // Сохраняем слова разделенные пробелом в предложение, которое оканчивается точкой.
            sb.Append(string.Join("", _charArr[0]));
            for (int i = 1; i < _charArr.Length; i++)
            {
                sb.Append(" ");
                sb.Append(string.Join("", _charArr[i]));
            }
            sb.Append(".");
            return sb.ToString();
        }
    }
}