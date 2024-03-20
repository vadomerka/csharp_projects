using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.InnerHandlers
{
    public class TGFetchMessagesHandler
    {
        /// <summary>
        /// Метод собирает данные от пользователя, по которым будет проведена выборка.
        /// </summary>
        /// <param name="update">Данные о сообщении</param>
        /// <param name="botClient">Экземпляр бота</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <returns>Нужно ли продолжать главный цикл программы.</returns>
        public static async Task<bool> HandleFetchingMessages(ITelegramBotClient botClient, Update update,
            ChatData curChat, CancellationToken cancellationToken)
        {
            if (botClient is null ||
                update.Message is null ||
                update.Message.Text is null) return false;
            if (curChat.IsFetching())
            {
                curChat.FetchedValues.Add(update.Message.Text);
                if (!curChat.IsFetching())
                {
                    try
                    {
                        // Попытка отфильтровать данные.
                        var plants = TGHelper.FetchPlants(curChat);
                        curChat.UpdatePlants(plants);
                        await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                            text: "Данные отфильтрованы.",
                            cancellationToken: cancellationToken);
                    }
                    catch (ArgumentNullException)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                            text: "Объекты не найдены. Результат не сохранен.",
                            cancellationToken: cancellationToken);
                        // Останавливаем выборку.
                        curChat.FetchCount = 0;
                    }
                    await TGHelper.SendMenuMessage(botClient, curChat, cancellationToken);
                    return false;
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите следующее значение.",
                        replyMarkup: new ReplyKeyboardRemove(),
                        cancellationToken: cancellationToken);
                    return false;
                }
            }
            // Если не считываем сообщения для выборки, продолжаем главный цикл программы.
            return true;
        }

    }
}
