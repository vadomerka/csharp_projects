namespace DataProcessing
{
    // Делегат DataFilterDelegate для методов правил фильтрации.
    public delegate bool DataFilterDelegate(Player p, string value);

    /// <summary>
    /// Класс DataFilter Фильтрует объекты и хранит правила фильтрации.
    /// </summary>
    public static class DataFilter
    {
        /// <summary>
        /// Метод FilterPlayers фильтрует список объектов по правилу фильтрации и значению.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filterRule"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<Player> FilterPlayers(List<Player> data, DataFilterDelegate filterRule, string filter)
        {
            if (data == null) return data;
            List<Player> res = new List<Player>(data.Count);
            foreach (Player p in data)
            {
                // Если объект не пуст.
                if (p != null)
                {
                    // Применяем правило фильтрации.
                    if (filterRule(p, filter))
                    {
                        // Добавляем объект, если он прошел.
                        res.Add(p);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Правило фильтрации по полю playerId.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool IdFilterRule(Player player, string filter)
        {
            if (player == null || string.IsNullOrEmpty(filter)) return false;
            return player.PlayerId.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Правило фильтрации по полю userName.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool UserNameFilterRule(Player player, string filter)
        {
            if (player == null || string.IsNullOrEmpty(player.UserName)
                || string.IsNullOrEmpty(filter)) return false;
            return player.UserName.Contains(filter, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Правило фильтрации по полю level.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool LevelFilterRule(Player player, string filter)
        {
            if (player == null || string.IsNullOrEmpty(filter)) return false;
            return player.Level.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Правило фильтрации по полю gameScore.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool ScoreFilterRule(Player player, string filter)
        {
            if (player == null || string.IsNullOrEmpty(filter)) return false;
            return player.GameScore.ToString().Contains(filter, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Правило фильтрации по полю achievements.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool AchievementsFilterRule(Player player, string filter)
        {
            if (player == null || player.Achievements == null
                    || string.IsNullOrEmpty(filter)) return false;
            foreach (string str in player.Achievements)
            {
                if (str != null && str.Contains(filter, StringComparison.OrdinalIgnoreCase)) 
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Правило фильтрации по полю inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool InventoryFilterRule(Player player, string filter)
        {
            if (player == null || player.Inventory == null
                    || string.IsNullOrEmpty(filter)) return false;
            foreach (string str in player.Inventory)
            {
                if (str != null && str.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Правило фильтрации по полю guild.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static bool GuildFilterRule(Player player, string filter)
        {
            if (player == null || string.IsNullOrEmpty(player.Guild)
                || string.IsNullOrEmpty(filter)) return false;
            return player.Guild.Contains(filter, StringComparison.OrdinalIgnoreCase);
        }
    }
}
