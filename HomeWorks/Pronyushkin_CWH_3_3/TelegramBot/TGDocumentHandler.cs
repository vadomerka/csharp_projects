using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using DataProcessing;

namespace TelegramBot
{
    public static class TGDocumentHandler
    {
        public static async Task<bool> HandleUploadedDocuments(ITelegramBotClient botClient, Update update, 
            ChatData curChat, CancellationToken cancellationToken)
        {
            CSVProcessing cp = new CSVProcessing();
            JSONProcessing jp = new JSONProcessing();
            if (botClient is null) return false;
            if (update.Message is not null &&
                update.Message.Document is not null &&
                update.Message.Document.FileId is not null)
            {
                var fileId = update.Message.Document.FileId;
                using (Stream fileStream = new MemoryStream())
                {
                    var file = await botClient.GetInfoAndDownloadFileAsync(
                        fileId: fileId,
                        destination: fileStream,
                        cancellationToken: cancellationToken);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    string fileType = update.Message.Document.MimeType ?? "";
                    try
                    {
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
                            await botClient.SendTextMessageAsync(
                                chatId: curChat.Id,
                                text: "Неверный формат файла.",
                                cancellationToken: cancellationToken);
                            return true;
                        }
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }
                    catch (Exception ex)
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: curChat.Id,
                            text: $"Произошла непредвиденная ошибка. Повторите попытку позже.",
                            cancellationToken: cancellationToken);
                        return true;
                    }
                }
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
                else
                {
                    curChat.Data = curChat.BufferData;
                    await botClient.SendTextMessageAsync(
                        chatId: curChat.Id,
                        text: "Данные сохранены.",
                        cancellationToken: cancellationToken);
                    return true;
                }
            }
            return true;
        }
    }
}
