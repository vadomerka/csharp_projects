namespace Program
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Запуск телеграм бота.");
            var tgb = new TelegramBot.TGBot();
            Console.WriteLine("Телеграм бот успешно запущен.");
            Console.ReadLine();
        }
    }
}