// Пронюшкин Радомир БПИ234 вариант 4.
// Ссылка на бота:
// https://t.me/tg_c_sharp_test_2_bot.
using Microsoft.Extensions.Logging;
using Pronyushkin_CWH_3_3;
using TelegramBot;

namespace Program
{
    public class Program
    {
       public static void Main(string[] args)
       {
            string logFilePath = Path.Combine("..", "..", "..", "..", "telegram_log.txt");

            using (StreamWriter logFileWriter = new StreamWriter(logFilePath, append: true))
            {
                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                {
                    builder.AddSimpleConsole(options =>
                    {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    });

                    builder.AddProvider(new TelegramLoggerProvider(logFileWriter));
                });

                Console.WriteLine("Запуск телеграм бота.");
                var tgb = new TelegramBot.TGBot(loggerFactory);
                Console.ReadLine();
            }
        }
    }
}