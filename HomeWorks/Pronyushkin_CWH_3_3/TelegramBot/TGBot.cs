using DataProcessing;
using Microsoft.VisualBasic;
using System.IO;
using System.Security.Claims;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TGBot
    {
        private TelegramBotClient? botClient = null;
        private List<ChatData> chatsData = new List<ChatData>();
        CSVProcessing cp = new CSVProcessing();
        private ReplyKeyboardMarkup menuKeyboard = new(new[]
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

        public TGBot() 
        {
            botClient = new TelegramBotClient("7188316095:AAECrzMzGiqOY3uhQSiTwyoQWBGxBL1f46k");
            using CancellationTokenSource cts = new();
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>(),
                ThrowPendingUpdates = true
            };
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            long chatId = update.Message.Chat.Id;
            var curChat = chatsData.Find(x => x.Id == chatId);
            if (curChat is null)
            {
                curChat = new ChatData(chatId);
                chatsData.Add(curChat);
            }

            string messageText = "";
            if (message.Text is not null)
                messageText = message.Text;
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            // Если пользователь прислал файл.
            Message? sentMessage = null;
            if (update.Message.Document is not null && 
                update.Message.Document.FileId is not null)
            {
                var fileId = update.Message.Document.FileId;
                var fileName = update.Message.Document.FileName;
                using (Stream fileStream = new MemoryStream())
                {
                    var file = await botClient.GetInfoAndDownloadFileAsync(
                        fileId: fileId,
                        destination: fileStream,
                        cancellationToken: cancellationToken);
                    fileStream.Seek(0, SeekOrigin.Begin);
                    try
                    {
                        curChat.BufferData = await cp.ReadAsync(fileStream);
                        fileStream.Seek(0, SeekOrigin.Begin);
                    }
                    catch (Exception ex)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Произошла ошибка.\n{ex.Message}",
                            cancellationToken: cancellationToken);
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
                    sentMessage = await botClient.SendTextMessageAsync(
                                    chatId: chatId,
                                    text: "Вы загрузили новый файл.\nВы хотите перезаписать данные?",
                                    replyMarkup: yesNoChoice,
                                    cancellationToken: cancellationToken);
                    return;
                }
                else
                {
                    curChat.Data = curChat.BufferData;
                    sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Данные сохранены.",
                        cancellationToken: cancellationToken);
                }
            }
            // Ответный центр.
            switch (messageText)
            {
                case "Произвести выборку.":
                    if (curChat.Data is null)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Загрузите файл с данными.",
                            cancellationToken: cancellationToken);
                        return;
                    }
                    break;
                case "Отсортировать данные.":
                    if (curChat.Data is null)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Загрузите файл с данными.",
                            cancellationToken: cancellationToken);
                        return;
                    }
                    break;
                case "Скачать данные.":
                    if (curChat.Data is null)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Загрузите файл с данными.",
                            cancellationToken: cancellationToken);
                        return;
                    }
                    try
                    {
                        using (Stream fileStream = await cp.WriteAsync(curChat.Data))
                        {
                            fileStream.Seek(0, SeekOrigin.Begin);
                            sentMessage = await botClient.SendDocumentAsync(
                                chatId: chatId,
                                document: InputFile.FromStream(stream: fileStream, fileName: "plants.csv"),
                                caption: "Обработанные данные:");
                            fileStream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                    catch (Exception ex)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: $"Произошла ошибка.\n{ex.Message}",
                            cancellationToken: cancellationToken);
                    }
                    break;
                case "Перезаписать.":
                    if (curChat.BufferData is null)
                    {
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Нет данных для перезаписи.",
                            cancellationToken: cancellationToken);
                        break;
                    }
                    curChat.Data = curChat.BufferData;
                    curChat.BufferData = null;
                    sentMessage = await botClient.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Данные перезаписаны.",
                            cancellationToken: cancellationToken);
                    break;
                case "Отменить.":
                    curChat.BufferData = null;
                    break;
                default:
                    break;
            }
            sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Выберите действие с данными.",
                        replyMarkup: menuKeyboard,
                        cancellationToken: cancellationToken);
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

             Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}