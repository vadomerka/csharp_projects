namespace ClassLibrary
{
    /// <summary>
    /// Абстрактный класс Item для удобного хранения заголовка и адвокатов в одном массиве.
    /// </summary>
    public abstract class Item
    {
        public virtual string Number => "";
        public virtual string LastName => "";
        public virtual string Name => "";
        public virtual string SurName => "";
        public virtual string[] Data => null;
        public virtual Requisites[] Requisites => null;
        public virtual string ToSmartString(int[] mwl) { return ""; }
    }

    /// <summary>
    /// Класс Заголовок. Наследник Item.
    /// </summary>
    public class Header : Item
    {
        // Сохраняем инициализирующие данные.
        private string[] _columns;

        public Header() { }

        public Header(string[] data)
        {
            _columns = data;
        }

        /// <summary>
        /// Метод для записи в файл.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(",", _columns);
        }

        /// <summary>
        /// Метод для красивого вывода на экран.
        /// </summary>
        /// <param name="maxwordLen"></param>
        /// <returns></returns>
        public override string ToSmartString(int[] maxwordLen)
        {
            char sep = ' ';
            string res = string.Empty;
            string[] info = new string[] { "№", "Фамилия", "Имя", "Отчество" };
            for (int i = 0; i < 4; i++)
            {
                // Разделяем слова нужным количеством пробелов.
                res += info[i] + new string(sep, maxwordLen[i] + 1 - info[i].Length) + "|";
            }
            return res;
        }
    }

    /// <summary>
    /// Класс Адвокат. Наследник Item.
    /// </summary>
    public class Lawer : Item
    {
        // Сохраняем инициализирующие данные.
        private string[] _data;
        // Поля Адвоката.
        private string _number;
        private string _lastName;
        private string _name;
        private string _surName;
        private Requisites[] _requisites;

        public Lawer() { }

        public Lawer(string[] values)
        {
            _data = values;
            _number = values[0];
            _lastName = values[1];
            _name = values[2];
            _surName = values[3];
            List<Requisites> reqs = new List<Requisites>();
            for (int i = 4; i < values.Length; i++)
            {
                // Создаем реквизиты из пустых и непустых строчек.
                if (string.IsNullOrEmpty(values[i])) { reqs.Add(new Requisites()); continue; }
                string[] numDate = values[i].Split("от");
                if (numDate.Length < 2) { reqs.Add(new Requisites()); continue; }
                reqs.Add(new Requisites(numDate[0], numDate[1]));
            }
            _requisites = reqs.ToArray();
        }

        public new string Number => _number;

        public new string LastName => _lastName;

        public new string Name => _name;

        public new string SurName => _surName;

        public new string[] Data => _data;

        public new Requisites[] Requisites => _requisites;

        /// <summary>
        /// Метод для записи в файл.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Join(",", _data);
        }

        /// <summary>
        /// Метод для красивого вывода на экран.
        /// </summary>
        /// <param name="maxwordLen"></param>
        /// <returns></returns>
        public override string ToSmartString(int[] maxwordLen)
        {
            char sep = ' ';
            string res = string.Empty;
            string[] info = new string[] { _number, _lastName, _name, _surName };
            for (int i = 0; i < 4; i++)
            {
                // Разделяем слова нужным количеством пробелов.
                res += info[i] + new string(sep, maxwordLen[i] + 1 - info[i].Length) + "|";
            }
            return res;
        }
    }

    /// <summary>
    /// Структура Реквизиты. Используется в Адвокате.
    /// </summary>
    public struct Requisites
    {
        private string _number;
        private string _date;

        public Requisites() : this("", "") { }

        public Requisites(string number, string date)
        {
            // В файле встречаются записи с лишними пробелами, удаляем их.
            _number = number.Trim();
            _date = date.Trim();
        }

        public string Number => _number;

        public string Date => _date;

        /// <summary>
        /// Метод для сравнения ввода пользователя с реквизитом.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{_number} от {_date}";
        }
    }
}
