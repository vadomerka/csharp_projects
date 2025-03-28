﻿using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.InnerHandlers;

namespace TelegramBot.UpdateHandlers
{
    /// <summary>
    /// Вспомогательный класс. Обрабатывает Messages.
    /// </summary>
    public static class TGUpdateMessageHandler
    {
        /// <summary>
        /// Метод обрабатывает Messages.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Информация о сообщении.</param>
        /// <param name="chatsData">Информация о чатах.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Не возвращает значений.</returns>
        public static async Task HandleMessagesAsync(ITelegramBotClient botClient, Update update,
            List<ChatData> chatsData, CancellationToken cancellationToken)
        {
            if (update.Message is null) return;
            var chatId = update.Message.Chat.Id;
            var curChat = await TGHelper.GetCurChat(botClient, chatId, chatsData, cancellationToken);

            string messageText = "";
            if (update.Message.Text is not null)
                messageText = update.Message.Text;
            
            // Если пользователь прислал файл.
            if (!await TGDocumentHandler.HandleUploadedDocuments(botClient, update, curChat, cancellationToken)) return;

            // Если нужно считать значения для выборки.
            if (!await TGFetchMessagesHandler.HandleFetchingMessages(botClient, update, curChat, cancellationToken)) return;

            // Обработка команд.
            TGBot.Logger().LogInformation($"Бот принял сообщение \"{messageText}\".");
            switch (messageText)
            {
                case "Произвести выборку.":
                    if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
                    await TGCommandHandler.FetchCommandAsync(botClient, curChat, cancellationToken);
                    return;
                case "Отсортировать данные.":
                    if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
                    await TGCommandHandler.SortCommandAsync(botClient, curChat, cancellationToken);
                    return;
                case "Скачать данные.":
                    if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
                    await TGCommandHandler.DownloadCommandAsync(botClient, curChat, cancellationToken);
                    return;
                case "Скачать в формате csv.":
                    await TGCommandHandler.DownloadCSVCommandAsync(botClient, curChat, cancellationToken);
                    return;
                case "Скачать в формате json.":
                    if (await TGHelper.CurChatDataCheck(botClient, curChat, cancellationToken) || curChat.Data is null) return;
                    await TGCommandHandler.DownloadJSONCommandAsync(botClient, curChat, cancellationToken);
                    return;
                case "Перезаписать.":
                    if (botClient is null) return;
                    if (curChat.BufferData is null)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                        text: "Нет данных для перезаписи.",
                            cancellationToken: cancellationToken);
                        return;
                    }
                    await TGCommandHandler.RewriteCommandAsync(botClient, curChat, cancellationToken);
                    break;
                case "Отменить.":
                    curChat.BufferData = null;
                    break;
                default:
                    break;
            }
            if (curChat.Data is not null)
            {
                await TGHelper.SendMenuMessage(botClient, curChat, cancellationToken);
            }
            else
            {
                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Загрузите файл с данными.",
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            }
        }
    }
}
