using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.InnerHandlers;

namespace TelegramBot.UpdateHandlers
{
    /// <summary>
    /// Вспомогательный класс. Обрабатывает Callbacks
    /// </summary>
    public static class TGUpdateCallbackHandler
    {
        /// <summary>
        /// Метод обрабатывает Callbacks.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Информация о сообщении.</param>
        /// <param name="chatsData">Информация о чатах.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task HandleCallbacksAsync(ITelegramBotClient botClient, Update update,
            List<ChatData> chatsData, CancellationToken cancellationToken)
        {
            if (update.CallbackQuery is null || update.CallbackQuery.Message is null) return;
            // Находим чат в базе данных.
            var chatId = update.CallbackQuery.Message.Chat.Id;
            var curChat = await TGHelper.GetCurChat(botClient, chatId, chatsData, cancellationToken);
            // Проверяем загружены ли данные в чат.
            if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;

            TGBot.Logger().LogInformation("Бот принял Callback.");

            // Получаем список объектов.
            var plants = curChat.GetPlants();
            switch (update.CallbackQuery.Data)
            {
                // Проводим сортировку сразу.
                case "sort/straight":
                    plants.Sort((x, y) =>
                    {
                        return x.LatinName.CompareTo(y.LatinName);
                    });
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Данные отсортированы.",
                        cancellationToken: cancellationToken);
                    TGBot.Logger().LogInformation("Данные отсортированы.");
                    break;
                case "sort/reverse":
                    plants.Sort((x, y) =>
                    {
                        return y.LatinName.CompareTo(x.LatinName);
                    });
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Данные отсортированы.",
                        cancellationToken: cancellationToken);
                    TGBot.Logger().LogInformation("Данные отсортированы.");
                    break;
                // Запускаем выборку, настроив, сколько значений нужно получить от пользователя.
                case "fetch/LandscapingZone":
                    // Количество значений.
                    curChat.FetchCount = 1;
                    // Список, в который будут заноситься значения пользователя.
                    curChat.FetchedValues = new List<string>();
                    // Столбцы, по которым пройдет выборка.
                    curChat.FetchedCols = new string[] { "LandscapingZone", "" };
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите значение по которому нужно отфильтровать данные.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                case "fetch/LocationPlace":
                    curChat.FetchCount = 1;
                    curChat.FetchedValues = new List<string>();
                    curChat.FetchedCols = new string[] { "LocationPlace", "" };
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите значение по которому нужно отфильтровать данные.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                case "fetch/LandscapingZone&ProsperityPeriod":
                    curChat.FetchCount = 2;
                    curChat.FetchedValues = new List<string>();
                    curChat.FetchedCols = new string[] { "LandscapingZone", "ProsperityPeriod" };
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите значение по которому нужно отфильтровать данные.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    break;
                default:
                    break;
            }
            // Обновляем значения в базе данных.
            curChat.UpdatePlants(plants);
            await botClient.AnswerCallbackQueryAsync(update.CallbackQuery.Id, cancellationToken: cancellationToken);
        }
    }
}
