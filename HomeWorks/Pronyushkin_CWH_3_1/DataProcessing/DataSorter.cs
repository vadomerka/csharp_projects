namespace DataProcessing
{
    // Делегат SorterDelegate для методов правил сортировки.
    public delegate int SorterDelegate(Player x, Player y);

    /// <summary>
    /// Класс DataSorter хранит правила сортировки.
    /// </summary>
    public static class DataSorter
    {
        /// <summary>
        /// Правило сортировки по полю playerId.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerIdSort(Player x, Player y)
        {
            if (x == null || y == null) return 0;
            return x.PlayerId.CompareTo(y.PlayerId);
        }

        /// <summary>
        /// Правило сортировки по полю userName.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerNameSort(Player x, Player y)
        {
            if (x == null || y == null) return 0;
            return x.UserName.CompareTo(y.UserName);
        }

        /// <summary>
        /// Правило сортировки по полю level.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerLevelSort(Player x, Player y)
        {
            if (x == null || y == null) return 0;
            return x.Level.CompareTo(y.Level);
        }

        /// <summary>
        /// Правило сортировки по полю gameScore.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerGameScoreSort(Player x, Player y)
        {
            if (x == null || y == null) return 0;
            return x.GameScore.CompareTo(y.GameScore);
        }

        /// <summary>
        /// Правило сортировки по полю achievements.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerInventorySort(Player x, Player y)
        {
            return x.Inventory.Length.CompareTo(y.Inventory.Length);
        }

        /// <summary>
        /// Правило сортировки по полю inventory.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerAchievementsSort(Player x, Player y)
        {
            return x.Achievements.Length.CompareTo(y.Achievements.Length);
        }

        /// <summary>
        /// Правило сортировки по полю guild.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int PlayerGuildSort(Player x, Player y)
        {
            return x.Guild.CompareTo(y.Guild);
        }
    }
}
