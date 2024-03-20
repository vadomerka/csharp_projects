using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using DataProcessing;

namespace TelegramBot.InnerHandlers
{
    /// <summary>
    /// Вспомогательный класс. Обрабатывает команды.
    /// </summary>
    public static class TGDocumentHandler
    {
        /// <summary>
        /// Метод обрабатывает загрузку документов.
        /// </summary>
        /// <param name="botClient">Бот клиент.</param>
        /// <param name="update">Информация о сообщении.</param>
        /// <param name="curChat">Текущий чат.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>Нужно ли продолжать главный цикл программы.</returns>
        public static async Task<bool> HandleUploadedDocuments(ITelegramBotClient botClient, Update update,
            ChatData curChat, CancellationToken cancellationToken)
        {
            // Создание обработчиков документов.
            CSVProcessing cp = new CSVProcessing();
            JSONProcessing jp = new JSONProcessing();
            if (botClient is null) return false;
            if (update.Message is not null &&
                update.Message.Document is not null &&
                update.Message.Document.FileId is not null)
            {
                try
                {
                    var fileId = update.Message.Document.FileId;
                    using (Stream fileStream = new MemoryStream())
                    {
                        // Попытка чтения файла.
                        var file = await botClient.GetInfoAndDownloadFileAsync(
                            fileId: fileId,
                            destination: fileStream,
                            cancellationToken: cancellationToken);
                        fileStream.Seek(0, SeekOrigin.Begin);

                        string fileType = update.Message.Document.MimeType ?? "";
                        if (fileType == "text/csv")
                        {
                            curChat.BufferData = await cp.ReadAsync(fileStream);
                        }
                        else if (fileType == "application/json")
                        {
                            curChat.BufferData = await jp.ReadAsync(fileStream);
                        }
                        else
                        {
                            // Если формат неверный, сообщаем пользователю.
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "Неверный формат файла.",
                                cancellationToken: cancellationToken);
                            return true;
                        }
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }
                }
                catch (FormatException)
                {
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: $"Произошла ошибка при чтении файла. Файл неверного формата.",
                        cancellationToken: cancellationToken);
                }
                catch
                {
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: $"Произошла ошибка при чтении файла. Повторите попытку позже.",
                        cancellationToken: cancellationToken);
                }
                // Если пользователь загрузил файл, но данные уже есть.
                if (curChat.Data is not null)
                {
                    ReplyKeyboardMarkup yesNoChoice = new(new[]
                    {
                        new KeyboardButton[] {
                            "Перезаписать.",
                            "Отменить."
                        }
                    })
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(
                                    chatId: curChat.Id,
                                    text: "Вы загрузили новый файл.\nВы хотите перезаписать данные?",
                                    replyMarkup: yesNoChoice,
                                    cancellationToken: cancellationToken);
                    return false;
                }
                // Если нет, сразу сохраняем данные в базу данных.
                else
                {
                    curChat.Data = curChat.BufferData;
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Данные сохранены.",
                        cancellationToken: cancellationToken);
                    curChat.Data = curChat.BufferData;
                    await TGHelper.SendMenuMessage(botClient, curChat, cancellationToken);
                    return true;
                }
            }
            return true;
        }
    }
}
