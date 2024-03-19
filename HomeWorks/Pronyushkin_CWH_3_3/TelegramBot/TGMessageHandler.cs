using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TGMessageHandler
    {
        private static ReplyKeyboardMarkup menuKeyboard = new(new[]
        {
            new KeyboardButton[] {
                "Произвести выборку.",
                "Отсортировать данные."
            },
            new KeyboardButton[] {
                "Скачать данные."
            }
        })
        {
            ResizeKeyboard = true
        };

        /// <summary>
        /// Метод собирает данные от пользователя, по которым будет проведена выборка.
        /// </summary>
        /// <param name="update">Данные о сообщении</param>
        /// <param name="botClient">Экземпляр бота</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <returns>Нужно ли продолжать главный цикл программы.</returns>
        public static async Task<bool> HandleFetchingMessages(Update update, ITelegramBotClient botClient,
            CancellationToken cancellationToken, ChatData curChat)
        {
            if (botClient is null ||
                update.Message is null ||
                update.Message.Text is null) return false;
            if (curChat.IsFetching())
            {
                curChat.FetchedValues.Add(update.Message.Text);
                if (!curChat.IsFetching())
                {
                    var plants = TGHelper.FetchPlants(curChat);
                    curChat.UpdatePlants(plants);
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Данные отфильтрованы.",
                        cancellationToken: cancellationToken);
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Выберите действие с данными.",
                        replyMarkup: menuKeyboard,
                        cancellationToken: cancellationToken);
                    return false;
                }
                else
                {
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Введите следующее значение.",
                        cancellationToken: cancellationToken);
                    return false;
                }
            }
            // Если не считываем сообщения для выборки, продолжаем главный цикл программы.
            return true;
        }

    }
}
