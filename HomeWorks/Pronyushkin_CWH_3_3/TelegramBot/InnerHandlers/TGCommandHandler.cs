using DataProcessing;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InnerHandlers
{
    /// <summary>
    /// Вспомогательный класс. Обрабатывает команды.
    /// </summary>
    public static class TGCommandHandler
    {
        /// <summary>
        /// Метод обрабатывает команду выборки.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task FetchCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            // Отправка сообщения с inline клавиатурой.
            TGBot.Logger().LogInformation($"Обработка команды Fetch.");
            await botClient.SendTextMessageAsync(
                chatId: curChat.Id,
                text: "По какому полю вы хотите провести выборку?",
                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("LandscapingZone.", "fetch/LandscapingZone"),
                        InlineKeyboardButton.WithCallbackData("LocationPlace.", "fetch/LocationPlace"),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("LandscapingZone + ProsperityPeriod.",
                        "fetch/LandscapingZone&ProsperityPeriod")
                    }
                }),
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Метод обрабатывает команду сортировки.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task SortCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            TGBot.Logger().LogInformation($"Обработка команды Sort.");
            await botClient.SendTextMessageAsync(
                chatId: curChat.Id,
                text: "В каком порядке вы хотите отсортировать объекты?\nСортировка будет происходить по полю LatinName",
                replyMarkup: new InlineKeyboardMarkup(new InlineKeyboardButton[][] {
                    new[] {
                        InlineKeyboardButton.WithCallbackData("В прямом.", "sort/straight"),
                        InlineKeyboardButton.WithCallbackData("В обратном.", "sort/reverse"),
                    }}),
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Метод обрабатывает команду скачивания данных.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task DownloadCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            TGBot.Logger().LogInformation($"Обработка команды Download.");
            ReplyKeyboardMarkup formatChoice = new(new[]
            {
                new KeyboardButton[] {
                    "Скачать в формате csv.",
                    "Скачать в формате json."
                }
            })
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(
                chatId: curChat.Id,
                text: "В каком формате вы хотите скачать данные?",
                replyMarkup: formatChoice,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Метод обрабатывает команду скачивания в формате csv.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task DownloadCSVCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
            try
            {
                CSVProcessing cp = new CSVProcessing();
                // Попытка загрузки данных в поток.
                using (Stream fileStream = await cp.WriteAsync(curChat.Data))
                {
                    if (fileStream is null) throw new ArgumentNullException();
                    // Отправка.
                    await TGHelper.SendStreamFile(fileStream, curChat, botClient, cancellationToken);
                }
            }
            catch
            {
                TGBot.Logger().LogError($"Произошла ошибка при формировании файла.");
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: $"Произошла ошибка при формировании файла. Повторите попытку позже.",
                    cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Метод обрабатывает команду скачивания в формате json.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task DownloadJSONCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            try
            {
                JSONProcessing jp = new JSONProcessing();
                var plants = curChat.GetPlants();
                using (Stream fileStream = await jp.WriteAsync(plants))
                {
                    if (fileStream is null) throw new ArgumentNullException();
                    await TGHelper.SendStreamFile(fileStream, curChat, botClient, cancellationToken, "plants.json");
                }
            }
            catch
            {
                TGBot.Logger().LogError($"Произошла ошибка при формировании файла.");
                await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: $"Произошла ошибка при формировании файла. Повторите попытку позже.",
                    cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Метод обрабатывает команду перезаписи данных.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task RewriteCommandAsync(ITelegramBotClient botClient, ChatData curChat, CancellationToken cancellationToken)
        {
            TGBot.Logger().LogInformation($"Обработка команды Rewrite.");
            curChat.Data = curChat.BufferData;
            curChat.BufferData = null;
            await botClient.SendTextMessageAsync(
                    chatId: curChat.Id,
                    text: "Данные перезаписаны.",
                    cancellationToken: cancellationToken);
        }
    }
}
