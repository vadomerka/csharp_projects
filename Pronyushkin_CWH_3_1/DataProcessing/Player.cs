using System.Text;

namespace DataProcessing
{
    /// <summary>
    /// Класс Player - объект, считываемый из json формата.
    /// </summary>
    public class Player
    {
        private int playerId;
        private string userName;
        private int level;
        private int gameScore;
        private string[] achievements;
        private string[] inventory;
        private string guild;

        public Player() { }

        public Player(int id, string name, int accLevel, int score, 
            string[] advancements, string[] items, string club) 
        {
            playerId = id;
            userName = name;
            level = accLevel;
            gameScore = score;
            achievements = advancements;
            inventory = items;
            guild = club;
        }

        /// <summary>
        /// Конструктор класса Player от списка аттрибутов. Используется при создании экземпляра из json.
        /// </summary>
        /// <param name="attributes"></param>
        /// <exception cref="ArgumentException"></exception>
        public Player(object[] attributes)
        {
            // Попытка приведения типов.
            try
            {
                playerId = (int)attributes[0];
                userName = (string)attributes[1];
                level = (int)attributes[2];
                gameScore = (int)attributes[3];
                achievements = (string[])attributes[4];
                inventory = (string[])attributes[5];
                guild = (string)attributes[6];
            }
            catch (Exception e)
            {
                throw new ArgumentException();
            }
        }

        // Свойства только для чтения.
        public int PlayerId { get { return playerId; } }
        public string UserName { get { return userName; } }
        public int Level { get { return level; } }
        public int GameScore { get { return gameScore; } }
        public string[] Achievements { get { return achievements; } }
        public string[] Inventory { get { return inventory; } }
        public string Guild { get { return guild; } }

        /// <summary>
        /// Метод Jsonify возвращает объект в формате json.
        /// </summary>
        /// <returns></returns>
        public string Jsonify()
        {
            StringBuilder res = new StringBuilder();
            // Табуляция.
            string t = "  ";
            // Запись первых аттрибутов объекта.
            res.AppendLine($"{t}{{");
            res.AppendLine($"{t}{t}\"player_id\": {playerId},");
            res.AppendLine($"{t}{t}\"username\": \"{userName}\",");
            res.AppendLine($"{t}{t}\"level\": {level},");
            res.AppendLine($"{t}{t}\"game_score\": {gameScore},");
            // Запись списка "achievements".
            res.AppendLine($"{t}{t}\"achievements\": [");
            for (int i = 0; i < achievements.Length - 1; i++)
            {
                res.AppendLine($"{t}{t}{t}\"{achievements[i]}\",");
            }
            if (achievements.Length > 0) res.AppendLine($"{t}{t}{t}\"{achievements[^1]}\"");
            res.AppendLine($"{t}{t}],");
            // Запись списка "inventory".
            res.AppendLine($"{t}{t}\"inventory\": [");
            for (int i = 0; i < inventory.Length - 1; i++)
            {
                res.AppendLine($"{t}{t}{t}\"{inventory[i]}\",");
            }
            if (inventory.Length > 0) res.AppendLine($"{t}{t}{t}\"{inventory[^1]}\"");
            res.AppendLine($"{t}{t}],");
            // Запись оставшихся аттрибутов объекта.
            res.AppendLine($"{t}{t}\"guild\": \"{guild}\"");
            // Закрытие скобок.
            res.Append($"{t}}}");
            return res.ToString();
        }
    }
}